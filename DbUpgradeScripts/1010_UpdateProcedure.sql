/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created on 08/02/2012
---use in itemmaster save
ALTER PROCEDURE [dbo].[AddMRateSetting2]
     @PkSrNo                              numeric(18),
--   @FkBcdSrNo                           numeric(18),
     @ItemNo                              numeric(18),
     @FromDate                            datetime,
     @PurRate                             numeric(18,4),
	 @MRP				                  numeric(18,4),
	 @UOMNo                               numeric(18),
	 @ASaleRate                           numeric(18,4),
	 @BSaleRate                           numeric(18,4),
	 @CSaleRate                           numeric(18,4),
	 @DSaleRate                           numeric(18,4),
	 @ESaleRate                           numeric(18,4),
	 @StockConversion					  Numeric(18,2),
--	 @PerOfRateVariation				  numeric(18),
     @MKTQty						      numeric(18),
	 @IsActive							  bit,
	 @UserID                              numeric(18),
     @UserDate                            datetime,
	 @CompanyNo							  numeric(18),
     @Weight1                             Numeric(18,2),  
     @Weight2                             Numeric(18,2),
     @LPPerc                             Numeric(18,2),  
     @SPPerc                             Numeric(18,2)
AS
if(cast(@FromDate as datetime) < cast('02-02-1900 00:00:00' as datetime))
begin
	set @FromDate= Convert(datetime,Cast(getdate() as varchar))
end
IF EXISTS(select PkSrNo from MRateSetting
          where
          PkSrNo = @PkSrNo)
     BEGIN
       --Update existing row
       UPDATE MRateSetting
       SET

          ItemNo = @ItemNo,
          FromDate = @FromDate,
          PurRate = @PurRate,
	      MRP = @MRP,
		  UOMNo=@UOMNo,                               
		  ASaleRate = @ASaleRate,
		  BSaleRate = @BSaleRate,
		  CSaleRate = @CSaleRate,
		  DSaleRate = @DSaleRate,
		  ESaleRate = @ESaleRate,
		  StockConversion=@StockConversion,
		  MKTQty=@MKTQty,
		  IsActive=@IsActive,
          UserID = @UserID,
          UserDate = @UserDate,
		  CompanyNo=@CompanyNo,
          StatusNo=2,
          Weight1 = @Weight1,                              
          Weight2 = @Weight2,
          LPPerc = @LPPerc,                              
          SPPerc = @SPPerc  
       WHERE
          PkSrNo = @PkSrNo

     END
ELSE
     BEGIN
       --Insert new row
       Declare @Id numeric
       SELECT @Id=IsNull(Max(PkSrNo),0) From MRateSetting
       DBCC CHECKIDENT('MRateSetting', RESEED, @Id)
       INSERT INTO MRateSetting(
          ItemNo,
          FromDate,
          PurRate,
	      MRP,
		  UOMNo,
		  ASaleRate,
		  BSaleRate,
		  CSaleRate,
		  DSaleRate,
		  ESaleRate,
		  StockConversion,

		  MKTQty,
		  IsActive,
          UserID,
          UserDate,
		  CompanyNo,
          StatusNo,
          Weight1 ,                             
          Weight2,
          LPPerc,
          SPPerc 
)
       VALUES(
          @ItemNo,
          @FromDate,
          @PurRate,
		  @MRP,
		  @UOMNo,
          @ASaleRate,
          @BSaleRate,
          @CSaleRate,
          @DSaleRate,
          @ESaleRate,
		  @StockConversion,
		  @MKTQty,
		  @IsActive,
          @UserID,
          @UserDate,
		  @CompanyNo,
          1,
          @Weight1,                              
          @Weight2,
          @LPPerc,
          @SPPerc 
)

END


/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
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
Stock numeric(18, 2),Stock2 numeric(18, 2),Weight1 numeric(18, 2),Weight2 numeric(18, 2))
AS
Begin
Declare @TempTbl TABLE (PkSrNo numeric(18)/*,FkBcdSrNo numeric(18) */,ItemNo numeric(18),FromDate datetime,PurRate numeric(18,2),MRP numeric(18,2),
ASaleRate numeric(18, 2),BSaleRate numeric(18, 2),CSaleRate numeric(18, 2),DSaleRate numeric(18, 2),ESaleRate numeric(18, 2),
UOMNo numeric(18), StockConversion numeric(18,2),PerOfRateVariation numeric(18, 2),IsActive bit)

Declare @PkSrNo numeric(18) , /*@FkBcdSrNo numeric(18) , */ @ItemNo numeric(18) , 
@FromDate datetime , @PurRate numeric(18,2), @MRP numeric(18,2),@IsActive bit,@Stock numeric(18, 2),@Stock2 numeric(18, 2),
@Weight1 numeric(18, 2),@Weight2 numeric(18, 2),
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
if(@PGroupNo is Null)
Declare CurRate Cursor For Select PkSrNo ,ItemNo/*,MRateSetting.FkBcdSrNo*/,FromDate ,PurRate,MRP,ASaleRate,BSaleRate,CSaleRate,DSaleRate,ESaleRate ,
				 UOMNo , StockConversion ,
				 PerOfRateVariation,MKTQty,IsActive, Stock,Stock2,Weight1,Weight2 From MRateSetting 
				 where IsActive='true' AND  ItemNo=Case When @PItemNo is null then ItemNo else @PItemNo end 
				AND UOMNo=Case When @PUOMNo is null then UOMNo else @PUOMNo end 
				AND MRP=Case When @PMRP is null then MRP else @PMRP end				
				 Order by ItemNo,UOMNo,MRP,FromDate DESC, PkSrNo DESC 
else
	Declare CurRate Cursor For Select MRateSetting.PkSrNo /*,MRateSetting.FkBcdSrNo*/,MRateSetting.ItemNo,MRateSetting.FromDate ,MRateSetting.PurRate,MRateSetting.MRP,MRateSetting.ASaleRate,MRateSetting.BSaleRate,MRateSetting.CSaleRate,MRateSetting.DSaleRate,MRateSetting.ESaleRate ,
				 MRateSetting.UOMNo , MRateSetting.StockConversion ,
				 MRateSetting.PerOfRateVariation,MRateSetting.MKTQty,MRateSetting.IsActive  ,MRateSetting.Stock,MRateSetting.Stock2,MRateSetting.Weight1,MRateSetting.Weight2
 From MRateSetting INNER JOIN 
				 MItemMaster ON MRateSetting.ItemNo=MItemMaster.ItemNo
				 where MRateSetting.IsActive='true' AND  MRateSetting.ItemNo=Case When @PItemNo is null then MRateSetting.ItemNo else @PItemNo end 
				AND MRateSetting.UOMNo=Case When @PUOMNo is null then MRateSetting.UOMNo else @PUOMNo end 
				AND MRateSetting.MRP=Case When @PMRP is null then MRateSetting.MRP else @PMRP end
				AND MItemMaster.GroupNo=@PGroupNo
				 Order by MRateSetting.ItemNo/*,MRateSetting.FkBcdSrNo*/,MRateSetting.UOMNo,MRateSetting.MRP,MRateSetting.FromDate DESC, PkSrNo DESC 
Open CurRate 

Fetch CurRate into @PkSrNo ,@ItemNo,@FromDate,@PurRate,@MRP,@ASaleRate,@BSaleRate,@CSaleRate,@DSaleRate,@ESaleRate ,
								  @UOMNo ,@StockConversion ,@PerOfRateVariation,@MKTQty,@IsActive,@Stock,@Stock2,@Weight1,@Weight2
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
								   @StockConversion ,@PerOfRateVariation,@MKTQty,@IsActive,@Stock,@Stock2,@Weight1,@Weight2)
	End 
	
	Fetch CurRate into @PkSrNo /*,@FkBcdSrNo*/,@ItemNo,@FromDate,@PurRate,@MRP,@ASaleRate,@BSaleRate,@CSaleRate,@DSaleRate,@ESaleRate ,
								  @UOMNo ,@StockConversion ,@PerOfRateVariation,@MKTQty,@IsActive,@Stock,@Stock2,@Weight1,@Weight2
End 

close CurRate deallocate CurRate 

Return
End
GO

/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created on 10/11/2017
ALTER PROCEDURE [dbo].[AddMItemMaster]
     @ItemNo                              numeric(18),
     @ItemName                            varchar(50),
     @ItemShortName                       varchar(50),
     @Barcode                             varchar(50),
     @ShortCode                           varchar(50),
     @GroupNo                             numeric(18),
     @UOMH                                numeric(18),
     @UOML                                numeric(18),
     @UOMDefault                          numeric(18),
     @FkDepartmentNo                      numeric(18),
     @FkCategoryNo                        numeric(18),
     @MinLevel                            numeric(18),
     @MaxLevel                            numeric(18),
    -- @ReOrderLevelQty                     numeric(18),
     @LangFullDesc                        varchar(50),
     @LangShortDesc                       varchar(50),
     @CompanyNo                           numeric(18),
     @IsActive                            bit,
     @UserId                              numeric(18),
     @UserDate                            datetime,
    -- @ModifiedBy                          text,
     --@StatusNo                            int,
     @ControlUnder                        numeric(18),
     @FactorVal                           numeric(18),
     @Margin                              numeric(18),
     @CessValue                           numeric(18,2),
     @PackagingCharges                    numeric(18,2),
     @Dhekhrek                            numeric(18,2),
     @OtherCharges                        numeric(18,2),
     @HigherVariation                     numeric(18),
     @LowerVariation                      numeric(18),
     @HSNCode                             varchar(50),
     @FKStockGroupTypeNo                  numeric(18),
     @ESFlag                              bit,
     @ItemType                            int,
    -- @Stock                               numeric(18),
     @ContainerCharges                    numeric(18,2), 
     @ReturnID		    	INT OUTPUT

AS
IF EXISTS(select ItemNo from MItemMaster
          where
          ItemNo = @ItemNo)
     BEGIN
       --Update existing row
       UPDATE MItemMaster
       SET
          ItemName = @ItemName,
          ItemShortName = @ItemShortName,
          Barcode = @Barcode,
          ShortCode = @ShortCode,
          GroupNo = @GroupNo,
         UOMH = @UOMH,
          UOML = @UOML,         UOMDefault = @UOMDefault,
          FkDepartmentNo = @FkDepartmentNo,
          FkCategoryNo = @FkCategoryNo,
          MinLevel = @MinLevel,
          MaxLevel = @MaxLevel,
          --ReOrderLevelQty = @ReOrderLevelQty,
          LangFullDesc = @LangFullDesc,
          LangShortDesc = @LangShortDesc,
          CompanyNo = @CompanyNo,
          IsActive = @IsActive,
          UserId = @UserId,
          UserDate = @UserDate,
          ModifiedBy =  isnull(ModifiedBy,'') + cast(@UserID as varchar)+'@'+ CONVERT(VARCHAR(10), GETDATE(), 105),
          StatusNo = 1,
          ControlUnder = @ControlUnder,
          FactorVal = @FactorVal,
          Margin = @Margin,
          CessValue = @CessValue,
          PackagingCharges = @PackagingCharges,
          Dhekhrek = @Dhekhrek,
          OtherCharges = @OtherCharges,
          HigherVariation = @HigherVariation,
          LowerVariation = @LowerVariation,
          HSNCode = @HSNCode,
          FKStockGroupTypeNo=@FKStockGroupTypeNo,
          ItemType=@ItemType,
          ESFlag=@ESFlag,
          ContainerCharges=@ContainerCharges
      --    Stock = @Stock
       WHERE
          ItemNo = @ItemNo
     SET @ReturnID =@ItemNo
     END
ELSE
     BEGIN
       --Insert new row
       INSERT INTO MItemMaster(
          ItemName,
          ItemShortName,
          Barcode,
          ShortCode,
          GroupNo,
          UOMH,
          UOML,
          UOMDefault,
          FkDepartmentNo,
          FkCategoryNo,
          MinLevel,
          MaxLevel,
        --  ReOrderLevelQty,
          LangFullDesc,
          LangShortDesc,
          CompanyNo,
          IsActive,
          UserId,
          UserDate,
        --  ModifiedBy,
          StatusNo,
          ControlUnder,
          FactorVal,
          Margin,
          CessValue,
          PackagingCharges,
          Dhekhrek,
          OtherCharges,
          HigherVariation,
          LowerVariation,
          HSNCode,
          FKStockGroupTypeNo,
          ItemType,
          ESFlag,
          ContainerCharges
         -- Stock
)
       VALUES(
          @ItemName,
          @ItemShortName,
          @Barcode,
          @ShortCode,
          @GroupNo,
          @UOMH,
          @UOML,
          @UOMDefault,
          @FkDepartmentNo,
          @FkCategoryNo,
          @MinLevel,
          @MaxLevel,
        --  @ReOrderLevelQty,
          @LangFullDesc,
          @LangShortDesc,
          @CompanyNo,
          @IsActive,
          @UserId,
          @UserDate,
         -- @ModifiedBy,
1,
          @ControlUnder,
          @FactorVal,
          @Margin,
          @CessValue,
          @PackagingCharges,
          @Dhekhrek,
          @OtherCharges,
          @HigherVariation,
          @LowerVariation,
          @HSNCode,
          @FKStockGroupTypeNo,
          @ItemType,
          @ESFlag,
          @ContainerCharges
         -- @Stock
)
     SET @ReturnID = SCOPE_IDENTITY()
END

/*--------------------------------------------------------------------------------------------------------------------------------------------------*/

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created on 09/12/2011
ALTER PROCEDURE [dbo].[AddMLedger1]
     @LedgerNo                            numeric(18),
     @LedgerUserNo                        varchar(100),
     @LedgerName                          varchar(250),
     @GroupNo                             numeric(18),
	 @OpeningBalance					  numeric(18,2),
	 @SignCode							  numeric(18),  
     @MaintainBillByBill                  bit,
	 @IsActive                            bit,
     @ContactPerson                       varchar(100),
     @CompanyNo                           numeric(18),
	 @LedgerStatus						  int,
     @IsEnroll                            bit,
     @IsSendSMS                           bit,
     @UserID                              numeric(18),
     @UserDate                            datetime,
     @TransporterNo                       numeric(18),
     @StateCode                           numeric(18),
	 @LedgerLangName					  varchar(100),
     @IsPartyWiseRate                     bit,
     @QuotationRate                       bit,
     @ContactPersonLang                   nvarchar(500),
     @IsSendEmail                         bit,
     @ReturnID							  int output
AS
IF EXISTS(select LedgerNo from MLedger
          where
          LedgerNo = @LedgerNo)
     BEGIN
       --Update existing row
       UPDATE MLedger
       SET
          LedgerUserNo = @LedgerUserNo,
          LedgerName = @LedgerName,
          GroupNo = @GroupNo,
		  OpeningBalance=@OpeningBalance,
		  SignCode=@SignCode,
          MaintainBillByBill = @MaintainBillByBill,
	      IsActive=@IsActive,
          ContactPerson = @ContactPerson,
          CompanyNo = @CompanyNo,
		  LedgerStatus = @LedgerStatus,
          IsEnroll = @IsEnroll,
          IsSendSMS = @IsSendSMS,
          UserID = @UserID,
          UserDate = @UserDate,
		  TransporterNo= @TransporterNo, 
          StateCode=@StateCode,   
          LedgerLangName=@LedgerLangName,
		  IsPartyWiseRate =  @IsPartyWiseRate,
          QuotationRate=  @QuotationRate, 
          ContactPersonLang=@ContactPersonLang,              
          ModifiedBy = isnull(ModifiedBy,'') + cast(@UserID as varchar)+'@'+ CONVERT(VARCHAR(10), GETDATE(), 105),
          StatusNo=2,
          IsSendEmail=@IsSendEmail

       WHERE
          LedgerNo = @LedgerNo
		  set @ReturnID=@LedgerNo

     END
ELSE
     BEGIN
       --Insert new row
       Declare @Id numeric
       SELECT @Id=IsNull(Max(LedgerNo),0) From MLedger
       DBCC CHECKIDENT('MLedger', RESEED, @Id)
	  --For Max Ledger User No 
	  Select @LedgerUserNo=IsNull(Max(Cast(LedgerUserNo as numeric)),0)+1 from MLedger Where GroupNo=@GroupNo

       INSERT INTO MLedger(
          LedgerUserNo,
          LedgerName,
          GroupNo,
		  OpeningBalance,
		  SignCode,
          MaintainBillByBill,
		  IsActive,
          ContactPerson,
          CompanyNo,
		  LedgerStatus,
          IsEnroll,
          IsSendSMS,
          UserID,
          UserDate,
          TransporterNo,
          StateCode,
		  LedgerLangName,
          IsPartyWiseRate,
		  QuotationRate,
          ContactPersonLang,
          StatusNo,
          IsSendEmail
)
       VALUES(
          @LedgerUserNo,
          @LedgerName,
          @GroupNo,
		  @OpeningBalance,
		  @SignCode,
          @MaintainBillByBill,
		  @IsActive,
          @ContactPerson,
          @CompanyNo,
		  @LedgerStatus,
          @IsEnroll,
          @IsSendSMS,
          @UserID,
          @UserDate,
		  @TransporterNo,
		  @StateCode,
	      @LedgerLangName,
          @IsPartyWiseRate,
          @QuotationRate,
          @ContactPersonLang,
          1,
          @IsSendEmail
)
set @ReturnID=Scope_Identity()

END

/*--------------------------------------------------------------------------------------------------------------------------------------------------*/

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created on umesh 30-10-2018
ALTER PROCEDURE [dbo].[StockUpdateAll]

AS---===========umesh 30-10-2018
--exec StockUpdateAll

update mratesetting set stock=0,stock2=0

--Purchase
update m set m.stock =m.stock + t.qty from mratesetting m 
inner join (select sum(tstock.billedquantity ) as qty,itemno from tstock 
  INNER JOIN  TVoucherEntry ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
where  TVoucherEntry.vouchertypecode = 9 and TVoucherEntry.iscancel='false' and TStock.itype = 0
group by itemno ) t
on m.itemno=t.itemno 
--Opening Stock
update m set m.stock =m.stock + t.qty from mratesetting m 
inner join (select sum(tstock.billedquantity ) as qty,itemno from tstock 
  INNER JOIN TVoucherEntry ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
where  TVoucherEntry.vouchertypecode = 36 and TVoucherEntry.iscancel='false' and TStock.itype = 0
group by itemno ) t
on m.itemno=t.itemno 
--Physical Stock
update m set m.stock =m.stock + t.qty from mratesetting m 
inner join (select sum(tstock.billedquantity ) as qty,itemno from tstock 
  INNER JOIN TVoucherEntry ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
where  TVoucherEntry.vouchertypecode = 8 and TVoucherEntry.iscancel='false' and TStock.itype = 0
group by itemno ) t
on m.itemno=t.itemno 
--sales return
update m set m.stock =m.stock + t.qty from mratesetting m 
inner join (select sum(tstock.billedquantity ) as qty,itemno from tstock 
  INNER JOIN  TVoucherEntry ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
where  TVoucherEntry.vouchertypecode = 12 and TVoucherEntry.iscancel='false' and TStock.itype = 0
group by itemno ) t
on m.itemno=t.itemno 

--sales
update m set m.stock =m.stock - t.qty from mratesetting m 
inner join (select sum(tstock.billedquantity ) as qty,itemno from tstock 
  INNER JOIN TVoucherEntry ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
where  TVoucherEntry.vouchertypecode = 15 and TVoucherEntry.iscancel='false' and TStock.itype = 0
group by itemno ) t
on m.itemno=t.itemno 


