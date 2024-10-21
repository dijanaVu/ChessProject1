using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class Pawn:Figure
    {
        private bool prvi = true;

        public Pawn(Table tab) { table = tab; }

        public override string name { get; set; }
      

        public override bool AllowedMove(int xp,int yp,int xk,int yk)
        {
            bool isAllowed = false;
            bool put = OnWay(xp, yp, xk, yk);
            if ( put) {
                bool isW = false;
                if (name == "beli") isW = true;
                isAllowed = table.logic.HandleMove(xp, yp, xk, yk, isW);
            } 
           
            return isAllowed;
        }

        public override bool OnWay(int xp, int yp, int xk, int yk) {
            bool put = false;
            if (table.matN[yp][xp] == 'b')
            {
                if (yk<8 && xp == xk && yp == yk + 1 && table.matN[yk][xk]==' ')
                {
                    prvi = false;
                    put = true;
                }
                else
                {
                    if (yk < 8 && (xk >= 0 && xp - 1 == xk && yp == yk + 1) || (xk < 8 && xp + 1 == xk && yp == yk + 1) &&
                        table.matN[yk][xk] != ' ' && table.matN[yk][xk] != ' ')
                    {
                        prvi = false;
                        put = true;
                    }
                    else
                        if (yk < 8 && prvi && xp == xk && yp == yk + 2 && (table.matN[yk][xk] == ' ' ||
                        table.matN[yk][xk] != 'b') && table.matN[yk + 1][xk] == ' ')
                    {
                        prvi = false;
                        put = true;
                    }
                }
            }
            else {
                if (yk>=0 && xp == xk && yk == yp + 1 && (table.matN[yk][xk] == ' '))
                {
                    if (!table.logic.isChessCheck)
                    {
                        prvi = false;
                        put = true;
                    }
                }
                else
                {
                    if (yk >=0 && (xk >= 0 && xp - 1 == xk && yk == yp + 1) || (xk < 8 && xp + 1 == xk && yk == yp + 1) &&
                       (( table.matN[yk][xk] != ' ' && table.matN[yk][xk] != 'w')|| table.logic.isChessCheck))
                    {
                        if(!table.logic.isChessCheck)
                            prvi = false;
                        put = true;
                    }
                    else
                        if (yk>=0 && prvi && xp == xk && yk == yp + 2 && (table.matN[yk][xk] == ' ' ||
                        table.matN[yk][xk]!= 'w') && table.matN[yp + 1][xk] == ' ')
                    {
                        if (!table.logic.isChessCheck)
                            prvi = false;
                        put = true;
                    }
                }
            }
            return put;
        }

       
        public override void Covers(int xp, int yp,out int xk,out int yk,out int rez, string str)
        {
            rez = -1;
            xk = xp;
            yk = yp;
            int max = -1;
            if (table.matN[yp][xp] == 'b')
            {
                if (yp - 1 >= 0)
                {
                    if (table.matN[yp - 1][xp] == ' ')
                    {
                        rez = 0;
                        if (yp - 1 > 2) rez += 2;
                        if (rez > max)
                        {
                            rez = max;
                            yk = yp - 1; xk = xp;
                        }
                    }

                    if (xp - 1 >= 0 &&  (table.matN[yp - 1][xp - 1] != ' ' &&
                        table.matN[yp - 1][xp - 1] == 'w'))
                    {
                        rez = table.matW[yp - 1][xp - 1];
                        if (yp - 1 > 2) rez += 2;
                        if (rez > max)
                        {
                            max=rez;
                            yk = yp - 1; xk = xp - 1;
                        }
                        if (table.matW[yk][xk] == 10)
                        {
                            table.logic.CallChess(xk, yk, xp, yp);
                            if (table.matN[yk][xk] == 'w')
                                table.logic.chessW = true;
                            else
                                table.logic.chessB = true;
                        }
                    }
                    if (xp + 1 < 8 && (table.matN[yp - 1][xp + 1] != ' ' && table.matN[yp - 1][xp + 1] == 'w'))
                    {
                        rez = table.matW[yp - 1][xp + 1];
                        if (yp - 1 > 2) rez += 2;
                       
                        if (rez > max)
                        {
                            max = rez;
                            yk = yp - 1; xk = xp + 1;
                        }
                        if (table.matW[yk][xk] == 10)
                        {
                            table.logic.CallChess(xk, yk, xp, yp);
                            if (table.matN[yk][xk]=='w')
                                table.logic.chessW = true;
                            else
                                table.logic.chessB = true;
                        }
                    }
                }
                if (yp - 2 >= 0 && prvi && (table.matN[yp - 2][xp] == ' ' || table.matN[yp - 2][xp] == 'w'))
                {

                    if (table.matN[yp - 2][xp] == ' ')
                        rez = 0;
                    else
                        rez = table.matW[yp - 2][xp];
                    if (yp - 1 > 2) rez += 2;
                    if (rez > max)
                    {
                        max = rez;
                        yk = yp - 2; xk = xp;
                    }

                }
                rez = max;
               
            }
            else {
                if (yp +1<8)
                {
                    if ((table.matN[yp + 1][xp] == ' '))
                    {
                       
                        yk = yp + 1;
                    }                
                    if (xp - 1 >= 0 &&  (table.matN[yp + 1][xp - 1] != ' ' &&   table.matN[yp + 1][xp - 1] == 'b'))
                    {                      
                        yk = yp + 1; xk = xp - 1;
                        if (table.matW[yk][xk] == 10)
                        {
                            table.logic.CallChess(xk, yk, xp, yp);
                            if (table.matN[yk][xk] == 'b')
                                table.logic.chessW = true;
                            else
                                table.logic.chessB = true;
                        }
                    }
                    if (xp + 1 < 8 &&  (table.matN[yp + 1][xp + 1] != ' ' &&
                        table.matN[yp + 1][xp + 1] == 'b'))
                    {                    
                        yk = yp + 1; xk = xp + 1;
                        if (table.matW[yk][xk] == 10)
                        {
                            table.logic.CallChess(xk, yk, xp, yp);
                            if (table.matN[yk][xk] == 'b')
                                table.logic.chessW = true;
                            else
                                table.logic.chessB = true;
                        }
                    }
                }
               if (yp + 2 <8 && prvi && (table.matN[yp +2][xp] == ' ' || table.matN[yp + 2][xp] == 'b'))
                {
                        yk = yp + 2;   
                }
            }         
        }


    }
}
