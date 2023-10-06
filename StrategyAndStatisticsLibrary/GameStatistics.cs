using GameOfPebblesLibrary;

namespace StrategyAndStatisticsLibrary
{
    public class GameStatistics
    {
        public GameEngine GameEngine { get; private set; }
        public List<int> NumOfYStates { get; set; }
        public List<int> NumOfNStates { get; set; }
        public List<int>? NumOfDStates { get; set; }
        public List<List<int>> IndiciesOfYStates { get; set; }
        public List<List<int>> IndiciesOfNStates { get; set; }
        public List<List<int>>? IndiciesOfDStates { get; set; }
        public List< // Indicies coincide with the move indicies
            Tuple<
                List<int>, // For states that lead to "Y" states
                List<int>, // For states that lead to "N" states
                List<int>  // For states that lead to "D" states
                >> MoveResultLists { get; set; }
        public GameStatistics(GameEngine gameEngine, bool optimalForwardFiller)
        {
            this.GameEngine = gameEngine;
            int numOfPlayers = this.GameEngine.Dimensions.Values[^1] + 1;
            int statesPerPlayer = this.GameEngine.Space / numOfPlayers;
            this.NumOfYStates = new();
            this.NumOfNStates = new();
            this.NumOfDStates = new();
            this.IndiciesOfYStates = new();
            this.IndiciesOfNStates = new();
            this.IndiciesOfDStates = new();
            this.MoveResultLists = new();
            // CHECKING ALL THE GAMESTATES FOR "Y", "N", AND "D" VALUES
            for (int playerIdx = 0; playerIdx < numOfPlayers; playerIdx++)
            {
                this.IndiciesOfYStates.Add(new());
                this.IndiciesOfNStates.Add(new());
                this.IndiciesOfDStates.Add(new());
                for (int stateIdx = 0; stateIdx < statesPerPlayer; stateIdx++)
                {
                    if (this.GameEngine.GameStates[stateIdx].MyState.Equals(GameState.State.WINNNING))    
                        this.IndiciesOfYStates[playerIdx].Add(stateIdx);
                    if (this.GameEngine.GameStates[stateIdx].MyState.Equals(GameState.State.LOSING))
                        this.IndiciesOfNStates[playerIdx].Add(stateIdx);
                    if (this.GameEngine.GameStates[stateIdx].MyState.Equals(GameState.State.UNDECIDED))
                        this.IndiciesOfDStates[playerIdx].Add(stateIdx);
                }
                this.NumOfYStates.Add(this.IndiciesOfYStates[playerIdx].Count);
                this.NumOfNStates.Add(this.IndiciesOfNStates[playerIdx].Count);
                this.NumOfDStates.Add(this.IndiciesOfDStates[playerIdx].Count);
            }
            // CHECKING HOW GOOD MOVES ARE AND CHECKING HOW MANY "Y"-S, "N"-S and "D"-S WE CAN STEP INTO FROM THE STATE
            CheckAllStatesAndMoves(optimalForwardFiller);
        }
        public void CheckAllStatesAndMoves(bool optimalForwardFiller)
        {
            for (int playerIdx = 0; playerIdx < this.GameEngine.NumOfPlayers; playerIdx++)
            {
                for (int moveIdx = playerIdx * this.GameEngine.MovesPerPlayer; moveIdx < (playerIdx + 1) * this.GameEngine.MovesPerPlayer; moveIdx++)
                {
                    this.MoveResultLists.Add(new(new(), new(), new()));
                    for (int stateIdx = playerIdx * this.GameEngine.StatesPerPlayer; stateIdx < (playerIdx + 1) * this.GameEngine.StatesPerPlayer; stateIdx++)
                    {
                        try
                        {
                            Dimensions stepIntoDimensions = this.GameEngine.Moves[moveIdx].ApplyModifiers(new(this.GameEngine.GameStates[stateIdx].Dimensions), false);
                            int stepIntoIdx = this.GameEngine.Dimensions.MapDimensionsToIndex(stepIntoDimensions);
                            GameState stepIntoState = this.GameEngine.GameStates[stepIntoIdx];
                            if (stepIntoState.MyState == GameState.State.WINNNING)
                            {
                                if (!optimalForwardFiller)
                                    this.GameEngine.GameStates[stateIdx].NumWinningStates++;
                                this.MoveResultLists[moveIdx].Item1.Add(stateIdx);
                            }
                            if (stepIntoState.MyState == GameState.State.LOSING)
                            {
                                this.GameEngine.GameStates[stateIdx].NumLosingStates++;
                                this.MoveResultLists[moveIdx].Item2.Add(stateIdx);
                            }

                            if (stepIntoState.MyState == GameState.State.UNDECIDED)
                            {
                                this.GameEngine.GameStates[stateIdx].NumUndecidedStates++;
                                this.MoveResultLists[moveIdx].Item3.Add(stateIdx);
                            }
                        }
                        catch (Exception)
                        {
                            if (!optimalForwardFiller)
                                this.GameEngine.GameStates[stateIdx].NumWinningStates++;
                            this.MoveResultLists[moveIdx].Item1.Add(stateIdx);
                        }
                    }
                }
            }
        }
        public int GetNumOfYStates(int stateIdx)
        {
            return this.GameEngine.GameStates[stateIdx].NumWinningStates;
        }
        public int GetNumOfNStates(int stateIdx)
        {
            return this.GameEngine.GameStates[stateIdx].NumLosingStates;
        }
        public int GetNumOfDStates(int stateIdx)
        {
            return this.GameEngine.GameStates[stateIdx].NumUndecidedStates;
        }
        public override string ToString()
        {
            string str = "\nGame Statistics:\n";
            int numOfPlayers = GameEngine.NumOfPlayers;
            for (int playerIdx = 0; playerIdx < numOfPlayers; playerIdx++)
            {
                str += $"\nPlayer #{playerIdx}'s table\n";
                str += "ALL GAME STATES\n";
                str += $"\t\tNumber of \"Y\" states in the table: {NumOfYStates[playerIdx]}\n";
                str += $"\t\tNumber of \"N\" states in the table: {NumOfNStates[playerIdx]}\n";
                str += $"\t\tNumber of \"D\" states in the table: {NumOfDStates[playerIdx]}\n";
                str += "\nMOVES\n";
                for (int moveIdx = 0; moveIdx < MoveResultLists.Count; moveIdx++)
                {
                    str += $"\tMove #{moveIdx}: {GameEngine.Moves[moveIdx]}\n";
                    str += $"\t\tNumber of \"Y\" states this move leads to: {MoveResultLists[moveIdx].Item1.Count}\n";
                    str += $"\t\tNumber of \"N\" states this move leads to: {MoveResultLists[moveIdx].Item2.Count}\n";
                    str += $"\t\tNumber of \"D\" states this move leads to: {MoveResultLists[moveIdx].Item3.Count}\n";
                }
            }
            return str;
        }
    }
}
