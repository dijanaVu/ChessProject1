using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication2
{
    class Player
    {
   
        public List<Figure> list = new List<Figure>();
        public bool turn = false;

        public Player() { }

        public virtual void ArrangeFigures()
        {
          
        }

        public virtual void DrawFig()
        {
             
        }

       
    }
}
