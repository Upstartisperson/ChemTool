using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;



namespace ChemUtility
{
    public class PeriodicTable
	{
        static string dataPath = "C:\\Users\\Thomas (Purdue)\\source\\repos\\PerdicTableOfElements";
        private static string[][] table;
        public string[][] Table { get { return table; } }
        
        PeriodicTable()
        {
            StreamReader sr = new StreamReader(dataPath);
            string raw = sr.ReadToEnd();
            sr.Close();
            table = ParseTable(raw);
        }
        string[][] ParseTable(string rawInfo)
        {
            StringReader sr = new StringReader(rawInfo);
            List<List<string>> output = new List<List<string>>();
            output.Add(new List<string>());
            while (true)
            {
                char current = (char)sr.Read();
                if (current == '\n')
                {
                    StringBuilder sb = new StringBuilder();
                    while (true)
                    {
                        if (sr.Peek() == -1)
                        {
                            output[output.Count - 1].Add(sb.ToString());
                            sb.Clear();
                            sr.Close();
                            break;
                        }

                        current = (char)sr.Read();
                        if (current == ',')
                        {
                            output[output.Count - 1].Add(sb.ToString());
                            sb.Clear();
                            continue;
                        }
                        if (current == '\n')
                        {
                            output[output.Count - 1].Add(sb.ToString());
                            sb.Clear();
                            output.Add(new List<string>());
                            continue;
                        }
                        sb.Append(current);

                    }
                    output.RemoveAt(output.Count - 1);
                    break;
                }
            }
            return output.Select(a => a.ToArray()).ToArray();
        }
    }
}
