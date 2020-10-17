# Software installer for Windows 10 - ***Polyzynger***

## What?
Polyzynger is a simple WPF application that **finds** the latest version, **downloads** and **installs** software on Windows.
Also, it additionally copies some resources and executes PowerShell scripts if necessary.

## Why?
I have created it to make work in my department easier and faster.

## How?
It works in simple steps, for each selected application:
- (If necessary) scan demanded product's official website to check if there is a newer version. For this purpose use **Regex**.
If somethings new has come, estabilish new download link and check it.
- Using **WebClient** download installer and save it in a temporary directory.
- Silently **install** the application.
- Delete temporary files.

### Resources
- [View UML Class Diagram](https://download.hubertgad.net/Polyzynger/Polyzynger.class.violet.html)
- [Download compiled file](https://download.hubertgad.net/Polyzynger/Polyzynger.exe)

### See also
- [My projects](https://hubertgad.net/projects)