--Estimate Purchase
update m set m.stock2 =m.stock2 + t.qty from mratesetting m 
inner join (select sum(tstock.billedquantity ) as qty,itemno from tstock 
  INNER JOIN  TVoucherEntry ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
where  TVoucherEntry.vouchertypecode = 109 and TVoucherEntry.iscancel='false' and TStock.itype = 1
group by itemno ) t
on m.itemno=t.itemno 
--Estimate Sales Return
update m set m.stock2 =m.stock2 + t.qty from mratesetting m 
inner join (select sum(tstock.billedquantity ) as qty,itemno from tstock 
  INNER JOIN  TVoucherEntry ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
where  TVoucherEntry.vouchertypecode = 112 and TVoucherEntry.iscancel='false' and TStock.itype = 1
group by itemno ) t
on m.itemno=t.itemno 

--Estimate Sales 
update m set m.stock2 =m.stock2 - t.qty from mratesetting m 
inner join (select sum(tstock.billedquantity ) as qty,itemno from tstock 
  INNER JOIN TVoucherEntry ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
where  TVoucherEntry.vouchertypecode = 115 and TVoucherEntry.iscancel='false' and TStock.itype = 1
group by itemno ) t
on m.itemno=t.itemno 

--Estimate Opening Stock
update m set m.stock =m.stock + t.qty from mratesetting m 
inner join (select sum(tstock.billedquantity ) as qty,itemno from tstock 
  INNER JOIN TVoucherEntry ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
where  TVoucherEntry.vouchertypecode = 136 and TVoucherEntry.iscancel='false' and TStock.itype = 1
group by itemno ) t
on m.itemno=t.itemno 
--Estimate Physical Stock
update m set m.stock =m.stock + t.qty from mratesetting m 
inner join (select sum(tstock.billedquantity ) as qty,itemno from tstock 
  INNER JOIN TVoucherEntry ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
where  TVoucherEntry.vouchertypecode = 108 and TVoucherEntry.iscancel='false' and TStock.itype = 1
group by itemno ) t
on m.itemno=t.itemno 


update m set m.stock2 =m.stock2 - t.qty from mratesetting m 
inner join (select sum(tstock.billedquantity ) as qty,itemno from tstock 
  INNER JOIN TVoucherEntry ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
where  TVoucherEntry.vouchertypecode = 15 and TVoucherEntry.iscancel='false' and TStock.itype = 1
group by itemno ) t
on m.itemno=t.itemno 

update m set m.stock2 =m.stock2 + t.qty from mratesetting m 
inner join (select sum(tstock.billedquantity ) as qty,itemno from tstock 
  INNER JOIN TVoucherEntry ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
where  TVoucherEntry.vouchertypecode = 12 and TVoucherEntry.iscancel='false' and TStock.itype = 1
group by itemno ) t
on m.itemno=t.itemno 

update m set m.stock =m.stock + t.qty from mratesetting m 
inner join (select sum(tstock.billedquantity ) as qty,itemno from tstock 
  INNER JOIN TVoucherEntry ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
where  TVoucherEntry.vouchertypecode = 9 and TVoucherEntry.iscancel='false' and TStock.itype = 1
group by itemno ) t
on m.itemno=t.itemno 

/*--------------------------------------------------------------------------------------------------------------------------------------------------*/

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetGSTTaxDetailsHSNCodeWise] 

	@FromDate datetime,
	@ToDate datetime,
	@VchType numeric(18)
	
AS
BEGIN
	Declare @PerTable Table(TempPer numeric(18,2),ColNo numeric(18))
declare @StrQry varchar(max)
--	Declare @ColName numeric(18,2),@Cnt numeric(18),@DocNo numeric(18),@Date datetime,@Amt numeric(18,2),@Per numeric(18,2),
--			@Pk numeric(18),@TotTax numeric(18,2), @TotAmt numeric(18,2),@TotAmt2 numeric(18,2),@TaxToal numeric(18,2),@AmtTotal numeric(18,2),
--			@FinalAmount numeric(18,2),@StrQry varchar(max),@TempPer numeric(18,2),@TaxAmt numeric(18,2),@TaxAmt2 numeric(18,2),@TempDate datetime,@GSTNO varchar(50),@HSNCode varchar(50),@UomName varchar(50),@ItemNo numeric(18)
--	Declare @Month varchar(20),@MNo int ,@TDate datetime ,@Yr int,@FrDate datetime,@TempPk numeric(18),@TempColNo numeric(18),
--			@TaxAmount numeric(18,2),@TaxAmount2 numeric(18,2),@TempDocNo numeric(18),@Disc numeric(18,2),@Charges numeric(18,2),@RndOff numeric(18,2),@LedgerName varchar(max)
--	Declare @TVal Table(DocNo numeric(18),Date datetime,LedgerName varchar(max),GSTNO varchar(50),HSNCode varchar(50),UomName varchar(50),ItemNo numeric(18), FinalAmt numeric(18,2),Disc numeric(18,2),Charges numeric(18,2),RndOff numeric(18,2),SAmt1 numeric(18,2),TAmt1 numeric(18,2),TCAmt1 numeric(18,2),
--			SAmt2 numeric(18,2),TAmt2 numeric(18,2),TCAmt2 numeric(18,2),SAmt3 numeric(18,2),TAmt3 numeric(18,2),TCAmt3 numeric(18,2),SAmt4 numeric(18,2),
--			TAmt4 numeric(18,2),TCAmt4 numeric(18,2),SAmt5 numeric(18,2),TAmt5 numeric(18,2),TCAmt5 numeric(18,2),TaxToal numeric(18,2),AmtTotal numeric(18,2))
--	Declare @TDisc Table(LedgNo numeric(18))
--	Declare @TChrg Table(LedgNo numeric(18))
--	set @Cnt=0  set @TempPk=0 set @TaxToal =0 set @AmtTotal =0
--    set @Amt=0 set @Per=0 set @TotTax=0 set @TotAmt=0 set @Disc =0 set @Charges =0 set @RndOff =0
--	 set @StrQry='' set @FrDate=@FromDate set @TempColNo=0  set @TempDocNo=0 set @TempDate='01-01-1900'
--
--set @StrQry='Select distinct TStock.TaxPercentage,0 FROM TVoucherEntry INNER JOIN
--								  TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
--								  WHERE TaxTypeNo= '+Cast(@TaxTypeNo as varchar)+' AND TVoucherEntry.VoucherTypeCode='+cast(@VchType as varchar)+'
--			and TVoucherEntry.VoucherDate>='''+cast(@FromDate as varchar)+''' and TVoucherEntry.VoucherDate<='''+cast(@ToDate as varchar)+''' order by TStock.TaxPercentage'
----(TStock.TaxPercentage <> 0) and
--insert into @PerTable Exec(@StrQry) --select * from @PerTable
--insert into @TDisc Exec('Select LedgerNo From MLedger Where LedgerNo in ('+ @DiscLedg +')')
--insert into @TChrg Exec('Select LedgerNo From MLedger Where LedgerNo in ('+ @ChargesLedg +')')
--
--Declare CurCol Cursor for Select TempPer from @PerTable
--		open CurCol
--		Fetch next from CurCol into @TempPer
--		while (@@Fetch_Status=0)
--		Begin
--			set @Cnt=@Cnt+1	
--			update 	@PerTable set ColNo=@Cnt where 	TempPer=@TempPer
--			Fetch next from CurCol into @TempPer
--		End
--		Close CurCol Deallocate CurCol
--
--select * from @PerTable
--set @Cnt=0 ,VoucherDate
------------------------------------------------------------------------------------------------------------------------------------------------------------------
 					
 set @StrQry='SELECT   HSNCode,ItemGroupName + ''  ''+MItemMaster.ItemName As Itemname,UOMShortCode +'' - ''+ UOMShortCode as Uomname,
sum(TStock.Quantity) as Quantity, sum(tstock.Amount) as Amount,sum((tstock.Netamount)-(TStock.SGSTAmount+TStock.IGSTAmount+TStock.CessAmount+TStock.CGSTAmount)) as NetAmount,
sum(TStock.IGSTAmount) as IGSTAmount ,sum(TStock.CGSTAmount) as CGSTAmount ,sum(TStock.SGSTAmount) as SGSTAmount,sum(TStock.CessAmount) as CessAmount 
FROM TVoucherEntry AS TVoucherEntry_1 INNER JOIN
TStock ON TVoucherEntry_1.PkVoucherNo = TStock.FKVoucherNo  
inner join MItemMaster on TStock.itemno=MItemMaster.itemno
INNER JOIN MItemGroup on MItemGroup.ItemGroupNo=MItemMaster.groupno
inner join muom on tstock.FkUomNo=muom.uomno
WHERE TVoucherEntry_1.VoucherTypeCode='+cast(@VchType as varchar)+' and  VoucherDate>='''+cast(@FromDate as varchar)+''' and VoucherDate<='''+cast(@ToDate as varchar)+'''
and TVoucherEntry_1.iscancel= 0  group by HSNCode,ItemGroupName,ItemName,UOMShortCode,UOMShortCode

order by HSNCode'

 Exec(@StrQry) 
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetSaleVouchEntryDayDtls]
@VchNo int,
@CompNo numeric(18),
@FrDate datetime,
@ToDate datetime,
@Type numeric(18),
@Temp numeric(18)


AS

Declare @TVchNo numeric(18)

if(@VchNo=15)
set @TVchNo=12
else if(@VchNo=115)
set @TVchNo=112
else if(@VchNo=109)
set @TVchNo=113
else if(@VchNo=9)
set @TVchNo=13
else if(@VchNo=12 or @VchNo=13 or @VchNo=113 or @VchNo=112)
begin
set @TVchNo=@VchNo
set @VchNo=-1
end

Begin
if(@Type=1)
Begin

SELECT DISTINCT TVoucherEntry.VoucherDate as Date ,                          
                          (SELECT     LedgerName
                            FROM          MLedger
                            WHERE      (LedgerNo = TVoucherDetails_1.LedgerNo)) AS Party,
			  MVoucherType.VoucherTypeName,
			  (Select count(*)  from TVoucherEntry INNER JOIN
                      TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo
				WHERE (TVoucherEntry.VoucherTypeCode  in (@VchNo)) AND  (TVoucherEntry.VoucherDate >= @FrDate) AND 
				(TVoucherEntry.VoucherDate <=@ToDate) AND (TVoucherDetails.SrNo = 501) And   TVoucherEntry.IsCancel=@Temp )as VoucherNo,
		      TVoucherEntry.VoucherUserNo as BillNo,
					(Select ISNULL(SUM(Debit+Credit), 0) 
                           from TVoucherEntry as TVoucherEntry_1 INNER JOIN
                      TVoucherDetails ON TVoucherEntry_1.PkVoucherNo = TVoucherDetails.FkVoucherNo
                            WHERE      (TVoucherDetails.FKVoucherNo = TVoucherEntry_1.PkVoucherNo) AND (TVoucherDetails.SrNo=501) AND (TVoucherEntry.PayTypeNo  = TVoucherEntry_1.PayTypeNo)
							and TVoucherEntry.IsCancel=@Temp and TVoucherEntry_1.PkVoucherNo=TVoucherEntry.PkVoucherNo) as TotalAmount,
				Case When(TVoucherEntry.MixMode=0) Then MPayType.ShortName Else 'MX' End as Prefix,
				1 AS VchTypeNo 
				FROM    TVoucherEntry INNER JOIN MVoucherType ON TVoucherEntry.VoucherTypeCode = MVoucherType.VoucherTypeCode INNER JOIN
				TVoucherDetails AS TVoucherDetails_1 ON TVoucherEntry.PkVoucherNo = TVoucherDetails_1.FkVoucherNo INNER JOIN
                MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo
				WHERE (TVoucherEntry.VoucherTypeCode in(@VchNo)) AND  (TVoucherEntry.VoucherDate >= @FrDate) AND 
				(TVoucherEntry.VoucherDate <= @ToDate) AND (TVoucherDetails_1.SrNo = 501) and (Select ISNULL(SUM(Debit+Credit), 0) 
                           from TVoucherEntry as TVoucherEntry_1 INNER JOIN
                      TVoucherDetails ON TVoucherEntry_1.PkVoucherNo = TVoucherDetails.FkVoucherNo
                            WHERE      (TVoucherDetails.FKVoucherNo = TVoucherEntry_1.PkVoucherNo) AND (TVoucherDetails.SrNo=501) AND (TVoucherEntry.PayTypeNo  = TVoucherEntry_1.PayTypeNo)
							and TVoucherEntry.IsCancel=@Temp and TVoucherEntry_1.PkVoucherNo=TVoucherEntry.PkVoucherNo)<>0
							
				


union 

SELECT DISTINCT TVoucherEntry.VoucherDate as Date ,                          
                          (SELECT     LedgerName
                            FROM          MLedger
                            WHERE      (LedgerNo = TVoucherDetails_1.LedgerNo)) AS Party,
			  MVoucherType.VoucherTypeName,
			  (Select count(*)  from TVoucherEntry INNER JOIN
                      TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo
				WHERE (TVoucherEntry.VoucherTypeCode  in (@TVchNo)) AND  (TVoucherEntry.VoucherDate >= @FrDate) AND 
				(TVoucherEntry.VoucherDate <=@ToDate) AND (TVoucherDetails.SrNo = 501) And   TVoucherEntry.IsCancel=@Temp )as VoucherNo,
		      TVoucherEntry.VoucherUserNo as BillNo,
					(Select ISNULL(SUM(Debit+Credit), 0) 
                           from TVoucherEntry as TVoucherEntry_1 INNER JOIN
                      TVoucherDetails ON TVoucherEntry_1.PkVoucherNo = TVoucherDetails.FkVoucherNo
                            WHERE      (TVoucherDetails.FKVoucherNo = TVoucherEntry_1.PkVoucherNo) AND (TVoucherDetails.SrNo=501) AND (TVoucherEntry.PayTypeNo  = TVoucherEntry_1.PayTypeNo)
							and TVoucherEntry.IsCancel=@Temp and TVoucherEntry_1.PkVoucherNo=TVoucherEntry.PkVoucherNo)*-1 as TotalAmount,
				Case When(TVoucherEntry.MixMode=0) Then MPayType.ShortName Else 'MX' End as Prefix,
				2 AS VchTypeNo 
				FROM    TVoucherEntry INNER JOIN MVoucherType ON TVoucherEntry.VoucherTypeCode = MVoucherType.VoucherTypeCode INNER JOIN
				TVoucherDetails AS TVoucherDetails_1 ON TVoucherEntry.PkVoucherNo = TVoucherDetails_1.FkVoucherNo INNER JOIN
                MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo
				WHERE (TVoucherEntry.VoucherTypeCode in(@TVchNo)) AND  (TVoucherEntry.VoucherDate >= @FrDate) AND 
				(TVoucherEntry.VoucherDate <= @ToDate) AND (TVoucherDetails_1.SrNo = 501) and (Select ISNULL(SUM(Debit+Credit), 0) 
                           from TVoucherEntry as TVoucherEntry_1 INNER JOIN
                      TVoucherDetails ON TVoucherEntry_1.PkVoucherNo = TVoucherDetails.FkVoucherNo
                            WHERE      (TVoucherDetails.FKVoucherNo = TVoucherEntry_1.PkVoucherNo) AND (TVoucherDetails.SrNo=501) AND (TVoucherEntry.PayTypeNo  = TVoucherEntry_1.PayTypeNo)
							and TVoucherEntry.IsCancel=@Temp and TVoucherEntry_1.PkVoucherNo=TVoucherEntry.PkVoucherNo)*-1<>0


				ORDER BY VchTypeNo,TVoucherEntry.VoucherDate,BillNo




End

if(@Type=2)

Begin
SELECT TVoucherEntry.VoucherDate as Date , '' Party, MVoucherType.VoucherTypeName, 0 as VoucherNo,Count(TVoucherEntry.VoucherDate) as TotalBills,
			  SUM(Debit+Credit) as TotalAmount,'' as Prefix, 1  AS VchTypeNo 
				FROM    TVoucherEntry INNER JOIN MVoucherType ON TVoucherEntry.VoucherTypeCode = MVoucherType.VoucherTypeCode INNER JOIN
				TVoucherDetails AS TVoucherDetails_1 ON TVoucherEntry.PkVoucherNo = TVoucherDetails_1.FkVoucherNo 
				WHERE (TVoucherEntry.VoucherTypeCode in(@VchNo)) AND  (TVoucherEntry.VoucherDate >= @FrDate) AND 
				(TVoucherEntry.VoucherDate <= @ToDate) AND (TVoucherDetails_1.SrNo = 501) AND  TVoucherEntry.IsCancel=@Temp and (Debit+Credit) <>0
                GROUP BY TVoucherEntry.VoucherDate, MVoucherType.VoucherTypeName

union 

SELECT TVoucherEntry.VoucherDate as Date , '' Party, MVoucherType.VoucherTypeName, 0 as VoucherNo, Count(TVoucherEntry.VoucherDate) as TotalBills,
			  SUM(Debit+Credit)*-1 as TotalAmount,'' as Prefix, 2 AS VchTypeNo 
				FROM    TVoucherEntry INNER JOIN MVoucherType ON TVoucherEntry.VoucherTypeCode = MVoucherType.VoucherTypeCode INNER JOIN
				TVoucherDetails AS TVoucherDetails_1 ON TVoucherEntry.PkVoucherNo = TVoucherDetails_1.FkVoucherNo 
				WHERE (TVoucherEntry.VoucherTypeCode in(@TVchNo)) AND  (TVoucherEntry.VoucherDate >= @FrDate) AND 
				(TVoucherEntry.VoucherDate <= @ToDate) AND (TVoucherDetails_1.SrNo = 501) AND  TVoucherEntry.IsCancel=@Temp and (Debit+Credit) <>0
				GROUP BY TVoucherEntry.VoucherDate, MVoucherType.VoucherTypeName

ORDER BY VchTypeNo,Date
end

if(@Type=3)

Begin

SELECT min(TVoucherEntry.VoucherDate) as Date , DateName( month , DateAdd( month , Month(TVoucherEntry.VoucherDate) , 0 ) - 1 ) +' -- '+ 
        Cast(Year(TVoucherEntry.VoucherDate) % 100 as varchar) Party, MVoucherType.VoucherTypeName, 0 as VoucherNo,
        Count(TVoucherEntry.VoucherDate) as TotalBills,  SUM(Debit+Credit) TotalAmount,'' as Prefix, 1 AS VchTypeNo 
				FROM    TVoucherEntry INNER JOIN MVoucherType ON TVoucherEntry.VoucherTypeCode = MVoucherType.VoucherTypeCode INNER JOIN
				TVoucherDetails AS TVoucherDetails_1 ON TVoucherEntry.PkVoucherNo = TVoucherDetails_1.FkVoucherNo 
				WHERE (TVoucherEntry.VoucherTypeCode in(@VchNo)) AND  (TVoucherEntry.VoucherDate >= @FrDate) AND 
				(TVoucherEntry.VoucherDate <= @ToDate) AND (TVoucherDetails_1.SrNo = 501) AND  TVoucherEntry.IsCancel=@Temp  and (Debit+Credit) <>0
                GROUP BY Year(TVoucherEntry.VoucherDate), Month(TVoucherEntry.VoucherDate), MVoucherType.VoucherTypeName

union 

