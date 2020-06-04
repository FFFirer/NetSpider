using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;

namespace NetSpider.XieCheng.Services
{
    public class JavaScriptV8Manager
    {
        /// <summary>
        /// V8 Engine Manager
        /// </summary>
        public JavaScriptV8Manager()
        {
            _engine = new V8ScriptEngine();
            _engine.DocumentSettings.AccessFlags = DocumentAccessFlags.EnableFileLoading;
            _engine.DefaultAccess = ScriptAccess.Full;
        }

        public string ScriptPath { get; set; }
        private V8ScriptEngine _engine { get; set; } 

        public V8ScriptEngine Engine
        {
            get { return _engine; }
        }

        public void LoadScript(string scriptPath)
        {
            if(_engine == null)
            {
                throw new ArgumentNullException($"{nameof(V8ScriptEngine)} not init, is null");
            }
            else
            {
                V8Script script = _engine.CompileDocument(scriptPath);
                _engine.Execute(script);
            }
        }
    }
}
