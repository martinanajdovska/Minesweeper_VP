namespace Minesweeper_VP
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnRestart = new System.Windows.Forms.Button();
            this.difficultyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.difficultyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.easyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.normalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hardToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lblHighScore = new System.Windows.Forms.Label();
            this.lblHighScoreValue = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblScoreValue = new System.Windows.Forms.Label();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Noto Sans Cond", 13.8F, System.Drawing.FontStyle.Bold);
            this.lblTime.Location = new System.Drawing.Point(354, 37);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(67, 32);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "Time:";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnRestart
            // 
            this.btnRestart.BackgroundImage = global::Minesweeper_VP.Properties.Resources.restart;
            this.btnRestart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRestart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestart.Location = new System.Drawing.Point(240, 12);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(39, 40);
            this.btnRestart.TabIndex = 0;
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // difficultyToolStripMenuItem
            // 
            this.difficultyToolStripMenuItem.Name = "difficultyToolStripMenuItem";
            this.difficultyToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.difficultyToolStripMenuItem.Text = "Difficulty";
            // 
            // easyToolStripMenuItem
            // 
            this.easyToolStripMenuItem.Name = "easyToolStripMenuItem";
            this.easyToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.easyToolStripMenuItem.Text = "Easy";
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.normalToolStripMenuItem.Text = "Normal";
            // 
            // hardToolStripMenuItem
            // 
            this.hardToolStripMenuItem.Name = "hardToolStripMenuItem";
            this.hardToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.hardToolStripMenuItem.Text = "Hard";
            // 
            // menuStrip2
            // 
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.difficultyToolStripMenuItem1});
            this.menuStrip2.Location = new System.Drawing.Point(0, 37);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(184, 40);
            this.menuStrip2.TabIndex = 3;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // difficultyToolStripMenuItem1
            // 
            this.difficultyToolStripMenuItem1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.difficultyToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.easyToolStripMenuItem1,
            this.normalToolStripMenuItem1,
            this.hardToolStripMenuItem1});
            this.difficultyToolStripMenuItem1.Font = new System.Drawing.Font("Noto Sans Cond", 13.8F, System.Drawing.FontStyle.Bold);
            this.difficultyToolStripMenuItem1.Name = "difficultyToolStripMenuItem1";
            this.difficultyToolStripMenuItem1.Size = new System.Drawing.Size(176, 36);
            this.difficultyToolStripMenuItem1.Text = "Select difficulty";
            // 
            // easyToolStripMenuItem1
            // 
            this.easyToolStripMenuItem1.Checked = true;
            this.easyToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.easyToolStripMenuItem1.Font = new System.Drawing.Font("Noto Sans Cond", 11.8F, System.Drawing.FontStyle.Bold);
            this.easyToolStripMenuItem1.Name = "easyToolStripMenuItem1";
            this.easyToolStripMenuItem1.Size = new System.Drawing.Size(224, 32);
            this.easyToolStripMenuItem1.Text = "Easy";
            this.easyToolStripMenuItem1.Click += new System.EventHandler(this.easyToolStripMenuItem1_Click);
            // 
            // normalToolStripMenuItem1
            // 
            this.normalToolStripMenuItem1.Font = new System.Drawing.Font("Noto Sans Cond", 11.8F, System.Drawing.FontStyle.Bold);
            this.normalToolStripMenuItem1.Name = "normalToolStripMenuItem1";
            this.normalToolStripMenuItem1.Size = new System.Drawing.Size(224, 32);
            this.normalToolStripMenuItem1.Text = "Normal";
            this.normalToolStripMenuItem1.Click += new System.EventHandler(this.normalToolStripMenuItem1_Click);
            // 
            // hardToolStripMenuItem1
            // 
            this.hardToolStripMenuItem1.Font = new System.Drawing.Font("Noto Sans Cond", 11.8F, System.Drawing.FontStyle.Bold);
            this.hardToolStripMenuItem1.Name = "hardToolStripMenuItem1";
            this.hardToolStripMenuItem1.Size = new System.Drawing.Size(224, 32);
            this.hardToolStripMenuItem1.Text = "Hard";
            this.hardToolStripMenuItem1.Click += new System.EventHandler(this.hardToolStripMenuItem1_Click);
            // 
            // lblHighScore
            // 
            this.lblHighScore.AutoSize = true;
            this.lblHighScore.Font = new System.Drawing.Font("Noto Sans Cond", 13.8F, System.Drawing.FontStyle.Bold);
            this.lblHighScore.Location = new System.Drawing.Point(12, 5);
            this.lblHighScore.Name = "lblHighScore";
            this.lblHighScore.Size = new System.Drawing.Size(122, 32);
            this.lblHighScore.TabIndex = 4;
            this.lblHighScore.Text = "High score:";
            // 
            // lblHighScoreValue
            // 
            this.lblHighScoreValue.AutoSize = true;
            this.lblHighScoreValue.Font = new System.Drawing.Font("Noto Sans Cond", 13.8F, System.Drawing.FontStyle.Bold);
            this.lblHighScoreValue.Location = new System.Drawing.Point(140, 5);
            this.lblHighScoreValue.Name = "lblHighScoreValue";
            this.lblHighScoreValue.Size = new System.Drawing.Size(26, 32);
            this.lblHighScoreValue.TabIndex = 5;
            this.lblHighScoreValue.Text = "0";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Noto Sans Cond", 13.8F, System.Drawing.FontStyle.Bold);
            this.lblScore.Location = new System.Drawing.Point(348, 5);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(73, 32);
            this.lblScore.TabIndex = 6;
            this.lblScore.Text = "Score:";
            // 
            // lblScoreValue
            // 
            this.lblScoreValue.AutoSize = true;
            this.lblScoreValue.Font = new System.Drawing.Font("Noto Sans Cond", 13.8F, System.Drawing.FontStyle.Bold);
            this.lblScoreValue.Location = new System.Drawing.Point(418, 5);
            this.lblScoreValue.Name = "lblScoreValue";
            this.lblScoreValue.Size = new System.Drawing.Size(26, 32);
            this.lblScoreValue.TabIndex = 7;
            this.lblScoreValue.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(516, 480);
            this.Controls.Add(this.lblScoreValue);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblHighScoreValue);
            this.Controls.Add(this.lblHighScore);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.lblTime);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Minesweeper";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem difficultyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hardToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem difficultyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem easyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hardToolStripMenuItem1;
        private System.Windows.Forms.Label lblHighScore;
        private System.Windows.Forms.Label lblHighScoreValue;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblScoreValue;
    }
}

