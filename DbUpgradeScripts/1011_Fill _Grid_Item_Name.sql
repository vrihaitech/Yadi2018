/*----version 1.0.1.1*/
USE [Yadi2018]
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER FUNCTION [dbo].[MStockItems_V](@Type int, @PItemNo numeric(18),@PGroupNo numeric(18),@PGroupNo1 numeric(18),@PFkDepartmentNo numeric(18),@PFkCategoryNo numeric(18),@PCompanyNo numeric(18))

RETURNS @StockItems TABLE (ItemNo numeric(18),ItemName varchar(max),ItemNameLang nvarchar(max),ItemShortName varchar(max), GroupNo numeric(18),
			   UOMH numeric(18),UOML numeric(18),UOMDefault numeric(18),CompanyNo numeric(18),
			   FkDepartmentNo numeric(18),FkCategoryNo numeric(18),IsActive bit,
			   MinLevel numeric(18),MaxLevel numeric(18),UserId numeric(18),UserDate Datetime,ModifiedBy varchar(Max),ControlUnder numeric(18,2),FactorVal numeric(18,3),CessValue numeric(18,2),PackagingCharges numeric(18,2),ShortCode varchar(50),esflag bit)
AS
Begin 

	Declare @ItemNo numeric(18),@ItemName varchar(max),@ItemNameLang nvarchar(max),@ItemShortName varchar(max), @GroupNo numeric(18),
			@UOMH numeric(18),@UOML numeric(18),@UOMDefault numeric(18),@CompanyNo numeric(18),
			@FkDepartmentNo numeric(18),@FkCategoryNo numeric(18),@IsActive bit,
			@MinLevel numeric(18),@MaxLevel numeric(18),@UserId numeric(18),@UserDate Datetime,@ModifiedBy varchar(Max),
			@ControlUnder numeric(18,0),@FactorVal numeric(18,3),@CessValue numeric(18,2),@PackagingCharges numeric(18,2),@ShortCode varchar(50)
			
			

	set @ItemNo = 0
	set @GroupNo =0 
	set @UOMH = 0
	set @UOMDefault = 0
	set @CompanyNo = 0

	set @FkDepartmentNo = 0
	set @FkCategoryNo = 0
	set @MinLevel = 0
	set @MaxLevel = 0
	set @UserId = 0
	--set @MfgCompNo=0
	set @CessValue =0
	set @PackagingCharges = 0
	
			if(@Type IS NULL)  --If @Type is NULL Values Will Taken From TSalesSetting
			Begin 
					Select  @Type=Cast(SettingValue as numeric(10)) from MSettings Where PKSettingNo=29--TSalesSetting
			End

			if(@Type = 1) -- For Retriving Values Only From MStockItems
			Begin
			Insert into @StockItems Select  ItemNo,Case When(ItemShortName<>'') Then ItemShortName Else ItemName End,Case When(LangShortDesc<>'') Then LangShortDesc Else LangFullDesc End,ItemShortName , GroupNo,UOMH,UOML,UOMDefault,CompanyNo,
					   FkDepartmentNo,FkCategoryNo,IsActive  ,MinLevel,MaxLevel,UserId,UserDate,ModifiedBy,IsNull(ControlUnder,0),IsNull(FactorVal,0),MItemMaster.CessValue,MItemMaster.PackagingCharges,MItemMaster.ShortCode,MItemMaster.esFlag
				From MItemMaster 
				WHERE MItemMaster.ItemNo=Case When @PItemNo is null then MItemMaster.ItemNo else @PItemNo end 
				And MItemMaster.GroupNo=Case When @PGroupNo is null then MItemMaster.GroupNo else @PGroupNo end 
				--And MItemMaster.GroupNo1=Case When @PGroupNo1 is null then MItemMaster.GroupNo1 else @PGroupNo1 end 
				And MItemMaster.FkCategoryNo=Case When @PFkCategoryNo is null then MItemMaster.FkCategoryNo else @PFkCategoryNo end 
			    And MItemMaster.CompanyNo=Case When @PCompanyNo is null then MItemMaster.CompanyNo else @PCompanyNo end 
				And MItemMaster.FkDepartmentNo=Case When @PFkDepartmentNo is null then MItemMaster.FkDepartmentNo else @PFkDepartmentNo end 
				--And MItemMaster.IsActive='true'
			End --@Type = 1

			if(@Type = 2) -- For Retriving Values With GroupNo1_Name + MStockItems
			Begin
				Insert into @StockItems SELECT MItemMaster.ItemNo, MItemGroup.ItemGroupName + ' ' + Case When(ItemShortName<>'') Then ItemShortName Else ItemName End AS ItemName,MItemGroup.LangGroupName + ' ' + Case When(LangShortDesc<>'') Then LangShortDesc Else LangFullDesc End AS ItemNameLang, MItemMaster.ItemShortName, MItemMaster.GroupNo, 
					   MItemMaster.UOMH,MItemMaster.UOML, MItemMaster.UOMDefault, MItemMaster.CompanyNo, MItemMaster.FkDepartmentNo, 
					   MItemMaster.FkCategoryNo, MItemMaster.IsActive, MItemMaster.MinLevel, MItemMaster.MaxLevel, MItemMaster.UserId, 
					   MItemMaster.UserDate, MItemMaster.ModifiedBy,IsNull(MItemMaster.ControlUnder,0),IsNull(MItemMaster.FactorVal,0),MItemMaster.CessValue,MItemMaster.PackagingCharges,MItemMaster.ShortCode,MItemMaster.esFlag
				FROM MItemMaster 
				INNER JOIN MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo
				WHERE MItemMaster.ItemNo=Case When @PItemNo is null then MItemMaster.ItemNo else @PItemNo end
				And MItemMaster.GroupNo=Case When @PGroupNo is null then MItemMaster.GroupNo else @PGroupNo end 
				--And MItemMaster.GroupNo1=Case When @PGroupNo1 is null then MItemMaster.GroupNo1 else @PGroupNo1 end 
				And MItemMaster.FkCategoryNo=Case When @PFkCategoryNo is null then MItemMaster.FkCategoryNo else @PFkCategoryNo end 
			    And MItemMaster.CompanyNo=Case When @PCompanyNo is null then MItemMaster.CompanyNo else @PCompanyNo end 
				--And MItemMaster.IsActive='true'

			End--@Type = 2

			if(@Type=3) -- For Retriving Values With GroupNo2_Name + MStockItems
			Begin
				Insert into @StockItems SELECT MItemMaster.ItemNo,MItemGroup3.ItemGroupName + ' ' +  MItemGroup2.ItemGroupName + ' ' + MItemGroup.ItemGroupName + ' ' + Case When(ItemShortName<>'') Then ItemShortName Else ItemName End AS ItemName,MItemGroup.LangGroupName + ' ' + Case When(LangShortDesc<>'') Then LangShortDesc Else LangFullDesc End AS ItemNameLang, MItemMaster.ItemShortName, MItemMaster.GroupNo, 
					   MItemMaster.UOMH,MItemMaster.UOML, MItemMaster.UOMDefault, MItemMaster.CompanyNo, MItemMaster.FkDepartmentNo, 
					   MItemMaster.FkCategoryNo, MItemMaster.IsActive, MItemMaster.MinLevel, MItemMaster.MaxLevel, MItemMaster.UserId, 
					   MItemMaster.UserDate, MItemMaster.ModifiedBy,IsNull(MItemMaster.ControlUnder,0),IsNull(MItemMaster.FactorVal,0),MItemMaster.CessValue,MItemMaster.PackagingCharges,MItemMaster.ShortCode,MItemMaster.esFlag
				FROM MItemMaster 
				INNER JOIN MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo
	INNER JOIN MItemGroup AS MItemGroup2 ON MItemMaster.FkCategoryNo = MItemGroup2.ItemGroupNo
