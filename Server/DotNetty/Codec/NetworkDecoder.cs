using DotNetty.Codecs;
using System;
using System.Collections.Generic;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using IcarusSharp.Server.DotNetty.Streams;

namespace IcarusSharp.Server.DotNetty.Codec
{
    public class NetworkDecoder : ByteToMessageDecoder
    {
        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            input.MarkReaderIndex();

            if (input.ReadableBytes < 6) return;

            byte delimeter = input.ReadByte();
            input.ResetReaderIndex();

            if (delimeter == 60)
            {
                string policy = "<?xml version=\"1.0\"?>\r\n"
        + "<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">\r\n"
        + "<cross-domain-policy>\r\n"
        + "<allow-access-from domain=\"*\" to-ports=\"*\" />\r\n"
        + "</cross-domain-policy>\0)";

                context.Channel.WriteAndFlushAsync(Unpooled.CopiedBuffer(Encoding.Default.GetBytes(policy)));
            }
            else
            {
                input.MarkReaderIndex();
                int length = input.ReadInt();

                if (input.ReadableBytes < length)
                {
                    input.ResetReaderIndex();
                    return;
                }

                if (length < 0) return;

                DotNettyRequest request = new DotNettyRequest(length, input.ReadBytes(length));
                output.Add(request);
            }
        }
    }
}
