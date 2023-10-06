using Newtonsoft.Json.Linq;

namespace GameOfPebblesLibrary
{
    public class PlayerFactory
    {
        public static IPlayer CreatePlayerFromJson(JObject jsonObj, GameModel game)
        {
            string playerType = jsonObj["playerType"].ToString();
            int playerID = (int)jsonObj["playerID"];
            if (playerType == "human")
            {
                return new PlayerHuman(playerID, game);
            }
            else if (playerType == "ai")
            {
                int aiIntelligence = (int)jsonObj["intelligence"];
                return new PlayerAI(playerID, game, (PlayerAI.Intelligence)aiIntelligence);
            }
            else
            {
                throw new ArgumentException("Unknown player type!");
            }
        }
    }
}
