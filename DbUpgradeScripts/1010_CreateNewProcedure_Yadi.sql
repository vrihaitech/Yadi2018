set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created on 01/11/2011
Create PROCEDURE [dbo].[AddMRack]
     @RackNo                              numeric(18),
     @RackName                            varchar(50),
     @RackCode                            varchar(50),
     @IsActive                            bit,
     @UserID                              numeric(18),
     @UserDate                            datetime,
	 @CompanyNo							  numeric(18)

AS
IF EXISTS(select RackNo from MRack
          where
          RackNo = @RackNo)
     BEGIN
       --Update existing row
       UPDATE MRack
       SET
          RackName = @RackName,
          RackCode = @RackCode,
          IsActive = @IsActive,
          UserID = @UserID,
          UserDate = @UserDate,
	      CompanyNo=@CompanyNo

       WHERE
          RackNo = @RackNo

     END
ELSE
     BEGIN
       --Insert new row
       Declare @Id numeric
       SELECT @Id=IsNull(Max(RackNo),0) From MRack
       DBCC CHECKIDENT('MRack', RESEED, @Id)
       INSERT INTO MRack(
          RackName,
          RackCode,
          IsActive,
          UserID,
          UserDate,
		  CompanyNo
)
       VALUES(
          @RackName,
          @RackCode,
          @IsActive,
          @UserID,
          @UserDate,
	      @CompanyNo
)

END

/*-------------------------------------------------------------------------------------------------------------------------------------------------------*/

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created on 01/11/2011
Create PROCEDURE [dbo].[AddMRackDetails]
     @RackDetailsNo                       numeric(18),
     @UOMLNo                              numeric(18, 0),
     @FkRackNo                            numeric(18, 0),
     @ItemNo                              numeric(18, 0),
     @ToQty                               numeric(18, 2) ,
     @IsActive                            bit,
     @UserID                              numeric(18),
     @UserDate                            datetime

AS
IF EXISTS(select RackDetailsNo from MRackDetails
          where
          RackDetailsNo = @RackDetailsNo)
     BEGIN
       --Update existing row
       UPDATE MRackDetails
       SET
          UOMLNo=@UOMLNo,
	      FkRackNo=@FkRackNo,
          ItemNo=@ItemNo,
          ToQty=@ToQty, 
          IsActive = @IsActive,
          UserID = @UserID,
          UserDate = @UserDate

       WHERE
          RackDetailsNo = @RackDetailsNo

     END
ELSE
     BEGIN
       --Insert new row
       Declare @Id numeric
       SELECT @Id=IsNull(Max(RackDetailsNo),0) From MRackDetails
       DBCC CHECKIDENT('MRackDetails', RESEED, @Id)
       INSERT INTO MRackDetails(

          UOMLNo,
	      FkRackNo,
          ItemNo,
          ToQty, 
          IsActive,
          UserID,
          UserDate
)
       VALUES(

          @UOMLNo,
	      @FkRackNo,
          @ItemNo,
          @ToQty, 
          @IsActive,
          @UserID,
          @UserDate
)

END

/*-------------------------------------------------------------------------------------------------------------------------------------------------------*/

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created on 09/12/2011
Create PROCEDURE [dbo].[AddMLedgerGroup]
     @LedgerGroupNo                       numeric(18),
     @LedgerName                          varchar(250),
	 @LedgerLangName					  nvarchar(500),
     @GroupNo                             numeric(18),
	 @IsActive                            bit,
	 @UserID                              numeric(18),
     @UserDate                            datetime,
	 @CompanyNo                           numeric(18),
     @ReturnID							  int output
AS
IF EXISTS(select LedgerGroupNo from MLedgerGroup
          where
          LedgerGroupNo = @LedgerGroupNo)
     BEGIN
       --Update existing row
       UPDATE MLedgerGroup
       SET
          LedgerName = @LedgerName,
          LedgerLangName=@LedgerLangName,
          GroupNo = @GroupNo,
		  IsActive=@IsActive,      
		  UserID = @UserID,
          UserDate = @UserDate,
		  CompanyNo = @CompanyNo
          
       WHERE
          LedgerGroupNo = @LedgerGroupNo		
		  set @ReturnID=@LedgerGroupNo

     END
