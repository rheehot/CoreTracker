

# CoreTracker 
![GitHub release (latest by date)](https://img.shields.io/github/v/release/Fhwang0926/CoreTracker)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Status: ing](https://img.shields.io/badge/Status-dev_ing-red.svg)](https://github.com/Fhwang0926/CoreTracker)
![CoreTracker-CICD](https://github.com/Fhwang0926/CoreTracker/workflows/CoreTracker-CICD/badge.svg)
![Compile](https://github.com/Fhwang0926/CoreTracker/workflows/Compile/badge.svg?branch=master)

<img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/CoreTracker.png" width="200" style="display: inline-block:">

***
We can see by graphic about the status of Logical Processor

<div>
  <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/form.ico" width="75" style="display: inline-block:">
  <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/cpu.png" width="75" style="display: inline-block:">
  <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/ram.png" width="75" style="display: inline-block:">
  <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/gpu.png" width="75" style="display: inline-block:">
  <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/mainboard.png" width="75" style="display: inline-block:">
</div>


_ _ _

# Download

 - `Release` : [`Latest`](https://github.com/Fhwang0926/CoreTracker/releases)
 
# Summary

- What is it ?
- How work it
- Demo
- Installation
- New Features!

___

## 1. What is it

___

### "Check the status of my computer with Trayicon."

> It's a time when most people learn coding and they use PCs a lot.
> Some developers consider performance.
> Some YouTubers test their benchmarks.
> Some people surf the web or create documents
> 
> Many benchmarks and resource checking programs are not intuitive.
> I need to pop up a new window, and I need to do more.
> 
> I hate it so much.
> 
> In developing as usual.
> As usual, on the Internet.
> Writing the document as usual.
> 
> My computer is
> 
> How busy are you?
> When are you busy?
> When is the load in my work?
> 
> I made it because I wanted to check.
> 
> Do you know when your computer is busy?
> 
> "As usual" is important.

## 2. how work it

> find data => status => display on trayicon

### Display criteria for tray icons

| Status | Color | Example |
| ------ | ------ | ------ |
| 0 <= status < 20 | GREEN | <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/Properties/status/10.ico" width="20"> |
| 20 <= status < 40 | YELLO | <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/Properties/status/20.ico" width="20"> |
| 40 <= status < 60 | ORANGE | <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/Properties/status/40.ico" width="20"> |
| 60 <= status < 80 | ORANGE | <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/Properties/status/60.ico" width="20"> |
| 80 <= status | RED  | <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/Properties/status/80.ico" width="20"> |

### Sample taskbar
![alt text](https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/taskbar.png "Logo Title Text 1")


Function Explain Start

| Name | Action |
| ------ | ------ |
| Auto Update | Auto-update check and notice, if you want to can continue update from recently version |
| Auto Start | Auto start when system turn on|
| CPU Temperature | Displays cpu temperature using trayicon|
| GPU(Graphic) Temperature | Displays GPU(Graphic temperature using trayicon|
| Mainboard Temperature | Displays Mainboard temperature  using trayicon|
| RAM Usage | Displays RAM Usage using trayicon|
| Show TrayIcon | Can enable or disable showing tray icon on the taskbar(is recommended setting)|
| Disabled Busy Alert | Display a notification when CPU utilization is 80% overall (if there is a continuous load, it will be displayed at regular intervals). |
| Refresh speed | trayicon refresh cycle setting : Fast(:=1s) or Normal(:=3s) or Slow(:=5s)|
| [Icon-Menu]Hide | The CoreTracker program Hide main windows |
| [Icon-Menu]Show | The CoreTracker program Show main windows |
| [Icon-Menu]Exit | The CoreTracker program close action |
| [Icon-Menu]Report | find out a bug, or want to a new function, can write on GitHub issue(login required) |
| [Icon-Menu]Update | Custom action for the update to new version|
| [Icon-Menu]Reset | Reset CPUs status watcher |


[CoreTarackerHelper] : excute tray icon refresh when uninstall using

[OpenHardwareMonitor] : Reference dll

Function Explain End
___

## DEMO with youtube (Attention Task bar)


[![CoreTracker](http://img.youtube.com/vi/rdZ1RNOGpvo/0.jpg)](http://www.youtube.com/watch?v=rdZ1RNOGpvo "CoreTracker")

___

## 3. Installation 

1. dependency(support x86 and x64)

> same or higher dotnet framework 3.5

2. Many core(?)  :D :D :D
3. testing tool(if you want to) ~ is optional

### Set Trayicon area manual
> trayicon area setting on windows(can setting toggle on CoreTracker application)
#### 1. Click mouse right button on taskbar
<img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/set_trayicon_1.png" width="300">

#### 2. Click trayicon area setting
<img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/set_trayicon_2.png" width="300">

#### 3. Can set all show tray icon on the taskbar or selected application tray icon on the taskbar
<img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/set_trayicon_3.png" width="300">

___

## 4. New Features

- Auto Bug Report
- memory reduce

___

## 5. REFER
- [NSIS]
- [CoreTarackerHelper]
- [OpenHardwareMonitor]
- [Fhwang0926/upload-release-asset]
- [actions/create-release@v1]
- [actions/download-artifact]
- [actions/upload-artifact]
- [microsoft/setup-msbuild]

[NSIS]: <https://github.com/joncloud/makensis-action>
[CoreTarackerHelper]: <https://github.com/Fhwang0926/CoreTrackerHelper>
[OpenHardwareMonitor]: <https://github.com/openhardwaremonitor/openhardwaremonitor>
[Fhwang0926/upload-release-asset]: <https://github.com/Fhwang0926/upload-release-asset>
[actions/download-artifact]: <https://github.com/actions/download-artifact>
[microsoft/setup-msbuild]: <https://github.com/microsoft/setup-msbuild>
[actions/upload-artifact]: <https://github.com/actions/upload-artifact>
[actions/create-release@v1]: <https://github.com/actions/create-release>

다운로드
https://l.facebook.com/l.php?u=https%3A%2F%2Fgithub.com%2FFhwang0926%2FCoreTracker%2Freleases%2Fdownload%2Fv0.10.3%2FCoreTracker_Installer_x86_x64_v0.10.3.exe%3Ffbclid%3DIwAR1iNM22h_6QZZVnqpMWJstuSYgCXqCbRrgaDyvRmEB7ldeB1Xrdx03t3wM&h=AT1IyXdjC2fdqV9OiLbP4c7dP2oo5pJ9epJ0dEXz-nZ564T83UMY95PlmBO2lJG6yLcb7TejQW0MUkmK1p8BpUdEb-8qK4Q_6s1pm4t07xGVY7tDxNEsmLlJ6pvP7B-rjwl60cdbwhTcjzLk9Vf_BeHGto5UlFlQ73Ru2oayi5Rycov-N_Xtv7gTTpR7VhtP-oUx2FNDjf28NEt1DDoRPOMB15-o6iJvx1-L39w8dkhw52QZzJ4NT2hAhPCCUiflotR6DMIdq1uJVKakIM64-aWC4pm9kiDbP1qKQlvi1dIQKttFHHJTx3t7dFdur62QzPchhB4QbK8poUYk4rflmzXI_4p8YRCelj2i1e6b40PCT9g9-gX4nshna3MfzLu8mOlifqr75VlXpdIg2AtP0z5MQuoJA-z5rhym7oGZb_zxHctaLJ-yRMTgKhEObtUsn8OqspbQ3oP9B2WOm3vVLgeWRLuhX5SNAVCzD5dTnti68-ae20AXNQJgyatOpamqDJrOFFnkrmu8GyMmn5hccUu6y52xs4L1PdvuiQ5FT8mq9ZLR8CkvizepJ5-cRaI4cpBqKHI71Jg7qzSBgvU1Errxrj0mIzu5N6wtu6xodXG11fpn8Vau4-hEbLDsQAMvlLl_aA
