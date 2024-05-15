using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
                //TODO check neighbors for mines and write amount
                //TODO call this function for every neighbor that doesn't have a mine
            }
            clicked.FlatStyle = FlatStyle.Flat;
            clicked.FlatAppearance.BorderSize = 1;
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
