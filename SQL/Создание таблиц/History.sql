USE [appeal_db]
GO

/****** Object:  Table [dbo].[History]    Script Date: 12.12.2024 23:35:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[History](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_appeal] [int] NOT NULL,
	[ID_user] [int] NOT NULL,
	[Change_date] [datetime] NOT NULL,
	[Description] [text] NULL,
	[Deletion_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[History]  WITH CHECK ADD  CONSTRAINT [FK_History_Appeal] FOREIGN KEY([ID_appeal])
REFERENCES [dbo].[Appeal] ([ID])
GO

ALTER TABLE [dbo].[History] CHECK CONSTRAINT [FK_History_Appeal]
GO

ALTER TABLE [dbo].[History]  WITH CHECK ADD  CONSTRAINT [FK_History_User] FOREIGN KEY([ID_user])
REFERENCES [dbo].[User] ([ID])
GO

ALTER TABLE [dbo].[History] CHECK CONSTRAINT [FK_History_User]
GO


