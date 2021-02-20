#!/bin/sh
mkdir /home/reflex/consolenot/
dotnet publish /home/reflex/Shit/C#/consolenot/Main -r linux-x64 -f netcoreapp3.1 --self-contained false -p:PublishSingleFile=true --configuration Release --output /home/reflex/consolenot/
rm /home/reflex/consolenot/ConsoleNot.pdb
dotnet publish /home/reflex/Shit/C#/consolenot/Main -r win-x64 -f netcoreapp3.1 --self-contained false -p:PublishSingleFile=true --configuration Release --output /home/reflex/consolenot/
#rm /home/reflex/consolenot/ConsoleNot.pdb
#rm /home/reflex/consolenot/README.txt
touch /home/reflex/consolenot/README.txt
echo "Execute (in console) 'ConsoleNot' file for Linux or 'ConsoleNot.exe' for Windows." >> /home/reflex/consolenot/README.txt
zip -r /home/reflex/consolenot.zip /home/reflex/consolenot/ -j
#scp /home/reflex/consolenot.zip 192.168.88.40:/home/reflex/ConsoleNotSite/ConsoleNotSite/wwwroot/