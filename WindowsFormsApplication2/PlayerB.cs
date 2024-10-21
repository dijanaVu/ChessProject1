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
    class PlayerB:Player
    {
        public Table tabla;  
        public Graphics g;
        public System.Drawing.Bitmap bitmap1;


        public PlayerB(Table tab, Graphics gg)
        {
            tabla = tab;
            g = gg;
            ArrangeFigures();
            DrawFig();
        }
     

        public override void ArrangeFigures()
        {
            string exPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            for (int i = 0; i < 1; i++)
            {
                tabla.mat[7][0] = new Rook(tabla);
                tabla.mat[7][0].path = Path.Combine(exPath ,"NewFolder1\\Crni_top.jpg");
                tabla.mat[7][0].x = 2;
                tabla.mat[7][0].y = 7 * tabla.h / 8 + 2;
                tabla.mat[7][0].name = "crni";
                tabla.mat[7][0].weight = 4;
                list.Add(tabla.mat[7][0]);
                tabla.matN[7][0] = 'b';
                tabla.matW[7][0] = 4;

                tabla.mat[7][7] = new Rook(tabla);
                tabla.mat[7][7].path = Path.Combine(exPath, "NewFolder1\\Crni_top.jpg");
                tabla.mat[7][7].x = 7 * tabla.w / 8 + 2;
                tabla.mat[7][7].y = 7 * tabla.h / 8 + 2;
                tabla.mat[7][7].name = "crni";
                tabla.mat[7][7].weight = 4;
                list.Add(tabla.mat[7][7]);
                tabla.matN[7][7] = 'b';
                tabla.matW[7][7] = 4;
            }
            for (int i = 0; i < 1; i++)
            {
                tabla.mat[7][1] = new Knight(tabla);
                tabla.mat[7][1].path = Path.Combine(exPath ,"NewFolder1\\Crni_konj.jpg");
                tabla.mat[7][1].x = 2+ tabla.w / 8;
                tabla.mat[7][1].y = 7 * tabla.h / 8 + 2;
                tabla.mat[7][1].name = "crni";
                tabla.mat[7][1].weight = 4;
                list.Add(tabla.mat[7][1]);
                tabla.matN[7][1] = 'b';
                tabla.matW[7][1] = 4;

                tabla.mat[7][6] = new Knight(tabla);
                tabla.mat[7][6].path = Path.Combine(exPath , "NewFolder1\\Crni_konj.jpg");
                tabla.mat[7][6].x = 6 * tabla.w / 8 + 2;
                tabla.mat[7][6].y = 7 * tabla.h / 8 + 2;
                tabla.mat[7][6].name = "crni";
                tabla.mat[7][6].weight = 4;
                list.Add(tabla.mat[7][6]);
                tabla.matN[7][6] = 'b';
                tabla.matW[7][6] = 4;

            }
            for (int i = 0; i < 1; i++)
            {
                tabla.mat[7][2] = new Bishop(tabla);
                tabla.mat[7][2].path = Path.Combine(exPath , "NewFolder1\\Crni_lovac.jpg");
                tabla.mat[7][2].x = 2 + 2*tabla.w / 8;
                tabla.mat[7][2].y = 7 * tabla.h / 8 + 2;
                tabla.mat[7][2].name = "crni";
                tabla.mat[7][2].weight = 4;
                list.Add(tabla.mat[7][2]);
      
                tabla.matN[7][2] = 'b';
                tabla.matW[7][2] = 4;
             
                tabla.mat[7][5] = new Bishop(tabla);
                tabla.mat[7][5].path = Path.Combine(exPath ,"NewFolder1\\Crni_lovac.jpg");
                tabla.mat[7][5].x = 5 * tabla.w / 8 + 2;
                tabla.mat[7][5].y = 7 * tabla.h / 8 + 2;
                tabla.mat[7][5].name = "crni";
                tabla.mat[7][5].weight = 4;
                list.Add(tabla.mat[7][5]);
                tabla.matN[7][5] = 'b';
                tabla.matW[7][5] = 4;
            
            }
            {
                tabla.mat[7][3] = new Queen(tabla);
                tabla.mat[7][3].path = Path.Combine(exPath , "NewFolder1\\Crna_kraljica.jpg");
                tabla.mat[7][3].x = 2 + 3 * tabla.w / 8;
                tabla.mat[7][3].y = 7 * tabla.h / 8 + 2;
                tabla.mat[7][3].name = "crni";
                tabla.mat[7][3].weight = 8;
                list.Add(tabla.mat[7][3]);
          
                tabla.matN[7][3] = 'b';
                tabla.matW[7][3] = 8;
               
            }
            {
                tabla.mat[7][4] = new King(tabla);
                tabla.mat[7][4].path = Path.Combine(exPath , "NewFolder1\\Crni_kralj.jpg");
                tabla.mat[7][4].x = 2 + 4 * tabla.w / 8;
                tabla.mat[7][4].y = 7 * tabla.h / 8 + 2;
                tabla.mat[7][4].name = "crni";
                tabla.mat[7][4].weight = 10;
                list.Add(tabla.mat[7][4]);
               
                tabla.matN[7][4] = 'b';
                tabla.matW[7][4] = 10;
              
            }
            for (int i = 0; i < 8; i++)
            {
                tabla.mat[6][i] = new Pawn(tabla);
                tabla.mat[6][i].path = Path.Combine(exPath , "NewFolder1\\Crni_pion.jpg");
                tabla.mat[6][i].x = i * tabla.w / 8 + 2;
                tabla.mat[6][i].y = 6 * tabla.h / 8 + 2;
                tabla.mat[6][i].name = "crni";
                tabla.mat[6][i].weight = 2;
                list.Add(tabla.mat[6][i]);
    
                tabla.matN[6][i] = 'b';
                tabla.matW[6][i] = 2;
               
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
                if (tabla.logic != null && tabla.logic.chessB && tabla.matN[tabla.logic.yk][tabla.logic.xk] != ' ')
                {
                    Font font = new Font(FontFamily.GenericSansSerif, 10);
                    g.DrawString("CHESS!!!", font, Brushes.Red, tabla.logic.xk * tabla.w / 8 + 2,
                       tabla.logic.yk * tabla.h / 8 + 2);
                }
            }
        }

    }
}
