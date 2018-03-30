var CameraReader = new function () {
    var address = 'ws://127.0.0.1:7788';
    var ws = new jsonWebSocket(address);

    // 连接成功
    ws.onopen = function (e) {
        alert("连接成功.");
        var s = ws.invokeApi("GetAllCameras", []);
        console.log(s);
    };

    // 未安装本机服务时触发
    this.onNoService = function (callback) {
        ws.onclose = function (e) {
            if (callback) callback();
        }
    }

    // 身份证读取时触发
    this.GetAllCameras = function (callBack) {
        ws.bindApi("GetAllCameras", callBack);
    }

};
