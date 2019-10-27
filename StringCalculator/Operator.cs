using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    class Operator
    {
        public int Order;
        public char Signe;
    }

    class Soustraction : Operator
    {
        public Soustraction()
        {
            Signe = '-';
            Order = 4;
        }
    }

    class Addition : Operator
    {
        public Addition()
        {
            Signe = '+';
            Order = 3;
        }
    }

    class Multiplication : Operator
    {
        public Multiplication()
        {
            Signe = '*';
            Order = 2;
        }
    }

    class Division : Operator
    {
        public Division()
        {
            Signe = '/';
            Order = 1;
        }
    }
}
