
USE master
GO
IF exists (SELECT * FROM sysdatabases WHERE NAME='praBeers')
BEGIN
ALTER DATABASE praBeers SET SINGLE_USER WITH ROLLBACK IMMEDIATE
DROP DATABASE praBeers
END
GO
CREATE DATABASE praBeers
GO

USE [praBeers]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Beers](
	[ID] [int] IDENTITY(15,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[BeerTypeID] [int] NULL,
	[Alcohol] [real] NULL,
	[Score] [int] NULL,
 CONSTRAINT [PK_Beers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BeerTypes](
	[ID] [int] IDENTITY(9,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_BeerTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Beers]  WITH CHECK ADD  CONSTRAINT [FK_Beers_BeerTypes] FOREIGN KEY([BeerTypeID])
REFERENCES [dbo].[BeerTypes] ([ID])
GO
ALTER TABLE [dbo].[Beers] CHECK CONSTRAINT [FK_Beers_BeerTypes]
GO

SET IDENTITY_INSERT [dbo].[BeerTypes] ON 
INSERT [dbo].[BeerTypes] ([ID], [Type]) VALUES (1, N'Dubbel')
INSERT [dbo].[BeerTypes] ([ID], [Type]) VALUES (2, N'Fruit')
INSERT [dbo].[BeerTypes] ([ID], [Type]) VALUES (3, N'Geuze')
INSERT [dbo].[BeerTypes] ([ID], [Type]) VALUES (4, N'Pale Ale')
INSERT [dbo].[BeerTypes] ([ID], [Type]) VALUES (5, N'Kriek')
INSERT [dbo].[BeerTypes] ([ID], [Type]) VALUES (6, N'Stout')
INSERT [dbo].[BeerTypes] ([ID], [Type]) VALUES (7, N'Tripels')
INSERT [dbo].[BeerTypes] ([ID], [Type]) VALUES (8, N'Wit')
SET IDENTITY_INSERT [dbo].[BeerTypes] OFF

SET IDENTITY_INSERT [dbo].[Beers] ON 

INSERT [dbo].[Beers] ([ID], [Name], [BeerTypeID], [Alcohol], [Score]) VALUES (1, N'Bornem Tripel', 7, 9, 10)
INSERT [dbo].[Beers] ([ID], [Name], [BeerTypeID], [Alcohol], [Score]) VALUES (2, N'Brugge Tripel', 7, 8.7, 5)
INSERT [dbo].[Beers] ([ID], [Name], [BeerTypeID], [Alcohol], [Score]) VALUES (3, N'Corsendonck Agnus', 7, 7.5, 5)
INSERT [dbo].[Beers] ([ID], [Name], [BeerTypeID], [Alcohol], [Score]) VALUES (4, N'Filou', 7, 8.5, 5)
INSERT [dbo].[Beers] ([ID], [Name], [BeerTypeID], [Alcohol], [Score]) VALUES (5, N'Lamme Jan', 7, 9, 5)
INSERT [dbo].[Beers] ([ID], [Name], [BeerTypeID], [Alcohol], [Score]) VALUES (6, N'Stramme Kabouter', 7, 8, 5)
INSERT [dbo].[Beers] ([ID], [Name], [BeerTypeID], [Alcohol], [Score]) VALUES (7, N'Westmalle Dubbel', 1, 7, 1)
INSERT [dbo].[Beers] ([ID], [Name], [BeerTypeID], [Alcohol], [Score]) VALUES (8, N'Slurfke', 1, 8.5, 2)
INSERT [dbo].[Beers] ([ID], [Name], [BeerTypeID], [Alcohol], [Score]) VALUES (9, N'Rochefort 6', 1, 7.5, 5)
INSERT [dbo].[Beers] ([ID], [Name], [BeerTypeID], [Alcohol], [Score]) VALUES (10, N'Nonneke', 1, 8, 3)
INSERT [dbo].[Beers] ([ID], [Name], [BeerTypeID], [Alcohol], [Score]) VALUES (11, N'Chimay Dubbel', 1, 7, 4)
INSERT [dbo].[Beers] ([ID], [Name], [BeerTypeID], [Alcohol], [Score]) VALUES (12, N'Bornem Dubbel', 1, 8, 3)
INSERT [dbo].[Beers] ([ID], [Name], [BeerTypeID], [Alcohol], [Score]) VALUES (13, N'Oude Geuze Boon', 3, 7, 3)
INSERT [dbo].[Beers] ([ID], [Name], [BeerTypeID], [Alcohol], [Score]) VALUES (14, N'Belle Vue Geuze', 3, 5.3, 3)
INSERT [dbo].[Beers] ([ID], [Name], [BeerTypeID], [Alcohol], [Score]) VALUES (15, N'Leffe Bruin', 1, 7.5, 3)
SET IDENTITY_INSERT [dbo].[Beers] OFF