SELECT min(TVoucherEntry.VoucherDate) as Date , DateName( month , DateAdd( month , Month(TVoucherEntry.VoucherDate) , 0 ) - 1 ) +' -- '+ 
        Cast(Year(TVoucherEntry.VoucherDate) % 100 as varchar) Party, MVoucherType.VoucherTypeName, 0 as VoucherNo,
        Count(TVoucherEntry.VoucherDate) as TotalBills,  SUM(Debit+Credit)*-1 TotalAmount,'' as Prefix, 2 AS VchTypeNo 
				FROM    TVoucherEntry INNER JOIN MVoucherType ON TVoucherEntry.VoucherTypeCode = MVoucherType.VoucherTypeCode INNER JOIN
				TVoucherDetails AS TVoucherDetails_1 ON TVoucherEntry.PkVoucherNo = TVoucherDetails_1.FkVoucherNo 
				WHERE (TVoucherEntry.VoucherTypeCode in(@TVchNo)) AND  (TVoucherEntry.VoucherDate >= @FrDate) AND 
				(TVoucherEntry.VoucherDate <= @ToDate) AND (TVoucherDetails_1.SrNo = 501) AND  TVoucherEntry.IsCancel=@Temp and (Debit+Credit) <>0
                GROUP BY Year(TVoucherEntry.VoucherDate), Month(TVoucherEntry.VoucherDate), MVoucherType.VoucherTypeName

ORDER BY VchTypeNo, Date
end

if(@Type=4)

Begin

SELECT '01-01-1900' as Date, 'Quarter-'+Cast(datepart (q, min(TVoucherEntry.VoucherDate)) as varchar) As Party, 
        MVoucherType.VoucherTypeName, 0 as VoucherNo,
        Count(TVoucherEntry.VoucherUserNo) as TotalBills,  SUM(Debit+Credit) TotalAmount,'' as Prefix, 1 AS VchTypeNo 
				FROM    TVoucherEntry INNER JOIN MVoucherType ON TVoucherEntry.VoucherTypeCode = MVoucherType.VoucherTypeCode INNER JOIN
				TVoucherDetails AS TVoucherDetails_1 ON TVoucherEntry.PkVoucherNo = TVoucherDetails_1.FkVoucherNo 
				WHERE (TVoucherEntry.VoucherTypeCode in(@VchNo)) AND  (TVoucherEntry.VoucherDate >= @FrDate) AND 
				(TVoucherEntry.VoucherDate <= @ToDate) AND (TVoucherDetails_1.SrNo = 501) AND  TVoucherEntry.IsCancel=@Temp and (Debit+Credit) <>0
                GROUP BY Year(TVoucherEntry.VoucherDate), datepart (q,TVoucherEntry.VoucherDate), MVoucherType.VoucherTypeName
union 

SELECT '01-01-1900'  as Date, 'Quarter-'+Cast(datepart (q, min(TVoucherEntry.VoucherDate)) as varchar) As Party, 
        MVoucherType.VoucherTypeName, 0 as VoucherNo,
        Count(TVoucherEntry.VoucherDate) as TotalBills,  SUM(Debit+Credit)*-1 TotalAmount,'' as Prefix, 2 AS VchTypeNo 
				FROM    TVoucherEntry INNER JOIN MVoucherType ON TVoucherEntry.VoucherTypeCode = MVoucherType.VoucherTypeCode INNER JOIN
				TVoucherDetails AS TVoucherDetails_1 ON TVoucherEntry.PkVoucherNo = TVoucherDetails_1.FkVoucherNo 
				WHERE (TVoucherEntry.VoucherTypeCode in(@TVchNo)) AND  (TVoucherEntry.VoucherDate >= @FrDate) AND 
				(TVoucherEntry.VoucherDate <= @ToDate) AND (TVoucherDetails_1.SrNo = 501) AND  TVoucherEntry.IsCancel=@Temp and (Debit+Credit) <>0
                GROUP BY Year(TVoucherEntry.VoucherDate),datepart (q,TVoucherEntry.VoucherDate), MVoucherType.VoucherTypeName

order by VchTypeNo, Date, Party
end


End

------------------------------------------------------------------------------------------------------------------------------------------------------------------

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created on 09/12/2011
ALTER PROCEDURE [dbo].[AddMItemTaxInfo1]
     @PkSrNo                              numeric(18),
     @ItemNo                              numeric(18),
     @TaxLedgerNo                         numeric(18),
	 @SalesLedgerNo						  numeric(18),
     @FromDate                            datetime,
     @CalculationMethod                   varchar(50),
     @Percentage                          numeric(18,2),
     @CompanyNo                           numeric(18),
	 @FKTaxSettingNo					  numeric(18),
     @UserID                              numeric(18),
     @UserDate                            datetime
     --@ModifiedBy                          varchar(50)
AS

IF EXISTS(select PkSrNo from MItemTaxInfo
          where
          PkSrNo = @PkSrNo)
     BEGIN
       --Update existing row
       UPDATE MItemTaxInfo
       SET
          ItemNo = @ItemNo,
          TaxLedgerNo = @TaxLedgerNo,
		  SalesLedgerNo=@SalesLedgerNo,
          FromDate = @FromDate,
          CalculationMethod = @CalculationMethod,
          Percentage = @Percentage,
          CompanyNo = @CompanyNo,
		  FKTaxSettingNo = @FKTaxSettingNo,
          UserID = @UserID,
          UserDate = @UserDate,
          ModifiedBy = isnull(ModifiedBy,'') + cast(@UserID as varchar)+'@'+ CONVERT(VARCHAR(10), GETDATE(), 105),
          StatusNo=2
       WHERE
          PkSrNo = @PkSrNo

     END
ELSE 

	BEGIN
	   --Insert new row
	   Declare @Id numeric
	   SELECT @Id=IsNull(Max(PkSrNo),0) From MItemTaxInfo
	   DBCC CHECKIDENT('MItemTaxInfo', RESEED, @Id)
	   INSERT INTO MItemTaxInfo(
		  ItemNo,
		  TaxLedgerNo,
		  SalesLedgerNo,
		  FromDate,
		  CalculationMethod,
		  Percentage,
		  CompanyNo,
		  FKTaxSettingNo,
		  UserID,
		  UserDate,
          StatusNo
		 -- ModifiedBy
       )
	   VALUES(
		  @ItemNo,
		  @TaxLedgerNo,
		  @SalesLedgerNo,
		  @FromDate,
		  @CalculationMethod,
		  @Percentage,
		  @CompanyNo,
		  @FKTaxSettingNo,
		  @UserID,
		  @UserDate,
          1
		  --@ModifiedBy
       )

	 
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created on 09/12/2011
ALTER PROCEDURE [dbo].[AddMLedgerDetails]
     @LedgerDetailsNo                           numeric(18),
     @LedgerNo                            numeric(18),
     @CreditLimit                         numeric(18,2),
     @CreditDays                          numeric(18),
     @Address                             varchar(300),
     @StateNo                             numeric(18),
     @CityNo                              numeric(18),
     @PinCode                             varchar(100),
     @PhNo1                               varchar(100),
     @PhNo2                               varchar(100),
     @MobileNo1                           varchar(100),
     @MobileNo2                           varchar(100),
     @EmailID                             varchar(100),
     @CustomerType                        numeric(18,0),
     @PANNo                               varchar(100),
     @AccountNo                           varchar(150),
     @ReportName                          varchar(max),
     @UserID                              numeric(18),
     @UserDate                            datetime,
	 @CompanyNo						      numeric(18),
	 @FSSAI								  varchar(100),
	 @AreaNo							  numeric(18),
	 @AddressLang                         nvarchar(500),
     @RateTypeNo						  numeric(18),
     @DiscPer						      numeric(18),
     @DiscRs					      	  numeric(18),
     @AdharNo					          varchar(100),
     @AnyotherNo1					      varchar(100),
     @AnyotherNo2				      	  varchar(100),
     @GSTNo				            	  varchar(100),
     @FSSAIDate                           datetime,
     @GSTDate                             datetime,
	 @Distance							  numeric(18,2)
AS
IF EXISTS(select LedgerDetailsNo from MLedgerDetails
          where
          LedgerDetailsNo = @LedgerDetailsNo)
     BEGIN
       --Update existing row
       UPDATE MLedgerDetails
       SET
          LedgerNo = @LedgerNo,
          CreditLimit=@CreditLimit,
          CreditDays=@CreditDays, 
          Address = @Address,
	      StateNo=@StateNo,
          CityNo = @CityNo,
          PinCode = @PinCode,
          PhNo1 = @PhNo1,
          PhNo2 = @PhNo2,
          MobileNo1 = @MobileNo1,
          MobileNo2 = @MobileNo2,
          EmailID = @EmailID,
          CustomerType = @CustomerType,
          PANNo=@PANNo,
          AccountNo=@AccountNo,
          
         ReportName=@ReportName,
          UserID = @UserID,
          UserDate = @UserDate,
		  CompanyNo = @CompanyNo,
		  FSSAI = @FSSAI,	
	      AreaNo = @AreaNo,
          ModifiedBy =  isnull(ModifiedBy,'') + cast(@UserID as varchar)+'@'+ CONVERT(VARCHAR(10), GETDATE(), 105),
          StatusNo=2,
		  AddressLang=@AddressLang ,
		  RateTypeNo=@RateTypeNo,
		  DiscPer	=@DiscPer,
		  DiscRs	=@DiscRs,
		  AdharCardNo=@AdharNo,
          AnyotherNo1=@AnyotherNo1,
          AnyotherNo2=@AnyotherNo2,
		  GSTNo=@GSTNo,
          FSSAIDate=@FSSAIDate,
		  GSTDate=@GSTDate,
		  Distance = @Distance

       WHERE
          LedgerDetailsNo = @LedgerDetailsNo

     END
ELSE
     BEGIN
       --Insert new row
       Declare @Id numeric
       SELECT @Id=IsNull(Max(LedgerDetailsNo),0) From MLedgerDetails
       DBCC CHECKIDENT('MLedgerDetails', RESEED, @Id)
       INSERT INTO MLedgerDetails(
          LedgerNo,
          CreditLimit,
          CreditDays,
          Address,
		  StateNo,
          CityNo,
          PinCode,
          PhNo1,
          PhNo2,
          MobileNo1,
          MobileNo2,
          EmailID,     
          CustomerType,
          PANNo,
          AccountNo,
          ReportName,      
          UserID,
          UserDate,
          StatusNo,
		  CompanyNo,  
		  FSSAI,
	      AreaNo,
		  AddressLang,
		  RateTypeNo,
		  DiscPer,
		  DiscRs,
          AdharCardNo,
	      AnyotherNo1,
		  AnyotherNo2,
		  GSTNo,
		  FSSAIDate,
		  GSTDate,
		  Distance
)
       VALUES(
          @LedgerNo,
          @CreditLimit,
          @CreditDays,
          @Address,
		  @StateNo,
          @CityNo,
          @PinCode,
          @PhNo1,
          @PhNo2,
          @MobileNo1,
          @MobileNo2,
          @EmailID,
          @CustomerType,
          @PANNo,
          @AccountNo,
          @ReportName,
          @UserID,
          @UserDate,
          1,
		  @CompanyNo,
		  @FSSAI,
	      @AreaNo,
		  @AddressLang,
		  @RateTypeNo,
          @DiscPer,
          @DiscRs,
          @AdharNo,
		  @AnyotherNo1,
		  @AnyotherNo2,
		  @GSTNo,
		  @FSSAIDate,
		  @GSTDate,
		  @Distance
)

END

------------------------------------------------------------------------------------------------------------------------------------------------------------------

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetGSTTaxDetailsCess] 
	@FromDate datetime,
	@ToDate datetime,
	@Type numeric(18),
	@VchType numeric(18),
	@DiscLedg varchar(max),
	@ChargesLedg varchar(max),
	@RoundOffLedgNo numeric(18),
	@TaxTypeNo	numeric(18)
AS
BEGIN
	Declare @PerTable Table(TempPer numeric(18,2),ColNo numeric(18))
	Declare @ColName numeric(18,2),@Cnt numeric(18),@DocNo numeric(18),@Date datetime,@Amt numeric(18,2),@Per numeric(18,2),
			@Pk numeric(18),@TotTax numeric(18,2), @TotAmt numeric(18,2),@TotAmt2 numeric(18,2),@TaxToal numeric(18,2),@AmtTotal numeric(18,2),
			@FinalAmount numeric(18,2),@StrQry varchar(max),@TempPer numeric(18,2),@TaxAmt numeric(18,2),@TaxAmt2 numeric(18,2),@TaxAmt3 numeric(18,2),@TaxAmt4 numeric(18,2),@TempDate datetime,@GSTNO varchar(50),@HSNCode varchar(50),@StateName varchar(100),@UomName varchar(50),@ItemNo numeric(18)
	Declare @Month varchar(20),@MNo int ,@TDate datetime ,@Yr int,@FrDate datetime,@TempPk numeric(18),@TempColNo numeric(18),
			@SGSTAmount numeric(18,2),@SGSTAmount2 numeric(18,2),@SGSTAmount3 numeric(18,2),@SGSTAmount4 numeric(18,2),@TempDocNo numeric(18),@Disc numeric(18,2),@Charges numeric(18,2),@RndOff numeric(18,2),@LedgerName varchar(max)
	Declare @TVal Table(DocNo numeric(18),Date datetime,LedgerName varchar(max),GSTNO varchar(50),HSNCode varchar(50),StateName varchar(100),UomName varchar(50), FinalAmt numeric(18,2),Disc numeric(18,2),Charges numeric(18,2),RndOff numeric(18,2),SAmt1 numeric(18,2),TAmt1 numeric(18,2),TCAmt1 numeric(18,2),TIAmt1 numeric(18,2),TCeAmt1 numeric(18,2),
			SAmt2 numeric(18,2),TAmt2 numeric(18,2),TCAmt2 numeric(18,2),TIAmt2 numeric(18,2),TCeAmt2 numeric(18,2),SAmt3 numeric(18,2),TAmt3 numeric(18,2),TCAmt3 numeric(18,2),TIAmt3 numeric(18,2),TCeAmt3 numeric(18,2),SAmt4 numeric(18,2),
			TAmt4 numeric(18,2),TCAmt4 numeric(18,2),TIAmt4 numeric(18,2),TCeAmt4 numeric(18,2),SAmt5 numeric(18,2),TAmt5 numeric(18,2),TIAmt5 numeric(18,2),TCeAmt5 numeric(18,2),TCAmt5 numeric(18,2),TaxToal numeric(18,2),AmtTotal numeric(18,2),ItemNo numeric(18))
	Declare @TDisc Table(LedgNo numeric(18))
	Declare @TChrg Table(LedgNo numeric(18))
	

	set @Cnt=0  set @TempPk=0 set @TaxToal =0 set @AmtTotal =0
    set @Amt=0 set @Per=0 set @TotTax=0 set @TotAmt=0 set @Disc =0 set @Charges =0 set @RndOff =0
	 set @StrQry='' set @FrDate=@FromDate set @TempColNo=0  set @TempDocNo=0 set @TempDate='01-01-1900'



set @StrQry='Select distinct TStock.SGSTPercentage,0 FROM TVoucherEntry INNER JOIN
								  TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
								  WHERE TaxTypeNo= '+Cast(@TaxTypeNo as varchar)+' AND TVoucherEntry.VoucherTypeCode='+cast(@VchType as varchar)+'
			and TVoucherEntry.VoucherDate>='''+cast(@FromDate as varchar)+''' and TVoucherEntry.iscancel= 0 
and TVoucherEntry.VoucherDate<='''+cast(@ToDate as varchar)+''' order by TStock.SGSTPercentage'
--(TStock.SGSTPercentage <> 0) and

insert into @PerTable Exec(@StrQry) --select * from @PerTable

insert into @TDisc Exec('Select LedgerNo From MLedger Where LedgerNo in ('+ @DiscLedg +')')
insert into @TChrg Exec('Select LedgerNo From MLedger Where LedgerNo in ('+ @ChargesLedg +')')

Declare CurCol Cursor for Select TempPer from @PerTable

		open CurCol
		Fetch next from CurCol into @TempPer
		while (@@Fetch_Status=0)
		Begin
			
			
			set @Cnt=@Cnt+1	
			update 	@PerTable set ColNo=@Cnt where 	TempPer=@TempPer
			Fetch next from CurCol into @TempPer
		End
		Close CurCol Deallocate CurCol

select * from @PerTable
set @Cnt=0
------------------------------------------------------------------------------------------------------------------------------------------------------------------
if(@Type =2 )
BEGIN
Declare InsValue Cursor for SELECT     TVoucherEntry_1.VoucherUserNo, TVoucherEntry_1.VoucherDate, TStock.SGSTPercentage, TVoucherEntry_1.PkVoucherNo, 
                      TVoucherEntry_1.BilledAmount, TStock.NetAmount AS SAmt1, TStock.SGSTAmount AS TAmt,TStock.CGSTAmount AS TCAmt,TStock.IGSTAmount AS TIAmt, TStock.CessAmount AS TCeAmt,  (TStock.DiscAmount+TStock.DiscRupees) as Disc,
                          (SELECT     isNull(SUM(CASE WHEN (Debit <> 0) THEN Debit ELSE -Credit END),0) 
                            FROM          TVoucherEntry AS TVoucherEntry_3 INNER JOIN
                                                   TVoucherDetails AS TVoucherDetails_3 ON TVoucherEntry_3.PkVoucherNo = TVoucherDetails_3.FkVoucherNo
                            WHERE      (TVoucherDetails_3.LedgerNo IN
                                                       (@RoundOffLedgNo)) AND (TVoucherDetails_3.FkVoucherNo = TVoucherEntry_1.PkVoucherNo)) ,
