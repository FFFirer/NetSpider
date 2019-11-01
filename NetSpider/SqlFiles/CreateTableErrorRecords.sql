USE [FilmData]
GO

/****** Object:  Table [dbo].[ErrorRecords]    Script Date: 2019/11/2 0:30:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ErrorRecords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteCategory] [nvarchar](50) NULL,
	[Url] [nvarchar](50) NULL,
	[StatusCode] [int] NULL,
	[RetryCount] [int] NULL,
	[FilmId] [int] NULL,
 CONSTRAINT [PK_ErrorRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


