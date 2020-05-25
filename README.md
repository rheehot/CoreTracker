

# CoreTracker

![GitHub release (latest by date)](https://img.shields.io/github/v/release/Fhwang0926/CoreTracker)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Status: ing](https://img.shields.io/badge/Status-ing-red.svg)](https://github.com/Fhwang0926/CoreTracker)

***
We can see by graphic about the status of Logical Processor

<img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/form.ico" width="150">
<img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/img/cpu.png" width="150">

_ _ _

# Summary

- What is it ?
- How work it
- Demo
- Installation
- Support
- How bug report
- New Features!

___

## 1. What is it

___
> now almost people use PC and some others consider them's PC Performance(someone check the benchmark on PC)
> don't forget what is important, performance of system when you using it!!!
> not important just benchmark test
> but usually program is open new windows and check it that performance data in program
> i very very very hate it style
> i just want to see on my computer performance like widget when i using PC
> so i developed it kkkk

### one-sentence summary

> " i just want to see on my computer performance like widget when i using PC "
___

## 2. how work it

> status is usage by logical core
> show core status using of tray icon
> logical core status(usage %) by logical core

| Status | Color | Example |
| ------ | ------ | ------ |
| 0 <= status < 10 | GREEN | <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/Properties/status/10.ico" width="20"> |
| 10 <= status < 20 | YELLO | <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/Properties/status/20.ico" width="20"> |
| 20 <= status < 40 | ORANGE | <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/Properties/status/40.ico" width="20"> |
| 40 <= status < 60 | DEEPORANG | <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/Properties/status/60.ico" width="20"> |
| 60 <= status | READ  | <img src="https://raw.githubusercontent.com/Fhwang0926/CoreTracker/master/Properties/status/80.ico" width="20"> |


Function Explain Start

| Name | Action |
| ------ | ------ |
| Auto Update | Auto-update check and notice, if you want to can continue update from recently version |
| Auto Start | Auto start when system turn on|
| [Icon-Menu]Hide | The CoreTracker program Hide main windows |
| [Icon-Menu]Show | The CoreTracker program Show main windows |
| [Icon-Menu]Exit | The CoreTracker program close action |
| [Icon-Menu]Report | find out a bug, or want to a new function, can write on GitHub issue(login required) |
| [Icon-Menu]Update | Custom action for the update to new version|
| [Icon-Menu]Reset | Reset CPUs status watcher |


[CoreTarackerHelper] : excute tray icon refresh when uninstall using

Function Explain End


___

## DEMO with youtube (Please Attention Task bar)


[![CoreTracker](http://img.youtube.com/vi/rdZ1RNOGpvo/0.jpg)](http://www.youtube.com/watch?v=rdZ1RNOGpvo "CoreTracker")

___

## Installation 

1. dependency(support x86 and x64)

> <= dotnet framework 3.5

2. Many core( ? kkkk )  :D :D :D
3. testing tool(if you want to) ~ is optional

___

## trayicon area setting on windows(can setting toggle on CoreTracker application)

1. mouse right click on taskbar
2. open taskbar setting
3. Click setting is "Select the icon to display on the taskbar"
4. Enable CoreTracker or enable all trayicon(We recommend choosing a wide monitor)

## New Features

- Auto Bug Report
- memory reduce
- Add Korean language


[CoreTarackerHelper]: <https://github.com/Fhwang0926/CoreTrackerHelper>