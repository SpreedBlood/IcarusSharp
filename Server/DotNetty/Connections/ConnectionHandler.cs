using DotNetty.Transport.Channels;
using IcarusSharp.Game.Player;
using IcarusSharp.Messages;
using IcarusSharp.Server.DotNetty.Streams;
using System;

namespace IcarusSharp.Server.DotNetty.Connections
{
    public class ConnectionHandler : SimpleChannelInboundHandler<DotNettyRequest>
    {
        private DotNettyServer server;

        public ConnectionHandler(DotNettyServer serv)
        {
            server = serv;
        }

        public override void ChannelRegistered(IChannelHandlerContext context)
        {
            Player player = new Player(new DotNettyPlayerNetwork(context.Channel, context.Channel.GetHashCode()));
            context.Channel.GetAttribute(Player.SESSION_KEY).Set(player);

            if (!server.Add(context.Channel))
            {
                context.DisconnectAsync();
                return;
            }

            base.ChannelRegistered(context);
        }

        public override void ChannelUnregistered(IChannelHandlerContext context)
        {
            server.Remove(context.Channel);
            Player player = context.Channel.GetAttribute(Player.SESSION_KEY).Get();

            //TODO: player.Dispose();
            base.ChannelUnregistered(context);
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, DotNettyRequest msg)
        {
            Player player = ctx.Channel.GetAttribute(Player.SESSION_KEY).Get();

            if (msg == null) return;

            MessageHandler.HandleRequest(player, msg);
        }
    }
}
