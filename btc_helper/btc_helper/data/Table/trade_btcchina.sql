USE [btce]
GO
/****** 对象:  Table [dbo].[trade_btcchina]    脚本日期: 01/05/2015 11:16:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[trade_btcchina](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date] [float] NULL,
	[price] [float] NULL,
	[amount] [float] NULL,
	[tid] [float] NULL,
	[type] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_trade_btcchina] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
