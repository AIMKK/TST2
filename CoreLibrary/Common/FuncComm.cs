using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CoreLibrary
{
    public class FuncComm
    {
        /// <summary>
        /// copy obj
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Clone<T>(T obj)
        {
            try
            {
                using (Stream objectStream = new MemoryStream())
                {
                    using (Stream stream = new MemoryStream())
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(T));
                        serializer.Serialize(stream, obj);
                        stream.Seek(0, SeekOrigin.Begin);
                        return (T)serializer.Deserialize(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                CoreLibrary.NlogHelper.LogToFile(ex.Message);
                throw ex;
            }

        }

        /// <summary>
        /// copy obj
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object Clone(object obj, Type t)
        {
            try
            {
                using (Stream objectStream = new MemoryStream())
                {
                    using (Stream stream = new MemoryStream())
                    {
                        XmlSerializer serializer = new XmlSerializer(t);
                        serializer.Serialize(stream, obj);
                        stream.Seek(0, SeekOrigin.Begin);
                        return serializer.Deserialize(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                CoreLibrary.NlogHelper.LogToFile(ex.Message);
                throw ex;
            }

        }

    }
}
