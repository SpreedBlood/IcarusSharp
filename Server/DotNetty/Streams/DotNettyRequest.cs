using DotNetty.Buffers;
using IcarusSharp.Server.Api.Messages;
using System.Text;

namespace IcarusSharp.Server.DotNetty.Streams
{
    public class DotNettyRequest : IClientMessage
    {
        private readonly short header;
        private readonly int length;
        private readonly IByteBuffer buf;

        public DotNettyRequest(int length, IByteBuffer buffer)
        {
            buf = buffer;
            header = buffer.ReadShort();
            this.length = length;
        }

        public int GetLength()
        {
            return length;
        }

        public string GetMessageBody()
        {
            string consoleText = buf.ToString(Encoding.Default);

            for (int i = 0; i < 13; i++)
            {
                consoleText = consoleText.Replace(char.ToString((char)i), "[" + i + "]");
            }

            return consoleText;
        }

        public short GetMessageId()
        {
            return header;
        }

        public bool ReadBoolean()
        {
            return buf.ReadByte() == 1;
        }

        public byte[] ReadBytes(int len)
        {
            return buf.ReadBytes(len).ToArray();
        }

        public int ReadInt()
        {
            try
            {
                return buf.ReadInt();
            }
            catch
            {
                return 0;
            }
        }

        public bool ReadIntAsBool()
        {
            return buf.ReadInt() == 1;
        }

        public string ReadString()
        {
            int length = buf.ReadShort();
            byte[] data = buf.ReadBytes(length).ToArray();
            return Encoding.Default.GetString(data);
        }
    }
}
