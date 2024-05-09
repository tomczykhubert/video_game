using System.Runtime.Serialization;

namespace Infrastructure.ServiceFiles.Services
{
    [Serializable]
    internal class GameAlreadyExist : Exception
    {
        private int id;

        public GameAlreadyExist()
        {
        }

        public GameAlreadyExist(int id)
        {
            this.id = id;
        }

        public GameAlreadyExist(string? message) : base(message)
        {
        }

        public GameAlreadyExist(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected GameAlreadyExist(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}