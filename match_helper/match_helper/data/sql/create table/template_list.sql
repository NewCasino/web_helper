USE [HtmlSelect]
GO
/****** 对象:  Table [dbo].[template_list]    脚本日期: 11/07/2014 13:47:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[template_list](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[order_num] [int] NULL,
	[template_id] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[type] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[html_path] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[regular_path] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[doc_path] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[redirect_template_id] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[remark] [nvarchar](4000) COLLATE Chinese_PRC_CI_AS NULL,
	[is_doc_id] [nvarchar](10) COLLATE Chinese_PRC_CI_AS NULL,
	[is_use] [nvarchar](10) COLLATE Chinese_PRC_CI_AS NULL,
	[create_time] [datetime] NULL,
	[update_time] [datetime] NULL,
 CONSTRAINT [PK_select_list] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
