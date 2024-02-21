using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ChemUtility;

namespace ChemUtility.States
{
    public class MassState : AbstractChemState
    {
        public override void DisplayPrompt()
        {
            Console.WriteLine("Mass Mode: Type '!MODE' To Change");
            Console.WriteLine("Enter Chemical Formula (ex H2O) => ");
        }

        public override void EnterState()
        {
            DisplayPrompt();
        }

        public override void ExitState()
        {
            throw new NotImplementedException();
        }

        public override void ParseInput(string input)
        {
            StringReader sr = new StringReader(input);
            char current = (char)sr.Read();
            StringBuilder elem = new StringBuilder();
            List<string> output = new List<string>();
            while (true)
            {
                elem.Clear();
                if (!char.IsLetter(current)) break;
                if (char.IsUpper(current)) //is the start of an element
                {
                    elem.Append(current);
                    while (true)
                    {
                        if (sr.Peek() == -1)
                        {
                            output.Add(elem.ToString());
                            current = (char)sr.Read();
                            break;
                        }


                        current = (char)sr.Read();
                        if (char.IsLower(current))
                        {
                            elem.Append(current);
                            continue;
                        }
                        if (char.IsNumber(current))
                        {
                            int i = 0;
                            i = (int)(current - '0');
                            while (true)
                            {
                                if (sr.Peek() == -1) break;
                                if (char.IsNumber((char)sr.Peek()))
                                {
                                    i = (i * 10) + (int)(sr.Read() - '0');
                                    continue;
                                }
                                break;
                            }
                            for (int num = 0; num < i; num++)
                            {
                                output.Add(elem.ToString());
                            }
                            current = (char)sr.Read();
                            break;

                        }
                        output.Add(elem.ToString());
                        break;

                    }
                    continue;
                }
                break;

            }

        }

        void DisplayOutput(string[] input, string UserInput)
        {
            float output = 0;
            if (input.Length == 0)
            {
                Console.Write("Unknown Element");
                Console.ReadKey();
                Console.WriteLine();
                return;
            }

            foreach (string st in input)
            {
                if (!PeriodicTable.ElemIndex.TryGetValue(st, out int id))
                {
                    Console.Write("Unknown Element");
                    Console.ReadKey();
                    Console.WriteLine();
                    
                }
                output += float.Parse(PeriodicTable.Table[id][(int)DataIndex.AtomicMass]);
            }
            Console.Write("Mass Of Molecule " + UserInput + " => " + output + "    Enter to continue...");
        }
    }
}

