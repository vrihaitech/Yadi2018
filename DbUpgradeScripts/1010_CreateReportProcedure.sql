/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

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

/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetStockSummaryAllReport] -- exec [GetStockSummaryAllReport] 1,'01-Apr-2018','01-Jan-2019','49,139,742,659'
@CompNo		           numeric(18),
@FromDate	           datetime,
@ToDate		           datetime,
@ItStr		           varchar(max),
@EsFlag                int
As

SELECT  StockType, ItemNo,sum(PurQty) AS PurQty, sum(SaleQty)  AS SaleQty,sum(PurReturnQty) AS PurReturnQty,sum(SalesReturnQty) AS SalesReturnQty,
sum(OpnOutQty) AS OpnOutQty,sum (OpnInQty) AS OpnInQty,sum (PhyOutQty) AS PhyOutQty,sum (PhyInQty) AS PhyInQty,
ItemGroupName, ItemName, Barcode,UOMH,UOMName_H,UOML,UOMName_L,HSNCode,VoucherDate from
(
--------Opening Stock for Sales---------------

SELECT     0 as StockType,   b.ItemNo, 0 AS PurQty, 
CASe When  MUOM_H.UOMName='KG' and MUOM_L.UOMName='GRAM' and MUOM_S.UOMName!='GRAM'  then SUM(b.Quantity)   
When  MUOM_H.UOMName='KG' and MUOM_L.UOMName='GRAM' and MUOM_S.UOMName='GRAM' then SUM(b.Quantity)/1000   
Else   SUM(b.Quantity*r.stockconversion) End  AS SaleQty,0 AS PurReturnQty,0 AS SalesReturnQty,b.FkUomNo, MUOM_S.UOMName AS UOMName_S,   
MItemGroup.ItemGroupName,MItemMASter.ItemName,MItemMASter.Barcode,MItemMASter.UOMH,MUOM_H.UOMName AS UOMName_H,  
MItemMASter.UOML, MUOM_L.UOMName AS UOMName_L,MItemMASter.HSNCode,VoucherDate,0 AS OpnOutQty,0 AS OpnInQty,0 AS PhyOutQty,0 AS PhyInQty
FROM              TStock AS b INNER JOIN
                           TVoucherEntry AS a ON b.FKVoucherNo = a.PkVoucherNo INNER JOIN
                           MRateSetting AS r ON r.PkSrNo = b.FkRateSettingNo AND b.ItemNo = r.ItemNo INNER JOIN
                           MItemMASter ON b.ItemNo =   MItemMASter.ItemNo INNER JOIN
                           MItemGroup ON   MItemMASter.GroupNo =   MItemGroup.ItemGroupNo INNER JOIN
                           MUOM AS MUOM_H ON   MItemMASter.UOMH = MUOM_H.UOMNo INNER JOIN
                           MUOM AS MUOM_L ON   MItemMASter.UOML = MUOM_L.UOMNo INNER JOIN
                           MUOM AS MUOM_S ON b.FkUomNo = MUOM_S.UOMNo 
INNER JOIN fn_Split (@ItStr,  ',') AS Bill_Itemno on b.ItemNo= CAST(Bill_Itemno.value AS numeric) 
WHERE     a.VoucherDate < @FromDate AND a.IsCancel = 'False' AND a.VoucherTypeCode IN (15)
GROUP BY b.ItemNo, b.FkUomNo,MUOM_S.UOMName,MItemGroup.ItemGroupName,MItemMASter.ItemName,MItemMASter.Barcode,   
MItemMASter.UOMH, MUOM_H.UOMName,MItemMASter.UOML, MUOM_L.UOMName,MItemMASter.HSNCode,VoucherDate

--------Opening Stock Purchase---------------

Union All

SELECT   0 as StockType,  b.ItemNo, CASe When  MUOM_H.UOMName='KG' and MUOM_L.UOMName='GRAM' and MUOM_S.UOMName!='GRAM'  then SUM(b.Quantity)   
When  MUOM_H.UOMName='KG' and MUOM_L.UOMName='GRAM' and MUOM_S.UOMName='GRAM' then SUM(b.Quantity)/1000   
Else   SUM(b.Quantity*r.stockconversion) End AS PurQty, 0 AS SaleQty,0 AS PurReturnQty,0 AS SalesReturnQty,b.FkUomNo, MUOM_S.UOMName AS UOMName_S,   
MItemGroup.ItemGroupName,MItemMASter.ItemName,MItemMASter.Barcode,MItemMASter.UOMH,MUOM_H.UOMName AS UOMName_H,  
MItemMASter.UOML, MUOM_L.UOMName AS UOMName_L,MItemMASter.HSNCode,VoucherDate,0 AS OpnOutQty,0 AS OpnInQty,0 AS PhyOutQty,0 AS PhyInQty
FROM              TStock AS b INNER JOIN
                           TVoucherEntry AS a ON b.FKVoucherNo = a.PkVoucherNo INNER JOIN
                           MRateSetting AS r ON r.PkSrNo = b.FkRateSettingNo AND b.ItemNo = r.ItemNo INNER JOIN
                           MItemMASter ON b.ItemNo =   MItemMASter.ItemNo INNER JOIN
                           MItemGroup ON   MItemMASter.GroupNo =   MItemGroup.ItemGroupNo INNER JOIN
                           MUOM AS MUOM_H ON   MItemMASter.UOMH = MUOM_H.UOMNo INNER JOIN
                           MUOM AS MUOM_L ON   MItemMASter.UOML = MUOM_L.UOMNo INNER JOIN
                           MUOM AS MUOM_S ON b.FkUomNo = MUOM_S.UOMNo
INNER JOIN fn_Split (@ItStr,  ',') AS Bill_Itemno on b.ItemNo= CAST(Bill_Itemno.value AS numeric) 

WHERE    (a.VoucherDate < @FromDate) AND (a.IsCancel = 'False') AND (a.VoucherTypeCode IN (9))
GROUP BY b.ItemNo, b.FkUomNo,MUOM_S.UOMName,MItemGroup.ItemGroupName,MItemMASter.ItemName,MItemMASter.Barcode,   
MItemMASter.UOMH, MUOM_H.UOMName,MItemMASter.UOML, MUOM_L.UOMName,MItemMASter.HSNCode,VoucherDate

--------Opening Stock for Sales Return---------------

Union All

SELECT     0 as StockType,   b.ItemNo, 0 AS PurQty, 
CASe When  MUOM_H.UOMName='KG' and MUOM_L.UOMName='GRAM' and MUOM_S.UOMName!='GRAM'  then SUM(b.Quantity)   
When  MUOM_H.UOMName='KG' and MUOM_L.UOMName='GRAM' and MUOM_S.UOMName='GRAM' then SUM(b.Quantity)/1000   
Else   SUM(b.Quantity*r.stockconversion) End  AS SaleQty,0 AS PurReturnQty,0 AS SalesReturnQty,b.FkUomNo, MUOM_S.UOMName AS UOMName_S,   
MItemGroup.ItemGroupName,MItemMASter.ItemName,MItemMASter.Barcode,MItemMASter.UOMH,MUOM_H.UOMName AS UOMName_H,  
MItemMASter.UOML, MUOM_L.UOMName AS UOMName_L,MItemMASter.HSNCode,VoucherDate,0 AS OpnOutQty,0 AS OpnInQty,0 AS PhyOutQty,0 AS PhyInQty
FROM              TStock AS b INNER JOIN
                           TVoucherEntry AS a ON b.FKVoucherNo = a.PkVoucherNo INNER JOIN
                           MRateSetting AS r ON r.PkSrNo = b.FkRateSettingNo AND b.ItemNo = r.ItemNo INNER JOIN
                           MItemMASter ON b.ItemNo =   MItemMASter.ItemNo INNER JOIN
                           MItemGroup ON   MItemMASter.GroupNo =   MItemGroup.ItemGroupNo INNER JOIN
                           MUOM AS MUOM_H ON   MItemMASter.UOMH = MUOM_H.UOMNo INNER JOIN
                           MUOM AS MUOM_L ON   MItemMASter.UOML = MUOM_L.UOMNo INNER JOIN
                           MUOM AS MUOM_S ON b.FkUomNo = MUOM_S.UOMNo
INNER JOIN fn_Split (@ItStr,  ',') AS Bill_Itemno on b.ItemNo= CAST(Bill_Itemno.value AS numeric) 

WHERE    
     (a.VoucherDate < @FromDate) AND (a.IsCancel = 'False') AND (a.VoucherTypeCode IN (12))
GROUP BY b.ItemNo, b.FkUomNo,MUOM_S.UOMName,MItemGroup.ItemGroupName,MItemMASter.ItemName,MItemMASter.Barcode,   
MItemMASter.UOMH, MUOM_H.UOMName,MItemMASter.UOML, MUOM_L.UOMName,MItemMASter.HSNCode,VoucherDate

--------Opening Stock Purchase Return---------------

Union All

SELECT     0 as StockType,   b.ItemNo, CASe When  MUOM_H.UOMName='KG' and MUOM_L.UOMName='GRAM' and MUOM_S.UOMName!='GRAM'  then SUM(b.Quantity)   
When  MUOM_H.UOMName='KG' and MUOM_L.UOMName='GRAM' and MUOM_S.UOMName='GRAM' then SUM(b.Quantity)/1000   
Else   SUM(b.Quantity*r.stockconversion) End AS PurQty, 0 AS SaleQty,0 AS PurReturnQty,0 AS SalesReturnQty,b.FkUomNo, MUOM_S.UOMName AS UOMName_S,   
MItemGroup.ItemGroupName,MItemMASter.ItemName,MItemMASter.Barcode,MItemMASter.UOMH,MUOM_H.UOMName AS UOMName_H,  
MItemMASter.UOML, MUOM_L.UOMName AS UOMName_L,MItemMASter.HSNCode,VoucherDate,0 AS OpnOutQty,0 AS OpnInQty,0 AS PhyOutQty,0 AS PhyInQty
FROM              TStock AS b INNER JOIN
                           TVoucherEntry AS a ON b.FKVoucherNo = a.PkVoucherNo INNER JOIN
                           MRateSetting AS r ON r.PkSrNo = b.FkRateSettingNo AND b.ItemNo = r.ItemNo INNER JOIN
                           MItemMASter ON b.ItemNo =   MItemMASter.ItemNo INNER JOIN
                           MItemGroup ON   MItemMASter.GroupNo =   MItemGroup.ItemGroupNo INNER JOIN
                           MUOM AS MUOM_H ON   MItemMASter.UOMH = MUOM_H.UOMNo INNER JOIN
                           MUOM AS MUOM_L ON   MItemMASter.UOML = MUOM_L.UOMNo INNER JOIN
                           MUOM AS MUOM_S ON b.FkUomNo = MUOM_S.UOMNo
INNER JOIN fn_Split (@ItStr,  ',') AS Bill_Itemno on b.ItemNo= CAST(Bill_Itemno.value AS numeric) 

WHERE       (a.VoucherDate < @FromDate) AND (a.IsCancel = 'False') AND (a.VoucherTypeCode IN (13))
GROUP BY b.ItemNo, b.FkUomNo,MUOM_S.UOMName,MItemGroup.ItemGroupName,MItemMASter.ItemName,MItemMASter.Barcode,   
MItemMASter.UOMH, MUOM_H.UOMName,MItemMASter.UOML, MUOM_L.UOMName,MItemMASter.HSNCode,VoucherDate

Union All

------------------- Sales -------------------
SELECT   1 as StockType,     b.ItemNo, 0 AS PurQty, 
CASe When  MUOM_H.UOMName='KG' and MUOM_L.UOMName='GRAM' and MUOM_S.UOMName!='GRAM'  then SUM(b.Quantity)   
When  MUOM_H.UOMName='KG' and MUOM_L.UOMName='GRAM' and MUOM_S.UOMName='GRAM' then SUM(b.Quantity)/1000   
Else   SUM(b.Quantity*r.stockconversion) End  AS SaleQty,0 AS PurReturnQty,0 AS SalesReturnQty,b.FkUomNo,MUOM_S.UOMName AS UOMName_S,
MItemGroup.ItemGroupName,MItemMASter.ItemName,MItemMASter.Barcode,MItemMASter.UOMH, MUOM_H.UOMName AS UOMName_H, 
MItemMASter.UOML, MUOM_L.UOMName AS UOMName_L,MItemMASter.HSNCode,VoucherDate,0 AS OpnOutQty,0 AS OpnInQty,0 AS PhyOutQty,0 AS PhyInQty
FROM              TStock AS b INNER JOIN
                           TVoucherEntry AS a ON b.FKVoucherNo = a.PkVoucherNo INNER JOIN
                           MRateSetting AS r ON r.PkSrNo = b.FkRateSettingNo AND b.ItemNo = r.ItemNo INNER JOIN
                           MItemMASter ON b.ItemNo =   MItemMASter.ItemNo INNER JOIN
                           MItemGroup ON   MItemMASter.GroupNo =   MItemGroup.ItemGroupNo INNER JOIN
                           MUOM AS MUOM_H ON   MItemMASter.UOMH = MUOM_H.UOMNo INNER JOIN
                           MUOM AS MUOM_L ON   MItemMASter.UOML = MUOM_L.UOMNo INNER JOIN
                           MUOM AS MUOM_S ON b.FkUomNo = MUOM_S.UOMNo
INNER JOIN fn_Split (@ItStr,  ',') AS Bill_Itemno on b.ItemNo= CAST(Bill_Itemno.value AS numeric) 

WHERE        (a.VoucherDate >= @FromDate) AND (a.VoucherDate <= @ToDate) AND (a.IsCancel = 'False') AND (a.VoucherTypeCode IN (15))
GROUP BY b.ItemNo, b.FkUomNo,MUOM_S.UOMName,   MItemGroup.ItemGroupName,MItemMASter.ItemName,MItemMASter.Barcode,
MItemMASter.UOMH, MUOM_H.UOMName,MItemMASter.UOML, MUOM_L.UOMName,MItemMASter.HSNCode,VoucherDate

Union All

------------------- Pur Return -------------------
SELECT    1 as StockType,     b.ItemNo, 0 AS PurQty, 0 AS SaleQty, 
CASe When  MUOM_H.UOMName='KG' and MUOM_L.UOMName='GRAM' and MUOM_S.UOMName!='GRAM'  then SUM(b.Quantity)   
When  MUOM_H.UOMName='KG' and MUOM_L.UOMName='GRAM' and MUOM_S.UOMName='GRAM' then SUM(b.Quantity)/1000   
Else   SUM(b.Quantity*r.stockconversion) End AS PurReturnQty,0 AS SalesReturnQty,b.FkUomNo,MUOM_S.UOMName AS UOMName_S, 
MItemGroup.ItemGroupName,MItemMASter.ItemName,MItemMASter.Barcode,MItemMASter.UOMH,MUOM_H.UOMName AS UOMName_H, 
MItemMASter.UOML, MUOM_L.UOMName AS UOMName_L,MItemMASter.HSNCode,VoucherDate,0 AS OpnOutQty,0 AS OpnInQty,0 AS PhyOutQty,0 AS PhyInQty
FROM              TStock AS b INNER JOIN
                           TVoucherEntry AS a ON b.FKVoucherNo = a.PkVoucherNo INNER JOIN
                           MRateSetting AS r ON r.PkSrNo = b.FkRateSettingNo AND b.ItemNo = r.ItemNo INNER JOIN
                           MItemMASter ON b.ItemNo =   MItemMASter.ItemNo INNER JOIN
                           MItemGroup ON   MItemMASter.GroupNo =   MItemGroup.ItemGroupNo INNER JOIN
                           MUOM AS MUOM_H ON   MItemMASter.UOMH = MUOM_H.UOMNo INNER JOIN
                           MUOM AS MUOM_L ON   MItemMASter.UOML = MUOM_L.UOMNo INNER JOIN
                           MUOM AS MUOM_S ON b.FkUomNo = MUOM_S.UOMNo
INNER JOIN fn_Split (@ItStr,  ',') AS Bill_Itemno on b.ItemNo= CAST(Bill_Itemno.value AS numeric) 

WHERE     (a.VoucherDate >= @FromDate) AND (a.VoucherDate <= @ToDate) AND (a.IsCancel = 'False') AND (a.VoucherTypeCode IN (13))
GROUP BY b.ItemNo, b.FkUomNo,MUOM_S.UOMName,MItemGroup.ItemGroupName,MItemMASter.ItemName,MItemMASter.Barcode,
MItemMASter.UOMH, MUOM_H.UOMName,MItemMASter.UOML, MUOM_L.UOMName,MItemMASter.HSNCode,VoucherDate

Union All
------------------- PurchASe -------------------
SELECT   1 as StockType, b.ItemNo, CASe When  MUOM_H.UOMName='KG' and MUOM_L.UOMName='GRAM' and MUOM_S.UOMName!='GRAM'  then SUM(b.Quantity)   
When  MUOM_H.UOMName='KG' and MUOM_L.UOMName='GRAM' and MUOM_S.UOMName='GRAM' then SUM(b.Quantity)/1000   
Else   SUM(b.Quantity*r.stockconversion) End AS PurQty, 
0 AS SaleQty,0 AS PurReturnQty, 0 AS SalesReturnQty,b.FkUomNo, MUOM_S.UOMName AS UOMName_S,MItemGroup.ItemGroupName,
MItemMASter.ItemName,MItemMASter.Barcode,MItemMASter.UOMH, MUOM_H.UOMName AS UOMName_H,MItemMASter.UOML,
MUOM_L.UOMName AS UOMName_L,MItemMASter.HSNCode,VoucherDate,0 AS OpnOutQty,0 AS OpnInQty,0 AS PhyOutQty,0 AS PhyInQty
FROM              TStock AS b INNER JOIN
                           TVoucherEntry AS a ON b.FKVoucherNo = a.PkVoucherNo INNER JOIN
                           MRateSetting AS r ON r.PkSrNo = b.FkRateSettingNo AND b.ItemNo = r.ItemNo INNER JOIN
                           MItemMASter ON b.ItemNo =   MItemMASter.ItemNo INNER JOIN
                           MItemGroup ON   MItemMASter.GroupNo =   MItemGroup.ItemGroupNo INNER JOIN
                           MUOM AS MUOM_H ON   MItemMASter.UOMH = MUOM_H.UOMNo INNER JOIN
                           MUOM AS MUOM_L ON   MItemMASter.UOML = MUOM_L.UOMNo INNER JOIN
                           MUOM AS MUOM_S ON b.FkUomNo = MUOM_S.UOMNo
INNER JOIN fn_Split (@ItStr,  ',') AS Bill_Itemno on b.ItemNo= CAST(Bill_Itemno.value AS numeric) 

WHERE     (a.VoucherDate >= @FromDate) AND (a.VoucherDate <= @ToDate) AND (a.IsCancel = 'False') AND (a.VoucherTypeCode IN (9))
GROUP BY b.ItemNo, b.FkUomNo,MUOM_S.UOMName,MItemGroup.ItemGroupName,MItemMASter.ItemName,MItemMASter.Barcode,
MItemMASter.UOMH, MUOM_H.UOMName,MItemMASter.UOML,MUOM_L.UOMName,MItemMASter.HSNCode,VoucherDate

Union All
------------------- Sales Return -------------------
SELECT    1 as StockType,  b.ItemNo, 0 AS PurQty, 0 AS SaleQty,0 AS PurReturnQty, 
CASe When  MUOM_H.UOMName='KG' and MUOM_L.UOMName='GRAM' and MUOM_S.UOMName!='GRAM'  then SUM(b.Quantity)   
When  MUOM_H.UOMName='KG' and MUOM_L.UOMName='GRAM' and MUOM_S.UOMName='GRAM' then SUM(b.Quantity)/1000   
Else   SUM(b.Quantity*r.stockconversion) End AS SalesReturnQty,b.FkUomNo, MUOM_S.UOMName AS UOMName_S,
MItemGroup.ItemGroupName,   MItemMASter.ItemName,MItemMASter.Barcode,MItemMASter.UOMH, MUOM_H.UOMName AS UOMName_H, 
MItemMASter.UOML, MUOM_L.UOMName AS UOMName_L,MItemMASter.HSNCode,VoucherDate,0 AS OpnOutQty,0 AS OpnInQty,0 AS PhyOutQty,0 AS PhyInQty
FROM              TStock AS b INNER JOIN
                           TVoucherEntry AS a ON b.FKVoucherNo = a.PkVoucherNo INNER JOIN
                           MRateSetting AS r ON r.PkSrNo = b.FkRateSettingNo AND b.ItemNo = r.ItemNo INNER JOIN
                           MItemMASter ON b.ItemNo =   MItemMASter.ItemNo INNER JOIN
                           MItemGroup ON   MItemMASter.GroupNo =   MItemGroup.ItemGroupNo INNER JOIN
                           MUOM AS MUOM_H ON   MItemMASter.UOMH = MUOM_H.UOMNo INNER JOIN
                           MUOM AS MUOM_L ON   MItemMASter.UOML = MUOM_L.UOMNo INNER JOIN
                           MUOM AS MUOM_S ON b.FkUomNo = MUOM_S.UOMNo
INNER JOIN fn_Split (@ItStr,  ',') AS Bill_Itemno on b.ItemNo= CAST(Bill_Itemno.value AS numeric) 

WHERE         (a.VoucherDate >= @FromDate) AND (a.VoucherDate <= @ToDate) AND (a.IsCancel = 'False') AND (a.VoucherTypeCode IN (12))
GROUP BY b.ItemNo,b.FkUomNo,MUOM_S.UOMName,MItemGroup.ItemGroupName,MItemMASter.ItemName,MItemMASter.Barcode, 
MItemMASter.UOMH,MUOM_H.UOMName,MItemMASter.UOML, MUOM_L.UOMName,MItemMASter.HSNCode,VoucherDate


--Union All
--
-------------------- Production In -------------------
--SELECT          MItemMASter.ItemNo,0 AS PurQty, 0 AS SaleQty,0 AS PurReturnQty,0 AS SalesReturnQty,   MItemMASter.UOML,  MUOM.UOMName,
--   MItemGroup.ItemGroupName,  MItemMASter.ItemName,  MItemMASter.Barcode,MItemMASter.UOMH, MUOM_H.UOMName AS UOMName_H, 
--   MItemMASter.UOML, MUOM_L.UOMName AS UOMName_L,0 AS ProdOutQty,SUM(  MProduction.Qty) AS ProdInQty
--
--FROM              MItemGroup INNER JOIN
--                           MItemMASter ON   MItemGroup.ItemGroupNo =   MItemMASter.GroupNo INNER JOIN
--                           MProduction ON   MItemMASter.ItemNo =   MProduction.ProductionItemId INNER JOIN
--                           MUOM ON   MItemMASter.UOML =   MUOM.UOMNo  INNER JOIN
--						   MUOM AS MUOM_H ON   MItemMASter.UOMH = MUOM_H.UOMNo INNER JOIN
--                           MUOM AS MUOM_L ON   MItemMASter.UOML = MUOM_L.UOMNo 
--GROUP BY   MItemMASter.ItemNo,   MItemMASter.UOML,   MItemMASter.ItemName,   MItemGroup.ItemGroupName,   MUOM.UOMName,MItemMASter.Barcode,
--  MItemMASter.UOMH, MUOM_H.UOMName,  MItemMASter.UOML, MUOM_L.UOMName
--
--Union All
-------------------- Production Out --------------------
--SELECT            MItemMASter.ItemNo,0 AS PurQty, 0 AS SaleQty,0 AS PurReturnQty,0 AS SalesReturnQty,   MProductionDetails.UOMID,  MUOM.UOMName,
--   MItemGroup.ItemGroupName,  MItemMASter.ItemName,  MItemMASter.Barcode,MItemMASter.UOMH, MUOM_H.UOMName AS UOMName_H,   MItemMASter.UOML, 
-- MUOM_L.UOMName AS UOMName_L,sum(  MProductionDetails.FinalQty) AS ProdOutQty,0 AS ProdInQty
--
--                         
--FROM              MItemGroup INNER JOIN
--                           MItemMASter ON   MItemGroup.ItemGroupNo =   MItemMASter.GroupNo INNER JOIN
--                           MProductionDetails ON   MItemMASter.ItemNo =   MProductionDetails.RawItemID INNER JOIN
--                           MUOM ON   MProductionDetails.UOMID =   MUOM.UOMNo INNER JOIN
--						     MUOM AS MUOM_H ON   MItemMASter.UOMH = MUOM_H.UOMNo INNER JOIN
--                           MUOM AS MUOM_L ON   MItemMASter.UOML = MUOM_L.UOMNo 
--GROUP BY   MItemMASter.ItemNo,   MItemMASter.UOML,   MItemMASter.ItemName,   MItemGroup.ItemGroupName,
--   MProductionDetails.UOMID,   MUOM.UOMName,MItemMASter.Barcode,  MItemMASter.UOMH, MUOM_H.UOMName,  MItemMASter.UOML, MUOM_L.UOMName
--

)AS tbl
GROUP BY StockType, ItemNo,ItemGroupName, ItemName, Barcode, UOMH,UOMName_H,UOML,UOMName_L,HSNCode,VoucherDate
Order BY ItemGroupName, ItemName

