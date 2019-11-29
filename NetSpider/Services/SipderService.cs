using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NetSpider.Models;
using System.Net.Http;
using NetSpider.Common;
using NetSpider.DAL;
using Microsoft.Extensions.Configuration;
using System.Linq;
using NLog;

namespace NetSpider.Services
{   
    /// <summary>
    /// 爬虫主服务
    /// </summary>
    public class SipderService : IHostedService
    {
        /// <summary>
        /// 日志组件
        /// </summary>
        ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// app host life time
        /// </summary>
        IHostApplicationLifetime _appLifeTime;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="appLifeTime">IHostApplicationLifetime实例</param>
        public SipderService(IHostApplicationLifetime appLifeTime)
        {
            _appLifeTime = appLifeTime;

        }

        /// <summary>
        /// 启动，异步
        /// </summary>
        /// <param name="cancellationToken">任务取消token</param>
        /// <returns>任务执行结果</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifeTime.ApplicationStarted.Register(OnStarted);
            _appLifeTime.ApplicationStopped.Register(OnStoped);
            _appLifeTime.ApplicationStopping.Register(OnStoping);
            //FilmDAL dal = new FilmDAL(_config);
            //dal.GetFilmCount();
            Seed();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 停止，异步
        /// </summary>
        /// <param name="cancellationToken">任务取消token</param>
        /// <returns>任务执行结果</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            SaveRunningEnv();
            logger.Info("Spider Service Stoped");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 启动完成时触发
        /// </summary>
        public void OnStarted()
        {
            logger.Info($"{nameof(SipderService)} Started!");
        }

        /// <summary>
        /// 停止中触发
        /// </summary>
        public void OnStoping()
        {
            logger.Warn($"{nameof(SipderService)} Stopping!");
        }

        /// <summary>
        /// 停止完成时触发
        /// </summary>
        public void OnStoped()
        {
            logger.Error($"{nameof(SipderService)} Stoped!");
        }

        /// <summary>
        /// 存放电影详情信息的页面连接
        /// </summary>
        private Queue<string> FilmInfoUrls = new Queue<string>();
        /// <summary>
        /// 有Film时触发
        /// </summary>
        private AutoResetEvent HasFilm = new AutoResetEvent(false);

        List<CategoryInfo> infos = new List<CategoryInfo>();

        /// <summary>
        /// 提供访问页面的HttpClient工厂
        /// </summary>
        private IHttpClientFactory _factory;
        private IConfiguration _config;
        private int FilmsPerPage = 30;
        private HttpClient Client;

        /// <summary>
        /// 操作film存储
        /// </summary>
        private FilmDAL _filmDAL { get; set; }

        /// <summary>
        /// 操作运行信息存储
        /// </summary>
        private RunningEnvDAL _runningEnvDAL { get; set; }

        /// <summary>
        /// 当前运行信息
        /// </summary>
        private RunningEnvModel _runningEnv { get; set; }

        /// <summary>
        /// 爬虫的Key
        /// </summary>
        private string SpiderKey = "Film1905";

        /// <summary>
        /// 不同首字母之间的停顿时间
        /// </summary>
        TimeSpan LetterPauseSpan = new TimeSpan(0, 0, 15);

        /// <summary>
        /// 每部电影之间的停顿时间
        /// </summary>
        TimeSpan FilmPauseSapn = new TimeSpan(0, 0, 1);

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpClientFactory">IHttpClientFactory实例</param>
        /// <param name="configuration">IConfiguration实例</param>
        public SipderService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _factory = httpClientFactory;
            _config = configuration;
            Client = _factory.CreateClient("1905");
            _filmDAL = new FilmDAL(configuration);
            _runningEnvDAL = new RunningEnvDAL(configuration);
            _runningEnv = _runningEnvDAL.GetLast(SpiderKey) ?? new RunningEnvModel()
            {
                SpiderKey = SpiderKey,
            };

