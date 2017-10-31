using System;
using System.Collections.Generic;
using System.IO;
//using System.Runtime.ExceptionServices;

namespace DataConverter
{
  public class TextFileLinesToObjectListConverter<T>
  {
    public delegate void ExceptionHandlingMethod(Exception e);

    private ExceptionHandlingMethod handleException; /*= (Exception e) =>
    {
      if (e.InnerException != null)
      {
        e = e.InnerException;
      }

      ExceptionDispatchInfo.Capture(e).Throw();
    };*/

    public TextFileLinesToObjectListConverter(ExceptionHandlingMethod handleException)
    {
      if (handleException != null)
      {
        this.handleException = handleException;
      }
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
            if (this.handleException != null)
            {
              this.handleException(e);
            }
            else
            {
              throw;
            }
          }
        }
      }
      catch (Exception e)
      {
        if (this.handleException != null)
        {
          this.handleException(e);
        }
        else
        {
          throw;
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
