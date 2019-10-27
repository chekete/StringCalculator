using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    class Arithme
    {
        public string Svalue { get; set; }
        public Operator Operation;
        public Arithme(string s, Operator o)
        {
            Svalue = s;
            Operation = o;
        }
    }
}
