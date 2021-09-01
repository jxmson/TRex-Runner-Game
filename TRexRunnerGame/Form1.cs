using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRexRunnerGame
{
    public partial class Form1 : Form
    {
        bool jump = false;
        int jumpSpeed;
        int force = 12; //Max height hat player can jump
        int score = 0;
        int obstacleSpeed = 10;
        Random r = new Random();
        int position;
        bool gameOver = false;
        public Form1()
        {
            InitializeComponent();
            Reset();
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            pctTRex.Top += jumpSpeed;
            lblScore.Text = "Score: " + score;

            if (jump == true && force < 0)
                jump = false;

            if (jump == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
                jumpSpeed = 12;

            if (pctTRex.Top > 356 && jump == false)
            {
                force = 12;
                pctTRex.Top = 356;
                jumpSpeed = 0;
            }
            foreach(Control c in this.Controls)
            {
                if (c is PictureBox && (string)c.Tag == "obstacle")
                {
                    c.Left -= obstacleSpeed;
                    if (c.Left < -100)
                    {
                        c.Left = this.ClientSize.Width + r.Next(200, 500) + (c.Width * 15);
                        score++;
                    }
                    //Collision control
                    if(pctTRex.Bounds.IntersectsWith(c.Bounds))
                    {
                        tmrTimer.Stop();
                        pctTRex.Image = Properties.Resources.dead;
                        lblScore.Text += " Press R to restart the game.";
                        gameOver = true;
                    }
                }
            }

            if (score > 10)
                obstacleSpeed = 15; 
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && jump == false)
                jump = true;

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if(jump == true)
                jump = false;

            if (e.KeyCode == Keys.R && gameOver == true)
                Reset();
        }
        private void Reset()
        {
            pctTRex.Image = Properties.Resources.running;
            force = 12;
            jumpSpeed = 0;
            jump = false;
            score = 0;
            lblScore.Text = "Score: " +  score;
            gameOver = false;
            pctTRex.Top = 357;        

            foreach(Control c in this.Controls)
            {
                if(c is PictureBox && (string)c.Tag == "obstacle")
                {
                    position = this.ClientSize.Width + r.Next(500, 800) + (c.Width * 10);
                    c.Left = position;
                }
            }
            tmrTimer.Start();
        }
        

    }
}
