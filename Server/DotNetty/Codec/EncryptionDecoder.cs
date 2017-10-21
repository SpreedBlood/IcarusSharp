using DotNetty.Codecs;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using IcarusSharp.Encryption;
using System.Collections.Generic;

namespace IcarusSharp.Server.DotNetty.Codec
{
    public class EncryptionDecoder : ByteToMessageDecoder
    {
        private RC4 rc4;

        public EncryptionDecoder(RC4 rc4)
        {
            this.rc4 = rc4;
        }

        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            IByteBuffer result = Unpooled.Buffer();

            if (input.ReadableBytes > 0)
            {
                result.WriteByte((byte)(input.ReadByte() ^ rc4.Next()));
            }
            output.Add(result);
        }
    }
}
