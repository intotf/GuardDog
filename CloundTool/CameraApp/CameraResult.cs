using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraApp
{
    /// <summary>
    /// 摄像头返回数据
    /// </summary>
    public class CameraResult
    {
        /// <summary>
        /// 当前摄像头图片
        /// </summary>
        public string imgBase64 { get; set; }

        /// <summary>
        /// 人脸信息标注
        /// </summary>
        public List<MarkCV> FacesMark { get; set; }

        /// <summary>
        /// 眼睛信息标注
        /// </summary>
        public List<MarkCV> EyesMark { get; set; }
    }

    /// <summary>
    /// 人脸/眼睛标注
    /// </summary>
    public class MarkCV
    {
        /// <summary>
        /// 左上角的 x 坐标
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// 左上角的 y 坐标
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; set; }
    }

    /// <summary>
    /// Rectangle 扩展
    /// </summary>
    public static class RectangleExtend
    {
        /// <summary>
        /// Rectangle 转 MarkCV
        /// </summary>
        /// <param name="Rec"></param>
        /// <returns></returns>
        public static MarkCV ToMark(this Rectangle Rec)
        {
            return new MarkCV() { X = Rec.X, Y = Rec.Y, Width = Rec.Width, Height = Rec.Height };
        }

        /// <summary>
        /// List<Rectangle> 转 List<MarkCV>
        /// </summary>
        /// <param name="Recs"></param>
        /// <returns></returns>
        public static List<MarkCV> ToListMark(this List<Rectangle> Recs)
        {
            var list = new List<MarkCV>();
            foreach (var item in Recs)
            {
                list.Add(item.ToMark());
            }
            return list;
        }
    }
}
