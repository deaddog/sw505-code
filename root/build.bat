REM Check MsBuild is available
 
SET MSBUILD=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\MsBuild.exe
IF NOT EXIST "%MSBUILD%" GOTO NOMSB

"%MSBUILD%" Master.sln /t:build /p:configuration=Debug /v:q
PAUSE 
GOTO :EOF

:NOMSB
echo. 
echo MSBUILD not found 
echo. 
GOTO :EOF 