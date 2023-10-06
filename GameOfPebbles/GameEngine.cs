namespace GameOfPebblesLibrary
{
    public class GameEngine
    {
        public GameModel GameModel { get; private set; } // the GameModel that holds the GameEngine
        public Dimensions Dimensions { get; private set; } // the initial pile sizes
        public Dimensions GameZeroVector { get; set; } // a zero vector that is the same length as the Dimensions vector of the GameModel
        public List<GameState> GameStates { get; private set; } // all the possible game states
        public List<Move> Moves { get; private set; } // all possible moves among all players
        public int MovesPerPlayer { get; private set; } // defines the size of the slice in the Moves list that contains a particular player's moves
        public int Space { get; private set; } // the size of the tensor of the gamespace
        public int NumOfPlayers { get; private set; } // how many players playing, always 2
        public int StatesPerPlayer { get; private set; } // what is the size of one player's table, always the same for all players
        public List<Tuple<int, int>>? WhereCertainMoveTypesStart { get; private set; } // used for optimal forward filler
        public bool LastMoveWins { get; private set; } // determines if the zero-vector is "Y" or "N" if a player would move from this position
        public List<List<Tuple<int, string>>> TerminalStates { get; private set; } // usually it's only the zero vector for all players but extra terminal states can be set
        public int NumStatesFilled { get; private set; } // the number of states that has a value of either "WINNING" or "LOSING"
        private Stack<int> LosingStateIndiciesForFilling { get; set; } // used for forward filling losing states
        private Stack<int> WinningStateIndiciesForFilling { get; set; } // used for forward filling winning states
        public GameEngine(GameModel GameModel, List<GameState>? gameStates, string fillingAlgorithm)
        {
            // GAME
            this.GameModel = GameModel;
            // DIMENSIONS
            this.Dimensions = GameModel.Dimensions;
            // MOVES
            this.Moves = GameModel.Moves;
            this.MovesPerPlayer = GameModel.MovesPerPlayer;
            this.WhereCertainMoveTypesStart = null;
            // TERMINAL STATES
            this.LastMoveWins = GameModel.LastMoveWins;
            this.TerminalStates = GameModel.TerminalStates;
            // GAME ENGINE INFORMATION
            this.Space = this.Dimensions.CalculateSpace();
            // Creating the Table
            this.GameStates = new List<GameState>()
            {
                Capacity = this.Space
            };
            // Initiating the Table
            this.GameZeroVector = Dimensions.CreateZeroVector(this.GameModel, this.Dimensions.Values.Count);
            Dimensions iterationDimensions = new(this.GameZeroVector);
            for (int i = 0; i < this.Space; i++)
            {
                this.GameStates.Add(new GameState(
                    new(this.GameModel, new(iterationDimensions.Values)),
                    GameState.State.UNDECIDED,
                    false
                    ));
                this.Dimensions.IncrementDimensions(iterationDimensions, this.GameZeroVector);
            }
            this.NumOfPlayers = this.Dimensions.Values[^1] + 1;
            this.StatesPerPlayer = this.GameStates.Count / this.NumOfPlayers;
            this.WinningStateIndiciesForFilling = new();
            this.LosingStateIndiciesForFilling = new();
            // Filling the Table 
            if(gameStates != null) // not null if the game states have already been calculated!
            {
                this.GameStates = gameStates;
            }
            else // fill the table with the chosen algorithm
            {
                switch (fillingAlgorithm)
                {
                    case "tabulation": // tabulation filler
                        InitializeTerminalGameStates(false, false);
                        TabulationFiller(null);
                        break;
                    case "etabulation": // extended tabulation filler
                        InitializeTerminalGameStates(false, false);
                        ExtendedTabulationFiller();
                        break;
                    case "forward": // forward filler
                        InitializeTerminalGameStates(false, true);
                        ForwardFiller();
                        break;
                    case "eforwardall": // extended forward filler
                        this.WhereCertainMoveTypesStart = FindWhereCertainMoveTypesStart();
                        InitializeTerminalGameStates(true, false); 
                        ExtendedForwardFiller(true);
                        break;
                    case "eforwardborder": // optimized forward filler
                        this.WhereCertainMoveTypesStart = FindWhereCertainMoveTypesStart();
                        InitializeTerminalGameStates(true, false); 
                        ExtendedForwardFiller(false);
                        break;
                    default:
                        ExtendedTabulationFiller(); // extended t. filler always fills the table but might be slow
                        break;
                }
            }
        }
        public override string ToString()
        {
            string str = $"\nGAME ENGINE\n";
            if (this.WhereCertainMoveTypesStart != null)
            {
                str += $"\nWhere Moves Start\n";
                for (int i = 0; i < this.Dimensions.Values[^1] + 1; i++)
                {
                    str += $"Player #{i}: ({this.WhereCertainMoveTypesStart[i].Item1}, {this.WhereCertainMoveTypesStart[i].Item2})\n";
                }
            }
            str += $"\nSize of the Tensor = {this.Space}";
            str += $"\nGameStates Capacity = {this.GameStates.Capacity}";
            str += $"\nGameState Count = {this.GameStates.Count}";
            return str;
        }
        private void InitializeTerminalGameStates(bool optimalForwardFiller, bool classicForwardFiller)
        {
            for (int playerIdx = 0; playerIdx < this.TerminalStates.Count; playerIdx++)
            {
                for (int termStatePairIdx = 0; termStatePairIdx < this.TerminalStates[playerIdx].Count; termStatePairIdx++)
                {
                    int stateIdx = this.TerminalStates[playerIdx][termStatePairIdx].Item1;
                    this.GameStates[stateIdx].Terminal = true;
                    if (this.TerminalStates[playerIdx][termStatePairIdx].Item2 == "Y")
                    {
                        this.GameStates[stateIdx].MyState = GameState.State.WINNNING;
                        if(optimalForwardFiller)
                            this.WinningStateIndiciesForFilling.Push(stateIdx);
                    }
                    else
                    {
                        this.GameStates[stateIdx].MyState = GameState.State.LOSING;
                        if (optimalForwardFiller)
                            this.LosingStateIndiciesForFilling.Push(stateIdx);
                        if (classicForwardFiller)
                            ForwardFillLosingState(stateIdx, false);
                    }
                }
            }
        }
        private bool CheckPreviousStates(int gameStateIdx)
        {
            int playerIdx = this.GameStates[gameStateIdx].Dimensions.Values[^1];
            bool hasOneUndecided = false;
            Dimensions stateDimensions = new(this.GameStates[gameStateIdx].Dimensions);
            for (int moveIdx = playerIdx * this.MovesPerPlayer; moveIdx < (playerIdx + 1) * this.MovesPerPlayer; moveIdx++)
            {
                Dimensions stepIntoDimensions = new(stateDimensions);
                try
                {
                    this.Moves[moveIdx].ApplyModifiers(stepIntoDimensions, false);
                    int stepIntoIndex = this.Dimensions.MapDimensionsToIndex(stepIntoDimensions);
                    if (this.GameStates[stepIntoIndex].MyState == GameState.State.LOSING)
                    {
                        this.GameStates[gameStateIdx].MyState = GameState.State.WINNNING;
                        this.NumStatesFilled++;
                        break;
                    }
                    if (this.GameStates[stepIntoIndex].MyState == GameState.State.UNDECIDED)
                        hasOneUndecided = true;
                }
                catch (Exception)
                { }
            }
            if (!hasOneUndecided && this.GameStates[gameStateIdx].MyState == GameState.State.UNDECIDED)
            {
                this.GameStates[gameStateIdx].MyState = GameState.State.LOSING;
                this.NumStatesFilled++;
            }
            return hasOneUndecided;
        }
        // places the number on dimensionIdx to the BEGINNING of the list
        private void SetCoordinateOrder_IndexFirst(List<int> coordinateOrder, int dimensionIdx) 
        {
            for (int i = 0; i < coordinateOrder.Count; i++)
            {
                coordinateOrder[i] = i;
            }
            for (int i = 0; i < dimensionIdx; i++)
            {
                coordinateOrder[i + 1] = i;
            }
            coordinateOrder[0] = dimensionIdx;
        }
        // places the number on dimensionIdx to the END of the list
        private void SetCoordinateOrder_IndexLast(List<int> coordinateOrder, int dimensionIdx)
        {
            for (int i = 0; i < coordinateOrder.Count; i++)
            {
                coordinateOrder[i] = i;
            }
            for (int i = coordinateOrder.Count - 1; i > dimensionIdx; i--)
            {
                coordinateOrder[i - 1] = i;
            }
            coordinateOrder[^1] = dimensionIdx;
        }
        private List<Tuple<int, int>>? FindWhereCertainMoveTypesStart()
        {
            List<Tuple<int, int>> whereCertainMoveTypesStart = new();
            for (int playerIdx = 0; playerIdx < this.NumOfPlayers; playerIdx++)
            {
                int whereMovesEndForPlayer = (playerIdx + 1) * this.MovesPerPlayer;
                int complexMovesIdx = whereMovesEndForPlayer;
                int increasingMovesIdx = whereMovesEndForPlayer;
                bool foundWhereComplexMovesStart = false;
                for (int moveIdx = playerIdx * this.MovesPerPlayer; moveIdx < whereMovesEndForPlayer; moveIdx++)
                {
                    List<int> sortedModifiers = new(this.Moves[moveIdx].Modifiers);
                    sortedModifiers.Sort();
                    if (sortedModifiers[0] >= 0)
                    {
                        increasingMovesIdx = moveIdx;
                        if (!foundWhereComplexMovesStart)
                            complexMovesIdx = moveIdx;
                        break;
                    }
                    if (!foundWhereComplexMovesStart)
                    {
                        int modifierIdx = 0;
                        while (this.Moves[moveIdx].Modifiers[modifierIdx] <= 0 && modifierIdx < this.Moves[moveIdx].Modifiers.Count - 1)
                            modifierIdx++;
                        if (modifierIdx != this.Moves[moveIdx].Modifiers.Count - 1)
                        {
                            complexMovesIdx = moveIdx;
                            foundWhereComplexMovesStart = true;
                        }
                    }
                }
                whereCertainMoveTypesStart.Add(new(complexMovesIdx, increasingMovesIdx));
            }
            return whereCertainMoveTypesStart;
        }
        private void ExtendedForwardFiller(bool checkAllStates)
        {
            if (checkAllStates)
                CheckAllStatesForInvalidMoves(); // this is the slower version
            else
                CheckBorderStatesForInvalidMoves(); // this is the faster version
            while (this.LosingStateIndiciesForFilling.Count > 0 || this.WinningStateIndiciesForFilling.Count > 0)
            {
                while (this.LosingStateIndiciesForFilling.Count > 0)
                {
                    int topState = this.LosingStateIndiciesForFilling.Pop();
                    ForwardFillLosingState(topState, true);
                }
                while (this.WinningStateIndiciesForFilling.Count > 0)
                {
                    int topState = this.WinningStateIndiciesForFilling.Pop();
                    ForwardFillWinningState(topState);
                }
            }
        }
        private void ForwardFillWinningState(int winningStateIdx)
        {
            Dimensions winningStateDimensions = new(this.GameStates[winningStateIdx].Dimensions);
            int previousPlayerIdx = winningStateDimensions.Values[^1] - 1;
            if (previousPlayerIdx < 0)
                previousPlayerIdx = this.GameModel.Dimensions.Values[^1];
            for (int moveIdx = previousPlayerIdx * this.MovesPerPlayer; moveIdx < (previousPlayerIdx + 1) * this.MovesPerPlayer; moveIdx++)
            {
                Dimensions previousStateDimensions = new(winningStateDimensions);
                try
                {
                    this.Moves[moveIdx].ApplyModifiers(previousStateDimensions, true);
                    int previousStateIdx = this.Dimensions.MapDimensionsToIndex(previousStateDimensions);
                    GameState previousState = this.GameStates[previousStateIdx];
                    previousState.NumWinningStates++;
                    if(previousState.NumWinningStates == this.MovesPerPlayer && previousState.MyState == GameState.State.UNDECIDED)
                    {
                        this.LosingStateIndiciesForFilling.Push(previousStateIdx);
                        previousState.MyState = GameState.State.LOSING;
                        this.NumStatesFilled++;
                    }
                }
                catch (Exception)
                {}
            }
        }
        private void ForwardFillLosingState(int losingStateIdx, bool optimalForwardFiller)
        {
            Dimensions losingStateDimensions = new(this.GameStates[losingStateIdx].Dimensions);
            int previousPlayerIdx = losingStateDimensions.Values[^1] - 1;
            if(previousPlayerIdx < 0) 
                previousPlayerIdx = this.NumOfPlayers - 1;
            for (int moveIdx = previousPlayerIdx * this.MovesPerPlayer; moveIdx < (previousPlayerIdx + 1) * this.MovesPerPlayer; moveIdx++)
            {
                Dimensions previousStateDimensions = new(losingStateDimensions);
                try
                {
                    this.Moves[moveIdx].ApplyModifiers(previousStateDimensions, true);
                    int previousStateIdx = this.Dimensions.MapDimensionsToIndex(previousStateDimensions);
                    GameState previousState = this.GameStates[previousStateIdx];
                    if (previousState.MyState == GameState.State.UNDECIDED)
                    {
                        if(optimalForwardFiller)
                            this.WinningStateIndiciesForFilling.Push(previousStateIdx);
                        previousState.MyState = GameState.State.WINNNING;
                        this.NumStatesFilled++;
                    }
                }
                catch (Exception)
                {}
            }
        }
        private void CheckBorderStatesForInvalidMoves()
        {
            for (int i = 0; i < this.Dimensions.Values[^1] + 1; i++)
            {
                CheckBorderStatesForInvalidMoves_Decreasing(i);
                CheckBorderStatesForInvalidMoves_IncreasingAndComplex(i, true);
                CheckBorderStatesForInvalidMoves_IncreasingAndComplex(i, false);
            }
        }
        private void CheckBorderStatesForInvalidMoves_Decreasing(int playerIdx)
        {
            int whereDecreasingMovesStart = playerIdx * this.MovesPerPlayer; 
            int whereDecreasingMovesEnd = this.WhereCertainMoveTypesStart[playerIdx].Item2;
            Dimensions startValuesVector = new(this.GameZeroVector);
            Dimensions borderVector = GetMaxModifiers(whereDecreasingMovesStart, whereDecreasingMovesEnd, true);
            startValuesVector.Values[^1] = playerIdx;
            borderVector.Values[^1] = playerIdx;
            Dimensions iterationDimensions;
            List<int> orderOfCoordinates = new();
            for(int coordinateIdx = 0; coordinateIdx < this.Dimensions.Values.Count - 1; coordinateIdx++)
            {
                orderOfCoordinates.Add(coordinateIdx);
            }
            for(int dimensionIdx = 0; dimensionIdx < this.Dimensions.Values.Count - 1; dimensionIdx++)
            {
                iterationDimensions = new(startValuesVector);
                SetCoordinateOrder_IndexLast(orderOfCoordinates, dimensionIdx);
                while (iterationDimensions.Values[dimensionIdx] < borderVector.Values[dimensionIdx])
                {
                    int stateIdx = this.Dimensions.MapDimensionsToIndex(iterationDimensions);
                    Dimensions stepIntoDimensions = new(iterationDimensions);
                    for(int moveIdx = whereDecreasingMovesStart; moveIdx < whereDecreasingMovesEnd; moveIdx++)
                    {
                        try
                        {
                            this.Moves[moveIdx].ApplyModifiers(stepIntoDimensions, false);
                        }
                        catch (Exception)
                        {
                            this.GameStates[stateIdx].NumWinningStates++;
                        }
                        stepIntoDimensions = new(iterationDimensions);
                    }
                    if(this.GameStates[stateIdx].NumWinningStates == this.MovesPerPlayer && this.GameStates[stateIdx].MyState == GameState.State.UNDECIDED)
                    {
                        this.GameStates[stateIdx].Terminal = true;
                        if (LastMoveWins)
                        {
                            this.LosingStateIndiciesForFilling.Push(stateIdx);
                            this.GameStates[stateIdx].MyState = GameState.State.LOSING;
                        }
                        else
                        {
                            this.WinningStateIndiciesForFilling.Push(stateIdx);
                            this.GameStates[stateIdx].MyState = GameState.State.WINNNING;
                        }
                        this.NumStatesFilled++;
                    }
                    this.Dimensions.IncrementDimensionsBasedOnIndicies(iterationDimensions, startValuesVector, orderOfCoordinates);
                }
                startValuesVector.Values[dimensionIdx] = iterationDimensions.Values[dimensionIdx];
            }
        }
        private void CheckBorderStatesForInvalidMoves_IncreasingAndComplex(int playerIdx, bool complexMoves) // complex moves are moves that have both increasing and decreasing modifiers
        {
            int whereMovesStart;
            int whereMovesEnd;
            Dimensions startValuesVector = new(this.Dimensions); // start from the max vector
            Dimensions endValuesVector;
            if (complexMoves)
            {
                whereMovesStart = this.WhereCertainMoveTypesStart[playerIdx].Item1;
                whereMovesEnd = this.WhereCertainMoveTypesStart[playerIdx].Item2;
                if (whereMovesStart == whereMovesEnd) // no complex moves in this game
                {
                    return;
                }
                endValuesVector = GetMaxModifiers(playerIdx * this.MovesPerPlayer, whereMovesEnd, true);
            }
            else
            {
                whereMovesStart = this.WhereCertainMoveTypesStart[playerIdx].Item2;
                whereMovesEnd = (playerIdx + 1) * this.MovesPerPlayer;
                if (whereMovesStart == whereMovesEnd) // no positive moves in this game
                {
                    return;
                }
                endValuesVector = new(this.GameZeroVector);
            }
            Dimensions borderVector = new();
            try
            {
                borderVector = new(this.GameModel, new(this.Dimensions.Values));
                borderVector.AddDimensions(GetMaxModifiers(whereMovesStart, whereMovesEnd, false).MulitplyDimensions(-1));
            }
            catch (Exception)
            { }
            startValuesVector.Values[^1] = playerIdx;
            endValuesVector.Values[^1] = playerIdx;
            borderVector.Values[^1] = playerIdx;
            Dimensions iterationDimensions;
            List<int> orderOfCoordinates = new();
            for (int i = 0; i < this.Dimensions.Values.Count - 1; i++)
            {
                orderOfCoordinates.Add(i);
            }
            for (int dimensionIdx = 0; dimensionIdx < this.Dimensions.Values.Count - 1; dimensionIdx++)
            {
                iterationDimensions = new(startValuesVector);
                SetCoordinateOrder_IndexLast(orderOfCoordinates, dimensionIdx);
                int threshold = endValuesVector.Values[dimensionIdx] > borderVector.Values[dimensionIdx] ? endValuesVector.Values[dimensionIdx] : borderVector.Values[dimensionIdx] + 1;
                while (iterationDimensions.Values[dimensionIdx] >= threshold)
                {
                    int stateIdx = this.Dimensions.MapDimensionsToIndex(iterationDimensions);
                    Dimensions stepIntoDimensions = new(iterationDimensions);
                    for (int moveIdx = whereMovesStart; moveIdx < whereMovesEnd; moveIdx++)
                    {
                        try
                        {
                            this.Moves[moveIdx].ApplyModifiers(stepIntoDimensions, false);
                        }
                        catch (Exception)
                        {
                            this.GameStates[stateIdx].NumWinningStates++;
                        }
                        stepIntoDimensions = new(this.GameModel, new(iterationDimensions.Values));
                    }
                    if (this.GameStates[stateIdx].NumWinningStates == this.MovesPerPlayer && this.GameStates[stateIdx].MyState == GameState.State.UNDECIDED)
                    {
                        this.GameStates[stateIdx].Terminal = true;
                        if (this.LastMoveWins)
                        {
                            this.LosingStateIndiciesForFilling.Push(stateIdx);
                            this.GameStates[stateIdx].MyState = GameState.State.LOSING;
                        }
                        else
                        {
                            this.WinningStateIndiciesForFilling.Push(stateIdx);
                            this.GameStates[stateIdx].MyState = GameState.State.WINNNING;
                        }
                        this.NumStatesFilled++;
                    }
                    this.Dimensions.DecrementDimensionsBasedOnIndicies(iterationDimensions, startValuesVector, endValuesVector, orderOfCoordinates);
                }
                startValuesVector.Values[dimensionIdx] = iterationDimensions.Values[dimensionIdx];
                if(startValuesVector.Values[dimensionIdx] < endValuesVector.Values[dimensionIdx])
                {
                    break;
                }
            }
        }
        private Dimensions GetMaxModifiers(int startIdx, int endIdx, bool decreasingMoves)
        {
            Dimensions maxModifiersVector = new();
            int maxModifierMoveIdx = 0;
            for (int coordinateIdx = 0; coordinateIdx < this.Dimensions.Values.Count; coordinateIdx++)
            {
                for (int moveIdx = startIdx; moveIdx < endIdx; moveIdx++)
                {
                    bool comp = decreasingMoves ? this.Moves[moveIdx].Modifiers[coordinateIdx] < this.Moves[maxModifierMoveIdx].Modifiers[coordinateIdx] :
                        this.Moves[moveIdx].Modifiers[coordinateIdx] > this.Moves[maxModifierMoveIdx].Modifiers[coordinateIdx];
                    if (comp) // Larger in absolute value
                        maxModifierMoveIdx = moveIdx;
                    
                }
                if (decreasingMoves)
                {
                    if (this.Moves[maxModifierMoveIdx].Modifiers[coordinateIdx] > 0)
                        maxModifiersVector.Values.Add(0);
                    else
                        maxModifiersVector.Values.Add(this.Moves[maxModifierMoveIdx].Modifiers[coordinateIdx] * (-1));
                }
                else
                {
                    if (this.Moves[maxModifierMoveIdx].Modifiers[coordinateIdx] < 0)
                        maxModifiersVector.Values.Add(0);                    
                    else
                        maxModifiersVector.Values.Add(this.Moves[maxModifierMoveIdx].Modifiers[coordinateIdx]);
                }
            }
            return maxModifiersVector;
        }
        public List<int> CheckAllStatesForInvalidMoves()
        {
            List<int> numWinningStatesForEachState = new();
            for (int playerIdx = 0; playerIdx < this.NumOfPlayers; playerIdx++)
            {
                for (int stateIdx = playerIdx * this.StatesPerPlayer; stateIdx < (playerIdx + 1) * this.StatesPerPlayer; stateIdx++)
                {
                    Dimensions stateDimensions = new(this.GameStates[stateIdx].Dimensions);
                    for (int moveIdx = playerIdx * this.MovesPerPlayer; moveIdx < (playerIdx + 1) * this.MovesPerPlayer; moveIdx++)
                    {
                        Dimensions stepIntoDimensions = new(stateDimensions);
                        try
                        {
                            this.Moves[moveIdx].ApplyModifiers(stepIntoDimensions, false);
                        }
                        catch (Exception)
                        {
                            this.GameStates[stateIdx].NumWinningStates++;
                        }
                    }
                    if (this.GameStates[stateIdx].NumWinningStates == this.MovesPerPlayer && this.GameStates[stateIdx].MyState == GameState.State.UNDECIDED)
                    {
                        this.GameStates[stateIdx].Terminal = true;
                        if (LastMoveWins)
                        {
                            this.LosingStateIndiciesForFilling.Push(stateIdx);
                            this.GameStates[stateIdx].MyState = GameState.State.LOSING;
                        }
                        else
                        {
                            this.WinningStateIndiciesForFilling.Push(stateIdx);
                            this.GameStates[stateIdx].MyState = GameState.State.WINNNING;
                        }
                        this.NumStatesFilled++;
                    }
                    numWinningStatesForEachState.Add(this.GameStates[stateIdx].NumWinningStates);
                }
            }
            return numWinningStatesForEachState;
        }
        private void ExtendedTabulationFiller()
        {
            List<int> oldUndecidedStates;
            List<int> newUndecidedStates = TabulationFiller(null);
            do
            {
                oldUndecidedStates = new(newUndecidedStates);
                newUndecidedStates = TabulationFiller(newUndecidedStates);
            } while (!(oldUndecidedStates.SequenceEqual(newUndecidedStates)));
        }
        private List<int> TabulationFiller(List<int>? gameStateIndicies)
        {
            gameStateIndicies ??= GetInitialGameStateIndicies();
            List<int> undecidedStateIndicies = new();
            for (int i = 0; i < gameStateIndicies.Count; i++)
            {
                bool gameStateSaved = false;
                int generalIndex = gameStateIndicies[i];
                for (int playerIdx = 0; playerIdx < this.NumOfPlayers; playerIdx++)
                {
                    int gameStateIdx = playerIdx * this.StatesPerPlayer + generalIndex;
                    if (this.GameStates[gameStateIdx].MyState == GameState.State.UNDECIDED)
                    {
                        CheckPreviousStates(gameStateIdx);
                        bool stateStillUndecided = this.GameStates[gameStateIdx].MyState == GameState.State.UNDECIDED;
                        if (stateStillUndecided && !gameStateSaved)
                        {
                            gameStateIdx -= playerIdx * this.StatesPerPlayer;
                            undecidedStateIndicies.Add(gameStateIdx);
                            gameStateSaved = true;
                        }
                    }
                }
            }
            return undecidedStateIndicies;
        }
        private List<int> GetInitialGameStateIndicies()
        {
            List<int> gameStateIndicies = new();
            for (int i = 0; i < this.StatesPerPlayer; i++)
            {
                gameStateIndicies.Add(i);
            }
            return gameStateIndicies;
        }
        private void ForwardFiller()
        {
            for (int stateIdx = 0; stateIdx < this.StatesPerPlayer; stateIdx++)
            {
                for (int playerIdx = 0; playerIdx < this.NumOfPlayers; playerIdx++)
                {
                    int playerStateIdx = playerIdx * this.StatesPerPlayer + stateIdx;
                    if (this.GameStates[playerStateIdx].MyState == GameState.State.UNDECIDED)
                    {
                        this.GameStates[playerIdx * this.StatesPerPlayer + stateIdx].MyState = GameState.State.LOSING;
                        ForwardFillLosingState(playerStateIdx, false);
                    }
                }
            }
        }
        public void CompareSetBorderStatesToCheckAll()
        {
            List<int> statesCheckedWithCheckAll;
            List<int> statesCheckedWithCheckBorderStates = new();
            for (int stateIdx = 0; stateIdx < this.GameStates.Count; stateIdx++)
            {
                this.GameStates[stateIdx].NumWinningStates = 0;
            }
            CheckBorderStatesForInvalidMoves();
            for (int stateIdx = 0; stateIdx < (this.GameStates.Count / (this.Dimensions.Values[^1] + 1)); stateIdx++)
            {
                statesCheckedWithCheckBorderStates.Add(this.GameStates[stateIdx].NumWinningStates);
            }
        }
        public bool AtLeastOneUndecidedState()
        {
            bool result = false;
            foreach (var state in this.GameStates)
            {
                if(state.MyState == GameState.State.UNDECIDED)
                {
                    result = true;
                    return result;
                }
            }
            return result;
        }
    }
}
