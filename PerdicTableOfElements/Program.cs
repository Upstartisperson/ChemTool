using System.IO;
using System.Collections;
using System.Net;
using Microsoft.Win32.SafeHandles;
using System.Text;
using System.Linq;
using System.ComponentModel.Design;
using System.Collections.Generic;

//TODO: simple calulator with mole to molecule built in
//TODO: be able to change modes to and from mole mode and shit
//TODO: remove need for enter to continue, read the next valid input as a start for the next line
//TODO: give atomic number of single atom
//TODO: allow atomic number to be used to produce atomic symbol and info on atom
// See https://aka.ms/new-console-template for more information


while (true)
{
    Start:
    Console.Write("Enter Atomic Symbol Or Chemical Formula (ex. H2O)=> ");
    string input = Console.ReadLine();
    string path = "C:\\Users\\Thomas (Purdue)\\Downloads\\c2dd862cd38f21b0ad36b8f96b4bf1ee-1d92663004489a5b6926e944c1b3d9ec5c40900e\\c2dd862cd38f21b0ad36b8f96b4bf1ee-1d92663004489a5b6926e944c1b3d9ec5c40900e\\Periodic Table of Elements.csv";
    StreamReader StreamReader = new StreamReader(path);
    string raw = StreamReader.ReadToEnd();
    StreamReader.Close();
    string[][] arrays = ParseTable(raw);
    Dictionary<string, int> ElemIndex = new Dictionary<string, int>();
    for (int i = 0; i < arrays.Length; i++)
    {
        ElemIndex.Add(arrays[i][(int)DataIndex.AtomicNumber], i);
        ElemIndex.Add(arrays[i][(int)DataIndex.Symbol], i);
    }
    
    float outputNum = 0;

    string[] inputarray = ParseInput(input);

    
    //using (StringReader sr3 = new StringReader(input))
    //{


    //}
    
       
       
    //    if(char.IsLetter(active) && char.IsUpper(active))
    //    {
    //        sb.Append(active);


    //    }
    //    char.IsUpper(active);
    //    char.IsNumber(active);

    //hello



    if (inputarray.Length == 0) 
    {
        Console.Write("Unknown Element");
        Console.ReadKey();
        Console.WriteLine();
        continue;
    }
        
    foreach(string st in inputarray)
    {
        if (!ElemIndex.TryGetValue(st, out int id))
        {
            Console.Write("Unknown Element");
            Console.ReadKey();
            Console.WriteLine();
            goto Start;
        }
        outputNum += float.Parse(arrays[id][(int)DataIndex.AtomicMass]);
    }
    Console.Write("Mass Of Element : " + outputNum + "    Enter to continue...");
    
   
   
    
    Console.ReadLine();
    //Console.WriteLine();
    //Console.Clear();
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
string[] ParseInput(string input)
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
                if(char.IsNumber(current))
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
                    for(int num = 0 ; num < i; num++)
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

    return output.ToArray();
}
public class Element
{
    string Symbol;
    int AtomicNumber;
    float AtomicMass;
    int NumNutrons;
    int NumElectrons;
    int Period;
    int Group;
    string Phase;
    string Type;
}

public enum DataIndex : int
{
    AtomicNumber = 0,
    Element = 1,
    Symbol = 2,
    AtomicMass = 3,
    NumRaberofNeutrons = 4,
    NumberofProtons = 5,
    NumberofElectrons = 6,
    Period = 7,
    Group = 8,
    Phase = 9,
    Radioactive = 10,
    Natural = 11,
    Metal = 12,
    Nonmetal = 13,
    Metalloid = 14,
    Type = 15,
    AtomicRadius = 16,
    Electronegativity = 17,
    FirstIonization = 18,
    Density = 19,
    MeltingPoint = 20,
    BoilingPoint = 21,
    NumberOfIsotopes = 22,
    Discoverer = 23,
    Year = 24,
    SpecificHeat = 25,
    NumberofShells = 26,
    NumberofValence = 27













}
