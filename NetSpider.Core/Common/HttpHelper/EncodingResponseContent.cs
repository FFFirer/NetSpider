using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace NetSpider.Core
{
    public class EncodingResponseContent
    {
        /// <summary>
        /// 将响应转换为正确编码的字符串
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string GetContent(HttpResponseMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (message.Content == null)
            {
                throw new NullReferenceException(nameof(message.Content));
            }

            if (message?.Content.Headers.ContentType?.CharSet != null)
            {
                // 根据编码类型解析
                return Encoding.GetEncoding(message.Content.Headers.ContentType.CharSet).GetString(message.Content.ReadAsByteArrayAsync().Result);
            }
            else
            {
                // 返回默认字符串
                return message.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
