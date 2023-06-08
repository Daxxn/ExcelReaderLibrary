using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReaderLibrary.Models
{
   /// <summary>
   /// Options for controlling the behaviour of the <see cref="ExcelReader"/>
   /// </summary>
   public class ExcelReaderOptions
   {
      /// <summary>
      /// Start at a certain workbook page index.
      /// </summary>
      public int WorkbookIndex { get; set; } = 0;
      /// <summary>
      /// The index of the column names to use as the name match.
      /// <para/>
      /// Default: The first row in the spreadsheet.
      /// </summary>
      public int HeaderIndex { get; set; } = 0;
      /// <summary>
      /// The starting index of the data.
      /// <para/>
      /// Default: start after the header field.
      /// </summary>
      public int StartIndex { get; set; } = 1;
      /// <summary>
      /// The stopping point of the data.
      /// <para/>
      /// Default: <c>-1</c> = dont stop until the end of the file.
      /// </summary>
      public int EndIndex { get; set; } = -1;
      /// <summary>
      /// The maximum number of columns to search.
      /// <para/> This is a safety to prevent overly long header searching. If the spreadsheet has more tables in other columns, this will exclude them.
      /// </summary>
      public int MaxColumnSize { get; set; } = 255;
      /// <summary>
      /// Assign all files a unique ID.
      /// <para/>
      /// Default: <see langword="null"/> = Use the file name as the FileID.
      /// </summary>
      public string FileID { get; set; } = null;

      /// <summary>
      /// Default Constructor
      /// </summary>
      public ExcelReaderOptions() { }
   }
}
