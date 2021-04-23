using System;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Telegram.Bot;

namespace Clansty.tianlang
{
    static class C
    {
        internal const string Version = "4.2.0.7"; //20210310
        /// <summary>
        /// 机器人的 TGUID
        /// </summary>
        internal const long tguid = 0; //TODO

        internal static readonly MiraiHttpSessionOptions miraiSessionOpinions = new MiraiHttpSessionOptions(
            "127.0.0.1",
            33111,
            ""
        );


        internal static void Write(object text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static void WriteLn(object text, ConsoleColor color = ConsoleColor.White)
        {
            Console.WriteLine(@"[{0}]{1}",DateTime.Now,text);
        }

        internal static MiraiHttpSession QQ = null;
        internal static TelegramBotClient TG = null;

        public static string ConvertFileSize(long size)
        {
            const double BYTE = 1024;

            if (size < BYTE)
                return size + "B";
            if (size < Math.Pow(BYTE, 2))
                return (size / BYTE).ToString("f1") + "KB";
            if (size < Math.Pow(BYTE, 3))
                return (size / Math.Pow(BYTE, 2)).ToString("f1") + "MB";
            if (size < Math.Pow(BYTE, 4))
                return (size / Math.Pow(BYTE, 3)).ToString("f1") + "GB";
            return (size / Math.Pow(BYTE, 4)).ToString("f1") + "TB";
        }
    }
}