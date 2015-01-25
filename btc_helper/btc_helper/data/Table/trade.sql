USE [btce]
GO

/****** Object:  Table [dbo].[trade]    Script Date: 2015/1/25 19:58:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[trade](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[timespan] [float] NULL,
	[website] [nvarchar](50) NULL,
	[tid] [float] NULL,
	[time] [float] NULL,
	[price] [float] NULL,
	[amount] [float] NULL,
	[type] [nvarchar](50) NULL,
	[currency] [nvarchar](50) NULL,
	[qty_type] [nvarchar](50) NULL,
 CONSTRAINT [PK_trade] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

