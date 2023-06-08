using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReaderLibrary.Exceptions
{
   /// <summary>
   /// Generated when an option is not valid and would cause malfunctions.
   /// </summary>
   public class ExcelOptionsException : Exception
   {
      /// <summary>
      /// Initalizes a new instance of the <see cref="ExcelOptionsException"/> class.
      /// </summary>
      public ExcelOptionsException() { }
      /// <summary>
      /// Initalizes a new instance of the <see cref="ExcelOptionsException"/> class with a specified error <paramref name="message"/>.
      /// </summary>
      public ExcelOptionsException(string message) : base(message) { }
   }
}