/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

--Created ON 10-Dec-2015
create PROCEDURE [dbo].[GetGSTB2BPayTypewise] 
	@FromDate datetime,
	@ToDate datetime
	
	
AS
BEGIN
SELECT    'Cash SAles' as LedgerName, '21' as LedgerNo,SUM(TStock.NetAmount) AS basic, TStock.SGSTPercentage *2 AS taxPer,
 SUM(TStock.SGSTAmount) as cgstamt  , sum(TStock.SGSTAmount) AS sgstamount,     SUM(TStock.Amount) AS Expr2, MPayType.PayTypeName
FROM         TVoucherEntry INNER JOIN
                      TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo INNER JOIN
                      MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo INNER JOIN
                      MLedger ON TVoucherEntry.LedgerNo = MLedger.LedgerNo
WHERE     (TVoucherEntry.VoucherTypeCode = 15) AND (TVoucherEntry.IsCancel = 'false') AND (TVoucherEntry.PayTypeNo = 2) AND (TVoucherEntry.LedgerNo = 21) 
AND (TVoucherEntry.VoucherDate >= @FromDate) AND (TVoucherEntry.VoucherDate <=@ToDate)
GROUP BY TStock.SGSTPercentage, MPayType.PayTypeName

union 
SELECT     MLedger.LedgerName, TVoucherEntry.LedgerNo, SUM(TStock.NetAmount) AS basic, TStock.SGSTPercentage * 2 AS taxPer,
SUM(TStock.SGSTAmount) as cgstamt  , sum(TStock.SGSTAmount) AS sgstamount, 
                      SUM(TStock.Amount) AS Expr2, MPayType.PayTypeName