HSNCode,TStock.Itemno,
                          (SELECT     isNull(SUM(CASE WHEN (Debit <> 0) THEN Debit ELSE Credit END),0)
                            FROM          TVoucherEntry AS TVoucherEntry_2 INNER JOIN
                                                   TVoucherDetails AS TVoucherDetails_2 ON TVoucherEntry_2.PkVoucherNo = TVoucherDetails_2.FkVoucherNo
                            WHERE      (TVoucherDetails_2.LedgerNo IN
                                                       (SELECT LedgNo FROM @TChrg)) AND (TVoucherDetails_2.FkVoucherNo = TVoucherEntry_1.PkVoucherNo)) 
                     
				FROM TVoucherEntry AS TVoucherEntry_1 INNER JOIN
                     TStock ON TVoucherEntry_1.PkVoucherNo = TStock.FKVoucherNo  inner join MStockItems on TStock.itemno=MStockItems.itemno
 inner join MUOM On Muom.UOMNo=TStock.FkUomNo
				WHERE TVoucherEntry_1.VoucherTypeCode=@VchType	 and TVoucherEntry_1.VoucherDate>=@FromDate and TVoucherEntry_1.VoucherDate<=@ToDate  AND (TVoucherEntry_1.IsCancel = 'false')
					  AND TVoucherEntry_1.TaxTypeNo= @TaxTypeNo --and TVoucherEntry_1.PkVoucherNo=14246
				--Group by TVoucherEntry_1.VoucherUserNo,MStockItems.HSNCode,TVoucherEntry_1.VoucherDate,TStock.SGSTPercentage,TVoucherEntry_1.PkVoucherNo,BilledAmount,TStock.NetAmount,TStock.SGSTAmount,TStock.CGSTAmount,TStock.DiscAmount,TStock.DiscRupees
				order by TVoucherEntry_1.VoucherUserNo,TStock.SGSTPercentage

		open InsValue
		fetch next from InsValue into @DocNo,@Date,@Per,@Pk,@FinalAmount,@Amt,@SGSTAmount,@SGSTAmount2,@SGSTAmount3,@SGSTAmount4,@Disc,@RndOff,@HSNCode,@ItemNo,@Charges
			While(@@Fetch_Status=0)
				Begin
						--Select @LedgerName=LedgerName FRom MLedger Where LedgerNo in(Select LedgerNo From TVoucherDetails Where FKVoucherNo=@Pk AND VoucherSrNo=1)
						Select @LedgerName=LedgerName,@StateName=StateName,@GSTNO=Case When(IsNull(GSTNO,'NA')='') Then '' Else  IsNull(GSTNO,'') end FRom MLedger inner Join MLedgerDetails On MLedger.LedgerNo=MLedgerDetails.LedgerNo inner join MState on MLedgerDetails.Stateno=MState.Stateno Where MLedger.LedgerNo in(Select LedgerNo From TVoucherDetails Where FKVoucherNo=@Pk AND VoucherSrNo=1)
					
							set @TempPk=@Pk
							insert into @TVal (DocNo ,Date ,LedgerName,GSTNO, FinalAmt,Disc ,Charges ,RndOff, HSNCode,StateName,Itemno,SAmt1,TAmt1,TCAmt1,TIAmt1,TCeAmt1,SAmt2,TAmt2,TCAmt2,TIAmt2,TCeAmt2,SAmt3,TAmt3,TCAmt3,TIAmt3,TCeAmt3,SAmt4,TAmt4,TCAmt4,TIAmt4,TCeAmt4,SAmt5,TAmt5,TCAmt5,TIAmt5,TCeAmt5) 
                            Values(@DocNo,@Date,@LedgerName,@GSTNO,@FinalAmount ,@Disc,@Charges,@RndOff,@HSNCode,@StateName,@ItemNo,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0)
							set @Cnt=@Cnt+1	

						select @TempColNo=ColNo from @PerTable where TempPer=@Per

						if (@TempColNo = 1)
							update @TVal set SAmt1 = @Amt , TAmt1 = @SGSTAmount , TCAmt1 = @SGSTAmount2 , TIAmt1 = @SGSTAmount3, TCeAmt1 = @SGSTAmount4 where DocNo = @DocNo and Itemno=@ItemNo
						if (@TempColNo = 2)
							update @TVal set SAmt2 = @Amt , TAmt2 = @SGSTAmount , TCAmt2 = @SGSTAmount2, TIAmt2 = @SGSTAmount3, TCeAmt2 = @SGSTAmount4  where DocNo = @DocNo and Itemno=@ItemNo
						if (@TempColNo = 3)
							update @TVal set SAmt3 = @Amt , TAmt3 = @SGSTAmount , TCAmt3 = @SGSTAmount2 , TIAmt3 = @SGSTAmount3 , TCeAmt3 = @SGSTAmount4  where DocNo = @DocNo and Itemno=@ItemNo
						if (@TempColNo = 4)
							update @TVal set SAmt4 = @Amt , TAmt4 = @SGSTAmount , TCAmt4 = @SGSTAmount2 , TIAmt4 = @SGSTAmount3, TCeAmt4 = @SGSTAmount4 where DocNo = @DocNo and Itemno=@ItemNo
						if (@TempColNo = 5)
						update @TVal set SAmt5 = @Amt , TAmt5 = @SGSTAmount , TCAmt5 = @SGSTAmount2 , TIAmt5 = @SGSTAmount3 , TCeAmt5 = @SGSTAmount4  where DocNo = @DocNo and Itemno=@ItemNo
						set @TaxToal =@TaxToal+@SGSTAmount+@SGSTAmount2+@SGSTAmount3+@SGSTAmount4
					   set @AmtTotal =@AmtTotal+@Amt
					   set @TempDocNo=@DocNo


--if (@@Fetch_Status=0)
					fetch next from InsValue into @DocNo,@Date,@Per,@Pk,@FinalAmount,@Amt,@SGSTAmount,@SGSTAmount2,@SGSTAmount3,@SGSTAmount4,@Disc,@RndOff,@HSNCode,@ItemNo,@Charges
				End

close InsValue deallocate InsValue
if(@TempPk<>0)
							BEGIN
							update @TVal set TaxToal=@TaxToal,AmtTotal=@AmtTotal where DocNo = @TempDocNo and Itemno=@ItemNo
							set @AmtTotal =0 set @TaxToal =0
							set @TempDocNo=@DocNo
							END
End
------------------------------------------------------------------------------------------------------------------------------------------------------------------
if(@Type =1 )
BEGIN
Declare InsValue Cursor for SELECT    case when @VchType=15 then   TVoucherEntry_1.VoucherUserNo else TVoucherEntry_1.Reference end as VoucherUserNo, TVoucherEntry_1.VoucherDate, TStock.SGSTPercentage, TVoucherEntry_1.PkVoucherNo, 
                      TVoucherEntry_1.BilledAmount, SUM(TStock.NetAmount) AS SAmt, SUM(TStock.SGSTAmount) AS TAmt,SUM(TStock.CGSTAmount) AS TCAmt,SUM(TStock.IGSTAmount) AS TCAmt,SUM(TStock.CessAmount) AS TCAmt,sum(TStock.DiscAmount+TStock.DiscRupees) as Disc,
--                          (SELECT     isNull(SUM(CASE WHEN (Debit <> 0) THEN Debit ELSE Credit END),0) 
--                            FROM          TVoucherEntry INNER JOIN
--                                                   TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo
--                            WHERE      (TVoucherDetails.LedgerNo IN
--                                                       (SELECT LedgNo FROM @TDisc )) AND (TVoucherDetails.FkVoucherNo = TVoucherEntry_1.PkVoucherNo)) ,
                        
                          (SELECT     isNull(SUM(CASE WHEN (Debit <> 0) THEN Debit ELSE -Credit END),0) 
                            FROM          TVoucherEntry AS TVoucherEntry_3 INNER JOIN
                                                   TVoucherDetails AS TVoucherDetails_3 ON TVoucherEntry_3.PkVoucherNo = TVoucherDetails_3.FkVoucherNo
                            WHERE      (TVoucherDetails_3.LedgerNo IN
                                                       (@RoundOffLedgNo)) AND (TVoucherDetails_3.FkVoucherNo = TVoucherEntry_1.PkVoucherNo)) ,
                          (SELECT     isNull(SUM(CASE WHEN (Debit <> 0) THEN Debit ELSE Credit END),0)
                            FROM          TVoucherEntry AS TVoucherEntry_2 INNER JOIN
                                                   TVoucherDetails AS TVoucherDetails_2 ON TVoucherEntry_2.PkVoucherNo = TVoucherDetails_2.FkVoucherNo
                            WHERE      (TVoucherDetails_2.LedgerNo IN
                                                       (SELECT LedgNo FROM @TChrg)) AND (TVoucherDetails_2.FkVoucherNo = TVoucherEntry_1.PkVoucherNo)) 
                     
				FROM TVoucherEntry AS TVoucherEntry_1 INNER JOIN
                     TStock ON TVoucherEntry_1.PkVoucherNo = TStock.FKVoucherNo 
				WHERE TVoucherEntry_1.VoucherTypeCode=@VchType	 and TVoucherEntry_1.VoucherDate>=@FromDate and TVoucherEntry_1.VoucherDate<=@ToDate  AND (TVoucherEntry_1.IsCancel = 'false')
					  AND TVoucherEntry_1.TaxTypeNo= @TaxTypeNo
				Group by TVoucherEntry_1.VoucherUserNo,Reference,TVoucherEntry_1.VoucherDate,TStock.SGSTPercentage,TVoucherEntry_1.PkVoucherNo,BilledAmount
				order by TVoucherEntry_1.VoucherUserNo,TStock.SGSTPercentage

		open InsValue
		fetch next from InsValue into @DocNo,@Date,@Per,@Pk,@FinalAmount,@Amt,@SGSTAmount,@SGSTAmount2,@SGSTAmount3,@SGSTAmount4,@Disc,@RndOff,@Charges
			While(@@Fetch_Status=0)
				Begin
						--Select @LedgerName=LedgerName FRom MLedger Where LedgerNo in(Select LedgerNo From TVoucherDetails Where FKVoucherNo=@Pk AND VoucherSrNo=1)
						Select @LedgerName=LedgerName,@GSTNO=Case When(IsNull(GSTNO,'NA')='') Then '' Else  IsNull(GSTNO,'') end FRom MLedger inner Join MLedgerDetails On MLedger.LedgerNo=MLedgerDetails.LedgerNo Where MLedger.LedgerNo in(Select LedgerNo From TVoucherDetails Where FKVoucherNo=@Pk AND VoucherSrNo=1)
					
                    if(@TempPk<>@Pk)
						BEgin
							if(@TempPk<>0)
							BEGIN								
							update @TVal set TaxToal=@TaxToal,AmtTotal=@AmtTotal where DocNo = @TempDocNo							
							set @AmtTotal =0 set @TaxToal =0
							set @TempDocNo=@DocNo
							END
--							if(@Cnt>2000)
--							BEGIn
--								Select * from @TVal
--								Delete from @TVal
--								set @Cnt=0
--							END
							set @TempPk=@Pk
							insert into @TVal (DocNo ,Date ,LedgerName,GSTNO, FinalAmt,Disc ,Charges ,RndOff ,SAmt1,TAmt1,TCAmt1,TIAmt1,TCeAmt1,SAmt2,TAmt2,TCAmt2,TIAmt2,TCeAmt2,SAmt3,TAmt3,TCAmt3,TIAmt3,TCeAmt3,SAmt4,TAmt4,TCAmt4,TIAmt4,TCeAmt4,SAmt5,TAmt5,TCAmt5,TIAmt5,TCeAmt5) 
                            Values(@DocNo,@Date,@LedgerName,@GSTNO,@FinalAmount ,@Disc,@Charges,@RndOff,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0)
							set @Cnt=@Cnt+1	
						End
						select @TempColNo=ColNo from @PerTable where TempPer=@Per

						if (@TempColNo = 1)
							update @TVal set SAmt1 = @Amt , TAmt1 = @SGSTAmount , TCAmt1 = @SGSTAmount2 , TIAmt1 = @SGSTAmount3, TCeAmt1 = @SGSTAmount4 where DocNo = @DocNo
						if (@TempColNo = 2)
							update @TVal set SAmt2 = @Amt , TAmt2 = @SGSTAmount , TCAmt2 = @SGSTAmount2 , TIAmt2 = @SGSTAmount3, TCeAmt2 = @SGSTAmount4 where DocNo = @DocNo
						if (@TempColNo = 3)
							update @TVal set SAmt3 = @Amt , TAmt3 = @SGSTAmount , TCAmt3 = @SGSTAmount2 , TIAmt3 = @SGSTAmount3 , TCeAmt3 = @SGSTAmount4 where DocNo = @DocNo
						if (@TempColNo = 4)
							update @TVal set SAmt4 = @Amt , TAmt4 = @SGSTAmount , TCAmt4 = @SGSTAmount2 , TIAmt4 = @SGSTAmount3, TCeAmt4 = @SGSTAmount4 where DocNo = @DocNo
						if (@TempColNo = 5)
							update @TVal set SAmt5 = @Amt , TAmt5 = @SGSTAmount , TCAmt5 = @SGSTAmount2 , TIAmt5 = @SGSTAmount3 , TCeAmt5 = @SGSTAmount4 where DocNo = @DocNo
						set @TaxToal =@TaxToal+@SGSTAmount+@SGSTAmount2+@SGSTAmount3+@SGSTAmount4
						set @AmtTotal =@AmtTotal+@Amt
						set @TempDocNo=@DocNo
					fetch next from InsValue into @DocNo,@Date,@Per,@Pk,@FinalAmount,@Amt,@SGSTAmount,@SGSTAmount2,@SGSTAmount3,@SGSTAmount4,@Disc,@RndOff,@Charges
				End

close InsValue deallocate InsValue
if(@TempPk<>0)
							BEGIN
							update @TVal set TaxToal=@TaxToal,AmtTotal=@AmtTotal where DocNo = @TempDocNo
							set @AmtTotal =0 set @TaxToal =0
							set @TempDocNo=@DocNo
							END
End
--------------------------------------------------------------------------------------------------------------------------------------------------------------
if(@Type=3)
Begin
	Declare InsValue Cursor for SELECT     Count(TVoucherEntry_1.PkVoucherNo) as DocNo, TVoucherEntry_1.VoucherDate, -1 as SGSTPercentage,
                      SUM(TVoucherEntry_1.BilledAmount) AS SAmt, 0 as  TAmt,
                          (SELECT     isNull(SUM(CASE WHEN (Debit <> 0) THEN Debit ELSE Credit END),0) 
                            FROM          TVoucherEntry INNER JOIN
                                                   TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo
                            WHERE      (TVoucherDetails.LedgerNo IN
                                                       (SELECT LedgNo FROM @TDisc)) AND (TVoucherEntry.VoucherDate=TVoucherEntry_1.VoucherDate) AND (TVoucherEntry.VoucherTypeCode=@VchType)) as Disc,
                          (SELECT     isNull(SUM(CASE WHEN (Debit <> 0) THEN Debit ELSE -Credit END),0) 
                            FROM          TVoucherEntry AS TVoucherEntry_3 INNER JOIN
                                                   TVoucherDetails AS TVoucherDetails_3 ON TVoucherEntry_3.PkVoucherNo = TVoucherDetails_3.FkVoucherNo
                            WHERE      (TVoucherDetails_3.LedgerNo IN
                                                       (@RoundOffLedgNo)) AND (TVoucherEntry_3.VoucherDate=TVoucherEntry_1.VoucherDate) AND (TVoucherEntry_3.VoucherTypeCode=@VchType)) as RndOff,
                          (SELECT     isNull(SUM(CASE WHEN (Debit <> 0) THEN Debit ELSE Credit END),0)
							 FROM  TVoucherEntry AS TVoucherEntry_2 INNER JOIN TVoucherDetails AS TVoucherDetails_2 ON TVoucherEntry_2.PkVoucherNo = TVoucherDetails_2.FkVoucherNo
                            WHERE      (TVoucherDetails_2.LedgerNo IN (SELECT LedgNo FROM @TChrg)) AND (TVoucherEntry_2.VoucherDate=TVoucherEntry_1.VoucherDate) AND (TVoucherEntry_2.VoucherTypeCode=@VchType)) as Charges
                     
			FROM         TVoucherEntry AS TVoucherEntry_1 
								 WHERE      TVoucherEntry_1.VoucherTypeCode=@VchType	 and TVoucherEntry_1.VoucherDate>=@FromDate and TVoucherEntry_1.VoucherDate<=@ToDate AND (TVoucherEntry_1.IsCancel = 'false')
										AND TVoucherEntry_1.TaxTypeNo= @TaxTypeNo
								Group by TVoucherEntry_1.VoucherDate					
			Union all
			SELECT     0 as DocNo,TVoucherEntry_1.VoucherDate, TStock.SGSTPercentage,SUM(TStock.NetAmount) AS SAmt, SUM(TStock.SGSTAmount) AS TAmt,
									   0 as Disc ,0 as RndOff ,0 as Charges                     
			FROM         TVoucherEntry AS TVoucherEntry_1 INNER JOIN
								  TStock ON TVoucherEntry_1.PkVoucherNo = TStock.FKVoucherNo 
								 WHERE      TVoucherEntry_1.VoucherTypeCode=@VchType	 and TVoucherEntry_1.VoucherDate>=@FromDate and TVoucherEntry_1.VoucherDate<=@ToDate  AND (TVoucherEntry_1.IsCancel = 'false')
										AND TVoucherEntry_1.TaxTypeNo= @TaxTypeNo
								Group by TVoucherEntry_1.VoucherDate,TStock.SGSTPercentage
			order by VoucherDate,SGSTPercentage
		open InsValue
		fetch next from InsValue into @DocNo,@Date,@Per,@Amt,@SGSTAmount,@Disc,@RndOff,@Charges
			While(@@Fetch_Status=0)
			Begin
				if(@Per=-1)
				Begin
					if(@TempDate<>'01-01-1900')
					Begin
						update @TVal set TaxToal=@TaxToal,AmtTotal=@AmtTotal where Date = @TempDate
						set @AmtTotal =0 set @TaxToal =0 set @TempDate=@Date					
					End
					insert into @TVal (DocNo ,Date ,LedgerName,FinalAmt,Disc ,Charges ,RndOff ,SAmt1,TAmt1,SAmt2,TAmt2,SAmt3,TAmt3,SAmt4,TAmt4,SAmt5,TAmt5) 
                            Values(@DocNo,@Date,@LedgerName,@Amt,@Disc,@Charges,@RndOff,0,0,0,0,0,0,0,0,0,0 )
				End
				Else
				Begin
					select @TempColNo=ColNo from @PerTable where TempPer=@Per
						if (@TempColNo = 1)
							update @TVal set SAmt1 = @Amt , TAmt1 = @SGSTAmount where Date = @Date
						if (@TempColNo = 2)
							update @TVal set SAmt2 = @Amt , TAmt2 = @SGSTAmount where Date = @Date
						if (@TempColNo = 3)
							update @TVal set SAmt3 = @Amt , TAmt3 = @SGSTAmount where Date = @Date
						if (@TempColNo = 4)
							update @TVal set SAmt4 = @Amt , TAmt4 = @SGSTAmount where Date = @Date
						if (@TempColNo = 5)
							update @TVal set SAmt5 = @Amt , TAmt5 = @SGSTAmount where Date = @Date
						set @TaxToal =@TaxToal+@SGSTAmount
						set @AmtTotal =@AmtTotal+@Amt
						set @TempDocNo=@DocNo
						set @TempDate=@Date
				
				End
				fetch next from InsValue into @DocNo,@Date,@Per,@Amt,@SGSTAmount,@Disc,@RndOff,@Charges
			End
close InsValue deallocate InsValue
if(@TempDate<>'01-01-1900')
					Begin
						update @TVal set TaxToal=@TaxToal,AmtTotal=@AmtTotal where Date = @TempDate
						set @AmtTotal =0 set @TaxToal =0 set @TempDate=@Date					
					End		

End
------------------------------------------------------------------------------------------------------------------------------------------------------------------
if(@Type=3)
Begin
	Begin
			if((Select Count(*) from @PerTable)=1)			
			select 'Quarter-'+Cast(datepart (q,Date) as varchar) as 'Quarter',Sum(cast (DocNo as numeric)) as 'TotalBills', sum(FinalAmt) as 'FinalAmt' ,sum(Disc) as 'Disc',sum(Charges) as 'Charges',sum(RndOff) as 'RndOff',sum(SAmt1) as 'SAmt1',sum(TAmt1) as 'TAmt1',sum(AmtTotal)as 'AmtTotal' ,sum(TaxToal) as 'TaxToal' from @TVal Group BY datepart (q,Date)
			if((Select Count(*) from @PerTable)=2)
			select 'Quarter-'+Cast(datepart (q,Date) as varchar) as 'Quarter',Sum(cast (DocNo as numeric)) as 'TotalBills', sum(FinalAmt) as 'FinalAmt' ,sum(Disc) as 'Disc',sum(Charges) as 'Charges',sum(RndOff) as 'RndOff',sum(SAmt1) as 'SAmt1',sum(TAmt1) as 'TAmt1',Sum(SAmt2) as 'SAmt2',sum(TAmt2) as 'TAmt2',sum(AmtTotal)as 'AmtTotal' ,sum(TaxToal) as 'TaxToal'  from @TVal Group BY datepart (q,Date)
			if((Select Count(*) from @PerTable)=3)
			select 'Quarter-'+Cast(datepart (q,Date) as varchar) as 'Quarter',Sum(cast (DocNo as numeric)) as 'TotalBills', sum(FinalAmt) as 'FinalAmt' ,sum(Disc) as 'Disc',sum(Charges) as 'Charges',sum(RndOff) as 'RndOff',sum(SAmt1) as 'SAmt1',sum(TAmt1) as 'TAmt1',Sum(SAmt2) as 'SAmt2',sum(TAmt2) as 'TAmt2' ,Sum(SAmt3) as 'SAmt3',sum(TAmt3) as 'TAmt3' ,sum(AmtTotal)as 'AmtTotal' ,sum(TaxToal) as 'TaxToal' from @TVal Group BY datepart (q,Date)
			if((Select Count(*) from @PerTable)=4)
			select 'Quarter-'+Cast(datepart (q,Date) as varchar) as 'Quarter',Sum(cast (DocNo as numeric)) as 'TotalBills', sum(FinalAmt) as 'FinalAmt' ,sum(Disc) as 'Disc',sum(Charges) as 'Charges',sum(RndOff) as 'RndOff',sum(SAmt1) as 'SAmt1',sum(TAmt1) as 'TAmt1',Sum(SAmt2) as 'SAmt2',sum(TAmt2) as 'TAmt2' ,Sum(SAmt3) as 'SAmt3',sum(TAmt3) as 'TAmt3',Sum(SAmt4) as 'SAmt4',sum(TAmt4) as 'TAmt4'  ,sum(AmtTotal)as 'AmtTotal' ,sum(TaxToal) as 'TaxToal' from @TVal Group BY datepart (q,Date)
			if((Select Count(*) from @PerTable)=5)
			select 'Quarter-'+Cast(datepart (q,Date) as varchar) as 'Quarter',Sum(cast (DocNo as numeric)) as 'TotalBills', sum(FinalAmt) as 'FinalAmt' ,sum(Disc) as 'Disc',sum(Charges) as 'Charges',sum(RndOff) as 'RndOff',sum(SAmt1) as 'SAmt1',sum(TAmt1) as 'TAmt1',Sum(SAmt2) as 'SAmt2',sum(TAmt2) as 'TAmt2' ,Sum(SAmt3) as 'SAmt3',sum(TAmt3) as 'TAmt3',Sum(SAmt4) as 'SAmt4',sum(TAmt4) as 'TAmt4',Sum(SAmt5) as 'SAmt5',sum(TAmt5) as 'TAmt5' ,sum(AmtTotal)as 'AmtTotal' ,sum(TaxToal) as 'TaxToal'  from @TVal  Group BY datepart (q,Date)
	End
