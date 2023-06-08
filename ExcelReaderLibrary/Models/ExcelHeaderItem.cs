using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReaderLibrary.Models
{
   internal class ExcelHeaderItem
   {
      public PropertyInfo Property { get; set; }
      public int Index { get; set; }
   }
}