INNER JOIN MItemGroup AS MItemGroup3 ON MItemMaster.FkDepartmentNo = MItemGroup3.ItemGroupNo
				WHERE MItemMaster.ItemNo=Case When @PItemNo is null then MItemMaster.ItemNo else @PItemNo end
				And MItemMaster.GroupNo=Case When @PGroupNo is null then MItemMaster.GroupNo else @PGroupNo end 
				--And MItemMaster.GroupNo1=Case When @PGroupNo1 is null then MItemMaster.GroupNo1 else @PGroupNo1 end 
				And MItemMaster.FkCategoryNo=Case When @PFkCategoryNo is null then MItemMaster.FkCategoryNo else @PFkCategoryNo end 
			    And MItemMaster.CompanyNo=Case When @PCompanyNo is null then MItemMaster.CompanyNo else @PCompanyNo end 
			    --And MItemMaster.IsActive='true'

			End --@Type = 3

			if(@Type=4)-- For Retriving Values With GroupNo1_Name + GroupNo2_Name  +MStockItems
			Begin
				Insert into @StockItems SELECT MItemMaster.ItemNo, MItemGroup2.ItemGroupName + ' ' +  MItemGroup3.ItemGroupName + ' ' + MItemGroup.ItemGroupName + ' ' + Case When(ItemShortName<>'') Then ItemShortName Else ItemName End AS ItemName,MItemGroup.LangGroupName + ' ' + Case When(LangShortDesc<>'') Then LangShortDesc Else LangFullDesc End AS ItemNameLang, MItemMaster.ItemShortName, MItemMaster.GroupNo, 
					     MItemMaster.UOMH,MItemMaster.UOML,MItemMaster.UOMDefault, MItemMaster.CompanyNo, MItemMaster.FkDepartmentNo, 
					   MItemMaster.FkCategoryNo, MItemMaster.IsActive,  MItemMaster.MinLevel, MItemMaster.MaxLevel, MItemMaster.UserId, 
					   MItemMaster.UserDate, MItemMaster.ModifiedBy,IsNull(MItemMaster.ControlUnder,0),IsNull(MItemMaster.FactorVal,0),MItemMaster.CessValue,MItemMaster.PackagingCharges,MItemMaster.ShortCode,MItemMaster.esFlag
				FROM MItemMaster 
				INNER JOIN MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo
	INNER JOIN MItemGroup AS MItemGroup2 ON MItemMaster.FkCategoryNo = MItemGroup2.ItemGroupNo
