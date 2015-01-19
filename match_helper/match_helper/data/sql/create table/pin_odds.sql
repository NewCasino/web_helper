USE [HtmlSelect]
GO
/****** 对象:  Table [dbo].[pin_odds]    脚本日期: 01/19/2015 17:06:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pin_odds](
	[int] [int] IDENTITY(1,1) NOT NULL,
	[timespan] [int] NULL,
	[event_id] [int] NULL,
	[period_type] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[bet_type] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[r1] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[r2] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[r3] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[r4] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[r5] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[r6] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[o1] [float] NULL,
	[o2] [float] NULL,
	[o3] [float] NULL,
	[o4] [float] NULL,
	[o5] [float] NULL,
	[o6] [float] NULL,
 CONSTRAINT [PK_pin_odds] PRIMARY KEY CLUSTERED 
(
	[int] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
