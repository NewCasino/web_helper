USE [HtmlSelect]
GO
/****** 对象:  Table [dbo].[s_mb_odds]    脚本日期: 06/15/2015 10:20:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[s_mb_odds](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[timespan] [int] NULL,
	[event_id] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[type_id] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[type_name] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[m1] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[m2] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[m3] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[m4] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[m5] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[m6] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[r1] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_mb_odds_r1]  DEFAULT (''),
	[r2] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_mb_odds_r2]  DEFAULT (''),
	[r3] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_mb_odds_r3]  DEFAULT (''),
	[r4] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_mb_odds_r4]  DEFAULT (''),
	[r5] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_mb_odds_r5]  DEFAULT (''),
	[r6] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_mb_odds_r6]  DEFAULT (''),
	[o1] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_mb_odds_o1]  DEFAULT (''),
	[o2] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_mb_odds_o2]  DEFAULT (''),
	[o3] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_mb_odds_o3]  DEFAULT (''),
	[o4] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_mb_odds_o4]  DEFAULT (''),
	[o5] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_mb_odds_o5]  DEFAULT (''),
	[o6] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_mb_odds_o6]  DEFAULT (''),
	[note1] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_mb_odds] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
