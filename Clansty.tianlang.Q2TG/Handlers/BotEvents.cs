using System;
using System.Threading.Tasks;
using Mirai_CSharp;
using Mirai_CSharp.Extensions;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;

namespace Clansty.tianlang
{
    class BotEvents : IFriendMessage, ITempMessage, IGroupMessage, INewFriendApply, IGroupApply,
        IBotInvitedJoinGroup, IDisconnected, IGroupMemberJoined
    {
        /// <summary>
        /// This is your bot's QQ ID
        /// </summary>
        internal const long SELF = 12345678;//TODO
        public async Task<bool> TempMessage(MiraiHttpSession session, ITempMessageEventArgs e)
        {
            return true;
        }

        public async Task<bool> FriendMessage(MiraiHttpSession session, IFriendMessageEventArgs e)
        {
            return true;
        }

        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            Q2Tg.NewGroupMsg(
                session,
                e.Sender.Group.Id,
                e.Sender.Id,
                e.Sender.Name,
                e.Chain
            );

            return true;
        }

        public async Task<bool> NewFriendApply(MiraiHttpSession session, INewFriendApplyEventArgs e)
        {
            return true;
        }

        public async Task<bool> GroupMemberJoined(MiraiHttpSession session, IGroupMemberJoinedEventArgs e)
        {
            return true;
        }

        public async Task<bool> GroupApply(MiraiHttpSession session, IGroupApplyEventArgs e)
        {
            return true;
        }

        public async Task<bool> BotInvitedJoinGroup(MiraiHttpSession session, IBotInvitedJoinGroupEventArgs e)
        {
            return false;
        }

        public async Task<bool> Disconnected(MiraiHttpSession session, IDisconnectedEventArgs e)
        {
            while (true)
            {
                try
                {
                    await session.ConnectAsync(C.miraiSessionOpinions, SELF);
                    return true;
                }
                catch (Exception)
                {
                    await Task.Delay(1000);
                }
            }
        }
    }
}