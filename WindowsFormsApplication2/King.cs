using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class King:Figure
    {
        public King(Table tab)
        { table = tab; }

        public override string name { get; set; }


        public override bool AllowedMove(int xp, int yp, int xk, int yk)
        {
            bool isAllowed = false; 
            bool allow = OnWay(xp, yp, xk, yk);
            if ( allow)
            {
                bool isW = false;
                if (name == "beli") isW = true;
                isAllowed = table.logic.HandleMove(xp, yp, xk, yk, isW);
            }
          
            return isAllowed;
        }


        public override bool OnWay(int xp, int yp, int xk, int yk)
        {
            bool put = false;
            if (Math.Abs(xk - xp) ==1&& Math.Abs(yk - yp)==1) put = true;
            if ((xp == xk&& Math.Abs(yk - yp) == 1) || (yp == yk&& Math.Abs(xk - xp) == 1)) put = true;
            return put;
        }
      

        public override void Covers(int xp, int yp, out int xk, out int yk, out int r, string str)
        {
            int[] nizx = new int[] { xp - 1, xp - 1, xp - 1,  xp, xp , xp + 1, xp + 1, xp + 1 };
            int[] nizy = new int[] { yp - 1, yp + 1, yp,  yp + 1, yp -1, yp +1, yp, yp - 1 };
            xk = xp;yk = yp; r = -1;
            for (int i = 0; i < nizx.Length; i++)
            {
                if (nizx[i] >= 0 && nizy[i] >= 0 && nizx[i] < 8 && nizy[i] < 8)
                {
                    if (table.matN[nizy[i]][nizx[i]] == ' ')
                    {
                        r = -1;
                        yk = nizy[i]; xk = nizx[i];
                    }
                    else {
                        if (table.matN[nizy[i]][nizx[i]] != ' ' && table.matN[nizy[i]][nizx[i]] != table.matN[yp][xp])
                        {
                            yk = nizy[i]; xk = nizx[i];
                            if(nizy[i]>5)
                            r = table.matW[nizy[i]][nizx[i]];
                        }
                    }
                }
            }
        }
    

    }
}
