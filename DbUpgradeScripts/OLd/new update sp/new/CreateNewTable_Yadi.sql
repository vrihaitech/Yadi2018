USE [Yadi_Common]
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
USE [Yadi_Common]
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

USE [Yadi_Common]
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
USE [Yadi_Common]
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


/*---------------------------------------------------------------------------------------------------------------------------------------------------*/


/*---------------------------------------------------------------------------------------------------------------------------------------------------*/


/*---------------------------------------------------------------------------------------------------------------------------------------------------*/
