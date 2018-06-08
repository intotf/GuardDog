var _insertText = '<input type="submit" value="我是Web提交按钮" height="40" onclick="_InjectionAlert()"><input type="button" id="su" value="点我试试" class="bg s_btn" onclick="_Notify()">';
function _Notify() {
    window.external.SendNotify(document.title);
}

function _InjectionHtml() {
    //document.body.innerHTML += _insertText;
    $(".tdw-logo-a").html(_insertText);
}

_InjectionHtml();

function _InjectionAlert() {
    alert("浏览器触发了Web 的js 方法 _InjectionAlert().");
}