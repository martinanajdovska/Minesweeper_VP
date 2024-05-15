﻿using System;
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
        int ticks = 0;
        static int rows = 13;
        static int cols = 15;
        int size = 25;
        int mines = 35;
        string difficulty = "easy";
        Button[,] field = new Button[rows, cols];
        static Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            CreateField("easy");
        }

        private void CreateField(string difficulty)
        {
            int leftStart = 6;
            int topStart = 60;
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
                mines = 250;
            }
            field = new Button[rows, cols];
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
                    newButton.MouseUp += new MouseEventHandler(MouseClickEvent);
                    this.field[i, j] = newButton;
                }
            }
            GenerateMines(mines);
            btnRestart.Left = this.Size.Width / 2 - 20;
            lblTime.Left = this.Size.Width - lblTime.Width - 81;
            lblHighScore.Left = this.Size.Width - lblHighScore.Width - 82;
            lblHighScoreValue.Left = lblHighScore.Left + 99;
            this.Show();
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
        private void MouseClickEvent(Object sender, MouseEventArgs e)
        {
            timer1.Enabled = true;
            if (e.Button == MouseButtons.Left) CheckForMine(sender);
            else if (e.Button == MouseButtons.Right) SetFlag(sender);
        }
        private void SetFlag(Object sender)
        {
            Button clicked = sender as Button;
            int i = int.Parse(clicked.Name.Split(',')[0]);
            int j = int.Parse(clicked.Name.Split(',')[1]);

            if (field[i, j].Tag.Equals("flag"))
            {
                field[i, j].BackgroundImage = null;
                field[i, j].FlatStyle = FlatStyle.Standard;
                field[i, j].Tag = "";
            }
            else
            {
                field[i, j].BackgroundImage = Properties.Resources.flag;
                field[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                field[i, j].Tag = "flag";
            }
        }
        private void CheckForMine(Object sender)
        {
            Button clicked = sender as Button;
            int i = int.Parse(clicked.Name.Split(',')[0]);
            int j = int.Parse(clicked.Name.Split(',')[1]);

            clicked = field[i, j];
            if (clicked.Tag.Equals("flag")) return;
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
            //clicked.FlatAppearance.BorderSize = 1;
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
                    if (field[i + k, j + l].FlatStyle == FlatStyle.Flat || field[i + k, j + l].Tag.Equals("flag")) continue;
                    if (field[i + k, j + l].Tag.Equals("bomb")) numOfMines++;
                }
            }
            field[i, j].Text = numOfMines == 0 ? "" : $"{numOfMines}";
            if (numOfMines != 0) return;

            field[i, j].FlatAppearance.BorderSize = 0;
            List<string> emptyNeighbors = GetEmptyNeighbours(i, j);
            if (emptyNeighbors.Count > 0)
            {
                foreach (string name in emptyNeighbors)
                {
                    int x = int.Parse(name.Split(',')[0]);
                    int y = int.Parse(name.Split(',')[1]);
                    if (field[x, y].FlatStyle == FlatStyle.Flat || field[x, y].Tag.Equals("flag")) continue;
                    field[x, y].FlatStyle = FlatStyle.Flat;
                    field[x, y].FlatAppearance.BorderSize = 1;
                    CountNeighbourMines(x, y);
                }
            }
        }
        private void GameOver()
        {
            timer1.Enabled = false;
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
                    field[i, j].Dispose();
                }
            }
        }

        private void Restart()
        {
            this.Hide();
            ticks = 0;
            lblTime.Text = $"Time: ";
            timer1.Enabled = false;
            ClearField();
            CreateField(difficulty);
        }
        private void btnRestart_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ticks++;
            int sec = ticks % 60;
            int min = ticks / 60;
            lblTime.Text = $"Time:   {min:00}:{sec:00}";
        }
        private void ChangeDifficultyDesign()
        {
            this.Hide();
            lblTime.Left = 0;
            btnRestart.Left = 0;
            lblHighScore.Left = 0;
            lblHighScoreValue.Left = 0;
        }

        private void easyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            difficulty = "easy";
            easyToolStripMenuItem1.Checked = true;
            normalToolStripMenuItem1.Checked = false;
            hardToolStripMenuItem1.Checked = false;
            ChangeDifficultyDesign();
            Restart();
        }

        private void normalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            difficulty = "normal";
            easyToolStripMenuItem1.Checked = false;
            normalToolStripMenuItem1.Checked = true;
            hardToolStripMenuItem1.Checked = false;
            ChangeDifficultyDesign();
            Restart();
        }

        private void hardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            difficulty = "hard";
            easyToolStripMenuItem1.Checked = false;
            normalToolStripMenuItem1.Checked = false;
            hardToolStripMenuItem1.Checked = true;
            ChangeDifficultyDesign();
            Restart();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }
    }
}
