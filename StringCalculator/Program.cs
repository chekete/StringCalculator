using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            SuperCalculator sc = new SuperCalculator();

            Console.WriteLine("Press Q pour quitter.");
            Console.WriteLine("Press Enter pour calculer.");

            while (true)
            {
                Console.WriteLine("Entrer une chaine a calculer.");
             
                var keyTap = Console.ReadLine();

                Console.WriteLine(string.Format($"{keyTap} = {sc.Execute(keyTap)}"));
            }

        }
    }
}