            SaveRunningEnv();
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
        public void Seed(CancellationToken cancellationToken)
        {
            int FirstLetter = 65;
            int LastLetter = 90;
            string BaseUrl = "/mdb/film/list/enindex-";
            int CurrentCategoryIndex = 0;
            int CurrentPage = 1;

            // 获取分类信息
            for(int i = FirstLetter; i <= LastLetter; i++)
            {
                // 根据每个字母首页的影片数量，计算每个字母的总页数。
                string Letter = ((char)i).ToString();
                string url = BaseUrl + Letter;
                // 获取页面内容
                var httpMsg = Client.GetAsync(url).Result;
                FilmListPageHelper helper = new FilmListPageHelper(httpMsg.Content.ReadAsStringAsync().Result);
                int Count = helper.GetFilmCount();
                infos.Add(new CategoryInfo()
                {
                    CateIndex = Letter,
                    Count = Count,
                    Pages = (int)Math.Ceiling(Convert.ToDouble(Count) / Convert.ToDouble(FilmsPerPage))
                });
                logger.Info($"CateIndex:{Letter}, Count:{Count}");
            }

            CategoryInfo lastInfo = infos.Where(i => i.CateIndex.Equals(_runningEnv.CurrentIndex)).FirstOrDefault();
            CurrentCategoryIndex = infos.IndexOf(lastInfo);
            CurrentPage = _runningEnv.CurrentPage;

            // 遍历字母Index
            for (int i = CurrentCategoryIndex; i < infos.Count; i++)
            {
                CheckCancelled(cancellationToken);
                try
                {
                    CategoryInfo CurrentInfo = infos[i];
                    _runningEnv.CurrentIndex = CurrentInfo.CateIndex;
                    string BasePageUrl = $"/mdb/film/list/enindex-{CurrentInfo.CateIndex}/";

                    // 遍历每个字母Index下的页面；
                    for (int page = CurrentPage; page <= CurrentInfo.Pages; page++)
                    {
                        _runningEnv.CurrentPage = page;
                        string CurrentPageUrl = BasePageUrl;
                        // 生成电影列表页面链接
                        if (page > 1)
                        {
                            CurrentPageUrl = BasePageUrl + $"o0d0p{page}.html";
                        }
                        // 获取页面所有电影链接
                        FilmListPageHelper listPageHelper = new FilmListPageHelper(Client.GetAsync(CurrentPageUrl).Result.Content.ReadAsStringAsync().Result);
                        Dictionary<string, string> films = listPageHelper.GetFilmsUrls();
                        foreach (string filmId in films.Keys)
                        {
                            try
                            {
                                if (_filmDAL.IsExist(int.Parse(filmId)))
                                {
                                    logger.Info($"已存在，Id:{filmId}");
                                }
                                else
                                {
                                    string filmInfoUrl = films[filmId] + "info/";
                                    FilmDataHelper datahelper = new FilmDataHelper(Client.GetAsync(filmInfoUrl).Result.Content.ReadAsStringAsync().Result);
                                    FilmModel filmInfo = datahelper.GetFilms(int.Parse(filmId));
                                    if (_filmDAL.AddFilmeInfo(filmInfo))
                                    {
                                        logger.Info($"保存成功, Name:{filmInfo.Name}");
                                        continue;
                                    }
                                    else
                                    {
                                        logger.Warn($"保存失败，Id:{filmId}, Name:{filmInfo.Name}");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                            logger.Trace($"Next Film: after {FilmPauseSapn.TotalSeconds}s");
                            Thread.Sleep((int)FilmPauseSapn.TotalMilliseconds);
                        }

                        SaveRunningEnv();
                        logger.Trace($"Next Letter: after {LetterPauseSpan.TotalSeconds}s");
                        Thread.Sleep((int)LetterPauseSpan.TotalMilliseconds);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    break;
                }
            }

            Console.WriteLine("finish");

        }

        /// <summary>
        /// 判断任务是否取消
        /// </summary>
        /// <param name="token"></param>
        public void CheckCancelled(CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                throw new OperationCanceledException(token);
            }
        }

        /// <summary>
        /// 保存运行信息
        /// </summary>
        public void SaveRunningEnv()
        {
            try
            {
                _runningEnvDAL.SaveRecord(_runningEnv);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
    }

    /// <summary>
    /// 电影分类信息
    /// </summary>
    public class CategoryInfo
    {
        public string CateIndex { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
    }
}
