#!/bin/sh
if [ "$EUID" -ne 0 ]
  then
    echo "Please, run the program as root. Пожалуйста, запустите программу от root."
  else
    systemctl stop consolenot
    systemctl disable consolenot
    rm /usr/lib/systemd/system/consolenot.service
    rm /usr/lib/systemd/system/consolenot_script.sh
    echo "Done! Завершено!"
fi