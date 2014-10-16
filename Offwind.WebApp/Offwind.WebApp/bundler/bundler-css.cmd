@echo off
pushd "%~dp0"
set process=..\..\..\bundler\node.exe ..\..\..\bundler\bundler.js

:: process Content and Scripts by default
if "%*" == "" (
    %process% ../static
    %process% ../app-js
) else (
    node bundler.js %*
)

popd
echo  %ERRORLEVEL%
pause
::exit %ERRORLEVEL%
