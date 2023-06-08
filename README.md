# ExcelReaderLibrary

Reads data from an Excel spreadsheet and returns the created objects. This uses the ExcelDataReader package, reducing the overhead of other libraries. This is a much faster library than things like EPPlus. Now, whats the point of this library? Its a wrapper for ExcelDataReader. It can convert the data to the object provided, based on the attributes and properties found in the object.

## Usage

The most important way the parser finds the properties to bind to is with the `ExcelField` attribute. It must be added to a property when the name of the property is NOT an exact match. Multiple attributes can be added to the same property, allowing for different names to be associated with the property.

### Example Excel Format

As of right now, it only supports the standard / boring layout. Basically, the typical column headers with each row being an individual object.

| name | val | desc |
| --- | --- | -- |
| obj1 | 42 | The first object. |
| obj2 | 84 | The second object. |
| obj3 | 1048 | and so on... |
| objn | 100000 | The last object. |

### Object Class Example

```cs
using ExcelReaderLibraryFW.Models;

public class ExcelObject
{
	[ExcelField("name")]
	public string Name { get; set; }

	[ExcelField("val")]
	public int Value { get; set; }

	[ExcelField("desc")]
	public string Description { get; set; }
}
```

### Console App Parse Example

```cs
using ExcelReaderLibraryFW;

private static bool ModernExcel { get; } = false;

static void Main(string[] args)
{
    Console.WriteLine("Starting Excel Reader Test Console...");
    string testFile;
    ExcelReaderOptions options;
    if (ModernExcel)
    {
       testFile = @"F:\Code\C#\CSharpLibraries\ExcelReaderLibraryFW\ExcelParserTestDoc.xlsx";
       options = new ExcelReaderOptions()
       {
          FileID = "ModernFile"
       };

    }
    else
    {
       testFile = @"F:\Code\C#\CSharpLibraries\ExcelReaderLibraryFW\TestOld.xls";
       options = new ExcelReaderOptions()
       {
          FileID = "OldFile"
       };
    }
    ExcelReader reader = new ExcelReader(options);
    var models = reader.Parse<TestModel>(testFile);

    foreach (var model in models)
    {
       Console.WriteLine(model);
    }

    Console.WriteLine("Done...");
    Console.ReadKey();
}
```
