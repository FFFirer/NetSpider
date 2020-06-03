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

                //var result = engine.Invoke("encodeURIComponent", "SHAURCOnewayduew&^%5d54nc'KH"); //只能调用全局方法

                //scriptContent += "m(\"SHAURCOnewayduew&^%5d54nc'KH\");";
                //engine.Execute(scriptContent);  // 方案1：取得脚本里的所有内容，Execute一下，然后，调用engine.Script.func(x,y)执行一下。

                V8Script script = engine.CompileDocument(ScriptFilePath);   // 方案2：载入并编译js文件, 然后Execute, 就可以直接调用。
                engine.Execute(script);
                var result = engine.Script.m("SHAURCOnewayduew&^%5d54nc'KH");

            }
        }
    }
}
