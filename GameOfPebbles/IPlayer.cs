using System.Runtime.Serialization;

namespace GameOfPebblesLibrary {
    public interface IPlayer : ISerializable
    {
        public int PlayerID { get; set; }
        public string PlayerType { get;  }
        public GameModel? Game { get; set; }
        public void Move(Move pMove);
    }
}
