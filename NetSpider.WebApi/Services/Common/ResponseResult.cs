using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetSpider.WebApi.Services
{
    public class ResponseResult
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public string error { get; set; }
        public object data { get; set; }
        private ResponseResult()
        {
            isSuccess = false;
            message = "无有效信息";
            error = "无有效信息";
            data = new object();
        }

        public static ResponseResult Default
        {
            get
            {
                return new ResponseResult();
            }
        }

        public static ResponseResult SetSuccess(dynamic Data)
        {
            return new ResponseResult
            {
                isSuccess = true,
                message = "请求成功",
                error = string.Empty,
                data = Data
            };
        }

        public static ResponseResult SetSuccess(string Message, dynamic Data)
        {
            return new ResponseResult
            {
                isSuccess = true,
                message = Message,
                error = string.Empty,
                data = Data
            };
        }

        public static ResponseResult SetError(string ErrorMessage)
        {
            return new ResponseResult
            {
                isSuccess = false,
                message = "请求失败",
                error = ErrorMessage,
                data = new object()
            };
        }

        public static ResponseResult SetError(string Message, string ErrorMessage)
        {
            return new ResponseResult
            {
                isSuccess = false,
                message = Message,
                error = ErrorMessage,
                data = new object()
            };
        }
    }
}
