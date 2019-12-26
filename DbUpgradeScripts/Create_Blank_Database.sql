
delete from MLedger where ledgerno>100
delete from MLedgerDetails where ledgerno not in (Select Ledgerno from MLedger)
delete from MRateSetting
delete from MItemMaster
delete from MItemTaxInfo
delete from MItemGroup where ItemGroupNo>10
delete from MLedgerRateSetting
delete fro MUser where usercode<>1
delete from MUserMenuMaster where fkuserid<>1

SELECT 'Delete From '+ TABLE_NAME FROM INFORMATION_SCHEMA.Tables  Where substring(TABLE_NAME,1,1)='T'
SELECT 'DBCC CHECKIDENT ('+ TABLE_NAME +'' +', RESEED, 100);' FROM INFORMATION_SCHEMA.Tables  Where substring(TABLE_NAME,1,5)='MItem' 


UPDATE  dbo.MDutiesTaxesInfo SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MCountry SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MLedgerType SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MAccYear SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MCity SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MUOM SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MRateType SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MNarration SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MBranch SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MPrinter SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MBank SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MFirm SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MPayType SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MLedgerBalances SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MVoucherType SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MOtherBank SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MItemGroup SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MItemMaster SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MState SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MItemTaxSetting SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MItemTaxInfo SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MSubTaxType SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MUserDesignation SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MGroup SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MUserLevel SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MRegion SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MLedgerDetails SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MLedger SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MRateSetting SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MLocation SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MStockLocation SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MStockCategory SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
uPDATE  dbo.MStockGroupType SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MGodownSetting SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MOccupation SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MTransporter SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MQualification SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MItemDiscount SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MArea SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MPayTypeDetails SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MSalesman SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;
UPDATE  dbo.MGodown SET ModifiedBy = NULL, userdate=GetDate(), Userid=1;





