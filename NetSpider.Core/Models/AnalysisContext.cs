using System;
using System.Collections.Generic;
using System.Text;
using NetSpider.Core;
using System.Linq;
using System.Net.Http;
using NetSpider.Core.Common;

namespace NetSpider.Core.Models
{
    public class AnalysisContext
    {
        public string Content { get; set; }

        private readonly SpiderTask _task;

        private IServiceProvider _provider { get; set; }

        public AnalysisResult Status { get; set; } = AnalysisResult.Init;

        public bool HasData
        {
            get
            {
                if(Datas != null && Datas.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 解析到的数据
        /// </summary>
        public Dictionary<string, dynamic> Datas { get; private set; } = new Dictionary<string, dynamic>();

        public bool HasTasks
        {
            get
            {
                if(Tasks != null && Tasks.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 解析到的任务
        /// </summary>
        public List<SpiderTask> Tasks { get;private set; } = new List<SpiderTask>();
        private List<string> _taskurls = new List<string>();

        public AnalysisContext(SpiderTask task)
        {
            _task = task;
            //Content = _task.Response?.Content.ReadAsStringAsync().Result;
            Content = _task.GetContent();
        }


        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TData"></param>
        public void AddData<T>(T TData)
        {
            Type type = typeof (T);
            
            if (!Datas.ContainsKey(type.Name))
            {
                Datas.Add(type.Name, new List<T>());
            }

            Datas[type.Name].Add(TData);
        }

        /// <summary>
        /// 添加任务，当前页面链接去重
        /// </summary>
        /// <param name="url"></param>
        public void AddTask(string url)
        {
            try
            {
                if (_taskurls.Exists(i => i.Equals(url)))
                {
                    return;
                }

                _taskurls.Add(url);
                Tasks.Add(new SpiderTask(url, HttpMethod.Get));
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 批量添加任务，当前页面链接去重
        /// </summary>
        /// <param name="urls"></param>
        public void AddTasks(IEnumerable<string> urls)
        {
            urls = urls.Distinct().ToList();
            foreach (string url in urls)
            {
                AddTask(url);
            }
        }
    }
}
