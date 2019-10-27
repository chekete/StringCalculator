using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    class Arithme
    {
        public string svalue { get; set; }
        public Operator Operation;
        public Arithme(string s, Operator o)
        {
            svalue = s;
            Operation = o;
        }
    }
}
