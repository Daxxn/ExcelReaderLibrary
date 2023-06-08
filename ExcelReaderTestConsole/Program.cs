using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExcelReaderLibrary;
using ExcelReaderLibrary.Models;

namespace ExcelReaderTestConsole
{
   internal class Program
   {
      static bool ModernExcel { get; } = false;
      static void Main(string[] args)
      {
         Console.WriteLine("Starting Excel Reader Test Console...");
         string testFile;
         ExcelReaderOptions options;
         if (ModernExcel)
         {
            testFile = @"F:\Code\C#\CSharpLibraries\ExcelReaderLibrary\ExcelParserTestDoc.xlsx";
            options = new ExcelReaderOptions()
            {
               FileID = "ModernFile"
            };

         }
         else
         {
            testFile = @"F:\Code\C#\CSharpLibraries\ExcelReaderLibrary\TestOld.xls";
            options = new ExcelReaderOptions()
            {
               FileID = "OldFile",
               EndIndex = 2,
            };
         }
         ExcelReader reader = new ExcelReader(options);
         var models = reader.Parse<TestModel>(testFile);

         foreach (var model in models)
         {
            Console.WriteLine(model);
         }

         Console.WriteLine("Done...    Press any key to close.");
         Console.ReadKey();
      }
   }
}
