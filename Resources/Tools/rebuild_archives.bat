@echo off
REM This script generates the files.7z, extras.7z, and themes.7z needed for the Rectify11 installer

REM load command line arguments
set SolutionDir=%1
set ProjectDir=%2

REM Validate command line
if not exist %SolutionDir% (
echo bad solution directory argument
exit /b 1
)

if not exist %ProjectDir% (
echo bad project directory argument
exit /b 1
goto exit
)

REM Check if we already built them
if not exist "%ProjectDir%Resources\files.7z" (
goto build_archives
)
if not exist "%ProjectDir%Resources\extras.7z" (
goto build_archives
)
if not exist "%ProjectDir%Resources\themes.7z" (
goto build_archives
)
if exist "%SolutionDir%Resources\DoNotBuild.txt" (
echo Skipping building archives as DoNotBuild.txt exists
goto exit
)

REM build the archives
:build_archives
echo building archives as DoNotBuild.txt doesn't exist
cd /D "%SolutionDir%Resources\Files\"
%SolutionDir%Resources\Tools\7z.exe a "%ProjectDir%Resources\files.7z" * -mx9 -ssp
if not %ERRORLEVEL% == 0 (
echo Failed to compress files.7z
exit /b
)
cd /D "%SolutionDir%Resources\Extras\"
%SolutionDir%Resources\Tools\7z.exe a "%ProjectDir%Resources\extras.7z" * -mx9 -ssp
if not %ERRORLEVEL% == 0 (
echo Failed to compress extras.7z
exit /b
)
cd /D "%SolutionDir%Resources\Themes\"
%SolutionDir%Resources\Tools\7z.exe a "%ProjectDir%Resources\themes.7z" * -mx9 -ssp
if not %ERRORLEVEL% == 0 (
echo Failed to compress themes.7z
exit /b
)

echo "Delete this file to recompress the archives" > %SolutionDir%Resources\DoNotBuild.txt

:exit