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
        private CameraCollection Instance = CameraCollection.Instance;

        /// <summary>
        /// 获取所有摄像头信息
        /// </summary>
        /// <returns></returns>
        [Api]
        public string GetAllCameras()
        {
            return JsonSerializer.Serialize(Instance.CamerasCollection);
        }

        /// <summary>
        /// 打开摄像头
        /// </summary>
        /// <param name="CameraName">打开指定摄像头</param>
        /// <returns></returns>
        [Api]
        public bool OpenCamera(string CameraName)
        {
            for (int i = 0; i < Instance.CamerasCollection.Count; i++)
            {
                if (Instance.CamerasCollection[i].Name.Equals(CameraName, StringComparison.Ordinal))
                {
                    return Instance.SetCamera(Instance.CamerasCollection[i]).StartCamera();
                }
            }
            return false;
        }

        /// <summary>
        /// 关闭当前摄像头
        /// </summary>
        [Api]
        public bool CloseCamera()
        {
            return Instance.StopCamera();
        }
    }
}
