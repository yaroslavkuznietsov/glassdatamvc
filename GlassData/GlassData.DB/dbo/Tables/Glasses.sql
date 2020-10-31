CREATE TABLE [dbo].[Glasses]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[TimeStamp] [datetime] NOT NULL,
	[LinePos] [nvarchar](3) NOT NULL,
	[SourcePos] [nvarchar](3) NOT NULL,
	[SourceSide] [smallint] NOT NULL,
	[GlassId] [nvarchar](10) NOT NULL,
	[GlassHeight] [float] NOT NULL,
	[GlassWidth] [float] NOT NULL,
	[GlassThickness] [float] NOT NULL,
	[GlassWeight] [float] NOT NULL,
	[DestRackPos] [nvarchar](3) NOT NULL,
	[DestRackSide] [nvarchar](3) NOT NULL,
	[PreviousHeight] [float] NOT NULL,
	[PreviousWidth] [float] NOT NULL,
	[GlassResult] [nvarchar](5) NULL
)