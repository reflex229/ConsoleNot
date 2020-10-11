#!/bin/sh
if [ "$EUID" -ne 0 ]
  then
    echo "Please, run the program as root. Пожалуйста, запустите программу от root."
  else
    cp $1/consolenot.service /usr/lib/systemd/system/consolenot.service
    cp $1/consolenot_script.sh /usr/lib/systemd/system/consolenot_script.sh
    systemctl start consolenot
    systemctl enable consolenot
    systemctl status consolenot
fi