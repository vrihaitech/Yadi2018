
GO
/****** Object:  Table [dbo].[MRack]    Script Date: 01/12/2019 18:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRack](
	[RackNo] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[RackName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RackCode] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [bit] NULL,
	[UserID] [numeric](18, 0) NULL,
	[UserDate] [datetime] NULL,
	[CompanyNo] [numeric](18, 0) NULL,
 CONSTRAINT [PK_MRack] PRIMARY KEY CLUSTERED 
(
	[RackNo] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

GO
/****** Object:  Table [dbo].[MRackDetails]    Script Date: 01/12/2019 18:24:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MRackDetails](
	[RackDetailsNo] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[UOMLNo] [numeric](18, 0) NULL,
	[FkRackNo] [numeric](18, 0) NULL,
	[ItemNo] [numeric](18, 0) NULL,
	[ToQty] [numeric](18, 2) NULL,
	[IsActive] [bit] NULL,
	[UserID] [numeric](18, 0) NULL,
	[UserDate] [datetime] NULL,
 CONSTRAINT [PK_MRackDetails] PRIMARY KEY CLUSTERED 
(
	[RackDetailsNo] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


/*---------------------------------------------------------------------------------------------------------------------------------------------------*/


GO
/****** Object:  Table [dbo].[MLedgerGroup]    Script Date: 01/12/2019 18:25:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MLedgerGroup](
	[LedgerGroupNo] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[LedgerName] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LedgerLangName] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GroupNo] [numeric](18, 0) NULL,
	[IsActive] [bit] NULL,
	[CompanyNo] [numeric](18, 0) NULL,
	[UserID] [numeric](18, 0) NULL,
	[UserDate] [datetime] NULL,
 CONSTRAINT [PK_MLedgerGroup] PRIMARY KEY CLUSTERED 
(
	[LedgerGroupNo] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

GO
/****** Object:  Table [dbo].[MLedgerGroupDetails]    Script Date: 01/12/2019 18:25:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MLedgerGroupDetails](
	[LedgerGrpDetailsNo] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[LedgerGroupNo] [numeric](18, 0) NULL,
	[LedgerNo] [numeric](18, 0) NULL,
	[IsActive] [bit] NULL,
	[CompanyNo] [numeric](18, 0) NULL,
	[UserID] [numeric](18, 0) NULL,
	[UserDate] [datetime] NULL,
 CONSTRAINT [PK_MLedgerGroupDetails] PRIMARY KEY CLUSTERED 
(
	[LedgerGrpDetailsNo] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


/*---------------------------------------------------------------------------------------------------------------------------------------------------*/

GO
/****** Object:  Table [dbo].[TDeliveryAddress]    Script Date: 01/21/2019 13:10:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TDeliveryAddress](
	[PkSrNo] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[FkVoucherno] [numeric](18, 0) NULL,
	[Ledgerno] [numeric](18, 0) NULL,
	[LedgerDetailsNo] [numeric](18, 0) NULL,
	[IsActive] [bit] NULL,
	[UserId] [int] NULL,
	[UserDate] [datetime] NULL,
	[ModifiedBy] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StatusNo] [int] NULL,
	[CompanyNo] [numeric](18, 0) NULL,
 CONSTRAINT [PK_TDeliveryAddress] PRIMARY KEY CLUSTERED 
(
	[PkSrNo] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

/*-------------------------------------------------------------------------------------------------------------------------------------------------------*/

GO
/****** Object:  Table [dbo].[TEWayDetails]    Script Date: 01/21/2019 13:11:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TEWayDetails](
	[PKEWayNo] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[FkVoucherNo] [int] NULL,
	[EWayNo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VoucherUserNo] [numeric](18, 0) NULL,
	[EWayDate] [datetime] NULL,
	[ModeNo] [numeric](18, 0) NULL,
	[Distance] [numeric](18, 2) NULL,
	[TransportNo] [numeric](18, 0) NULL,
	[VehicleNo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LRNo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LRDate] [datetime] NULL,
	[LedgerNo] [numeric](18, 0) NULL,
	[LedgerName] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CityNo] [numeric](18, 0) NULL,
	[CityName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PinCode] [numeric](18, 0) NULL,
	[StateCode] [numeric](18, 0) NULL,
	[StateName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UserID] [numeric](18, 0) NULL,
	[UserDate] [datetime] NULL,
	[ModifiedBy] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StatusNo] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_TEWayDetails] PRIMARY KEY CLUSTERED 
(
	[PKEWayNo] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

/*-------------------------------------------------------------------------------------------------------------------------------------------------------*/

GO
/****** Object:  Table [dbo].[MRecipeMain]    Script Date: 01/22/2019 11:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRecipeMain](
	[MRecipeID] [int] IDENTITY(1,1) NOT NULL,
	[DocNo] [int] NULL,
	[ItemType] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GroupNo] [int] NULL,
	[FinishItemID] [int] NULL,
	[PackingSize] [decimal](18, 2) NULL,
	[RDate] [datetime] NULL,
	[Qty] [numeric](18, 2) NULL,
	[ProdQty] [numeric](18, 2) NULL,
	[UomNo] [numeric](18, 0) NULL,
	[RecipeType] [numeric](18, 0) NULL,
	[FkRecipeID] [numeric](18, 0) NULL,
	[IsLock] [bit] NULL,
	[IsActive] [bit] NULL,
	[UserID] [int] NULL,
	[UserDate] [datetime] NULL,
	[ModifiedOn] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MRecipeMain] PRIMARY KEY CLUSTERED 
(
	[MRecipeID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

/****** -------------------------------------------------------------------------------------------******/

GO
/****** Object:  Table [dbo].[MRecipeSub]    Script Date: 01/22/2019 11:36:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MRecipeSub](
	[SRecipeID] [int] IDENTITY(1,1) NOT NULL,
	[FKMRecipeID] [int] NULL,
	[RawGroupNo] [int] NULL,
	[RawProductID] [int] NULL,
	[UomNo] [int] NULL,
	[Qty] [decimal](18, 2) NULL,
	[ProductQty] [numeric](18, 2) NULL,
	[Wastageper] [decimal](18, 2) NULL,
	[WastagePerQty] [decimal](18, 2) NULL,
	[FinalQty] [decimal](18, 2) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_MRecipeSub] PRIMARY KEY CLUSTERED 
(
	[SRecipeID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** -------------------------------------------------------------------------------------------******/
