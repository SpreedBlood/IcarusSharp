using IcarusSharp.Messages.Types;

namespace IcarusSharp.Server.Api
{
    public abstract class PlayerNetwork
    {
        private int connectionId;

        public PlayerNetwork(int connectionId)
        {
            this.connectionId = connectionId;
        }
        
        public abstract void Send(MessageComposer response);
        
        public abstract void SendQueued(MessageComposer response);
        
        public abstract void Close();
        
        public int GetConnectionId()
        {
            return connectionId;
        }
        
        public void SetConnectionId(int connectionId)
        {
            this.connectionId = connectionId;
        }
        
        public abstract void AddPipelineStage(object obj);

        public abstract void Flush();
    }
}
