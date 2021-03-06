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
     @SPPerc                             Numeric(18,2),
     @Hamali                             Numeric(18,2)
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
          SPPerc = @SPPerc,
          Hamali=@Hamali  
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
          SPPerc,
          Hamali
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
          @SPPerc,
          @Hamali
)

END







