using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication2
{
    class Table
    {
        public System.Drawing.Bitmap bitmap;
       
        public int h, w;
        public Figure[][] mat = new Figure[8][];
        public char[][] matN = new char[8][];
        public int[][] matW = new int[8][];
        public Player playerW,playerB;
        public Graphics g;
        public Logic logic;
        public delegate void InvokeDelegat();
   //     public event EventHandler MyEvent;


        public Table(int hh,int vv,Graphics gg) {
            h = hh;
            w = vv;
            g = gg;
            for (int i = 0; i < matN.Length; i++)
            {
                mat[i] = new Figure[8];
                matN[i] = new char[] {' ',' ', ' ', ' ', ' ', ' ', ' ', ' ' };
                matW[i] = new int[8];
            }

            bitmap = new Bitmap(h, w);
            draw();
            playerW = new PlayerW(this,g);
            playerB = new PlayerB(this, g);
            logic = new Logic(this);
           
        }


        public void handle_end() {
            Form1.btn.BeginInvoke(new InvokeDelegat(Form1.btn.PerformClick));
        //   Form1.btn.PerformClick();
        }

        public void draw() {
            bool isGray = true;        
            for (int i = 0; i < h; i+=(h)/8) {
                for (int j = 0; j < w; j += (w) / 8)
                {
                    if (!isGray)
                    {
                        for (int m = i; m <i+ (h ) / 8; m++)
                        {
                            for (int n = j; n < j + (w ) / 8; n++)
                            {
                                bitmap.SetPixel(n, m, Color.NavajoWhite);
                            }
                        }
                        isGray = true;
                    }
                    else {
                        for (int m = i; m < i + (h ) / 8; m++)
                        {
                            for (int n = j; n < j + (w ) / 8; n++)
                            {
                                bitmap.SetPixel(n, m, Color.Gray);
                            }
                            
                        }
                        isGray = false;
                    }
                }
                isGray = !isGray;
            }
            
        }
    }
}
