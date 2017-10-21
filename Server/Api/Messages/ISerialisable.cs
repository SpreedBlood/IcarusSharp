namespace IcarusSharp.Server.Api.Messages
{
    public interface ISerialisable
    {
        void Compose(IResponse response);
    }
}
