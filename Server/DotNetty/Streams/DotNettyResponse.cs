using DotNetty.Buffers;
using IcarusSharp.Server.Api.Messages;
using System;
using System.Text;

namespace IcarusSharp.Server.DotNetty.Streams
{
    public class DotNettyResponse : IResponse
    {
        private short id;
        private IByteBuffer buffer;

        public DotNettyResponse(short header, IByteBuffer buffer)
        {
            id = header;
            this.buffer = buffer;
            this.buffer.WriteInt(-1);
            this.buffer.WriteShort(id);
        }

        public string GetBodyString()
        {
            String str = buffer.ToString(Encoding.Default);

            for (int i = 0; i < 14; i++)
            {
                str = str.Replace(char.ToString((char)i), "[" + i + "]");
            }

            return str;
        }

        public short GetHeader()
        {
            return id;
        }

        public void WriteBool(bool obj)
        {
            buffer.WriteBoolean(obj);
        }

        public void WriteInt(int obj)
        {
            buffer.WriteInt(obj);
        }

        public void WriteInt(bool obj)
        {
            buffer.WriteInt(obj ? 1 : 0);
        }

        public void WriteObject(ISerialisable serialise)
        {
            serialise.Compose(this);
        }

        public void WriteShort(short obj)
        {
            buffer.WriteShort(obj);
        }

        public void WriteString(object obj)
        {
            buffer.WriteShort(obj.ToString().Length);
            buffer.WriteBytes(Encoding.Default.GetBytes(obj.ToString()));
        }

        public bool HasLength()
        {
            return (buffer.GetInt(0) > -1);
        }
    }
}
