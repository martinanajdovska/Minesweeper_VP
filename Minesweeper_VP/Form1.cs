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
        int rows = 13;
        int cols = 15;
        int size = 25;
        int mines = 30;

        public Form1()
        {
            InitializeComponent();
            CreateField();
        }

        private void CreateField()
        {
            Button[,] field = new Button[rows, cols];
            int leftStart = 6;
            int topStart = 50;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button newButton = new Button();
                    newButton.Parent = this;
                    newButton.Size = new Size(size, size);
                    newButton.Name = "i" + "," + "j";
                    newButton.Location = new Point(leftStart + j * size, topStart + i * size);
                    newButton.MouseClick += new MouseEventHandler(CheckForMine);
                    field[i, j] = newButton;
                }
            }
        }
        private void CheckForMine(Object sender, MouseEventArgs e)
        {
        }
    }
}
