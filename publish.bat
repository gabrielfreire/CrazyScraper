@ECHO off
echo Publishing...
dotnet publish -r win-x86 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true
echo Done.