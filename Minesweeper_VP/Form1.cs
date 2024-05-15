using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper_VP
{
    public partial class Form1 : Form
    {
        static int rows = 13;
        static int cols = 15;
        int size = 25;
        int mines = 35;
        Button[,] field = new Button[rows, cols];
        static Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            CreateField();
            GenerateMines(mines);
        }

        private void CreateField()
        {
            int leftStart = 6;
            int topStart = 50;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button newButton = new Button();
                    newButton.Parent = this;
                    newButton.Size = new Size(size, size);
                    newButton.Name = $"{i},{j}";
                    newButton.Tag = "";
                    newButton.Location = new Point(leftStart + j * size, topStart + i * size);
                    newButton.MouseClick += new MouseEventHandler(CheckForMine);
                    this.field[i, j] = newButton;
                }
            }
        }
        private void GenerateMines(int mines)
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
        private void CheckForMine(Object sender, MouseEventArgs e)
        {
            Button clicked = sender as Button;
            int i = int.Parse(clicked.Name.Split(',')[0]);
            int j = int.Parse(clicked.Name.Split(',')[1]);

            clicked = field[i, j];
            if (clicked.Tag.Equals("bomb"))
            {
                clicked.BackgroundImage = Properties.Resources.bomb;
                clicked.BackgroundImageLayout = ImageLayout.Stretch;
                GameOver();
            }
            else
            {
                CountNeighbourMines(i, j);
            }
            clicked.FlatStyle = FlatStyle.Flat;
            clicked.FlatAppearance.BorderSize = 1;
        }
        private List<string> GetEmptyNeighbours(int i, int j)
        {
            List<string> emptyNeighbours = new List<string>();
            for (int k = -1; k <= 1; k++)
            {
                for (int l = -1; l <= 1; l++)
                {
                    if (i + k < 0 || j + l < 0 || i + k >= rows || j + l >= cols) continue;
                    if (!field[i + k, j + l].Tag.Equals("bomb")) emptyNeighbours.Add(field[i + k, j + l].Name);
                }
            }
            return emptyNeighbours;
        }
        private void CountNeighbourMines(int i, int j)
        {
            int numOfMines = 0;
            for (int k = -1; k <= 1; k++)
            {
                for (int l = -1; l <= 1; l++)
                {
                    if (i + k < 0 || j + l < 0 || i + k >= rows || j + l >= cols) continue;
                    if (field[i + k, j + l].FlatStyle == FlatStyle.Flat) continue;
                    if (field[i + k, j + l].Tag.Equals("bomb")) numOfMines++;
                }
            }
            field[i, j].Text = numOfMines == 0 ? "" : $"{numOfMines}";
            if (numOfMines != 0) return;

            List<string> emptyNeighbors = GetEmptyNeighbours(i, j);
            if (emptyNeighbors.Count > 0)
            {
                foreach (string name in emptyNeighbors)
                {
                    int x = int.Parse(name.Split(',')[0]);
                    int y = int.Parse(name.Split(',')[1]);
                    if (field[x, y].FlatStyle == FlatStyle.Flat) continue;
                    field[x, y].FlatStyle = FlatStyle.Flat;
                    field[x, y].FlatAppearance.BorderSize = 1;
                    CountNeighbourMines(x, y);
                }
            }
        }
        private void GameOver()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    field[i, j].Enabled = false;
                }
            }
        }
        private void ClearField()
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

                }
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            ClearField();
            GenerateMines(this.mines);
        }
    }
}
