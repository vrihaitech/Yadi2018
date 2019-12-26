Update dbo.MItemGroup set controlgroup=4 where ItemGroupNo=10

Update dbo.MItemGroup set controlgroup=2 where ItemGroupNo=8

Update dbo.MItemMaster set FkDepartmentNo= 10,FkCategoryNo=8

Update MSettings set SettingKeyCode= 'S_IsUseLastPartyWiseDiscEnabled' where PkSettingNo=98

ALTER TABLE MRateSetting ADD CONSTRAINT Stock DEFAULT 0 FOR Stock

ALTER TABLE MRateSetting ADD CONSTRAINT Stock2 DEFAULT 0 FOR Stock2

ALTER TABLE MRateSetting ADD CONSTRAINT Weight1 DEFAULT 0 FOR Weight1

ALTER TABLE MRateSetting ADD CONSTRAINT Weight2 DEFAULT 0 FOR Weight2

ALTER TABLE MRateSetting ADD CONSTRAINT SPPerc DEFAULT 0 FOR SPPerc

ALTER TABLE MRateSetting ADD CONSTRAINT LPPerc DEFAULT 0 FOR LPPerc

Drop table Company 
Drop table Customer
Drop table Itemmast

Truncate table MItemNameDisplayType
