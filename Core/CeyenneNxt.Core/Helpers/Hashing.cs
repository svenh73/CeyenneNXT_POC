using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace CeyenneNxt.Core.Helpers
{
  public static class Hashing
  {
    /// <summary>
    /// Generates an MD5 hash of the current object. Keep in mind, this is not suitable for cryptographic operations.
    /// Also, this will only work on types defined as public which contain a parameterless constructor.
    /// </summary>
    /// <param name="sourceObject">An object to compute the hash from</param>
    /// <returns>The md5 hash representation of the object.</returns>
    public static string GenerateKey(object sourceObject)
    {
      //Catch unuseful parameter values
      if (sourceObject == null)
      {
        throw new ArgumentNullException($"Null as parameter ({nameof(sourceObject)}) is not allowed");
      }

      //We determine if the passed object is really serializable.
      try
      {
        //Now we begin to do the real work.
        return ComputeHash(ObjectToByteArray(sourceObject));
      }
      catch (AmbiguousMatchException ex)
      {
        throw new ApplicationException($"Could not definitely decide if object ({nameof(sourceObject)}) is serializable. Message:" + ex.Message);
      }
    }

    private static readonly object Locker = new object();

    /// <summary>
    /// Converts an object to a byte array using the XmlSerializer.
    /// </summary>
    /// <param name="objectToConvert">An object to convert to a byte array.</param>
    /// <returns>A byte array representation of the supplied object parameter.</returns>
    public static byte[] ObjectToByteArray(object objectToConvert)
    {
      var fs = new MemoryStream();

      try
      {
        lock (Locker)
        {
          new XmlSerializer(objectToConvert.GetType()).Serialize(fs, objectToConvert);
        }
        return fs.ToArray();
      }
      catch (SerializationException ex)
      {
        Console.WriteLine($"Error occurred during serialization of {nameof(objectToConvert)}. Message: " + ex.Message);
        return null;
      }
      finally
      {
        fs.Close();
      }
    }

    /// <summary>
    /// Computes an Md5 hash over the passed byte array.
    /// </summary>
    /// <param name="objectAsBytes">Byte array representation of the object to hash.</param>
    /// <returns>An Md5 hash string.</returns>
    public static string ComputeHash(byte[] objectAsBytes)
    {
      MD5 md5 = new MD5CryptoServiceProvider();
      try
      {
        var result = md5.ComputeHash(objectAsBytes);

        // Build the final string by converting each byte
        // into hex and appending it to a StringBuilder
        var sb = new StringBuilder();
        foreach (var t in result)
        {
          sb.Append(t.ToString("X2"));
        }

        // And return it
        return sb.ToString();
      }
      catch (ArgumentNullException)
      {
        //If something occurred during serialization, 
        //this method is called with a null argument. 
        Console.WriteLine("Hash has not been generated.");
        return null;
      }
    }
  }
}
