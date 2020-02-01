using System;
using BepInEx.Logging;
using UnityEngine;

namespace peppersprayContentBundleLoaderPlugin.Utils
{
    public class Log
    {
        public static Log Instance;

        private ManualLogSource _log;

        public Log(ManualLogSource log)
        {
            _log = log;
            
            Debug.Assert(log != null);
        }

        public void Error(string format, params object[] args)
        {
            _log.LogError(String.Format(format, args));
        }

        public void Info(string format, params object[] args)
        {
            _log.LogInfo(String.Format(format, args));
        }
        
        public void Warning(string format, params object[] args)
        {
            _log.LogWarning(String.Format(format, args));
        }
    }
}