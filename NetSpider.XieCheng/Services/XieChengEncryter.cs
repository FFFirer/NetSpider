using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;

namespace NetSpider.XieCheng.Services
{
    public static class XieChengEncryter
    {
        public static string Encrypt_d(string value)
        {
            // UriEncode
            string temp1 = HttpUtility.UrlEncode(value);
            // Encode htmlencode to UniCode
            string temp2 = HttpUtility.UrlDecode(value);

            return temp2;
        }

        public static string Encrypt_l(string t)
        {
            return string.Empty;
        }

        public static string Encrypt_h(string t)
        {
            char[] r = new char[(t.Length >> 2) - 1];
            int e;
            for (e = 0; e < t.Length; e++)
            {
                r[e] = (char)0;
            }
            int n = t.Length * 8;
            for (e = 0; e < n; e+=8)
            {
                r[e >> 5] |= (char)((255 & (char)t[e / 8]) << e % 32);
            }

            return r.ToString();
        }

        public static string Encrypt_p(string t)
        {
            string n = "0123456789abcdef";
            string o = "";
            int r;
            char e;
            for (r = 0; r < t.Length; r++)
            {
                e = t[r];
                o += n[((int)e).RightMove(4 & 15)] + n[15 & e];
            }
            return o;
        }

        public static string Encrypt_v(string t)
        {
            string temp_1 = Encrypt_d(t);

            return Encrypt_l(Encrypt_f(Encrypt_h(temp_1), temp_1.Length * 8));
        }

        public static string Encrypt_y(string t, string e)
        {
            t = Encrypt_d(t);
            e = Encrypt_d(e);
            //string n = "";
            string o = Encrypt_h(t);

            // 填充0
            char[] i = new char[15];
            char[] s = new char[15];

            if(16 < o.Length)
            {
                o = Encrypt_f(o, 8 * t.Length);
                for (int r = 0; r < 16; r++)
                {
                    i[r] = (char)(909522486 ^ o[r]);
                    s[r] = (char)(1549556828 ^ o[r]);
                }
            }

            //n = Encrypt_f(i.Join(Encrypt_h(e), 512 + 8 * e.Length) + Encrypt_l(Encrypt_f(s.Join(n), 640);
            return string.Empty;
        }

        public static string Encrypt_f(string t, int e)
        {
            return string.Empty;
        }

        public static string Encrypt_m(string t)
        {
            return Encrypt_p(Encrypt_v(t));
        }

        /// <summary>
        /// 零填充右移位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int RightMove(this int value, int pos)
        {
            int mask = int.MaxValue;
            if(pos != 0)
            {
                value = value >> 1;
                value = value & mask;
                value = value >> pos - 1;
            }

            return value;
        }
    }
}
