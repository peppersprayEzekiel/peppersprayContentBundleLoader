using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using YamlDotNet.Serialization;

namespace guidAssetNameLookup
{
    internal class FileReference
    {
        internal string Path;
        internal string GUID;

        public FileReference(string path, string guid)
        {
            Path = path;
            GUID = guid;
        }
    }
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            var inputFile = args[0];
            var searchFolder = args.Length >= 2 ? args[1] : @".\";
            var contents = fixYaml(File.ReadAllText(inputFile));

            var deserializer = new Deserializer();
            var obj = deserializer.Deserialize<Dictionary<object, object>>(contents);
            
            var references = findReferences(obj).ToList();
            var mappedReferences = findReferenceLocations(references, searchFolder);

            foreach (var kv in mappedReferences)
            {
                Console.WriteLine($"Reference {kv.Key.Path} (guid {kv.Key.GUID}) found at {kv.Value}");
                references.Remove(kv.Key);
            }

            foreach (var missingReference in references)
            {
                Console.WriteLine($"Couldn't find file for reference {missingReference.Path} (guid {missingReference.GUID})!");
            }
        }

        private static string fixYaml(string input)
        {
            var commentsRegex = new Regex("^(---|%).*?$", RegexOptions.Multiline);
            input = commentsRegex.Replace(input, "");
            
            var arrayRegex = new Regex("^(\\w+):$", RegexOptions.Multiline);
            var offset = 0;
            var names = new List<string>();
            
            foreach (Match match in arrayRegex.Matches(input))
            {
                var group = match.Groups[1];
                var count = names.Count(a => a == group.Value);
                var suffix = count == 0 ? "" : count + "";
                names.Add(group.Value);
                
                input = input.Insert(@group.Index + @group.Length + offset, suffix);
                offset += suffix.Length;
            }

            return input;
        }

        private static IEnumerable<FileReference> findReferences(IEnumerable tree, string path = @"\")
        {
            var result = new List<FileReference>();
            
            foreach (var element in tree)
            {
                switch (element)
                {
                    case KeyValuePair<string, object> el:
                    {
                        if (el.Value is IList || el.Value is IDictionary)
                        {
                            result.AddRange(findReferences(el.Value as IEnumerable, Path.Combine(path, el.Key)));
                            continue;
                        }

                        break;
                    }
                    
                    case KeyValuePair<object, object> el:
                    {
                        if (el.Value is IList || el.Value is IDictionary)
                        {
                            result.AddRange(findReferences(el.Value as IEnumerable, Path.Combine(path, el.Key.ToString())));
                            continue;
                        }

                        break;
                    }
                    
                    case IDictionary el:
                        result.AddRange(findReferences(el, path));
                        continue;
                    
                    case IList el:
                        result.AddRange(findReferences(el, path));
                        continue;
                }

                var property = element as KeyValuePair<object, object>?;
                if (property?.Key.ToString() == "guid")
                {
                    result.Add(new FileReference(path, property.Value.Value.ToString()));
                }
            }

            return result;
        }

        private static IEnumerable<KeyValuePair<FileReference, string>> findReferenceLocations(IEnumerable<FileReference> references, string lookupRoot)
        {
            var pendingReferences = references.ToList();
            
            foreach (var path in Directory.EnumerateFiles(lookupRoot, "*.meta", SearchOption.AllDirectories))
            {
                var stream = File.OpenText(path);
                var buf = new char[64];
                var foundReference = false;
                
                while (!stream.EndOfStream)
                {
                    stream.Read(buf, 32, 32);

                    var bufString = new string(buf);
                    foreach (var r in pendingReferences.ToArray())
                    {
                        if (bufString.Contains(r.GUID))
                        {
                            pendingReferences.Remove(r);
                            foundReference = true;
                            
                            yield return new KeyValuePair<FileReference, string>(r, path);
                        }
                    }

                    if (foundReference)
                    {
                        break;
                    }
                    
                    Array.Copy(buf, 32, buf, 0, 32);
                }

                if (!pendingReferences.Any())
                {
                    break;
                }
            }
        }
    }
}