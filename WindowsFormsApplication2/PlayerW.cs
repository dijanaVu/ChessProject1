using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class PlayerW:Player
    {
        public Table tabla;
        public Graphics g;
        public System.Drawing.Bitmap bitmap1;

        public PlayerW(Table tab, Graphics gg) {
            tabla = tab;
            g = gg;
            ArrangeFigures();
            DrawFig();
        }

        public override void ArrangeFigures()
        {
            string exPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            for (int i = 0; i < 1; i++) {
                tabla.mat[0][0] = new Rook(tabla);
                tabla.mat[0][0].path = Path.Combine(exPath, "NewFolder1\\Beli_top.jpg");
                tabla.mat[0][0].x =  2;
                tabla.mat[0][0].y =  2;
                tabla.mat[0][0].name = "beli";
                tabla.mat[0][0].weight= 4;
                list.Add(tabla.mat[0][0]);
                tabla.matN[0][0] = 'w';
                tabla.matW[0][0] = 4;
     

                tabla.mat[0][7] = new Rook(tabla);
                tabla.mat[0][7].path = Path.Combine(exPath,"NewFolder1\\Beli_top.jpg");
                tabla.mat[0][7].x = 7 * tabla.w / 8 + 2;
                tabla.mat[0][7].y = 2;
                tabla.mat[0][7].name = "beli";
                tabla.mat[0][7].weight = 4;
                list.Add(tabla.mat[0][7]);
                tabla.matN[0][7] = 'w';
                tabla.matW[0][7] = 4;
            
            }

            for (int i = 0; i < 1; i++)
            {
                tabla.mat[0][1] = new Knight(tabla);
                tabla.mat[0][1].path = Path.Combine(exPath, "NewFolder1\\Beli_konj.jpg");
                tabla.mat[0][1].x = tabla.w / 8 + 2;
                tabla.mat[0][1].y = 2;
                tabla.mat[0][1].name = "beli";
                tabla.mat[0][1].weight = 4;
                list.Add(tabla.mat[0][1]);
                tabla.matN[0][1] = 'w';
                tabla.matW[0][1] = 4;
            
                tabla.mat[0][6] = new Knight(tabla);
                tabla.mat[0][6].path = Path.Combine(exPath ,"NewFolder1\\Beli_konj.jpg");
                tabla.mat[0][6].x = 6 * tabla.w / 8 + 2;
                tabla.mat[0][6].y = 2;
                tabla.mat[0][6].name = "beli";
                tabla.mat[0][6].weight = 4;
                list.Add(tabla.mat[0][6]);
                tabla.matN[0][6] = 'w';
                tabla.matW[0][6] = 4;
            }
            for (int i = 0; i < 1; i++)
            {
                tabla.mat[0][2] = new Bishop(tabla);
                tabla.mat[0][2].path = Path.Combine(exPath, "NewFolder1\\Beli_lovac.jpg");
                tabla.mat[0][2].x = 2*tabla.w / 8 + 2;
                tabla.mat[0][2].y = 2;
                tabla.mat[0][2].name = "beli";
                tabla.mat[0][2].weight = 4;
                list.Add(tabla.mat[0][2]);
                tabla.matN[0][2] = 'w';
                tabla.matW[0][2] = 4;
 
                tabla.mat[0][5] = new Bishop(tabla);
                tabla.mat[0][5].path = Path.Combine(exPath,"NewFolder1\\Beli_lovac.jpg");
                tabla.mat[0][5].x = 5 * tabla.w / 8 + 2;
                tabla.mat[0][5].y = 2;
                tabla.mat[0][5].name = "beli";
                tabla.mat[0][5].weight = 4;
                list.Add(tabla.mat[0][5]);
                tabla.matN[0][5] = 'w';
                tabla.matW[0][5] = 4;
                
            }
            {
                tabla.mat[0][3] = new Queen(tabla);
                tabla.mat[0][3].path = Path.Combine( exPath,"NewFolder1\\Bela_kraljica.jpg");
                tabla.mat[0][3].x = 3 * tabla.w / 8 + 2;
                tabla.mat[0][3].y = 2;
                tabla.mat[0][3].name = "beli";
                tabla.mat[0][3].weight = 8;
                list.Add(tabla.mat[0][3]);
                tabla.matN[0][3] = 'w';
                tabla.matW[0][3] = 8;
              
            }
            {
                tabla.mat[0][4] = new King(tabla);
                tabla.mat[0][4].path = Path.Combine(exPath ,"NewFolder1\\Beli_kralj.jpg");
                tabla.mat[0][4].x = 4 * tabla.w / 8 + 2;
                tabla.mat[0][4].y = 2;
                tabla.mat[0][4].name = "beli";
                tabla.mat[0][4].weight = 8;
                list.Add(tabla.mat[0][4]);
                tabla.matN[0][4] = 'w';
                tabla.matW[0][4] = 10;
               
            }
            for (int i = 0; i < 8; i++)
            {
                tabla.mat[1][i] = new Pawn(tabla);
                tabla.mat[1][i].path = Path.Combine(exPath, "NewFolder1\\Beli_pion.jpg");
                tabla.mat[1][i].x = i * tabla.w / 8 + 2;
                tabla.mat[1][i].y = tabla.h / 8 + 2;
                tabla.mat[1][i].name = "beli";
                tabla.mat[1][i].weight = 2;
                list.Add(tabla.mat[1][i]);
                tabla.matN[1][i] = 'w';
                tabla.matW[1][i] = 2;
               
            }
        }

        public override void DrawFig()
        {
            lock (list)
            {
               
                for (int i = 0; i < list.Count; i++)
                {
                    bitmap1 = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(list[i].path);
                    g.DrawImage(bitmap1, list[i].x, list[i].y, 25, 25);
                }

                if (tabla.logic != null && tabla.logic.chessW && tabla.matN[tabla.logic.yk][tabla.logic.xk] != ' ')
                {
                    Font font = new Font(FontFamily.GenericSansSerif, 10);
                    g.DrawString("CHESS!!!", font, Brushes.Red, tabla.logic.xk * tabla.w / 8 + 2,
                       tabla.logic.yk * tabla.h / 8 + 2);
                }
            }
        }

   
    }
}
