using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReaderLibrary.Models
{
   /// <summary>
   /// Associate a <see langword="string"/> in the Excel header column with the property its added to.
   /// </summary>
   [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
   public sealed class ExcelFieldAttribute : Attribute
   {
      private readonly string propName = null;
      private readonly string fileID = null;
      private readonly bool ignoreCase = false;

      /// <summary>
      /// Assign the properties full name as the Excel column header.
      /// <para/>
      /// <see langword="true"/> to ignore casing.
      /// </summary>
      /// <param name="ignoreCase"><see langword="true"/> to ignore casing.</param>
      public ExcelFieldAttribute(bool ignoreCase = false) =>
         this.ignoreCase = ignoreCase;

      /// <summary>
      /// Assign the provided <see langword="string"/> as the Excel column header.
      /// </summary>
      /// <param name="propertyName">The name of the Excel column header.</param>
      public ExcelFieldAttribute(string propertyName)
      {
         propName = propertyName;
      }

      /// <summary>
      /// Assign the provided <see langword="string"/> as the Excel column header and ignore casing.
      /// </summary>
      /// <param name="propertyName">The name of the Excel column header.</param>
      /// <param name="ignoreCase"><see langword="true"/> to ignore casing.</param>
      public ExcelFieldAttribute(string propertyName, bool ignoreCase)
      {
         propName = propertyName;
         this.ignoreCase = ignoreCase;
      }

      /// <summary>
      /// Assign the provided <see langword="string"/> as the Excel column header.
      /// <para/>
      /// Also must match with the <see cref="ExcelReaderOptions.FileID"/> or the file name.
      /// </summary>
      /// <param name="propertyName">The name of the Excel column header.</param>
      /// <param name="fileTypeID">Unique file ID.</param>
      public ExcelFieldAttribute(string propertyName, string fileTypeID)
      {
         propName = propertyName;
         fileID = fileTypeID;
      }

      /// <summary>
      /// Assign the provided <see langword="string"/> as the Excel column header and ignore casing.
      /// <para/>
      /// Also must match with the <see cref="ExcelReaderOptions.FileID"/> or the file name.
      /// </summary>
      /// <param name="propertyName">The name of the Excel column header.</param>
      /// <param name="fileTypeID">Unique file ID.</param>
      /// <param name="ignoreCase"><see langword="true"/> to ignore casing.</param>
      public ExcelFieldAttribute(string propertyName, string fileTypeID, bool ignoreCase)
      {
         propName = propertyName;
         fileID = fileTypeID;
         this.ignoreCase = ignoreCase;
      }

      /// <summary>
      /// Check the Excel field with this <see cref="ExcelFieldAttribute"/>.
      /// </summary>
      /// <param name="input">Excel field value.</param>
      /// <param name="propName">The full name of the bound property.</param>
      /// <param name="fileID">The unique FileID.</param>
      /// <returns><see langword="true"/> if the field value matches the bound property.</returns>
      public bool CheckProperty(string input, string propName, string fileID)
      {
         if (FileID != null)
         {
            if (fileID != FileID) return false;
         }
         if (PropertyName == null && propName == input) return true;
         return ignoreCase ? PropertyName.ToLower() == input.ToLower() : PropertyName == input;
      }

      /// <summary>
      /// Ignore all letter casing.
      /// </summary>
      public bool IgnoreCase => ignoreCase;
      /// <summary>
      /// The name of the Excel column header.
      /// </summary>
      public string PropertyName => propName;
      /// <summary>
      /// The unique file id/name that this property is associated with.
      /// </summary>
      public string FileID => fileID;
   }
}