End
---------------------------------------------------------------------------------------------------------------------------------------------------------
Else
Begin
	if(@Type=1 or @Type=2 )
	Begin
		
		if((Select Count(*) from @PerTable)=1)
		select Date,DocNo ,LedgerName,GSTNO, FinalAmt ,Disc ,Charges ,RndOff,SAmt1 ,TAmt1,TCAmt1,TIAmt1,TCeAmt1,AmtTotal ,TaxToal from @TVal
		union All select null as Date, null as DocNo ,null,null, sum(FinalAmt) ,sum(Disc) ,sum(Charges) ,sum(RndOff),sum(SAmt1) ,sum(TAmt1),sum(TCAmt1),sum(TIAmt1),sum(TCeAmt1),sum(AmtTotal) ,sum(TaxToal) from @TVal
		if((Select Count(*) from @PerTable)=2)
		select Date,DocNo  ,LedgerName,GSTNO, FinalAmt ,Disc ,Charges ,RndOff,SAmt1 ,TAmt1,TCAmt1,TIAmt1,TCeAmt1,SAmt2 ,TAmt2,TCAmt2,TIAmt2 ,TCeAmt2 ,AmtTotal,TaxToal  from @TVal
		union All select null as Date, null as DocNo ,null,null, sum(FinalAmt) ,sum(Disc) ,sum(Charges) ,sum(RndOff),sum(SAmt1) ,sum(TAmt1),sum(TCAmt1),sum(TIAmt1),sum(TCeAmt1),sum(SAmt2) ,sum(TAmt2),sum(TCAmt2),sum(TIAmt2),sum(TCeAmt2),sum(AmtTotal) ,sum(TaxToal) from @TVal
		if((Select Count(*) from @PerTable)=3)
		select Date,DocNo  ,LedgerName,GSTNO, FinalAmt ,Disc ,Charges ,RndOff,SAmt1 as AMT ,TAmt1 as SGST,TCAmt1 as CGST,TIAmt1 as IGST,TCeAmt1 as Cess,SAmt2 as AMT ,TAmt2 as SGST,TCAmt2 as CGST,TIAmt2 as IGST ,TCeAmt2 as Cess  ,SAmt3 as AMT ,TAmt3 as SGST ,TCAmt3 as CGST,TIAmt3 as IGST,TCeAmt3 as Cess,AmtTotal,TaxToal  from @TVal
		union All select null as Date, null as DocNo ,null, null,sum(FinalAmt) ,sum(Disc) ,sum(Charges) ,sum(RndOff),sum(SAmt1) as AMT ,sum(TAmt1) as SGST,sum(TCAmt1) as CGST,sum(TIAmt1) as IGST,sum(TCeAmt1) as Cess,sum(SAmt2) ,sum(TAmt2),sum(TCAmt2),sum(TIAmt2),sum(TCeAmt2),sum(SAmt3) ,sum(TAmt3),sum(TCAmt3),sum(TIAmt3),sum(TCeAmt3),sum(AmtTotal) ,sum(TaxToal) from @TVal
		if((Select Count(*) from @PerTable)=4)
		--select Date,DocNo  , LedgerName,FinalAmt ,Disc ,Charges ,RndOff,SAmt1 ,TAmt1,SAmt2 ,TAmt2 ,SAmt3 ,TAmt3 ,SAmt4 ,TAmt4  ,AmtTotal,TaxToal from @TVal
		--union All select null as Date, null as DocNo ,null, sum(FinalAmt) ,sum(Disc) ,sum(Charges) ,sum(RndOff),sum(SAmt1) ,sum(TAmt1),sum(SAmt2) ,sum(TAmt2),sum(SAmt3) ,sum(TAmt3),sum(SAmt4) ,sum(TAmt4),sum(AmtTotal) ,sum(TaxToal) from @TVal
		select Date,DocNo  ,LedgerName,GSTNO, FinalAmt ,Disc ,Charges ,RndOff,SAmt1 ,TAmt1,TCAmt1,TIAmt1,TCeAmt1,SAmt2 ,TAmt2,TCAmt2,TIAmt2 ,TCeAmt2  ,SAmt3 ,TAmt3 ,TCAmt3,TIAmt3,TCeAmt3,SAmt4 ,TAmt4 ,TCAmt4,TIAmt4,TCeAmt4,AmtTotal,TaxToal  from @TVal
		union All select null as Date, null as DocNo ,null,null, sum(FinalAmt) ,sum(Disc) ,sum(Charges) ,sum(RndOff),sum(SAmt1) ,sum(TAmt1),sum(TCAmt1),sum(TIAmt1),sum(TCeAmt1),sum(SAmt2) ,sum(TAmt2),sum(TCAmt2),sum(TIAmt2),sum(TCeAmt2),sum(SAmt3) ,sum(TAmt3),sum(TCAmt3),sum(TIAmt3),sum(TCeAmt3),sum(SAmt4) ,sum(TAmt4),sum(TCAmt4),sum(TIAmt4),sum(TCeAmt4),sum(AmtTotal) ,sum(TaxToal) from @TVal
		if((Select Count(*) from @PerTable)=5)
		select Date,DocNo  ,LedgerName, GSTNO,FinalAmt ,Disc ,Charges ,RndOff,SAmt1 ,TAmt1,TCAmt1,TIAmt1,TCeAmt1,SAmt2 ,TAmt2,TCAmt2,TIAmt2 ,TCeAmt2  ,SAmt3 ,TAmt3 ,TCAmt3,TIAmt3,TCeAmt3,SAmt4 ,TAmt4 ,TCAmt4,TIAmt4,TCeAmt4,SAmt5 ,TAmt5 ,TCAmt5,TIAmt5,TCeAmt5,AmtTotal,TaxToal  from @TVal
		union All select null as Date, null as DocNo ,null, null,sum(FinalAmt) ,sum(Disc) ,sum(Charges) ,sum(RndOff),sum(SAmt1) ,sum(TAmt1),sum(TCAmt1),sum(TIAmt1),sum(TCeAmt1),sum(SAmt2) ,sum(TAmt2),sum(TCAmt2),sum(TIAmt2),sum(TCeAmt2),sum(SAmt3) ,sum(TAmt3),sum(TCAmt3),sum(TIAmt3),sum(TCeAmt3),sum(SAmt4) ,sum(TAmt4),sum(TCAmt4),sum(TIAmt4),sum(TCeAmt4),sum(SAmt5) ,sum(TAmt5),sum(TCAmt5),sum(TIAmt5),sum(TCeAmt5),sum(AmtTotal) ,sum(TaxToal) from @TVal
	End

--	if(@Type=2)
--	Begin
--		
--		if((Select Count(*) from @PerTable)=1)
--		select Date,DocNo ,LedgerName,GSTNO, FinalAmt ,Disc ,Charges ,RndOff,SAmt1 ,TAmt1,TCAmt1,TIAmt1,TCeAmt1,AmtTotal ,TaxToal from @TVal
--		union All select null as Date, null as DocNo ,null,null, sum(FinalAmt) ,sum(Disc) ,sum(Charges) ,sum(RndOff),sum(SAmt1) ,sum(TAmt1),sum(TCAmt1),sum(TIAmt1),sum(TCeAmt1),sum(AmtTotal) ,sum(TaxToal) from @TVal
--		if((Select Count(*) from @PerTable)=2)
--		select Date,DocNo  ,LedgerName,GSTNO, FinalAmt ,Disc ,Charges ,RndOff,SAmt1 ,TAmt1,TCAmt1,TIAmt1,TCeAmt1,SAmt2 ,TAmt2,TCAmt2,TIAmt2 ,TCeAmt2 ,AmtTotal,TaxToal  from @TVal
--		union All select null as Date, null as DocNo ,null,null, sum(FinalAmt) ,sum(Disc) ,sum(Charges) ,sum(RndOff),sum(SAmt1) ,sum(TAmt1),sum(TCAmt1),sum(TIAmt1),sum(TCeAmt1),sum(SAmt2) ,sum(TAmt2),sum(TCAmt2),sum(TIAmt2),sum(TCeAmt2),sum(AmtTotal) ,sum(TaxToal) from @TVal
--		if((Select Count(*) from @PerTable)=3)
--		select Date,DocNo  ,LedgerName,GSTNO, FinalAmt ,Disc ,Charges ,RndOff,SAmt1,TAmt1,TCAmt1,TIAmt1,TCeAmt1,SAmt2 ,TAmt2,TCAmt2,TIAmt2 ,TCeAmt2  ,SAmt3 ,TAmt3 ,TCAmt3,TIAmt3,TCeAmt3,AmtTotal,TaxToal  from @TVal
--		union All select null as Date, null as DocNo ,null, null,sum(FinalAmt) ,sum(Disc) ,sum(Charges) ,sum(RndOff),sum(SAmt1) ,sum(TAmt1),sum(TCAmt1),sum(TIAmt1),sum(TCeAmt1),sum(SAmt2) ,sum(TAmt2),sum(TCAmt2),sum(TIAmt2),sum(TCeAmt2),sum(SAmt3) ,sum(TAmt3),sum(TCAmt3),sum(TIAmt3),sum(TCeAmt3),sum(AmtTotal) ,sum(TaxToal) from @TVal
--		if((Select Count(*) from @PerTable)=4)
--		--select Date,DocNo  , LedgerName,FinalAmt ,Disc ,Charges ,RndOff,SAmt1 ,TAmt1,SAmt2 ,TAmt2 ,SAmt3 ,TAmt3 ,SAmt4 ,TAmt4  ,AmtTotal,TaxToal from @TVal
--		--union All select null as Date, null as DocNo ,null, sum(FinalAmt) ,sum(Disc) ,sum(Charges) ,sum(RndOff),sum(SAmt1) ,sum(TAmt1),sum(SAmt2) ,sum(TAmt2),sum(SAmt3) ,sum(TAmt3),sum(SAmt4) ,sum(TAmt4),sum(AmtTotal) ,sum(TaxToal) from @TVal
--		select Date,DocNo  ,LedgerName,GSTNO, FinalAmt ,Disc ,Charges ,RndOff,HSNCode,StateName,SAmt1 ,TAmt1,TCAmt1,TIAmt1,TCeAmt1,SAmt2 ,TAmt2,TCAmt2,TIAmt2 ,TCeAmt2  ,SAmt3 ,TAmt3 ,TCAmt3,TIAmt3,TCeAmt3,SAmt4 ,TAmt4 ,TCAmt4,TIAmt4,TCeAmt4,AmtTotal,TaxToal  from @TVal
--		union All select null as Date, null as DocNo ,null,null, sum(FinalAmt) ,sum(Disc) ,sum(Charges) ,sum(RndOff),null,null,sum(SAmt1) ,sum(TAmt1),sum(TCAmt1),sum(TIAmt1),sum(TCeAmt1),sum(SAmt2) ,sum(TAmt2),sum(TCAmt2),sum(TIAmt2),sum(TCeAmt2),sum(SAmt3) ,sum(TAmt3),sum(TCAmt3),sum(TIAmt3),sum(TCeAmt3),sum(SAmt4) ,sum(TAmt4),sum(TCAmt4),sum(TIAmt4),sum(TCeAmt4),sum(AmtTotal) ,sum(TaxToal) from @TVal
--		if((Select Count(*) from @PerTable)=5)
--		select Date,DocNo  ,LedgerName, GSTNO,FinalAmt ,Disc ,Charges ,RndOff,HSnCode,StateName,SAmt1 ,TAmt1,TCAmt1,TIAmt1,TCeAmt1,SAmt2 ,TAmt2,TCAmt2,TIAmt2 ,TCeAmt2  ,SAmt3 ,TAmt3 ,TCAmt3,TIAmt3,TCeAmt3,SAmt4 ,TAmt4 ,TCAmt4,TIAmt4,TCeAmt4,SAmt5 ,TAmt5 ,TCAmt5,TIAmt5,TCeAmt5,AmtTotal,TaxToal  from @TVal
--		union All select null as Date, null as DocNo ,null, null,sum(FinalAmt) ,sum(Disc) ,sum(Charges) ,sum(RndOff),null,null,sum(SAmt1) ,sum(TAmt1),sum(TCAmt1),sum(TIAmt1),sum(TCeAmt1),sum(SAmt2) ,sum(TAmt2),sum(TCAmt2),sum(TIAmt2),sum(TCeAmt2),sum(SAmt3) ,sum(TAmt3),sum(TCAmt3),sum(TIAmt3),sum(TCeAmt3),sum(SAmt4) ,sum(TAmt4),sum(TCAmt4),sum(TIAmt4),sum(TCeAmt4),sum(SAmt5) ,sum(TAmt5),sum(TCAmt5),sum(TIAmt5),sum(TCeAmt5),sum(AmtTotal) ,sum(TaxToal) from @TVal
--	End
--	else
--	Begin
--		if((Select Count(*) from @PerTable)=1)
--		select Date,DocNo , FinalAmt ,Disc ,Charges ,RndOff,SAmt1 ,TAmt1,AmtTotal ,TaxToal from @TVal
--		union All select null as Date, null as DocNo , sum(FinalAmt) ,sum(Disc) ,sum(Charges) ,sum(RndOff),sum(SAmt1) ,sum(TAmt1),sum(AmtTotal) ,sum(TaxToal) from @TVal
--		if((Select Count(*) from @PerTable)=2)
--		select Date,DocNo  , FinalAmt ,Disc ,Charges ,RndOff,SAmt1 ,TAmt1,SAmt2 ,TAmt2 ,AmtTotal,TaxToal  from @TVal
--		union All select null as Date, null as DocNo , sum(FinalAmt) ,sum(Disc) ,sum(Charges) ,sum(RndOff),sum(SAmt1) ,sum(TAmt1),sum(SAmt2) ,sum(TAmt2),sum(AmtTotal) ,sum(TaxToal) from @TVal
--		if((Select Count(*) from @PerTable)=3)
--		select Date,DocNo  , FinalAmt ,Disc ,Charges ,RndOff,SAmt1 ,TAmt1,SAmt2 ,TAmt2 ,SAmt3 ,TAmt3 ,AmtTotal ,TaxToal from @TVal
--		union All select null as Date, null as DocNo , sum(FinalAmt) ,sum(Disc) ,sum(Charges) ,sum(RndOff),sum(SAmt1) ,sum(TAmt1),sum(SAmt2) ,sum(TAmt2),sum(SAmt3) ,sum(TAmt3),sum(AmtTotal) ,sum(TaxToal) from @TVal
--		if((Select Count(*) from @PerTable)=4)
--		select Date,DocNo  , FinalAmt ,Disc ,Charges ,RndOff,SAmt1 ,TAmt1,SAmt2 ,TAmt2 ,SAmt3 ,TAmt3 ,SAmt4 ,TAmt4  ,AmtTotal,TaxToal from @TVal
--		union All select null as Date, null as DocNo , sum(FinalAmt) ,sum(Disc) ,sum(Charges) ,sum(RndOff),sum(SAmt1) ,sum(TAmt1),sum(SAmt2) ,sum(TAmt2),sum(SAmt3) ,sum(TAmt3),sum(SAmt4) ,sum(TAmt4),sum(AmtTotal) ,sum(TaxToal) from @TVal
--		if((Select Count(*) from @PerTable)=5)
--		select Date,DocNo  , FinalAmt ,Disc ,Charges ,RndOff,SAmt1 ,TAmt1,SAmt2 ,TAmt2 ,SAmt3 ,TAmt3 ,SAmt4 ,TAmt4 ,SAmt5 ,TAmt5 ,AmtTotal,TaxToal  from @TVal
--		union All select null as Date, null as DocNo , sum(FinalAmt) ,sum(Disc) ,sum(Charges) ,sum(RndOff),sum(SAmt1) ,sum(TAmt1),sum(SAmt2) ,sum(TAmt2),sum(SAmt3) ,sum(TAmt3),sum(SAmt4) ,sum(TAmt4),sum(SAmt5) ,sum(TAmt5),sum(AmtTotal) ,sum(TaxToal) from @TVal
--	End


End


	
END

------------------------------------------------------------------------------------------------------------------------------------------------------------------

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
-- =============================================
alter PROCEDURE [dbo].[GetItemClosingStockByDate]
@MonthNo int,
@CompNo numeric(18),
@FrDate datetime,
@ToDate datetime,	
@ItemNo numeric(18)
AS
BEGIN
	SELECT  Distinct   CONVERT(varchar(11), TVoucherEntry.VoucherDate, 105) AS VoucherDate, TVoucherEntry.VoucherUserNo AS VoucherSrNo , MLedger.LedgerName AS Particulars, MVoucherType.VoucherTypeName, TVoucherEntry.PKVoucherNo, 
             case when  TStock.trncode=1 then sum(abs(TStock.BilledQuantity+TStock.FreeQty)) else 0 end as [Inward Quantity], case when  TStock.trncode=2 then sum(abs(TStock.BilledQuantity+TStock.FreeQty)) else 0 end AS [Outward Quantity]
    FROM        TVoucherEntry INNER JOIN
                      --TVoucherDetails ON TVoucherEntry.PKVoucherNo = TVoucherDetails.FKVoucherNo INNER JOIN
                      MLedger ON TVoucherEntry.LedgerNo = MLedger.LedgerNo INNER JOIN
                      MVoucherType ON TVoucherEntry.VoucherTypeCode = MVoucherType.VoucherTypeCode INNER JOIN
                      TStock ON TVoucherEntry.PkVoucherNo  = TStock.FkVoucherTrnNo
WHERE   --  (TVoucherDetails.VoucherSrNo = 1) AND 
(TStock.ItemNo = @ItemNo) and (month(TVoucherEntry.VoucherDate)= @MonthNo)
		And (TVoucherEntry.CompanyNo = @CompNo) And	(TVoucherEntry.VoucherDate >= @FrDate) And (TVoucherEntry.VoucherDate <= @ToDate)  and TVoucherEntry.IsCancel='false'
