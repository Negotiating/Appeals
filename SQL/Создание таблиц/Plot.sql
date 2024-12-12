USE [appeal_db]
GO

/****** Object:  Table [dbo].[Plot]    Script Date: 12.12.2024 23:35:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Plot](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Organization_ID] [int] NOT NULL,
	[Deletion_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Plot]  WITH CHECK ADD FOREIGN KEY([Organization_ID])
REFERENCES [dbo].[Organization] ([ID])
GO

ALTER TABLE [dbo].[Plot]  WITH CHECK ADD  CONSTRAINT [FK_Plot_Organization] FOREIGN KEY([Organization_ID])
REFERENCES [dbo].[Organization] ([ID])
GO

ALTER TABLE [dbo].[Plot] CHECK CONSTRAINT [FK_Plot_Organization]
GO


