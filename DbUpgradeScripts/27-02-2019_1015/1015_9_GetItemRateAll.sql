set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER Function [dbo].[GetItemRateAll]
(
@PItemNo		           numeric(18),
--@PBarCodeNo			   numeric(18),
@PUOMNo	               numeric(18),
@PMRP				   numeric(18,4),
@POnDate		           datetime,
@PGroupNo				numeric(18)
)
RETURNS @TRate TABLE  (PkSrNo numeric(18)/*,FkBcdSrNo numeric(18)*/ ,ItemNo numeric(18),FromDate datetime,PurRate numeric(18,2),MRP numeric(18,2),
ASaleRate numeric(18, 2),BSaleRate numeric(18, 2),CSaleRate numeric(18, 2),DSaleRate numeric(18, 2),ESaleRate numeric(18, 2)
,UOMNo numeric(18), StockConversion numeric(18,2),PerOfRateVariation numeric(18, 2),MKTQty numeric(18,0),IsActive bit,
Stock numeric(18, 2),Stock2 numeric(18, 2),Weight1 numeric(18, 2),Weight2 numeric(18, 2),LPPerc numeric(18, 2),SPPerc numeric(18, 2),Hamali numeric(18, 2))
AS
Begin
Declare @TempTbl TABLE (PkSrNo numeric(18)/*,FkBcdSrNo numeric(18) */,ItemNo numeric(18),FromDate datetime,PurRate numeric(18,2),MRP numeric(18,2),
ASaleRate numeric(18, 2),BSaleRate numeric(18, 2),CSaleRate numeric(18, 2),DSaleRate numeric(18, 2),ESaleRate numeric(18, 2),
UOMNo numeric(18), StockConversion numeric(18,2),PerOfRateVariation numeric(18, 2),IsActive bit)

Declare @PkSrNo numeric(18) , /*@FkBcdSrNo numeric(18) , */ @ItemNo numeric(18) , 
@FromDate datetime , @PurRate numeric(18,2), @MRP numeric(18,2),@IsActive bit,@Stock numeric(18, 2),@Stock2 numeric(18, 2),
@Weight1 numeric(18, 2),@Weight2 numeric(18, 2),@LPPerc numeric(18, 2),@SPPerc numeric(18, 2),@Hamali numeric(18, 2),
@ASaleRate numeric(18, 2),@BSaleRate numeric(18, 2),@CSaleRate numeric(18, 2),@DSaleRate numeric(18, 2),@ESaleRate numeric(18, 2),
@UOMNo numeric(18) ,@StockConversion numeric(18,2) , @PerOfRateVariation numeric(18, 2),@MKTQty numeric(18, 0),
@StrFilter varchar(max),@SqlQuery varchar(max), @VItemNo numeric(18), 
@VBarCodeNo numeric(18), @VUOMNo numeric(18),@VMRP numeric(18,4)

Set  @PkSrNo = 0-- Set @FkBcdSrNo = 0 
set @MKTQty=0 set @MRP=0.00
Set  @ItemNo = 0 Set @FromDate = 0 Set  @PurRate  = 0 
Set  @ASaleRate = 0 Set  @BSaleRate = 0 Set  @CSaleRate = 0 Set  @DSaleRate = 0 Set  @ESaleRate = 0 Set  @UOMNo  = 0 
Set  @StockConversion = 0 
Set  @PerOfRateVariation = 0
Set @VItemNo = 0 
set @VBarCodeNo=0 set @VUOMNo =0 set @VMRP=0.00
set @Stock=0 set @Stock2=0
set @Weight1=0 set @Weight2=0
set @Hamali=0
if(@PGroupNo is Null)
Declare CurRate Cursor For Select PkSrNo ,ItemNo/*,MRateSetting.FkBcdSrNo*/,FromDate ,PurRate,MRP,ASaleRate,BSaleRate,CSaleRate,DSaleRate,ESaleRate ,
				 UOMNo , StockConversion ,
				 PerOfRateVariation,MKTQty,IsActive, Stock,Stock2,Weight1,Weight2,LPPerc,SPPerc,Hamali From MRateSetting 
				 where IsActive='true' AND  ItemNo=Case When @PItemNo is null then ItemNo else @PItemNo end 
				AND UOMNo=Case When @PUOMNo is null then UOMNo else @PUOMNo end 
				AND MRP=Case When @PMRP is null then MRP else @PMRP end				
				 Order by ItemNo,UOMNo,MRP,FromDate DESC, PkSrNo DESC 
