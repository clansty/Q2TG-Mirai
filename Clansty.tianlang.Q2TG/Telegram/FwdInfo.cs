using Mirai_CSharp;

namespace Clansty.tianlang
{
    public class FwdInfo
    {
        /// <summary>
        /// qq 号
        /// </summary>
        public MiraiHttpSession host = C.QQ;

        /// <summary>
        /// 群号
        /// </summary>
        public long gin;

        /// <summary>
        /// tguid
        /// </summary>
        public long tg;

        /// <summary>
        /// TG 到 QQ 的消息里面要不要包含发送者的名片
        /// </summary>
        public bool includeSender = true;
    }
}