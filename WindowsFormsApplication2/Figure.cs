using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class  Figure
    {
        public string path;
        public int x, y,weight;
        protected Table table;

        public Figure() { }

        public virtual string name { get; set; }


        public virtual bool AllowedMove(int xp, int yp, int xk, int yk) {
            bool isAllowed = false;
            return isAllowed;
        }

        public virtual void CheckFields(int xp, int yp,string str) {

        }

        public virtual bool OnWay(int xp, int yp, int xk, int yk) {
            bool put = true;
            return put;
        }

        public virtual void Covers(int xp, int yp, out int xk, out int yk, out int r, string str) {
            xk = 1;yk = 1;r = 1;
        }

      

    }
}
