namespace GameOfPebblesLibrary
{
    public class Dimensions
    {
        public class InvalidDimensionException : Exception
        { }
        public class DimensionsMismatchException : Exception
        { }
        public GameModel? Game { get; set; }
        public List<int> Values { get; private set; }
        public Dimensions() {
            this.Values = new();
        }
        public Dimensions(GameModel game, List<int> dimensionValues)
        {
            this.Game = game;
            this.Values = dimensionValues;
        }
        public Dimensions(Dimensions other)
        {
            this.Game = other.Game;
            this.Values = new(other.Values);
        }
        public override string ToString()
        {
            string str = "( ";
            for (int i = 0; i < this.Values.Count - 1; i++)
            {
                str += this.Values[i] + " ";
            }
            str += ")";
            return str;
        }
        public void IncrementDimensions(Dimensions dimensionsToIncrement, Dimensions StartValuesVector)
        {
            dimensionsToIncrement.Values[0]++;
            for (int i = 1; i < dimensionsToIncrement.Values.Count; i++)
            {
                if (dimensionsToIncrement.Values[i - 1] > this.Values[i - 1])
                {
                    dimensionsToIncrement.Values[i - 1] = StartValuesVector.Values[i - 1];
                    dimensionsToIncrement.Values[i]++;
                }
                else
                    break;
            }
        }
        public void IncrementDimensionAt(int index)
        {
            this.Values[index]++;
        }
        public int CalculateSpace()
        {
            int space = 1;
            for (int i = 0; i < this.Values.Count; i++)
            {
                space *= (this.Values[i] + 1);
            }
            return space;
        }
        public int MapDimensionsToIndex(Dimensions dimensionsToMap)
        {
            if (dimensionsToMap.Values.Count != this.Values.Count)
                throw new DimensionsMismatchException();
            int space = CalculateSpace();
            int index = 0;
            for (int dimIdx = this.Values.Count - 1; dimIdx >= 0; dimIdx--)
            {
                space /= (this.Values[dimIdx] + 1);
                index += dimensionsToMap.Values[dimIdx] * space;
            }
            return index;
        }
        public void IncrementDimensionsBasedOnIndicies(Dimensions dimensionsToIncrement, Dimensions startingValuesVector, List<int> orderOfCoordinates)
        {
            dimensionsToIncrement.Values[orderOfCoordinates[0]]++;
            for (int i = 1; i < dimensionsToIncrement.Values.Count - 1; i++)
            {
                if (dimensionsToIncrement.Values[orderOfCoordinates[i - 1]] > this.Values[orderOfCoordinates[i - 1]])
                {
                    dimensionsToIncrement.Values[orderOfCoordinates[i - 1]] = startingValuesVector.Values[i - 1];
                    dimensionsToIncrement.Values[orderOfCoordinates[i]]++;
                }
                else
                    break;
            }
            if (dimensionsToIncrement.Values[orderOfCoordinates[^1]] > this.Values[orderOfCoordinates[^1]])
                dimensionsToIncrement.Values[orderOfCoordinates[^1]] = startingValuesVector.Values[^1];
        }
        public void DecrementDimensionsBasedOnIndicies(Dimensions dimensionsToDecrement, Dimensions startingValuesVector, Dimensions endingValuesVector , List<int> orderOfCoordinates)
        {
            dimensionsToDecrement.Values[orderOfCoordinates[0]]--;
            for (int i = 1; i < dimensionsToDecrement.Values.Count - 1; i++)
            {
                if (dimensionsToDecrement.Values[orderOfCoordinates[i - 1]] < endingValuesVector.Values[orderOfCoordinates[i - 1]])
                {
                    dimensionsToDecrement.Values[orderOfCoordinates[i - 1]] = startingValuesVector.Values[orderOfCoordinates[i - 1]];
                    dimensionsToDecrement.Values[orderOfCoordinates[i]]--;
                }
                else
                    break;
            }
        }
        public Dimensions MulitplyDimensions(int scalar)
        {
            for (int i = 0; i < this.Values.Count; i++)
            {
                this.Values[i] *= scalar;
            }
            return this;
        }
        public Dimensions AddDimensions(Dimensions otherDimensions)
        {
            if (otherDimensions.Values.Count != this.Values.Count)
                throw new DimensionsMismatchException();
            for(int i = 0; i < this.Values.Count; i++)
            {
                this.Values[i] += otherDimensions.Values[i];
            }
            return this;
        }
        public static Dimensions CreateZeroVector(GameModel game, int count)
        {
            List<int> values = new();
            for (int i = 0; i < count; i++)
            {
                values.Add(0);
            }
            return new Dimensions(game, values);
        }
    }
}
