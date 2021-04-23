using System.Linq;
using Mirai_CSharp;

namespace Clansty.tianlang
{
    public static class Q2TgMap
    {
        public static readonly FwdInfo[] infos =
        {
            //TODO: 这里填写各个群的对应关系
            new FwdInfo
            {
                gin = 0,
                tg = -0,
            },
        };

        internal static FwdInfo Q2Tg(MiraiHttpSession uin, long gin)
        {
            return infos.FirstOrDefault(i => i.host == uin && i.gin == gin);
        }

        internal static FwdInfo Tg2Q(long tguid)
        {
            return infos.FirstOrDefault(i => i.tg == tguid);
        }
    }
}