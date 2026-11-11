@echo off
setlocal

set CSC="C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe"
set OUTDIR=bin

set OPTS=/nologo /platform:x64 /optimize+ /debug- /warn:4

if not exist %OUTDIR% mkdir %OUTDIR%

echo Building ime-state...
%CSC% %OPTS% /target:exe /out:%OUTDIR%\ime-state.exe ime-state.cs
if errorlevel 1 goto :error

echo.
echo Build succeeded.
echo Output: %CD%\%OUTDIR%\ime-state.exe
goto :end

:error
echo.
echo Build failed.
exit /b 1

:end
endlocal
