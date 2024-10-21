using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class Logic
    {
        public Table table;
        public List<T> toMove= new List<T>();
        public bool chessB = false, chessW = false, isChessCheck=false;
        public int xk, yk, xf, yf;

        public Logic(Table tab)
        {
            table = tab;
        }

        public class T {
            public int weight;
            public int x,y;
            public int posx,posy;
            public T() { }
        }


        public void MakeT(int x,int y,int xk,int yk,int w) {
            T t= new T();
            t.x = x;
            t.y = y;
            t.posx = xk;
            t.posy = yk;
            t.weight = w;
            toMove.Add(t);
        }


        public void BlackMove() {
            PlayerB ig = table.playerB as PlayerB;
            for (int i = 0; i < ig.list.Count; i++)
            {
                CheckFields((ig.list[i].x - 2) / (table.w / 8), (ig.list[i].y - 2) / (table.w / 8), "move", ig.list[i]);
            }
        }


        public bool HandleMove(int xp, int yp, int xk, int yk, bool isW)
        {
            // update positions and remove from list 
            bool isAllowed = false;
            Player pl, upd;
            if (isW) { pl = table.playerB as PlayerB; upd = table.playerW as PlayerW; }
            else { pl = table.playerW as PlayerW; upd = table.playerB as PlayerB; }
            bool isValid = false;
            if (table.matN[yk][xk] == ' ')
                isValid = true;
            if (!isValid)
            {
                for (int i = 0; i < pl.list.Count; i++)
                {
                    if ((pl.list[i].x - 2) / (table.w / 8) == xk && (pl.list[i].y - 2) / (table.h / 8) == yk)
                    {
                        isValid = true;
                        lock (pl.list)
                        {
                            if (pl.list[i].GetType().FullName == "WindowsFormsApplication2.King") table.handle_end();
                            pl.list.RemoveAt(i);
                        }
                        table.matN[yk][xk] = ' ';

                        break;
                    }
                }
            }
            if (isValid)
            {
                isAllowed = true;
                table.matN[yk][xk] = table.matN[yp][xp];
                table.matN[yp][xp] = ' ';
                table.matW[yk][xk] = table.matW[yp][xp];
                table.matW[yp][xp] = 0;
                int ind = 0;
                for (int i = 0; i < upd.list.Count; i++)
                {
                    if ((upd.list[i].x - 2) / (table.w / 8) == xp && (upd.list[i].y - 2) / (table.h / 8) == yp)
                    {
                       
                        ind = i;
                        break;
                    }
                }

                lock (upd)
                {
                    upd.list[ind].x = xk * table.w / 8 + 2;
                    upd.list[ind].y = yk * table.h / 8 + 2;
                }
            }
            return isAllowed;
        }


        public void CallChess(int x1,int y1,int x2,int y2) {
            xk = x1;yk = y1;yf = y2; xf = x2;
        }


        public void Chess() {
            if (table.matN[yk][xk]!=' '&& table.matN[yk][xk] == 'b')
            {
                // search for figure to take down white player
                int rez = 16, x = 8, y = 8;
                Player playerB = table.playerB as PlayerB;
                for (int i = 0; i < playerB.list.Count; i++)
                {
                    chessB = true;
                    bool way = playerB.list[i].OnWay((playerB.list[i].x - 2) / (table.w / 8), (playerB.list[i].y - 2) / (table.h / 8), xf, yf);
                    if (way && playerB.list[i].weight < rez)
                    {
                        rez = playerB.list[i].weight;
                        x = (playerB.list[i].x - 2) / (table.w / 8);
                        y = (playerB.list[i].y - 2) / (table.h / 8);
                    }
                }

                if (x == 8)
                {
                    // king has to move away as above not found
                    isChessCheck = true;
                    int[] nizx = new int[] { xk - 1, xk - 1, xk - 1, xk + 1, xk,     xk,     xk + 1, xk + 1, xk + 1 };
                    int[] nizy = new int[] { yk - 1, yk + 1, yk,     yk ,    yk + 1, yk - 1, yk + 1, yk,     yk - 1 };
                    table.matN[yk][xk] = ' ';
                    for (int i = 0; i < nizx.Length; i++)
                    {
                        if (nizx[i] >= 0 && nizx[i] < 8 && nizy[i] >= 0 && nizy[i] < 8)
                        {
                            if ((table.matN[nizy[i]][nizx[i]] == ' ' ||
                                (table.matN[nizy[i]][nizx[i]] != ' ' && table.matN[nizy[i]][nizx[i]] != 'b')))
                            {            
                                                   
                                PlayerW plW = table.playerW as PlayerW;
                                for (int j = 0; j < plW.list.Count; j++)
                                {
                                    chessB = plW.list[j].OnWay((plW.list[j].x - 2) / (table.w / 8), (plW.list[j].y - 2) / (table.h / 8), nizx[i], nizy[i]);
                                    if (chessB) break;
                                }
                                if (!chessB)
                                {
                                    Form1.begX = xk;
                                    Form1.begY = yk;
                                    Form1.lastX = nizx[i];
                                    Form1.lastY = nizy[i];                                 
                                    break;
                                }
                            }               
                        }
                    }
                    table.matN[yk][xk] = 'b';
                    isChessCheck = false;
                }
                else
                {
                    chessB = false;                 
                    {
                        Form1.begX = x;
                        Form1.begY = y;
                        Form1.lastX = xf;
                        Form1.lastY = yf;
                    }                 
                }
                if (x < 8) { Form1.lab.Text = ""; }
            }
            else {
                chessB = false;
            }         
        }


        public void CheckWhiteChess()
        {
            // check if is chess after white player move
            PlayerW pl = table.playerW as PlayerW;
            for (int i = 0; i < pl.list.Count; i++)
            {
                int xk, yk, r;
                pl.list[i].Covers((pl.list[i].x - 2) / (table.w / 8), (pl.list[i].y - 2) / (table.w / 8), out xk,out yk,out r,"");
            }
        }


        public void Decision()
        {
            // decision for black player
            if (!chessB)
            {
                // check is it chess
                CheckWhiteChess();
                if (!chessB)
                {                
                    toMove.Clear();
                    // craets list of posible moves with weights
                    BlackMove(); 
                    int  poz = 0, b1 = -1;
                    for (int i = 0; i < toMove.Count; i++)
                    {
                        if (toMove[i].weight > b1)
                        {
                            b1 = toMove[i].weight;
                            poz = i;
                        }
                    }

                    if(toMove.Count>0)
                    {
                        Form1.begX = toMove[poz].x;
                        Form1.begY = toMove[poz].y;
                        Form1.lastX = toMove[poz].posx;
                        Form1.lastY = toMove[poz].posy;
                    }
                }
                else
                    Chess();
            }
            else
                Chess();            
        }


        public void CheckFields(int xp, int yp, string str, Figure fig)
        {
            // check where to move
            int xk = xp;
            int yk = yp;
            int r = -1;
            fig.Covers(xp, yp, out xk, out yk, out r, str);
            if (str == "move" && r >= 0 && (yk != yp || xk != xp))
            {
                table.logic.MakeT(xp, yp, xk, yk, r);
            }
        }


        public bool BlackPlayerMove(int xb,int yb,int xl,int yl,int ind)
        {
            // checking if move to position (xl,yl) is allowed
            // if yes, covers checks is it chess
            bool move = false;
            if (table.matN[yb][xb] != ' ')
            {
                move =table.playerB.list[ind].AllowedMove(xb, yb, xl, yl);
                if (move) {
                    int xk, yk, r;
                    table.playerB.list[ind].Covers((table.playerB.list[ind].x - 2) / (table.w / 8),
                        (table.playerB.list[ind].y - 2) / (table.w / 8), out xk, out yk, out r, "");
                }
                
                if (!move && !chessB) { Decision(); }
            }
            else
                Decision();
            return move;
        }
    }
}
