USE [HtmlSelect]
GO
/****** 对象:  Table [dbo].[names]    脚本日期: 11/07/2014 13:46:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[names](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[name_all] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_names] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
