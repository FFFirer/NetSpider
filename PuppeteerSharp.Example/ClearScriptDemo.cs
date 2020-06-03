using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript.V8;

namespace PuppeteerSharp.Example
{
    public class ClearScriptDemo
    {
        public void Execute()
        {
            string ScriptFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "demo.js");
            string scriptContent = string.Empty;
            using(FileStream fs = new FileStream(ScriptFilePath, FileMode.Open, FileAccess.Read))
            {
                using(StreamReader sr = new StreamReader(fs))
                {
                    scriptContent = sr.ReadToEnd().Replace("\r\n", "");
                }
            }

            using (var engine = new V8ScriptEngine())
            {
                engine.DocumentSettings.AccessFlags = Microsoft.ClearScript.DocumentAccessFlags.EnableFileLoading;
                engine.DefaultAccess = Microsoft.ClearScript.ScriptAccess.Full;
                engine.CompileDocument(ScriptFilePath);
                //var result = engine.Invoke("encodeURIComponent", "SHAURCOnewayduew&^%5d54nc'KH"); //只能调用全局方法
                scriptContent += "m(\"SHAURCOnewayduew&^%5d54nc'KH\");";
                var result = engine.Evaluate(scriptContent);

            }
        }
    }
}
