USE [btce]
GO
/****** 对象:  Table [dbo].[ticker]    脚本日期: 12/26/2014 16:09:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ticker](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[timespan] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[website] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[currency] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[last] [float] NULL,
	[sell] [float] NULL,
	[buy] [float] NULL,
	[high] [float] NULL,
	[low] [float] NULL,
	[vol] [float] NULL,
 CONSTRAINT [PK_ticker] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
