using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    class SuperCalculator
    {
        public string Execute(string myString)
        {
            myString = myString.Replace(" ", "");

            string retour = "";
            while (myString.Length > 0)
            {
                int indexStart = 0;
                int indexEnd = 0;
                bool dansLaParenthese = false;
                try
                {
                    string cleanString = CreateCleanString(myString, ref indexStart, ref indexEnd, ref dansLaParenthese);

                    retour = CleanStringCalculation(cleanString);

                    myString = myString.Remove(indexStart, (!dansLaParenthese && indexEnd == 0) ? myString.Length : indexEnd);

                    if (retour != null)
                        if (!string.IsNullOrEmpty(myString))
                            myString = myString.Insert(Math.Min(indexStart, myString.Length - 1), retour);
                }
                catch (Exception)
                {
                    break;
                }
            }

            if (myString.Length > 0)
            {
                Console.WriteLine("Une erreur est survenu lors du traitement de la chaine de caractere a cote de <{0}>", myString);
            }
            if (string.IsNullOrEmpty(retour))
                Console.WriteLine("Erreur : Assurer vous que votre chaine de caractere est correct.");

            return retour;
        }

        private string CreateCleanString(string myString, ref int indexStart, ref int indexEnd, ref bool dansLaParenthese)
        {
            string cleanString = "";
            for (int i = 0; i < myString.Length; i++)
            {
                char c = myString[i];
                
                if (c.Equals(')'))
                {
                    if (i >= 0 && !NewMath.IsFunctionExiste(cleanString))
                    {
                        indexEnd += 1;
                        dansLaParenthese = false;
                    }
                }
                if (dansLaParenthese)
                {
                    indexEnd += 1;
                    
                    cleanString += c;
                }
                else if (indexEnd == 0)
                {
                    cleanString += c;
                }

                if (c.Equals('('))
                {
                    if (i >= 0 && !NewMath.IsFunctionExiste(cleanString))
                    {
                        indexStart = i;
                        indexEnd = 1;
                        dansLaParenthese = true;
                        cleanString = "";
                    }
                }
            }
            return cleanString;
        }

        string CleanStringCalculation(string sClean)
        {
            List<Arithme> entry = new List<Arithme>();
            string part = "";

            for (int i = 0; i < sClean.Length; i++)
            {
                char c = sClean[i];

                if (IsADigit(c))
                {
                    part += c;
                }
                else
                {
                    if (i == 0)
                    {
                        part = c.ToString();
                    }
                    else
                    {
                        if (IsOperateur(c) && !IsOperateur(sClean[i - 1]))
                        {
                            entry.Add(new Arithme(part, null));
                            entry.Add(new Arithme(c.ToString(), estOperateurArithmetique(c)));
                            part = "";
                        }
                        else
                        {
                            part += c.ToString();
                        }
                    }
                }

                if (i == sClean.Length - 1)
                {
                    entry.Add(new Arithme(part, null));
                    part = "";
                }
            }
            var result = Calc(entry);
            if (result == null)
                Console.WriteLine("Mauvais string");

            return result.Svalue;
        }



        Arithme Calc(List<Arithme> lNext)
        {
            var tt = lNext.FindAll(vx => vx.Operation != null).OrderBy(vy => vy.Operation.Order).ToList();
            if (tt.Count > 0)
            {
                try
                {
                    foreach (var arithme in tt)
                    {
                        int ind = lNext.IndexOf(arithme);

                        double.TryParse(lNext[ind - 1].Svalue, out double val1);
                        double.TryParse(lNext[ind + 1].Svalue, out double val2);

                        if (NewMath.IsFunctionExiste(lNext[ind - 1].Svalue))
                        {
                            double.TryParse(NewMath.ExecuteFunction(lNext[ind - 1].Svalue), out val1);
                        }
                        if (NewMath.IsFunctionExiste(lNext[ind + 1].Svalue))
                        {
                            double.TryParse(NewMath.ExecuteFunction(lNext[ind + 1].Svalue), out val2);
                        }

                        var res = Calculate(val1, val2, arithme.Operation.Signe);

                        lNext.RemoveAt(ind - 1);
                        lNext.RemoveAt(ind - 1);
                        lNext.RemoveAt(ind - 1);

                        lNext.Insert(ind - 1, new Arithme(res.ToString(), null));
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Une erreur est survenue lors du calcule de votre chaine, Merci de le verifier");
                }
            }
            else
            {
                try
                {
                    if (NewMath.IsFunctionExiste(lNext[0].Svalue))
                    {
                        string result = NewMath.ExecuteFunction(lNext[0].Svalue);
                        lNext.Clear();
                        lNext.Add(new Arithme(result, null));
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Une erreur est survenue lors du calcule de votre chaine, Merci de le verifier");
                }
            }

            return lNext.Single();
        }

        double Calculate(double value1, double value2, char op)
        {
            double valueR = 0;
            switch (op)
            {
                case '+':
                    valueR = value1 + value2;
                    break;
                case '-':
                    valueR = value1 - value2;
                    break;
                case '*':
                    valueR = value1 * value2;
                    break;
                case '/':
                    valueR = value1 / value2;
                    break;
            }
            return valueR;
        }

        bool IsADigit(char c)
        {
            return "0123456789".Contains(c);
        }

        bool IsOperateur(char c)
        {
            return "+*-/".Contains(c);
        }

        Operator estOperateurArithmetique(char c)
        {
            switch (c)
            {
                case '+':
                    return new Addition();
                case '-':
                    return new Soustraction();
                case '*':
                    return new Multiplication();
                case '/':
                    return new Division();
                default:
                    return null;
            }
        }
    }
}
