echo off
cls
echo start install script
SET location=%~dp0
echo start install script
echo location : %location:~0,-1%
ping 127.0.0.1 -n 1 > nul
del NULL
echo copy start
If "%PROCESSOR_ARCHITEW6432%" == "AMD64" (copy CoreTracker.x64.exe CoreTracker.exe) else ( copy CoreTracker.x32.exe CoreTracker.exe )
If "%PROCESSOR_ARCHITEW6432%" == "AMD64" (copy Newtonsoft.Json.x64.dll Newtonsoft.Json.dll) else ( copy Newtonsoft.Json.x32.dll Newtonsoft.Json.dll )
If "%PROCESSOR_ARCHITEW6432%" == "AMD64" (copy OpenHardwareMonitorLib.x64.dll OpenHardwareMonitorLib.dll) else ( copy OpenHardwareMonitorLib.x32.dll OpenHardwareMonitorLib.dll )
If "%PROCESSOR_ARCHITEW6432%" == "AMD64" (copy CoreTrackerHelper.x64.exe CoreTrackerHelper.exe) else ( copy CoreTrackerHelper.x32.exe CoreTrackerHelper.exe )
echo copy done
echo all done