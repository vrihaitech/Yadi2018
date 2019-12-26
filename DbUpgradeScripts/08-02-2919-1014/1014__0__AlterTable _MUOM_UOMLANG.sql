/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* Add ContainerCharges column in MItemMaster table */
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON


ALTER TABLE dbo.MUOM ADD
	UomLang varchar(50) NULL
GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
