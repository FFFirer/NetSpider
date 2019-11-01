USE [FilmData]
GO

/****** Object:  Table [dbo].[Film1905]    Script Date: 2019/11/2 0:27:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Film1905](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FilmId] [int] NULL,
	[Name] [nchar](255) NULL,
	[Country] [nchar](255) NULL,
	[Language] [nchar](255) NULL,
	[FilmType] [nchar](255) NULL,
	[OtherCNFilmName] [nchar](255) NULL,
	[OtherENFilmName] [nchar](255) NULL,
	[PlayTime] [nchar](255) NULL,
	[Color] [nchar](255) NULL,
	[PlayType] [nchar](255) NULL,
	[PlayInfo] [nvarchar](max) NULL,
	[ShootingTime] [nchar](255) NULL,
	[FilmingLocations] [nchar](255) NULL,
	[UserRating] [nchar](255) NULL,
 CONSTRAINT [PK_Film1905] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1905电影网数据库' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Film1905', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电影名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Film1905', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'国别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Film1905', @level2type=N'COLUMN',@level2name=N'Country'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'语言' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Film1905', @level2type=N'COLUMN',@level2name=N'Language'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'影片类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Film1905', @level2type=N'COLUMN',@level2name=N'FilmType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更多中文名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Film1905', @level2type=N'COLUMN',@level2name=N'OtherCNFilmName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更多外文名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Film1905', @level2type=N'COLUMN',@level2name=N'OtherENFilmName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时长' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Film1905', @level2type=N'COLUMN',@level2name=N'PlayTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'色彩' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Film1905', @level2type=N'COLUMN',@level2name=N'Color'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Film1905', @level2type=N'COLUMN',@level2name=N'PlayType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上映信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Film1905', @level2type=N'COLUMN',@level2name=N'PlayInfo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'拍摄时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Film1905', @level2type=N'COLUMN',@level2name=N'ShootingTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户评分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Film1905', @level2type=N'COLUMN',@level2name=N'UserRating'
GO