Group by TVoucherEntry.VoucherDate,TVoucherEntry.VoucherUserNo,MLedger.LedgerName,MVoucherType.VoucherTypeName, TVoucherEntry.PKVoucherNo,
TStock.trncode

Union
SELECT DISTINCT 
                      CONVERT(varchar(11), TVoucherEntry.VoucherDate, 105) AS VoucherDate, TVoucherEntry.VoucherUserNo AS VoucherSrNo,MVoucherType.VoucherTypeName AS Particulars, MVoucherType.VoucherTypeName, TVoucherEntry.PKVoucherNo, 
                      CASE WHEN TStock.trncode = 1 THEN sum(abs(TStock.BilledQuantity+TStock.FreeQty)) ELSE 0 END AS [Inward Quantity], 
                      CASE WHEN TStock.trncode = 2 THEN sum(abs(TStock.BilledQuantity+TStock.FreeQty)) ELSE 0 END AS [Outward Quantity]
FROM           TStock INNER JOIN
                      TVoucherEntry INNER JOIN
                      MVoucherType ON TVoucherEntry.VoucherTypeCode = MVoucherType.VoucherTypeCode ON TStock.FKVoucherNo = TVoucherEntry.PkVoucherNo
WHERE     (TStock.ItemNo = @ItemNo) and (month(TVoucherEntry.VoucherDate)= @MonthNo)
		And (TVoucherEntry.CompanyNo = @CompNo) And	(TVoucherEntry.VoucherDate >= @FrDate) And (TVoucherEntry.VoucherDate <= @ToDate) AND (TVoucherEntry.VoucherTypeCode in(23,24,8,32,19))  and TVoucherEntry.IsCancel='false'
	Group by TVoucherEntry.VoucherDate,TVoucherEntry.VoucherUserNo,TVoucherEntry.VoucherTypeCode,MVoucherType.VoucherTypeName, TVoucherEntry.PKVoucherNo,
TStock.trncode
	
END
GO
------------------------------------------------------------------------------------------------------------------------------------------------------------------

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[GetOpeningStock]
	(
	@CompNo		numeric(18),
	@FromDate	datetime,
	@ItemNo		numeric(18),
	@Type		int,
	@No			numeric(18)
	)
RETURNS @TStockBal Table(OpQty numeric(18,2),OpAmt numeric(18,2))
AS
	BEGIN
	Declare @OpAmt numeric(18,2),@OpQty numeric(18,2),@DrAmt numeric(18,2),@DrQty numeric(18,2)
Declare @CrAmt numeric(18,2),@CrQty numeric(18,2),@ClosingQty numeric(18,2),@CategoryNo numeric(18,2)
Declare @Rate numeric(18,2),@BalAmt numeric(18,2)
Declare @OpDt datetime,@TrnDr int,@TrnCr int
Declare @VType int,@VNo numeric(18)--@IsJobWork int,@JobWorkCo numeric(18),
Declare @DAmt numeric(18,2),@DQty Numeric(18,2),@CAmt numeric(18,2),@CQty Numeric(18,2)
Declare @Cnt int
set @VType=0 set @VNo=0--set @IsJobWork=0 set @JobWorkCo=0 
set @Cnt=0
	set @ClosingQty = 0 set @BalAmt = 0
set @DrQty=0 set @DrAmt=0 set @CrQty = 0 set @CrAmt = 0
set @TrnDr = 1 set @TrnCr = 2

if(@Type =0)--Stock Summary
begin
	
	select @OpDt=BooksBeginFrom from Mfirm
	set @OpDt=0
	

	set @OpAmt =0 set @OpQty=0
	if(@FromDate <= @OpDt)
	begin
	
	insert into @TStockBal values(@OpQty,@OpAmt)
	set @Cnt=1
	end
	else
	begin
		Declare CurDr Cursor For 	
		SELECT     isnull(TStock.Amount,0), isNull(abs(TStock.BilledQuantity),0)--isnull(abs(TStock.Quantity),0)+isnull(abs(TStock.FreeQty),0)
					,TVoucherEntry.VoucherTypeCode,TVoucherEntry.PKVoucherNo--@DrAmt=isnull(SUM(TStock.Amount),0) , @DrQty=isnull(SUM(TStock.Quantity) ,0)
		From TVoucherEntry INNER JOIN
                      TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo

		WHERE     (TStock.ItemNo = @ItemNo) AND (TStock.TrnCode = @TrnDr) AND 
			(TVoucherEntry.VoucherDate >= @OpDt) AND 
                      (TVoucherEntry.VoucherDate <= DateAdd(d,-1,@FromDate)) and TVoucherEntry.IsCancel='false'
                      
		Declare CurCr Cursor For
		SELECT     isnull(TStock.Amount,0),isNull(abs(TStock.BilledQuantity),0)-- isnull(TStock.Quantity,0)+isnull(TStock.FreeQty,0)
			,TVoucherEntry.VoucherTypeCode,TVoucherEntry.PKVoucherNo--@CrAmt=isnull(SUM(TStock.Amount),0) , @CrQty=isnull(SUM(TStock.Quantity),0) 
		FROM   TVoucherEntry INNER JOIN
                      TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo

		WHERE     (TStock.ItemNo = @ItemNo) AND (TStock.TrnCode = @TrnCr)  AND 
		(TVoucherEntry.VoucherDate >= @OpDt) AND 
        (TVoucherEntry.VoucherDate <= DateAdd(d,-1,@FromDate)) and TVoucherEntry.IsCancel='false'
            
     
	insert into @TStockBal values(@ClosingQty,@BalAmt)
	end
end
--===============================================================================================================
else if(@Type = 1)--Godownwise Stock
begin	
	select @OpDt=BooksBeginFrom from Mfirm

	set @OpAmt=0 set @OpQty=0
	if(@FromDate <= @OpDt)
	begin
	
	insert into @TStockBal values(@OpQty,@OpAmt)
	set @Cnt=1
	end
	else
	begin
		Declare CurDr Cursor For 	
		SELECT     isnull(TStock.Amount,0), isnull(abs(TStockGodown.Qty),0),TVoucherEntry.VoucherTypeCode,TVoucherEntry.PKVoucherNo-- @DrAmt=isnull(SUM(TStock.Amount),0) , @DrQty=isnull(SUM(TStock.Quantity) ,0)
		FROM  TVoucherEntry INNER JOIN
                      TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo

					  INNER JOIN TStockGodown ON TStock.PkStockTrnNo = TStockGodown.FKStockTrnNo
		WHERE     (TStock.ItemNo = @ItemNo) AND (TStock.TrnCode = @TrnDr)  AND 
			(TVoucherEntry.VoucherDate >= @OpDt) AND 
                        (TVoucherEntry.VoucherDate <= DateAdd(d,-1,@FromDate)) And TStockGodown.GodownNo=@No and TVoucherEntry.IsCancel='false'
 
		Declare CurCr Cursor For
		SELECT     isnull(TStock.Amount,0), isnull(TStockGodown.Qty,0),TVoucherEntry.VoucherTypeCode,TVoucherEntry.PKVoucherNo--     @CrAmt=isnull(SUM(TStock.Amount),0) , @CrQty=isnull(SUM(TStock.Quantity),0) 
		FROM        TVoucherEntry INNER JOIN
                      TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
 INNER JOIN
                      TStockGodown ON TStock.PkStockTrnNo = TStockGodown.FKStockTrnNo
		WHERE     (TStock.ItemNo = @ItemNo) AND (TStock.TrnCode = @TrnCr) AND 
			(TVoucherEntry.VoucherDate >= @OpDt) AND 
                        (TVoucherEntry.VoucherDate <= DateAdd(d,-1,@FromDate)) And TStockGodown.GodownNo=@No and TVoucherEntry.IsCancel='false'
            
     
	insert into @TStockBal values(@ClosingQty,@BalAmt)
	end
end	
--===============================================================================================================
else if(@Type = 3)--Categorywise Stock Details
begin
	--set @OpDt=@FromDate
	--Select @OpDt=UserDate From MBranch where PKBranchCode=@BrID	
	select @OpDt=BooksBeginFrom from Mfirm
--	SELECT     @OpAmt=isnull(SUM(OpAmount),0), @OpQty=isnull(SUM(OpQuantity) ,0)
--	FROM         MItemOpeningStock
--	WHERE     (PkItemNo = @ItemNo) And PkItemNo=(Select ItemNo from MStockItems WHERE(CategoryNo =@No))
	set @OpAmt=0 set @OpQty=0
	--And IsJobWork=@IsJobWork AND ForJobWorkCompNo=@JobWorkCo
	
	if(@FromDate <= @OpDt)
	begin
	
	insert into @TStockBal values(@OpQty,@OpAmt)
	set @Cnt=1
	end
	else
	begin
		
		Declare CurDr Cursor For 	
		SELECT     isnull(TStock.Amount,0),  isNull(abs(TStock.BilledQuantity),0)--isnull(abs(TStock.Quantity),0)+isnull(abs(TStock.FreeQty),0)
			,TVoucherEntry.VoucherTypeCode,TVoucherEntry.PKVoucherNo--     @DrAmt=isnull(SUM(TStock.Amount),0) , @DrQty=isnull(SUM(TStock.Quantity) ,0)
		FROM     TVoucherEntry INNER JOIN
                      TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
INNER JOIN
                      MStockItems_V(1,NULL,NULL,NULL,NULL,NULL,NULL) as MStockItems ON TStock.ItemNo = MStockItems.ItemNo
		WHERE     (TStock.ItemNo = @ItemNo) AND (TStock.TrnCode = @TrnDr) AND 
			(TVoucherEntry.VoucherDate >= @OpDt) AND 
                      (TVoucherEntry.VoucherDate <= DateAdd(d,-1,@FromDate)) And MStockItems.FKCategoryNo=@No  and TVoucherEntry.IsCancel='false'
 
		Declare CurCr Cursor For
		SELECT     isnull(TStock.Amount,0),  isNull(abs(TStock.BilledQuantity),0)--isnull(TStock.Quantity,0)+isnull(TStock.FreeQty,0)
			,TVoucherEntry.VoucherTypeCode,TVoucherEntry.PKVoucherNo--     @CrAmt=isnull(SUM(TStock.Amount),0) , @CrQty=isnull(SUM(TStock.Quantity),0) 
		FROM      TVoucherEntry INNER JOIN
                      TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
 INNER JOIN
                      MStockItems_V(1,NULL,NULL,NULL,NULL,NULL,NULL) as MStockItems ON TStock.ItemNo = MStockItems.ItemNo
		WHERE     (TStock.ItemNo = @ItemNo) AND (TStock.TrnCode = @TrnCr) AND 
			(TVoucherEntry.VoucherDate >= @OpDt) AND 
                      (TVoucherEntry.VoucherDate <= DateAdd(d,-1,@FromDate)) And MStockItems.FKCategoryNo=@No and TVoucherEntry.IsCancel='false'
            
    
	end
end	
--===============================================================================================================
else if(@Type = 5)--Groupwise wise Stock Details
begin
	--set @OpDt=@FromDate
	--Select @OpDt=UserDate From MBranch where PKBranchCode=@BrID	
	select @OpDt=BooksBeginFrom from Mfirm
--	SELECT     @OpAmt=isnull(SUM(OpAmount),0), @OpQty=isnull(SUM(OpQuantity) ,0)
--	FROM         MItemOpeningStock
--	WHERE     (PkItemNo = @ItemNo) 
	--And IsJobWork=@IsJobWork AND ForJobWorkCompNo=@JobWorkCo
	set @OpAmt=0 set @OpQty=0
	if(@FromDate <= @OpDt)
	begin
	
	insert into @TStockBal values(@OpQty,@OpAmt)
	set @Cnt=1
	end
	else
	begin
		
		Declare CurDr Cursor For 	
		SELECT     isnull(TStock.Amount,0),  isNull(abs(TStock.BilledQuantity),0)--isnull(abs(TStock.Quantity),0)+isnull(abs(TStock.FreeQty),0)
					,TVoucherEntry.VoucherTypeCode,TVoucherEntry.PKVoucherNo--     @DrAmt=isnull(SUM(TStock.Amount),0) , @DrQty=isnull(SUM(TStock.Quantity) ,0)
		FROM      TVoucherEntry INNER JOIN
                      TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
 INNER JOIN
                      MStockItems_V(1,NULL,NULL,NULL,NULL,NULL,NULL) as MStockItems ON TStock.ItemNo = MStockItems.ItemNo
		WHERE     (TStock.ItemNo = @ItemNo) AND (TStock.TrnCode = @TrnDr) AND 
			(TVoucherEntry.VoucherDate >= @OpDt) AND 
                      (TVoucherEntry.VoucherDate <= DateAdd(d,-1,@FromDate)) And MStockItems.GroupNo=@No and TVoucherEntry.IsCancel='false'
 
		Declare CurCr Cursor For
		SELECT     isnull(TStock.Amount,0),  isNull(abs(TStock.BilledQuantity),0)--isnull(TStock.Quantity,0)+isnull(TStock.FreeQty,0)
			,TVoucherEntry.VoucherTypeCode,TVoucherEntry.PKVoucherNo--     @CrAmt=isnull(SUM(TStock.Amount),0) , @CrQty=isnull(SUM(TStock.Quantity),0) 
		FROM    TVoucherEntry INNER JOIN
                      TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
 INNER JOIN
                      MStockItems_V(1,NULL,NULL,NULL,NULL,NULL,NULL) as MStockItems ON TStock.ItemNo = MStockItems.ItemNo
		WHERE     (TStock.ItemNo = @ItemNo) AND (TStock.TrnCode = @TrnCr) AND 
			(TVoucherEntry.VoucherDate >= @OpDt) AND 
                      (TVoucherEntry.VoucherDate <= DateAdd(d,-1,@FromDate)) And MStockItems.GroupNo=@No and TVoucherEntry.IsCancel='false'
            
     
	end
end	
--===============================================================================================================
else if(@Type = 6)--Ledger wise Stock Details
begin
	--set @OpDt=@FromDate
	--Select @OpDt=UserDate From MBranch where PKBranchCode=@BrID	
	select @OpDt=BooksBeginFrom from Mfirm
	/*SELECT     @OpAmt=isnull(SUM(OpAmount),0), @OpQty=isnull(SUM(OpQuantity) ,0)
	FROM         MItemOpeningStock
	WHERE     (PkItemNo = @ItemNo) AND (BranchCode = @BrID) */
	set @OpAmt = 0 Set @OpQty = 0
	if(@FromDate <= @OpDt)
	begin
	
	insert into @TStockBal values(@OpQty,@OpAmt)
	set @Cnt=1
	end
	else
	begin
		
		Declare CurDr Cursor For 	
		SELECT     isnull(TStock.Amount,0), isNull(abs(TStock.BilledQuantity),0)-- isnull(abs(TStock.Quantity),0)+isnull(abs(TStock.FreeQty),0)
					,TVoucherEntry.VoucherTypeCode,TVoucherEntry.PKVoucherNo--     @DrAmt=isnull(SUM(TStock.Amount),0) , @DrQty=isnull(SUM(TStock.Quantity) ,0)
		FROM         TVoucherEntry INNER JOIN
                      TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN
                      TStock ON TVoucherDetails.PkVoucherTrnNo = TStock.FkVoucherTrnNo 
							WHERE (TVoucherEntry.VoucherDate >= @OpDt) AND (TVoucherEntry.VoucherDate <= DateAdd(d,-1,@FromDate)) 
							AND (TVoucherDetails.LedgerNo = @No) AND (TVoucherDetails.VoucherSrNo = 1)
						--	And (TVoucherEntry.CompanyNo =@CompNo) 
							AND (TStock.TrnCode = @TrnDr) And  (TStock.ItemNo = @ItemNo) and TVoucherEntry.IsCancel='false'
 
		Declare CurCr Cursor For
		SELECT     isnull(TStock.Amount,0),  isNull(abs(TStock.BilledQuantity),0)--isnull(TStock.Quantity,0)+isnull(TStock.FreeQty,0)
				,TVoucherEntry.VoucherTypeCode,TVoucherEntry.PKVoucherNo--     @CrAmt=isnull(SUM(TStock.Amount),0) , @CrQty=isnull(SUM(TStock.Quantity),0) 
		FROM       TVoucherEntry INNER JOIN
                      TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN
                      TStock ON TVoucherDetails.PkVoucherTrnNo = TStock.FkVoucherTrnNo 
							WHERE (TVoucherEntry.VoucherDate >= @OpDt) AND (TVoucherEntry.VoucherDate <= DateAdd(d,-1,@FromDate)) 
							AND (TVoucherDetails.LedgerNo = @No) AND (TVoucherDetails.VoucherSrNo = 1)
						--	And (TVoucherEntry.CompanyNo =@CompNo) 
							AND (TStock.TrnCode = @TrnCr) And  (TStock.ItemNo = @ItemNo) and TVoucherEntry.IsCancel='false'
      
	end
--===============================================================================================================
end	

if(@Cnt=0)
begin
--Open Debit Cursor
Open CurDr
	Fetch Next From CurDr Into @DAmt,@DQty,@VType,@VNo
	While (@@Fetch_status=0)
	begin
		set @DrAmt=@DrAmt+@DAmt
		set @DrQty=@DrQty+@DQty
		Fetch Next From CurDr Into @DAmt,@DQty,@VType,@VNo
	end
	Close CurDr Deallocate CurDr

set @VType=0 set @VNo=0	
--Open Credit Cursor
Open CurCr
	Fetch Next From CurCr Into @CAmt,@CQty,@VType,@VNo
	While (@@Fetch_status=0)
	begin
		set @CrAmt=@CrAmt+@CAmt
		set @CrQty=@CrQty+@CQty
		Fetch Next From CurCr Into @CAmt,@CQty,@VType,@VNo
	end
	Close CurCr Deallocate CurCr
	
set @ClosingQty=(@DrQty-@CrQty)+@OpQty
	                   
	set @BalAmt = (@DrAmt - @CrAmt)+ @OpAmt  
	insert into @TStockBal values(@ClosingQty,@BalAmt)
end
	RETURN
	END



GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetPayTypeDetails]
@FromDate			 datetime,
@ToDate				 datetime,
@VchType			 numeric(18),
@RType				numeric(18),
@CompanyNo			numeric(18)
AS
Declare @Details TABLE (PayTypeNo numeric(18,0),PayTypeName Varchar(500),PayTypeActName varchar(200),PayTypeShortName varchar(20),Amount numeric(18,2),NoOfBills numeric(18),typeno numeric(18))
Declare @PayTypeNo numeric(18,0),@PayTypeName Varchar(500),@Amount numeric(18,2),@NoOfBills numeric(18),
		@PayTypeActName varchar(200),@ShortName varchar(20),@MixAmt numeric(18,2),@TotAmt numeric(18,2),
		@TAmount numeric(18,2),@MAmount numeric(18,2),@VchStr numeric(18)
set @VchStr=0

if(@VchType=15)
begin
set @VchStr=12
Declare CurDtls Cursor  for SELECT PKPayTypeNo, PayTypeName +' ('+ShortName+')',PayTypeName,ShortName
							FROM MPayType 
							WHERE  PKPayTypeNo<>1
							Union 
							SELECT 12000,'Mix Mode(MX)','Mix Mode','MX'

end
else if(@VchType=115)
begin
set @VchStr=112
Declare CurDtls Cursor  for SELECT PKPayTypeNo, PayTypeName +' ('+ShortName+')',PayTypeName,ShortName
							FROM MPayType 
							WHERE  PKPayTypeNo<>1
							Union 
							SELECT 12000,'Mix Mode(MX)','Mix Mode','MX'

