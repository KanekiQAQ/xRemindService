# xRemindService

一个可以自定义提醒标题、内容、时间的项目。

因为想每个小时提醒自己喝水就写了一个项目来搞这个。

# 使用说明

在`appsettings.json`中含有默认的提醒，可以随意添加

```JSON
"CronJob": {
    "RelaxJob": {
      "Title": "休息提醒",
      "CronScheduler": "0 0 * * * ?",
      "RemindString": "整点了休息一下吧!!!"
    },
    "GoodJob": {
      "Title": "工作提醒",
      "CronScheduler": "0 2 * * * ?",
      "RemindString": "该努力了!!!"
    }
  }
```

- `RelaxJob`是 Job 名称可随意定义，包含三个参数`Title`、`CronScheduler`、`RemindString`
  - `Title`是提醒标题 **必填**
  - `CronScheduler`是提醒时间 Cron 表达式 **必填**
  - `RemindString`是提醒内容 **必填**

# 安装

可以安装成服务这样不用每天都启动项目了

1. 项目发布
2. 我选择的是
   - 配置：Release|Any CPU
   - 目标框架：net5.0
   - 部署模式：独立
   - 目标运行时：win-x64
   - 文件发布选项：勾选 `生成单个文件`
3. 本人提供一个简单的安装方式
   - 在发布目录下新建两个 bat 文件，一个`setup.bat` 一个`unsetup.bat`内容如下：
     - `setup.bat`
       ```
       @echo off
       set path=%~dp0
       echo %path%
       sc.exe create xRemindService binPath=%path%xRemindService.exe displayname= xRemindService
       net start xRemindService
       sc.exe config xRemindService start= AUTO
       pause
       ```
     - `unsetup.bat`
       ```
       @echo off
       sc.exe stop xRemindService
       sc.exe delete xRemindService
       pause
       ```
   - 通过管理员运行`setup.bat`进行安装服务，`unsetup.bat`进行卸载服务
   - 如果修改配置文件需要重启服务

# TO DO

- [ ] 新增 Web 添加 Job (替代原有`appsettings.json`的配置) **不一定做不做**
