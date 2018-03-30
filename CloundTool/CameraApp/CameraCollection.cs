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
        /// 当前摄像信息
        /// </summary>
        private FilterInfo DefaultCamera { get; set; }

        /// <summary>
        /// 摄像头集合
        /// </summary>
        public FilterInfoCollection CamerasCollection { get; set; }

        /// <summary>
        /// 选中的摄像头驱动
        /// </summary>
        public VideoCaptureDevice videoDevice { get; set; }

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
            this.CamerasCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        }

        /// <summary>
        /// 选择摄像头
        /// </summary>
        /// <param name="Camera">CamerasCollection 中某一个摄像头</param>
        /// <returns></returns>
        public Camera SetCamera(FilterInfo Camera)
        {
            this.DefaultCamera = Camera;
            this.videoDevice = new VideoCaptureDevice(this.DefaultCamera.MonikerString);
            return new Camera(videoDevice);
        }

        /// <summary>
        /// 停止设备
        /// </summary>
        public bool StopCamera()
        {
            if (videoDevice != null)
            {
                videoDevice.Stop();
            }
            return true;
        }

        /// <summary>
        /// 当前选择的摄像头监视
        /// </summary>
        public class Camera
        {
            /// <summary>
            /// 选中的摄像头驱动
            /// </summary>
            private VideoCaptureDevice videoDevice { get; set; }

            /// <summary>
            /// 读取头像信息
            /// </summary>
            private Action<string> OnRead;

            /// <summary>
            /// 上一次发送图片时间
            /// </summary>
            private DateTime lastSendTime = DateTime.Now;

            /// <summary>
            /// 发送图片间隔时间 250毫秒
            /// 每秒 发送 5张图片
            /// </summary>
            private readonly TimeSpan Delay = TimeSpan.FromMilliseconds(250d);

            /// <summary>
            /// 时实图片字节
            /// 开启2M 临时连接空间
            /// </summary>
            private readonly byte[] BitmapBuffer = new byte[2 * 1024 * 1024];

            /// <summary>
            /// 时实图片文内存流
            /// </summary>
            private readonly MemoryStream BitmapStream = new MemoryStream();

            /// <summary>
            /// 构建一个摄像头
            /// </summary>
            /// <param name="videoDevice"></param>
            public Camera(VideoCaptureDevice videoDevice)
            {
                this.videoDevice = videoDevice;
            }


            /// <summary>
            /// 开始监控摄像头
            /// </summary>
            /// <param name="Device">设备</param>
            /// <returns></returns>
            public bool StartCamera()
            {
                try
                {
                    //开启摄像功能
                    if (this.videoDevice == null)
                    {
                        return false;
                    }
                    this.videoDevice.NewFrame += videoDevice_NewFrame;
                    this.videoDevice.Start();
                    if (this.videoDevice.IsRunning)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("打开摄像头失败：{0}" + ex.Message);
                    return false;
                }
            }

            /// <summary>
            /// 摄像头实时数据流
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="eventArgs"></param>
            private void videoDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
            {
                if (DateTime.Now.Subtract(this.lastSendTime) > this.Delay)
                {
                    if (eventArgs.Frame != null)
                    {
                        Bitmap bmp = eventArgs.Frame;
                        var action = this.OnRead;
                        if (action != null)
                        {
                            action.Invoke(this.bitmapToBase64(bmp));
                            this.lastSendTime = DateTime.Now;
                        }
                        bmp.Dispose();
                        eventArgs.Frame.Dispose();
                    }
                }
            }

            /// <summary>
            /// 停止设备
            /// </summary>
            public bool StopCamera()
            {
                if (videoDevice != null)
                {
                    videoDevice.Stop();
                }
                return true;
            }

            /// <summary>
            /// Bitmap 转 Base64
            /// </summary>
            /// <param name="bitmap"></param>
            /// <returns></returns>
            private string bitmapToBase64(Bitmap bitmap)
            {
                this.BitmapStream.Position = 0;
                bitmap.Save(this.BitmapStream, ImageFormat.Jpeg);
                this.BitmapStream.Position = 0;
                var length = this.BitmapStream.Read(this.BitmapBuffer, 0, (int)this.BitmapStream.Length);
                return Convert.ToBase64String(this.BitmapBuffer, 0, length);
            }
        }
    }
}
