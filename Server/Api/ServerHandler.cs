namespace IcarusSharp.Server.Api
{
    public abstract class ServerHandler
    {
        public int Port { get; }
        public string Ip { get; }

        public ServerHandler(string ip, int port)
        {
            this.Port = port;
            this.Ip = ip;
        }
        
        public abstract void CreateSocket();
        
        public abstract void Bind();
    }
}