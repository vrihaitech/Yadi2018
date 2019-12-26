/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
USE [Yadi2018]
GO
/* Initiate db transaction */
BEGIN TRANSACTION
GO
/* Verify script already applied */
IF NOT EXISTS(select ScriptNo from DBVersionLog where ScriptNo = 1)
BEGIN
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* Insert Setting for Partywise discount */
INSERT INTO [MSettings]
           ([PkSettingNo]
           ,[SettingKeyCode]
           ,[SettingTypeNo]
           ,[SettingValue]
           ,[SettingDescription])
     VALUES
           (369
           ,'S_IsPartyWiseDisc'
           ,1
           ,'False'
           ,NULL);

/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* Add CreditDays column in MLedger table */
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON

ALTER TABLE dbo.MLedgerDetails ADD
	CreditDays numeric(18, 0) NULL

/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* Update default value for CreditDays */
UPDATE MLedgerDetails SET CreditDays = 0

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