ELSE
     BEGIN
       --Insert new row
       Declare @Id numeric
       SELECT @Id=IsNull(Max(LedgerGroupNo),0) From MLedgerGroup
       DBCC CHECKIDENT('MLedgerGroup', RESEED, @Id)

       INSERT INTO MLedgerGroup(
         
          LedgerName,
          LedgerLangName,
          GroupNo,
		  IsActive,
          UserID,
          UserDate,
          CompanyNo
         
)
       VALUES(

          @LedgerName,
          @LedgerLangName,
          @GroupNo,
	      @IsActive,
		  @UserID,
          @UserDate,
          @CompanyNo
  	 
)
set @ReturnID=Scope_Identity()

END

/*-------------------------------------------------------------------------------------------------------------------------------------------------------*/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created on 09/12/2011
Create PROCEDURE [dbo].[AddMLedgerGroupDetails]
     @LedgerGrpDetailsNo                  numeric(18),
     @LedgerGroupNo                       numeric(18),
	 @LedgerNo				        	  numeric(18),
     @IsActive                            bit,
	 @UserID                              numeric(18),
     @UserDate                            datetime,
	 @CompanyNo                           numeric(18)
AS
IF EXISTS(select LedgerGrpDetailsNo from MLedgerGroupDetails
          where
          LedgerGrpDetailsNo = @LedgerGrpDetailsNo)
     BEGIN
       --Update existing row
       UPDATE MLedgerGroupDetails
       SET
          LedgerGroupNo = @LedgerGroupNo,
          LedgerNo=@LedgerNo,
          IsActive=@IsActive,      
		  UserID = @UserID,
          UserDate = @UserDate,
		  CompanyNo = @CompanyNo
          
       WHERE
          LedgerGrpDetailsNo = @LedgerGrpDetailsNo	

     END
ELSE
     BEGIN
       --Insert new row
       Declare @Id numeric
       SELECT @Id=IsNull(Max(LedgerGrpDetailsNo),0) From MLedgerGroupDetails
       DBCC CHECKIDENT('MLedgerGroupDetails', RESEED, @Id)

       INSERT INTO MLedgerGroupDetails(
         
          LedgerGroupNo,
          LedgerNo,
          IsActive,
          UserID,
          UserDate,
          CompanyNo
         
)
       VALUES(

          @LedgerGroupNo,
          @LedgerNo,
          @IsActive,
		  @UserID,
          @UserDate,
          @CompanyNo
  	 
)
END

/*-------------------------------------------------------------------------------------------------------------------------------------------------------*/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created on 01/11/2011
Create PROCEDURE  [dbo].[DeleteMRack] 
@RackNo                             numeric(18)

AS
Update MRack set IsActive='False' where RackNo = @RackNo

/*-------------------------------------------------------------------------------------------------------------------------------------------------------*/

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created on 09/12/2011
Create PROCEDURE  [dbo].[DeleteMLedgerGroup] 
@LedgerGroupNo                          numeric(18)


AS
Update MLedgerGroup set IsActive='False' where LedgerGroupNo= @LedgerGroupNo

/*-------------------------------------------------------------------------------------------------------------------------------------------------------*/
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[GetRackWiseBill]
@PkVoucherNo                numeric(18),
@Type						int


AS
Begin

SELECT     TVoucherEntry.VoucherUserNo, TVoucherEntry.VoucherDate, CASE WHEN (@Type = 1) 
                      THEN MStockItems_V_1.ItemName ELSE MStockItems_V_1.ItemNameLang END AS ItemName, MRateSetting.MRP, TStock.Quantity, TStock.GRWeight, 
                      TStock.TRWeight, TStock.PackagingCharges, TStock.NoOfBag, TStock.CessValue, TStock.OtherCharges AS Dhekharek, TStock.Rate, TStock.Amount, MUOM.UOMName, 
                      TStock.SGSTPercentage, TStock.SGSTAmount, TStock.DiscPercentage, TStock.DiscAmount, TStock.DiscRupees, TStock.DiscPercentage2, TStock.DiscAmount2, 
                      TStock.DiscRupees2, TStock.CGSTPercentage, TStock.CGSTAmount, TStock.IGSTPercentage, TStock.IGSTAmount, TStock.CessPercentage, TStock.CessAmount, 
                      MLedgerDetails.GSTNO AS CustGSTNo, MLedgerDetails.PANNo AS CustPANNo, MLedgerDetails.FSSAI, MFirm.GSTNO AS CmpGSTNo, MLedger.StateCode, 
                      MItemMaster.HSNCode, TVoucherEntry.BilledAmount, CASE WHEN (@Type = 1) THEN LedgerName ELSE LedgerLangName END AS LedgerName, 
                      MLedger.ContactPerson, MLedgerDetails.PhNo1 AS CustPhNo, MLedgerDetails.MobileNo1 AS CustMoNo, CASE WHEN (@Type = 1) 
                      THEN MLedgerDetails.Address ELSE MLedgerDetails.AddressLang END AS CustAddress, MFirm.Address + ' / ' + MFirm.PinCode AS CmpAddress, 
                      MFirm.EmailID AS CmpEmailID, MFirm.PhoneNo1 + ' / ' + MFirm.PhoneNo2 AS CompPhoneNo, MFirm.FSSAINO AS CompFSSAINo, TVoucherEntry.Reference, 
                      TVoucherEntry.VoucherTime, TStock.NetAmount + TStock.CessValue + TStock.PackagingCharges + TStock.OtherCharges AS NetAmount, TStock.NetRate, 
                      TStock.DisplayItemName, TStock.Remarks AS RemarksItemlevel, '' AS TransporterName, TVoucherEntry.LRNo, MTransporterMode.TransModeName, 
                      TVoucherEntry.TransNoOfItems,
                          (SELECT     SettingValue
                            FROM          MSettings
                            WHERE      (PkSettingNo = 311)) AS DisplayTrans, TVoucherEntry.Remark, MState.StateName, MCity.CityName, MPayType.PayTypeName, 
                      MLedgerDetails_1.Address AS CustDeliveryAddress, mcity_1.CityName AS custDeliverycity, MRack.RackName, MRackDetails.ToQty
