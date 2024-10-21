using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class Bishop:Figure
    {

        public Bishop(Table tab) { table = tab; }

        public override string name { get; set; }


        public override bool AllowedMove(int xp, int yp, int xk, int yk)
        {
            bool isAllowed = false;
            bool put = OnWay(xp, yp, xk, yk);
            bool allow = Allowed(xp, yp, xk, yk);
            if ( put && allow)
            {
                bool isW = false;
                if (name == "beli") isW = true;
                isAllowed = table.logic.HandleMove(xp, yp, xk, yk, isW);     
            }
                    
            return isAllowed;
        }

        public bool Allowed(int xp, int yp, int xk, int yk)
        {
            if (Math.Abs(xk - xp) == Math.Abs(yk - yp)) return true;
            return false;
        }

        public override bool OnWay(int xp, int yp, int xk, int yk)
        {
            
            bool put = true;
            if (Math.Abs(xk - xp) != Math.Abs(yk - yp)) put = false;
            if (put)
            {
                int poc, pos, min, max, xmin, xmax, kraj;
                if (xk > xp) { xmin = xp; xmax = xk; }
                else { xmin = xk; xmax = xp; }
                if (yk > yp) { min = yp; max = yk; }
                else { min = yk; max = yp; }
                if ((xp - xk) * (yp - yk) >= 0) { pos = min + 1; poc = xmin + 1; kraj = xmax; }
                else { pos = max - 1; poc = xmin + 1; kraj = xmax; }
                for (int i = poc; i < kraj; i++)
                {
                    if (table.matN[pos][i] != ' ')
                    {
                        put = false;
                        break;
                    }
                    if ((xp - xk) * (yp - yk) >= 0) { pos++; }
                    else pos--;
                }
            }
            return put;
        }


        public override void Covers(int xp, int yp, out int xk, out int yk, out int r, string str)
        {
            int[] savex = new int[] { 0, 0, 0,0};
            int[] savey = new int[] { 0,0, 0, 0};
            int[] arrx = new int[] { -1, 1, 1, -1 };
            int[] arry = new int[] { 1, 1, -1, -1 };
            int[] next = new int[] { 1, 1, 1, 1 };
            int[] res = new int[] { -1, -1, -1, -1 };
            yk = yp; xk = xp; r = -1;
            bool isNext = true;
            while (isNext)
            {
                isNext = false;
                for (int i = 0; i < 4; i++)
                {
                    int xx = xp + arrx[i] * next[i];
                    int yy = yp + arry[i] * next[i];
                    if (xx >= 0 && xx < 8 && yy < 8 && yy >= 0 && next[i] > 0)
                    {
                        next[i]++;
                        isNext = true;
                        // if (table.mat[yy][xx] == null)
                        if (table.matN[yy][xx] == ' ')
                        {
                            savex[i] = xx;
                            savey[i] = yy;
                            res[i] = 0;
                            if (yy > 1 && yy < 5) res[i] += 2;
                        }
                        else
                         if (table.matN[yy][xx] != ' ' && table.matN[yy][xx] != table.matN[yp][xp])
                        {
                            res[i] = table.matW[yy][xx];
                            if ((yy > 2 && yy < 5) || table.matW[yy][xx] > 2) res[i] += 2;
                            savex[i] = xx;
                            savey[i] = yy;
                            next[i] = 0;
                            if (table.matW[yy][xx] == 10)
                            {              
                                table.logic.CallChess(xx, yy, xp, yp);
                                if (table.matN[yy][xx] =='w')
                                    table.logic.chessW = true;
                                else
                                    table.logic.chessB = true;
                            }
                        }
                        else
                            next[i] = 0;
                    }
                    else
                    {
                        next[i] = 0;
                    }
                }
            }
            int max = -1;
            for (int i = 0; i < 4; i++)
            {
                if (res[i] > max)
                {
                    max = res[i];
                    yk = savey[i];
                    xk = savex[i];
                }
            }
            r = max;
        }  

    }
}
