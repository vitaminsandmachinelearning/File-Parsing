using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;

namespace File_Parse
{
    class FileParser
    {
        public List<Part> LoadFile(string fileName)
        {
            TextFieldParser parser;
            List<Part> Parts = new List<Part>();
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    parser = new TextFieldParser(reader);
                    parser.HasFieldsEnclosedInQuotes = true;
                    parser.SetDelimiters(",");
                    //Read once before loop to ignore titles line in csv file
                    parser.ReadFields();
                    while (!parser.EndOfData)
                    {
                        string[] temp = parser.ReadFields();
                        Parts.Add(new Part(temp[0], temp[1], double.Parse(temp[2]), int.Parse(temp[3])));
                    }
                }
                Console.WriteLine("File load successful.");
                return Parts;
            }
            catch (Exception e)
            {
                Console.WriteLine("File load error - " + e);
                return new List<Part>();
            }
        }
    }
}