FROM         MCity AS mcity_1 RIGHT OUTER JOIN
                      TStock INNER JOIN
                      MUOM ON TStock.FkUomNo = MUOM.UOMNo INNER JOIN
                      TVoucherEntry ON TStock.FKVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN
                      MFirm ON TVoucherEntry.CompanyNo = MFirm.FirmNo INNER JOIN
                      MItemMaster ON TStock.ItemNo = MItemMaster.ItemNo INNER JOIN
                      dbo.MStockItems_V(NULL, NULL, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1 ON TStock.ItemNo = MStockItems_V_1.ItemNo INNER JOIN
                      MLedger ON TVoucherEntry.LedgerNo = MLedger.LedgerNo INNER JOIN
                      MLedgerDetails ON MLedgerDetails.LedgerNo = MLedger.LedgerNo INNER JOIN
                      MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo LEFT OUTER JOIN
                      MTransporterMode ON TVoucherEntry.TransportMode = MTransporterMode.TransModeNo INNER JOIN
                      MState ON MLedger.StateCode = MState.StateCode LEFT OUTER JOIN
                      MCity ON MLedgerDetails.CityNo = MCity.CityNo INNER JOIN
                      MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo INNER JOIN
                      MRack INNER JOIN
                      MRackDetails ON MRack.RackNo = MRackDetails.FkRackNo ON MItemMaster.GroupNo = MRackDetails.ItemNo LEFT OUTER JOIN
                      TDeliveryAddress ON TVoucherEntry.PkVoucherNo = TDeliveryAddress.FkVoucherno AND TVoucherEntry.LedgerNo = TDeliveryAddress.Ledgerno LEFT OUTER JOIN
                      MLedgerDetails AS MLedgerDetails_1 ON MLedgerDetails_1.LedgerDetailsNo = TDeliveryAddress.LedgerDetailsNo AND 
                      TDeliveryAddress.Ledgerno = MLedgerDetails_1.DeliveryLedgerNo ON mcity_1.CityNo = MLedgerDetails_1.CityNo
WHERE     (TVoucherEntry.PkVoucherNo = @PkVoucherNo)

ORDER BY TStock.PkStockTrnNo

END

/*-------------------------------------------------------------------------------------------------------------------------------------------------------*/

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created on 01/11/2011
Create PROCEDURE  [dbo].[DeleteMRack] 
@RackNo                             numeric(18)

AS
Update MRack set IsActive='False' where RackNo = @RackNo

/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created on 01/11/2011
Create PROCEDURE  [dbo].[DeleteMRackDetails] 
@RackDetailsNo                             numeric(18)

AS
Update MRackDetails set IsActive='False' where RackDetailsNo = @RackDetailsNo

/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created on 09/12/2011
Create PROCEDURE  [dbo].[DeleteMLedgerGroup] 
@LedgerGroupNo                          numeric(18)


AS
Update MLedgerGroup set IsActive='False' where LedgerGroupNo= @LedgerGroupNo

/*-------------------------------------------------------------------------------------------------------------------------------------------------------*/

