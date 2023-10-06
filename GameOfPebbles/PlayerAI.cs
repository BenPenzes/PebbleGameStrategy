using System.Runtime.Serialization;
using static GameOfPebblesLibrary.PlayerAI;

namespace GameOfPebblesLibrary { 
    [Serializable]
    public class PlayerAI : IPlayer, ISerializable
    {
        public int PlayerID { get; set; }
        public string PlayerType => "ai";
        public GameModel? Game { get; set; }
        public enum Intelligence
        {
            LOW, MID, HIGH
        }
        public Intelligence MyIntelligence { get; set; }
        public PlayerAI(int playerID, GameModel? game, Intelligence intelligence)
        {
            this.PlayerID = playerID;
            this.Game = game;
            this.MyIntelligence = intelligence;
        }
        public PlayerAI(SerializationInfo info, StreamingContext context, GameModel game)
        {
            this.PlayerID = (int)info.GetValue("playerID", typeof(int));
            this.Game = game;
            this.MyIntelligence = (Intelligence)info.GetValue("intelligence", typeof(Intelligence));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("playerID", this.PlayerID);
            info.AddValue("playerType", "ai");
            info.AddValue("intelligence", this.MyIntelligence);
        }
        public override string ToString()
        {
            string str = "";
            str += $"Player #{this.PlayerID + 1} - Type: AI\n";
            return str;
        }
        public Move GetMove(List<Move> moves, out int thinkingTime)
        {
            int undecideMoveIdx = -1;
            double threshold = 0.0;
            double randDouble = this.Game.RNG.NextDouble();
            Move move;
            thinkingTime = Think();
            switch (this.MyIntelligence)
            {
                case Intelligence.LOW:
                    threshold = 0.4;
                    break;
                case Intelligence.MID:
                    threshold = 0.7;
                    break;
                case Intelligence.HIGH:
                    threshold = 0.9;
                    break;
            }
            if(randDouble <= threshold) // the AI chooses the move optimally
            {
                Dimensions stateDimensions = new(this.Game.GameEngine.GameStates[this.Game.CurrentGameStateIndex].Dimensions);
                for (int moveIdx = 0; moveIdx < moves.Count; moveIdx++)
                {
                    Dimensions stepIntoDimensions = new(stateDimensions);
                    moves[moveIdx].ApplyModifiers(stepIntoDimensions, false);
                    // ApplyModifiers() here will not throw an invalid exception for all moves if the state is a terminal state
                    // the AI always chooses the first winning move
                    // if the moves are only decreasing moves and they are ordered by ascending absolute value, then no invalid moves will be chosen
                    if (this.Game.GameEngine.GameStates[this.Game.Dimensions.MapDimensionsToIndex(stepIntoDimensions)].MyState == GameState.State.LOSING)
                    {
                        move = moves[moveIdx];
                        return move; // always chooses the first winning move
                    }
                    if (this.Game.GameEngine.GameStates[this.Game.Dimensions.MapDimensionsToIndex(stepIntoDimensions)].MyState == GameState.State.UNDECIDED)
                    {
                        undecideMoveIdx = moveIdx; // always chooses the last move that steps to an undecided state
                    }
                }
                if(undecideMoveIdx != -1) // there is at least one undecied state so the AI chooses it
                {
                    move = moves[undecideMoveIdx];
                    return move;
                }
                else // the AI cannot step to winning or undecided states so it just chooses a random move
                {
                    move = moves[this.Game.RNG.Next(0, moves.Count)];
                    return move;
                }
            } 
            else // the AI is a dummy so it chooses a random move
            {
                move = moves[this.Game.RNG.Next(0, moves.Count)];
                return move;
                
            }
        }
        public void Move(Move move)
        {
            move.Execute();
        }
        private int Think()
        {
            int secondsToSleep = this.Game.RNG.Next(1, 4);
            Thread.Sleep(secondsToSleep * 1000);
            return secondsToSleep;
        }
    }
}
