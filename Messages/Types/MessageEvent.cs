using IcarusSharp.Game.Player;
using IcarusSharp.Server.Api.Messages;

namespace IcarusSharp.Messages.Types
{
    public interface MessageEvent
    {
        void Handle(Player player, IClientMessage reader);
    }
}
