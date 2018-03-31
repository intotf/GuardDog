using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkSocket.WebSocket;
using NetworkSocket.Core;
using AForge.Video.DirectShow;

namespace CameraApp
{
    /// <summary>
    /// 摄像头WebSocket接口
    /// </summary>
    public class CameraApi : JsonWebSocketApiService
    {
        private readonly CameraCollection Instance = CameraCollection.Instance;

        /// <summary>
        /// 读取到摄像头图片
        /// </summary>
        /// <param name="fpBase64">摄像头贞图片</param>
        [Api]
        public static void CameraReader_OnRead(CameraResult data)
        {
            var clients = Listener.WebSocketListener.SessionManager.FilterWrappers<JsonWebSocketSession>().ToArray();
            foreach (var item in clients)
            {
                try
                {
                    item.InvokeApi("OnReadCamera", data);
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// 获取所有摄像头信息
        /// </summary>
        /// <returns></returns>
        [Api]
        public string GetAllCameras()
        {
            return JsonSerializer.Serialize(Instance.Cameras.Select(item => item.videoInfo.Name).ToList());
        }

        /// <summary>
        /// 打开摄像头
        /// </summary>
        /// <param name="CameraName">打开指定摄像头</param>
        /// <returns></returns>
        [Api]
        public bool OpenCamera(string CameraName)
        {
            var Camera = Instance.Cameras.Where(item => item.videoInfo.Name == CameraName).FirstOrDefault();
            if (Camera != null)
            {
                return Camera.StartCamera();
            }
            return false;
        }

        /// <summary>
        /// 关闭当前摄像头
        /// </summary>
        [Api]
        public bool CloseCamera(string CameraName)
        {
            var Camera = Instance.Cameras.Where(item => item.videoInfo.Name == CameraName).FirstOrDefault();
            if (Camera != null)
            {
                return Camera.StopCamera();
            }
            return false;
        }
    }
}
