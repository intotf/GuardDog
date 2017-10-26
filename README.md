# 小工具集合

## 1、看门狗程序 GuardDog
      1.1 主要应用于服务器上，对特定的一些服务做强制看管，以免被关闭或是系统重启后某些软件或服务不能自动启动；
      1.2 在系统时实监控程序的运行状态，出现异常情况下使用System 帐号自行进行启动服务/运行程序；
      1.3 在监控的软件出现异常情况下，可发送邮件通知及Http 推送消息到指定Web平台；
## 2、网站监控  WebMonitor
      用于多个网站的监控，一但网站出现设定的超时时间或异常情况下，发送邮件通知。
      2.1 可对站点进行添加删除配置；
      2.2 可配置出现异常接收通知的多个邮件；
      2.3 可对软件基础配置（异常超时，监测轮循间隔，出现异常尝试次数，是否发送邮件）；
 ## 3、大文件批量删除  FilesDelete
      用于服务器产生大日志文件或图片等信息进行最有效的批量过滤删除，当文件数超过 100万或更多的时候，通过系统搜索全选进行删除会相当的慢。
      3.1 可对指定目录递归子目录进行过滤文件及空目录进行删除；
      3.2 可配置需删除的多种文件后缀；
      3.3 可配置对空目录是否进行删除；
      3.4 可配置删除目录或相对当前程序的相对路径文件名；
      3.5 可配置删除在N天以前创建的文件；
      3.6 可配置轮训扫描文件间隔时间,最小单位分钟; 
