USE [btce]
GO
/****** 对象:  Table [dbo].[ticker_log]    脚本日期: 02/02/2015 15:54:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ticker_log](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[timespan] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[website] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[currency] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[last] [float] NULL,
	[buy] [float] NULL,
	[sell] [float] NULL,
	[low] [float] NULL,
	[high] [float] NULL,
	[vol] [float] NULL,
	[time] [float] NULL,
 CONSTRAINT [PK_ticker_log] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
