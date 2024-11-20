# uintptrDPI | Install GoodByeDPI One Click

**uintptrDPI** is a tool designed to easily install and manage the GoodbyeDPI application via a C# Windows Forms application. It automates the installation process, provides feedback to the user, and ensures the service is properly installed.

## Features

- **Latest Version Download**: The application automatically finds and downloads the latest GoodbyeDPI release from GitHub.
- **Service Installation**: It downloads the ZIP file, extracts it to the specified folder, and automates the service installation.
- **Run as Administrator**: The necessary file is executed with administrator privileges to install the service.
- **Startup Type Check**: It automatically sets the service's startup type to 'Automatic' so that it starts on system reboot.
- **Shutdown Warning**: The program warns the user not to delete files in the target directory before closing, ensuring the program works properly.

## Contributing

This is an open-source project, and contributions are welcome. 

## License

This project is licensed under the [MIT License](LICENSE).
