using System;
using System.Collections.Generic;
using System.IO;

namespace DataConverter
{
  public class TextFileLinesToObjectListConverter<T>
  {
    public delegate void ExceptionHandlingMethod(Exception e);

    private ExceptionHandlingMethod HandleException;

    public TextFileLinesToObjectListConverter(ExceptionHandlingMethod handleException)
    {
      this.HandleException = handleException;
    }

    public List<T> Convert(string textFilePathAndName)
    {
      string textFileLine;
      T objectFromTextFileLine;
      String[] parameterFromTextFileLine = new String[1];
      List<T> objectListFromTextFileLines = new List<T>();

      StreamReader textFileStreamReader = null;
      try
      {
        textFileStreamReader = new StreamReader(textFilePathAndName);

        while((textFileLine = textFileStreamReader.ReadLine()) != null)
        {
          try
          {
            parameterFromTextFileLine[0] = textFileLine;
            objectFromTextFileLine = (T)Activator.CreateInstance(
              typeof(T), 
              parameterFromTextFileLine
            );
            objectListFromTextFileLines.Add(objectFromTextFileLine);
          }
          catch (Exception e)
          {
            if (this.HandleException != null)
            {
              this.HandleException(e);
            }
          }
        }
      }
      catch (Exception e)
      {
        if (this.HandleException != null)
        {
          this.HandleException(e);
        }
      }
      finally
      {
        if (textFileStreamReader != null)
        {
          textFileStreamReader.Close();
        }
      }

      return objectListFromTextFileLines;
    }
  }
}
