using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
namespace ChemUtility
{
    public class PeriodicTable
    {
        static string dataPath = "C:\\Users\\Thomas (Purdue)\\source\\repos\\PerdicTableOfElements";
        public static string[][] Table { get; private set; }
        public static Dictionary<string, int> ElemIndex { get; private set; }
        

        PeriodicTable()
        {
            StreamReader sr = new StreamReader(dataPath);
            ElemIndex = new Dictionary<string, int>();
            string raw = sr.ReadToEnd();
            sr.Close();
            Table = ParseTable(raw);
            for (int i = 0; i < Table.Length; i++)
            {
                ElemIndex.Add(Table[i][(int)DataIndex.AtomicNumber], i);
                ElemIndex.Add(Table[i][(int)DataIndex.Symbol], i);
            }
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
