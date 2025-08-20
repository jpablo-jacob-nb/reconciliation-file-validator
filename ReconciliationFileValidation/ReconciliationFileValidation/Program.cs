using ReconciliationFileValidation.Helper;

Console.WriteLine("Welcome to the File Processing App!");
Console.WriteLine("This app will generate a readable file from your Reconciliation File.");

Console.Write(@"Enter the full path to the file (e.g., C:\data\file.txt or /Users/username/data/file.txt): ");
var fullFilePath = Console.ReadLine();

if (string.IsNullOrEmpty(fullFilePath))
{
    Console.WriteLine("Invalid file path. Exiting program.");
    return;
}

try
{
    var absoluteFilePath = Path.GetDirectoryName(fullFilePath);

    var fileName = Path.GetFileName(fullFilePath);

    await GenerateReadableFile.Process(
        string.IsNullOrEmpty(absoluteFilePath) ? throw new InvalidOperationException() : absoluteFilePath,
        fileName);
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}

Console.WriteLine("Press any key to exit.");
Console.ReadKey();