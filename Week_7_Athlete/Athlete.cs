using System;
using System.Collections.Generic;
using System.Text;

namespace Week_7_Athlete
{
    internal class Athlete
    {
        private string _Name = "";
        private int _Time = int.MaxValue;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public int Time
        {
            get { return _Time; }
            set { _Time = value; }

        }

        public override string ToString()
        {
            return $"Snelste atleet is {Name} in {Time} seconden.";
        }

    }    

} 

