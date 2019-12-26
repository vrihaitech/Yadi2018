set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created On 20-Jan-2019
create Procedure [dbo].[GetChequePrintDetails]
@ChqPrintingNo	numeric(18)
AS
Begin
	SELECT     MLedger.LedgerName, TChequePrinting.ChequeDate, TVoucherEntry.BilledAmount As ChequeAmount, TChequePrinting.Remark1, TChequePrinting.Remark2, 
                      TChequePrinting.Remark3
FROM         TChequePrinting INNER JOIN
                      MLedger ON TChequePrinting.LedgerNo = MLedger.LedgerNo INNER JOIN
                      TVoucherEntry ON TChequePrinting.FKVoucherNo = TVoucherEntry.PkVoucherNo
WHERE     (TChequePrinting.PkSrNo = @ChqPrintingNo)	
End


