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
    /// 摄像头
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// 摄像头信息
        /// </summary>
        public FilterInfo videoInfo { get; set; }

        /// <summary>
        /// 选中的摄像头驱动
        /// </summary>
        public VideoCaptureDevice videoDevice { get; set; }

        /// <summary>
        /// 读取头像信息
        /// </summary>
        public Action<string> OnRead;

        /// <summary>
        /// 上一次发送图片时间
        /// </summary>
        public DateTime lastSendTime = DateTime.Now;

        /// <summary>
        /// 发送图片间隔时间 250毫秒
        /// 每秒 发送 5张图片
        /// </summary>
        public readonly TimeSpan Delay = TimeSpan.FromMilliseconds(250d);

        /// <summary>
        /// 时实图片字节
        /// 开启2M 临时连接空间
        /// </summary>
        public readonly byte[] BitmapBuffer = new byte[2 * 1024 * 1024];

        /// <summary>
        /// 时实图片文内存流
        /// </summary>
        public readonly MemoryStream BitmapStream = new MemoryStream();

        /// <summary>
        /// 构建一个摄像头
        /// </summary>
        /// <param name="videoDevice"></param>
        public Camera(FilterInfo videoInfo)
        {
            try
            {
                this.videoInfo = videoInfo;
                this.videoDevice = new VideoCaptureDevice(this.videoInfo.MonikerString);
            }
            catch (Exception ex)
            {

            }
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
                return this.videoDevice.IsRunning;
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
            if (this.videoDevice != null)
                this.videoDevice.Stop();
            return this.videoDevice.IsRunning ? false : true;
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
