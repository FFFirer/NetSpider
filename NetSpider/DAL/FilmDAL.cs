using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using NetSpider.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace NetSpider.DAL
{
    public class FilmDAL
    {
        public AppSettings Setting { get; set; }
        public IConfiguration _config { get; set; }
        public FilmDAL(IConfiguration configuration)
        {
            _config = configuration;
            _config.GetSection("connectioStrings").Bind(Setting);
        }

        public void GetFilmCount()
        {
            string sql = "select count(1) from Film1905";
            using(IDbConnection connection = new SqlConnection(Setting.MsSqlConnection))
            {
                var res = (int)connection.ExecuteScalar(sql);
                Console.WriteLine(res);
            }
        }
    }
}