end
else if(@VchType=9)
begin
set @VchStr=13
Declare CurDtls Cursor  for SELECT PKPayTypeNo, PayTypeName +' ('+ShortName+')',PayTypeName,ShortName
							FROM MPayType 
							WHERE  PKPayTypeNo<>1
end
else if(@VchType=109)
begin
set @VchStr=113
Declare CurDtls Cursor  for SELECT PKPayTypeNo, PayTypeName +' ('+ShortName+')',PayTypeName,ShortName
							FROM MPayType 
							WHERE  PKPayTypeNo<>1
end
else if (@VchType=12 or @VchType=13 or @VchType=113 or @VchType=112)
Begin
set @VchStr=@VchType 
Declare CurDtls Cursor  for SELECT PKPayTypeNo, PayTypeName +' ('+ShortName+')',PayTypeName,ShortName
							FROM MPayType 
							WHERE  PKPayTypeNo NOT IN (1,4,5,6,7)
end
	Open CurDtls	
	Fetch Next From CurDtls into @PayTypeNo ,@PayTypeName,@PayTypeActName,@ShortName

	while(@@Fetch_Status = 0)
	begin
	if(@RType=0)
	Begin
		if(@PayTypeNo <>12000)
		Begin
		Select @TAmount=ISNull(SUM(case when TVoucherEntry.voucherTypecode in (15,9) then isNull((Debit+Credit),0) else isNull((Debit+Credit),0)*-1 end), 0),@NoOfBills=Count(*) FROM TVoucherEntry INNER JOIN
					   TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN
                       MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo
                       WHERE (TVoucherDetails.SrNo=501) AND (TVoucherEntry.PayTypeNo = @PayTypeNo) 
					   AND TVoucherEntry.VoucherDate>=@FromDate and  TVoucherEntry.VoucherDate<=@ToDate 
					   AND TVoucherEntry.VoucherTypeCode in(@VchType,@VchStr) and TVoucherEntry.CompanyNo=@CompanyNo
					   AND TVoucherEntry.IsCancel='false' AND MIXMode=0
--		SELECT @MAmount= IsNull(SUM(TVoucherRefDetails.Amount),0) FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN
--						TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo
--						WHERE  (TVoucherEntry.VoucherTypeCode = 30) AND (TVoucherEntry.CompanyNo = @CompanyNo) AND 
--						(TVoucherRefDetails.RefNo IN (SELECT RefNo FROM TVoucherRefDetails AS TVoucherRefDetails_1 WHERE (FkVoucherTrnNo IN (SELECT PkVoucherTrnNo FROM TVoucherDetails AS TVoucherDetails_1
--						WHERE (FkVoucherNo IN  (SELECT PkVoucherNo FROM TVoucherEntry AS TVoucherEntry_1 WHERE (PayTypeNo = 3) AND (VoucherDate >= @FromDate) AND (VoucherDate <= @ToDate) 
--						AND (VoucherTypeCode = @VchType) AND  TVoucherEntry.IsCancel = 'false'  AND (CompanyNo = @CompanyNo) AND (MixMode = 1))) AND (VoucherSrNo = 1))))) and MPayType.PKPayTypeNo=@PayTypeNo

				SELECT    @MAmount=IsNull(SUM(TVoucherRefDetails.Amount),0) 
				FROM         TVoucherEntry INNER JOIN
									  TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN
									  TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo INNER JOIN
									  MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo INNER JOIN
									  TVoucherPayTypeDetails ON TVoucherEntry.PkVoucherNo = TVoucherPayTypeDetails.FKReceiptVoucherNo
				WHERE      (TVoucherEntry.VoucherTypeCode = case when @VchType=15 then 30 else 31 end) and (TVoucherEntry .CompanyNo = @CompanyNo) and MPayType.PKPayTypeNo=@PayTypeNo 
				AND (VoucherDate >= @FromDate) AND (VoucherDate <= @ToDate) AND  TVoucherEntry.IsCancel = 'false'
						
		set @Amount=@TAmount+@MAmount
		end
		else 
		Select @Amount=ISNull(SUM(Debit+Credit), 0),@NoOfBills=Count(*) FROM TVoucherEntry INNER JOIN
					   TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN
                       MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo
                       WHERE (TVoucherDetails.SrNo=501) AND (TVoucherEntry.PayTypeNo = 3) 
					   AND TVoucherEntry.VoucherDate>=@FromDate and  TVoucherEntry.VoucherDate<=@ToDate 
					   AND TVoucherEntry.VoucherTypeCode=@VchType and TVoucherEntry.CompanyNo=@CompanyNo
					   AND TVoucherEntry.IsCancel='false' AND MIXMode=1
	End
	if(@RType=1)		
	BEgin
		if(@PayTypeNo <>12000)
		Begin
		Select @TAmount=ISNull(SUM(case when TVoucherEntry.voucherTypecode in (15,9) then isNull((Debit+Credit),0) else isNull((Debit+Credit),0)*-1 end), 0),@NoOfBills=Count(*) FROM TVoucherEntry INNER JOIN
                       TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN
                       MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo
                       WHERE (TVoucherDetails.SrNo=501) AND (TVoucherEntry.PayTypeNo = @PayTypeNo) 
					   AND TVoucherEntry.VoucherDate>=@FromDate and  TVoucherEntry.VoucherDate<=@ToDate 
					   AND TVoucherEntry.VoucherTypeCode in(@VchType,@VchStr) and TVoucherEntry.CompanyNo=@CompanyNo
					   AND TVoucherEntry.IsCancel='true' AND MIXMode=0

--		SELECT  @MAmount=IsNull(SUM(TVoucherRefDetails.Amount),0) FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN
--						TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo
--						WHERE  (TVoucherEntry.VoucherTypeCode = 30) AND (TVoucherEntry.CompanyNo = @CompanyNo) AND 
--						(TVoucherRefDetails.RefNo IN (SELECT RefNo FROM TVoucherRefDetails AS TVoucherRefDetails_1 WHERE (FkVoucherTrnNo IN (SELECT PkVoucherTrnNo FROM TVoucherDetails AS TVoucherDetails_1
--						WHERE (FkVoucherNo IN  (SELECT PkVoucherNo FROM TVoucherEntry AS TVoucherEntry_1 WHERE (PayTypeNo = 3) AND (VoucherDate >= @FromDate) AND (VoucherDate <= @ToDate) 
--						AND (VoucherTypeCode = @VchType) AND  TVoucherEntry.IsCancel = 'true'  AND (CompanyNo = @CompanyNo) AND (MixMode = 1))) AND (VoucherSrNo = 1))))) and MPayType.PKPayTypeNo=@PayTypeNo
SELECT    @MAmount=IsNull(SUM(TVoucherRefDetails.Amount),0) 
				FROM         TVoucherEntry INNER JOIN
									  TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN
									  TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo INNER JOIN
									  MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo INNER JOIN
									  TVoucherPayTypeDetails ON TVoucherEntry.PkVoucherNo = TVoucherPayTypeDetails.FKReceiptVoucherNo
				WHERE      (TVoucherEntry.VoucherTypeCode = 30) and (TVoucherEntry .CompanyNo = @CompanyNo) and MPayType.PKPayTypeNo=@PayTypeNo 
				AND (VoucherDate >= @FromDate) AND (VoucherDate <= @ToDate) AND  TVoucherEntry.IsCancel = 'true'
		set @Amount=@TAmount+@MAmount
		end
		else 
		Select @Amount=ISNull(SUM(Debit+Credit), 0),@NoOfBills=Count(*) FROM TVoucherEntry INNER JOIN
                       TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN
                       MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo
                       WHERE (TVoucherDetails.SrNo=501) AND (TVoucherEntry.PayTypeNo = 3) 
					   AND TVoucherEntry.VoucherDate>=@FromDate and  TVoucherEntry.VoucherDate<=@ToDate 
					   AND TVoucherEntry.VoucherTypeCode=@VchType and TVoucherEntry.CompanyNo=@CompanyNo
					   AND TVoucherEntry.IsCancel='true' AND MIXMode=1
	End
	if(@RType=2)
	Begin
		if(@PayTypeNo <>12000)
		Begin
		Select @TAmount=ISNull(SUM(case when TVoucherEntry.voucherTypecode in (15,9) then isNull((Debit+Credit),0) else isNull((Debit+Credit),0)*-1 end), 0),@NoOfBills=Count(*) FROM TVoucherEntry INNER JOIN
                       TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN
                       MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo
                       WHERE(TVoucherDetails.SrNo=501) AND (TVoucherEntry.PayTypeNo = @PayTypeNo) 
					   AND TVoucherEntry.VoucherDate>=@FromDate and  TVoucherEntry.VoucherDate<=@ToDate 
					   AND TVoucherEntry.VoucherTypeCode in(@VchType,@VchStr) and TVoucherEntry.CompanyNo=@CompanyNo  --AND MIXMode=0
		
--		SELECT  @MAmount= isNull(SUM(TVoucherRefDetails.Amount),0) FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN
--						TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo
--						WHERE  (TVoucherEntry.VoucherTypeCode = 30) AND (TVoucherEntry.CompanyNo = @CompanyNo) AND 
--						(TVoucherRefDetails.RefNo IN (SELECT RefNo FROM TVoucherRefDetails AS TVoucherRefDetails_1 WHERE (FkVoucherTrnNo IN (SELECT PkVoucherTrnNo FROM TVoucherDetails AS TVoucherDetails_1
--						WHERE (FkVoucherNo IN  (SELECT PkVoucherNo FROM TVoucherEntry AS TVoucherEntry_1 WHERE (PayTypeNo = 3) AND (VoucherDate >= @FromDate) AND (VoucherDate <= @ToDate) 
--						AND (VoucherTypeCode = 15)  AND (CompanyNo = @CompanyNo) AND (MixMode = 1))) AND (VoucherSrNo = 1))))) and MPayType.PKPayTypeNo=@PayTypeNo

SELECT    @MAmount=IsNull(SUM(TVoucherRefDetails.Amount),0) 
				FROM         TVoucherEntry INNER JOIN
									  TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN
									  TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo INNER JOIN
									  MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo INNER JOIN
									  TVoucherPayTypeDetails ON TVoucherEntry.PkVoucherNo = TVoucherPayTypeDetails.FKReceiptVoucherNo
				WHERE      (TVoucherEntry.VoucherTypeCode = 30) and (TVoucherEntry .CompanyNo = @CompanyNo) and MPayType.PKPayTypeNo=@PayTypeNo 
				AND (VoucherDate >= @FromDate) AND (VoucherDate <= @ToDate) 
		set @Amount=@TAmount+@MAmount
		end
		else
		Select @Amount=ISNull(SUM(Debit+Credit), 0),@NoOfBills=Count(*) FROM TVoucherEntry INNER JOIN
                       TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN
                       MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo
                       WHERE(TVoucherDetails.SrNo=501) AND (TVoucherEntry.PayTypeNo = 3) 
					   AND TVoucherEntry.VoucherDate>=@FromDate and  TVoucherEntry.VoucherDate<=@ToDate 
					   AND TVoucherEntry.VoucherTypeCode=@VchType and TVoucherEntry.CompanyNo=@CompanyNo -- AND MIXMode=1
	End			  
	insert into @Details values(@PayTypeNo ,@PayTypeName,@PayTypeActName,@ShortName,@Amount,@NoOfBills,1)
	if(@PayTypeNo=12000)
	Begin
			set @MixAmt=@Amount
			set @TotAmt=0
		if(@RType=0)
--		Declare CurMix Cursor For SELECT  MPayType.PKPayTypeNo,MPayType.PayTypeName AS Name,MPayType.DisplayName,MPayType.ShortName, SUM(TVoucherRefDetails.Amount) AS Amount FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN
--						TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo
--						WHERE  (TVoucherEntry.VoucherTypeCode = 30) AND (TVoucherEntry.CompanyNo = @CompanyNo) AND 
--						(TVoucherRefDetails.RefNo IN (SELECT RefNo FROM TVoucherRefDetails AS TVoucherRefDetails_1 WHERE (FkVoucherTrnNo IN (SELECT PkVoucherTrnNo FROM TVoucherDetails AS TVoucherDetails_1
--						WHERE (FkVoucherNo IN  (SELECT PkVoucherNo FROM TVoucherEntry AS TVoucherEntry_1 WHERE (PayTypeNo = 3) AND (VoucherDate >= @FromDate) AND (VoucherDate <= @ToDate) 
--						AND (VoucherTypeCode = 15) AND  TVoucherEntry.IsCancel = 'false'  AND (CompanyNo = @CompanyNo) AND (MixMode = 1))) AND (VoucherSrNo = 1)))))
--						GROUP BY MPayType.PKPayTypeNo, MPayType.PayTypeName,MPayType.DisplayName,MPayType.ShortName
Declare CurMix Cursor for SELECT MPayType.PKPayTypeNo,MPayType.PayTypeName AS Name,MPayType.DisplayName,MPayType.ShortName, SUM(TVoucherRefDetails.Amount) AS Amount FROM TVoucherEntry INNER JOIN  TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo INNER JOIN  MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo INNER JOIN  TVoucherPayTypeDetails ON TVoucherEntry.PkVoucherNo = TVoucherPayTypeDetails.FKReceiptVoucherNo WHERE  (TVoucherEntry.VoucherTypeCode = 30) AND (TVoucherRefDetails.RefNo in (SELECT RefNo FROM TVoucherRefDetails AS TVoucherRefDetails_1 WHERE (FkVoucherTrnNo IN (SELECT PkVoucherTrnNo FROM TVoucherDetails AS TVoucherDetails_1
						WHERE (FkVoucherNo IN  (SELECT PkVoucherNo FROM TVoucherEntry AS TVoucherEntry_1 WHERE (PayTypeNo = 3) AND (VoucherDate >= @FromDate) AND (VoucherDate <= @ToDate) 
						AND (VoucherTypeCode = @VchType) AND  TVoucherEntry.IsCancel = 'false'  AND (CompanyNo = @CompanyNo) AND (MixMode = 1))) AND (VoucherSrNo = 1))))) AND (TVoucherEntry.CompanyNo = @CompanyNo)  
GROUP BY MPayType.PKPayTypeNo, MPayType.PayTypeName,MPayType.DisplayName,MPayType.ShortName
	if(@RType=1)
--		Declare CurMix Cursor For SELECT  MPayType.PKPayTypeNo,MPayType.PayTypeName AS Name,MPayType.DisplayName,MPayType.ShortName, SUM(TVoucherRefDetails.Amount) AS Amount FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN
--						TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo
--						WHERE  (TVoucherEntry.VoucherTypeCode = 30) AND (TVoucherEntry.CompanyNo = @CompanyNo) AND 
--						(TVoucherRefDetails.RefNo IN (SELECT RefNo FROM TVoucherRefDetails AS TVoucherRefDetails_1 WHERE (FkVoucherTrnNo IN (SELECT PkVoucherTrnNo FROM TVoucherDetails AS TVoucherDetails_1
--						WHERE (FkVoucherNo IN  (SELECT PkVoucherNo FROM TVoucherEntry AS TVoucherEntry_1 WHERE (PayTypeNo = 3) AND (VoucherDate >= @FromDate) AND (VoucherDate <= @ToDate) 
--						AND (VoucherTypeCode = 15) AND  TVoucherEntry.IsCancel = 'true'  AND (CompanyNo = @CompanyNo) AND (MixMode = 1))) AND (VoucherSrNo = 1)))))
--						GROUP BY MPayType.PKPayTypeNo, MPayType.PayTypeName,MPayType.DisplayName,MPayType.ShortName
Declare CurMix Cursor for SELECT MPayType.PKPayTypeNo,MPayType.PayTypeName AS Name,MPayType.DisplayName,MPayType.ShortName, SUM(TVoucherRefDetails.Amount) AS Amount FROM TVoucherEntry INNER JOIN  TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo INNER JOIN  MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo INNER JOIN  TVoucherPayTypeDetails ON TVoucherEntry.PkVoucherNo = TVoucherPayTypeDetails.FKReceiptVoucherNo WHERE  (TVoucherEntry.VoucherTypeCode = 30) AND (TVoucherRefDetails.RefNo in (SELECT RefNo FROM TVoucherRefDetails AS TVoucherRefDetails_1 WHERE (FkVoucherTrnNo IN (SELECT PkVoucherTrnNo FROM TVoucherDetails AS TVoucherDetails_1
						WHERE (FkVoucherNo IN  (SELECT PkVoucherNo FROM TVoucherEntry AS TVoucherEntry_1 WHERE (PayTypeNo = 3) AND (VoucherDate >= @FromDate) AND (VoucherDate <= @ToDate) 
						AND (VoucherTypeCode = @VchType) AND  TVoucherEntry.IsCancel = 'true'  AND (CompanyNo = @CompanyNo) AND (MixMode = 1))) AND (VoucherSrNo = 1))))) AND (TVoucherEntry.CompanyNo = @CompanyNo)  
GROUP BY MPayType.PKPayTypeNo, MPayType.PayTypeName,MPayType.DisplayName,MPayType.ShortName
    if(@RType=2)
--		Declare CurMix Cursor For SELECT  MPayType.PKPayTypeNo,MPayType.PayTypeName AS Name,MPayType.DisplayName,MPayType.ShortName, SUM(TVoucherRefDetails.Amount) AS Amount FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN
--						TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo
--						WHERE  (TVoucherEntry.VoucherTypeCode = 30) AND (TVoucherEntry.CompanyNo = @CompanyNo) AND 
--						(TVoucherRefDetails.RefNo IN (SELECT RefNo FROM TVoucherRefDetails AS TVoucherRefDetails_1 WHERE (FkVoucherTrnNo IN (SELECT PkVoucherTrnNo FROM TVoucherDetails AS TVoucherDetails_1
--						WHERE (FkVoucherNo IN  (SELECT PkVoucherNo FROM TVoucherEntry AS TVoucherEntry_1 WHERE (PayTypeNo = 3) AND (VoucherDate >= @FromDate) AND (VoucherDate <= @ToDate) 
--						AND (VoucherTypeCode = 15)  AND (CompanyNo = @CompanyNo) AND (MixMode = 1))) AND (VoucherSrNo = 1)))))
--						GROUP BY MPayType.PKPayTypeNo, MPayType.PayTypeName,MPayType.DisplayName,MPayType.ShortName
Declare CurMix Cursor for SELECT MPayType.PKPayTypeNo,MPayType.PayTypeName AS Name,MPayType.DisplayName,MPayType.ShortName, SUM(TVoucherRefDetails.Amount) AS Amount FROM TVoucherEntry INNER JOIN  TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo INNER JOIN  MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo INNER JOIN  TVoucherPayTypeDetails ON TVoucherEntry.PkVoucherNo = TVoucherPayTypeDetails.FKReceiptVoucherNo WHERE  (TVoucherEntry.VoucherTypeCode = 30) AND (TVoucherRefDetails.RefNo in (SELECT RefNo FROM TVoucherRefDetails AS TVoucherRefDetails_1 WHERE (FkVoucherTrnNo IN (SELECT PkVoucherTrnNo FROM TVoucherDetails AS TVoucherDetails_1
						WHERE (FkVoucherNo IN  (SELECT PkVoucherNo FROM TVoucherEntry AS TVoucherEntry_1 WHERE (PayTypeNo = 3) AND (VoucherDate >= @FromDate) AND (VoucherDate <= @ToDate) 
						AND (VoucherTypeCode = @VchType)  AND (CompanyNo = @CompanyNo) AND (MixMode = 1))) AND (VoucherSrNo = 1))))) AND (TVoucherEntry.CompanyNo = @CompanyNo)  
