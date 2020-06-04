using System;
using System.Collections.Generic;
using System.Text;
using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher;
using System.IO;

namespace PuppeteerSharp.Example
{
    public class JsEngineSwitcherDemo
    {
        public void Execute()
        {
            string ScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "demo.js");
            var switcher = JsEngineSwitcher.Current;
            switcher.EngineFactories.Add(new ChakraCoreJsEngineFactory());
            switcher.DefaultEngineName = ChakraCoreJsEngine.EngineName;
            IJsEngine engine = JsEngineSwitcher.Current.CreateDefaultEngine();
            engine.ExecuteFile(ScriptPath, Encoding.UTF8);
            string result = engine.CallFunction<string>("m", "SHAURCOnewayduew&^%5d54nc'KH");
        }
    }
}
