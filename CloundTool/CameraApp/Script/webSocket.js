function jsonWebSocket(n){function i(n){return!!n&&typeof n=="function"}function o(n){for(var i,t=0;t<u.length;t++)if(i=u[t],i.id===n)return u.splice(t,1),i.callback}function s(n){var t=JSON.parse(n.data),r,u;if(!t.fromClient){h.apply(this,[t]);return}(r=o(t.id),r)&&(u=t.state?r.doneFunc:r.exFunc,i(u)&&u(t.body))}function h(n){var t=e[n.api.toLowerCase()],r;if(!i(t)){f(n,"请求的api不存在："+t);return}try{r=t.apply(this,n.body);r!==undefined&&c(n,r)}catch(u){f(n,u.message)}}function f(n,i){n.state=!1;n.body=i;var r=JSON.stringify(n);t.send(r)}function c(n,i){n.body=i;var r=JSON.stringify(n);t.send(r)}function l(){var i=this;t=new(window.WebSocket||window.MozWebSocket)(n);t.onclose=function(n){i.connected=!1;i.onclose(n)};t.onopen=function(n){i.connected=!0;i.onopen(n)};t.onmessage=function(n){s.apply(i,[n])}}var t,r=0,u=[],e={};this.connected=!1;this.onclose=function(){};this.onopen=function(){};this.bindApi=function(n,t){return typeof n!="string"||!n||!i(t)?!1:(e[n.toLowerCase()]=t,!0)};this.invokeApi=function(n,f,e,o){if(this.connected===!1)return!1;r=r+1;var s={api:n,id:r,body:f||[]},h=JSON.stringify(s);return(i(e)||i(o))&&u.push({id:r,callback:{doneFunc:e,exFunc:o}}),t.send(h),!0};l.apply(this)}typeof JSON!="object"&&(JSON={}),function(){"use strict";function i(n){return n<10?"0"+n:n}function f(){return this.valueOf()}function e(n){return o.lastIndex=0,o.test(n)?'"'+n.replace(o,function(n){var t=h[n];return typeof t=="string"?t:"\\u"+("0000"+n.charCodeAt(0).toString(16)).slice(-4)})+'"':'"'+n+'"'}function r(i,f){var s,l,h,a,v=n,c,o=f[i];o&&typeof o=="object"&&typeof o.toJSON=="function"&&(o=o.toJSON(i));typeof t=="function"&&(o=t.call(f,i,o));switch(typeof o){case"string":return e(o);case"number":return isFinite(o)?String(o):"null";case"boolean":case"null":return String(o);case"object":if(!o)return"null";if(n+=u,c=[],Object.prototype.toString.apply(o)==="[object Array]"){for(a=o.length,s=0;s<a;s+=1)c[s]=r(s,o)||"null";return h=c.length===0?"[]":n?"[\n"+n+c.join(",\n"+n)+"\n"+v+"]":"["+c.join(",")+"]",n=v,h}if(t&&typeof t=="object")for(a=t.length,s=0;s<a;s+=1)typeof t[s]=="string"&&(l=t[s],h=r(l,o),h&&c.push(e(l)+(n?": ":":")+h));else for(l in o)Object.prototype.hasOwnProperty.call(o,l)&&(h=r(l,o),h&&c.push(e(l)+(n?": ":":")+h));return h=c.length===0?"{}":n?"{\n"+n+c.join(",\n"+n)+"\n"+v+"}":"{"+c.join(",")+"}",n=v,h}}var c=/^[\],:{}\s]*$/,l=/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g,a=/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g,v=/(?:^|:|,)(?:\s*\[)+/g,o=/[\\\"\u0000-\u001f\u007f-\u009f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g,s=/[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g,n,u,h,t;typeof Date.prototype.toJSON!="function"&&(Date.prototype.toJSON=function(){return isFinite(this.valueOf())?this.getUTCFullYear()+"-"+i(this.getUTCMonth()+1)+"-"+i(this.getUTCDate())+"T"+i(this.getUTCHours())+":"+i(this.getUTCMinutes())+":"+i(this.getUTCSeconds())+"Z":null},Boolean.prototype.toJSON=f,Number.prototype.toJSON=f,String.prototype.toJSON=f);typeof JSON.stringify!="function"&&(h={"\b":"\\b","\t":"\\t","\n":"\\n","\f":"\\f","\r":"\\r",'"':'\\"',"\\":"\\\\"},JSON.stringify=function(i,f,e){var o;if(n="",u="",typeof e=="number")for(o=0;o<e;o+=1)u+=" ";else typeof e=="string"&&(u=e);if(t=f,f&&typeof f!="function"&&(typeof f!="object"||typeof f.length!="number"))throw new Error("JSON.stringify");return r("",{"":i})});typeof JSON.parse!="function"&&(JSON.parse=function(text,reviver){function walk(n,t){var r,u,i=n[t];if(i&&typeof i=="object")for(r in i)Object.prototype.hasOwnProperty.call(i,r)&&(u=walk(i,r),u!==undefined?i[r]=u:delete i[r]);return reviver.call(n,t,i)}var j;if(text=String(text),s.lastIndex=0,s.test(text)&&(text=text.replace(s,function(n){return"\\u"+("0000"+n.charCodeAt(0).toString(16)).slice(-4)})),c.test(text.replace(l,"@").replace(a,"]").replace(v,"")))return j=eval("("+text+")"),typeof reviver=="function"?walk({"":j},""):j;throw new SyntaxError("JSON.parse");})}();var swfobject=function(){function a(){var i,r,n;if(!l){try{i=t.getElementsByTagName("body")[0].appendChild(o("span"));i.parentNode.removeChild(i)}catch(u){return}for(l=!0,r=b.length,n=0;n<r;n++)b[n]()}}function st(n){l?n():b[b.length]=n}function ht(n){if(typeof r.addEventListener!=i)r.addEventListener("load",n,!1);else if(typeof t.addEventListener!=i)t.addEventListener("load",n,!1);else if(typeof r.attachEvent!=i)ri(r,"onload",n);else if(typeof r.onload=="function"){var u=r.onload;r.onload=function(){u();n()}}else r.onload=n}function dt(){wt?gt():nt()}function gt(){var s=t.getElementsByTagName("body")[0],u=o(f),r,e;u.setAttribute("type",w);r=s.appendChild(u);r?(e=0,function(){if(typeof r.GetVariable!=i){var t=r.GetVariable("$version");t&&(t=t.split(" ")[1].split(","),n.pv=[parseInt(t[0],10),parseInt(t[1],10),parseInt(t[2],10)])}else if(e<10){e++;setTimeout(arguments.callee,10);return}s.removeChild(u);r=null;nt()}()):nt()}function nt(){var y=h.length,r,t,f,l,a;if(y>0)for(r=0;r<y;r++){var e=h[r].id,o=h[r].callbackFn,s={success:!1,id:e};if(n.pv[0]>0){if(t=u(e),t)if(!p(h[r].swfVersion)||n.wk&&n.wk<312)if(h[r].expressInstall&&it()){f={};f.data=h[r].expressInstall;f.width=t.getAttribute("width")||"0";f.height=t.getAttribute("height")||"0";t.getAttribute("class")&&(f.styleclass=t.getAttribute("class"));t.getAttribute("align")&&(f.align=t.getAttribute("align"));var w={},v=t.getElementsByTagName("param"),b=v.length;for(l=0;l<b;l++)v[l].getAttribute("name").toLowerCase()!="movie"&&(w[v[l].getAttribute("name")]=v[l].getAttribute("value"));rt(f,w,e,o)}else ni(t),o&&o(s);else c(e,!0),o&&(s.success=!0,s.ref=tt(e),o(s))}else c(e,!0),o&&(a=tt(e),a&&typeof a.SetVariable!=i&&(s.success=!0,s.ref=a),o(s))}}function tt(n){var r=null,t=u(n),e;return t&&t.nodeName=="OBJECT"&&(typeof t.SetVariable!=i?r=t:(e=t.getElementsByTagName(f)[0],e&&(r=e))),r}function it(){return!g&&p("6.0.65")&&(n.win||n.mac)&&!(n.wk&&n.wk<312)}function rt(f,e,s,h){var c,v,l,a;g=!0;et=h||null;bt={success:!1,id:s};c=u(s);c&&(c.nodeName=="OBJECT"?(y=ut(c),d=null):(y=c,d=s),f.id=yt,(typeof f.width==i||!/%$/.test(f.width)&&parseInt(f.width,10)<310)&&(f.width="310"),(typeof f.height==i||!/%$/.test(f.height)&&parseInt(f.height,10)<137)&&(f.height="137"),t.title=t.title.slice(0,47)+" - Flash Player Installation",v=n.ie&&n.win?"ActiveX":"PlugIn",l="MMredirectURL="+r.location.toString().replace(/&/g,"%26")+"&MMplayerType="+v+"&MMdoctitle="+t.title,typeof e.flashvars!=i?e.flashvars+="&"+l:e.flashvars=l,n.ie&&n.win&&c.readyState!=4&&(a=o("div"),s+="SWFObjectNew",a.setAttribute("id",s),c.parentNode.insertBefore(a,c),c.style.display="none",function(){c.readyState==4?c.parentNode.removeChild(c):setTimeout(arguments.callee,10)}()),ft(f,e,s))}function ni(t){if(n.ie&&n.win&&t.readyState!=4){var i=o("div");t.parentNode.insertBefore(i,t);i.parentNode.replaceChild(ut(t),i);t.style.display="none",function(){t.readyState==4?t.parentNode.removeChild(t):setTimeout(arguments.callee,10)}()}else t.parentNode.replaceChild(ut(t),t)}function ut(t){var u=o("div"),e,i,s,r;if(n.win&&n.ie)u.innerHTML=t.innerHTML;else if(e=t.getElementsByTagName(f)[0],e&&(i=e.childNodes,i))for(s=i.length,r=0;r<s;r++)i[r].nodeType==1&&i[r].nodeName=="PARAM"||i[r].nodeType==8||u.appendChild(i[r].cloneNode(!0));return u}function ft(t,r,e){var v,y=u(e),p,s,b,a,c,h,l;if(n.wk&&n.wk<312)return v;if(y)if(typeof t.id==i&&(t.id=e),n.ie&&n.win){p="";for(s in t)t[s]!=Object.prototype[s]&&(s.toLowerCase()=="data"?r.movie=t[s]:s.toLowerCase()=="styleclass"?p+=' class="'+t[s]+'"':s.toLowerCase()!="classid"&&(p+=" "+s+'="'+t[s]+'"'));b="";for(a in r)r[a]!=Object.prototype[a]&&(b+='<param name="'+a+'" value="'+r[a]+'" />');y.outerHTML='<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"'+p+">"+b+"<\/object>";k[k.length]=t.id;v=u(t.id)}else{c=o(f);c.setAttribute("type",w);for(h in t)t[h]!=Object.prototype[h]&&(h.toLowerCase()=="styleclass"?c.setAttribute("class",t[h]):h.toLowerCase()!="classid"&&c.setAttribute(h,t[h]));for(l in r)r[l]!=Object.prototype[l]&&l.toLowerCase()!="movie"&&ti(c,l,r[l]);y.parentNode.replaceChild(c,y);v=c}return v}function ti(n,t,i){var r=o("param");r.setAttribute("name",t);r.setAttribute("value",i);n.appendChild(r)}function ct(t){var i=u(t);i&&i.nodeName=="OBJECT"&&(n.ie&&n.win?(i.style.display="none",function(){i.readyState==4?ii(t):setTimeout(arguments.callee,10)}()):i.parentNode.removeChild(i))}function ii(n){var t=u(n),i;if(t){for(i in t)typeof t[i]=="function"&&(t[i]=null);t.parentNode.removeChild(t)}}function u(n){var i=null;try{i=t.getElementById(n)}catch(r){}return i}function o(n){return t.createElement(n)}function ri(n,t,i){n.attachEvent(t,i);v[v.length]=[n,t,i]}function p(t){var r=n.pv,i=t.split(".");return i[0]=parseInt(i[0],10),i[1]=parseInt(i[1],10)||0,i[2]=parseInt(i[2],10)||0,r[0]>i[0]||r[0]==i[0]&&r[1]>i[1]||r[0]==i[0]&&r[1]==i[1]&&r[2]>=i[2]?!0:!1}function lt(r,u,s,h){var a,c,l;n.ie&&n.mac||(a=t.getElementsByTagName("head")[0],a)&&(c=s&&typeof s=="string"?s:"screen",h&&(e=null,ot=null),e&&ot==c||(l=o("style"),l.setAttribute("type","text/css"),l.setAttribute("media",c),e=a.appendChild(l),n.ie&&n.win&&typeof t.styleSheets!=i&&t.styleSheets.length>0&&(e=t.styleSheets[t.styleSheets.length-1]),ot=c),n.ie&&n.win?e&&typeof e.addRule==f&&e.addRule(r,u):e&&typeof t.createTextNode!=i&&e.appendChild(t.createTextNode(r+" {"+u+"}")))}function c(n,t){if(kt){var i=t?"visible":"hidden";l&&u(n)?u(n).style.visibility=i:lt("#"+n,"visibility:"+i)}}function at(n){var t=/[\\\"<>\.;]/.exec(n)!=null;return t&&typeof encodeURIComponent!=i?encodeURIComponent(n):n}var i="undefined",f="object",vt="Shockwave Flash",ui="ShockwaveFlash.ShockwaveFlash",w="application/x-shockwave-flash",yt="SWFObjectExprInst",pt="onreadystatechange",r=window,t=document,s=navigator,wt=!1,b=[dt],h=[],k=[],v=[],y,d,et,bt,l=!1,g=!1,e,ot,kt=!0,n=function(){var l=typeof t.getElementById!=i&&typeof t.getElementsByTagName!=i&&typeof t.createElement!=i,e=s.userAgent.toLowerCase(),o=s.platform.toLowerCase(),a=o?/win/.test(o):/win/.test(e),v=o?/mac/.test(o):/mac/.test(e),y=/webkit/.test(e)?parseFloat(e.replace(/^.*webkit\/(\d+(\.\d+)?).*$/,"$1")):!1,h=!+"\v1",u=[0,0,0],n=null,c;if(typeof s.plugins!=i&&typeof s.plugins[vt]==f)n=s.plugins[vt].description,n&&(typeof s.mimeTypes==i||!s.mimeTypes[w]||s.mimeTypes[w].enabledPlugin)&&(wt=!0,h=!1,n=n.replace(/^.*\s+(\S+\s+\S+$)/,"$1"),u[0]=parseInt(n.replace(/^(.*)\..*$/,"$1"),10),u[1]=parseInt(n.replace(/^.*\.(.*)\s.*$/,"$1"),10),u[2]=/[a-zA-Z]/.test(n)?parseInt(n.replace(/^.*[a-zA-Z]+(.*)$/,"$1"),10):0);else if(typeof r.ActiveXObject!=i)try{c=new ActiveXObject(ui);c&&(n=c.GetVariable("$version"),n&&(h=!0,n=n.split(" ")[1].split(","),u=[parseInt(n[0],10),parseInt(n[1],10),parseInt(n[2],10)]))}catch(p){}return{w3:l,pv:u,wk:y,ie:h,win:a,mac:v}}(),fi=function(){n.w3&&((typeof t.readyState!=i&&t.readyState=="complete"||typeof t.readyState==i&&(t.getElementsByTagName("body")[0]||t.body))&&a(),l||(typeof t.addEventListener!=i&&t.addEventListener("DOMContentLoaded",a,!1),n.ie&&n.win&&(t.attachEvent(pt,function(){t.readyState=="complete"&&(t.detachEvent(pt,arguments.callee),a())}),r==top&&function(){if(!l){try{t.documentElement.doScroll("left")}catch(n){setTimeout(arguments.callee,0);return}a()}}()),n.wk&&function(){if(!l){if(!/loaded|complete/.test(t.readyState)){setTimeout(arguments.callee,0);return}a()}}(),ht(a)))}(),ei=function(){n.ie&&n.win&&window.attachEvent("onunload",function(){for(var e=v.length,r,i,u,f,t=0;t<e;t++)v[t][0].detachEvent(v[t][1],v[t][2]);for(r=k.length,i=0;i<r;i++)ct(k[i]);for(u in n)n[u]=null;n=null;for(f in swfobject)swfobject[f]=null;swfobject=null})}();return{registerObject:function(t,i,r,u){if(n.w3&&t&&i){var f={};f.id=t;f.swfVersion=i;f.expressInstall=r;f.callbackFn=u;h[h.length]=f;c(t,!1)}else u&&u({success:!1,id:t})},getObjectById:function(t){if(n.w3)return tt(t)},embedSWF:function(t,r,u,e,o,s,h,l,a,v){var y={success:!1,id:r};n.w3&&!(n.wk&&n.wk<312)&&t&&r&&u&&e&&o?(c(r,!1),st(function(){var n,k,w,d,b,g;if(u+="",e+="",n={},a&&typeof a===f)for(k in a)n[k]=a[k];if(n.data=t,n.width=u,n.height=e,w={},l&&typeof l===f)for(d in l)w[d]=l[d];if(h&&typeof h===f)for(b in h)typeof w.flashvars!=i?w.flashvars+="&"+b+"="+h[b]:w.flashvars=b+"="+h[b];if(p(o))g=ft(n,w,r),n.id==r&&c(r,!0),y.success=!0,y.ref=g;else{if(s&&it()){n.data=s;rt(n,w,r,v);return}c(r,!0)}v&&v(y)})):v&&v(y)},switchOffAutoHideShow:function(){kt=!1},ua:n,getFlashPlayerVersion:function(){return{major:n.pv[0],minor:n.pv[1],release:n.pv[2]}},hasFlashPlayerVersion:p,createSWF:function(t,i,r){return n.w3?ft(t,i,r):undefined},showExpressInstall:function(t,i,r,u){n.w3&&it()&&rt(t,i,r,u)},removeSWF:function(t){n.w3&&ct(t)},createCSS:function(t,i,r,u){n.w3&&lt(t,i,r,u)},addDomLoadEvent:st,addLoadEvent:ht,getQueryParamValue:function(n){var r=t.location.search||t.location.hash,u,i;if(r){if(/\?/.test(r)&&(r=r.split("?")[1]),n==null)return at(r);for(u=r.split("&"),i=0;i<u.length;i++)if(u[i].substring(0,u[i].indexOf("="))==n)return at(u[i].substring(u[i].indexOf("=")+1))}return""},expressInstallCallback:function(){if(g){var t=u(yt);t&&y&&(t.parentNode.replaceChild(y,t),d&&(c(d,!0),n.ie&&n.win&&(y.style.display="block")),et&&et(bt));g=!1}}}}();(function(){if(!window.WEB_SOCKET_FORCE_FLASH){if(window.WebSocket)return;if(window.MozWebSocket){window.WebSocket=MozWebSocket;return}}var n;if(n=window.WEB_SOCKET_LOGGER?WEB_SOCKET_LOGGER:window.console&&window.console.log&&window.console.error?window.console:{log:function(){},error:function(){}},swfobject.getFlashPlayerVersion().major<10){n.error("Flash Player >= 10.0.0 is required.");return}location.protocol=="file:"&&n.error("WARNING: web-socket-js doesn't work in file:///... URL unless you set Flash Security Settings properly. Open the page via Web server i.e. http://...");window.WebSocket=function(n,t,i,r,u){var f=this;f.__id=WebSocket.__nextId++;WebSocket.__instances[f.__id]=f;f.readyState=WebSocket.CONNECTING;f.bufferedAmount=0;f.__events={};t?typeof t=="string"&&(t=[t]):t=[];f.__createTask=setTimeout(function(){WebSocket.__addTask(function(){f.__createTask=null;WebSocket.__flash.create(f.__id,n,t,i||null,r||0,u||null)})},0)};WebSocket.prototype.send=function(n){if(this.readyState==WebSocket.CONNECTING)throw"INVALID_STATE_ERR: Web Socket connection has not been established";var t=WebSocket.__flash.send(this.__id,encodeURIComponent(n));return t<0?!0:(this.bufferedAmount+=t,!1)};WebSocket.prototype.close=function(){if(this.__createTask){clearTimeout(this.__createTask);this.__createTask=null;this.readyState=WebSocket.CLOSED;return}this.readyState!=WebSocket.CLOSED&&this.readyState!=WebSocket.CLOSING&&(this.readyState=WebSocket.CLOSING,WebSocket.__flash.close(this.__id))};WebSocket.prototype.addEventListener=function(n,t){n in this.__events||(this.__events[n]=[]);this.__events[n].push(t)};WebSocket.prototype.removeEventListener=function(n,t){var r,i;if(n in this.__events)for(r=this.__events[n],i=r.length-1;i>=0;--i)if(r[i]===t){r.splice(i,1);break}};WebSocket.prototype.dispatchEvent=function(n){for(var r=this.__events[n.type]||[],t,i=0;i<r.length;++i)r[i](n);t=this["on"+n.type];t&&t.apply(this,[n])};WebSocket.prototype.__handleEvent=function(n){var t,i;if("readyState"in n&&(this.readyState=n.readyState),"protocol"in n&&(this.protocol=n.protocol),n.type=="open"||n.type=="error")t=this.__createSimpleEvent(n.type);else if(n.type=="close")t=this.__createSimpleEvent("close"),t.wasClean=n.wasClean?!0:!1,t.code=n.code,t.reason=n.reason;else if(n.type=="message")i=decodeURIComponent(n.message),t=this.__createMessageEvent("message",i);else throw"unknown event type: "+n.type;this.dispatchEvent(t)};WebSocket.prototype.__createSimpleEvent=function(n){if(document.createEvent&&window.Event){var t=document.createEvent("Event");return t.initEvent(n,!1,!1),t}return{type:n,bubbles:!1,cancelable:!1}};WebSocket.prototype.__createMessageEvent=function(n,t){if(window.MessageEvent&&typeof MessageEvent=="function"&&!window.opera)return new MessageEvent("message",{view:window,bubbles:!1,cancelable:!1,data:t});if(document.createEvent&&window.MessageEvent&&!window.opera){var i=document.createEvent("MessageEvent");return i.initMessageEvent("message",!1,!1,t,null,null,window,null),i}return{type:n,data:t,bubbles:!1,cancelable:!1}};WebSocket.CONNECTING=0;WebSocket.OPEN=1;WebSocket.CLOSING=2;WebSocket.CLOSED=3;WebSocket.__isFlashImplementation=!0;WebSocket.__initialized=!1;WebSocket.__flash=null;WebSocket.__instances={};WebSocket.__tasks=[];WebSocket.__nextId=0;WebSocket.loadFlashPolicyFile=function(n){WebSocket.__addTask(function(){WebSocket.__flash.loadManualPolicyFile(n)})};WebSocket.__initialize=function(){var i,t,r;if(!WebSocket.__initialized){if(WebSocket.__initialized=!0,WebSocket.__swfLocation&&(window.WEB_SOCKET_SWF_LOCATION=WebSocket.__swfLocation),!window.WEB_SOCKET_SWF_LOCATION){n.error("[WebSocket] set WEB_SOCKET_SWF_LOCATION to location of WebSocketMain.swf");return}window.WEB_SOCKET_SUPPRESS_CROSS_DOMAIN_SWF_ERROR||WEB_SOCKET_SWF_LOCATION.match(/(^|\/)WebSocketMainInsecure\.swf(\?.*)?$/)||!WEB_SOCKET_SWF_LOCATION.match(/^\w+:\/\/([^\/]+)/)||(i=RegExp.$1,location.host!=i&&n.error("[WebSocket] You must host HTML and WebSocketMain.swf in the same host ('"+location.host+"' != '"+i+"'). See also 'How to host HTML file and SWF file in different domains' section in README.md. If you use WebSocketMainInsecure.swf, you can suppress this message by WEB_SOCKET_SUPPRESS_CROSS_DOMAIN_SWF_ERROR = true;"));t=document.createElement("div");t.id="webSocketContainer";t.style.position="absolute";WebSocket.__isFlashLite()?(t.style.left="0px",t.style.top="0px"):(t.style.left="-100px",t.style.top="-100px");r=document.createElement("div");r.id="webSocketFlash";t.appendChild(r);document.body.appendChild(t);swfobject.embedSWF(WEB_SOCKET_SWF_LOCATION,"webSocketFlash","1","1","10.0.0",null,null,{hasPriority:!0,swliveconnect:!0,allowScriptAccess:"always"},null,function(t){t.success||n.error("[WebSocket] swfobject.embedSWF failed")})}};WebSocket.__onFlashInitialized=function(){setTimeout(function(){WebSocket.__flash=document.getElementById("webSocketFlash");WebSocket.__flash.setCallerUrl(location.href);WebSocket.__flash.setDebug(!!window.WEB_SOCKET_DEBUG);for(var n=0;n<WebSocket.__tasks.length;++n)WebSocket.__tasks[n]();WebSocket.__tasks=[]},0)};WebSocket.__onFlashEvent=function(){return setTimeout(function(){var i,t;try{for(i=WebSocket.__flash.receiveEvents(),t=0;t<i.length;++t)WebSocket.__instances[i[t].webSocketId].__handleEvent(i[t])}catch(r){n.error(r)}},0),!0};WebSocket.__log=function(t){n.log(decodeURIComponent(t))};WebSocket.__error=function(t){n.error(decodeURIComponent(t))};WebSocket.__addTask=function(n){WebSocket.__flash?n():WebSocket.__tasks.push(n)};WebSocket.__isFlashLite=function(){if(!window.navigator||!window.navigator.mimeTypes)return!1;var n=window.navigator.mimeTypes["application/x-shockwave-flash"];return!n||!n.enabledPlugin||!n.enabledPlugin.filename?!1:n.enabledPlugin.filename.match(/flashlite/i)?!0:!1};window.WEB_SOCKET_DISABLE_AUTO_INITIALIZATION||swfobject.addDomLoadEvent(function(){WebSocket.__initialize()})})(),function(n){function t(){var t=document.getElementsByTagName("script"),n=t[t.length-1].src,i=n.split("/")[n.split("/").length-1];return n.replace(i,"")}n.WEB_SOCKET_SWF_LOCATION=t()+"webSocket.swf";n.WEB_SOCKET_DEBUG=!1}(window)