INNER JOIN MItemGroup AS MItemGroup3 ON MItemMaster.FkDepartmentNo = MItemGroup3.ItemGroupNo
				--INNER JOIN MItemGroup AS MItemGroup2 ON MItemMaster.GroupNo1 = MItemGroup2.ItemGroupNo
				WHERE MItemMaster.ItemNo=Case When @PItemNo is null then MItemMaster.ItemNo else @PItemNo end
				And MItemMaster.GroupNo=Case When @PGroupNo is null then MItemMaster.GroupNo else @PGroupNo end 
				--And MItemMaster.GroupNo1=Case When @PGroupNo1 is null then MItemMaster.GroupNo1 else @PGroupNo1 end 
				And MItemMaster.FkCategoryNo=Case When @PFkCategoryNo is null then MItemMaster.FkCategoryNo else @PFkCategoryNo end 
			    And MItemMaster.CompanyNo=Case When @PCompanyNo is null then MItemMaster.CompanyNo else @PCompanyNo end 
				--And MItemMaster.IsActive='true'
			End --@Type = 4

			if(@Type=5)-- For Retriving Values With GroupNo1_Name + GroupNo2_Name
			Begin
				Insert into @StockItems SELECT MItemMaster.ItemNo,  MItemGroup.ItemGroupName + ' ' + Case When(ItemShortName<>'') Then ItemShortName Else ItemName End AS ItemName, MItemGroup.LangGroupName + ' ' + Case When(LangShortDesc<>'') Then LangShortDesc Else LangFullDesc End AS ItemNameLang, MItemMaster.ItemShortName, MItemMaster.GroupNo, 
				MItemMaster.UOMH,MItemMaster.UOML, MItemMaster.UOMDefault, MItemMaster.CompanyNo, MItemMaster.FkDepartmentNo, 
				MItemMaster.FkCategoryNo, MItemMaster.IsActive, MItemMaster.MinLevel, MItemMaster.MaxLevel, MItemMaster.UserId, 
				MItemMaster.UserDate, MItemMaster.ModifiedBy,IsNull(MItemMaster.ControlUnder,0),IsNull(MItemMaster.FactorVal,0),MItemMaster.CessValue,MItemMaster.PackagingCharges,MItemMaster.ShortCode,MItemMaster.esFlag
				FROM MItemMaster 
				INNER JOIN MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo
