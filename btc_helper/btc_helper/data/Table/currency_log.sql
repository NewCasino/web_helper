USE [btce]
GO
/****** 对象:  Table [dbo].[currency_log]    脚本日期: 12/26/2014 16:08:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[currency_log](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[timespan] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[website] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[c_from] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[c_to] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[c_rate] [float] NULL,
 CONSTRAINT [PK_currency_log] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
