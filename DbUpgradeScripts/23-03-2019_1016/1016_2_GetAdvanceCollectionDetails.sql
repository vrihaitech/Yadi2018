/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
USE [Yadi2018]
alter PROCEDURE [dbo].[GetAdvanceCollectionDetails] 
@LedgerNo   numeric(18),
@VchType   numeric(18),
@CompanyNo  numeric(18),
@PTypeOfRef numeric(18)

AS
Declare @Summary Table(SrNo numeric(18),VoucherDate datetime,PartyName Varchar(500),AdvAmount numeric(18,2),TotRec numeric(18,2),NetBal numeric(18,2),RefNo numeric(18),PkRefNo numeric(18))
					   

Declare @SrNo numeric(18),@Amount numeric(18,2),
		@RefNo numeric(18),@SNo numeric(18),@BillNo numeric(18),
		@TotalAmount numeric(18,2),@TypeNo numeric(18),@VchDate datetime,
		@ReceivedAmt numeric(18,2),@BalanceAmt numeric(18,2),@PayAmt numeric(18,2),@PKVoucherNo numeric(18),@Disc numeric(18,2),@TotBalAmt numeric(18,2),@DiscAmt numeric(18,2),@NewBalAmt numeric(18,2),
		@CompanyName varchar(max),@VoucherNo numeric(18),@LedgNo numeric(18),@PkRefNo numeric(18),@PartyName Varchar(500),
		@PKVoucherTrnNo numeric(18),@VoucherSrNo numeric(18)

set @SrNo=0
set @TotalAmount=0
set @TotBalAmt=0
set @DiscAmt=0


	--Select @LedgNo=LedgerNo FRom MPayTypeLedger Where CompanyNo=@CompanyNo AND PayTypeNo=2		
--		Declare CurTemp cursor for SELECT TVoucherRefDetails.RefNo, TVoucherEntry.VoucherDate, TVoucherEntry.VoucherUserNo, TVoucherRefDetails.Amount,TVoucherRefDetails.PkRefTrnNo,  MLedger.LedgerName
--
--								   FROM  TVoucherEntry INNER JOIN
--										  TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN
--										  TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo  INNER JOIN
--										 MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo
--										  
--								  WHERE   (TVoucherDetails.LedgerNo = @LedgerNo)  AND (TVoucherRefDetails.TypeOfRef = @PTypeOfRef) AND 
--										  (TVoucherEntry.CompanyNo = @CompanyNo) and (TVoucherEntry.IsCancel='false') and (TVoucherEntry.VoucherTypeCode=@VchType)
--								  ORDER BY TVoucherEntry.VoucherDate
--
--		open CurTemp
--
--			Fetch next from CurTemp into  @RefNo ,@VchDate,@BillNo,@Amount,@PkRefNo,@PartyName
--														
--
--				While(@@Fetch_Status=0)
--				 Begin
--					if(@PTypeOfRef=1)
--						select @TotBalAmt=IsNull(sum(TVoucherRefDetails.Amount),0) from TVoucherRefDetails where RefNo=@RefNo and TypeOfRef=6  and RefNo=@RefNo
--					else
--						select @TotBalAmt=IsNull(sum(TVoucherRefDetails.Amount),0) from TVoucherRefDetails where RefNo=@RefNo and TypeOfRef in (2, 5)  
--					set @TotalAmount=@Amount-@TotBalAmt
--					if(@TotalAmount>0)
--					insert into @Summary values (0,@VchDate,@PartyName,@Amount,@TotBalAmt,@TotalAmount,@RefNo,@PkRefNo)
--				
--					Fetch next from CurTemp into  @RefNo ,@VchDate,@BillNo,@Amount,@PkRefNo,@PartyName
--				 End 
--		Close CurTemp Deallocate CurTemp
--		
--		set @TotalAmount=0
--		set @TotBalAmt=0
--		set @DiscAmt=0

--Select *  From @Summary
if(@VchType = 12 or @VchType = 13)
		Declare CurTemp Cursor For SELECT     TVoucherEntry.VoucherDate, MLedger.LedgerName, SUM(TVoucherDetails.Credit + TVoucherDetails.Debit) AS AdvAmount 
						, TVoucherDetails.PkVoucherTrnNo, TVoucherDetails.VoucherSrNo FROM TVoucherDetails INNER JOIN
						TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo
						WHERE     (TVoucherEntry.PayTypeNo <> 2) AND (TVoucherEntry.VoucherTypeCode = @VchType) AND (TVoucherDetails.SrNo = 501) AND (MLedger.LedgerNo = @LedgerNo) AND 
						(TVoucherEntry.CompanyNo = @CompanyNo) AND (TVoucherEntry.IsCancel = 'false')
						GROUP BY TVoucherEntry.PkVoucherNo, TVoucherEntry.VoucherDate, MLedger.LedgerName, TVoucherDetails.PkVoucherTrnNo, TVoucherDetails.VoucherSrNo 
else
		Declare CurTemp Cursor For SELECT     TVoucherEntry.VoucherDate, MLedger.LedgerName, SUM(TVoucherDetails.Credit + TVoucherDetails.Debit) AS AdvAmount 
						, TVoucherDetails.PkVoucherTrnNo, TVoucherDetails.VoucherSrNo FROM TVoucherDetails INNER JOIN
						TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo
						WHERE     (TVoucherEntry.VoucherTypeCode = @VchType) AND (TVoucherDetails.SrNo = 501) AND (MLedger.LedgerNo = @LedgerNo) AND 
						(TVoucherEntry.CompanyNo = @CompanyNo) AND (TVoucherEntry.IsCancel = 'false')
						GROUP BY TVoucherEntry.PkVoucherNo, TVoucherEntry.VoucherDate, MLedger.LedgerName, TVoucherDetails.PkVoucherTrnNo, TVoucherDetails.VoucherSrNo 


open CurTemp
Fetch next from CurTemp into   @VchDate,@PartyName,@Amount,@PKVoucherTrnNo,@VoucherSrNo
While(@@Fetch_Status = 0)
Begin
	SELECT   @TotBalAmt= IsNull(SUM(Amount),0) FROM  TVoucherRefDetails WHERE(FkVoucherTrnNo = @PKVoucherTrnNo)
	
	set @TotalAmount=@Amount-@TotBalAmt
	if(@TotalAmount>0)
	insert into @Summary values (0,@VchDate,@PartyName,@Amount,@TotBalAmt,@TotalAmount,@PKVoucherTrnNo,@VoucherSrNo)
	
	Fetch next from CurTemp into @VchDate,@PartyName,@Amount,@PKVoucherTrnNo,@VoucherSrNo
End
Close CurTemp Deallocate CurTemp

Select *  From @Summary
--Select SNo AS 'SNo', VchDate AS 'Date', ItemName AS 'Particulars',
--					   Qty AS 'Qty', Rate AS 'Rate',Vatav as 'Vatav' ,Freight AS 'Freight',Amount AS 'Amount',
--				   ReceivedAmt AS 'Received',BalanceAmt AS 'Balance' ,chkSelect AS 'Select',PayAmt AS 'PayAmt',Disc AS 'Disc', NewBalAmt as 'New Bal' ,Chk as 'Final',PKStockTrnNo as 'PK' From @Summary
--



