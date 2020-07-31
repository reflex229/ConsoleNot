# ConsoleNot
Console notifier with desktop notifications, written in C#.

## Windows
###### Dependencies:
.NET Core 3.1
###### Usage:
1. Download this repository.
2. Compile the program.
3. Run the program. To show all commands use:
```cmd
ConsoleNot.exe --help
```
Example usage:
```cmd
ConsoleNot.exe -s 10 -c 3 -t Title -d Description
```
## Linux
###### Dependencies:
.NET Core 3.1; notify-send; git
###### Usage:
1. "git clone" this repository:
```bash
git clone https://github.com/reflex229/ConsoleNot.git
```
2. Enter the repository's directory:
```bash
cd ConsoleNot
```
3. Compile the program.
3. Run the program. To show all commands use:
```bash
./ConsoleNot --help
```
Example usage:
```cmd
./ConsoleNot -s 10 -c 3 -t Title -d Description
```
