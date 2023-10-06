namespace GameOfPebblesLibrary
{
    public class GameParameters
    {
        public List<int> Dimensions { get; private set; } // the initial pile sizes
        public List<IPlayer> PlayerList { get; private set; } // list of players: can be human or computer controlled
        public string MoveSetLabel { get; private set; } // define if there is a custom moveset or the game is a nim game
        public List<Move>? Moves { get; set; } // all possible moves among all players
        public bool Symetric { get; set; } // player can have different movesets (asymetric) or the same moveset (symetric)
        public int MovesPerPlayer { get; internal set; } // defines the size of the slice in the Moves list that contains a particular player's moves
        public bool LastMoveWins { get; private set; } // determines if the zero-vector is "Y" or "N" if a player would move from this position
        public List<List<Tuple<int, string>>> TerminalStates { get; set; } // usually it's only the zero vector for all players but extra terminal states can be set
        public List<GameState>? GameStates { get; set; } // all the possible game states
        public GameParameters(
            List<int> dimensions,
            List<IPlayer> playerList,
            string moveSetLabel,
            List<Move>? moves,
            bool symetric,
            bool lastMoveWins,
            List<List<Tuple<int, string>>> terminalStates,
            List<GameState>? gameStates
            )
        {
            this.Dimensions = dimensions;
            this.PlayerList = playerList;
            this.MoveSetLabel = moveSetLabel;
            this.Moves = moves;
            if ( Moves != null && moveSetLabel == "custom")
                this.MovesPerPlayer = Moves.Count / 2;
            else
                this.MovesPerPlayer = 0;
            this.Symetric = symetric;
            this.LastMoveWins = lastMoveWins;
            this.TerminalStates = terminalStates;
            this.GameStates = gameStates;

        }
        public override string? ToString()
        {
            bool symetricGame = true;
            int statesPerPlayer, numOfPlayers = this.Dimensions[^1] + 1, movesPerPlayer = 0;
            if(this.Moves != null) 
                movesPerPlayer = this.Moves.Count / numOfPlayers;
            if (this.GameStates != null)
                statesPerPlayer = this.GameStates.Count / numOfPlayers; 
            else
            {
                int space = 1;
                for (int dimensionIdx = 0; dimensionIdx < this.Dimensions.Count; dimensionIdx++)
                    space *= (this.Dimensions[dimensionIdx] + 1);
                statesPerPlayer = space / numOfPlayers;
            }
            string str = "";
            str += $"Pile Size(s) = (";
            for (int i = 0; i < this.Dimensions.Count - 1; i++)
            {
                str += this.Dimensions[i] + ", ";
            }     
            str = str[..^2];
            str += ")\n";
            if (this.PlayerList[0].PlayerType == "human" && this.PlayerList[1].PlayerType == "human")
            {
                str += "PvP GameModel\n";
            }
            if (this.PlayerList[0].PlayerType == "human" && this.PlayerList[1].PlayerType == "ai" )
            {
                PlayerAI playerAI = this.PlayerList[1] is null ? new(0, null, PlayerAI.Intelligence.MID) : this.PlayerList[1] as PlayerAI;
                str += "PvE GameModel\n";
                str += "Player Goes First\n";
                str += $"AI Intelligence Level: {playerAI.MyIntelligence}\n"; 
            }
            if (this.PlayerList[0].PlayerType == "ai" && this.PlayerList[1].PlayerType == "human")
            {
                PlayerAI playerAI = this.PlayerList[0] is null ? new(0, null, PlayerAI.Intelligence.MID) : this.PlayerList[0] as PlayerAI;
                str += "PvE GameModel\n";
                str += "AI Goes First\n";
                str += $"AI Intelligence Level: {playerAI.MyIntelligence}\n";
            }
            if (this.PlayerList[0].PlayerType == "ai" && this.PlayerList[1].PlayerType == "ai")
            {
                PlayerAI playerAI1 = this.PlayerList[0] is null ? new(0, null, PlayerAI.Intelligence.MID) : this.PlayerList[0] as PlayerAI;
                PlayerAI playerAI2 = this.PlayerList[1] is null ? new(0, null, PlayerAI.Intelligence.MID) : this.PlayerList[1] as PlayerAI;
                str += "EvE GameModel\n";
                str += $"AI #1 Intelligence Level: {playerAI1.MyIntelligence}\n";
                str += $"AI #2 Intelligence Level: {playerAI2.MyIntelligence}\n";
            }
            str += $"Move(s): " + this.MoveSetLabel;
            if(this.MoveSetLabel == "custom" && this.Moves != null)
            {
                for (int moveIdx = 0; moveIdx < movesPerPlayer; moveIdx++)
                {
                    if (!(this.Moves[moveIdx].Modifiers.SequenceEqual(this.Moves[movesPerPlayer + moveIdx].Modifiers))) // this only works for 2 players
                    {
                        symetricGame = false;
                        break;
                    }
                }
                if (symetricGame)
                {
                    str += "\nSymetric Moveset\n";
                    for (int moveIdx = 0; moveIdx < movesPerPlayer; moveIdx++)
                        str += this.Moves[moveIdx] + "\n";
                } 
                else
                {
                    str += "\nAsymetric Moveset\n";
                    for (int moveIdx = 0; moveIdx < this.Moves.Count; moveIdx++)
                        str += Moves[moveIdx] + "\n";        
                }
            }
            str += $"Terminal States\n";
            for (int playerIdx = 0; playerIdx < this.TerminalStates.Count; playerIdx++)
            {
                str += $"Terminal State(s) for Player #{playerIdx + 1}: ";
                for (int stateIdx = 0; stateIdx < this.TerminalStates[playerIdx].Count; stateIdx++)
                {
                    str += $"(";
                    if (this.TerminalStates[playerIdx][stateIdx].Item1 >= statesPerPlayer)
                    {
                        str += this.TerminalStates[playerIdx][stateIdx].Item1 % (statesPerPlayer * playerIdx);
                    }
                    else
                    {
                        str += this.TerminalStates[playerIdx][stateIdx].Item1;
                    }
                    str += $", {this.TerminalStates[playerIdx][stateIdx].Item2})";
                    
                }
                str += "\n";
            }
            return str;
        } 
    }
}
