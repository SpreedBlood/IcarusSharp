using DotNetty.Transport.Channels;
using IcarusSharp.Server.DotNetty.Codec;
using IcarusSharp.Server.DotNetty.Connections;

namespace IcarusSharp.Server.DotNetty
{
    public class DotNettyChannelInitializer : ChannelInitializer<IChannel>
    {
        private readonly DotNettyServer server;

        public DotNettyChannelInitializer(DotNettyServer server)
        {
            this.server = server;
        }

        protected override void InitChannel(IChannel channel)
        {
            IChannelPipeline pipeline = channel.Pipeline;
            pipeline.AddLast("gameEncoder", new NetworkEncoder());
            pipeline.AddLast("gameDecoder", new NetworkDecoder());
            pipeline.AddLast("handler", new ConnectionHandler(server));
        }
    }
}
