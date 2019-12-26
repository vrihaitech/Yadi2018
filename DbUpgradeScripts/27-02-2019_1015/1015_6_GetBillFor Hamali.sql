set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetBill]
@PkVoucherNo                numeric(18),
@Type						int


AS
Begin


SELECT        TVoucherEntry.VoucherUserNo, TVoucherEntry.VoucherDate, CASE WHEN (@Type = 1) 
                          THEN MStockItems_V_1.ItemName ELSE MStockItems_V_1.ItemNameLang END AS ItemName, MRateSetting.MRP, TStock.Quantity, TStock.GRWeight, 
                         TStock.TRWeight, TStock.PackagingCharges, TStock.NoOfBag, TStock.CessValue, TStock.OtherCharges AS Dhekharek, TStock.Rate, TStock.Amount, 
                         MUOM.UOMName, TStock.SGSTPercentage, TStock.SGSTAmount, TStock.DiscPercentage, TStock.DiscAmount, TStock.DiscRupees, TStock.DiscPercentage2, 
                         TStock.DiscAmount2, TStock.DiscRupees2, TStock.CGSTPercentage, TStock.CGSTAmount, TStock.IGSTPercentage, TStock.IGSTAmount, TStock.CessPercentage, 
                         TStock.CessAmount, MLedgerDetails.GSTNO AS CustGSTNo, MLedgerDetails.PANNo AS CustPANNo, MLedgerDetails.FSSAI, MFirm.GSTNO AS CmpGSTNo, MUOM.UomLang,
                         MLedger.StateCode, MItemMaster.HSNCode, TVoucherEntry.BilledAmount, CASE WHEN (@Type = 1) THEN LedgerName ELSE LedgerLangName END AS LedgerName, 
                         MLedger.ContactPerson, MLedgerDetails.PhNo1 AS CustPhNo, MLedgerDetails.MobileNo1 AS CustMoNo, CASE WHEN (@Type = 1) 
                         THEN MLedgerDetails.Address ELSE MLedgerDetails.AddressLang END AS CustAddress, MFirm.Address + ' / ' + Mfirm.pincode AS CmpAddress, MFirm.EmailID AS CmpEmailID, 
                         MFirm.PhoneNo1 + ' / ' + MFirm.PhoneNo2 AS CompPhoneNo,MFirm.FSSAINo as CompFSSAINo, TVoucherEntry.Reference, TVoucherEntry.VoucherTime, 
                         TStock.NetAmount + TStock.CessValue + TStock.PackagingCharges + TStock.OtherCharges AS NetAmount, TStock.NetRate, TStock.DisplayItemName, 
                         TStock.Remarks AS RemarksItemlevel, '' AS TransporterName, TVoucherEntry.LRNo, MTransporterMode.TransModeName, TVoucherEntry.TransNoOfItems,
                             (SELECT        SettingValue
                               FROM            MSettings
                               WHERE        (PkSettingNo = 311)) AS DisplayTrans, TVoucherEntry.Remark, MState.StateName, MCity.CityName, MPayType.PayTypeName
							      ,MLedgerDetails_1.address as CustDeliveryAddress,Mcity_1.Cityname as custDeliverycity,tstock.hamali
FROM            TStock INNER JOIN
                         MUOM ON TStock.FkUomNo = MUOM.UOMNo INNER JOIN
                         TVoucherEntry ON TStock.FKVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN
                         MFirm ON TVoucherEntry.CompanyNo = MFirm.FirmNo INNER JOIN
                         MItemMaster ON TStock.ItemNo = MItemMaster.ItemNo INNER JOIN
                         dbo.MStockItems_V(NULL, NULL, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1 ON TStock.ItemNo = MStockItems_V_1.ItemNo INNER JOIN
                         MLedger ON TVoucherEntry.LedgerNo = MLedger.LedgerNo INNER JOIN
                         MLedgerDetails ON MLedgerDetails.LedgerNo = MLedger.LedgerNo  INNER JOIN
                         MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo LEFT OUTER JOIN
                         MTransporterMode ON TVoucherEntry.TransportMode = MTransporterMode.TransModeNo INNER JOIN
                         MState ON MLedger.StateCode = MState.StateCode Left outer JOIN
                         MCity ON MLedgerDetails.CityNo = MCity.CityNo INNER JOIN
                         MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo
						 left outer JOIN
                         TDeliveryAddress ON  TVoucherEntry.PkVoucherNo = TDeliveryAddress.FkVoucherno AND
                          TVoucherEntry.LedgerNo = TDeliveryAddress.Ledgerno
						  Left outer join MLedgerDetails as MLedgerDetails_1
						   on MLedgerDetails_1.Ledgerdetailsno=TDeliveryAddress.Ledgerdetailsno 
						   and TDeliveryAddress.Ledgerno=MLedgerDetails_1.DeliveryLedgerNo
 Left outer join mcity as mcity_1 on MLedgerDetails_1.cityno=mcity_1.cityno
WHERE        (TVoucherEntry.PkVoucherNo = @PkVoucherNo)
-- and MLedgerDetails.ledgerdetailsno 
--in (select min(ledgerdetailsno) as ledgerdetailsno from MLedgerDetails where ledgerno=TVoucherEntry.ledgerno)
ORDER BY TStock.PkStockTrnNo
END



























