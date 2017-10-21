using DotNetty.Common.Utilities;
using IcarusSharp.Server.Api;

namespace IcarusSharp.Game.Player
{
    public class Player
    {
        public static readonly AttributeKey<Player> SESSION_KEY = AttributeKey<Player>.ValueOf("Player");

        public Player(PlayerNetwork network)
        {

        }
    }
}