FROM         TVoucherEntry INNER JOIN
                      TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo INNER JOIN
                      MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo INNER JOIN
                      MLedger ON TVoucherEntry.LedgerNo = MLedger.LedgerNo
WHERE     (TVoucherEntry.VoucherTypeCode = 15) AND (TVoucherEntry.IsCancel = 'false') AND (TVoucherEntry.PayTypeNo = 2) AND (TVoucherEntry.LedgerNo != 21) 
AND (TVoucherEntry.VoucherDate >= @FromDate) AND (TVoucherEntry.VoucherDate <=@ToDate)
GROUP BY TVoucherEntry.LedgerNo, TStock.SGSTPercentage, MPayType.PayTypeName, MLedger.LedgerName
union
SELECT     MLedger.LedgerName, TVoucherEntry.LedgerNo, SUM(TStock.NetAmount) AS basic, TStock.SGSTPercentage * 2 AS taxPer,
SUM(TStock.SGSTAmount) as cgstamt  , sum(TStock.SGSTAmount) AS sgstamount, 
                      SUM(TStock.Amount) AS Expr2, MPayType.PayTypeName
FROM         TVoucherEntry INNER JOIN
                      TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo INNER JOIN
                      MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo INNER JOIN
                      MLedger ON TVoucherEntry.LedgerNo = MLedger.LedgerNo
