using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ExcelDataReader;

using ExcelReaderLibrary.Exceptions;
using ExcelReaderLibrary.Models;

namespace ExcelReaderLibrary
{
   /// <summary>
   /// Read data from an Excel file.
   /// </summary>
   public class ExcelReader
   {
      #region Local Props
      /// <summary>
      /// Options to use while parsing the file.
      /// </summary>
      public ExcelReaderOptions Options { get; set; } = new ExcelReaderOptions();

      private Dictionary<int, PropertyInfo> PropertyHeaders { get; set; } = new Dictionary<int, PropertyInfo>();
      #endregion

      #region Constructors
      /// <summary>
      /// Default Constructor
      /// </summary>
      public ExcelReader() { }
      /// <summary>
      /// Constructor with provided options.
      /// </summary>
      /// <param name="options">Parsing options.</param>
      public ExcelReader(ExcelReaderOptions options)
      {
         Options = options;
         if (Options.HeaderIndex >= Options.StartIndex) throw new ExcelOptionsException();
         if (Options.EndIndex > 0 && Options.StartIndex >= Options.EndIndex) throw new ExcelOptionsException();
      }
      #endregion

      #region Methods
      /// <summary>
      /// Parse the Excel file and return an <see cref="IEnumerable{T}"/> of the data.
      /// </summary>
      /// <typeparam name="T">The object to create. The object must be a class with an empty constructor.</typeparam>
      /// <param name="filePath">The full path to the file.</param>
      /// <returns>An <see cref="IEnumerable{T}"/> of all the objects created from the Excel file.</returns>
      /// <exception cref="FileLoadException"></exception>
      /// <exception cref="FileNotFoundException"></exception>
      public IEnumerable<T> Parse<T>(string filePath) where T : class, new()
      {
         if (File.Exists(filePath))
         {
            var ext = Path.GetExtension(filePath);
            if (ext == ".xlsx" || ext == ".xls")
            {
               if (Options.HeaderIndex >= Options.StartIndex)
               {
                  throw new Exception("The header index cannot be greater than or equal to the data index");
               }
               using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
               {
                  using (var reader = ExcelReaderFactory.CreateReader(stream))
                  {

                     ParseHeader<T>(reader);
                     SetReaderIndex(reader);
                     if (Options.EndIndex > 0)
                     {
                        int currentLine = Options.StartIndex;
                        List<T> data = new List<T>();
                        while (reader.Read() && currentLine < Options.EndIndex)
                        {
                           data.Add(ParseDataRow<T>(reader));
                           currentLine++;
                        }
                        return data;
                     }
                     else
                     {
                        List<T> data = new List<T>();
                        while (reader.Read())
                        {
                           data.Add(ParseDataRow<T>(reader));
                        }
                        return data;
                     }
                  }
               }
            }
            else
            {
               throw new FileLoadException("File isnt a valid type. Needs to be either \".xls\" or \".xlsx\"");
            }
         }
         else
         {
            throw new FileNotFoundException("File cannot be found.", filePath);
         }
      }

      private void ParseHeader<T>(IExcelDataReader reader) where T : class, new()
      {
         SetReaderIndex(reader, true);
         var props = new T().GetType().GetProperties();
         reader.Read();
         for (int i = 0; i < reader.FieldCount && i < Options.MaxColumnSize; i++)
         {
            foreach (var prop in props)
            {
               var excelFields = prop.GetCustomAttributes<ExcelFieldAttribute>();
               if (excelFields.Any())
               {
                  string headerName = reader.GetString(i);
                  if (excelFields.Any(field => field.CheckProperty(headerName, prop.Name, Options.FileID)))
                  {
                     if (!PropertyHeaders.ContainsKey(i))
                     {
                        PropertyHeaders.Add(i, prop);
                        break;
                     }
                  }
               }
            }
         }
      }

      private T ParseDataRow<T>(IExcelDataReader reader) where T : class, new()
      {
         T newObj = new T();
         for (int i = 0; i < reader.FieldCount && i < Options.MaxColumnSize; i++)
         {
            if (PropertyHeaders.ContainsKey(i))
            {
               var prop = PropertyHeaders[i];
               ParseProperty(reader.GetValue(i), prop, newObj);
            }
         }
         return newObj;
      }

      private void SetReaderIndex(IExcelDataReader reader, bool header = false)
      {
         reader.Reset();
         if (Options.WorkbookIndex != 0)
         {
            if (reader.ResultsCount >= Options.WorkbookIndex)
            {
               for (int i = 0; i < Options.WorkbookIndex; i++)
               {
                  reader.NextResult();
               }
            }
         }
         if (header)
         {
            if (Options.HeaderIndex != 0)
            {
               for (int i = 0; i < Options.HeaderIndex; i++)
               {
                  reader.Read();
               }
            }
         }
         else
         {
            for (int i = 0; i < Options.StartIndex; i++)
            {
               reader.Read();
            }
         }
      }

      private static void ParseProperty<T>(object value, PropertyInfo prop, T newObj, bool ignoreCase = false) where T : class, new()
      {
         if (value == null) return;
         if (prop.PropertyType == value.GetType())
         {
            prop.SetValue(newObj, value);
         }
         else if (value is double)
         {
            if (prop.PropertyType == typeof(int))
            {
               prop.SetValue(newObj, Convert.ToInt32(value));
            }
            else if (prop.PropertyType == typeof(float))
            {
               prop.SetValue(newObj, Convert.ToSingle(value));
            }
            else if (prop.PropertyType == typeof(decimal))
            {
               prop.SetValue(newObj, Convert.ToDecimal(value));
            }
            else if (prop.PropertyType == typeof(byte))
            {
               prop.SetValue(newObj, Convert.ToByte(value));
            }
         }
         else if (value is string)
         {
            if (prop.PropertyType == typeof(char))
            {
               prop.SetValue(newObj, Convert.ToChar(value));
            }
            else if (prop.PropertyType.IsEnum)
            {
               try
               {
                  prop.SetValue(newObj, Enum.Parse(prop.PropertyType, (string)value, ignoreCase));
               }
               catch (Exception)
               {

               }
            }
         }
      }
      #endregion

      #region Full Props

      #endregion
   }
}
