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


ALTER TABLE dbo.MRateSetting ADD
	Hamali  numeric(18,2) NULL

ALTER TABLE dbo.MRateSetting ADD CONSTRAINT
	DF_MRateSetting_Hamali DEFAULT 0.0 FOR Hamali

GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

/* Update default value for Hamali*/
UPDATE dbo.MRateSetting SET Hamali = 0
GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON

ALTER TABLE dbo.TStock ADD
	Hamali  numeric(18,2) NULL
GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

/* Update default value for Hamali*/
UPDATE dbo.TStock SET Hamali = 0

ALTER TABLE dbo.TStock ADD CONSTRAINT
	DF_TStock_Hamali DEFAULT 0.0 FOR Hamali
GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

