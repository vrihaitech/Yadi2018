/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
USE [Yadi2018]
GO
ALTER TABLE dbo.MItemMaster ADD
        GSTSlab bit NULL
GO
ALTER TABLE dbo.MItemMaster ADD CONSTRAINT
        DF_MItemMaster_GSTSlab DEFAULT 0 FOR GSTSlab
GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
GO
--Created on 23/03/2019 UMESH
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
@GSTSlab   bit,
     @ReturnID                            INT OUTPUT

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
          ContainerCharges=@ContainerCharges,
GSTSlab=@GSTSlab
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
          ContainerCharges,GSTSlab
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
          @ContainerCharges,
@GSTSlab
         -- @Stock
)
     SET @ReturnID = SCOPE_IDENTITY()
END

