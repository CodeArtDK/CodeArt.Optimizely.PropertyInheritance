@ECHO OFF
SETLOCAL

SET CONFIGURATION=Debug

IF "%2"=="Release" (SET CONFIGURATION=Release)

@ECHO Packaging in %CONFIGURATION%

powershell ./build/pack.ps1 -configuration %CONFIGURATION%

EXIT /B %errorlevel%
