using System.Runtime.Serialization;

namespace GameOfPebblesLibrary
{
    [Serializable]
    public class PlayerHuman : IPlayer, ISerializable
    {
        public int PlayerID { get; set; }
        public string PlayerType => "human";
        public GameModel? Game { get; set; }
        public PlayerHuman(int playerID, GameModel? game)
        {
            this.PlayerID = playerID;
            this.Game = game;
        }
        public PlayerHuman(SerializationInfo info, StreamingContext context, GameModel game)
        {
            this.PlayerID = (int)info.GetValue("playerID", typeof(int));
            this.Game = game;
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("playerID", this.PlayerID);
            info.AddValue("playerType", "human");
        }
        public override string ToString()
        {
            string str = "";
            str += $"Player #{this.PlayerID + 1} - Type: Human\n";
            return str;
        }
        public void Move(Move move)
        {
            move.Execute();
        }
    }
}
