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
        var trailerCreationDate = line.Substring(pos, 8);
        pos += 8;
        var trailerTransactionRecordCount = line.Substring(pos, 10);
        pos += 10;
        var trailerTransactionAmountSign = line.Substring(pos, 1);
        pos += 1;
        var trailerTransactionAmountTotal = line.Substring(pos, 10);
        pos += 10;
        var trailerRewardsAmountSign = line.Substring(pos, 1);
        pos += 1;
        var trailerRewardsAmountTotal = line.Substring(pos, 10);
        pos += 10;
        var trailerManufacturerDiscAmountSign = line.Substring(pos, 1);
        pos += 1;
        var trailerManufacturerDiscAmountTotal = line.Substring(pos, 10);
        pos += 10;
        var trailerStoreDiscAmountSign = line.Substring(pos, 1);
        pos += 1;
        var trailerStoreDiscAmountTotal = line.Substring(pos, 10);
        pos += 10;
        var trailerAuthFeeSign = line.Substring(pos, 1);
        pos += 1;
        var trailerAuthFeeTotal = line.Substring(pos, 10);
        pos += 10;
        var trailerSettlementAmountSign = line.Substring(pos, 1);
        pos += 1;
        var trailerFinalSettlementAmountTotal = line.Substring(pos, 10);
        pos += 10;
        var filler = line.Substring(pos, 414);
        pos += 414;

        if (pos != line.Length)
        {
            return $"Error: Incorrect Length. Waiting: 494, Got: {line.Length}";
        }

        var sb = new StringBuilder();

        sb.AppendLine($"TrailerTransactionCode | {trailerTransactionCode} | {trailerTransactionCode.Length}");
        sb.AppendLine(
                $"FileTrailerCreationDate | {DateTime.ParseExact(trailerCreationDate, "yyyyMMdd", CultureInfo.InvariantCulture):yyyy-MM-dd} | {trailerCreationDate.Length}");
        sb.AppendLine(
                $"TransactionRecordCount | {trailerTransactionRecordCount} | {trailerTransactionRecordCount.Length}");
        sb.AppendLine(
                $"TransactionAmountSign | {trailerTransactionAmountSign} | {trailerTransactionAmountSign.Length}");
        sb.AppendLine(
                $"TransactionAmountTotal | {trailerTransactionAmountSign}{decimal.Parse(trailerTransactionAmountTotal) / 100:F2} | {trailerTransactionAmountTotal.Length}");
        sb.AppendLine(
            $"RewardsAmountSign | {trailerRewardsAmountSign} | {trailerRewardsAmountSign.Length}");
        sb.AppendLine(
                        $"RewardsAmountTotal | {trailerRewardsAmountSign}{decimal.Parse(trailerRewardsAmountTotal) / 100:F2} | {trailerRewardsAmountTotal.Length}");
        sb.AppendLine(
            $"ManufacturerDiscountsAmountSign | {trailerManufacturerDiscAmountSign} | {trailerManufacturerDiscAmountSign.Length}");
        sb.AppendLine(
                $"ManufacturerDiscountsAmountTotal | {trailerManufacturerDiscAmountSign}{decimal.Parse(trailerManufacturerDiscAmountTotal) / 100:F2} | {trailerManufacturerDiscAmountTotal.Length}");
        sb.AppendLine(
            $"StoreDiscountsAmountSign | {trailerStoreDiscAmountSign} | {trailerStoreDiscAmountSign.Length}");
        sb.AppendLine(
                $"StoreDiscountsAmountTotal | {trailerStoreDiscAmountSign}{decimal.Parse(trailerStoreDiscAmountTotal) / 100:F2} | {trailerStoreDiscAmountTotal.Length}");
        sb.AppendLine(
            $"AuthorizationFeeSign | {trailerAuthFeeSign} | {trailerAuthFeeSign.Length}");
        sb.AppendLine(
                $"AuthorizationFeeTotal | {trailerAuthFeeSign}{decimal.Parse(trailerAuthFeeTotal) / 100:F2} | {trailerAuthFeeTotal.Length}");
        sb.AppendLine(
            $"SettlementAmountSign | {trailerSettlementAmountSign} | {trailerSettlementAmountSign.Length}");

        sb.AppendLine(
                $"FinalSettlementAmountTotal | {trailerSettlementAmountSign}{decimal.Parse(trailerFinalSettlementAmountTotal) / 100:F2} | {trailerFinalSettlementAmountTotal.Length}");
        sb.AppendLine($"Filler | <empty string> | {filler.Length}");
        return sb.ToString();
    }
}
