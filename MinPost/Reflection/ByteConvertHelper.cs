using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MinPost
{
    public class ByteConvertHelper
    {
        /// <summary>
        /// 将对象转换为byte数组
        /// </summary>
        /// <param name="obj">被转换对象</param>
        /// <returns>转换后byte数组</returns>
        public static byte[] T2Bytes<T>(T obj)
        {
            byte[] buff;
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter iFormatter = new BinaryFormatter();
                iFormatter.Serialize(ms, obj);
                buff = ms.GetBuffer();
            }
            return buff;
        }

        /// <summary>
        /// 将byte数组转换成对象
        /// </summary>
        /// <param name="buff">被转换byte数组</param>
        /// <returns>转换完成后的对象</returns>
        public static T Bytes2T<T>(byte[] buff)
        {
            using (MemoryStream ms = new MemoryStream(buff))
            {
                try
                {
                    IFormatter iFormatter = new BinaryFormatter();

                    var obj = (T)iFormatter.Deserialize(ms);
                    return obj;
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("数据加载失败,请点继续并选择一个正确的文件.");
                    throw new Exception("数据加载失败,请点继续并选择一个正确的文件.");
                }
            }
        }
    }
}