WHERE     (TVoucherEntry.VoucherTypeCode = 15) AND (TVoucherEntry.IsCancel = 'false') AND (TVoucherEntry.PayTypeNo!= 2)
AND (TVoucherEntry.VoucherDate >= @FromDate) AND (TVoucherEntry.VoucherDate <=@ToDate)
GROUP BY TVoucherEntry.LedgerNo, TStock.SGSTPercentage, MPayType.PayTypeName, MLedger.LedgerName

end

/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetStockAllBrandQty]
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
		set @StrItem='  MItemMaster.GroupNo IN ('+@ItStr+') and  ' 
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

set @StrQry='Select Tbl1.ItemNo,Tbl1.ItemGroupName As ItemName, Sum(OpQty) As OpQty, abs(Sum(InQty)) As InwardQty, abs(Sum(OutQty)) As OutwardQty, 
Sum(OpQty + abs(InQty) - abs(OutQty)) As Quantity,''0'' as Barcode From
(
SELECT    MItemGroup.ItemGroupNo as ItemNo , MItemGroup.ItemGroupName , SUM(CASE WHEN (TStock.TrnCode = 1) THEN isnull(TStock.BilledQuantity, 0) ELSE isnull(TStock.BilledQuantity, 0) * - 1 END) AS OpQty, 0.00 AS InQty, 
                      0.00 AS OutQty
FROM         TVoucherEntry INNER JOIN
                      TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo INNER JOIN
                      MItemMaster ON TStock.ItemNo = MItemMaster.ItemNo INNER JOIN
                      MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo
WHERE   ' + @StrItem + ' (TVoucherEntry.VoucherDate < '''+cast(@FromDate as varchar)+''')  and TVoucherEntry.IsCancel=''false'' ' + @StrVchType + '
GROUP BY MItemGroup.ItemGroupNo , MItemGroup.ItemGroupName 
UNION ALL
SELECT     MItemGroup.ItemGroupNo as ItemNo, MItemGroup.ItemGroupName , 0.00 AS OpQty, SUM(CASE WHEN (TStock.TrnCode = 1) THEN isnull(TStock.BilledQuantity, 0) ELSE 0.00 END) AS InQty, 
                      SUM(CASE WHEN (TStock.TrnCode = 2) THEN isnull(TStock.BilledQuantity, 0) * - 1 ELSE 0.00 END) AS OutQty
FROM         TVoucherEntry INNER JOIN
                      TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo INNER JOIN
                      MItemMaster ON TStock.ItemNo = MItemMaster.ItemNo INNER JOIN
                      MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo
WHERE    ' + @StrItem + ' (TVoucherEntry.VoucherDate >= '''+cast(@FromDate as varchar)+''') AND 
        (TVoucherEntry.VoucherDate <= '''+cast(@ToDate as varchar)+''')  and TVoucherEntry.IsCancel=''false'' ' + @StrVchType + '
GROUP BY MItemGroup.ItemGroupNo, MItemGroup.ItemGroupName 
) As Tbl1 
Group BY Tbl1.ItemNo,Tbl1.ItemGroupName
order by ItemName  '

