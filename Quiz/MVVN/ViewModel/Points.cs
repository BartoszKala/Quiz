using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.MVVN.ViewModel
{
    public class Points
    {
        private int points;
        private int scale;

        public Points(int p, int s)
        {
            points = p;
            scale = s;
        }

        public int getP() => points;
        public int getS() => scale;

        public void Reset()
        {
            points = 0;
            scale = 0;
        }

        public string Show() => $"Twój wynik to: \n \n {points} / {scale}";
    }
}
    

