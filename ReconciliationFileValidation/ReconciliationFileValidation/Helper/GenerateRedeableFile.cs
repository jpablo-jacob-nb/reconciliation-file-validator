using ReconciliationFileValidation.TitanReconciliationFile;

namespace ReconciliationFileValidation.Helper;

public static class GenerateReadableFile
{
    public static async Task Process(string absoluteFilePath, string fileName)
    {
        try
        {
            //TODO: Quitar extension
            var readableFileName = $"{fileName}-readable.txt";
            var outputFilePath = Path.Combine(absoluteFilePath, readableFileName);

            Console.WriteLine($"Processing the file: {Path.Combine(absoluteFilePath, fileName)}");

            using var sr = new StreamReader(Path.Combine(absoluteFilePath, fileName));
            await using var sw = new StreamWriter(outputFilePath);

            while (await sr.ReadLineAsync() is { } line)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                var transactionCode = line[..1];

                switch (transactionCode)
                {
                    case "H":
                        var readableHeader = FileTitanHeaderParser.ToReadableFormat(line);
                        await sw.WriteLineAsync("--- FILE HEADER ---");
                        await sw.WriteLineAsync(readableHeader);
                        await sw.WriteLineAsync("-------------------");
                        break;
                    case "D":
                        var readableTransaction = FileTitanTransactionParser.ToReadableFormat(line);
                        await sw.WriteLineAsync("--- DETAIL TRANSACTION ---");
                        await sw.WriteLineAsync(readableTransaction);
                        await sw.WriteLineAsync("--------------------------");
                        break;
                    case "T":
                        var readableTrailer = FileTitanHeaderParser.ToReadableTrailerFormat(line);
                        await sw.WriteLineAsync("--- TRAILER ---");
                        await sw.WriteLineAsync(readableTrailer);
                        await sw.WriteLineAsync("---------------");
                        break;
                    default:
                        await sw.WriteLineAsync($"--- Unknown Line ---\n{line}\n-------------------------");
                        break;
                }
            }

            Console.WriteLine($"Readable File generated in: {outputFilePath}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error While Processing the File: {e.Message}");
        }
    }
}