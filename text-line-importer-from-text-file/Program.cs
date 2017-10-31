using System;
using System.IO;
using System.Collections.Generic;
using DataConverter;

namespace MainProgram
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length != 1)
      {
        Console.WriteLine("Please execute: text-line-importer-from-text-file [text-file-path-and-name]");
        return;
      }

      TextFileLinesToObjectListConverter<StringWrapper> textFileLinesTostringListConverter =
        new TextFileLinesToObjectListConverter<StringWrapper>(HandleException);
      List<StringWrapper> stringListToSort =
        textFileLinesTostringListConverter.Convert(args[0]);

      if (stringListToSort == null || stringListToSort.Count == 0)
      {
        Console.WriteLine("Program is stopped. No data to process.");
        return;
      }

      ExportstringsToTextFile(stringListToSort, "text-file-output.txt", true);
    }

    private class StringWrapper
    {
      public string TextData { get; }

      public StringWrapper(string inputText)
      {
        this.TextData = inputText;
      }
    }

    private static void HandleException(Exception e)
    {
      if (e.InnerException != null)
      {
        e = e.InnerException;
      }

      Console.WriteLine(e.GetType().Name + ": " + e.Message);
    }

    private static void ExportstringsToTextFile(
      List<StringWrapper> stringsList,
      string textFilePathAndName,
      bool displaystringsOnScreen
    )
    {
      StreamWriter textFileWriter = null;
      try
      {
        textFileWriter = new StreamWriter(textFilePathAndName);

        foreach (StringWrapper aString in stringsList)
        {
          if (displaystringsOnScreen)
          {
            Console.WriteLine(aString.TextData);
          }

          textFileWriter.WriteLine(aString.TextData);
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("The file could not be written:");
        Console.WriteLine(e.Message);
      }
      finally
      {
        if (textFileWriter != null)
        {
          textFileWriter.Close();
        }
      }
   }
  }
}
