USE [appeal_db]
GO

/****** Object:  Table [dbo].[Appeal]    Script Date: 12.12.2024 23:34:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Appeal](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](240) NOT NULL,
	[Text] [text] NOT NULL,
	[Creation_date] [date] NOT NULL,
	[Decision_date] [date] NULL,
	[ID_status] [int] NOT NULL,
	[ID_executor] [int] NOT NULL,
	[ID_resident] [int] NOT NULL,
	[ID_plot] [int] NOT NULL,
	[ID_topic] [int] NOT NULL,
	[Grade] [int] NULL,
	[Deletion_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Appeal]  WITH CHECK ADD  CONSTRAINT [FK_Appeal_Executor] FOREIGN KEY([ID_executor])
REFERENCES [dbo].[User] ([ID])
GO

ALTER TABLE [dbo].[Appeal] CHECK CONSTRAINT [FK_Appeal_Executor]
GO

ALTER TABLE [dbo].[Appeal]  WITH CHECK ADD  CONSTRAINT [FK_Appeal_Plot] FOREIGN KEY([ID_plot])
REFERENCES [dbo].[Plot] ([ID])
GO

ALTER TABLE [dbo].[Appeal] CHECK CONSTRAINT [FK_Appeal_Plot]
GO

ALTER TABLE [dbo].[Appeal]  WITH CHECK ADD  CONSTRAINT [FK_Appeal_Resident] FOREIGN KEY([ID_resident])
REFERENCES [dbo].[User] ([ID])
GO

ALTER TABLE [dbo].[Appeal] CHECK CONSTRAINT [FK_Appeal_Resident]
GO

ALTER TABLE [dbo].[Appeal]  WITH CHECK ADD  CONSTRAINT [FK_Appeal_Status] FOREIGN KEY([ID_status])
REFERENCES [dbo].[Status] ([ID])
GO

ALTER TABLE [dbo].[Appeal] CHECK CONSTRAINT [FK_Appeal_Status]
GO

ALTER TABLE [dbo].[Appeal]  WITH CHECK ADD  CONSTRAINT [FK_Appeal_Topic] FOREIGN KEY([ID_topic])
REFERENCES [dbo].[Topic] ([ID])
GO

ALTER TABLE [dbo].[Appeal] CHECK CONSTRAINT [FK_Appeal_Topic]
GO