GROUP BY MPayType.PKPayTypeNo, MPayType.PayTypeName,MPayType.DisplayName,MPayType.ShortName


		Open CurMix
		Fetch Next From CurMix Into @PayTypeNo,@PayTypeName,@PayTypeActName,@ShortName,@Amount
		While(@@Fetch_Status = 0)
		Begin
			--insert into @Details values(0 ,@PayTypeName,@PayTypeActName,@ShortName,@Amount,0,2)
			set @TotAmt=@TotAmt+@Amount
			Fetch Next From CurMix Into @PayTypeNo,@PayTypeName,@PayTypeActName,@ShortName,@Amount
		End
		Close CurMix Deallocate CurMix
		set @TotAmt=@MixAmt-@TotAmt
--		if(@TotAmt<>0)
--			insert into @Details values(0 ,'Pending','Pending','PP',@TotAmt,0,2)
	End
	Fetch Next From CurDtls into @PayTypeNo ,@PayTypeName,@PayTypeActName,@ShortName
	end
Close CurDtls Deallocate CurDtls
if(@TotAmt<>0)
update @Details set Amount=Amount+@TotAmt where PayTypeNo=3
delete from @Details where PayTypeNo=12000
Select PayTypeNo ,PayTypeName ,PayTypeActName ,PayTypeShortName ,abs(Amount) AS Amount ,NoOfBills ,typeno from @Details

------------------------------------------------------------------------------------------------------------------------------------------------------------------

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetSaleRegister]
@VchNo int,
@CompNo numeric(18),
@FromDate datetime,
@ToDate datetime,
@Type numeric(18)


AS


--if(@Type=1)
--SELECT  TVoucherEntry.VoucherDate, MVoucherType.VoucherTypeName, TVoucherEntry.Paytypeno, TVoucherEntry.VoucherUserNo,
                          --(SELECT     ISNULL(SUM(Debit+Credit), 0) 
                           -- FROM          TVoucherDetails
                          --  WHERE      (TVoucherDetails.FKVoucherNo = TVoucherEntry.PkVoucherNo) AND (TVoucherDetails.SrNo=501) AND (TVoucherEntry.PayTypeNo in (Select PKPayTypeNo From MPayType Where ControlUnder in(2,6)) )) AS CashAmt,
                         -- (SELECT     ISNULL(SUM(Debit+Credit), 0) 
                          --  FROM          TVoucherDetails
                          --  WHERE      (TVoucherDetails.FKVoucherNo = TVoucherEntry.PkVoucherNo) AND (TVoucherDetails.SrNo=501) AND (TVoucherEntry.PayTypeNo in (Select PKPayTypeNo From MPayType Where ControlUnder=3))) AS CreditAmt,
                          --(SELECT     ISNULL(SUM(Debit+Credit), 0) 
                           -- FROM          TVoucherDetails
                          --  WHERE      (TVoucherDetails.FKVoucherNo = TVoucherEntry.PkVoucherNo) AND (TVoucherDetails.SrNo=501) AND (TVoucherEntry.PayTypeNo in (Select PKPayTypeNo From MPayType Where ControlUnder=4))) AS ChequeAmt,
                        --  (SELECT     ISNULL(SUM(Debit+Credit), 0) 
                           -- FROM          TVoucherDetails
                          --  WHERE      (TVoucherDetails.FKVoucherNo = TVoucherEntry.PkVoucherNo) AND (TVoucherDetails.SrNo=501) AND (TVoucherEntry.PayTypeNo in (Select PKPayTypeNo From MPayType Where ControlUnder=5))) AS CCAmt,
                          --(SELECT     LedgerName
                           -- FROM          MLedger
                           -- WHERE      (LedgerNo = TVoucherDetails_1.LedgerNo)) AS LedgerName,TVoucherEntry.IsCancel
				--FROM    TVoucherEntry INNER JOIN MVoucherType ON TVoucherEntry.VoucherTypeCode = MVoucherType.VoucherTypeCode INNER JOIN
				--TVoucherDetails AS TVoucherDetails_1 ON TVoucherEntry.PkVoucherNo = TVoucherDetails_1.FkVoucherNo
				--WHERE (TVoucherEntry.VoucherTypeCode = 15) AND  (TVoucherEntry.VoucherDate >= @FromDate) AND 
				--(TVoucherEntry.VoucherDate <= @ToDate) AND (TVoucherDetails_1.SrNo = 501)
				--ORDER BY TVoucherEntry.VoucherDate
if(@Type=1)

SELECT     TVoucherEntry.VoucherDate, TVoucherEntry.VoucherUserNo,TVoucherEntry.Reference, MLedger.LedgerName,'' as ItemName, 0 as Barcode, TVoucherEntry.BilledAmount, MPayType.PayTypeName, TStock.Quantity, 
                      TStock.BilledQuantity, TStock.NetAmount, TStock.Amount, TStock.SGSTPercentage, TStock.SGSTAmount, TStock.CGSTPercentage, TStock.CGSTAmount, 
                      TStock.IGSTPercentage, TStock.IGSTAmount, TStock.CessPercentage, TStock.CessAmount, TStock.DiscPercentage, TStock.DiscAmount, TStock.DiscRupees, 
                      TStock.DiscPercentage2, TStock.DiscAmount2, TStock.DiscRupees2,
 '' as UOMName , '' AS FreeUomName,TStock.itemno,0 as MRP, TStock.PackagingCharges
FROM         TVoucherEntry INNER JOIN
                      MLedger ON TVoucherEntry.LedgerNo = MLedger.LedgerNo INNER JOIN
                      MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo INNER JOIN
                      TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
WHERE (TVoucherEntry.VoucherTypeCode = @VchNo) AND  (TVoucherEntry.VoucherDate >= @FromDate) AND 
				(TVoucherEntry.VoucherDate <= @ToDate) and TVoucherEntry.iscancel='false'
				ORDER BY TVoucherEntry.VoucherDate, TVoucherEntry.VoucherUserNo


else 

SELECT     TVoucherEntry.VoucherDate, TVoucherEntry.VoucherUserNo,TVoucherEntry.Reference, MLedger.LedgerName, MItemGroup.ItemGroupName + ' ' + MItemMaster.ItemShortName AS ItemName, 
                      MItemMaster.Barcode, TVoucherEntry.BilledAmount, MPayType.PayTypeName, TStock.Quantity, TStock.BilledQuantity, TStock.NetAmount, TStock.Amount, 
                      TStock.SGSTPercentage, TStock.SGSTAmount, TStock.CGSTPercentage, TStock.CGSTAmount, TStock.IGSTPercentage, TStock.IGSTAmount, TStock.CessPercentage, 
                      TStock.CessAmount, TStock.DiscPercentage, TStock.DiscAmount, TStock.DiscRupees, TStock.DiscPercentage2, TStock.DiscAmount2, TStock.DiscRupees2, 
                      MUOM.UOMName, MUOM_1.UOMName AS FreeUomName, TStock.ItemNo, TStock.Rate, MRateSetting.MRP, TStock.PackagingCharges
FROM         TVoucherEntry INNER JOIN
                      MLedger ON TVoucherEntry.LedgerNo = MLedger.LedgerNo INNER JOIN
                      MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo INNER JOIN
                      TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo INNER JOIN
                      MItemMaster ON TStock.ItemNo = MItemMaster.ItemNo INNER JOIN
                      MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo INNER JOIN
                      MUOM ON TStock.FkUomNo = MUOM.UOMNo INNER JOIN
                      MUOM AS MUOM_1 ON TStock.FreeUOMNo = MUOM_1.UOMNo INNER JOIN
                      MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo
WHERE (TVoucherEntry.VoucherTypeCode = @VchNo) AND  (TVoucherEntry.VoucherDate >= @FromDate) AND 
				(TVoucherEntry.VoucherDate <= @ToDate) and TVoucherEntry.iscancel='false'
				ORDER BY  TVoucherEntry.VoucherDate, TVoucherEntry.VoucherUserNo

------------------------------------------------------------------------------------------------------------------------------------------------------------------

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetStockAllItemQty]
@CompNo		           numeric(18),
@FromDate	           datetime,
@ToDate		           datetime,
@ItStr		            varchar(max),
@Itype                numeric(18)



AS
Declare @ItemNo		numeric(18)
Declare @TStock TABLE (ItemNo int,ItemName varchar(max),OpQty numeric(18,2),InwardQty numeric(18,2),OutwardQty numeric(18,2),Quantity numeric(18,2))
Declare @ItemTable TABLE(ItemNo numeric(18))
Declare @ItemName varchar(max),@OpQty numeric(18,2),@CrQty numeric(18,2),@ClosingQty numeric(18,2),@DrQty numeric(18,2),@StrQry varchar(max),@ItNo numeric(18),@StrItem varchar(max),@StrVchType varchar(max)
set @OpQty=0  set @DrQty=0  set @CrQty=0 set @StrQry='' 
set @StrItem=''


if(@ItStr<>'')
	begin
		set @StrItem='(TStock.ItemNo in ('+@ItStr+')) AND ' 
		--insert into @ItemTable  Exec (@StrQry) 
    end
else
    begin
		set @StrItem=' '
    end

if(@Itype=0)
begin 
set @StrVchType='and TVoucherEntry.vouchertypecode in(15,12,13,9,8)'
end
else
begin 
set @StrVchType='and TVoucherEntry.vouchertypecode in(115,112,113,109,108)'
end

set @StrQry='Select Tbl1.ItemNo,(SELECT     MItemGroup.ItemGroupName + '' '' + CASE WHEN (ItemShortName <> '''') THEN ItemShortName ELSE ItemName END AS Expr1
                            FROM          MItemMaster AS MItemMaster_1 INNER JOIN
                                                   MItemGroup ON MItemMaster_1.GroupNo = MItemGroup.ItemGroupNo
                            WHERE      (MItemMaster_1.ItemNo = Tbl1.ItemNo)) As ItemName, Sum(OpQty) As OpQty, abs(Sum(InQty)) As InwardQty, abs(Sum(OutQty)) As OutwardQty, 
Sum(OpQty + abs(InQty) - abs(OutQty)) As Quantity,MItemMaster.Barcode From
(
SELECT  TStock.ItemNo,
         sum(case when (TStock.TrnCode = 1) then isnull(TStock.BilledQuantity,0) 
                 else isnull(TStock.BilledQuantity,0)*-1 end) as OpQty,
        0.00 as InQty, 0.00 as OutQty
FROM    TVoucherEntry INNER JOIN
        TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
WHERE   ' + @StrItem + ' (TVoucherEntry.VoucherDate < '''+cast(@FromDate as varchar)+''')  and TVoucherEntry.IsCancel=''false'' ' + @StrVchType + '
Group by TStock.itemno
UNION ALL
SELECT  TStock.ItemNo,
         0.00 as OpQty,
         sum(case when (TStock.TrnCode = 1) then isnull(TStock.BilledQuantity,0) 
                 else 0.00 end ) as InQty,
         sum(case when (TStock.TrnCode = 2) then isnull(TStock.BilledQuantity,0)*-1 
                 else 0.00 end) as OutQty
FROM    TVoucherEntry INNER JOIN
        TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
WHERE    ' + @StrItem + ' (TVoucherEntry.VoucherDate >= '''+cast(@FromDate as varchar)+''') AND 
        (TVoucherEntry.VoucherDate <= '''+cast(@ToDate as varchar)+''')  and TVoucherEntry.IsCancel=''false'' ' + @StrVchType + '
Group by TStock.itemno
) As Tbl1 
INNER JOIN  MItemMaster ON MItemMaster.ItemNo = Tbl1.ItemNo
Group BY Tbl1.ItemNo, MItemMaster.ItemName ,MItemMaster.Barcode
order by ItemName '

Exec(@StrQry)

--Select Tbl1.ItemNo,(SELECT     MItemGroup.ItemGroupName + ' ' + CASE WHEN (ItemShortName <> '') THEN ItemShortName ELSE ItemName END AS Expr1
--                            FROM          MItemMaster AS MItemMaster_1 INNER JOIN
--                                                   MItemGroup ON MItemMaster_1.GroupNo = MItemGroup.ItemGroupNo
--                            WHERE      (MItemMaster_1.ItemNo = Tbl1.ItemNo)) As ItemName, Sum(OpQty) As OpQty, abs(Sum(InQty)) As InwardQty, abs(Sum(OutQty)) As OutwardQty, 
--Sum(OpQty + abs(InQty) - abs(OutQty)) As Quantity,MItemMaster.Barcode From
--(
--SELECT  TStock.ItemNo,
--         sum(case when (TStock.TrnCode = 1) then isnull(TStock.BilledQuantity,0) 
--                 else isnull(TStock.BilledQuantity,0)*-1 end) as OpQty,
--        0.00 as InQty, 0.00 as OutQty
--FROM    TVoucherEntry INNER JOIN
--        TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
--WHERE  TStock.itype=1 and  (TStock.ItemNo in (46637,46639,46640,46592,46587,46609,46608,46590,46607)) AND  (TVoucherEntry.VoucherDate < 'Jan  1 2018 12:00AM')  and TVoucherEntry.IsCancel='false'
--Group by TStock.itemno
--UNION ALL
--SELECT  TStock.ItemNo,
--         0.00 as OpQty,
--         sum(case when (TStock.TrnCode = 1) then isnull(TStock.BilledQuantity,0) 
--                 else 0.00 end ) as InQty,
--         sum(case when (TStock.TrnCode = 2) then isnull(TStock.BilledQuantity,0)*-1 
--                 else 0.00 end) as OutQty
--FROM    TVoucherEntry INNER JOIN
--        TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo
--WHERE  TStock.itype=1 and  (TStock.ItemNo in (46637,46639,46640,46592,46587,46609,46608,46590,46607)) AND  (TVoucherEntry.VoucherDate >= 'Jan  1 2018 12:00AM') AND 
--        (TVoucherEntry.VoucherDate <= 'Jan 11 2019 12:00AM')  and TVoucherEntry.IsCancel='false'
--Group by TStock.itemno
--) As Tbl1 
--INNER JOIN  MItemMaster ON MItemMaster.ItemNo = Tbl1.ItemNo
--Group BY Tbl1.ItemNo, MItemMaster.ItemName ,MItemMaster.Barcode
--order by ItemName 

	RETURN

------------------------------------------------------------------------------------------------------------------------------------------------------------------

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AddTDeliveryAddress]
     @PkSrNo                              numeric(18),
     @FkVoucherno                         numeric(18),
     @Ledgerno                            numeric(18),
     @LedgerDetailsNo                     numeric(18),
     @IsActive                            bit,
     @UserId                              int,
	 @UserDate					          datetime,
	 --@ModifiedBy					      varchar(500),
@Companyno                   int
   --  @StatusNo							  int
	
AS
IF EXISTS(select PkSrNo from TDeliveryAddress
          where
          FkVoucherno = @FkVoucherno)
     BEGIN
       --Update existing row
       UPDATE TDeliveryAddress
       SET
          FkVoucherno		= @FkVoucherno,
          Ledgerno			= @Ledgerno,
		  LedgerDetailsNo	= @LedgerDetailsNo,
          IsActive			= @IsActive,
          UserID			= @UserID,
          UserDate			= @UserDate,
          ModifiedBy		= isnull(ModifiedBy,'') + cast(@UserID as varchar)+'@'+ CONVERT(VARCHAR(10), GETDATE(), 105),
          StatusNo			= 2

       WHERE
          FkVoucherno = @FkVoucherno

     END
ELSE
     BEGIN
       --Insert new row
       Declare @Id numeric
       SELECT @Id=IsNull(Max(PkSrNo),0) From TDeliveryAddress
       DBCC CHECKIDENT('TDeliveryAddress', RESEED, @Id)
       INSERT INTO TDeliveryAddress(
 
          FkVoucherno,
          Ledgerno,
		  LedgerDetailsNo,
          IsActive,
          UserID,
          UserDate,
CompanyNo,
          StatusNo
	
)
       VALUES(

          @FkVoucherno,
          @Ledgerno,
		  @LedgerDetailsNo,
		  @IsActive,
		  @UserID,
		  @UserDate,
@CompanyNo,
          1
)

END

------------------------------------------------------------------------------------------------------------------------------------------------------------------

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created on 01/11/2011
ALTER PROCEDURE [dbo].[AddTEWayDetails]
    @PKEWayNo						numeric(18),						                            
    @FkVoucherNo					numeric(18),		                       
    @EWayNo							varchar(50),                     
    @VoucherUserNo                  numeric(18),     
    @EWayDate                       datetime,
    @ModeNo                         numeric(18),
    @Distance                       numeric(18,2),    
	@TransportNo					numeric(18),							  
	@VehicleNo						varchar(50),
	@LRNo								varchar(50),
	@LRDate							datetime,
	@LedgerNo						numeric(18),
	@LedgerName						varchar(max),
	@Address						varchar(max),
	@CityNo							numeric(18),
	@CityName						varchar(50),
	@PinCode						numeric(18),
	@StateCode						numeric(18),
	@StateName						varchar(50),
	@UserID							numeric(18),
	@UserDate						datetime,
	--@ModifiedBy						varchar(max),
	@StatusNo						int,
	@IsActive						bit
    
AS
IF EXISTS(select FkVoucherNo from [TEWayDetails]
          where
          FkVoucherNo = @FkVoucherNo)
     BEGIN
       UPDATE TEWayDetails
       SET

	FkVoucherNo		= @FkVoucherNo,
	EWayNo			= @EWayNo,
	VoucherUserNo	= @VoucherUserNo,
	EWayDate		= @EWayDate,
	ModeNo			= @ModeNo,
	Distance		= @Distance,
	TransportNo		= @TransportNo,
	VehicleNo		= @VehicleNo,
	LRNo			= @LRNo,
	LRDate			= @LRDate,
	LedgerNo		= @LedgerNo,
	LedgerName		= @LedgerName,
	Address			= @Address,
	CityNo			= @CityNo,
	CityName		= @CityName,
	PinCode			= @PinCode,
	StateCode		= @StateCode,
	StateName		= @StateName,
	UserID			= @UserID,
	UserDate		= @UserDate,
	ModifiedBy		= isnull(ModifiedBy,'') + cast(@UserID as varchar)+'@'+ CONVERT(VARCHAR(10), GETDATE(), 105),
	StatusNo		= 2,
	IsActive		= @IsActive
    
       WHERE
          PKEWayNo = @PKEWayNo

     END
ELSE
     BEGIN
       Declare @Id numeric
       SELECT @Id=IsNull(Max(PKEWayNo),0) From TEWayDetails
       DBCC CHECKIDENT('TEWayDetails', RESEED, @Id)
       INSERT INTO TEWayDetails(

			FkVoucherNo,
			EWayNo,
			VoucherUserNo,
			EWayDate,
			ModeNo,
			Distance,
			TransportNo,
			VehicleNo,
			LRNo,
			LRDate,
			LedgerNo,
			LedgerName,
			Address,
			CityNo,
			CityName,
			PinCode,
			StateCode,
			StateName,
			UserID,
			UserDate,
			--ModifiedBy,
			StatusNo,
			IsActive
		        
)
       VALUES(
			@FkVoucherNo,
			@EWayNo,
			@VoucherUserNo,
			@EWayDate,
			@ModeNo,
			@Distance,
			@TransportNo,
			@VehicleNo,
			@LRNo,
			@LRDate,
			@LedgerNo,
			@LedgerName,
			@Address,
			@CityNo,
			@CityName,
			@PinCode,
			@StateCode,
			@StateName,
			@UserID,
			@UserDate,
			--@ModifiedBy,
			1,
			@IsActive        
          
)

END

------------------------------------------------------------------------------------------------------------------------------------------------------------------














