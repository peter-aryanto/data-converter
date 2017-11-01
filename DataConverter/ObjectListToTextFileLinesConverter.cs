using System;
using System.Collections.Generic;
using System.IO;
//using System.Runtime.ExceptionServices;

namespace DataConverter
{
  public class ObjectListToTextFileLinesConverter<T>
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

    public ObjectListToTextFileLinesConverter(ExceptionHandlingMethod handleException)
    {
      if (handleException != null)
      {
        this.handleException = handleException;
      }
    }

    public void Convert(
      List<T> objectList,
      string textFilePathAndName,
      bool displayConvertedObjectAsStringOnScreen
    )
    {
      StreamWriter textFileStreamWriter = null;
      try
      {
        textFileStreamWriter = new StreamWriter(textFilePathAndName);

        foreach (T anObject in objectList)
        {
          textFileStreamWriter.WriteLine(anObject.ToString());

          if (displayConvertedObjectAsStringOnScreen)
          {
            Console.WriteLine(anObject.ToString());
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
        if (textFileStreamWriter != null)
        {
          textFileStreamWriter.Close();
        }
      }
    }
  }
}