else
	Declare CurRate Cursor For Select MRateSetting.PkSrNo /*,MRateSetting.FkBcdSrNo*/,MRateSetting.ItemNo,MRateSetting.FromDate ,MRateSetting.PurRate,MRateSetting.MRP,MRateSetting.ASaleRate,MRateSetting.BSaleRate,MRateSetting.CSaleRate,MRateSetting.DSaleRate,MRateSetting.ESaleRate ,
				 MRateSetting.UOMNo , MRateSetting.StockConversion ,
				 MRateSetting.PerOfRateVariation,MRateSetting.MKTQty,MRateSetting.IsActive  ,MRateSetting.Stock,MRateSetting.Stock2,MRateSetting.Weight1,
MRateSetting.Weight2,MRateSetting.LPPerc,MRateSetting.SPPerc,MRateSetting.Hamali
 From MRateSetting INNER JOIN 
				 MItemMaster ON MRateSetting.ItemNo=MItemMaster.ItemNo
				 where MRateSetting.IsActive='true' AND  MRateSetting.ItemNo=Case When @PItemNo is null then MRateSetting.ItemNo else @PItemNo end 
				AND MRateSetting.UOMNo=Case When @PUOMNo is null then MRateSetting.UOMNo else @PUOMNo end 
				AND MRateSetting.MRP=Case When @PMRP is null then MRateSetting.MRP else @PMRP end
				AND MItemMaster.GroupNo=@PGroupNo
				 Order by MRateSetting.ItemNo/*,MRateSetting.FkBcdSrNo*/,MRateSetting.UOMNo,MRateSetting.MRP,MRateSetting.FromDate DESC, PkSrNo DESC 
Open CurRate 

Fetch CurRate into @PkSrNo ,@ItemNo,@FromDate,@PurRate,@MRP,@ASaleRate,@BSaleRate,@CSaleRate,@DSaleRate,@ESaleRate ,
								  @UOMNo ,@StockConversion ,@PerOfRateVariation,@MKTQty,@IsActive,@Stock,@Stock2,@Weight1,@Weight2,@LPPerc,@SPPerc,@Hamali
DECLARE @isRecOK int

while(@@Fetch_Status = 0)
Begin
		SET @isRecOK = 1
	    if(@PItemNo IS not NULL AND @PItemNo != @ItemNo)
		Begin
            SET @isRecOK = 0
		End 

		if( @PUOMNo is not NUll AND @PUOMNo != @UOMNo)
		Begin
			 SET @isRecOK = 0
		End 

		if( @PMRP is not NUll AND @PMRP != @MRP)
		Begin
			 SET @isRecOK = 0
		End 
		
		if(@POnDate is not NUll AND @FromDate > @POnDate)
		Begin
			 SET @isRecOK = 0
		End 
		else if(getdate()<@FromDate)
		Begin
			SET @isRecOK = 0
		End
 
	if((@isRecOK = 1) AND 
       (@VItemNo != @ItemNo or @VUOMNo != @UOMNo or @VMRP!=@MRP ))
	Begin
		set @VItemNo = @ItemNo  
		set @VUOMNo = @UOMNo 
		set @VMRP=@MRP
		
		insert into @TRate values (@PkSrNo /*,@FkBcdSrNo*/,@ItemNo,@FromDate,@PurRate,@MRP,@ASaleRate,@BSaleRate,@CSaleRate,@DSaleRate,@ESaleRate ,@UOMNo, 
								   @StockConversion ,@PerOfRateVariation,@MKTQty,@IsActive,@Stock,@Stock2,@Weight1,@Weight2,@LPPerc,@SPPerc,@Hamali)
	End 
	
	Fetch CurRate into @PkSrNo /*,@FkBcdSrNo*/,@ItemNo,@FromDate,@PurRate,@MRP,@ASaleRate,@BSaleRate,@CSaleRate,@DSaleRate,@ESaleRate ,
								  @UOMNo ,@StockConversion ,@PerOfRateVariation,@MKTQty,@IsActive,@Stock,@Stock2,@Weight1,@Weight2,@LPPerc,@SPPerc,@Hamali
End 

close CurRate deallocate CurRate 

Return
End




