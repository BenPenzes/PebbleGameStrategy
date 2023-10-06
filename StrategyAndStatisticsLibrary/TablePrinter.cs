using GameOfPebblesLibrary;
using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

namespace StrategyAndStatisticsLibrary
{
    public class TablePrinter
    {
        public GameEngine GameEngine { get; private set; }
        private string FolderPath { get;  set; }
        private string Filename { get; set; }
        private string Separator { get; set; }
        public List<List<string>> Table { get; private set; }
        public List<int> Offsets { get; private set; }
        int Rows;
        int Cols;
        public TablePrinter(GameEngine GameEngine, string Path, string Filename, string Separator)
        {
            this.GameEngine= GameEngine;
            this.FolderPath = Path;
            this.Filename = Filename;
            this.Separator = Separator;
            this.Offsets = new List<int>();
            this.Table = InitializeTable();
            Dimensions Zeroes = Dimensions.CreateZeroVector(
                this.GameEngine.GameModel,
                this.GameEngine.Dimensions.Values.Count);
            BuildTable(Zeroes, Zeroes.Values.Count -1, 0, 0);
        }
        public void PrintExcel()
        {
            Application excelApp = new();
            Workbook workbook = excelApp.Workbooks.Add();
            Worksheet worksheet = workbook.Worksheets[1];
            int rows = this.Table.Count;
            int cols = this.Table[0].Count; // all rows have the same number of columns
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if(this.Table[i][j] == " (1)")
                        Console.Write("{" + this.Table[i][j] + "}");
                    worksheet.Cells[i + 1, j + 1].Value2 = this.Table[i][j];
                }
            }
            for (int col = 1; col <= worksheet.UsedRange.Columns.Count; col++)
            {
                worksheet.Columns[col].AutoFit();
            }
            var colorY = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
            var colorN = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            var colorD = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
            var colorForNumberBackground = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkBlue);
            var ColorForNumberCharacter = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= cols; j++)
                {
                    bool hasNumeric = Regex.IsMatch(worksheet.Cells[i, j].Value2.ToString(), @"\d");

                    if (hasNumeric)
                    {
                        worksheet.Cells[i, j].Interior.Color = colorForNumberBackground;
                        worksheet.Cells[i, j].Font.Color = ColorForNumberCharacter;
                    }
                    if (worksheet.Cells[i, j].Value2.ToString() == "Y")
                    {
                        worksheet.Cells[i, j].Interior.Color = colorY;
                    }
                    else if (worksheet.Cells[i, j].Value2.ToString() == "N")
                    {
                        worksheet.Cells[i, j].Interior.Color = colorN;
                    }
                    else if (worksheet.Cells[i, j].Value2.ToString() == "D")
                    {
                        worksheet.Cells[i, j].Interior.Color = colorD;
                    }
                }
            }
            Microsoft.Office.Interop.Excel.Range range = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[rows, cols]];
            range.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
            range.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
            range.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
            range.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
            range.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
            range.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;
            workbook.SaveAs(FolderPath + Filename + ".xlsx");
            workbook.Close();
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }
        public void PrintCSV()
        {
            using (StreamWriter writer = new StreamWriter(FolderPath + Filename + ".csv"))
            {
                for (int i = 0; i < this.Table.Count; i++)
                {
                    string str = "";
                    int colNumber = this.Table[i].Count;
                    for (int j = 0; j < colNumber; j++)
                    {
                        str += this.Table[i][j] + this.Separator;
                    }
                    str = str.Substring(0, str.Length - 1);
                    writer.WriteLine(str);
                }
            }
        }
        public List<List<string>> InitializeTable()
        {
            int rows = 1, cols = 1;
            List<List<string>> table = new List<List<string>>();
            int pileDimensions = this.GameEngine.Dimensions.Values.Count - 1;
            if(pileDimensions < 2) // The game is 1D = 1 Pile
            {
                rows *= 2;
                cols *= (this.GameEngine.Dimensions.Values[0] + 1);
                Offsets.Add(0);
            }
            else // The game is 2D
            {
                rows *= (this.GameEngine.Dimensions.Values[1] + 2);
                cols *= (this.GameEngine.Dimensions.Values[0] + 2);
                this.Offsets.Add(0); // 1D => For the columns
                this.Offsets.Add(1); // 2D => For the rows
            }
            if (pileDimensions > 2)
            {
                Offsets.Add(cols + 1);
                cols *= (this.GameEngine.Dimensions.Values[2] + 1);
                cols += this.GameEngine.Dimensions.Values[2]; // These represent a one column break
            }
            if(pileDimensions > 3)
            {
                Offsets.Add(rows + 1);
                rows *= (this.GameEngine.Dimensions.Values[3] + 1);
                rows += this.GameEngine.Dimensions.Values[3]; // These represent a one row break
            }
            if(pileDimensions > 4)
            {
                for (int i = 4; i < pileDimensions; i++) // start from 4 which is the idx of the 5th dimension
                { // go until -1 because we handle the player parts separately
                    if (i % 2 == 0) // this means odd dimensions 5, 7 etc. -> multiply columns
                    {
                        rows++;
                        cols++;
                        this.Offsets.Add(cols + 1);
                        cols *= (this.GameEngine.Dimensions.Values[i] + 1);
                        cols += this.GameEngine.Dimensions.Values[i]; // These represent a one column break
                    }
                    else
                    {
                        this.Offsets.Add(rows + 1);
                        rows *= (this.GameEngine.Dimensions.Values[i] + 1);
                        rows += this.GameEngine.Dimensions.Values[i]; // These represent a one row break                         
                    }
                }
            }
            // handle the players' part
            rows++;
            cols++;
            this.Offsets.Add(rows + 1);
            rows *= (this.GameEngine.Dimensions.Values[pileDimensions] + 1); // pileDimensions = Player Dimension
            rows += this.GameEngine.Dimensions.Values[pileDimensions]; // These represent a one row break                         
            this.Rows = rows;
            this.Cols = cols;
            for (int i = 0; i < rows; i++)
            {
                List<string> row = new();
                for (int j = 0; j < cols; j++)
                {
                    row.Add(" ");
                }
                table.Add(row);
            }
            return table;
        }
        public string JoinTable()
        {
            string str = "";
            for(int i = 0; i < this.Table.Count; i++)
            {
                int colNumber = this.Table[i].Count;
                for (int j = 0; j < colNumber; j++)
                {
                    str += this.Table[i][j] + this.Separator;
                }
                str = str.Substring(0, str.Length - 1);
                str += "\n";
            }
            return str;
        }
        public void WriteDimNumber(Dimensions CurrentDimensions, int DimIndex, int RowIndex, int ColIndex)
        {
            int MaxDimIndex = this.GameEngine.Dimensions.Values.Count - 1;
            if (DimIndex == MaxDimIndex)
            {
                this.Table[RowIndex][ColIndex] = ("Player #" + (CurrentDimensions.Values[DimIndex] + 1));
                return;
            }
            if (DimIndex == 1)
            {
                this.Table[RowIndex][ColIndex] += CurrentDimensions.Values[DimIndex];
                return;
            }
            if (DimIndex % 2 == 0) // odd Dimension Number -> 3D, 5D, 7D, etc.
            {
                this.Table[RowIndex][ColIndex] += ("[" + CurrentDimensions.Values[DimIndex]);
                if (DimIndex == MaxDimIndex - 1) // so if the game itself is 3D, 5D, etc. we write the upper left corner
                     // in this format [0], or [7], etc.
                {
                    this.Table[RowIndex][ColIndex] += "]";
                }
                else // the game is even dimensioned so we always write in the upper left corner in the following format: (DimEven, DimOdd),i. e. [0,1], [5,3]
                {
                    this.Table[RowIndex][ColIndex] += ("," + CurrentDimensions.Values[DimIndex + 1] + "]");
                }
                return;
            }
        }
        List<int> CalcStartingCellIndex(int dimIdx, int rowIdx, int colIdx)
        {
            int MaxDimIndex = this.GameEngine.Dimensions.Values.Count - 1;
            if (dimIdx == MaxDimIndex)
            {
                if(MaxDimIndex <= 2) 
                {
                    return new List<int>() { rowIdx + 2, colIdx + 1 };
                }
                return new List<int>() { rowIdx + 1, colIdx + 1 };
            }
            if (dimIdx == 1)
            {
                return new List<int>() { rowIdx, colIdx + 1 };
            }
            if (dimIdx == 2)
            {
                return new List<int>() { rowIdx + 1, colIdx};
            }
            if(dimIdx % 2 == 0)
            {
                return new List<int>() { rowIdx + 1, colIdx + 1 };
            }
            return new List<int>() { rowIdx, colIdx};
        }
        private void BuildTable(Dimensions iterationDimensions, int dimIdx, int rowIdx, int colIdx)
        {
            if (dimIdx == 0) // base case
            {
                if (iterationDimensions.Values[dimIdx + 1] == 0 || iterationDimensions.Values.Count == 2) // writes the header row of the first dimension
                {
                    for (int i = 0; i < this.GameEngine.Dimensions.Values[dimIdx] + 1; i++)
                    {
                        Table[rowIdx - 1][colIdx + i] = i.ToString();
                    }
                }
                for (int i = 0; i < this.GameEngine.Dimensions.Values[dimIdx] + 1; i++)
                {
                    int GameStateIndex = this.GameEngine.Dimensions.MapDimensionsToIndex(iterationDimensions);
                    GameState.State state = this.GameEngine.GameStates[GameStateIndex].MyState;
                    if(state == GameState.State.WINNNING)
                        Table[rowIdx][colIdx + i] = "Y";
                    if (state == GameState.State.LOSING)
                        Table[rowIdx][colIdx + i] = "N";
                    if (state == GameState.State.UNDECIDED)
                        Table[rowIdx][colIdx + i] = "D";
                    iterationDimensions.IncrementDimensionAt(dimIdx);
                }
                iterationDimensions.Values[dimIdx] = 0;
                return;
            }
            for (int i = 0; i < this.GameEngine.Dimensions.Values[dimIdx] + 1; i++)
            {
                List<int> NewStartingCell;
                if (dimIdx % 2 == 1 || dimIdx == this.GameEngine.Dimensions.Values.Count - 1)
                {
                    WriteDimNumber(iterationDimensions, dimIdx, rowIdx + Offsets[dimIdx] * i, colIdx);
                    NewStartingCell = CalcStartingCellIndex(dimIdx, rowIdx + Offsets[dimIdx] * i, colIdx);
                }
                else
                {
                    WriteDimNumber(iterationDimensions, dimIdx, rowIdx, colIdx + Offsets[dimIdx] * i);
                    NewStartingCell = CalcStartingCellIndex(dimIdx, rowIdx, colIdx + Offsets[dimIdx] * i);
                }
                BuildTable(
                    iterationDimensions, 
                    dimIdx - 1,
                    NewStartingCell[0],
                    NewStartingCell[1]);
                iterationDimensions.IncrementDimensionAt(dimIdx);
            }
            iterationDimensions.Values[dimIdx] = 0;
        }
    }
}
