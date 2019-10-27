using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    class NewMath
    {
        public void runAllFunctions()
        {
            var met = typeof(Math).GetMethods();

            foreach (var methodInfo in met)
            {
                Console.WriteLine(methodInfo.Name);
                var param = methodInfo.GetParameters();
                foreach (var parameterInfo in param)
                {
                    Console.WriteLine(parameterInfo.Name);
                    Console.WriteLine(parameterInfo.ParameterType);
                }
            }
        }

        public static bool IsFunctionExiste(string sv)
        {
            var met = typeof(Math).GetMethods();

            foreach (var methodInfo in met)
            {
                if (sv.ToLower().Contains(methodInfo.Name.ToLower()))
                {
                    return true;
                }

            }
            return false;
        }

        public static string ExecuteFunction(string sv)
        {
            object retour = "";
            var met = typeof(Math).GetMethods();
            string sClean = "";
            bool entreLesParanthese = false;
            for (int i = 0; i < sv.Length; i++)
            {

                if (sv[i] == ')')
                {
                    entreLesParanthese = false;
                }
                if (entreLesParanthese)
                    sClean += sv[i].ToString();
                if (sv[i] == '(')
                {
                    entreLesParanthese = true;
                    sClean = "";
                }
            }

            foreach (var methodInfo in met)
            {
                if (sv.ToLower().Contains(methodInfo.Name.ToLower()))
                {
                    object[] param = new object[methodInfo.GetParameters().Length];
                    var getParams = methodInfo.GetParameters();
                    var listDigit = sClean.Split(',');

                    for (int i = 0; i < getParams.Length; i++)
                    {
                        var par = getParams[i];
                        Type type = par.ParameterType;

                        var d = Convert.ChangeType(listDigit[i], type);

                        param[i] = d;
                    }

                    retour = methodInfo.Invoke(null, param);
                    return retour.ToString();
                }
            }
            return retour.ToString();
        }
    }
}
