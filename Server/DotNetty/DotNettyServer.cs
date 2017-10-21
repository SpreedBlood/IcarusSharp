using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using IcarusSharp.Server.Api;
using System.Collections.Generic;

namespace IcarusSharp.Server.DotNetty
{
    public class DotNettyServer : ServerHandler
    {
        private readonly static int BACK_LOG = 20;
        private readonly static int BUFFER_SIZE = 2048;
        
        private ServerBootstrap bootstrap;
        private Dictionary<IChannelId, IChannel> channels;

        public DotNettyServer(string ip, int port)
            : base(ip, port)
        {
            bootstrap = new ServerBootstrap();
            channels = new Dictionary<IChannelId, IChannel>();
        }

        public override void CreateSocket()
        {
            IEventLoopGroup bossGroup = new MultithreadEventLoopGroup(6);
            IEventLoopGroup workerGroup = new MultithreadEventLoopGroup(1);
            bootstrap
                .Group(bossGroup, workerGroup)
                .Channel<TcpServerSocketChannel>()
                .ChildHandler(new DotNettyChannelInitializer(this))
                .Option(ChannelOption.SoBacklog, BACK_LOG)
                .ChildOption(ChannelOption.TcpNodelay, true)
                .ChildOption(ChannelOption.SoKeepalive, true)
                .ChildOption(ChannelOption.SoRcvbuf, BUFFER_SIZE)
                .ChildOption(ChannelOption.RcvbufAllocator, new FixedRecvByteBufAllocator(BUFFER_SIZE))
                .ChildOption(ChannelOption.Allocator, UnpooledByteBufferAllocator.Default);
            System.Console.WriteLine("Created socket!");
        }

        public override void Bind()
        {
            bootstrap.BindAsync(Port);
            System.Console.WriteLine("Listening on port: " + Port);
        }

        public bool Add(IChannel channel)
        {
            if (channels.TryAdd(channel.Id, channel))
            {
                return true;
            }
            return false;
        }

        public void Remove(IChannel channel)
        {
            channels.Remove(channel.Id);
        }
    }
}
