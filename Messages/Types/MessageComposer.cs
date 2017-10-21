using DotNetty.Buffers;
using IcarusSharp.Server.Api.Messages;

namespace IcarusSharp.Messages.Types
{
    public abstract class MessageComposer
    {
        public abstract void Compose(IResponse response);

        public IResponse WriteToBuffer(IByteBuffer buf)
        {

            return null;
        }

        public abstract short GetHeader();
    }
}