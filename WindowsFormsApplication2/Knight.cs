using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class Knight:Figure
    {
       
        public Knight(Table tab) { table = tab; }

        public override string name { get; set; }


        public override bool AllowedMove(int xp, int yp, int xk, int yk)
        {
            bool isAllowed = false;
            bool put = OnWay(xp, yp, xk, yk);
            if ( put)
            {
                bool isW = false;
                if (name == "beli") isW = true;
                isAllowed = table.logic.HandleMove(xp, yp, xk, yk, isW);            
            }
           
            return isAllowed;
        }


        public override bool OnWay(int xp, int yp, int xk, int yk)
        {
     
            int[] nizx = new int[] { xp-1,xp-1,xp-2,xp-2,xp+1,xp+1,xp+2,xp+2 };
            int[] nizy = new int[] { yp-2,yp+2,yp-1,yp+1,yp+2,yp-2,yp-1,yp+1 };
            for (int i = 0; i < nizx.Length; i++) {
                if (nizx[i] == xk && nizy[i] == yk && xk >= 0 && yk >= 0 && yk < 8 && xk < 8)
                {
                    return true;
                }
            }
            return false;
        }
     

        public override void Covers(int xp, int yp,out int xk,out int yk,out int r, string str) {
            int[] nizx = new int[] { xp - 1, xp - 1, xp - 2, xp - 2, xp + 1, xp + 1, xp + 2, xp + 2 };
            int[] nizy = new int[] { yp - 2, yp + 2, yp - 1, yp + 1, yp + 2, yp - 2, yp - 1, yp + 1 };
            int[] res = new int[] { -1, -1, -1, -1, -1, -1, -1, -1 };
            xk = xp;
            yk = yp;
            r = -1;
            for (int i = 0; i < nizx.Length; i++)
            {
                if (nizx[i]>=0 && nizy[i] >=0 && nizx[i]<8 && nizy[i]<8)
                {

                    if (table.matN[nizy[i]][nizx[i]] == ' ')
                    {
                        res[i] = 0;
                        if (nizy[i] > 2 && nizy[i] < 5) res[i] += 2;
                        else
                           if (nizy[i] > 1 && nizy[i] < 6) res[i] += 1;
                    }
                    else
                        if (table.matN[nizy[i]][nizx[i]] != ' ' && table.matN[nizy[i]][nizx[i]] != table.matN[yp][xp])
                    {
 
                        res[i] = table.matW[nizy[i]][nizx[i]];
                        if ((nizy[i] > 2 && nizy[i] < 5) || table.matW[nizy[i]][nizx[i]] > 2) res[i] += 2;
                        else
                        if (nizy[i] > 1 && nizy[i] < 6) res[i] += 1;
 
                        if (table.matW[nizy[i]][nizx[i]] == 10)
                        {
   
                            table.logic.CallChess(nizx[i], nizy[i], xp, yp);
                            if (table.matN[nizy[i]][nizx[i]] == 'w')
                                table.logic.chessW = true;
                            else
                                table.logic.chessB = true;
                        }

                    }                   
                }
            }

            int max = -1;
            for (int i = 0; i < 8; i++)
            {
                if (res[i] > max)
                {
                    max = res[i];
                    yk = nizy[i];
                    xk = nizx[i];
                }
            }
            r = max;
        }

      

    }
}
