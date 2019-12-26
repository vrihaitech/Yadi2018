/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/

USE [Yadi2018]

GO

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO

/* Check DBVersionLog table exist in database or not */

IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'DBVersionLog')
begin
/* Create table  DBVersionLog  */
CREATE TABLE dbo.DBVersionLog
	(
	ScriptNo numeric(18, 0) NOT NULL,
	ScriptDescription text NOT NULL,
	ExecutedOn datetime NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]

ALTER TABLE dbo.DBVersionLog ADD CONSTRAINT
	DF_DBVersionLog_ExecutedOn DEFAULT getDate() FOR ExecutedOn

ALTER TABLE dbo.DBVersionLog ADD CONSTRAINT
	PK_DBVersionLog PRIMARY KEY CLUSTERED 
	(
	ScriptNo
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

end

GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* Commot all changes */
COMMIT
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
