using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Mirai_CSharp;
using Mirai_CSharp.Extensions;
using Mirai_CSharp.Models;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace Clansty.tianlang
{
    static class Q2Tg
    {
        public static async Task NewGroupMsg(MiraiHttpSession recvQQ, long fromGroup, long fromQQ, string fromCard,
            IMessageBase[] chain)
        {
            var fwdinfo = Q2TgMap.Q2Tg(recvQQ, fromGroup);
            if (fwdinfo is null) return;

            var from = fromCard;

            var msg = chain.GetPlain().Trim(' ', '\r', '\n', '\t');

            #region replies

            var replies = chain.OfType<QuoteMessage>().ToArray();
            string replyIdStr = null;
            if (replies.Any())
            {
                var qrid = replies.First().Id;
                replyIdStr = Db.ldb.Get(qrid.ToString());
            }

            var replyId = 0;
            if (!string.IsNullOrWhiteSpace(replyIdStr))
                replyId = int.Parse(replyIdStr);

            #endregion

            #region xml cards

            var xmls = chain.OfType<XmlMessage>().ToArray();
            if (xmls.Any())
            {
                if(xmls.First().Xml.Contains(@"action=""viewMultiMsg"""))
                    msg += "\n[转发多条消息记录]";
                else if (xmls.First().Xml.Contains("url=\""))
                    msg += "\n" + msg.Between("url = \"", "\"");
                else
                    msg += "\n[XML 卡片]";
            }

            #endregion

            #region json

            var jsons = chain.OfType<JsonMessage>().ToArray();
            var apps = chain.OfType<AppMessage>().ToArray();
            if (jsons.Any() || apps.Any())
            {
                var jsonText = jsons.Any() ? jsons.First().Json : apps.First().Content;
                var biliRegex =
                    new Regex(@"(https?:\\?/\\?/b23\.tv\\?/\w*)\??");
                var zhihuRegex =
                    new Regex(@"(https?:\\?/\\?/\w*\.?zhihu\.com\\?/[^?""=]*)\??");
                var jsonLinkRegex =
                    new Regex(@"{.*""app"":""com.tencent.structmsg"".*""jumpUrl"":""(https?:\\?/\\?/[^"",]*)"".*}");
                if (biliRegex.IsMatch(jsonText))
                {
                    var url = biliRegex.Match(jsonText).Groups[1].Value;
                    url = url.Replace("\\", "");
                    msg += "\n" + url;
                }
                else if (zhihuRegex.IsMatch(jsonText))
                {
                    var url = zhihuRegex.Match(jsonText).Groups[1].Value;
                    url = url.Replace("\\", "");
                    msg += "\n" + url;
                }
                else if (jsonLinkRegex.IsMatch(jsonText))
                {
                    var url = jsonLinkRegex.Match(jsonText).Groups[1].Value;
                    url = url.Replace("\\/", "/");
                    msg += "\n" + url;
                }
                else if (jsonText.Contains("com.tencent.gamecenter.gameshare_sgame"))
                    msg += "\n王者荣耀组队邀请";
                else if (jsons.Any())
                    msg += "\n[JSON 卡片消息]";
                else if (apps.Any())
                    msg += "\n[QQ 小程序]";
            }

            #endregion

            #region atall

            var atAll = false;
            if (msg.Contains("@全体成员") || chain.OfType<AtAllMessage>().Any())
            {
                atAll = true;
                msg = msg.Replace("@全体成员", "");
                msg = msg.Trim(' ', '\r', '\n', '\t');
                if (msg == "")
                    msg = "@全体成员";
            }

            #endregion

            msg = msg.Trim(' ', '\r', '\n', '\t');
            
            Message message;
            var photos = chain.OfType<ImageMessage>().ToArray();
            var voices = chain.OfType<VoiceMessage>().ToArray();

            #region photo

            if (photos.Any())
            {
                var purl = photos.First().Url;
                if (!string.IsNullOrWhiteSpace(msg))
                    msg = "\n" + msg;
                message = await C.TG.SendPhotoAsync(fwdinfo.tg,
                    purl,
                    from + ":" + msg,
                    replyToMessageId: replyId);
            }

            #endregion

            #region voice

            else if (voices.Any())
            {
                //voice
                var url = voices.First().Url;
                var path = "/home/clansty/silk/" + DateTime.Now.ToBinary();
                new WebClient().DownloadFile(url, path);
                var oggpath = Silk.decode(path);
                message = await C.TG.SendVoiceAsync(fwdinfo.tg, File.OpenRead(oggpath), from + ":",
                    replyToMessageId: replyId);
            }

            #endregion

            #region text or unknown

            else
            {
                if (string.IsNullOrWhiteSpace(msg))
                    msg = "未支持的消息";
                message = await C.TG.SendTextMessageAsync(fwdinfo.tg,
                    from + ":\n" + msg,
                    replyToMessageId: replyId);
            }

            #endregion

            var msgid = message.MessageId;
            Db.ldb.Put(((SourceMessage) chain.First()).Id.ToString(), msgid.ToString());
            if (atAll)
                C.TG.PinChatMessageAsync(message.Chat, msgid);
        }
    }
}