Exec(@StrQry)


	RETURN

/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[AddMRecipeMain]
 @MRecipeID                  int,
 @DocNo                      int,
 @ItemType                   varchar(50),
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
		ItemType=@ItemType,
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
		ItemType, 
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
		@ItemType,
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
Go

/****** -------------------------------------------------------------------------------------------******/


set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[AddMRecipeSub]
 @SRecipeID             int,
 @FKMRecipeID           int,
 @RawGroupNo            int,
 @RawProductID          int, 
 @Qty                   decimal(18,2),
 @ProductQty            numeric(18,2),
 @UomNo                 int,
 @Wastageper            decimal(18,2),
 @WastagePerQty         decimal(18,2),
 @FinalQty              decimal(18,2),
 @IsActive              bit

AS
IF EXISTS(select SRecipeID from MRecipeSub
          where
          SRecipeID = @SRecipeID) 
     BEGIN
       --Update existing row
       UPDATE MRecipeSub
       SET

        FKMRecipeID=@FKMRecipeID,
		RawGroupNo=@RawGroupNo,
		RawProductID=@RawProductID,
		Qty=@Qty,
		UomNo=@UomNo,
        ProductQty=@ProductQty,
		Wastageper=@Wastageper,
		WastagePerQty=@WastagePerQty,
		FinalQty=@FinalQty, 
        IsActive=@IsActive

       WHERE
          SRecipeID = @SRecipeID 

     END
ELSE
	BEGIN
		   --Insert new row
		   Declare @Id numeric
		   SELECT @Id=IsNull(Max(SRecipeID),0) From MRecipeSub
		   DBCC CHECKIDENT('MRecipeSub', RESEED, @Id)
		   INSERT INTO MRecipeSub
(
			FKMRecipeID, 
			RawGroupNo,
			RawProductID, 
			Qty, 
			UomNo, 
            ProductQty,
			Wastageper, 
			WastagePerQty, 
			FinalQty, 
			IsActive			  
)
VALUES
(
			@FKMRecipeID,
			@RawGroupNo,
			@RawProductID,
			@Qty,
			@UomNo,
            @ProductQty,
			@Wastageper,
			@WastagePerQty,
			@FinalQty,
			@IsActive
)
	END

/****** -------------------------------------------------------------------------------------------******/


/****** -------------------------------------------------------------------------------------------******/






























