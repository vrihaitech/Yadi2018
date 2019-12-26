/*----version 1.0.1.1*/
USE [Yadi2018]
--altered on 01/24/2019--umesh

SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON

GO
ALTER TABLE dbo.MRecipeMain ADD
	ESFlag bit NULL
GO

/*--------Added new column esflag ------------------------------------------------------------------------------------------------------------------------------*/

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--altered on 01/24/2019--umesh

ALTER PROCEDURE [dbo].[AddMRecipeMain]
 @MRecipeID                  int,
 @DocNo                      int,
 --@ItemType                   varchar(50),
 @GroupNo                    int,
 @FinishItemID               int,
 @PackingSize                decimal(18,2), 
 @RDate                      datetime,
 @Qty                        numeric(18,2), 
 @UomNo                      numeric(18,0),
 @RecipeType                 numeric(18,0),
 @IsActive                   bit, 
 @UserID                     int,
 @UserDate                   datetime,
 @ProdQty                    numeric(18,2),
 @FkRecipeID                 numeric(18,2),
 @IsLock                     bit,
 @ReturnID                   int output

AS
IF EXISTS(select MRecipeID from MRecipeMain
          where
          MRecipeID = @MRecipeID and RecipeType=@RecipeType)
     BEGIN
       --Update existing row
       UPDATE MRecipeMain
       SET
        DocNo=@DocNo,
		--ItemType=@ItemType,
		GroupNo=@GroupNo,
		FinishItemID=@FinishItemID,
		PackingSize=@PackingSize,
		RDate=@RDate,
        Qty=@Qty,
        ProdQty=@ProdQty,
        UomNo=@UomNo,
        RecipeType=@RecipeType,
        FkRecipeID=@FkRecipeID,
        IsLock=@IsLock,
		IsActive=@IsActive,
		UserID=@UserID,
        ModifiedOn = isnull(ModifiedOn,'') + cast(@UserID as varchar)+'@'+ CONVERT(VARCHAR(10), GETDATE(), 105)

       WHERE
          MRecipeID = @MRecipeID and RecipeType=@RecipeType
		set @ReturnID=@MRecipeID 

     END
ELSE
     BEGIN
       --Insert new row
       Declare @Id numeric
  SELECT @Id=IsNull(Max(MRecipeID),0) From MRecipeMain
       DBCC CHECKIDENT('MRecipeMain', RESEED, @Id)
       SELECT @DocNo=IsNull(Max(DocNo),0)+1 From MRecipeMain where RecipeType=@RecipeType
     
       INSERT INTO MRecipeMain(
        DocNo,
		--ItemType, 
		GroupNo,
		FinishItemID, 
		PackingSize, 
		RDate, 
        Qty,
        ProdQty,
        UomNo,
        RecipeType,
        FkRecipeID,
        IsLock,
		IsActive, 
		UserID, 
		UserDate
         
)
       VALUES(
         
		@DocNo,
		--@ItemType,
		@GroupNo, 
		@FinishItemID, 
		@PackingSize, 
		@RDate,
        @Qty, 
        @ProdQty,
        @UomNo,
        @RecipeType,
        @FkRecipeID,
        @IsLock,
		@IsActive, 
		@UserID,
        @UserDate         
)
Set @ReturnID=Scope_Identity()
END


/*---------------------------------------------------------------------------------------------------------------------------------------------------*/


set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created on 01/24/2019--umesh
ALTER PROCEDURE [dbo].[AddMItemTaxInfo2]
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

BEGIN
	   --Insert new row
	     Declare @Id numeric
	   SELECT @Id=IsNull(Max(PkSrNo),0) From MItemTaxInfo
	   DBCC CHECKIDENT('MItemTaxInfo', RESEED, @Id)
	   INSERT INTO MItemTaxInfo(
          -- PkSrNo,     
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
          -- @PkSrNo,
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


/*---------------------------------------------------------------------------------------------------------------------------------------------------*/


