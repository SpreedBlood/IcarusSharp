using System.Collections.Generic;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using IcarusSharp.Messages.Types;
using DotNetty.Buffers;
using IcarusSharp.Server.DotNetty.Streams;

namespace IcarusSharp.Server.DotNetty.Codec
{
    public class NetworkEncoder : MessageToMessageEncoder<MessageComposer>
    {
        protected override void Encode(IChannelHandlerContext context, MessageComposer message, List<object> output)
        {
            IByteBuffer buffer = Unpooled.Buffer(6);
            DotNettyResponse response = new DotNettyResponse(message.GetHeader(), buffer);
            message.Compose(response);

            if (!response.HasLength())
            {
                buffer.SetInt(0, buffer.WriterIndex - 4);
            }

            output.Add(buffer);
        }
    }
}
