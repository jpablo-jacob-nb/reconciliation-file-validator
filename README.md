# Reconciliation File Processing App

A simple cross-platform .NET 8 console application that converts a “Reconciliation File” into a human-readable output file.

## Features
- Prompts for an input file path interactively
- Processes reconciliation data into a readable format
- Works on Windows, macOS, and Linux
- Minimal setup and easy to run

## Prerequisites
- .NET SDK 8.0 or later
    - Verify with:
```shell script
dotnet --info
```


## Getting Started

### Clone the repository
```shell script
git clone https://github.com/jpablo-jacob-nb/reconciliation-file-validator.git
cd reconciliation-file-validator
```


### Build
```shell script
dotnet build
```


### Run
- From the project directory:
```shell script
dotnet run
```

- Or run the compiled binary:
```shell script
dotnet bin/Debug/net8.0/ReconciliationFileValidation.dll
```


When the app starts, it will ask you to enter the full path to your reconciliation file. Provide an absolute path, for example:
- Windows: C:\data\reconciliation.txt
- macOS/Linux: /Users/<your-username>/data/reconciliation.txt

The application will then generate a readable output based on the provided file. Unless configured otherwise, the output is typically written alongside the input file.

## Usage Notes
- Ensure the input path exists and the application has read permissions.
- If the path is empty or invalid, the app will exit gracefully with an error message.
- Large files may take longer to process depending on your system.

## Cross-Platform Examples

- Windows (PowerShell):
```textmate
dotnet run
```

- macOS/Linux (Bash):
```shell script
dotnet run
```


## Troubleshooting
- “Invalid file path”: Double-check the path and ensure it’s absolute.
- Permission issues on macOS/Linux: You may need to adjust file permissions:
```shell script
chmod +r /path/to/your/reconciliation-file.txt
```


## Development
- Language: C# (net8.0)
- You can open and run the project in your preferred IDE or editor that supports .NET 8.

### Recommended commands
- Restore packages:
```shell script
dotnet restore
```

- Clean build:
```shell script
dotnet clean && dotnet build
```


## Contributing
1. Fork the repository
2. Create a feature branch
3. Commit your changes with clear messages
4. Open a pull request

Please include tests and documentation updates where applicable.

## License
Add your license here (e.g., MIT, Apache-2.0). If a license file is present, it governs usage.

## Support
- Open an issue with:
    - OS and .NET version
    - Exact steps to reproduce
    - Sample (redacted) paths or minimal input example

Thank you for using the Reconciliation File Processing App!