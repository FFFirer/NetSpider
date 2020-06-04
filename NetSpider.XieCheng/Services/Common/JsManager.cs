using System;
using System.Collections.Generic;
using System.Text;
using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Core;

namespace NetSpider.XieCheng.Services
{
    public class JsManager
    {
        private IJsEngine _engine;
        /// <summary>
        /// Js Engine Manager，管理Js脚本，请使用单例模式调用
        /// </summary>
        public JsManager()
        {
            var switcher = JsEngineSwitcher.Current;
            switcher.EngineFactories.Add(new ChakraCoreJsEngineFactory());
            switcher.DefaultEngineName = ChakraCoreJsEngine.EngineName;
            _engine = JsEngineSwitcher.Current.CreateDefaultEngine();
        }

        public void LoadScript(string ScriptPath)
        {
            if(_engine == null)
            {
                throw new ArgumentNullException($"{nameof(_engine)} is not defined. ");
            }

            _engine.ExecuteFile(ScriptPath);
        }

        public T Call<T>(string functionName, params object[] args)
        {
            if (_engine == null)
            {
                throw new ArgumentNullException($"{nameof(_engine)} is not defined. ");
            }

            return _engine.CallFunction<T>(functionName, args);
        }
    }
}
