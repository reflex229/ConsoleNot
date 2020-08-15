# ConsoleNot
## Plans for future updates
Unlimited amount of notifications.

Web interface to manage all notifications.

Socket will be changed to HTTP requests. 
## English description
Console notifier with desktop notifications, written in C#.
Example: the doctor prescribed you a course of treatment with pills (you need to take
the pills 6 times a day).
This program will remind you when it is the time to take the pills and sends a
desktop notification. Give the arguments "-h 2 -c 6 -t -d", so the program will notify you every 2 hours,
6 times.
## Русское описание
Консольная напоминалка с уведомлениями на рабочем столе, написанная на C#.
Пример: доктор прописал вам курс лечения таблетками, принимать по 6 раз в день.
Данная программа поможет вам вовремя вспомнить о принятии таблеток и вышлет
уведомление на рабочий стол. Задаём аргументы "-h 2 -c 6 -t -d",
таким образом, программа будет выдавать вам уведомление раз в 2 часа,
6 раз.

## English
## Windows
### Client
###### Dependencies
.NET Core 3.1
#### Usage
1. Download this repository.
2. Enter the repository's directory
3. Build the project.
```cmd
dotnet build
```
###### Standard mode
Run the program (in /ConsoleNot/bin/Debug/netcoreapp3.1/). To show all commands use
```cmd
ConsoleNot.exe --help
```
Example usage
```cmd
ConsoleNot.exe -s 10 -c 3 -t -d
```
or dotnet
```cmd
dotnet ConsoleNot.dll
```
###### Client mode
Run the program (in /ConsoleNot/bin/Debug/netcoreapp3.1/). To show all commands use
```cmd
ConsoleNot.exe --help
```
Example usage
```cmd
ConsoleNot.exe -s 10 -c 3 -t -d --client
```
or dotnet
```cmd
dotnet ConsoleNot.dll
```
### Server
Run the program (in /ConsoleNotServer/bin/Debug/netcoreapp3.1/). To show all commands use
```cmd
ConsoleNotServer.exe --help
```
Example usage
```cmd
ConsoleNotServer.exe --port 8005
```
or dotnet
```cmd
dotnet ConsoleNotServer.dll
```
## Linux
### Client
###### Dependencies
.NET Core 3.1; notify-send; git
###### Usage
1. "git clone" this repository
```bash
git clone https://github.com/reflex229/ConsoleNot.git
```
2. Enter the repository's directory
```bash
cd ConsoleNot
```
3. Build the project.
```bash
dotnet build
```
###### Standard mode
Run the program (in /ConsoleNot/bin/Debug/netcoreapp3.1/). To show all commands use
```bash
./ConsoleNot --help
```
Example usage
```bash
./ConsoleNot -s 10 -c 3 -t -d
```
or mono
```bash
mono ConsoleNot.dll
```
###### Client mode
Run the program (in /ConsoleNot/bin/Debug/netcoreapp3.1/). To show all commands use
```bash
./ConsoleNot --help
```
Example usage
```bash
./ConsoleNot -s 10 -c 3 -t -d --client
```
or mono
```bash
mono ConsoleNot.dll
```
### Server
Run the program (in /ConsoleNotServer/bin/Debug/netcoreapp3.1/). To show all commands use
```bash
./ConsoleNotServer --help
```
Example usage
```bash
./ConsoleNotServer --port 8005
```
or mono
```bash
mono ConsoleNotServer.dll
```
## Русский
## Windows
### Клиент
###### Зависимости
.NET Core 3.1
###### Использование
1. Скачайте данный репозиторий.
2. Войдите в директорию репозитория
3. Соберите проект.
```cmd
dotnet build
```
###### Стандартный режим
Запустите программу (в /ConsoleNot/bin/Debug/netcoreapp3.1/). Чтобы вывести все команды, введите
```cmd
ConsoleNot.exe --help
```
Пример использования
```cmd
ConsoleNot.exe -s 10 -c 3 -t -d
```
или dotnet
```cmd
dotnet ConsoleNot.dll
```
###### Режим клиента
Запустите программу (в /ConsoleNot/bin/Debug/netcoreapp3.1/). Чтобы вывести все команды, введите
```cmd
ConsoleNot.exe --help
```
Пример использования
```cmd
ConsoleNot.exe -s 10 -c 3 -t -d --client
```
или dotnet
```cmd
dotnet ConsoleNot.dll
```
### Сервер
Запустите программу (в /ConsoleNotServer/bin/Debug/netcoreapp3.1/). Чтобы вывести все команды, введите
```cmd
ConsoleNotServer.exe --help
```
Пример использования
```cmd
ConsoleNotServer.exe --port 8005
```
или dotnet
```cmd
dotnet ConsoleNotServer.dll
```
## Linux
### Клиент
###### Зависимости
.NET Core 3.1; notify-send; git
###### Использование
1. "git clone" для данного репозитория
```bash
git clone https://github.com/reflex229/ConsoleNot.git
```
2. Войдите в директорию репозитория
```bash
cd ConsoleNot
```
3. Соберите проект.
```bash
dotnet build
```
###### Стандартный режим
Запустите программу (в /ConsoleNot/bin/Debug/netcoreapp3.1/). Чтобы увидеть все команды, введите
```bash
./ConsoleNot --help
```
Пример использования
```bash
./ConsoleNot -s 10 -c 3 -t -d
```
или mono
```bash
mono ConsoleNot.dll
```
###### Режим клиента
Запустите программу (в /ConsoleNot/bin/Debug/netcoreapp3.1/). Чтобы увидеть все команды, введите
```bash
./ConsoleNot --help
```
Пример использования
```bash
./ConsoleNot -s 10 -c 3 -t -d --client
```
или mono
```bash
mono ConsoleNot.dll
```
### Сервер
Запустите программу (в /ConsoleNotServer/bin/Debug/netcoreapp3.1/). Чтобы увидеть все команды, введите
```bash
./ConsoleNotServer --help
```
Пример использования
```bash
./ConsoleNotServer --port 8005
```
или mono
```bash
mono ConsoleNotServer.dll
```
