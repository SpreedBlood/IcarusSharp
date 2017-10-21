using IcarusSharp.Game.Player;
using IcarusSharp.Messages.Types;
using IcarusSharp.Server.Api.Messages;
using System.Collections.Generic;

namespace IcarusSharp.Messages
{
    public class MessageHandler
    {
        private static Dictionary<short, List<MessageEvent>> messages;

        public static void Load()
        {
            messages = new Dictionary<short, List<MessageEvent>>();
        }

        public static void HandleRequest(Player player, IClientMessage message)
        {
            if (messages.ContainsKey(message.GetMessageId()))
                System.Console.WriteLine("Handling known event");
            else
                System.Console.WriteLine("Handling unknown event " + message.GetMessageId());
            Invoke(player, message.GetMessageId(), message);
        }

        private static void Invoke(Player player, short messageId, IClientMessage message)
        {
            if (messages.TryGetValue(messageId, out List<MessageEvent> events))
            {
                foreach (MessageEvent msgEvent in events)
                    msgEvent.Handle(player, message);
            }
        }
    }
}