--				INNER JOIN MItemGroup AS MItemGroup2 ON MItemMaster.GroupNo1 = MItemGroup2.ItemGroupNo
				WHERE MItemMaster.ItemNo=Case When @PItemNo is null then MItemMaster.ItemNo else @PItemNo end
				And MItemMaster.GroupNo=Case When @PGroupNo is null then MItemMaster.GroupNo else @PGroupNo end 
				--And MItemMaster.GroupNo1=Case When @PGroupNo1 is null then MItemMaster.GroupNo1 else @PGroupNo1 end 
				And MItemMaster.FkCategoryNo=Case When @PFkCategoryNo is null then MItemMaster.FkCategoryNo else @PFkCategoryNo end 
			    And MItemMaster.CompanyNo=Case When @PCompanyNo is null then MItemMaster.CompanyNo else @PCompanyNo end 
				--And MItemMaster.IsActive='true'

			End --@Type = 5

			if(@Type=6)-- For Retriving Values With GroupNo1_Name + Item Name + GroupNo2_Name
			Begin
				Insert into @StockItems SELECT     MItemMaster.ItemNo, MItemGroup.ItemGroupName + ' ' + Case When(ItemShortName<>'') Then ItemShortName Else ItemName End   AS ItemName,MItemGroup.LangGroupName + ' ' + Case When(LangShortDesc<>'') Then LangShortDesc Else LangFullDesc End   AS ItemNameLang, 
                      MItemMaster.ItemShortName, MItemMaster.GroupNo, MItemMaster.UOMH,MItemMaster.UOML, MItemMaster.UOMDefault, MItemMaster.CompanyNo, 
                     MItemMaster.FkDepartmentNo, MItemMaster.FkCategoryNo, MItemMaster.IsActive, 
                      MItemMaster.MinLevel, MItemMaster.MaxLevel, MItemMaster.UserId, MItemMaster.UserDate, MItemMaster.ModifiedBy,IsNull(MItemMaster.ControlUnder,0),IsNull(MItemMaster.FactorVal,0),MItemMaster.CessValue,MItemMaster.PackagingCharges,MItemMaster.ShortCode,MItemMaster.esFlag
					  FROM         MItemMaster INNER JOIN
                      MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo --INNER JOIN
--                      MItemGroup AS MItemGroup2 ON MItemMaster.GroupNo1 = MItemGroup2.ItemGroupNo
					  WHERE MItemMaster.ItemNo=Case When @PItemNo is null then MItemMaster.ItemNo else @PItemNo end
				    And MItemMaster.GroupNo=Case When @PGroupNo is null then MItemMaster.GroupNo else @PGroupNo end 
					--And MItemMaster.GroupNo1=Case When @PGroupNo1 is null then MItemMaster.GroupNo1 else @PGroupNo1 end 
					And MItemMaster.FkCategoryNo=Case When @PFkCategoryNo is null then MItemMaster.FkCategoryNo else @PFkCategoryNo end 
					And MItemMaster.CompanyNo=Case When @PCompanyNo is null then MItemMaster.CompanyNo else @PCompanyNo end 
					--And MItemMaster.IsActive='true'

			End --@Type = 6

	if(@Type=7)-- For Retriving Values With GroupNo1_Name + Item Name + GroupNo2_Name
			Begin
				Insert into @StockItems SELECT     MItemMaster.ItemNo, MItemGroup.ItemGroupName + ' ' + Case When(ItemShortName<>'') Then ItemShortName Else ItemName End   AS ItemName,MItemGroup.LangGroupName + ' ' + Case When(LangShortDesc<>'') Then LangShortDesc Else LangFullDesc End   AS ItemNameLang, 
                      MItemMaster.ItemShortName, MItemMaster.GroupNo, MItemMaster.UOMH,MItemMaster.UOML, MItemMaster.UOMDefault, MItemMaster.CompanyNo, 
                     MItemMaster.FkDepartmentNo, MItemMaster.FkCategoryNo, MItemMaster.IsActive, 
                      MItemMaster.MinLevel, MItemMaster.MaxLevel, MItemMaster.UserId, MItemMaster.UserDate, MItemMaster.ModifiedBy,IsNull(MItemMaster.ControlUnder,0),IsNull(MItemMaster.FactorVal,0),MItemMaster.CessValue,MItemMaster.PackagingCharges,MItemMaster.ShortCode,MItemMaster.esFlag
					  FROM         MItemMaster INNER JOIN
                      MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo --INNER JOIN
