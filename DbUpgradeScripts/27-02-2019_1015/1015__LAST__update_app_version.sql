/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
USE [Yadi2018]
GO
/* Initiate db transaction */
BEGIN TRANSACTION
GO
/* Verify script already applied */
IF NOT EXISTS(select ScriptNo from DBVersionLog where ScriptNo = 7)
BEGIN
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

/* UPDATE AppVersion to 1.0.1.5 */
UPDATE MSetting SET AppVersion = 'Aaco4ishkXk=';
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

/* Insert DBVersionLog   */
INSERT INTO [DBVersionLog]
           ([ScriptNo]
           ,[ScriptDescription])
     VALUES
           (7
           ,'Hamali added in itemmaster also in tstock')

/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* End of Verify script already applied */
END
GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* Commit all changes */
COMMIT
GO
