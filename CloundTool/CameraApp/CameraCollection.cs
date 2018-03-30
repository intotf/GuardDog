using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraApp
{
    /// <summary>
    /// 摄像头集合控制
    /// </summary>
    public class CameraCollection
    {
        /// <summary>
        /// 摄像头集合
        /// </summary>
        public List<Camera> Cameras = new List<Camera>();

        /// <summary>
        /// 实例化摄像头
        /// </summary>
        public static CameraCollection Instance = new CameraCollection();

        /// <summary>
        /// 初始化摄像头
        /// </summary>
        private CameraCollection()
        {
            //获取当前电脑所有摄像头
            var CamerasCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            for (var i = 0; i < CamerasCollection.Count; i++)
            {
                var ca = CamerasCollection[i];
                var model = new Camera(ca);
                model.OnRead += CameraApi.CameraReader_OnRead;
                this.Cameras.Add(model);
            }
        }
    }
}
