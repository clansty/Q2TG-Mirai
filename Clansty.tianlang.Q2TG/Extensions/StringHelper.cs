using Mirai_CSharp.Models;

namespace Clansty.tianlang
{
    static class StringHelper
    {
        /// <summary>
        /// 取文本左边内容
        /// </summary>
        /// <param name="str">文本</param>
        /// <param name="s">标识符</param>
        /// <returns>左边内容</returns>
        internal static string GetLeft(this string str, string s)
        {
            try
            {
                return str.Substring(0, str.IndexOf(s));
            }
            catch
            {
                return "";
            }
        }


        /// <summary>
        /// 取文本右边内容
        /// </summary>
        /// <param name="str">文本</param>
        /// <param name="s">标识符</param>
        /// <returns>右边内容</returns>
        internal static string GetRight(this string str, string s)
        {
            try
            {
                return str.Substring(str.IndexOf(s) + s.Length);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 取文本中间内容
        /// </summary>
        /// <param name="str">原文本</param>
        /// <param name="leftstr">左边文本</param>
        /// <param name="rightstr">右边文本</param>
        /// <returns>返回中间文本内容</returns>
        internal static string Between(this string str, string leftstr, string rightstr)
        {
            var i = str.IndexOf(leftstr) + leftstr.Length;
            var temp = str.Substring(i, str.IndexOf(rightstr, i) - i);
            return temp;
        }

        internal static string LastBetween(this string str, string leftstr, string rightstr)
        {
            var i = str.LastIndexOf(leftstr) + leftstr.Length;
            var temp = str.Substring(i, str.IndexOf(rightstr, i) - i);
            return temp;
        }

        internal static PlainMessage[] MakeChain(this string msg)
        {
            return new[] {new PlainMessage(msg)};
        }
    }
}