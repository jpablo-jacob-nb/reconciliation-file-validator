using System.Globalization;
using System.Text;

namespace ReconciliationFileValidation.TitanReconciliationFile;

public static class FileTitanHeaderParser
{
    public static string ToReadableFormat(string line)
    {
        if (line.Length != 494)
        {
            return $"Error: Incorrect Length. Waiting: 494, Got: {line.Length}";
        }

        try
        {
            var pos = 0;
            var fileTransactionCode = line.Substring(pos, 1);
            pos += 1;
            var fileCreationDate = line.Substring(pos, 8);
            pos += 8;
            var merchantName = line.Substring(pos, 35);
            pos += 35;
            var merchantId = line.Substring(pos, 10);
            pos += 10;
            var fileSequenceNumber = line.Substring(pos, 6);
            pos += 6;
            var filler = line.Substring(pos, 434);

            var sb = new StringBuilder();
            sb.AppendLine($"FileTransactionCode | {fileTransactionCode} | {fileTransactionCode.Length}");
            sb.AppendLine(
                $"FileCreationDate | {DateTime.ParseExact(fileCreationDate, "yyyyMMdd", CultureInfo.InvariantCulture):yyyy-MM-dd} | {fileCreationDate.Length}");
            sb.AppendLine($"MerchantName | {merchantName.Trim()} | {merchantName.Length}");
            sb.AppendLine($"MerchantId | {merchantId.Trim()} | {merchantId.Length}");
            sb.AppendLine($"FileSequenceNumber | {int.Parse(fileSequenceNumber)} | {fileSequenceNumber.Length}");
            sb.AppendLine($"Filler | <empty string> | {filler.Length}");

            return sb.ToString();
        }
        catch (Exception ex)
        {
            return $"Error while Processing de line: {ex.Message}";
        }
    }

    public static string ToReadableTrailerFormat(string line)
    {
        var pos = 0;
        var trailerTransactionCode = line.Substring(pos, 1);
        pos += 1;
        var totalTransactionCount = line.Substring(pos, 10);
        pos += 10;
        var totalTransactionAmount = line.Substring(pos, 12);
        pos += 12;
        var filler = line.Substring(pos, 477);
        pos += 477;

        if (pos != line.Length)
        {
            return $"Error: Incorrect Length. Waiting: 494, Got: {line.Length}";
        }

        var sb = new StringBuilder();

        sb.AppendLine($"TrailerTransactionCode | {trailerTransactionCode} | {trailerTransactionCode.Length}");
        sb.AppendLine($"TotalTransactionCount | {long.Parse(totalTransactionCount)} | {totalTransactionCount.Length}");

        //Convert amount with 2 decimals
        var amount = decimal.Parse(totalTransactionAmount) / 100;
        sb.AppendLine(
            $"TotalTransactionAmount | {amount.ToString("F2", CultureInfo.InvariantCulture)} | {totalTransactionAmount.Length}");

        sb.AppendLine($"Filler | <empty string> | {filler.Length}");

        return sb.ToString();
    }
}