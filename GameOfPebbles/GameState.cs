using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace GameOfPebblesLibrary 
{
    [Serializable]
    public class GameState : ISerializable
    {
        public enum State
        {
            UNDECIDED,
            WINNNING,
            LOSING
        }
        public Dimensions Dimensions { get; private set; } // Last dimension is the player who moves from this gamestate
        public State MyState { get; set; }
        public bool Terminal { get; set; }
        public int NumWinningStates { get; set; } // For GameStatistics and ExtendedForwardFiller
        public int NumLosingStates { get; set; } // For GameStatistics
        public int NumUndecidedStates { get; set; } // For GameStatistics
        public GameState(SerializationInfo info, StreamingContext context)
        {
            this.Dimensions = new(null, (List<int>)info.GetValue("dimensions", typeof(List<int>)));
            switch ((string)info.GetValue("state", typeof(string)))
            {
                case "winning":
                    this.MyState = State.WINNNING;
                    break;
                case "losing":
                    this.MyState = State.LOSING;
                    break;
                case "undecided":
                    this.MyState = State.UNDECIDED;
                    break;
                default:
                    this.MyState = State.UNDECIDED;
                    break;
            }
            this.Terminal = (bool)info.GetValue("terminal", typeof(bool));
            this.NumWinningStates = 0;
            this.NumLosingStates = 0;
            this.NumUndecidedStates = 0;
        }
        public GameState(Dimensions dimensions, State state, bool terminal)
        {
            this.Dimensions = dimensions;
            this.MyState = state;
            this.Terminal = terminal;
            this.NumWinningStates = 0;
            this.NumLosingStates = 0;
            this.NumUndecidedStates = 0;
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            JArray jsonDimensions = new();
            foreach (int dimValue in this.Dimensions.Values)
            {
                jsonDimensions.Add(dimValue);
            }
            info.AddValue("dimensions", jsonDimensions);
            switch (this.MyState)
            {
                case State.UNDECIDED:
                    info.AddValue("state", "undecided");
                    break;
                case State.WINNNING:
                    info.AddValue("state", "winning");
                    break;
                case State.LOSING:
                    info.AddValue("state", "losing");
                    break;
                default:
                    info.AddValue("state", "undecided");
                    break;
            }
            info.AddValue("terminal", this.Terminal);
        }
        public override string ToString()
        {
            string str = "";
            if (this.Dimensions != null && this.Dimensions.Values.Count > 0)
            {
                str += $"\nPile Sizes = ";
                for (int i = 0; i < this.Dimensions.Values.Count; i++)
                    str += this.Dimensions.Values[i] + ", ";
                str = str.Substring(0, str.Length - 2);
            }
            else
                str += "\nNo Dimensions Set!";
            str += $"\nValue = {this.MyState}";
            str += $"\nTerminal state? = ";
            str += this.Terminal ? $"Yes" : "No";
            str += "\nNumber of \"Y\" states we can move to = " + this.NumWinningStates;
            str += "\nNumber of \"N\" states we can move to = " + this.NumLosingStates;
            str += "\nNumber of \"D\" states we can move to = " + this.NumUndecidedStates;
            str += "\n";
            return str;
        }
    }
}
