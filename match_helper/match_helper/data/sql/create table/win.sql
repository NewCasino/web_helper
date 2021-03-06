USE [HtmlSelect]
GO
/****** 对象:  Table [dbo].[win]    脚本日期: 11/07/2014 13:47:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[win](
	[timespan] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[website] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[start_time] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[host] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[client] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[give_type] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[three] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[one] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[zero] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
