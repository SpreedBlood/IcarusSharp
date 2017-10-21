using DotNetty.Transport.Channels;
using IcarusSharp.Encryption;
using IcarusSharp.Messages.Types;
using IcarusSharp.Server.Api;
using IcarusSharp.Server.DotNetty.Codec;

namespace IcarusSharp.Server.DotNetty
{
    public class DotNettyPlayerNetwork : PlayerNetwork
    {
        private IChannel channel;

        public DotNettyPlayerNetwork(IChannel channel, int connectionId)
            : base(connectionId)
        {
            this.channel = channel;
        }

        public override void AddPipelineStage(object obj)
        {
            if (obj is RC4)
            {
                channel.Pipeline.AddBefore("gameDecoder", "gameCrypto", new EncryptionDecoder((RC4)obj));
            }
        }

        public override void Close()
        {
            channel.CloseAsync();
        }

        public override void Send(MessageComposer response)
        {
            channel.WriteAndFlushAsync(response);
        }

        public override void SendQueued(MessageComposer response)
        {
            channel.WriteAsync(response);
        }

        public override void Flush()
        {
            channel.Flush();
        }
    }
}
