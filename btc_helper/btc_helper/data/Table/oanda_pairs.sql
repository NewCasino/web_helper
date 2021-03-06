USE [btce]
GO
/****** 对象:  Table [dbo].[oanda_pairs]    脚本日期: 01/12/2015 15:26:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[oanda_pairs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pair] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[display_name] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[pip] [float] NULL,
	[max_unit] [float] NULL,
 CONSTRAINT [PK_oanda_pair] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
