/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
USE [Yadi2018]
GO
/* Initiate db transaction */
BEGIN TRANSACTION
GO
/* Verify script already applied */
IF NOT EXISTS(select ScriptNo from DBVersionLog where ScriptNo = 6)
BEGIN
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

/* UPDATE AppVersion to 1.0.1.4 */
UPDATE MSetting SET AppVersion = 'LMT1Er+CsO8=';
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

/* Insert DBVersionLog   */
INSERT INTO [DBVersionLog]
           ([ScriptNo]
           ,[ScriptDescription])
     VALUES
           (6
           ,'Cash Dnomination& Email Password setting also karnataka font')

/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* End of Verify script already applied */
END
GO
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* Commit all changes */
COMMIT
GO
