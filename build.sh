#!/bin/sh
mkdir /home/reflex/consolenot/
dotnet publish /home/reflex/Shit/C#/ConsoleNot/ConsoleNot -r linux-x64 --self-contained false -p:PublishSingleFile=true --output /home/reflex/consolenot/
rm /home/reflex/consolenot/ConsoleNot.pdb
dotnet publish /home/reflex/Shit/C#/ConsoleNot/ConsoleNot -r win-x64 --self-contained false -p:PublishSingleFile=true --output /home/reflex/consolenot/
rm /home/reflex/consolenot/ConsoleNot.pdb
touch /home/reflex/consolenot/README.txt
echo "Execute (in console) 'ConsoleNot' file for Linux or 'ConsoleNot.exe' for Windows." >> /home/reflex/consolenot/README.txt
zip -r /home/reflex/consolenot.zip /home/reflex/consolenot/ -j
scp /home/reflex/consolenot.zip reflex@192.168.88.40:/home/reflex/ConsoleNotSite/ConsoleNotSite/ConsoleNotSite/wwwroot/