using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace GameOfPebblesLibrary
{
    [Serializable]
    public class GameTemplate : ISerializable
    {
        [Serializable]
        public class PlayedGame : ISerializable
        {
            public List<Tuple<int, int>> MoveHistory { get; private set; } // stores list of move indicies and thinking time in seconds
            public List<int> PlayerResults { get; private set; }
            public PlayedGame(List<Tuple<int, int>> moveHistory, List<int> playerResults) {
                this.MoveHistory = moveHistory;
                this.PlayerResults = playerResults;
            }
            PlayedGame(SerializationInfo info, StreamingContext context)
            {   
                this.MoveHistory = (List<Tuple<int, int>>)info.GetValue("moveHistory", typeof(List<Tuple<int, int>>));
                this.PlayerResults = (List<int>)info.GetValue("playerResults", typeof(List<int>));
            }
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("moveHistory", this.MoveHistory);
                info.AddValue("playerResults", this.PlayerResults);
            }
        }
        public string GameTitle { get; set; }
        public GameParameters GameParameters { get; private set; }
        public List<PlayedGame>? PlayedGames { get; private set; }
        public GameTemplate(string gameTitle, GameParameters gameParameters, List<PlayedGame>? gameHistory)
        {
            this.GameTitle = gameTitle;
            this.GameParameters = gameParameters;
            this.PlayedGames = gameHistory;
        }
        public override string ToString()
        {
            int losingPlayerIdx;
            int winningPlayerIdx;
            Move chosenMove;
            string player1Str;
            string player2Str;
            string whoWonStr;
            string whoMoves;
            string str = "";
            str += "Game Title: " + this.GameTitle + "\n";
            str += this.GameParameters + "\n";
            str += "Game History:\n";
            if (this.PlayedGames != null)
                for (int playedGameIdx = 0; playedGameIdx < this.PlayedGames.Count; playedGameIdx++)
                {
                    winningPlayerIdx = this.PlayedGames[playedGameIdx].PlayerResults[0];
                    losingPlayerIdx = this.PlayedGames[playedGameIdx].PlayerResults[1];
                    if (this.GameParameters.PlayerList[winningPlayerIdx] is PlayerAI)
                    {
                        whoWonStr = "AI";
                        if(winningPlayerIdx == 0)
                        {
                            player1Str = "AI";
                            player2Str = "Player";
                        }
                        else
                        {
                            player1Str = "Player";
                            player2Str = "AI";
                        }
                    }
                    else if (this.GameParameters.PlayerList[losingPlayerIdx] is PlayerHuman)
                    {
                        whoWonStr = $"Player#{winningPlayerIdx + 1}";
                        player1Str = "Player #1";
                        player2Str = "Player #2";
                    }
                    else
                    {
                        whoWonStr = "Player";
                        if (winningPlayerIdx == 0)
                        {
                            player1Str = "Player";
                            player2Str = "AI";
                        }
                        else
                        {
                            player1Str = "AI";
                            player2Str = "Player";
                        }
                    }
                    str += $"Played Game #{playedGameIdx + 1} - {whoWonStr} won!\n";
                    str += "Move History\n";
                    for (int moveThinkingTimePairIdx = 0; moveThinkingTimePairIdx < this.PlayedGames[playedGameIdx].MoveHistory.Count; moveThinkingTimePairIdx++)
                    {
                        if (moveThinkingTimePairIdx % 2 == 0) // first player moves
                            whoMoves = player1Str;
                        else
                            whoMoves = player2Str;
                        if (this.GameParameters.MoveSetLabel == "custom")
                            chosenMove = this.GameParameters.Moves[this.PlayedGames[playedGameIdx].MoveHistory[moveThinkingTimePairIdx].Item1];
                        else
                            chosenMove = null;
                        str += whoMoves + "s move: " + chosenMove + ", \t\tthinking Time: " + this.PlayedGames[playedGameIdx].MoveHistory[moveThinkingTimePairIdx].Item2 + " seconds\n";
                    }
                    str += "\n";
                }   
            return str;
        }
        public GameTemplate(SerializationInfo info, StreamingContext context)
        {
            this.GameTitle = (string)info.GetValue("title", typeof(string));
            List<int> dimensions = (List<int>)info.GetValue("dimensions", typeof(List<int>));
            JArray playerJsonArray = (JArray)info.GetValue("playerList", typeof(JArray));
            List<IPlayer> playerList = new();//
            foreach (JObject jsonObj in playerJsonArray)
            {
                IPlayer player = PlayerFactory.CreatePlayerFromJson(jsonObj, null);
                playerList.Add(player);
            }
            string typeOfMovesLabel  = (string)info.GetValue("typesOfMovesLabel", typeof(string));
            List<Move> moves = new();
            JArray movesJsonList = (JArray)info.GetValue("moves", typeof(JArray));
            foreach(var move in movesJsonList)
            {
                JToken modifiersJToken = move.SelectToken("modifiers");
                JArray modifiersJArray = (JArray)modifiersJToken;
                List<int> modifiers = modifiersJArray.ToObject<List<int>>();
                moves.Add(new(null, modifiers));
            }
            bool symetric = (bool)info.GetValue("symetric", typeof(bool));
            bool lastMoveWins = (bool)info.GetValue("lastMoveWins", typeof(bool));
            List<List<Tuple<int, string>>> terminalStates = (List<List<Tuple<int, string>>>)info.GetValue("terminalStates", typeof(List<List<Tuple<int, string>>>));
            List<GameState> gameStates = (List<GameState>)info.GetValue("gameStates", typeof(List<GameState>));
            this.GameParameters = new(
                dimensions, 
                playerList, 
                typeOfMovesLabel, 
                moves, 
                symetric,
                lastMoveWins, 
                terminalStates, 
                gameStates);
            this.PlayedGames = (List<PlayedGame>)info.GetValue("playedGames", typeof(List<PlayedGame>));
            if (this.PlayedGames == null)
                this.PlayedGames = new();
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("title", this.GameTitle);
            info.AddValue("dimensions", this.GameParameters.Dimensions);
            List<JObject> serializedPlayers = new List<JObject>();
            foreach (IPlayer player in this.GameParameters.PlayerList)
            {
                if (player is PlayerAI playerAI)
                {
                    JObject playerData = JObject.FromObject(playerAI);
                    serializedPlayers.Add(JObject.FromObject(playerData));

                }
                else if (player is PlayerHuman playerHuman)
                {
                    JObject playerData = JObject.FromObject(playerHuman);
                    serializedPlayers.Add(JObject.FromObject(playerData));
                }
            }
            info.AddValue("playerList", serializedPlayers);
            info.AddValue("typesOfMovesLabel", this.GameParameters.MoveSetLabel);
            info.AddValue("moves", this.GameParameters.Moves);
            info.AddValue("symetric", this.GameParameters.Symetric);
            info.AddValue("lastMoveWins", this.GameParameters.LastMoveWins);
            info.AddValue("terminalStates", this.GameParameters.TerminalStates);
            info.AddValue("gameStates", this.GameParameters.GameStates);
            info.AddValue("playedGames", PlayedGames);
        }
    }
}
