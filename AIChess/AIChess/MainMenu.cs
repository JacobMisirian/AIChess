using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AIChess
{
    public partial class MainMenu : Form
    {
        private Difficulty difficulty;
        public MainMenu()
        {
            InitializeComponent();
            this.difficulty = Difficulty.EASY;
            difficultyBox.Load("../../Difficulty_Easy.png");
        }

        private void newGameBox_MouseDown(object sender, MouseEventArgs e)
        {
            newGameBox.Load("../../New_Game_OnClick.png");
        }

        private void newGameBox_MouseUp(object sender, MouseEventArgs e)
        {
            newGameBox.Load("../../New_Game.png");
            Board board = new Board();
            board.ShowDialog();
        }

        private void githubBox_MouseUp(object sender, MouseEventArgs e) {
            Process.Start("https://github.com/JacobMisirian/AIChess");
        }

        private void difficultyBox_MouseDown(object sender, MouseEventArgs e)
        {
            switch(this.difficulty)
            {
                case Difficulty.EASY:
                    difficultyBox.Load("../../Difficulty_Easy_OnClick.png");
                    break;
                case Difficulty.MEDIUM:
                    difficultyBox.Load("../../Difficulty_Medium_OnClick.png");
                    break;
                case Difficulty.HARD:
                    difficultyBox.Load("../../Difficulty_Hard_OnClick.png");
                    break;
            }
        }

        private void difficultyBox_MouseUp(object sender, MouseEventArgs e)
        {
            switch (this.difficulty)
            {
                case Difficulty.EASY:
                    difficultyBox.Load("../../Difficulty_Medium.png");
                    this.difficulty = Difficulty.MEDIUM;
                    break;
                case Difficulty.MEDIUM:
                    difficultyBox.Load("../../Difficulty_Hard.png");
                    this.difficulty = Difficulty.HARD;
                    break;
                case Difficulty.HARD:
                    difficultyBox.Load("../../Difficulty_Easy.png");
                    this.difficulty = Difficulty.EASY;
                    break;
            }
        }

        private void exitBox_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
