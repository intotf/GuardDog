var CameraReader = new function () {
    var address = 'ws://127.0.0.1:7788';
    var ws = new jsonWebSocket(address);

    // 连接成功
    ws.onopen = function (e) {
        //获取所有摄像头
        $("#CameraBtn").show();
        ws.invokeApi("GetAllCameras", [], function (data) {
            var Cameras = JSON.parse(data);
            $.each(Cameras, function (index, value, array) {
                $("#camera").append("<option value='" + value + "'>" + value + "</option>");
            });
        });
    };

    // 未安装本机服务时触发
    this.onNoService = function (callback) {
        ws.onclose = function (e) {
            if (callback) callback();
        }
    }

    // 接收摄像头数据
    this.OnReadCamera = function (callBack) {
        ws.bindApi("OnReadCamera", callBack);
    }

    // 打开摄像头
    this.OpenCamera = function (name, callBack) {
        ws.invokeApi("OpenCamera", [name], callBack);
    }

    // 关闭摄像头
    this.CloseCamera = function (name, callBack) {
        ws.invokeApi("CloseCamera", [name], callBack);
    }
};
