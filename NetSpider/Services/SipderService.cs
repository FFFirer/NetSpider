using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NetSpider.Models;
using System.Net.Http;
using NetSpider.Common;

namespace NetSpider.Services
{
    /// <summary>
    /// 爬虫主服务
    /// </summary>
    public class SipderService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Start Spider Service");
            Seed();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }

        /// <summary>
        /// 存放电影详情信息的页面连接
        /// </summary>
        private Queue<string> FilmInfoUrls = new Queue<string>();
        /// <summary>
        /// 有Film时触发
        /// </summary>
        private AutoResetEvent HasFilm = new AutoResetEvent(false);

        /// <summary>
        /// 存放电影列表信息的页面
        /// </summary>
        private Queue<string> PageUrls = new Queue<string>();
        /// <summary>
        /// Has Page
        /// </summary>
        private AutoResetEvent HasPage = new AutoResetEvent(false);

        /// <summary>
        /// 提供访问页面的HttpClient工厂
        /// </summary>
        private IHttpClientFactory _factory;
        private int FilmsPerPage = 30;
        private HttpClient Client;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public SipderService(IHttpClientFactory httpClientFactory)
        {
            _factory = httpClientFactory;
            Client = _factory.CreateClient("1905");
        }

        /// <summary>
        /// 消化FilmInfoUrls，获取每部电影的信息，插入数据库
        /// </summary>
        public void FilmWorker()
        {

        }

        /// <summary>
        /// 消化PageInfoUrls，获取FilmInfoUrl插入FilmInfoUrls
        /// </summary>
        public void PageWorker()
        {

        }

        /// <summary>
        /// 种子方法，把抓取到的页面放进PageUrls
        /// </summary>
        public void Seed()
        {
            int FirstLetter = 65;
            int LastLetter = 90;
            List<CategoryInfo> infos = new List<CategoryInfo>();
            string BaseUrl = "/mdb/film/list/enindex-";
            // 获取上一次运行时的字母，到的页码
            //int CurrentLetter = FirstLetter;
            //int CurrentPage = 1;
            int AllCount = 0;
            for(int i = FirstLetter; i <= LastLetter; i++)
            {
                // 根据每个字母首页的影片数量，计算每个字母的总页数。
                string Letter = ((char)i).ToString();
                string url = BaseUrl + Letter;
                // 获取页面内容
                var httpMsg = Client.GetAsync(url).Result;
                int Count = XmlHelper.GetFilmCount(httpMsg.Content.ReadAsStringAsync().Result);
                infos.Add(new CategoryInfo()
                {
                    CateIndex = Letter,
                    Count = Count,
                    Pages = (int)Math.Ceiling(Convert.ToDouble(Count / Count))
                });
                AllCount += Count;
            }

            

            Console.WriteLine("finish");
        }
    }

    public class CategoryInfo
    {
        public string CateIndex { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
    }
}
