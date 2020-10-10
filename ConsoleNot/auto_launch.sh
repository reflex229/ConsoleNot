#!/bin/sh
if [ "$EUID" -ne 0 ]
  then 
    echo "Please, run the program as root. Пожалуйста, запустите программу от root."
  else 
    wget -O /usr/lib/systemd/system/consolenot.service http://192.168.88.40/consolenot.service #TODO: Try to copy these files not from the dedicated server.
    wget -O /usr/lib/systemd/system/consolenot.service http://192.168.88.40/consolenot_script.sh
    systemctl start consolenot
fi