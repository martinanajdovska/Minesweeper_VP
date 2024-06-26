﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        int openedTiles = 0;
        int totalEmptyTiles = 0;
        int numOfFlagsUsed = 0;
        string difficulty = "easy";
        Button[,] field = new Button[rows, cols];
        static Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            CreateField();
        }
        /// <summary>
        /// Used to create a new game at the start of the game or when changing a difficulty
        /// It changes the number of tiles to open and number of mines based on the difficulty
        /// </summary>
        private void CreateField()
        {
            // The positions at which the mines will start to be attached
            int leftStart = 6;
            int topStart = 65;
            if (difficulty.Equals("easy"))
            {
                rows = 13;
                cols = 15;
                mines = 25;
                lblHighScore.Text = $"High score: {Properties.Settings.Default.easyHighScore}";
            }
            else if (difficulty.Equals("normal"))
            {
                rows = 18;
                cols = 22;
                mines = 65;
                lblHighScore.Text = $"High score: {Properties.Settings.Default.normalHighScore}";
            }
            else
            {
                rows = 25;
                cols = 45;
                mines = 170;
                lblHighScore.Text = $"High score: {Properties.Settings.Default.hardHighScore}";
            }
            totalEmptyTiles = rows * cols - mines;

            field = new Button[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button newButton = new Button();
                    newButton.Parent = this;
                    newButton.Size = new Size(size, size);
                    newButton.Name = $"{i},{j}"; // used to identify the buttons by their position on the form
                    newButton.Tag = ""; // used to check if a button is empty, has a mine or has a flag
                    newButton.Location = new Point(leftStart + j * size, topStart + i * size); // makes sure the buttons are positioned next to each other
                    newButton.MouseUp += new MouseEventHandler(MouseClickEvent);
                    this.field[i, j] = newButton;
                    this.Update();
                }
            }

            GenerateMines(mines);
            btnRestart.Left = this.Size.Width / 2 - 20;
        }
        /// <summary>
        /// Generates the number of mines provided as an argument in random spots
        /// </summary>
        /// <param name="mines"></param>
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
                        mines--;
                    }
                }
            }
        }
        /// <summary>
        /// It enables the timer in case this is the user's first click of the game
        /// Checks for a mine or sets a flag depending on if the user used left or right mouse click
        /// It also calls a function that calculates the score after the click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseClickEvent(Object sender, MouseEventArgs e)
        {
            timer1.Enabled = true;
            Button clicked = sender as Button;
            if (clicked.FlatStyle == FlatStyle.Flat) return;

            if (e.Button == MouseButtons.Left) CheckForMine(clicked);
            else if (e.Button == MouseButtons.Right) SetFlag(clicked);
            CalculateScore();
        }
        /// <summary>
        /// If the button is empty or it has a bomb it sets a flag
        /// If the button has a flag already then it removes it
        /// </summary>
        /// <param name="clicked"></param>
        private void SetFlag(Button clicked)
        {
            if (clicked.Tag.Equals("") || clicked.Tag.Equals("bomb"))
            {
                clicked.BackgroundImage = Properties.Resources.flag;
                clicked.BackgroundImageLayout = ImageLayout.Stretch;
                numOfFlagsUsed++;

                if (clicked.Tag.Equals("bomb")) clicked.Tag = "flagged-bomb"; // used to make sure we return the bomb in its place after the removal of the flag
                else clicked.Tag = "flag";
            }
            else
            {
                clicked.BackgroundImage = null;
                clicked.FlatStyle = FlatStyle.Standard;
                numOfFlagsUsed--;

                if (clicked.Tag.Equals("flagged-bomb")) clicked.Tag = "bomb"; // removes the flag and brings back the mine
                else clicked.Tag = "";
            }
        }
        /// <summary>
        /// Checks if the clicked button has a mine underneath or if it is empty
        /// If it's empty then it checks all of its neighbors for mines and calculates the number of mines around it
        /// If it has a mine then it calls GameOver
        /// </summary>
        /// <param name="clicked"></param>
        private void CheckForMine(Button clicked)
        {
            if (clicked.Tag.Equals("flag") || clicked.Tag.Equals("flagged-bomb")) return;
            if (openedTiles == 0 || !clicked.Tag.Equals("bomb")) // openedTiles makes sure the first click doesn't end the game even if there is a mine
            {
                if (openedTiles == 0 && clicked.Tag.Equals("bomb")) totalEmptyTiles++;
                CountNeighbourMines(clicked);
                clicked.FlatStyle = FlatStyle.Flat;
                clicked.FlatAppearance.BorderSize = 1;
            }
            else
            {
                clicked.BackgroundImage = Properties.Resources.bomb;
                clicked.BackgroundImageLayout = ImageLayout.Stretch;
                GameOver();
            }
        }
        /// <summary>
        /// For a given button checks all of its neighbors that don't have a mine or a flag and returns them
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>A list of untagged buttons</returns>
        private List<Button> GetEmptyNeighbours(int i, int j)
        {
            List<Button> emptyNeighbours = new List<Button>();
            for (int k = -1; k <= 1; k++)
            {
                for (int l = -1; l <= 1; l++)
                {
                    if (i + k < 0 || j + l < 0 || i + k >= rows || j + l >= cols) continue; // checks if it is out of bounds
                    if (field[i + k, j + l].Tag.Equals("")) emptyNeighbours.Add(field[i + k, j + l]); // checks if the button has a flag or a bomb
                }
            }
            return emptyNeighbours;
        }
        /// <summary>
        /// Counts the number of mines around the given button
        /// It opens every button that doesn't have a mine underneath, stopping when that button has a mine or a flag as a neighbor
        /// It sets the text of the button to say how many mines are around it
        /// If this button is empty and has 0 mines around it, it calls the method again for every neighbor
        /// </summary>
        /// <param name="button"></param>
        private void CountNeighbourMines(Button button)
        {
            int i = int.Parse(button.Name.Split(',')[0]);
            int j = int.Parse(button.Name.Split(',')[1]);

            int numOfMines = 0;
            bool flag = false;
            for (int k = -1; k <= 1; k++)
            {
                for (int l = -1; l <= 1; l++)
                {
                    if (i + k < 0 || j + l < 0 || i + k >= rows || j + l >= cols) continue;
                    if (field[i + k, j + l].Tag.Equals("flag") || field[i + k, j + l].Tag.Equals("flagged-bomb")) flag = true;
                    if (field[i + k, j + l].Tag.Equals("bomb") || field[i + k, j + l].Tag.Equals("flagged-bomb")) numOfMines++;
                }
            }
            button.Text = numOfMines == 0 ? "" : $"{numOfMines}";

            if (numOfMines == 1) field[i, j].ForeColor = Color.Blue;
            else if (numOfMines == 2) field[i, j].ForeColor = Color.Green;
            else if (numOfMines == 3) field[i, j].ForeColor = Color.Red;
            else if (numOfMines == 4) field[i, j].ForeColor = Color.Purple;
            else if (numOfMines == 5) field[i, j].ForeColor = Color.Brown;
            else if (numOfMines == 6) field[i, j].ForeColor = Color.Aqua;
            button.FlatAppearance.BorderColor = Color.LightGray;
            button.FlatStyle = FlatStyle.Flat;

            openedTiles++;

            if (numOfMines == 0) field[i, j].FlatAppearance.BorderSize = 0;

            if (flag || numOfMines != 0) return; // if there is a flag or mine around it then don't continue checking neighbors

            List<Button> emptyNeighbors = GetEmptyNeighbours(i, j);
            if (emptyNeighbors.Count > 0)
            {
                for (int index = 0; index < emptyNeighbors.Count; index++)
                {
                    Button neighbor = emptyNeighbors.ElementAt(index);
                    if (neighbor.FlatStyle == FlatStyle.Flat) continue; // makes sure opened buttons aren't counted more than once
                    neighbor.FlatStyle = FlatStyle.Flat;
                    neighbor.FlatAppearance.BorderSize = 1;
                    CountNeighbourMines(neighbor);
                }
            }
        }
        /// <summary>
        /// Calculates the score differently for every difficulty and displays the value on the form
        /// Score is calculated based on time spent playing, opened tiles (in case the user clicks a mine) 
        /// and number of flags user used plus the bonus if the user won the game
        /// </summary>
        private void CalculateScore()
        {
            int bonus = 0;
            if (totalEmptyTiles == openedTiles) bonus = 500;

            if (difficulty.Equals("easy")) lblScore.Text = $"Score: {-ticks * 0.1 + openedTiles - numOfFlagsUsed * 0.5 + bonus}";
            else if (difficulty.Equals("normal")) lblScore.Text = $"Score: {-ticks * 0.1 + openedTiles - numOfFlagsUsed * 0.5 + bonus * 4}";
            else lblScore.Text = $"Score: {-ticks * 0.1 + openedTiles - numOfFlagsUsed * 0.2 + bonus * 10}";
        }
        /// <summary>
        /// Disables all of the buttons so the user can't continue playing
        /// Checks if the high score has been changed and saves the new value
        /// </summary>
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

            decimal score = decimal.Parse(lblScore.Text.Split(' ')[1]);
            decimal highScore = decimal.Parse(lblHighScore.Text.Split(' ')[2]);
            if (score > highScore)
            {
                if (difficulty.Equals("easy")) Properties.Settings.Default.easyHighScore = score.ToString();
                else if (difficulty.Equals("normal")) Properties.Settings.Default.normalHighScore = score.ToString();
                else Properties.Settings.Default.hardHighScore = score.ToString();

                Properties.Settings.Default.Save();
            }
        }
        /// <summary>
        /// Deletes all of the buttons so the game can be created again when a difficulty is changed
        /// </summary>
        private void DeleteField()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    field[i, j].Dispose();
                    this.Update();
                }
            }
        }
        /// <summary>
        /// Resets all of the values and generates new mines when the user hits the Restart button
        /// </summary>
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
                    field[i, j].ForeColor = Color.Black;
                    field[i, j].FlatAppearance.BorderColor = Color.Black;
                    this.Update();
                }
            }
            GenerateMines(mines);
        }
        /// <summary>
        /// If the user clicked the restart button then it resets all values
        /// If the user changed the difficulty then it creates a new game 
        /// </summary>
        /// <param name="difficultyChanged"></param>
        private void Restart(bool difficultyChanged)
        {
            ticks = 0;
            lblTime.Text = $"Time: ";
            lblScore.Text = $"Score: 0";
            openedTiles = 0;
            numOfFlagsUsed = 0;
            timer1.Enabled = false;
            Cursor = Cursors.WaitCursor;
            if (difficultyChanged)
            {
                //this.Hide();
                DeleteField();
                CreateField();
            }
            else ClearField();

            totalEmptyTiles = rows * cols - mines;

            if (difficulty.Equals("easy")) lblHighScore.Text = $"High score: {Properties.Settings.Default.easyHighScore}";
            else if (difficulty.Equals("normal")) lblHighScore.Text = $"High score: {Properties.Settings.Default.normalHighScore}";
            else lblHighScore.Text = $"High score: {Properties.Settings.Default.hardHighScore}";
            Cursor = Cursors.Default;
            this.Show();
        }
        /// <summary>
        /// Called when the user clicks the restart button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestart_Click(object sender, EventArgs e)
        {
            Restart(false);
        }
        /// <summary>
        /// Calculates how long the user has been playing
        /// Displays the time spent playing
        /// Checks if the user ran out of time (check the CalculateScore() method) or if the user won the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            ticks++;
            int sec = ticks % 60;
            int min = ticks / 60;
            lblTime.Text = $"Time:   {min:00}:{sec:00}";
            if (openedTiles == totalEmptyTiles)
            {
                GameOver();
                MessageBox.Show("You won!");
            }
        }
        /// <summary>
        /// Called to create a new game when the user changes the difficulty
        /// </summary>
        private void ChangeDifficultyDesign()
        {
            this.Hide();
            Cursor = Cursors.WaitCursor;
            lblTime.Left = 0;
            btnRestart.Left = 0;
            lblScore.Left = 0;
            Restart(true);
        }
        /// <summary>
        /// Called when user selects easy mode
        /// Creates a new game and changes the design of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void easyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            difficulty = "easy";
            easyToolStripMenuItem1.Checked = true;
            normalToolStripMenuItem1.Checked = false;
            hardToolStripMenuItem1.Checked = false;
            ChangeDifficultyDesign();
            lblTime.Left = this.Size.Width - lblTime.Width - 91;
            lblScore.Left = this.Size.Width - lblScore.Width - 92;
        }
        /// <summary>
        /// Called when user selects normal mode
        /// Creates a new game and changes the design of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void normalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            difficulty = "normal";
            easyToolStripMenuItem1.Checked = false;
            normalToolStripMenuItem1.Checked = true;
            hardToolStripMenuItem1.Checked = false;
            ChangeDifficultyDesign();
            lblTime.Left = this.Size.Width - lblTime.Width - 101;
            lblScore.Left = this.Size.Width - lblScore.Width - 102;
        }
        /// <summary>
        /// Called when user selects hard mode
        /// Creates a new game and changes the design of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            difficulty = "hard";
            easyToolStripMenuItem1.Checked = false;
            normalToolStripMenuItem1.Checked = false;
            hardToolStripMenuItem1.Checked = true;
            ChangeDifficultyDesign();
            lblTime.Left = this.Size.Width - lblTime.Width - 121;
            lblScore.Left = this.Size.Width - lblScore.Width - 125;
        }
        /// <summary>
        /// Used to make sure the form's size is as big or small as it needs when changing difficulties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }
    }
}