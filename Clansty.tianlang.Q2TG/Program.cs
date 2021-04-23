using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Mirai_CSharp;
using Telegram.Bot;

namespace Clansty.tianlang
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        static async Task Main()
        {
            Console.CancelKeyPress += Exit;
            Console.Title = $@"Q2TG Bots Server {C.Version}";
            //init qq bots
            var nthsBotHandler = new BotEvents();
            C.QQ = new MiraiHttpSession();
            C.QQ.AddPlugin(nthsBotHandler);
            await C.QQ.ConnectAsync(C.miraiSessionOpinions, BotEvents.SELF);
            
            //init telegram bots
            C.TG = new TelegramBotClient(Tg.Token);
            C.TG.OnMessage += Tg.OnMsg;
            C.TG.StartReceiving();

            while (true)
                Console.ReadLine();
        }

        internal static void Exit(object a = null, object b = null)
        {
            var tt = Process.GetProcessById(Process.GetCurrentProcess().Id);
            tt.Kill();
        }
    }
}