using System.Runtime.Serialization;

namespace GameOfPebblesLibrary
{
    [Serializable]
    public class Move : ISerializable
    {
        public GameModel? Game { get; set; }
        public List<int> Modifiers { get; private set; } // How it modifies the piles
        public Move(GameModel? game, List<int> modifiers)
        {
            this.Game = game;
            this.Modifiers = modifiers;
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("modifiers", this.Modifiers);
        }
        public override string ToString()
        {
            string str = $"(";
            for (int i = 0; i < this.Modifiers.Count - 1; i++)
            {
                if (this.Modifiers[i] > 0)
                    str += "+";
                str += this.Modifiers[i] + ", ";
            }
            str = str[..^2];
            str += $")";
            return str;
        }
        public void Execute()
        {
            if(this.Game != null)
            {
                ApplyModifiers(this.Game.CurrentGameDimensions, false);
                this.Game.CurrentGameStateIndex = this.Game.GameEngine.Dimensions.MapDimensionsToIndex(this.Game.CurrentGameDimensions);
            }
        }
        public Dimensions ApplyModifiers(Dimensions dimensionsToChange, bool negativeModifiers)
        {
            List<int> modifiers = negativeModifiers ? NegateMoveModifiers() : this.Modifiers;
            for(int i = 0; i < dimensionsToChange.Values.Count - 1; i++)
            {
                dimensionsToChange.Values[i] += modifiers[i];
                if (dimensionsToChange.Values[i] <  0 || dimensionsToChange.Values[i] > dimensionsToChange.Game.Dimensions.Values[i])
                {
                    throw new Dimensions.InvalidDimensionException();
                }
            }
            if (negativeModifiers)
            {
                dimensionsToChange.Values[^1]--;
                if(dimensionsToChange.Values[^1] < 0) 
                    dimensionsToChange.Values[^1] = dimensionsToChange.Game.Dimensions.Values[^1];
            }
            else 
            {           
                dimensionsToChange.Values[^1]++; // passes the move to the next player
                dimensionsToChange.Values[^1] = dimensionsToChange.Values[^1] % (dimensionsToChange.Game.Dimensions.Values[^1] + 1); // passes the move to the first player
            }
            return dimensionsToChange;
        }
        public List<int> NegateMoveModifiers()
        {
            List<int> NegatedModifiers = new(this.Modifiers);
            for(int i = 0; i < NegatedModifiers.Count; i++)
            {
                NegatedModifiers[i] *= -1;
            }
            return NegatedModifiers;
        }
        public static List<Move> CreateNimMoves(GameModel gameModel)
        {
            List<Move> result = new();
            for (int i = 0; i < gameModel.PlayerList.Count; i++)
            {
                List<IPlayer> owner = new();
                owner.Add(gameModel.PlayerList[i]);
                for (int j = 0; j < gameModel.Dimensions.Values.Count - 1; j++)
                {
                    for (int k = 1; k <= gameModel.Dimensions.Values[j]; k++)
                    {
                        List<int> modifiers = new()
                        {
                            Capacity = gameModel.Dimensions.Values.Capacity
                        };
                        for (int l = 0; l < gameModel.Dimensions.Values.Count - 1; l++)
                        {
                            if (l == j)
                            {
                                modifiers.Add(k * -1);
                            }
                            else
                            {
                                modifiers.Add(0);
                            }
                        }
                        modifiers.Add(1);
                        result.Add(new Move(gameModel, modifiers));
                    }
                }
            }
            return result;
        }
    }
}
