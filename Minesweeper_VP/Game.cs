using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper_VP
{
    public class Game
    {
        public int rows { get; set; }
        public int cols { get; set; }
        public int size { get; set; }
        public int mines { get; set; }
        public int openedTiles { get; set; }
        public int totalEmptyTiles { get; set; }
        public int numOfFlagsUsed { get; set; }
        public Button[,] field { get; set; }
        public Form1 form { get; set; }
        static Random random = new Random();

        public Game(string difficulty, Form1 form)
        {
            this.form = form;
            CreateField(difficulty);
        }
        public void CreateField(string difficulty)
        {
            int leftStart = 6;
            int topStart = 65;
            if (difficulty.Equals("easy"))
            {
                rows = 13;
                cols = 15;
                mines = 35;
            }
            else if (difficulty.Equals("normal"))
            {
                rows = 18;
                cols = 22;
                mines = 100;
            }
            else
            {
                rows = 25;
                cols = 45;
                mines = 260;
            }
            totalEmptyTiles = rows * cols - mines;

            field = new Button[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button newButton = new Button();
                    newButton.Parent = this.form;
                    newButton.Size = new Size(size, size);
                    newButton.Name = $"{i},{j}";
                    newButton.Tag = "";
                    newButton.Location = new Point(leftStart + j * size, topStart + i * size);
                    newButton.MouseUp += new MouseEventHandler(form.MouseClickEvent);
                    this.field[i, j] = newButton;
                }
            }

            GenerateMines(mines);
        }
        public void GenerateMines(int mines)
        {
            while (mines > 0)
            {
                int i = random.Next(50);
                if (i < rows)
                {
                    int j = random.Next(50);
                    if (j < cols)
                    {
                        if (field[i, j].Tag.Equals("bomb")) continue;
                        field[i, j].Tag = "bomb";
                        field[i, j].Text = "b";
                        mines--;
                    }
                }
            }
        }
        public void SetFlag(int i, int j)
        {
            Button clicked = field[i, j];
            if (clicked.Tag.Equals("") || clicked.Tag.Equals("bomb"))
            {
                clicked.BackgroundImage = Properties.Resources.flag;
                clicked.BackgroundImageLayout = ImageLayout.Stretch;
                numOfFlagsUsed++;

                if (clicked.Tag.Equals("")) clicked.Tag = "flag";
                else clicked.Tag = "flagged-bomb";
            }
            else
            {
                clicked.BackgroundImage = null;
                clicked.FlatStyle = FlatStyle.Standard;
                numOfFlagsUsed--;

                if (clicked.Tag.Equals("flagged-bomb")) clicked.Tag = "bomb";
                else clicked.Tag = "";
            }
        }
        public void CheckForMine(int i, int j)
        {
            Button clicked = field[i, j];
            if (clicked.Tag.Equals("flag") || clicked.Tag.Equals("flagged-bomb")) return;

            if (openedTiles == 0 || !clicked.Tag.Equals("bomb"))
            {
                if (openedTiles == 0) totalEmptyTiles++;
                clicked.FlatStyle = FlatStyle.Flat;
                CountNeighbourMines(i, j);
                openedTiles++;
            }
            else
            {
                clicked.BackgroundImage = Properties.Resources.bomb;
                clicked.BackgroundImageLayout = ImageLayout.Stretch;
                GameOver();
            }
        }

        public void CountNeighbourMines(int i, int j)
        {
            int numOfMines = 0;
            bool flag = false;

            for (int k = -1; k <= 1; k++)
            {
                for (int l = -1; l <= 1; l++)
                {
                    if (i + k < 0 || j + l < 0 || i + k >= rows || j + l >= cols) continue;
                    if (field[i + k, j + l].FlatStyle == FlatStyle.Flat) continue;
                    if (field[i + k, j + l].Tag.Equals("flag") || field[i + k, j + l].Tag.Equals("flagged-bomb")) flag = true;
                    if (field[i + k, j + l].Tag.Equals("bomb")) numOfMines++;
                }
            }
            field[i, j].Text = numOfMines == 0 ? "" : $"{numOfMines}";

            if (numOfMines == 1) field[i, j].ForeColor = Color.Blue;
            else if (numOfMines == 2) field[i, j].ForeColor = Color.Green;
            else if (numOfMines == 3) field[i, j].ForeColor = Color.Red;
            else if (numOfMines == 4) field[i, j].ForeColor = Color.Purple;
            else if (numOfMines == 5) field[i, j].ForeColor = Color.Brown;
            else if (numOfMines == 6) field[i, j].ForeColor = Color.Aqua;
            field[i, j].FlatAppearance.BorderColor = Color.LightGray;

            if (numOfMines == 0) field[i, j].FlatAppearance.BorderSize = 0;
            if (flag || numOfMines != 0) return;

            List<string> emptyNeighbors = GetEmptyNeighbours(i, j);
            if (emptyNeighbors.Count > 0)
            {
                for (int index = 0; index < emptyNeighbors.Count; index++)
                {
                    int x = int.Parse(emptyNeighbors.ElementAt(index).Split(',')[0]);
                    int y = int.Parse(emptyNeighbors.ElementAt(index).Split(',')[1]);

                    field[x, y].FlatStyle = FlatStyle.Flat;
                    field[x, y].FlatAppearance.BorderSize = 1;
                    openedTiles++;
                    CountNeighbourMines(x, y);
                }
            }
        }
        private List<string> GetEmptyNeighbours(int i, int j)
        {
            List<string> emptyNeighbours = new List<string>();
            for (int k = -1; k <= 1; k++)
            {
                for (int l = -1; l <= 1; l++)
                {
                    if (i + k < 0 || j + l < 0 || i + k >= rows || j + l >= cols) continue;
                    if (field[i + k, j + l].Tag.Equals("") && field[i + k, j + l].FlatStyle != FlatStyle.Flat) emptyNeighbours.Add(field[i + k, j + l].Name);
                }
            }
            return emptyNeighbours;
        }
        public void GameOver()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    field[i, j].Enabled = false;
                }
            }
            form.GameOver();
        }
        public void DeleteField()
        {
            // game = new game()????
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    field[i, j].Dispose();
                }
            }
        }
        public void ClearField()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    field[i, j].Enabled = true;
                    field[i, j].Text = "";
                    field[i, j].Tag = "";
                    field[i, j].FlatStyle = FlatStyle.Standard;
                    field[i, j].BackgroundImage = null;
                    field[i, j].ForeColor = Color.Black;
                    field[i, j].FlatAppearance.BorderColor = Color.Black;
                }
            }
        }
    }
}
