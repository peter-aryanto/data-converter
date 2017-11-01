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
      const string constDefaultOutputTextFilePathAndName = "output-text-file.txt";

      string inputTextFilePathAndName;
      TextFileLinesToObjectListConverter<StringWrapper> textFileLinesToStringListConverter;

      List<StringWrapper> stringListToConvert;

      string outputTextFilePathAndName = constDefaultOutputTextFilePathAndName;
      ObjectListToTextFileLinesConverter<StringWrapper> stringListToTextLinesConverter;

      if (args.Length == 0)
      {
        Console.WriteLine("Please execute: text-line-importer-from-text-file <input-text-file-path-and-name> [<output-text-file-path-and-name>]");
        return;
      }
      else
      {
        inputTextFilePathAndName = args[0];
        Console.WriteLine("Reading from file: " + inputTextFilePathAndName);
      }

      textFileLinesToStringListConverter =
        new TextFileLinesToObjectListConverter<StringWrapper>(ShowExceptionErrorMessage);
      stringListToConvert =
        textFileLinesToStringListConverter.Convert(inputTextFilePathAndName);

      if (stringListToConvert == null || stringListToConvert.Count == 0)
      {
        Console.WriteLine("Program is stopped. No data to process.");
        return;
      }

      if (args.Length >= 2)
      {
        outputTextFilePathAndName = args[1];
      }

      stringListToTextLinesConverter =
        new ObjectListToTextFileLinesConverter<StringWrapper>(ShowExceptionErrorMessage);
      stringListToTextLinesConverter.Convert(
        stringListToConvert,
        outputTextFilePathAndName,
        true
      );
    }

    private class StringWrapper
    {
      private string textData;

      public StringWrapper(string inputText)
      {
        this.textData = inputText;
      }

      public override string ToString()
      {
        return textData;
      }
    }

    private static void ShowExceptionErrorMessage(Exception e)
    {
      if (e.InnerException != null)
      {
        e = e.InnerException;
      }

      Console.WriteLine(e.GetType().Name + ": " + e.Message);
    }
  }
}
