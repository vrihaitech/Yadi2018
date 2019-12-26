/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
USE [Yadi2018]
GO
/* Initiate db transaction */
BEGIN TRANSACTION
GO
/* Verify script already applied */
IF NOT EXISTS(select ScriptNo from DBVersionLog where ScriptNo = 9)
BEGIN
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

/* UPDATE AppVersion to 1.0.1.6 */
UPDATE MSetting SET AppVersion = 'rwbztfVKW9E=';
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

/* Insert DBVersionLog   */
INSERT INTO [DBVersionLog]
           ([ScriptNo]
           ,[ScriptDescription])
     VALUES
           (9
           ,'In Item master Gst Slab Option added for Kurlon mattress if gstslab on  and Rate 1000 and gst 12 else 5 per')

/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* End of Verify script already applied */
END
GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* Commit all changes */
COMMIT
GO
