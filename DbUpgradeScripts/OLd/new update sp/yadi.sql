--a/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
USE [Yadi2018BLANK]
GO
/* Initiate db transaction */
BEGIN TRANSACTION
GO
/* Verify script already applied */
IF NOT EXISTS(select ScriptNo from DBVersionLog where ScriptNo = 1)
BEGIN
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* Insert Setting for Partywise discount */
INSERT INTO [MSettings]([PkSettingNo],[SettingKeyCode],[SettingTypeNo],[SettingValue],[SettingDescription])
     VALUES(370,'S_IsSMSSend',1,'False',NULL);

INSERT INTO [MSettings]([PkSettingNo],[SettingKeyCode],[SettingTypeNo],[SettingValue],[SettingDescription])
     VALUES(371,'S_IsEmailSend',1,'False',NULL);

INSERT INTO [MSettings]([PkSettingNo],[SettingKeyCode],[SettingTypeNo],[SettingValue],[SettingDescription])
     VALUES(372,'S_IsAutoSMSSend',1,'true',NULL);

INSERT INTO [MSettings]([PkSettingNo],[SettingKeyCode],[SettingTypeNo],[SettingValue],[SettingDescription])
     VALUES(373,'S_IsAutoEmailSend',1,'False',NULL);



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

ALTER TABLE dbo.MLedger ADD
	IsSendEmail bit NULL
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
/* Update default value for IsSendEmail */
UPDATE dbo.MLedger SET IsSendEmail = 'False'

/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
--Created on 09/12/2011
ALTER PROCEDURE [dbo].[AddMLedger1]
     @LedgerNo                            numeric(18),
     @LedgerUserNo                        varchar(100),
     @LedgerName                          varchar(250),
     @GroupNo                             numeric(18),
	 @OpeningBalance					  numeric(18,2),
	 @SignCode							  numeric(18),  
     @MaintainBillByBill                  bit,
	 @IsActive                            bit,
     @ContactPerson                       varchar(100),
     @CompanyNo                           numeric(18),
	 @LedgerStatus						  int,
     @IsEnroll                            bit,
     @IsSendSMS                           bit,
     @UserID                              numeric(18),
     @UserDate                            datetime,
     @TransporterNo                       numeric(18),
     @StateCode                           numeric(18),
	 @LedgerLangName					  varchar(100),
     @IsPartyWiseRate                     bit,
     @QuotationRate                       bit,
     @ContactPersonLang                   nvarchar(500),
     @IsSendEmail                         bit,
     @ReturnID							  int output
AS
IF EXISTS(select LedgerNo from MLedger
          where
          LedgerNo = @LedgerNo)
     BEGIN
       --Update existing row
       UPDATE MLedger
       SET
          LedgerUserNo = @LedgerUserNo,
          LedgerName = @LedgerName,
          GroupNo = @GroupNo,
		  OpeningBalance=@OpeningBalance,
		  SignCode=@SignCode,
          MaintainBillByBill = @MaintainBillByBill,
	      IsActive=@IsActive,
          ContactPerson = @ContactPerson,
          CompanyNo = @CompanyNo,
		  LedgerStatus = @LedgerStatus,
          IsEnroll = @IsEnroll,
          IsSendSMS = @IsSendSMS,
          UserID = @UserID,
          UserDate = @UserDate,
		  TransporterNo= @TransporterNo, 
          StateCode=@StateCode,   
          LedgerLangName=@LedgerLangName,
		  IsPartyWiseRate =  @IsPartyWiseRate,
          QuotationRate=  @QuotationRate, 
          ContactPersonLang=@ContactPersonLang,              
          ModifiedBy = isnull(ModifiedBy,'') + cast(@UserID as varchar)+'@'+ CONVERT(VARCHAR(10), GETDATE(), 105),
          StatusNo=2,
          IsSendEmail=@IsSendEmail

       WHERE
          LedgerNo = @LedgerNo
		  set @ReturnID=@LedgerNo

     END
ELSE
     BEGIN
       --Insert new row
       Declare @Id numeric
       SELECT @Id=IsNull(Max(LedgerNo),0) From MLedger
       DBCC CHECKIDENT('MLedger', RESEED, @Id)
	  --For Max Ledger User No 
	  Select @LedgerUserNo=IsNull(Max(Cast(LedgerUserNo as numeric)),0)+1 from MLedger Where GroupNo=@GroupNo

       INSERT INTO MLedger(
          LedgerUserNo,
          LedgerName,
          GroupNo,
		  OpeningBalance,
		  SignCode,
          MaintainBillByBill,
		  IsActive,
          ContactPerson,
          CompanyNo,
		  LedgerStatus,
          IsEnroll,
          IsSendSMS,
          UserID,
          UserDate,
          TransporterNo,
          StateCode,
		  LedgerLangName,
          IsPartyWiseRate,
		  QuotationRate,
          ContactPersonLang,
          StatusNo,
          IsSendEmail
)
       VALUES(
          @LedgerUserNo,
          @LedgerName,
          @GroupNo,
		  @OpeningBalance,
		  @SignCode,
          @MaintainBillByBill,
		  @IsActive,
          @ContactPerson,
          @CompanyNo,
		  @LedgerStatus,
          @IsEnroll,
          @IsSendSMS,
          @UserID,
          @UserDate,
		  @TransporterNo,
		  @StateCode,
	      @LedgerLangName,
          @IsPartyWiseRate,
          @QuotationRate,
          @ContactPersonLang,
          1,
          @IsSendEmail
)
set @ReturnID=Scope_Identity()

END
/*--------------------------------------------------------------------------------------------------------------------------------------------------*/
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
