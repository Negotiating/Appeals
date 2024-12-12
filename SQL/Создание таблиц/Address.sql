USE [appeal_db]
GO

/****** Object:  Table [dbo].[Address]    Script Date: 12.12.2024 23:33:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Address](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Plot_ID] [int] NOT NULL,
	[City] [varchar](240) NOT NULL,
	[Street] [varchar](240) NOT NULL,
	[Build] [varchar](10) NOT NULL,
	[Appart] [int] NULL,
	[Deletion_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Plot] FOREIGN KEY([Plot_ID])
REFERENCES [dbo].[Plot] ([ID])
GO

ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Plot]
GO


