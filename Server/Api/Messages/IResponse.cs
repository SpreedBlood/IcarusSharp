namespace IcarusSharp.Server.Api.Messages
{
    public interface IResponse
    {
        void WriteString(object obj);

        void WriteInt(int obj);

        void WriteInt(bool obj);

        void WriteShort(short obj);

        void WriteBool(bool obj);

        void WriteObject(ISerialisable serialise);

        string GetBodyString();

        short GetHeader();
    }
}
