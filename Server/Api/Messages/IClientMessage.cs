namespace IcarusSharp.Server.Api.Messages
{
    public interface IClientMessage
    {
        int ReadInt();

        bool ReadIntAsBool();

        bool ReadBoolean();

        string ReadString();

        byte[] ReadBytes(int len);

        string GetMessageBody();

        short GetMessageId();

        int GetLength();
    }
}
