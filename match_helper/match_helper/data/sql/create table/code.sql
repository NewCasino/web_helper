USE [HtmlSelect]
GO
/****** 对象:  Table [dbo].[code]    脚本日期: 11/07/2014 13:44:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[code](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date] [datetime] NULL,
	[tag] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[code] [nvarchar](4000) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_code] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
