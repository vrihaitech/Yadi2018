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


ALTER TABLE dbo.MItemMaster ADD
	ContainerCharges  numeric(18,2) NULL
GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

/* Update default value for ContainerCharges */
UPDATE dbo.MItemMaster SET ContainerCharges = 0
GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

/* Add Weight1,Weight2  column in MRateSetting table */

SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON

ALTER TABLE dbo.MRateSetting ADD
	Weight1  numeric(18,2) NULL,
    Weight2  numeric(18,2) NULL
GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

/* Update default value for Weight1,Weight2*/
UPDATE dbo.MRateSetting SET Weight1 = 0,Weight2 = 0
GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

/* Add CreditDays column in MLedger table */

SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON

ALTER TABLE dbo.MLedger ADD
	IsSendEmail bit NULL
GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

/* Update default value for IsSendEmail */
UPDATE dbo.MLedger SET IsSendEmail = 'False'
GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

/* Add LPPerc,SPPerc column in MRateSetting table */

ALTER TABLE dbo.MRateSetting ADD
	SPPerc numeric(18, 2) NULL
ALTER TABLE dbo.MRateSetting ADD
	LPPerc numeric(18, 2) NULL
GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

/* Update default value for LPPerc,SPPerc*/
update MRateSetting set LPPerc=0.0,SPPerc=0.0
GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON

ALTER TABLE dbo.tstock ADD
	Itype numeric(18, 0) NULL
GO

/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

update tstock set Itype=3
GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