--                      MItemGroup AS MItemGroup2 ON MItemMaster.GroupNo1 = MItemGroup2.ItemGroupNo
					  WHERE MItemMaster.ItemNo=Case When @PItemNo is null then MItemMaster.ItemNo else @PItemNo end
				    And MItemMaster.GroupNo=Case When @PGroupNo is null then MItemMaster.GroupNo else @PGroupNo end 
					--And MItemMaster.GroupNo1=Case When @PGroupNo1 is null then MItemMaster.GroupNo1 else @PGroupNo1 end 
					And MItemMaster.FkCategoryNo=Case When @PFkCategoryNo is null then MItemMaster.FkCategoryNo else @PFkCategoryNo end 
					And MItemMaster.CompanyNo=Case When @PCompanyNo is null then MItemMaster.CompanyNo else @PCompanyNo end 
					--And MItemMaster.IsActive='true'

			End 

	if(@Type=8)-- For Retriving Values With GroupNo1_Name + Item Name + GroupNo2_Name
			Begin
				Insert into @StockItems SELECT     MItemMaster.ItemNo, MItemGroup.ItemGroupName + ' ' + Case When(ItemShortName<>'') Then ItemShortName Else ItemName End   AS ItemName,MItemGroup.LangGroupName + ' ' + Case When(LangShortDesc<>'') Then LangShortDesc Else LangFullDesc End   AS ItemNameLang, 
                      MItemMaster.ItemShortName, MItemMaster.GroupNo, MItemMaster.UOMH,MItemMaster.UOML, MItemMaster.UOMDefault, MItemMaster.CompanyNo, 
                     MItemMaster.FkDepartmentNo, MItemMaster.FkCategoryNo, MItemMaster.IsActive, 
                      MItemMaster.MinLevel, MItemMaster.MaxLevel, MItemMaster.UserId, MItemMaster.UserDate, MItemMaster.ModifiedBy,IsNull(MItemMaster.ControlUnder,0),IsNull(MItemMaster.FactorVal,0),MItemMaster.CessValue,MItemMaster.PackagingCharges,MItemMaster.ShortCode,MItemMaster.esFlag
					  FROM         MItemMaster INNER JOIN
                      MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo --INNER JOIN
--                      MItemGroup AS MItemGroup2 ON MItemMaster.GroupNo1 = MItemGroup2.ItemGroupNo
					  WHERE MItemMaster.ItemNo=Case When @PItemNo is null then MItemMaster.ItemNo else @PItemNo end
				    And MItemMaster.GroupNo=Case When @PGroupNo is null then MItemMaster.GroupNo else @PGroupNo end 
					--And MItemMaster.GroupNo1=Case When @PGroupNo1 is null then MItemMaster.GroupNo1 else @PGroupNo1 end 
					And MItemMaster.FkCategoryNo=Case When @PFkCategoryNo is null then MItemMaster.FkCategoryNo else @PFkCategoryNo end 
					And MItemMaster.CompanyNo=Case When @PCompanyNo is null then MItemMaster.CompanyNo else @PCompanyNo end 
					--And MItemMaster.IsActive='true'

			End 
	
--	Open CurMStockItems
--	fetch CurMStockItems into @ItemNo ,@ItemName ,@ItemShortName , @GroupNo ,@UOMPrimary ,@UOMDefault ,
--							  @CompanyNo ,@GroupNo1 ,@FkDepartmentNo ,@FkCategoryNo ,@IsActive,@IsFixedBarcode,
--							  @MinLevel ,@MaxLevel ,@UserId ,@UserDate ,@ModifiedBy
--		while(@@fetch_status = 0)
--		Begin
--				Insert into @StockItems values (@ItemNo ,@ItemName ,@ItemShortName , @GroupNo ,@UOMPrimary ,@UOMDefault ,
--								  @CompanyNo ,@GroupNo1 ,@FkDepartmentNo ,@FkCategoryNo ,@IsActive,@IsFixedBarcode,
--								  @MinLevel ,@MaxLevel ,@UserId ,@UserDate ,@ModifiedBy)
--
--				fetch CurMStockItems into @ItemNo ,@ItemName ,@ItemShortName , @GroupNo ,@UOMPrimary ,@UOMDefault ,
--								  @CompanyNo ,@GroupNo1 ,@FkDepartmentNo ,@FkCategoryNo ,@IsActive,@IsFixedBarcode,
--								  @MinLevel ,@MaxLevel ,@UserId ,@UserDate ,@ModifiedBy
--		End --@@fetch_status=0
--		Close CurMStockItems Deallocate CurMStockItems
	Return
	End














/*---------------------------------------------------------------------------------------------------------------------------------------------------*/


