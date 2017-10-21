using IcarusSharp.Messages;
using IcarusSharp.Server.Api;
using IcarusSharp.Server.DotNetty;

namespace IcarusSharp
{
    class Icarus
    {
        private static ServerHandler handler;

        static void Main(string[] args)
        {
            MessageHandler.Load();


            handler = new DotNettyServer("127.0.0.1", 30000);
            handler.CreateSocket();
            handler.Bind();

            while (true) { }
        }
    }
}
