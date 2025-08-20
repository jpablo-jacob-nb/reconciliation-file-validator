using System.Globalization;
using System.Text;

namespace ReconciliationFileValidation.TitanReconciliationFile;

public static class FileTitanTransactionParser
{
    public static string ToReadableFormat(string line)
    {
        // TODO: Cuál es la suma? Siempre pensé que eran 500. El ejemplo que me pasó miguel no es así.
        if (line.Length != 500)
        {
            return $"Error: Incorrect length. Waiting: 500, Got: {line.Length}";
        }

        try
        {
            var pos = 0;
            var detailTransactionCode = line.Substring(pos, 1);
            pos += 1;
            var recordSequenceNumber = line.Substring(pos, 10);
            pos += 10;
            var storeNumber = line.Substring(pos, 10);
            pos += 10;
            var terminalNumber = line.Substring(pos, 4);
            pos += 4;
            var transactionDate = line.Substring(pos, 8);
            pos += 8;
            var transactionTime = line.Substring(pos, 6);
            pos += 6;
            var businessDate = line.Substring(pos, 8);
            pos += 8;
            var transactionNumber = line.Substring(pos, 100);
            pos += 100;
            var alternateTransactionId = line.Substring(pos, 20);
            pos += 20;
            var transactionType = line.Substring(pos, 2);
            pos += 2;
            var transactionAmountSign = line.Substring(pos, 1);
            pos += 1;
            var transactionAmount = line.Substring(pos, 10);
            pos += 10;
            var rewardsAmountSign = line.Substring(pos, 1);
            pos += 1;
            var rewardsAmount = line.Substring(pos, 10);
            pos += 10;
            var manufacturerDiscountsSign = line.Substring(pos, 1);
            pos += 1;
            var manufacturerDiscountsAmount = line.Substring(pos, 10);
            pos += 10;
            var storeDiscountsSign = line.Substring(pos, 1);
            pos += 1;
            var storeDiscountsAmount = line.Substring(pos, 10);
            pos += 10;
            var authorizationFeeSign = line.Substring(pos, 1);
            pos += 1;
            var authorizationFee = line.Substring(pos, 5);
            pos += 5;
            var settlementAmountSign = line.Substring(pos, 1);
            pos += 1;
            var finalSettlementAmount = line.Substring(pos, 10);
            pos += 10;
            var panEntryIndicator = line.Substring(pos, 1);
            pos += 1;
            var primaryAccountNumber = line.Substring(pos, 25);
            pos += 25;
            var authorizationNumber = line.Substring(pos, 36);
            pos += 36;
            var cardProgramType = line.Substring(pos, 4);
            pos += 4;
            var cardProgramDescription = line.Substring(pos, 30);
            pos += 30;
            var transactionSource = line.Substring(pos, 2);
            pos += 2;
            var settleDate = line.Substring(pos, 8);
            pos += 8;
            var orderId = line.Substring(pos, 100);
            pos += 100;
            var filler = line.Substring(pos, 68);

            
            var sb = new StringBuilder();
            sb.AppendLine($"\tDetailTransactionCode | {detailTransactionCode} | {detailTransactionCode.Length}");
            sb.AppendLine(
                $"\tRecordSequenceNumber | {long.Parse(recordSequenceNumber)} | {recordSequenceNumber.Length}");
            sb.AppendLine($"\tStoreNumber | {long.Parse(storeNumber)} | {storeNumber.Length}");
            sb.AppendLine($"\tTerminalNumber | {int.Parse(terminalNumber)} | {terminalNumber.Length}");
            sb.AppendLine(
                $"\tTransactionDate | {DateTime.ParseExact(transactionDate, "yyyyMMdd", CultureInfo.InvariantCulture):yyyy-MM-dd} | {transactionDate.Length}");
            sb.AppendLine(
                $"\tTransactionTime | {DateTime.ParseExact(transactionTime, "HHmmss", CultureInfo.InvariantCulture):HH:mm:ss} | {transactionTime.Length}");
            sb.AppendLine(
                $"\tBusinessDate | {DateTime.ParseExact(businessDate, "yyyyMMdd", CultureInfo.InvariantCulture):yyyy-MM-dd} | {businessDate.Length}");
            sb.AppendLine($"\tTransactionNumber | {transactionNumber.TrimStart('0')} | {transactionNumber.Length}");
            sb.AppendLine($"\tAlternateTransactionId | {alternateTransactionId} | {alternateTransactionId.Length}");
            sb.AppendLine($"\tTransactionType | {transactionType} | {transactionType.Length}");
            sb.AppendLine(
                $"\tTransactionAmount | {transactionAmountSign}{decimal.Parse(transactionAmount) / 100:F2} | {transactionAmount.Length}");
            sb.AppendLine(
                $"\tRewardsAmount | {rewardsAmountSign}{decimal.Parse(rewardsAmount) / 100:F2} | {rewardsAmount.Length}");
            sb.AppendLine(
                $"\tManufacturerDiscountsAmount | {manufacturerDiscountsSign}{decimal.Parse(manufacturerDiscountsAmount) / 100:F2} | {manufacturerDiscountsAmount.Length}");
            sb.AppendLine(
                $"\tStoreDiscountsAmount | {storeDiscountsSign}{decimal.Parse(storeDiscountsAmount) / 100:F2} | {storeDiscountsAmount.Length}");
            sb.AppendLine(
                $"\tAuthorizationFee | {authorizationFeeSign}{decimal.Parse(authorizationFee) / 100:F2} | {authorizationFee.Length}");
            sb.AppendLine(
                $"\tFinalSettlementAmount | {settlementAmountSign}{decimal.Parse(finalSettlementAmount) / 100:F2} | {finalSettlementAmount.Length}");
            sb.AppendLine($"\tPanEntryIndicator | {panEntryIndicator} | {panEntryIndicator.Length}");
            sb.AppendLine($"\tPrimaryAccountNumber | {primaryAccountNumber.Trim()} | {primaryAccountNumber.Length}");
            sb.AppendLine($"\tAuthorizationNumber | {authorizationNumber.Trim()} | {authorizationNumber.Length}");
            sb.AppendLine($"\tCardProgramType | {cardProgramType} | {cardProgramType.Length}");
            sb.AppendLine(
                $"\tCardProgramDescription | {cardProgramDescription.Trim()} | {cardProgramDescription.Length}");
            sb.AppendLine($"\tTransactionSource | {transactionSource.Trim()} | {transactionSource.Length}");
            sb.AppendLine(
                $"\tSettleDate | {DateTime.ParseExact(settleDate, "yyyyMMdd", CultureInfo.InvariantCulture):yyyy-MM-dd} | {settleDate.Length}");
            sb.AppendLine($"\tOrderId | {orderId.Trim()} | {orderId.Length}");
            sb.AppendLine($"\tFiller | <empty string> | {filler.Length}");

            return sb.ToString();
        }
        catch (Exception ex)
        {
            return $"Error processing de line: {ex.Message}";
        }
    }
}