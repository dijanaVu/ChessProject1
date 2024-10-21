using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private static Table tabla;
        public static bool work = false;
        public System.Threading.Thread thread;
        public int currX, currY;
        public static int begX,begY,lastX,lastY;
        public static Bitmap btm;
        public static Label lab;
        public static Button btn;
        public static object this_l = new object();
        private static Graphics g;



        public Form1()
        {
            InitializeComponent();
           
            thread = new System.Threading.Thread(ThreadRun);
            lab = new Label();
            lab.Location = new Point(10,10);
            this.Controls.Add(lab);

            btn = new Button();
            btn.Location = new Point(150,10);
            btn.Size = new Size(100, 20);
            btn.Text = "End button";
            btn.Click += new EventHandler(OnClick);
            this.Controls.Add(btn);
            btn.Enabled = false;

        }

        private void OnClick(object sender,EventArgs e) {
            try
            {
                work = false;
                label1.Text = "The End ! ! !";
                thread.Abort();
                btn.Enabled = false;
            }
            catch (Exception ex) { label1.Text = ex.Message; }
        }

        public  void callClick() {
            OnClick(this,EventArgs.Empty);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                work = false;
                label1.Text = "The End ! ! !";
                thread.Abort();
                btn.Enabled = false;
            }
            catch (Exception ex) { label1.Text = ex.Message; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                g = pictureBox1.CreateGraphics();
                tabla = new Table(pictureBox1.Height, pictureBox1.Width, g);        
                work = true;
                pictureBox1.Image = tabla.bitmap;
                thread.Start();
                button1.Enabled = false;
                btn.Enabled = true;
             //   btm = new Bitmap(pictureBox1.ClientSize.Height, pictureBox1.ClientSize.Width);
             //   pictureBox1.DrawToBitmap(btm, ClientRectangle);
              //  btm.Save("C:\\Users\\dixiv\\Desktop\\vs folder\\New folder\\tabla.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex) { label1.Text = ex.Message; }
        }


        public void ThreadRun() {
             while (work) {
               // lock (this_l)
                {
                    // pictureBox1.Image =tabla.bitmap;
                    tabla.playerW.DrawFig();
                    tabla.playerB.DrawFig();
                    System.Threading.Thread.Sleep(20);
                }
            }
        }


        private void OnDown(object sender, MouseEventArgs e)
        {
            currX = e.X/(tabla.w/8);
            currY = e.Y/(tabla.h/8);
        }


        private void OnUp(object sender, MouseEventArgs e)
        {
            int x = e.X / (tabla.w / 8);
            int y = e.Y / (tabla.h / 8);

            if (x >= 0 && currX >= 0 && currY >= 0 && y >= 0 && x < 8 && y < 8 && currY < 8 && y < 8 && tabla.matN[currY][currX] != ' ')
            {
                label1.Text = " ";
                PlayerW pl = tabla.playerW as PlayerW;
                int ind = 0;
                for (int i = 0; i < pl.list.Count; i++)
                {
                    if ((pl.list[i].x - 2) / (tabla.w / 8) == currX && (pl.list[i].y - 2) / (tabla.h / 8) == currY)
                    {
                        ind = i;
                        break;
                    }
                }
                bool poz = tabla.playerW.list[ind].AllowedMove(currX, currY, x, y);
                if (poz == false) { label1.Text = "Move not allowed"; }
                else
                {
                    pictureBox1.Image = tabla.bitmap;
                    tabla.logic.chessW = false;
                    label1.Text = "White player move";
                    // black player move starts here
                    tabla.logic.Decision();
                    tabla.playerW.turn = !tabla.playerW.turn;
                    if (tabla.playerW.turn) {
                        PlayerB plb = tabla.playerB as PlayerB;
                        ind = 0;
                        for (int i = 0; i < plb.list.Count; i++)
                        {
                            if ((plb.list[i].x - 2) / (tabla.w / 8) == begX && (plb.list[i].y - 2) / (tabla.h / 8) == begY)
                            {
                                ind = i;
                                break;
                            }
                        }
                        tabla.logic.BlackPlayerMove(begX,begY,lastX,lastY,ind);
                        tabla.playerW.turn = !tabla.playerW.turn;
                    } 
                }             
            }
            else
            {
                label1.Text = "Out of table";
            }
        }

        
    }
}
