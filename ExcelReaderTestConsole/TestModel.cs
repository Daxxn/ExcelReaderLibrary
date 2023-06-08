using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExcelReaderLibrary.Models;

namespace ExcelReaderTestConsole
{
   public enum TestEnum
   {
      ItemA,
      ItemB,
      ItemC,
      ItemD,
   };

   public class TestModel
   {
      [ExcelField]
      public string Name { get; set; }
      [ExcelField("desc", true)]
      public string Description { get; set; }
      [ExcelField("Float")]
      public double Float { get; set; }
      [ExcelField("Index")]
      public int Value { get; set; }
      [ExcelField("Enum", "ModernFile")]
      [ExcelField("Type", "OldFile")]
      public TestEnum Enum { get; set; }

      public override string ToString() => $"{Value} - {Name} - {Enum} - {Float} -- {Description}";
   }
}
