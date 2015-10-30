using System;

namespace SOLID.OCP
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceFileName = "InputFile.xml";
            var targetFileName = "OutputFile.json";

            var formatConverter = new FormatConverter();

            if (formatConverter.Convert(sourceFileName, targetFileName) == false)
            {
                Console.WriteLine("Conversion failed.");
                Console.ReadLine();
            }
        }
    }
}
