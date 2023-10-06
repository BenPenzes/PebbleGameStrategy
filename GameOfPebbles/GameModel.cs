namespace GameOfPebblesLibrary
{
    public class GameModel
    {
        public Dimensions Dimensions; // the initial pile sizes
        public List<IPlayer> PlayerList { get; private set; } // list of players: can be human or computer controlled
        public List<Move> Moves { get; private set; } // all possible moves among all players
        public int MovesPerPlayer { get; set; } // defines the size of the slice in the Moves list that contains a particular player's moves
        public bool LastMoveWins { get; private set; } // determines if the zero-vector is "Y" or "N" if a player would move from this position
        public List<List<Tuple<int, string>>> TerminalStates { get; private set; } // usually it's only the zero vector for all players but extra terminal states can be set
        public Random RNG { get; set; }
        public GameEngine GameEngine { get; private set; }
        // GAME INFORMATION
        public List<int> VisitsPerState { get; set; }
        public int MaxVisitsPerState { get; set; }
        public int CurrentGameStateIndex { get; set; }
        public Dimensions CurrentGameDimensions { get; set; }
        public int CurrentPlayerIdx { get; set; }
        public GameModel(GameParameters gameParameters, string fillingAlgorithm)
        {
            if (gameParameters is null)
                throw new ArgumentNullException(nameof(gameParameters));
            // DIMENSIONS
            this.Dimensions = new(this, gameParameters.Dimensions);
            // PLAYERS
            this.PlayerList = gameParameters.PlayerList;
            // MOVES
            switch (gameParameters.MoveSetLabel.ToLower())
            {
                case "nim":
                    this.Moves = Move.CreateNimMoves(this);
                    this.MovesPerPlayer = Moves.Count / 2;
                    break;
                case "custom":
                    this.Moves = gameParameters.Moves != null ? gameParameters.Moves : new();
                    this.MovesPerPlayer = gameParameters.MovesPerPlayer;
                    break;
                default: // same as "custom"
                    this.Moves = gameParameters.Moves != null ? gameParameters.Moves : new();
                    break;
            }
            // TERMINAL STATES
            this.LastMoveWins = gameParameters.LastMoveWins;
            this.TerminalStates = gameParameters.TerminalStates;
            // RANDOMNESS
            this.RNG = new();
            // GAME ENGINE
            this.GameEngine = new(this, gameParameters.GameStates, fillingAlgorithm);
            // GAME INFORMATION
            this.CurrentGameDimensions = new(this.Dimensions);
            this.CurrentGameStateIndex = this.Dimensions.MapDimensionsToIndex(this.Dimensions);
            this.CurrentPlayerIdx = 0;
            this.VisitsPerState = new();
            this.MaxVisitsPerState = 5;
            for (int i = 0; i < this.GameEngine.Space; i++)
            {
                this.VisitsPerState.Add(0);
            }
            // CONNECTING THE GAME TO DIFFERENT OBJECTS
            foreach(var player in this.PlayerList)
            {
                player.Game = this;
            }
            foreach(var move in this.Moves)
            {
                move.Game = this;
            }
            if(gameParameters.GameStates != null) // when the gamestates are read from json, their Dimensions objects are not connected to any game!
            {
                foreach(var gameState in this.GameEngine.GameStates) 
                { 
                    gameState.Dimensions.Game = this;
                }
            }
        }
        public override string? ToString()
        {
            string str = "";
            str += $"Pile Sizes = ";
            str += this.Dimensions;
            str += $"\n\nPlayers = ";
            for (int playerIdx = 0; playerIdx < this.PlayerList.Count; playerIdx++)
                str += this.PlayerList[playerIdx];
            str += $"\nMoves:\n";
            for (int moveIdx = 0; moveIdx < this.Moves.Count; moveIdx++)
                str += this.Moves[moveIdx];
            str += $"Moves Per Player = {this.MovesPerPlayer}";
            str += this.GameEngine;
            str += $"Terminal States\n";
            for (int playerIdx = 0; playerIdx < this.TerminalStates.Count; playerIdx++)
            {
                str += $"Terminal States for Player #{playerIdx}: ";
                for (int stateIdx = 0; stateIdx < this.TerminalStates[playerIdx].Count; stateIdx++)
                {
                    str += $"({this.TerminalStates[playerIdx][stateIdx].Item1}, {this.TerminalStates[playerIdx][stateIdx].Item2})";
                }
                str += "\n";
            }
            return str;
        }
        public int GetNextPlayerIdx()
        {
            int numOfPlayers = this.GameEngine.NumOfPlayers;
            int nextPlayerIdx;
            nextPlayerIdx = (this.CurrentPlayerIdx + 1) % numOfPlayers;
            return nextPlayerIdx;
        }
        public void UpdateGameModelInformation() // called after a Move is executed
        {
            this.VisitsPerState[CurrentGameStateIndex]++;
            this.CurrentGameStateIndex = this.Dimensions.MapDimensionsToIndex(this.CurrentGameDimensions);
            this.CurrentPlayerIdx = GetNextPlayerIdx();
        }
        public bool GameStateIsTerminal()
        {
            if(this.GameEngine.GameStates[this.CurrentGameStateIndex].Terminal)
                return true;
            return false;
        }
        public bool CheckGameOver()
        {
            if(GameStateIsTerminal() ||
                this.VisitsPerState[this.CurrentGameStateIndex] == this.MaxVisitsPerState || 
                GetValidMovesOfPlayer(this.CurrentPlayerIdx).Count == 0)
                return true;
            return false;
        }
        public List<Move> GetAllMovesOfPlayer(int playerID)
        {
            List<Move> result = new();
            int wherePlayerMovesStart = playerID * this.GameEngine.MovesPerPlayer;
            int wherePlayerMovesEnd = (playerID + 1) * this.GameEngine.MovesPerPlayer;
            for (int moveIdx = wherePlayerMovesStart; moveIdx < wherePlayerMovesEnd; moveIdx++)
                result.Add(this.Moves[moveIdx]);
            return result;
        }
        public List<Move> GetValidMovesOfPlayer(int playerID)
        {
            List<Move> result = new();
            int wherePlayerMovesStart = playerID * this.GameEngine.MovesPerPlayer;
            int wherePlayerMovesEnd = (playerID + 1) * this.GameEngine.MovesPerPlayer;
            for (int moveIdx = wherePlayerMovesStart; moveIdx < wherePlayerMovesEnd; moveIdx++)
            {
                try
                {
                    this.Moves[moveIdx].ApplyModifiers(new(this.CurrentGameDimensions), false);
                    result.Add(this.Moves[moveIdx]);
                }
                catch (Exception) { }
            }
            return result;
        }
    }
}
