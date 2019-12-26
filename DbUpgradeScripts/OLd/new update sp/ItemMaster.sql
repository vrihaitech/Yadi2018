--a/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
USE [Yadi2018BLANK]
GO
/* Initiate db transaction */
BEGIN TRANSACTION
GO
/* Verify script already applied */
IF NOT EXISTS(select ScriptNo from DBVersionLog where ScriptNo = 1)
BEGIN

/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* Add ContainerCharges column in MItemMaster table */
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON


ALTER TABLE dbo.MItemMaster ADD
	ContainerCharges  numeric(18,2) NULL
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* Update default value for IsSendEmail */

UPDATE dbo.MItemMaster SET ContainerCharges = 0

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
/* Add ContainerCharges column in MRateSetting table */
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
USE [Yadi2018BLANK]
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON

ALTER TABLE dbo.MRateSetting ADD
	Weight1  numeric(18,2) NULL,
    Weight2  numeric(18,2) NULL
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* Update default value for IsSendEmail */
UPDATE dbo.MRateSetting SET Weight1 = 0,Weight2 = 0

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
     @Weight2                             Numeric(18,2)
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
          Weight2 = @Weight2 
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
          Weight2 
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
          @Weight2 
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

/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

/* Insert DBVersionLog   */
INSERT INTO [DBVersionLog]
           ([ScriptNo]
           ,[ScriptDescription])
     VALUES
           (1
           ,'001_added_customerwise_discount.sql file execute')

/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* End of Verify script already applied */
END
GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* Commit all changes */
COMMIT
GO
