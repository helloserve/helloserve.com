/****** Object:  Table [dbo].[User]    Script Date: 2015-09-08 08:07:49 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE Name = 'User')
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[SellingPoint]    Script Date: 2015-09-08 08:07:49 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE Name = 'SellingPoint')
DROP TABLE [dbo].[SellingPoint]
GO
/****** Object:  Table [dbo].[Requirement]    Script Date: 2015-09-08 08:07:49 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE Name = 'Requirement')
DROP TABLE [dbo].[Requirement]
GO
/****** Object:  Table [dbo].[RelatedLink]    Script Date: 2015-09-08 08:07:49 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE Name = 'RelatedLink')
DROP TABLE [dbo].[RelatedLink]
GO
/****** Object:  Table [dbo].[News]    Script Date: 2015-09-08 08:07:49 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE Name = 'News')
DROP TABLE [dbo].[News]
GO
/****** Object:  Table [dbo].[Media]    Script Date: 2015-09-08 08:07:49 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE Name = 'Media')
DROP TABLE [dbo].[Media]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 2015-09-08 08:07:49 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE Name = 'Log')
DROP TABLE [dbo].[Log]
GO
/****** Object:  Table [dbo].[ForumTopic]    Script Date: 2015-09-08 08:07:49 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE Name = 'ForumTopic')
DROP TABLE [dbo].[ForumTopic]
GO
/****** Object:  Table [dbo].[ForumPost]    Script Date: 2015-09-08 08:07:49 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE Name = 'ForumPost')
DROP TABLE [dbo].[ForumPost]
GO
/****** Object:  Table [dbo].[ForumCategory]    Script Date: 2015-09-08 08:07:49 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE Name = 'ForumCategory')
DROP TABLE [dbo].[ForumCategory]
GO
/****** Object:  Table [dbo].[Forum]    Script Date: 2015-09-08 08:07:49 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE Name = 'Forum')
DROP TABLE [dbo].[Forum]
GO
/****** Object:  Table [dbo].[FeatureRequirement]    Script Date: 2015-09-08 08:07:49 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE Name = 'FeatureRequirement')
DROP TABLE [dbo].[FeatureRequirement]
GO
/****** Object:  Table [dbo].[FeatureMedia]    Script Date: 2015-09-08 08:07:49 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE Name = 'FeatureMedia')
DROP TABLE [dbo].[FeatureMedia]
GO
/****** Object:  Table [dbo].[Feature]    Script Date: 2015-09-08 08:07:49 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE Name = 'Feature')
DROP TABLE [dbo].[Feature]
GO
/****** Object:  Table [dbo].[Error]    Script Date: 2015-09-08 08:07:49 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE Name = 'Error')
DROP TABLE [dbo].[Error]
GO
/****** Object:  Table [dbo].[EdmMetadata]    Script Date: 2015-09-08 08:07:49 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE Name = 'EdmMetadata')
DROP TABLE [dbo].[EdmMetadata]
GO
/****** Object:  Table [dbo].[Downloadable]    Script Date: 2015-09-08 08:07:49 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE Name = 'Downloadable')
DROP TABLE [dbo].[Downloadable]
GO
/****** Object:  Table [dbo].[Downloadable]    Script Date: 2015-09-08 08:07:49 AM ******/




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Downloadable](
	[DownloadableID] [int] IDENTITY(1,1) NOT NULL,
	[FeatureID] [int] NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Location] [nvarchar](max) NULL,
	[ModifiedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DownloadableID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[EdmMetadata]    Script Date: 2015-09-08 08:07:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EdmMetadata](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModelHash] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Error]    Script Date: 2015-09-08 08:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Error](
	[ErrorID] [int] IDENTITY(1,1) NOT NULL,
	[ErrorDate] [datetime] NOT NULL,
	[ErrorType] [int] NOT NULL,
	[Message] [nvarchar](max) NULL,
	[StackTrace] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ErrorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Feature]    Script Date: 2015-09-08 08:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feature](
	[FeatureID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[IsMainFeature] [bit] NOT NULL CONSTRAINT [DF__Feature__IsMainF__3E52440B]  DEFAULT ((0)),
	[Description] [nvarchar](max) NULL,
	[ExtendedDescription] [nvarchar](max) NULL,
	[MediaFolder] [nvarchar](max) NULL,
	[HeaderImageID] [int] NULL,
	[Subdomain] [nvarchar](max) NULL,
	[CustomPage] [nvarchar](max) NULL,
	[IndieDBLink] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Color] [nvarchar](50) NULL,
	[BackgroundColor] [nvarchar](50) NULL,
	[LinkColor] [nvarchar](100) NULL,
	[LinkHoverColor] [nvarchar](100) NULL,
	[HeaderLinkColor] [nvarchar](100) NULL,
	[HeaderLinkHoverColor] [nvarchar](100) NULL,
 CONSTRAINT [PK__Feature__82230A2930F848ED] PRIMARY KEY CLUSTERED 
(
	[FeatureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[FeatureMedia]    Script Date: 2015-09-08 08:07:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeatureMedia](
	[FeatureMediaID] [int] IDENTITY(1,1) NOT NULL,
	[FeatureID] [int] NOT NULL,
	[MediaID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FeatureMediaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[FeatureRequirement]    Script Date: 2015-09-08 08:07:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeatureRequirement](
	[FeatureRequirementID] [int] IDENTITY(1,1) NOT NULL,
	[RequirementID] [int] NOT NULL,
	[FeatureID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FeatureRequirementID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Forum]    Script Date: 2015-09-08 08:07:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forum](
	[ForumID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Internal] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ForumID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[ForumCategory]    Script Date: 2015-09-08 08:07:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ForumCategory](
	[ForumCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[ForumID] [int] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[SortOrder] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ForumCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[ForumPost]    Script Date: 2015-09-08 08:07:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ForumPost](
	[ForumPostID] [int] IDENTITY(1,1) NOT NULL,
	[ForumTopicID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Post] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ForumPostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[ForumTopic]    Script Date: 2015-09-08 08:07:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ForumTopic](
	[ForumTopicID] [int] IDENTITY(1,1) NOT NULL,
	[ForumCategoryID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Sticky] [bit] NOT NULL,
	[Locked] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ForumTopicID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Log]    Script Date: 2015-09-08 08:07:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[Category] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
	[Source] [nvarchar](max) NULL,
	[UserID] [int] NULL,
	[FeatureID] [int] NULL,
	[NewsID] [int] NULL,
	[MediaID] [int] NULL,
	[DownloadID] [int] NULL,
	[Initiated] [datetime] NULL,
	[ElapsedSeconds] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Media]    Script Date: 2015-09-08 08:07:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Media](
	[MediaID] [int] IDENTITY(1,1) NOT NULL,
	[MediaType] [int] NOT NULL,
	[FileName] [nvarchar](max) NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Location] [nvarchar](max) NULL,
	[DateAdded] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MediaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[News]    Script Date: 2015-09-08 08:07:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[NewsID] [int] IDENTITY(1,1) NOT NULL,
	[FeatureID] [int] NULL,
	[Title] [nvarchar](max) NULL,
	[Cut] [nvarchar](max) NULL,
	[Post] [nvarchar](max) NULL,
	[HeaderImageID] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsPublished] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NewsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[RelatedLink]    Script Date: 2015-09-08 08:07:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RelatedLink](
	[RelatedLinkID] [int] IDENTITY(1,1) NOT NULL,
	[FeatureID] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Link] [nvarchar](max) NULL,
	[Icon] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[RelatedLinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Requirement]    Script Date: 2015-09-08 08:07:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Requirement](
	[RequirementID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Link] [nvarchar](max) NULL,
	[Icon] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[RequirementID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SellingPoint]    Script Date: 2015-09-08 08:07:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellingPoint](
	[SellingPointID] [int] IDENTITY(1,1) NOT NULL,
	[FeatureID] [int] NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[SellingPointID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[User]    Script Date: 2015-09-08 08:07:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NULL,
	[Password] [varbinary](max) NULL,
	[EmailAddress] [nvarchar](max) NULL,
	[ReceiveUpdates] [bit] NOT NULL,
	[Administrator] [bit] NOT NULL,
	[ActivationToken] [uniqueidentifier] NOT NULL,
	[Activated] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Downloadable] ON 

INSERT [dbo].[Downloadable] ([DownloadableID], [FeatureID], [Name], [Description], [Location], [ModifiedDate]) VALUES (1, 1, N'Stingray_IndieCade2012.rar', NULL, N'C:\hostingspaces\serve\helloserve.com\data\Downloads\Stingray_IndieCade2012.rar', CAST(N'2012-10-02 07:53:58.457' AS DateTime))
INSERT [dbo].[Downloadable] ([DownloadableID], [FeatureID], [Name], [Description], [Location], [ModifiedDate]) VALUES (2, 3, N'LudumMini_Fear.rar', NULL, N'C:\hostingspaces\serve\helloserve.com\data\Downloads\LudumMini_Fear.rar', CAST(N'2013-01-22 07:07:02.370' AS DateTime))
INSERT [dbo].[Downloadable] ([DownloadableID], [FeatureID], [Name], [Description], [Location], [ModifiedDate]) VALUES (3, 3, N'LudumMini_Fear_Code.rar', NULL, N'C:\hostingspaces\serve\helloserve.com\data\Downloads\LudumMini_Fear_Code.rar', CAST(N'2013-01-22 07:07:09.500' AS DateTime))
INSERT [dbo].[Downloadable] ([DownloadableID], [FeatureID], [Name], [Description], [Location], [ModifiedDate]) VALUES (4, 3, N'SpdToFdm_LD22.rar', NULL, N'C:\hostingspaces\serve\helloserve.com\data\Downloads\SpdToFdm_LD22.rar', CAST(N'2013-01-22 07:07:55.127' AS DateTime))
INSERT [dbo].[Downloadable] ([DownloadableID], [FeatureID], [Name], [Description], [Location], [ModifiedDate]) VALUES (5, 3, N'SpdToFdm_LD22_Code.rar', NULL, N'C:\hostingspaces\serve\helloserve.com\data\Downloads\SpdToFdm_LD22_Code.rar', CAST(N'2013-01-22 07:07:50.933' AS DateTime))
INSERT [dbo].[Downloadable] ([DownloadableID], [FeatureID], [Name], [Description], [Location], [ModifiedDate]) VALUES (6, 5, N'BadaChing.zip', NULL, N'C:\hostingspaces\serve\helloserve.com\data\Downloads\BadaChing.zip', CAST(N'2014-02-20 18:38:52.443' AS DateTime))
SET IDENTITY_INSERT [dbo].[Downloadable] OFF
SET IDENTITY_INSERT [dbo].[Feature] ON 

INSERT [dbo].[Feature] ([FeatureID], [Name], [IsMainFeature], [Description], [ExtendedDescription], [MediaFolder], [HeaderImageID], [Subdomain], [CustomPage], [IndieDBLink], [CreatedDate], [ModifiedDate], [Color], [BackgroundColor], [LinkColor], [LinkHoverColor], [HeaderLinkColor], [HeaderLinkHoverColor]) VALUES (1, N'Stingray Incursion', 0, N'Another XNA project, but I attempted to build a full game this time round, putting the player in control of a highly sophisticated helicopter against an enemy out to defend their assets at all costs!', N'<p>Sometime in 2012, after I completed the <a href="/project/tech-demo">tech demo</a>, I started this project with the intention of building a spiritual successor of the very popular <a href="http://en.wikipedia.org/wiki/Desert_Strike:_Return_to_the_Gulf">Desert Strike</a> game.</p>
<p>I opted for the XNA platform since I''m well versed in C#, and I found it to be a very mature wrapping of the DirectX interfaces. As I found out during development though, that decision came with a few compromises as to available functionality, and ultimately the demise of the project as a whole.</p>
<p>
Today, this project is simply a bunch of screen shots and a single YouTube video. The source code is intact, but because "Games for Windows Live" is now dead, GameStudio (as XNA is known to the layman) is no longer supported and cannot compile or run on versions of Windows later than 7.
</p>
<p>
The only feasible way to revive this project is to port everything over to Visual C++. I have neither the time nor the inclination for such a herculean effort; my last C++ coding was done sometime around 2001.
</p>
<p>
The development diary of this project is still a very interesting read, and everything I learnt doing this made it well worth the effort. Stingray will defintely not be my last project containing realtime 3D visuals.
</p>
<p style="text-align:center"><iframe width="420" height="315" src="http://www.youtube.com/embed/6So_0yg_Lxw" frameborder="0" allowfullscreen></iframe></p>', N'StingrayIncursion', 71, NULL, NULL, N'http://www.indiedb.com/games/stingray-incursion', CAST(N'2012-10-02 00:00:00.000' AS DateTime), CAST(N'2015-04-16 11:10:31.810' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Feature] ([FeatureID], [Name], [IsMainFeature], [Description], [ExtendedDescription], [MediaFolder], [HeaderImageID], [Subdomain], [CustomPage], [IndieDBLink], [CreatedDate], [ModifiedDate], [Color], [BackgroundColor], [LinkColor], [LinkHoverColor], [HeaderLinkColor], [HeaderLinkHoverColor]) VALUES (2, N'Html 5 Features', 0, N'Html 5 Canvas implementations. There''s only one at the moment, but I''ll add more later.', N'<p><a href="/content/canvas/trafficLight" >Traffic Light</a> - a small intersection simulation that I built as a training excersize in about two or three days.</p>
<iframe src="/content/canvas/trafficLight" width=1020, height=1020 style="background-color:white;"/>', NULL, 15, NULL, NULL, NULL, CAST(N'2012-12-11 00:00:00.000' AS DateTime), CAST(N'2015-04-29 07:02:22.153' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Feature] ([FeatureID], [Name], [IsMainFeature], [Description], [ExtendedDescription], [MediaFolder], [HeaderImageID], [Subdomain], [CustomPage], [IndieDBLink], [CreatedDate], [ModifiedDate], [Color], [BackgroundColor], [LinkColor], [LinkHoverColor], [HeaderLinkColor], [HeaderLinkHoverColor]) VALUES (3, N'Ludum Dare', 0, N'Ludum Dare participation is a lot of fun, but also very taxing. Here are my efforts, available for download and including the source.', N'<p>The Ludum Dare events are pretty awesome, and it''s been a worthwhile effort for me to take part. The themed 48 hours (compo) or 72 hours (jam) or even the mini LDs available to you makes for some gross efforts or super brilliant ideas.</p>

<p><b>"Mommy there''s a monster"</b> MiniLD 31 (January 2012) Second submission to LD48. This one forced me to learn animation in XNA within a few hours, and it turned out pretty well. <a href="http://test.helloserve.com/Download/LudumMini_Fear.rar"> Download here to play.</a> Requires .NET 4.0 and XNA 4.0 with DirectX 9c or later.</p>

<p><b>"Speed to Freedom"</b> Ludum Dare 22 (December 2011) My first entry to LD48. Had a ton of fun joining the in-person session with other members of the Cape Town community. Download here to play. Requires .NET 4.0 and XNA 4.0 with DirectX 9c or later.</p>', N'LudumDare', 11, NULL, NULL, NULL, CAST(N'2013-01-14 00:00:00.000' AS DateTime), CAST(N'2013-01-14 00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Feature] ([FeatureID], [Name], [IsMainFeature], [Description], [ExtendedDescription], [MediaFolder], [HeaderImageID], [Subdomain], [CustomPage], [IndieDBLink], [CreatedDate], [ModifiedDate], [Color], [BackgroundColor], [LinkColor], [LinkHoverColor], [HeaderLinkColor], [HeaderLinkHoverColor]) VALUES (4, N'Tech Demo', 0, N'Based on the idea of an orbiter, this demo puts the user in a geo-stationary position around Earth.', N'<p>This feature is only a proof of concept, and as such will serve to illustrate the feasibility of independent developers using the XNA framework. Well, really just to illustrate to me that is, since there are plenty of indie devs around working in XNA already. However since the advent of MDX (Managed Direct X) the framework has evolved extensively and with XNA, the seemingly complicated Direct X interfaces have been wrapped up to the same mature level as any other Visual Studio .NET set of solutions.</p>

<p>To this end, a POC was necessary to establish the ease, speed and flexibity of the framework, and to simply just get myself used to it. Being just myself, this demo contains limited content so as to focus on the tech and the framework specifically. This has proved a huge success. However, in taking the complete globe into account, it was not so much the design that was holding me back, but the loading of the data. There are very many countries in the world, and very many air routes. Capturing this information has been a challenge in itself, and required the construction of a bench app with which to create and manage it, from where it is then fed into the XNA pipeline.</p>', N'TechDemo', 8, NULL, NULL, NULL, CAST(N'2013-01-14 00:00:00.000' AS DateTime), CAST(N'2013-01-14 00:00:00.000' AS DateTime), N'#E6E6E6', N'#2E5CB8', NULL, NULL, NULL, NULL)
INSERT [dbo].[Feature] ([FeatureID], [Name], [IsMainFeature], [Description], [ExtendedDescription], [MediaFolder], [HeaderImageID], [Subdomain], [CustomPage], [IndieDBLink], [CreatedDate], [ModifiedDate], [Color], [BackgroundColor], [LinkColor], [LinkHoverColor], [HeaderLinkColor], [HeaderLinkHoverColor]) VALUES (5, N'BadaChing', 0, N'A personal money manager - currently still under heavy development.', N'<p>This project comes from personal experience; my wife and myself trying to manage our finances and accounts. The premise is to import your bank statements which the application then organizes and groups through a learning-algorithm into ledgers that you define. It''s a direct correlation to how we manage our personal life (ledgers in a cupboard), except it''s digital, scroll-able, searchable and graph-able. Other features include automatically extracting bank fees (where it is possible) into a line item, to give you visibility of where <u>all</u> your money goes.</p>
<p>Development has migrated from a WPF application to a web based solution, and now also supports importing PDF bank statements in addition to the old CSV and OFX type files. This means the user no longer has to manually download any content from on-line banking, and all the information they need to drive this application should arrive in their email: their bank statements.</p>', N'BadaChing', 79, NULL, NULL, NULL, CAST(N'2014-01-19 00:00:00.000' AS DateTime), CAST(N'2015-07-28 12:13:44.847' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Feature] ([FeatureID], [Name], [IsMainFeature], [Description], [ExtendedDescription], [MediaFolder], [HeaderImageID], [Subdomain], [CustomPage], [IndieDBLink], [CreatedDate], [ModifiedDate], [Color], [BackgroundColor], [LinkColor], [LinkHoverColor], [HeaderLinkColor], [HeaderLinkHoverColor]) VALUES (6, N'The Blue Car', 1, N'Affectionally referred to by its colour, this 1991 Eunos Roadster is my first project car, and has a significant bond with our family.', N'<p>Before I even got the car, my wife bought me a workshop manual for it as a wedding gift. And to this day it is both a source of enjoyment and endearment to both of us. I''ve had the car since 2009 now, and in that time I''ve become an amateur mechanic, it''s been my daily driver, we''ve done a road trip to Namibia, and now it''s time for a complete recondition.</p>

<p>There are no shortages of project and restoration posts on various forums across the web featuring an MX-5. Some of them will be very detailed with excellent photos and how-to guides, if you''re into that sort of thing. I won''t do that here because while I enjoy working on a car, I suspect you don''t so much. So instead, while the first few posts are mostly related to the initial restoration process, I''m going to focus on what is probably the most important aspect of motoring for me: the ownership of a car, and in particular my ownership of an old car that need extensive restoration and care. And me being an idiot in the process.</p>

<p>What this car symbolises to me most of all though is family and friendship. The enjoyment we got as a couple from it, the support from my wife in owning it, and in fixing it. I also did not perform any of these tasks by myself. In almost every case I had help (and required help) from friends with more knowledge and experience in either mechanics or electrics. This car would not have been drivable today were it not for them. And it won''t be the last time I''d have to call on them to come and help me, although hopefully my (currently 8 month old) son will be keen to jump in soon.</p>
', NULL, 81, NULL, NULL, NULL, CAST(N'2015-04-16 10:42:33.000' AS DateTime), CAST(N'2015-04-29 06:18:31.560' AS DateTime), NULL, N'#0099FF', N'#0099FF', N'#004C80', N'#000000', N'#004C80')
INSERT [dbo].[Feature] ([FeatureID], [Name], [IsMainFeature], [Description], [ExtendedDescription], [MediaFolder], [HeaderImageID], [Subdomain], [CustomPage], [IndieDBLink], [CreatedDate], [ModifiedDate], [Color], [BackgroundColor], [LinkColor], [LinkHoverColor], [HeaderLinkColor], [HeaderLinkHoverColor]) VALUES (7, N'Simeka Day One', 0, N'Simeka contracted me to build a prototype calculator based on their Excel models. This calculator sports various market specific inputs, assumptions and conditions, all configurable per client. The calculator is now used nationwide by Sanlam.', N'<p>This was a small project which was completed by a small team within in 4 months. The prototype was hugely well received, and has been built out slightly into a more robust, but still very lean, solution. There was continuous discussion and testing between myself and Simeka''s head actuary as to the correctness and long term implementation strategy of the prototype. Specific requirements included reservation for integration points (e.g. member data) in later phases, possibly with-in a bigger SOA architecture.</p>
<p>Current development on this project is geared towards re-use and abstraction as different front-ends and user input options now needs to drive the same calculation model.</p>', NULL, 84, NULL, NULL, NULL, CAST(N'2015-04-28 08:30:09.000' AS DateTime), CAST(N'2015-07-28 12:16:35.573' AS DateTime), NULL, N'#cec2a2', N'#cec2a2', N'#676151', N'#000000', N'#676151')
INSERT [dbo].[Feature] ([FeatureID], [Name], [IsMainFeature], [Description], [ExtendedDescription], [MediaFolder], [HeaderImageID], [Subdomain], [CustomPage], [IndieDBLink], [CreatedDate], [ModifiedDate], [Color], [BackgroundColor], [LinkColor], [LinkHoverColor], [HeaderLinkColor], [HeaderLinkHoverColor]) VALUES (8, N'Counsel Connect', 1, N'This start-up centres around a product offering for the legal fraternity, aimed at productivity, cost savings and leading efforts for industry transformation via digital progress.', N'<p>Counsel Connect was envisaged by an attorney and built by myself. This project is a grass roots initiative, which so far has not required any funding. I built the web platform in my spare time, with the focus on systems automation.</p>
<p> The project is a revolutionary concept in the legal space, challenging current operational maturity and giving new-comers in the profession a competitive leg-up through building their network. It is a secure and full function web application aimed at attorneys and advocates (soon also arbitrators, mediators, facilitators etc). Registration is invitation-only and all users are vetted. Security is a very personal aspect of this project for me. So many sites in South Africa <a href="http://www.wantitall.co.za/">are not</a>, and the average user does not understand the impact, or actually even care. This is something that I want to change with Counsel Connect, and we harp on about it quite a lot in the news posts on that site. On a side-note: the recent spate of vulnerabilities with regards to SSL and general security on the web is unfortunate, but is no reason to not be vigilante about your own site''s security. At this point, SSL is all we have available to the general browsing public.</p>
<p>Current challenges are UI design, feature extension and platform adoption. Specifically development related, the front-page needs to be redesigned completely for a start, but for now I''m focused on extending the platform to include the additional set of user types. Platform adoption has been very slow. This is not specifically related to Counsel Connect however, but more generally to IT and cloud-based or internet-based technologies; individuals in the industry still prefer to register on a print form, for example.</p>
<p>More information is available at <a href="https://www.counselconnect.co.za">the official Counsel Connect site</a>.', NULL, 83, NULL, NULL, NULL, CAST(N'2015-04-28 09:04:17.000' AS DateTime), CAST(N'2015-04-29 06:51:37.040' AS DateTime), NULL, N'#6cafcb', N'#2B4651', N'#568CA2', N'#000000', N'#2B4651')
SET IDENTITY_INSERT [dbo].[Feature] OFF
SET IDENTITY_INSERT [dbo].[FeatureMedia] ON 

INSERT [dbo].[FeatureMedia] ([FeatureMediaID], [FeatureID], [MediaID]) VALUES (1, 1, 1)
INSERT [dbo].[FeatureMedia] ([FeatureMediaID], [FeatureID], [MediaID]) VALUES (2, 1, 2)
INSERT [dbo].[FeatureMedia] ([FeatureMediaID], [FeatureID], [MediaID]) VALUES (3, 1, 3)
INSERT [dbo].[FeatureMedia] ([FeatureMediaID], [FeatureID], [MediaID]) VALUES (4, 1, 4)
INSERT [dbo].[FeatureMedia] ([FeatureMediaID], [FeatureID], [MediaID]) VALUES (5, 1, 5)
INSERT [dbo].[FeatureMedia] ([FeatureMediaID], [FeatureID], [MediaID]) VALUES (6, 1, 6)
INSERT [dbo].[FeatureMedia] ([FeatureMediaID], [FeatureID], [MediaID]) VALUES (7, 4, 9)
INSERT [dbo].[FeatureMedia] ([FeatureMediaID], [FeatureID], [MediaID]) VALUES (8, 4, 10)
INSERT [dbo].[FeatureMedia] ([FeatureMediaID], [FeatureID], [MediaID]) VALUES (9, 3, 12)
INSERT [dbo].[FeatureMedia] ([FeatureMediaID], [FeatureID], [MediaID]) VALUES (10, 3, 13)
INSERT [dbo].[FeatureMedia] ([FeatureMediaID], [FeatureID], [MediaID]) VALUES (11, 3, 14)
SET IDENTITY_INSERT [dbo].[FeatureMedia] OFF
SET IDENTITY_INSERT [dbo].[FeatureRequirement] ON 

INSERT [dbo].[FeatureRequirement] ([FeatureRequirementID], [RequirementID], [FeatureID]) VALUES (1, 2, 1)
INSERT [dbo].[FeatureRequirement] ([FeatureRequirementID], [RequirementID], [FeatureID]) VALUES (2, 1, 1)
INSERT [dbo].[FeatureRequirement] ([FeatureRequirementID], [RequirementID], [FeatureID]) VALUES (3, 3, 1)
INSERT [dbo].[FeatureRequirement] ([FeatureRequirementID], [RequirementID], [FeatureID]) VALUES (4, 4, 1)
INSERT [dbo].[FeatureRequirement] ([FeatureRequirementID], [RequirementID], [FeatureID]) VALUES (5, 5, 5)
SET IDENTITY_INSERT [dbo].[FeatureRequirement] OFF
SET IDENTITY_INSERT [dbo].[Forum] ON 

INSERT [dbo].[Forum] ([ForumID], [Name], [Description], [Internal]) VALUES (1, N'helloserve Productions', N'Internal company stuff...', 0)
INSERT [dbo].[Forum] ([ForumID], [Name], [Description], [Internal]) VALUES (2, N'Stingray Incursion', N'Post anything you like related to our first feature title.', 0)
SET IDENTITY_INSERT [dbo].[Forum] OFF
SET IDENTITY_INSERT [dbo].[ForumCategory] ON 

INSERT [dbo].[ForumCategory] ([ForumCategoryID], [ForumID], [Name], [Description], [SortOrder]) VALUES (1, 1, N'Website Related', NULL, 0)
INSERT [dbo].[ForumCategory] ([ForumCategoryID], [ForumID], [Name], [Description], [SortOrder]) VALUES (2, 1, N'General Discussions', NULL, 1)
INSERT [dbo].[ForumCategory] ([ForumCategoryID], [ForumID], [Name], [Description], [SortOrder]) VALUES (3, 2, N'Gameplay', N'Share your incursion experiences', 0)
INSERT [dbo].[ForumCategory] ([ForumCategoryID], [ForumID], [Name], [Description], [SortOrder]) VALUES (4, 2, N'Issues and Support', N'Problems and solutions.', 1)
SET IDENTITY_INSERT [dbo].[ForumCategory] OFF
SET IDENTITY_INSERT [dbo].[ForumPost] ON 

INSERT [dbo].[ForumPost] ([ForumPostID], [ForumTopicID], [UserID], [Date], [Post]) VALUES (1, 1, 1, CAST(N'2012-10-02 01:20:23.357' AS DateTime), N'Please log issues experienced with the site.

Remember, include as much detail as possible. A good error is only worth as much as the story...')
INSERT [dbo].[ForumPost] ([ForumPostID], [ForumTopicID], [UserID], [Date], [Post]) VALUES (2, 2, 2, CAST(N'2012-11-09 05:10:28.697' AS DateTime), N'So ... I wanted to post a picture of a guy stealing a cookie from the cookie jar but I can''t use image tags, so for now you peeps will just have to pretend, and instead i''ll fail by posting some ascii.

       _==/          i     i          \==_
     /XX/            |\___/|            \XX\' + N'
   /XXXX\            |XXXXX|            /XXXX\' + N'
  |XXXXXX\_         _XXXXXXX_         _/XXXXXX|
 XXXXXXXXXXXxxxxxxxXXXXXXXXXXXxxxxxxxXXXXXXXXXXX
|XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX|
XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
|XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX|
 XXXXXX/^^^^"\XXXXXXXXXXXXXXXXXXXXX/^^^^^\XXXXXX
  |XXX|       \XXX/^^\XXXXX/^^\XXX/       |XXX|
    \XX\       \X/    \XXX/    \X/       /XX/
       "\       "      \X/      "      /"')
INSERT [dbo].[ForumPost] ([ForumPostID], [ForumTopicID], [UserID], [Date], [Post]) VALUES (3, 2, 1, CAST(N'2012-11-12 01:26:03.213' AS DateTime), N'Aaait! Fixed it!

[img]http://www.technama.com/wp-content/uploads/2011/01/1167.jpg[/img]

How''s that?')
INSERT [dbo].[ForumPost] ([ForumPostID], [ForumTopicID], [UserID], [Date], [Post]) VALUES (4, 1, 1, CAST(N'2012-12-18 12:24:06.120' AS DateTime), N'Site update was completed at 22:21 CAT on 18/12/2012.
User Account migration resulted in account locks.

If you''re having trouble logging into the site, request a password reset, or let me know if that doesn''t work.')
INSERT [dbo].[ForumPost] ([ForumPostID], [ForumTopicID], [UserID], [Date], [Post]) VALUES (5, 2, 1, CAST(N'2013-01-16 05:15:12.003' AS DateTime), N'I''d like to share another image with you all:
[img]http://underscorediscovery.com/artaday/2013/13-Metal-Study.png[/img]')
SET IDENTITY_INSERT [dbo].[ForumPost] OFF
SET IDENTITY_INSERT [dbo].[ForumTopic] ON 

INSERT [dbo].[ForumTopic] ([ForumTopicID], [ForumCategoryID], [UserID], [Name], [Date], [Sticky], [Locked]) VALUES (1, 1, 1, N'Errors and Issues FAQ', CAST(N'2012-10-02 01:20:23.283' AS DateTime), 1, 1)
INSERT [dbo].[ForumTopic] ([ForumTopicID], [ForumCategoryID], [UserID], [Name], [Date], [Sticky], [Locked]) VALUES (2, 2, 2, N'The admin made me do it.', CAST(N'2012-11-09 05:10:28.667' AS DateTime), 0, 0)
SET IDENTITY_INSERT [dbo].[ForumTopic] OFF
SET IDENTITY_INSERT [dbo].[Media] ON 

INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (1, 1, N'Stingray_2012-04-29 09-09-30-45.png', 2000, 1250, N'StingrayIncursion\Stingray_2012-04-29 09-09-30-45.png', CAST(N'2013-01-14 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (2, 1, N'Stingray_2012-04-29 09-10-00-45.png', 2000, 1250, N'StingrayIncursion\Stingray_2012-04-29 09-10-00-45.png', CAST(N'2013-01-14 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (3, 1, N'Stingray_2012-04-29 09-11-10-45.png', 2000, 1250, N'StingrayIncursion\Stingray_2012-04-29 09-11-10-45.png', CAST(N'2013-01-14 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (4, 1, N'Stingray_2012-04-29 09-16-39-98.png', 2000, 1250, N'StingrayIncursion\Stingray_2012-04-29 09-16-39-98.png', CAST(N'2013-01-14 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (5, 1, N'Stingray_2012-04-29 09-17-09-99.png', 2000, 1250, N'StingrayIncursion\Stingray_2012-04-29 09-17-09-99.png', CAST(N'2013-01-14 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (6, 1, N'Stingray_2012-04-29 09-17-19-99.png', 2000, 1250, N'StingrayIncursion\Stingray_2012-04-29 09-17-19-99.png', CAST(N'2013-01-14 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (8, 1, N'TechDemo_FeatureHeader.jpg', 300, 150, N'TechDemo_FeatureHeader.jpg', CAST(N'2013-01-14 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (9, 1, N'Screen001.jpg', 1440, 900, N'TechDemo\Screen001.jpg', CAST(N'2013-01-14 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (10, 1, N'Screen002.jpg', 1440, 900, N'TechDemo\Screen002.jpg', CAST(N'2013-01-14 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (11, 1, N'LudumDare_FeatureHeader.png', 300, 150, N'LudumDare_FeatureHeader.png', CAST(N'2013-01-14 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (12, 1, N'22_Screen01.jpg', 1020, 639, N'LudumDare\22_Screen01.jpg', CAST(N'2013-01-14 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (13, 1, N'22_TitleScreen_Med.png', 1020, 638, N'LudumDare\22_TitleScreen_Med.png', CAST(N'2013-01-14 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (14, 1, N'LudumMini_Fear_ss.png', 1017, 634, N'LudumDare\LudumMini_Fear_ss.png', CAST(N'2013-01-14 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (15, 1, N'html5_FeatureHeader.jpg', 300, 150, N'html5_FeatureHeader.jpg', CAST(N'2013-01-14 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (16, 1, N'AboveCliffs.jpg', 1040, 678, N'AboveCliffs.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (17, 1, N'crappyshadowmap.jpg', 636, 396, N'crappyshadowmap.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (18, 1, N'EnvironmentNormalMap_01.png', 1533, 859, N'EnvironmentNormalMap_01.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (19, 1, N'EnvironmentNormalMap_02.png', 1530, 859, N'EnvironmentNormalMap_02.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (20, 1, N'ForwardBase.png', 1536, 864, N'ForwardBase.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (21, 1, N'Gun.png', 1532, 860, N'Gun.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (22, 1, N'HeavyGuardTower.jpg', 762, 435, N'HeavyGuardTower.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (23, 1, N'HUDEffectDay_U.png', 1000, 562, N'HUDEffectDay_U.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (24, 1, N'HUDEffectNight_U.png', 1000, 561, N'HUDEffectNight_U.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (25, 1, N'IncorrectClipping.jpg', 606, 295, N'IncorrectClipping.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (26, 1, N'LevelEditorManual.jpg', 800, 478, N'LevelEditorManual.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (27, 1, N'LinkingTiles.jpg', 720, 430, N'LinkingTiles.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (28, 1, N'Mine_combined_01.jpg', 1024, 554, N'Mine_combined_01.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (29, 1, N'MissingTile01.jpg', 764, 433, N'MissingTile01.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (30, 1, N'MissingTiles02.jpg', 764, 436, N'MissingTiles02.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (31, 1, N'Night Battle 2_U.png', 1000, 560, N'Night Battle 2_U.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (32, 1, N'Night Battle_U.png', 1000, 563, N'Night Battle_U.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (33, 1, N'NightBattleTracers_U.png', 1000, 563, N'NightBattleTracers_U.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (34, 1, N'NoiseMap.jpg', 612, 470, N'NoiseMap.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (35, 1, N'OceanView.jpg', 1040, 678, N'OceanView.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (36, 1, N'PalmLOD.jpg', 640, 480, N'PalmLOD.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (37, 1, N'palmtreeAlpha.jpg', 945, 571, N'palmtreeAlpha.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (38, 1, N'PreSmoke.png', 1024, 640, N'PreSmoke.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (39, 1, N'ReconMapped.jpg', 1024, 465, N'ReconMapped.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (40, 1, N'ReconVehicle.jpg', 1024, 465, N'ReconVehicle.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (41, 1, N'RocketFire.png', 1024, 640, N'RocketFire.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (42, 1, N'RocketHit.png', 1024, 640, N'RocketHit.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (43, 1, N'ShadowMapDepthbuffer.jpg', 506, 507, N'ShadowMapDepthbuffer.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (44, 1, N'ShootingBack.png', 1536, 864, N'ShootingBack.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (45, 1, N'skyboxDawn.jpg', 648, 419, N'skyboxDawn.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (46, 1, N'skyboxLateEve.jpg', 648, 419, N'skyboxLateEve.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (47, 1, N'skyboxNoon.jpg', 648, 419, N'skyboxNoon.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (48, 1, N'Smoke01.png', 1024, 640, N'Smoke01.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (49, 1, N'Smoke02.png', 1024, 640, N'Smoke02.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (50, 1, N'Smoke03.png', 1024, 640, N'Smoke03.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (51, 1, N'SmokeAndDust.jpg', 1022, 638, N'SmokeAndDust.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (52, 1, N'speedgraph01.jpg', 500, 300, N'speedgraph01.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (53, 1, N'speedgraph02.jpg', 500, 300, N'speedgraph02.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (54, 1, N'speedgraph03.jpg', 500, 300, N'speedgraph03.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (55, 1, N'stingray01.jpg', 800, 500, N'stingray01.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (56, 1, N'StingrayGunAni.png', 769, 410, N'StingrayGunAni.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (57, 1, N'stingrayrender4.jpg', 1024, 768, N'stingrayrender4.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (58, 1, N'StingrayRender8.jpg', 640, 480, N'StingrayRender8.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (60, 1, N'SystemsMap.png', 881, 794, N'SystemsMap.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (61, 1, N'TestingLinks.jpg', 720, 430, N'TestingLinks.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (62, 1, N'TheRealStingray.jpg', 1040, 678, N'TheRealStingray.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (63, 1, N'TheRealStingray02.jpg', 1040, 678, N'TheRealStingray02.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (64, 1, N'VehicleSlope.png', 1024, 640, N'VehicleSlope.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (65, 1, N'VehicleSlopeCalc.jpg', 500, 631, N'VehicleSlopeCalc.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (66, 1, N'WaterShader.png', 1000, 562, N'WaterShader.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (67, 1, N'WaterShader_Coastline.png', 1000, 561, N'WaterShader_Coastline.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (68, 1, N'WithRecon.jpg', 1040, 678, N'WithRecon.jpg', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (69, 1, N'ZCliffs.png', 1000, 549, N'ZCliffs.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (70, 1, N'ZCliffs02.png', 1000, 492, N'ZCliffs02.png', CAST(N'2013-01-17 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (71, 1, N'Stingray_FeatureHeader.png', 605, 250, N'Stingray_FeatureHeader.png', CAST(N'2013-01-21 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (72, 1, N'bigexplosion.jpg', 1000, 578, N'bigexplosion.jpg', CAST(N'2013-01-21 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (73, 1, N'fireworks_live.jpg', 1000, 482, N'fireworks_live.jpg', CAST(N'2013-01-21 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (74, 1, N'wallpaper-3-Strike-Suit-Zero.jpg', 1920, 1200, N'wallpaper-3-Strike-Suit-Zero.jpg', CAST(N'2013-01-28 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (75, 1, N'construction.jpg', 480, 360, N'construction.jpg', CAST(N'2013-01-30 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (76, 1, N'xna.jpg', 301, 167, N'xna.jpg', CAST(N'2013-02-03 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (78, 1, N'presentation.jpg', 300, 150, N'presentation.jpg', CAST(N'2013-10-23 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (79, 1, N'BadaChing_FeatureHeader.png', 300, 150, N'BadaChing_FeatureHeader.png', CAST(N'2014-01-23 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (80, 1, N'BadaChing_Feature.png', 605, 250, N'BadaChing\BadaChing_Feature.png', CAST(N'2014-01-23 00:00:00.000' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (81, 1, N'bloukar_header.jpg', 704, 528, N'bloukar_header.jpg', CAST(N'2015-04-16 10:37:00.077' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (82, 1, N'Stingray_FeatureHeader.jpg', 2000, 1250, N'Stingray_FeatureHeader.jpg', CAST(N'2015-04-16 10:37:00.647' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (83, 1, N'cccPoster.jpg', 1240, 781, N'cccPoster.jpg', CAST(N'2015-04-28 08:19:06.323' AS DateTime))
INSERT [dbo].[Media] ([MediaID], [MediaType], [FileName], [Width], [Height], [Location], [DateAdded]) VALUES (84, 1, N'DayOne.jpg', 1063, 602, N'DayOne.jpg', CAST(N'2015-04-28 08:19:07.573' AS DateTime))
SET IDENTITY_INSERT [dbo].[Media] OFF
SET IDENTITY_INSERT [dbo].[News] ON 

INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (1, NULL, N'Testers, Destroy!', N'So the new site is in "Consumer Preview" state, what ever that means.
', N'This means that any problems you experience, let me know so that I can fix it. There''s a forum category for that specifically.

Post it anywhere else, and you suck.', NULL, CAST(N'2012-10-02 00:00:00.000' AS DateTime), CAST(N'2012-10-02 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (2, NULL, N'Test Site Update', N'The recent update brings performance, activity logs and a SQL Server back-end...', N'<p>I started building the logging module bespoke for Alacrity since I found that <a href="http://logging.apache.org/log4net/">Log4Net</a> doesn''t serve my purposes all that well. So I needed a test implementation, and where better than my own site :) That, together with moving to SQL Server from a SQL Compact database required some scripting to JSON to preserve the database contents during the migration. But all''s well. Features, News and Forum posts are all intact.</p>

<p>The only problem is that with migrating the user accounts across, the password information is necessarily lost. If you''re having problems logging in, request a password reset. That should set you right.</p>

<p>Other things that''s been added is the availability of image tags in the forum posts. So, go wild. I do not support hosting your images though...</p>', NULL, CAST(N'2012-12-18 00:00:00.000' AS DateTime), CAST(N'2012-12-18 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (3, NULL, N'Simulate all the things', N'So for a while there everyone was making fun at <a href="http://www.farming-simulator.com/">Farming Simulator</a>. What else can they do with that engine?', N'<p>It was kalahari.com''s best selling PC game for a while too. That maybe says more about South Africa than we''d like to admit. But here''s what is coming up next:<p>

<p><a href="http://www.gametrailers.com/videos/v4ihwx/woodcutter-simulator-2013-debut-trailer">Woodcutter Simulator 2013</a></p>

<p>
I think this must be a joke... made using the Farming Simulator actually :)</p>', NULL, CAST(N'2012-12-19 00:00:00.000' AS DateTime), CAST(N'2012-12-19 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (4, NULL, N'Design Guru', N'Titled for irony, I am not a designer and really don''t know what I''m doing. But, I think the way it looks now is more communicable and easier on the eye.', N'<p>Basically, I got rid of all the rounded rectangles. That ''phase'' of the web seems to be over.</p>

<p>I''ve added another feature page for my HTML 5 experiments. There''s only one up at the moment, but it''s a good one ;) Apart from that, have a look through and let me know if you find problems, either on the forums or elsewhere.</p>

<p>Oh, and a happy 2013!</p>', NULL, CAST(N'2013-01-14 00:00:00.000' AS DateTime), CAST(N'2013-01-14 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (5, 1, N'Transition to XNA 4.0', N'I never tried XNA 4.0 while it was in CTP, so the list of changes from 3.1 to 4.0 was new for me, but it''s going well. 
However, there''s one problem which I have not been able to solve yet. And no matter what I try, it simply won''t go away.', N'<img src="/content/media/IncorrectClipping.jpg" alt="IncorrectClipping.jpg" />
<p>The above is a screen dump of my actual back buffer from the level editor. And, as highlighted, there seems to be some problem 
with the clipping or something. Areas which are supposed to be hidden behind other areas are drawn and seen. Sort of like a Z order problem when working with sprites.</p>
<p>I''ve searched high and low for solutions to this, but Google has not been forthcoming. And the App hub registration is so 
spastic that I cannot sign up (South Africa is not supported). My options are now either to 
<li>Continue development while ignoring this ''artifact''.</li>
<li>Revert back to XNA 3.1 and go with DirectX 9.0c and shader profiles 2.0</li>
</p>
<p>Of course, I''ll still continue to look for a solution. The members over at Stackoverflow might hit on something...</p>', NULL, CAST(N'2011-05-24 00:00:00.000' AS DateTime), CAST(N'2011-05-24 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (6, 1, N'Some stutterings, but progress!!', N'So, most of my initial teething problems have now been overcome. Partly to do with differences between XNA 3.1 and XNA 4.0, 
and partly other stuff, like shader code and model export problems. ', N'<p>The upshot of it all is that I''m finally in a position to actually start building my game. To date it''s just been infrastructure mostly, and there are still a bit left of course. But as you can see, 
the level editor is now gaining momentum.</p>
<img src="/content/media/LevelEditorManual.jpg" alt="LevelEditorManual.jpg" />
<p>Those are the 8 basic tiles I''ve modeled so far. There''s plenty more to come, and they''ve been manually placed in XML because the editor is still lacking vital functions. Something else I''ve managed to accomplish 
is a solar model, or sunlight & moonlight model if you want. It manages itself through calls from the Update() method. The main game code does not have to do anything special to change the direction or the light color. 
It all happens within the instance itself. You simply instantiate it at a certain time of day and it starts ticking over. The effect as it goes from dawn to dusk through the color and direction changes is quite amazing to see. Of course, 
in the finished game, a complete cycle of 24 game time hours will probably take around 3 hours real time. It should be subtle, otherwise it will distract the player. However, if someone does play for 1.5 hours straight, the changes will be noticed. That''s the sort of scenario I''m looking for.</p>
<p>And as you can see, I managed to solve the problem with the incorrect rendering of hidden faces. Some settings in the shaders solved it. Not sure what device state in XNA 4.0 is appropriate to fix it yet though.</p>', NULL, CAST(N'2011-05-26 00:00:00.000' AS DateTime), CAST(N'2011-05-26 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (7, 1, N'Content Generation - Challenge Accepted', N'By far the most time intensive part of game development in my experience is content generation.', N'<p>Once you''ve defined the scope, 
the types and the number units you will be putting into the game, there''s really no getting around the fact that making all of the content, 
be it 3D models or sprites for the UI, takes a very long time.</p>
<p>Much more so than the actual coding of the game. Needless to say, as an indie 
I don''t have a team of artists at my disposal. So between myself and Ramon, we''re filling up our free time (an increasingly scarce commodity!!) quite comprehensively.</p>
<img src="/content/media/palmtreeAlpha.jpg" alt="palmtreeAlpha.jpg" />
<p>Currently I''m concentrating on level objects, like the palm tree above. There''s some problems with the alpha map as you can see. Depending on the lighting it''s either white, 
or it''s the fill color. Not yet sure what the reason is, but I''ll muck around with the shader and the device state to try and solve it. Ramon is currently concentrating on the actual player 
helicopter model. We''ve dicussed various "modes" for the chopper, and there will be an upgrade system. Mostly though, we want a visual aspect to each of the upgrades and special abilities. 
This might complicate matters dramatically with the animation loops etc, but we''ll get to that when we get to that :) </p>
', NULL, CAST(N'2011-06-10 00:00:00.000' AS DateTime), CAST(N'2011-06-10 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (8, 1, N'Weekend overtime', N'I''ve had a good weekend in so far as Stingray is concerned. I also suppose that it''s time I add it to the features on this 
website. Anyway, the alpha blending problem was sort-of solved after I posted on <a href="http://stackoverflow.com/questions/6308079/device-alphablend-state-in-xna-4-0">stackoverflow</a>.', N'<p>I say sort-of, because if you read the articles that was linked by Andrew, it''s quite obvious that I didn''t understand some pretty basic concepts very well. For instance, the depth buffer. 
That forced me to rethink my design a bit, and how I process the content for drawing. It also forced me to do some math, and I realised that for items like the palm tree, it might just be 
possible to model it completely instead of using very few faces with alpha blending. And as a result it actually looks better.</p>
<p>Apart from the above, I''ve managed to get somewhere with one of the enemy units as well. A simple recon vehicle. Here is a render of it. There''s still a bit to do before I can use it 
as a game model, one of which is the texture coordinate unwrapping, which will probably prove to be a pain.</p>
<img src="/content/media/ReconVehicle.jpg" alt="ReconVehicle.jpg" />
<p>You might have noticed that there is a rather complete lack of concept art so far. The fact of the matter is, I just don''t have the time to sit and churn out pencil sketches or otherwise 
to design the game elements. However, as things start ramping up, I''m pretty sure I will have to take a step back and spend some time on this.</p>
', NULL, CAST(N'2011-06-12 00:00:00.000' AS DateTime), CAST(N'2011-06-12 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (9, 1, N'Unwrapping', N'It''s been slow progress for us, but, at least I''ve managed to complete the unwrapping of the recon vehicle. It''s got a basic color map for now, with 
little or no details at the moment.', N'<p>Also, I intended to make the map only 512x512, but that proved to be a bit too small in getting the colors on the right places. Anyway, here''s another render of it with a small thumbnail of the map. 
With these units there is a difficult decision as to what level of detail is required, and I suppose it will take a few iterations to get it right. For the most part I envisage that the player won''t be too near them at any time. But, if there are any animated cut scenes later on starring these models, 
they should at least look the part. So, this model clocks in at just under 7000 triangles. If that''s too much or too little, I don''t know. From these pre-renders, the quality looks ok-ish to me. And I still got to do all the normal maps as well.</p>
<img src="/content/media/ReconMapped.jpg" alt="ReconMapped.jpg" />
<p>Ramon also sent me a render of the chopper. This is already a week out of date, and he has already started unwrapping it I believe. 
As you can see, he put a lot of effort into the rotor mechanism. It''s something that will be on the screen almost all the time, and I think it''s stellar. There''s also a lot of animation loops he''s got to do still. The wheels, bay doors and the various guns and their different animations. Hopefully I''ll be able to figure out how 
to get it all into the game itself.</p>
<img src="/content/media/stingray01.jpg" alt="stingray01.jpg" />
<p>There''s so much more content to make still, but slow and steady wins the race :)</p>
', NULL, CAST(N'2011-06-21 00:00:00.000' AS DateTime), CAST(N'2011-06-21 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (10, 1, N'Transforming Normals', N'I''ve been thinking about my problems lighting my scene using directional light (the mentioned sunlight system) for a while now. 
', N'<p>The screens of the level editor I''ve posted have hacked code to get it working correctly. I basically passed the original normals 
straight on to the pixel shader code instead of transforming them.</p>
<p>This means that all the tiles get the same amount of sunlight. 
It''s obviously a problem when you start to rotate stuff, since the shadow areas will change depending on the orientation. 
Thus I need to transform the normals. However, doing so resulted in very strange results. Tiles to the left were lit differently 
from tiles to right. It''s actually an age-old problem, and perfectly obvious when you visualise the process of the vertex and 
pixel shaders.</p>
<p>The problem comes in with using the same matrix as the one for the vertex transformations (which, incidentily, all 
tutorials do). But, <a href="http://www.unknownroad.com/rtfm/graphics/rt_normals.html">this article</a> explains quite 
clearly that using the main transform matrix as is, is not correct. Also see my news post about the MATHS :)</p>
<p>As the article explains, if you use the correct distinction between points and vectors, the same transform matrix could 
apply if you don''t do any scaling. Which means you modelled everything correctly at the start. But I did a test, and even 
though you can declare the input to the vertex shader as float4 for the normal, XNA does NOT pass in a normal with the w 
component. In the .X files, the normals only have three components, and XNA''s vertex structures are based around the 
Vector3 struct. This means the w component does not exist within the C# space. So it''s up to you to set w to zero in the 
shader code for normals (it seems it''s set to 1 by default based on my test results).</p>
<p>The conclusion then, is that XNA does not employ "homogenoeus vectors and matrices" at all. So you have to make it do so,
otherwise you won''t be able to move your models around your world. Well, you will, but it will all be dark.</p>
', NULL, CAST(N'2011-07-05 00:00:00.000' AS DateTime), CAST(N'2011-07-05 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (11, 1, N'Big Update!', N'There''s no release or anything, but the more we finish the more excited I''m getting about this project. Honestly...', N'<p>Ramon has almost completed mapping the chopper. I know he''s having difficulties and headaches since it''s a rather complex 
model, but if anyone can get it done, he can. This is what he''s come up with so far...</p>
<img src="/content/media/stingrayrender4.jpg" alt="stingrayrender4.jpg" />
<p>In other news, I''ve finished a prototype ''skybox''. I say prototype, because at this point it''s not really all that good 
looking, but the idea is proven and all that remains is some tweaking. The textures are transitioned based on the sunlight 
system. Actually, the sunlight system drives the whole skybox by itself, including the rendering of it. All nicely packed into 
a single box of pleasure :) The screenshots were taken at dawn, noon and during late evening respectively.</p>
<img src="/content/media/skyboxDawn.jpg" alt="skyboxDawn.jpg" />
<img src="/content/media/skyboxNoon.jpg" alt="skyboxNoon.jpg" />
<img src="/content/media/skyboxLateEve.jpg" alt="skyboxLateEve.jpg" />
', NULL, CAST(N'2011-07-13 00:00:00.000' AS DateTime), CAST(N'2011-07-13 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (12, 1, N'The Dark Side', N'Tricky shadow mapping bits comming up...', N'<p>The principle behind this is quite simple. There are no shortage of tutorials out there on this topic, 
and plenty of discussions about the best method to use. The one I have opted for is the custom shadow mapping option. 
I suppose at some point I will try and figure out proper hardware supported stencil buferring etc, but there are well 
documented pros and cons attached to each method. See <a href="http://forum.beyond3d.com/showthread.php?t=4369">here</a> for 
details. It is a very, very old post, but serves to illustrate my point.</p>
<p>So anyway, moving on to what I''m trying to accomplish... Here''s the depth buffer I''m generating from the viewpoint of the 
sun. This bit is pretty straight forward, using its own (but small) shader and a render target. One note on this - most native 
DirectX implementations opt to use the R32 surface format for this to retain accuracy. For XNA, I initialize the render target to the 
R1010102 format. This is memory overkill at the moment, since I''m only using the red channel for depth information. The problem 
with this is that other formats like Single does not support blending (or requires Point filtering instead of linear), so 
passing it to the real scene render shader is not supported when the BlendState is set for alpha. So, to make the best of it, 
I''ll probably change it to a normal RGBA32 format and pack the float depth value into the various components instead. 
Should make for some pretty psychedelic depth buffers :)</p>
<img src="/content/media/ShadowMapDepthbuffer.jpg" alt="ShadowMapDepthbuffer.jpg" />
<p>Currently the actual generated shadows are all over the place. I''m thinking an error in the projection from the camera 
viewpoint to the light viewpoint for depth comparison. No luck so far, but I''ll keep at it. After that, there''s blurring to be 
done as an additional step. The only problem with all this is that the frame rate is cut in half... At this point I''m not 
discriminating as to what I''m rendering for the depth buffer, but that said there isn''t a lot on the test level to exclude 
either.</p>
<p><b>UPDATE</b></p>
<p>I''ve managed to solve the problems with the projection from the camera view to the depth buffer. The HLSL function <i>tex2Dproj</i> 
doesn''t actually do what it says on the tin. Basically, once you''ve transformed the pixel to the depth buffer space using the 
appropriate projection, simply calling <i>tex2Dproj</i> directly using your resulting projected vector isn''t actually 
going to work. Even though the function does the projection divide, it doesn''t restrict it into the 0.0 to 1.0 space that 
is used for UV lookup. A <a href="http://www.gamedev.net/topic/408894-what-the-hell-tex2dproj-have-done-/">simple matrix</a> 
calculation can do that for you though, and then use the function to get the appropriate texel.</p>
<img src="/content/media/crappyshadowmap.jpg" alt="crappyshadowmap.jpg" />
<p>This is what I ended up with. Very blocky and very low fidility, but at least it''s accurate. These is still a problem with 
the clamp filtering or something. The far tiles are dark which shouldn''t be. But all in good time...</p>', NULL, CAST(N'2011-07-20 00:00:00.000' AS DateTime), CAST(N'2011-07-20 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (13, 1, N'Painting the Environment', N'After some good time spent at the <i>art centre</i> (my desk top PC at home), I''ve managed to make some 
cool additions to the level layouts.', N'<p>There''s not much to say here. Doing this using tiles is a bit of a pain, but it proves interesting, and gives us the 
ability to upsize the level area dramatically without a big impact on the footprint on disk, memory or framerate for that matter. 
Also, since the inspiration dates from the late 80''s and early 90''s, the tiling is sort of mandatory. Think of the new Ford Mustang. 
It''s a faithfull homage to the old classic. The lines, the overall design harks back explicitly, but yet manages to convey a modern 
design. This is what these tiles are all about. Back in the old days they had maybe... 5 or 6 different ones. Today we have excellent 
capacity and capability, so I can have 600 different tiles split between the various geographical areas. But of course, I''d still want 
you to get the sense that it''s tiled... just the like old games.</p>
<p>The screens below show two of the new tile areas, cliffs and the coastal regions. There''s also a shot showing the recon vehicle in 
game. It''s still with the basic texture. And I''ve not yet built in the positioning of objects (static or actor), so the palm tree and recon 
is in the same spot for the time being :)</p>
<img src="/content/media/AboveCliffs.jpg" alt="AboveCliffs.jpg" />
<img src="/content/media/OceanView.jpg" alt="OceanView.jpg" />
<img src="/content/media/WithRecon.jpg" alt="WithRecon.jpg" />
', NULL, CAST(N'2011-07-29 00:00:00.000' AS DateTime), CAST(N'2011-07-29 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (14, 1, N'Balancing the Environment', N'After cracking on with the content, I realized that I was going to have to redo some of it...', N'<p>It sounds like a bit of a setback, and to a certain extent it is, but I was never under any illusion that I''d be able to 
get away without LOD. So, I revised my content model and drawing engine a bit, and then proceeded to model different versions 
of some of the same models. The palm tree below as exhibit A.</p>
<img src="/content/media/PalmLOD.jpg" alt="PalmLOD.jpg" />
<p>I reached a point where I was forced to solve my frame-rate issues. I built an object brush for the level editor. It allowed 
me to pretty quickly paint many, many, many palm trees. After battling a bit to determine the exact height underneath each instance 
so that they ''stick'' to the level tiles, I ended up with a frame-rate of about 12... And it was even slower in the game when the 
depth and shadow maps are also rendered. So, LOD as a first step. Secondly, I''ve had to limit the content that is used for the 
depth map and shadow map renders. Not all content will generate shadows, and not all content will receive shadows. Discriminating 
between that has helped to get the frame-rate back to around 35... which is better for what is currently all happening.. but I''ll 
still need to spend time revising. I hope to avoid having to do LOD on the tiles - it will probably cause tears and look 
pretty bad. So hopefully far-plane distance and drawing distance will help to limit the lag from the tiles. </p>
<p>Also, Ramon has made progress with the Stingray...</p>
<img src="/content/media/StingrayRender8.jpg" alt="StingrayRender8.jpg" />', NULL, CAST(N'2011-08-12 00:00:00.000' AS DateTime), CAST(N'2011-08-12 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (15, 1, N'Graph Theory', N'Moving on to random level generation...', N'<p>Noise maps are a tricky thing. The theory of it, as with everything, is pretty <a href="http://freespace.virgin.net/hugo.elias/models/m_perlin.htm">straight forward</a>. 
Implementation, on the other hand, proved to be more than a day''s worth of frustration. But I did come up with something usefull 
for generating the levels in Stingray.</p>
<img src="/content/media/NoiseMap.jpg" alt="NoiseMap.jpg" />
<p>The second problem is knowing which tiles belong together. Apart from the fact that four tiles at a time share a single 
texture map, there''s no other obivous link apart from the naming of the tiles. This is not something usefull. I''ve opted 
to build a tool to link tiles to one another in all four directions. It''s hard work up front (read tedious) but it will make it 
fairly easy to determine which tiles to place where on the map. Considering the number of tiles I already have (and that are 
still to come), the links of all these tiles would make for some excellent graph theory examples :)</p>
<p>So, to achieve this, I''ve worked hard on the content set editor to facilitate both the actual linking and testing the 
linking. These screenshots also show the various elements that the drawing engine supports, although I haven''t made maps for any 
of the slots except the diffuse.</p>
<img src="/content/media/LinkingTiles.jpg" alt="LinkingTiles.jpg" />
<p>Testing the links is simple - you select the tile in the appropriate direction and it''s drawn next to the tile being edited. 
It''s worth while to note that the coordinate system in XNA is different (Y-Up) from the modelling software (Z-Up). 
So, when modelling you have to remember that north becomes south, and west becomes east... maybe thats the future of world politics as well?</p>
<img src="/content/media/TestingLinks.jpg" alt="TestingLinks.jpg" />', NULL, CAST(N'2011-08-25 00:00:00.000' AS DateTime), CAST(N'2011-08-25 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (16, 1, N'The Real Stingray', N'Finally, a much anticipated moment has arrived... Click through to see the real Stingray model in action.', N'<p>The office asked me to do a presentation on Stingray for one of the Knowledge Sharing Sessions they have on a monthly basis. A 
couple of guys at the office have been keeping tabs on the progress and they''ve deemed it worthy of a recommendation for a session. 
We''ll mostly cover the performance side of things - optimal code with optimal structures - to keep it relevant.</p>
<p>As part of the preparation for it, we decided to wrap up the production on the actual Stingray chopper model and stick it 
into the game for real. Ramon had actually delivered the model to me a while ago, and it''s been lying in my inbox ever since. But 
the weekend I decided to take it in, clean it up, finish the unwrapping and well... see for yourself...</p>
<img src="/content/media/TheRealStingray.jpg" alt="TheRealStingray.jpg" />
<p>I decided to start with a ''stealth'' texture. There will be more options later on, which will be determined by your 
upgrades. The other neat success about this is that the canopy of the chopper is the first real transparent item in the game. 
I''ve had to tweak the shaders a bit to get the sun/moon reflection off it looking good, and it pretty much all worked out 
well.</p>
<img src="/content/media/TheRealStingray02.jpg" alt="TheRealStingray02.jpg" />
<p>I''ve also built some more items for the levels and overall there''s much more going on in the scene than before. The frame 
rate on my laptop seems to still hover around 30 after I revised some infrastructure and optimised distance sorting, but I think 
that''s the limit of the hardware on here. I should start taking screens from my gaming rig now. All in all it''s now really 
starting to look like a game... At least that''s what Ramon said :D</p>', NULL, CAST(N'2011-09-07 00:00:00.000' AS DateTime), CAST(N'2011-09-07 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (17, 1, N'Don''t loose faith', N'So ya''all figured the project was dead?... No it''s not.', N'<p>Progress has been steady, but nothing that was worth posting. I''ve finished all the tiles for the basic level layout. There 
were quite a few do to, but now that it''s done I can start working on the level generation. There is already something happening 
there, but with missing content things didn''t work very well.</p>
<p>Other than that, I''ve also started on the targeting system, e.g. basic hit testing. The chopper''s gun follows the cursor 
on screen and the system correctly identifies which object is being aimed at. Some simple reverse projection calls and maths really. 
The biggest challenge was to do that as part of the pre-process loops to avoid having to traverse the object structures a second 
time. Of course it''s easy to just make a bunch of global variables... but come-on... seriously? So, since there''s already loops 
happening to identify viewable objects and LOD distance in the pre-process phase, there was no point in looping through that 
list again to find my target. Also, the gun does fire, ammo gets expended, but no visual cue and no damage is being delt yet.</p>
<p>Next on my agenda, I''m looking for a small success item, so I''m gonna do the HUD. But not as you might think ;)</p>
<p>There''s also other stuff that I''m starting to think of. Especially sound and music. I''ve asked Ramon to start on an intro 
sequence as well. Whether that will be stills based or movie based we''re not sure yet. But now that the basic level layout options 
are done for the most part I can get on with rounding out the experience, story line, missions etc. At some point though I''ll have 
to go back and revise all the art, get normal maps in there etc. That will be the polish phase.</p>', NULL, CAST(N'2011-10-11 00:00:00.000' AS DateTime), CAST(N'2011-10-11 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (18, 1, N'The pepper grinder', N'Still busy with the random level generation. But the sprinkles of success is landing everywhere.', N'<p>There are still a few missing tiles that needs to be made in order to complete the puzzle. These are the obscure tiles that 
will appear maybe five times in the level in very suspicious nooks and crannies. But those nooks and crannies are there because 
the random decides for them to be there. That is the problem with the random level generation. If everything was sand, for instance, 
the problem would not be present at all. But with the transition areas from sand to ground, and with the cliff tiles in the mix, 
the current seed I''m testing with throws me a few curve balls with regards to the layout.</p>
<img src="/content/media/MissingTile01.jpg" alt="MissingTile01.jpg" />
<img src="/content/media/MissingTiles02.jpg" alt="MissingTiles02.jpg" />
<p>Apart from that though, other level bits like the trees, the shacks and the prefab houses are now also being ''strewn'' accross 
the level. It''s kinda like discriminatingly shaking a pepper grinder over the basic layout. But palm trees should stick to the sand tiles. The shacks 
and prefabs can land anywhere. And nothing should land on the dune tiles. Of course, since this is random, the specific locations 
are not exactly controlled. Hence sometimes (again on the transition areas) the trees do land on the ground. But that''s ok.</p>
<p>And ''land'' is the operative word here. I know the level elevation will never go higher than 240. So I start by placing the object 
on an altitude of 1000. From there I cast a ray straight down and get the intersection on the bounds, and then get the intersection on 
the exact face. It''s the same routine I use for the chopper''s altitude calculation. From that I can determine with a high degree of 
certainty exactly what Y value the object must have to be on the ground. Basically firing the objects from space.</p>
<p>The other bit of work I had to so was to restructure some bits of the entity model so that I could create an object preset. These presets 
are used to define objects that will be used in the level. For example, the guard tower is made up of the main building model, and then 
the windows/screens that has an alpha component. The alpha components are drawn last for obvious reasons, so it has to be two separate 
models in the content set. The preset combines these and from the preset various different instances is created as level objects.</p>
<img src="/content/media/HeavyGuardTower.jpg" alt="HeavyGuardTower.jpg" />
<p>The HUD has also been tweaked and the display errors on the alti-meter has been corrected. But there''s more changes I want to do on 
it before I''ll show you what it looks like.</p>', NULL, CAST(N'2011-11-16 00:00:00.000' AS DateTime), CAST(N'2011-11-16 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (19, 1, N'Smoke signals', N'Finally, after a bit of a rewrite, the particle system is taking shape.', N'<p>I initially just set off doing my own thing, using the existing architecture of the game. Things worked... but it looked 
utter crap. I was initially using an instanced quad, and was about to get into how to billboard it. But it wouldn''t have worked. 
Calculating rotation angles for 600+ particles per smoke plume was always going to be too expensive. Of course, you can argue 
that you only need to calculate it once because each particle needs to face the same direction. But only if you''re viewing it 
from a horizontal position. Instead I decided to look at the AppHub 3D Particle sample. And boy did that one look pretty damn good.</p>
<img src="/content/media/PreSmoke.png" alt="PreSmoke.png" />
<p>So I set about rewriting my particle system a bit to make use of the clever ideas implemented in that sample. The circular array 
instead of a list (must faster). The dynamic vertex buffer instead of an instanced quad. And the vertex shader which does almost 
all the calculations on the GPU instead of on the CPU. Clever stuff.</p>
<img src="/content/media/Smoke01.png" alt="Smoke01.png" />
<p>There''s still one bit of this implementation that I don''t quite understand: the way it calculates the actual screen space 
coordinates of each particle based on the viewport scale (??) and a single position vector. It''s something I''d never have thought of 
in the first place, but using this method evidently requires very little overhead to do billboarding. So I''m quite embarassed to 
say that at least that bit of code is a straight copy and paste from the sample.</p>
<img src="/content/media/Smoke02.png" alt="Smoke02.png" />
<p>In the end though it''s at least looking proper. That said, the first implementation is something I''d want to keep, because 
there will be use for a system that uses instanced geometry. I''m thinking debris particles when something blows up. That would just 
look 100 times better using actual models than billboards, and you can do some excellent random rotation per particle.</p>
<img src="/content/media/Smoke03.png" alt="Smoke03.png" />
<p>And lastly, untweaked and not done yet, is the new HUD showing in these screenshots. It''s fully integrated with the chopper, 
and doesn''t required the player to move focus from the action in order to see the details. Granted, at some angles it does become 
less readable, but so far to me that doesn''t pose a problem.</p>', NULL, CAST(N'2011-11-23 00:00:00.000' AS DateTime), CAST(N'2011-11-23 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (20, 1, N'Gameplay', N'Yes... there are finally some gameplay elements.', N'<p>If you expected to see some gameplay footage though, sorry to dissapoint. But I have sort of completed the 
shooting. This followed from my chat to Danny from <a href="http://www.qcfdesign.com/">QCF Design</a> after 
Wednesday''s game dev session. Basically it went something like : "You say you''re writing a game, but there''s no gameplay..?" "Um..yeah.. but...um.."</p>
<p>I was looking for a way to determine an exact hit. Until now I targeted the objects using the bounding boxes. 
It''s fine to know what your aiming at in the general area, but not good enough to determine what exactly you are 
hitting. So I got an idea last night as I lay in bed, got up, and tried it out. As it turned out, it only worked 
in my head. But I then I found <a href="http://www.blackpawn.com/texts/pointinpoly/default.html">another solution</a>, 
and that worked like a charm.</p>
<p>The chorus goes something like this:
<ul>
<li>Find the ray that is the gun sight</li>
<li>Use that ray and object bounding boxes to find potential targets</li>
<li>Test every object found if there is an exact hit based on the point in triangle method</li>
<li>Now I know exactly which object is hit, and I know precisly where that object is being hit based on the UV coordinates</li>
</ul>
</p>
<p>Two things here to note: Firstly, I already have a FOR loop to determine viewable objects in a pre-process method. I implemented these steps 
into this same FOR loop, otherwise you''re doubling up on CPU time going through the same list of things. Secondly, when using the point-
in-triangle method, I use my lowest detail models. They closely mimick the shape and size of the high detail versions, and there''s no point 
in hit-testing detail. You''re only interested in the bottom line - is it hitting exactly and where is it hitting? If I was writing Battlefield 4, then yes 
I''d use the higher detail versions to determine of an overhead power cable is being hit and break apart and spew electricity on everyone :)
</p>
<p>All that was left to do now was to pass a couple of event handlers around to effect the damage dealt by the gun. This actually happens 
per round being fired. I have a single particle system, attached to the chopper, that generates one particle for each round being fired. 
Each particle is assigned it''s target at the time of creation based on what the chopper was targeting at that specific time. When it expires 
it triggers its event that is hooked up to the linked object and some method calls sorts out the rest.</p>
<p>So, I finally have some real gameplay. Go figure :)</p>', NULL, CAST(N'2011-12-05 00:00:00.000' AS DateTime), CAST(N'2011-12-05 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (21, 1, N'Patrolling', N'I''m on a roll man! The few vehicles (actors) on my test level are now roving around, following thier randomly 
generated patrol routes.', N'<p>Started this last night, and they''re all moving around already. Simple waypoint manager class attached to each 
actor objects. At this point I just randomly generate a circular route or each object.</p>
<p>As far as problems go, correct orientation was a slight issue. At some point all of them moved around in 
reverse gear. But once that got sorted it wasn''t too difficult to make them actually turn around to go to the next 
waypoint. Just like a car would, e.g. not turn in the same spot. </p>
<p>Another one was to stick to the ground. The tiles are not flat. There''s bumps and stuff on them. So I hijacked 
the code used for the chopper''s altitude calculation to determine the correct height of each object at their location.</p>
<p>And then the speed. At the moment they''re all travelling at max speed. It''s fine, but it looks strange when they 
first set off - immediately barelling along at 25 m/s. There needs to be a run up. So I''ve been mucking 
about in Maxima to determine a good formula.</p>
<img src="/content/media/speedgraph01.jpg" alt="speedgraph01.jpg" />
<p>At first I was trying for a formula that I could simply use at any point between two waypoints. But the multiplier, 
which is the maximum speed, doesn''t actually result in the maximum speed. So I''ve abandoned that one.</p>
<img src="/content/media/speedgraph02.jpg" alt="speedgraph02.jpg" />
<p>Then I decided I''ll work on a percentage from the start of the ''from'' waypoint and a reverse percentage from the 
destination waypoint seperately and apply the above function.</p>
<img src="/content/media/speedgraph03.jpg" alt="speedgraph03.jpg" />
<p>But it looks that the ramp-up is still too quick to 10 m/s, so adjusting the exponent solves that with the third function.</p>
<p>Now to implement! Oh yeah - also all of them always turn left. I need to look at the dot product to get a sign 
to apply to the direction adjuster.</p>', NULL, CAST(N'2011-12-06 00:00:00.000' AS DateTime), CAST(N'2011-12-06 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (22, 1, N'Patrolling continued', N'So when you''re going up a hill, you''re going up a hill, right? Not so easy when you''re coding it.', N'<p>It''s all fine and dandy moving vehicles around the level, heading in the correct direction and at the correct 
height based on the tile underneath. But when it encounters a hill/dune/bump it shouldn''t just float up and float down, 
the vehicle''s orientation should change with it''s nose point towards the sky as it mounts the slope.
</p>
<p>It was easy enough reading the normal from the currently occupied face. But I had some trouble getting the measurements 
to work correctly according the vehicle''s local axis, as opposed to the world axis. This means that if the vehicle is heading 
north-east, it''s local axis is turned 45 degrees (or Pi over 4 :P) from the world axis. But in essense I ended up with a value that I could use to stick into the world matrix for the vehicle.</p>
<img src="/content/media/VehicleSlopeCalc.jpg" alt="VehicleSlopeCalc.jpg" />
<p>The green (tangent) vector here is the key. You cannot work with the normal, because it has no relation to the direction. I initially 
used the red (normal) vector to calculate an angle with the Unit Y vector, because irrespective of direction, Unit Y is still applicable to the 
vechicle''s local axis. But in fact that was wrong. The direction is important, and the calculation of the tangent to use in conjunction 
with the vehicle''s own direction vector is the key. In the image below the magenta lines are the face normals, and the green lines are the 
tangent normals.</p>
<img src="/content/media/VehicleSlope.png" alt="VehicleSlope.png" />
<p>So that solved the tilt angle, but as you can see, I still need to do the roll-angle. Hopefully this won''t take me as long.</p>
', NULL, CAST(N'2011-12-08 00:00:00.000' AS DateTime), CAST(N'2011-12-08 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (23, 1, N'Spit and Polish (just a bit)', N'I decided that after the quantum leap of the last week or so, I needed to refine what I had done. 
Both in artistic appeal and technical implementation.', N'<p>The graphical side of it is not as important for me at this time. If it''s functional and it''s 
visible, it''s fine. But with the particle systems the work involved in just making it look so much 
better than the first draft implementation was miniscule, so I spent an hour on it.</p>
<p>More importantly however, technically I had to consolidate a bit. It''s so easy for the code to 
run away from you if you''re not carefull. Sloppy implementation up-front will make for big headaches 
later on in life. That''s kinda common knowledge. The problem with prototyping though is that you 
tend to rush through things with your mind on the game play design and not so much on the implementation 
design. Because the purpose is primarily to test the game play, this isn''t a problem per-se. However 
once the game play is confirmed and the idea is not binned, you <b>have</b> to go back and refactor 
and revise that implementation.</p>
<img src="/content/media/SmokeAndDust.jpg" alt="SmokeAndDust.jpg" />
<p>So after some structural changes, some new additions to the XML pipeline and three re-works of 
the smoke and new dust textures, this is the result. The dust trail specially adds a lot of base for me. 
It makes the vehicles have substance. But anyway, I hope you like!</p>', NULL, CAST(N'2011-12-12 00:00:00.000' AS DateTime), CAST(N'2011-12-12 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (24, 1, N'The Nature of Digital Control', N'So after some effort I''ve got the rocket''s firing. But now I''ve come upon a control problem.', N'<p>The thing about rockets is that they fire straight from thier mounted position, much like the 
mounted machine guns on the old WW2 airplanes. Of course the mountings are slightly angled inwards 
so that eventually the two lines of fire cross each other, which determines the optimal distance to target. 
But in that I''ve now come across an issue, and it''s to do with the controls. The chopper pithes and rolls, and 
affects where the rockets are aimed at. But with keyboard control it''s really difficult to keep that reticle pointed 
on a specific target. Partly because my targets are now moving as well (if you aim it at a vehicle), but mostly 
because with keyboard controls there''s no in-between. The chopper is either fully pitched flying forward, 
or hovering flat, or lifted up going backwards, in which case you''re firing into the air.</p>
<p>Now, the controls as implemented at the moment are shaky at best, because I''ve never needed it done well enough 
for gameplay. So that''s obviously what I now need to do next, before I even bother with the smoke effects and all the 
other neat stuff that will give the pleasure of firing rockets.</p>
<p>There was also a more minor problem with the HUD in regards the rockets. Since they have a static reticle, and since 
the player can swivel the view around the chopper, seeing where the rockets were going to hit didn''t work out that well. 
Of course it''s easy to say "Yes, but the player should look down the sight when using rockets", but that''s not a 
very good argument because then it renders the use of the gun, which is tied to the camera, impossible.</p>
<p>Fortunately, with the reticle for the nav/gun sight, I''ve already solved the problem of correctly <i>projecting</i> the 
reticle at the correct position onto the HUD relative to the spot being aimed in conjunction with the camera position. 
So it wasn''t too much of a hassle having the rocket reticle always <i>appear</i> to show where the rockets will hit, 
irrespective of where the camera is. In this way the player can now fight two battles as once - attacking one target with the 
gun, and another with the rockets, all at the same time.</p>
<p>Hopefully I''ll have some screenshots towards the end of next week with smoke effects, bullet tracers etc, but first I''ve 
got to solve the control issue, otherwise there''s no point to all this.</p>', NULL, CAST(N'2012-01-12 00:00:00.000' AS DateTime), CAST(N'2012-01-12 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (25, 1, N'Team Rocket', N'The ordnance are flying all over the place. And it''s just beautifull.', N'<p>This morning I tweeted a picture of the first successful rocket smoke trails. That was a major milestone. By that time 
the controls were also about half way sorted, and I did a lot a of play testing through-out the day. But there was still 
more to do. A rocket is pointless if it can''t hit anything, and the next step was to start blowing stuff up.</p>
<img src="/content/media/RocketFire.png" alt="RocketFire.png" />
<p>Without too much effort on content (I only made a new explosion particle map) I managed to get the explosions going, 
and soon after that the accurate hit testing followed. Fortunately, this far into the project, there''s already a lot of 
fully functioning methods, and I really didn''t have to write anything majorly new to achieve it.</p>
<img src="/content/media/RocketHit.png" alt="RocketHit.png" />
<p>All that remained was further play testing, and of course bugs. Some were small and some where pretty hard to figure 
out. For example, the rockets travel fast. Their speed is configured at 40 meters per second, and after failing to destroy 
targets that I thought I really did hit, I found that the hit testing condition is too simple. It tested how close the rocket 
was to the target to ascertain if it''s explod''n time. But, on one frame it was just .045 units too far away, and on the next frame it was 
already past it, e.g. -3.5. Which meant I had to build in a prediction variable of where it will be on the next frame which is 
then used in confuction with the simple test.</p>
<p>Fortunately I managed to solve all the problems that I could uncover during all my testing, and boy, it''s now really 
starting to look like war. On my test level the objects are pretty closely placed, so everything is within reach of your 
gun and rockets. At some point I had about 20 smoke plumes going up, and that''s a big smile on my face...</p>
', NULL, CAST(N'2012-01-14 00:00:00.000' AS DateTime), CAST(N'2012-01-14 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (26, 1, N'Re-start your engines and re-arm your guns', N'Guns firing, missiles flying and some sort of engagement on the books.', N'<p>So what have I been busy with? Well mostly player engagement. Previously I had the chopper''s gun firing, and shortly 
after that the rockets started flying as well. It wasn''t long until missles were added to the mix using locked-on 
targets and following the up and over flight path towards the target.</p>
<p>After that I messed around with the controls a bit more. Some people suggested a Battlefield control system which seems 
to work a lot better for aiming the rockets, but to me not such an intuitive third-person system. It''s still subject to change. 
What I also did was put in the forward outposts. These are massive roaming landing platforms to refuel, rearm and repair at. 
They are commandable from within the chopper''s Nav mode and they have some defensive capabilities, but they are very slow to move around 
and require time to deploy and redeploy.</p>
<img src="/content/media/ForwardBase.png" alt="ForwardBase.png" />
<p>Then I moved on to the NPCs. After some discussions with Sven (<a href="https://twitter.com/#!/FuzzYspo0N">@FuzzYspo0N</a>) 
regarding systems in the game, I sat down and started 
designing the gameplay systems with regards to the AI and player engagement. The first order of business was individual AI. Each NPC 
needs to react to the environment in some sort of autonomous way. Questions like "Do I attack?" and "Do I flee?" is not always subject 
to external commands. These questions'' answers are also different depending on the type of NPC. A simple recon jeep will react differently 
to a tank when a chopper appears over the horizon.</p>
<p>So, coming off that, I first had to restructure yet again. My asset structures were incompatible with different settings for different 
unit types and the class hierarchy did not lend itself too well to applying these different settings. The restructuring took about a day, but it 
left me with a clean implementation structure for not just custom AI and individual configurations, but also for unit specific animations and 
other sorts of class-differentiating NPC attributes.</p>
<p>So, what''s the outcome of that you ask? Well, the recon jeeps now react when you come into visual range. If you''re still far enough and 
appear not to head in their direction, they will continue on their patrol route. If you come closer they will start to put distance between 
themselves and you. If you''re really close, they start to open fire in defense. And they are also reporting your position to the central AI every 60 seconds. 
Of course, this is just the start of the autonomous part of it all that I''ve now completed. Going furthur than that, the AI should be able to 
form groups of recon jeeps who will then 
openly attack you instead of simply defending for example. But those sorts of things will all be built as the model becomes clearer and the design 
improves. Also they are actually shooting back and dealing damage to you - that already works too :)</p>
<img src="/content/media/ShootingBack.png" alt="ShootingBack.png" />', NULL, CAST(N'2012-02-07 00:00:00.000' AS DateTime), CAST(N'2012-02-07 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (27, 1, N'Mind Games', N'The last couple of weeks has been a real mind fuck.', N'<p>Probably the most difficult part of this projet to date has been definining the various game systems and AI states. Yes, 
it is a bit late in the day for this sort of thing. But at least I''m there and I''m doing it. Descriptively, 
<i>tiring</i> is an understatement. Much like concept art this process starts on paper, and then I transitioned it into 
a mind map. But conceptually it''s way more taxing than anything I''ve done so far. It''s just hard.</p>
<img src="/content/media/SystemsMap.png" alt="SystemsMap.png" />
<p>Initially it seemed like it would take two ticks to do actually, hence the up-beat nature of the previous blog entry. The autonomous 
AI for the vehicles were quick to define and implement. And also easy to test. But when you start fleshing out the ''greater force'' that the player 
will be up against, things start spiraling out of control very easily. And like most software projects, when you start eyeballing the details 
there''s nothing keeping an eye on the bigger picture. I''ve come to refer to this as the distilling process.</p>
<p>Piling on states and variables is no good thing, and it''s hard to take all that and boil it away until the essentials remain. 
So what is essential then? That rather depends, but one non-negotiable requisite is to have a very clear idea of what your game is going 
to be about. And I''m not talking about your idea''s premise. I''m talking about your game systems.</p>
<p>How are you going to achieve meaningfull player decisions? Variations in player engagement? Sure, some of the story elements or the underlying 
premise drive towards that, but that doesn''t go deep enough.</p>
<p>In Stingray, the premise is corporate warfare. Part of the story is infrastructure. So, a tactic that would slowly disable intallations is 
on the cards because if you''re on the full-stealth side of the tech-tree you can''t bull-run an installation out right. Take out the power supply then, 
that''ll shut it down. Oh wait, there''s more than one. But now they know where you are. Was that a good decision? Because here they come to investigate. 
Will they repair it while I''m dodging?</p>
<p>And so the spiral continues...</p>', NULL, CAST(N'2012-02-16 00:00:00.000' AS DateTime), CAST(N'2012-02-16 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (28, 1, N'On the Up', N'I''m in a better mood regarding the development this week. It''s been a great weekend.', N'<p>Earlier in the month I visited The Bakery Studio in Claremont who agreed to assist in making sound effects for Stingray. That process was completed this week and 
I picked up the sound effects from them. To put it simply, it was worth the very reasonable cost. The engine sounds and stuff were pretty much spot on and went into 
the game without a hitch. Actually, that''s the case for 90% of the sounds. I was quite amazed at the difference it made. Of course there were some technical implementation 
issues as usual, but some intellegent queueing and what-not seems to have solved most of the issues.</p>
<p>And so with that in place, a gameplay video is on the cards, but there''s a few things I need to do before I record one. Chief of which is the controls. The prototype 
control system was a bit too jerky and had a whole host of issues. I''ve come to know how to get around those through all the play testing, but keen viewer eyes will certainly 
pick up on the issues when watching the movie. So I finally pulled out the old (1998) physics text books from varsity and started overhauling. The effect was quite dramatic. It 
actually works very very well, and it''s not super higher-grade flying mechanics either. Well, play-testing will reveal that, but so far it''s smooth and weighty. It has a nice feel. 
However, to alleviate some complexity I''ve also had to build a feed-back system that basically tries to keep the chopper from ascending and descending when there''s no player input. 
All that''s left now is the tail-rotor physics. When all this is done I hope to tune it such that it will be possible to do some very cool evasive and strafe-attack manoeuvres.</p>
<p>I''ve also identified and determined the 10 main story points. This is a great leap forward for me since it will make determining the content and AI scope going forward 
a lot less daunting and random. In fact, I have already drawn up a list of all the various static defence systems that will be in the game. It will include beam weapons :) So go 
dodge that :P</p>
', NULL, CAST(N'2012-02-22 00:00:00.000' AS DateTime), CAST(N'2012-02-22 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (29, 1, N'Animations', N'Animations. I''ve come to realize that this is a very important part of game development. Of course there are various different types of animations, and in this blog post I''m going 
to talk bit about hierarchical ridged body animation.', N'<p>So what exactly does that constitute? Well firstly, I''m not even sure that it''s the correct term - it''s just what I think it''s called. But basically it''s animating objects that 
doesn''t have skeletons. Skeletons and thus character animations through skin modifiers is a whole different sort of animation, and also on a whole different level of complexity. I 
dabbled a bit with that in the MiniLD submission "Mommy there''s monsters". But for Stingray, here, today, I''m not doing that.</p>
<p>What I''m talking about mostly mechanical objects that have different parts, and each part can rotate/move to a certain extent, but is also subject on it''s parent''s rotation/movement. 
A good general example is robot arms used for vehicle manufacture and production line stuff. The upper arm is connected to a base, which can rotate around the vertical, turning it either left 
or right. The upper-arm itself is constrained by it''s mounting point, and can only lift and lower. The lower arm has the same constraints, but is subject to the upper arm. And so on. 
This situation is prevelant in Stingray, specifically with the mounted mini-gun on the front of the chopper. It''s mounted to the body where it can turn around. The gun itself can only 
be lifted or lowered. Together, the movements of the two parts allows the gun to be aimed almost anywhere.</p>
<p>Now, I realise that most middleware solutions (UDK, Unity etc) has implementations of this which is (probably) easy to use and setup. Since I''m not using any of those, I had to build 
a small system to handle that and to configure it quickly. So far it''s not a very robust system, and I''m sure there are many situations which it won''t be able to handle yet. But I''ll 
expand it as I need to. For the moment, however, it''s a simple implementation with a small config that does what I need it to do.</p>
<img src="/content/media/StingrayGunAni.png" alt="StingrayGunAni.png" />
<p>In the image you can see the two different pivot points. The mounting bit, which turns left or right, and then the gun which is linked up off-center from the mounting point. The gun''s 
position is dependent on the left or right rotation of the mounting.</p>
<p>Each object that needs to be included in the hierarchy is listed in the XML, together with which type of animation (rotation or translation) and it''s own constraints in terms of the three 
axis. In this example, the mounting can only rotate around the Y (green) axis, and the gun can only rotate around the X (red) axis. Optionally, each object can then be linked to a previously 
configured item in the list and the constraints of this link, the inherited movement, is also specified in terms of the axis. So the rotation of the mounting around the Y axis causes the gun 
to also rotate around the Y axis. The tricky part here is that the gun''s pivot should be rotated relative to the mount''s position, and not relative to the world origin, and you have to keep 
in mind which transformations is applied before or after the model transformations in the .X file. But like I said, it''s pretty basic, but it''s a solution that works for now and which has a 
very small footprint.</p>
', NULL, CAST(N'2012-03-09 00:00:00.000' AS DateTime), CAST(N'2012-03-09 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (30, 1, N'Quick Visual Update', N'Not much to say in this update. I put the gun in the game proper, fixed a projection bug, and started mucking around with normal maps for the environment.', N'<p>Even though the gun is hardly going to be visible, it had to go in. I really just had to texture it and so one, and started looking at a bug where the gun wouldn''t lift 
higher than a certain point. Quite quickly I noticed that it is similar to another bug on the sight. Turned out it was the Dot product that always takes the smallest angle, and you have 
know when it''s negative or positive. So, solved that.</p>
<img src="/content/media/Gun.png" alt="Gun.png" />
<p>So Saturday I was kinda lethargic and just messed around in photoshop trying out some ideas for the normal textures for the ground and sand. This quickly turned into a quite a bit 
of work that carried on through Sunday, and I''ve only done like 12 tiles worth :) But it least helps to make things look awesome. Here''s the ground and the sand, both obviously most visible in 
low light conditions.</p>
<img src="/content/media/EnvironmentNormalMap_01.png" alt="EnvironmentNormalMap_01.png" />
<img src="/content/media/EnvironmentNormalMap_02.png" alt="EnvironmentNormalMap_02.png" />', NULL, CAST(N'2012-03-12 00:00:00.000' AS DateTime), CAST(N'2012-03-12 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (31, 1, N'More Visual updates', N'In wrapping up preparation for my submission to IndieCade, I''ve spent some more time on visuals. My focus last night was on getting animated textures working, specifically to do good 
normal mapping for the constantly changing water.', N'<p>It might seem superfluous, but these updates just serve to flesh out the experience on the test level. Instead of just having a small piece of land that simply ends in space, the level 
now extendeds all the way to the edges of the map, and this gives a well rounded impression. It will also serve to fill up the space in the final game.</p>
<p>I opted for the solution of the ''sprite sheet'' setup. One 2084 texture with 16 frames of 512px each tiled onto it. The animated texture class finds the correct texture for the current frame, 
and also finds the correct texture for the next frame. The duration of a frame is pre-calculated and then the two frames are transitioned using alpha-blending depending on how far into the 
current frame we are. This means I can easily use only 16 frames for a 5 second loop. Naturally, a screen-shot doesn''t show this off very well :)</p>
<img src="/content/media/WaterShader.png" alt="WaterShader.png" />
<p>
<b>Update: </b>I''ve spent some more time on the water shader and on the assets for the water and also the coast line. The animation is now much smoother and the all the coastline assets 
have the shader applied. To prevent secularity on the sand and non-water bits I had to use a specular value map. This is basically a map with 8 bits per pixel detailing how much specular 
should be applied in the pixel shader. Instead of using a whole texture for this I''ve embedded it into the alpha channels of the existing normal maps. It''s a bit of an extra process, but saves 
a hell of a lot of memory. There are some artefacts along the edges of the tiles, but this is something I can get right with a bit of shader manipulation, so not so much of a worry for me now. 
On to other things!</p>
<img src="/content/media/WaterShader_Coastline.png" alt="WaterShader_Coastline.png" />
', NULL, CAST(N'2012-03-14 00:00:00.000' AS DateTime), CAST(N'2012-03-14 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (32, 1, N'Normal Maps and Sculpting', N'I remodelled the cliffs again. They were too blocky and well, looked crap. Messed around with some normal mapping and then, well, I got sculpting.', N'<p>It was my first time. It took some getting used to, but ultimately I must say I''m super happy with the results of that, and the resulting normal maps that I could make.</p>
<img src="/content/media/ZCliffs.png" alt="ZCliffs.png" />
<p>All that''s left to do now is rework the diffuse maps a bit and then get around to sculpting all the rest of the cliffs. The challenge at the moment is the tiling aspect. 
With the normal maps in the mix it''s difficult to control at the moment particularly along the lines of the cliffs itself. If it does appear very obvious I''ll have to think of something. 
But other than that it''s all lovely.</p>
<p><b>Update</b> Here''s an updated screenshot of the cliffs with a new diffuse texture. You''ll notice that the ground and cliff colours are all a bit more de-saturated and lightened. 
This was thanks to my wife''s suggestion since she didn''t think the colours matched.</p>
<img src="/content/media/ZCliffs02.png" alt="ZCliffs02.png" />
', NULL, CAST(N'2012-03-28 00:00:00.000' AS DateTime), CAST(N'2012-03-28 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (33, 1, N'Polish', N'So with the submission deadline for Indie Cade coming closer, I''ve spent some time polishing up the game. And fixed a lot of bugs. And worked some more on the effects, specifically for night time.', N'<p>Simple stuff like a main menu on the title screen and an in-game pause menu seemed easy enough, but as it turns out there were some real problems with my disposing and switching 
between the different sections of the game. Also, I had a bug in my sound code that caused some sounds to continue playing irrespective of the game being paused. This same bug is also 
what caused some strange sound corruption issues.</p>
<p>The effects I worked on was mostly explosions and other combat effects, like the missiles and rockets. Fighting at night time now proves to be way more epic than day-time, simply 
because of the updated effects. Tracer rounds now fill the sky, and the explosions at night time are pretty awesome to behold, specially on moving vehicles. I''ve added a small integrated 
tutorial as well, the end of which is visible in the shot below.</p>
<img src="/content/media/Night Battle_U.png" alt="Night Battle_U.png" />
<p>I also added actual geometry for the rockets and misses. Both have an illumination map, making their tail-pipes visible during the night. At day time they''re not really that easy 
to spot though, due to size and speed. The missile is visible below the chopper in the following shot.</p>
<img src="/content/media/Night Battle 2_U.png" alt="Night Battle 2_U.png" />
<p>The HUD-integrated pause menu is also visible in these shots. Additionally, when landed, there is another in-game menu in the same fashion where later options to save the game and 
select upgrades will appear. Soon the build for the submission to Indie Cade will be made available to the public, so watch this space.</p>

', NULL, CAST(N'2012-04-14 00:00:00.000' AS DateTime), CAST(N'2012-04-14 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (34, 1, N'Scale Models', N'More modelled content, but not everything''s to scale...', N'<p>I continued to model the ordnance in the game and recently added the flak rounds and the bullets. The references I initially used was the WW2 flak cannons. The most widely used 
AA round was 22mm calibre, and thus I modelled the rounds based on this information. As it turns out, it''s so small that it wasn''t visible in the game at all. This is in contrast with 
the rocket and missile models which are to actual scale. Because those are launched from a position close to the player, they are visible when it matters. However as the player dodges and 
manoeuvres around, the flak rounds were almost never close enough to the player to see. My solution was to make them bigger. They ended up being almost 7 meters long in real world scale, 
and close to .5 meters diameter. This is massive. But to give the player the ability to see them, it was necessary. Similarly, the bullet rounds are the same, almost 7 meters in length.</p>
<p>Even after all this, I found that it looked crap. Previously I had simply drawn lines between the previous and the next positions. This had looked a lot better, and my conclusion was 
that it created the illusion of motion blur. I then adjusted my textures to blend some alpha towards the rear of the AA and bullet models, which did the trick! Additionally, with the 
use of models I''m able to make and apply illumination maps for the tracer rounds as well.</p>
<img src="/content/media/NightBattleTracers_U.png" alt="NightBattleTracers_U.png" />
<p>The bullet rounds are visible on it''s way to the player here, and also some from the chopper being fired. With the models it looks much more realistic than the orange lines did. The 
design decision behind all this is to facilitate the shoot-em-up/dodge-em-up aspect of the game play. Instead of the bullets hitting almost instantaneously, it''s instead very obvious to 
the plater that there''s a burst of rounds coming his/her way, and the speed is balanced such that the player can react to it. Making the visual aspect of this work towards that goal is 
very important. Of course, there''s no such thing as tracer flak rounds as far as I know, so those will not be visible at night and makes night-time engagement a bit more tricky.</p>

', NULL, CAST(N'2012-04-24 00:00:00.000' AS DateTime), CAST(N'2012-04-24 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (35, 1, N'Tweaks and Highlights', N'My wife made a comment about how she struggles to distinguish the HUD from the background. And she had made a good point.', N'<p>So I Googled a bit for images of real-world HUDs, and found that almost all of them are lit up like a christmas tree. The high contrast from the background is what makes all the 
difference. Here''s an example:</p>
<div style="text-align:center"><img src="http://upload.wikimedia.org/wikipedia/commons/7/79/HUD_view.jpg"/></div>
<p>Looking at that it was obvious that I couldn''t just get away with adjusting the color from Lime to something more white. I could maybe have gotten away with adding some gradients 
and clever masking to my PNGs files which I use for the HUD elements, but that wouldn''t have solved my problem for the text. So I started writing a pixel shader. A derivation of the 
Poisson blur and a few pow() and clamp() calls later, and the result seems pretty ok.</p>
<img src="/content/media/HUDEffectDay_U.png" alt="HUDEffectDay_U.png" />
<img src="/content/media/HUDEffectNight_U.png" alt="HUDEffectNight_U.png" />
<p>Obviously this will get tweaked as time goes by. The filter settings is pretty sensitive to the render target size (where a 0.001 shift in UV could be a dramatic shift), and there''s 
some glaring/white-out where there''s overlapping, like on the radar. But all in all I''m pretty happy in a preliminary sort of way.</p>
', NULL, CAST(N'2012-04-25 00:00:00.000' AS DateTime), CAST(N'2012-04-25 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (36, 1, N'Michael Bay-ish', N'As craptastic a title as that might be, stick with me because I think you might like what you''re about to see.', N'<p>Rogue Trooper is a game that''s still on the top of my personal charts. It''s a basic shooter. Fun, and stays fairly close to 
it''s source material. And quite <a href="http://store.steampowered.com/app/7020/">cheap</a> on Steam now. Those are not the 
reasons I like it though.</p>
<p>I like it because of the set-pieces. This is the same reason a lot of people liked Spec Ops: The Line as well. The whole game is 
one long Michael Bay movie. Every level is Optimus-Prime vs Megatron, and this is what I think gives that overall feeling of excellent 
narrative. It''s not really complicated or in-depth narrative at all when taken in isolation, but because of the environment, because of the 
level of involvement in the set-piece, it feels like it has massive narrative. Rogue Trooper didn''t have the tech to do this very well, 
but it is still there. Spec Ops though, that does it all. The vistas, the vertigo, the armoured enemy, the banter and the swearing. Well worth 
the popcorn. It''s also worth noting that the Uncharted series is based on the same ideas, but I''ve never owned a playstation so I don''t have 
personal experience with it.</p>
<p>So what makes for a good set-piece? Well, I''m not an award winning director or anything, but the first thing I''ve identified is size. The 
bigger it is, the better. This is why in the first Far Cry, they plopped down an aircraft carrier instead of a simple abandoned airbase.</p> 
<div class="pictureDiv"><img src="http://cdn.chud.com/6/6c/350x263px-LL-6c3f9bf4_FarCry1--2.jpeg" title="The best bit of the game for me..."/></div>
<p>The 
aircraft carrier is not just long and wide, but from a first-person perspective on the ground, it''s also damn high. That''s a set-piece right there.</p>
<p>The second thing I''ve identified is effects and animation. A dead area is dead. That''s not a meme, it''s a truth. If there''s radars in 
the area, move them about. If there''s a stack, make smoke come out of it. Old doors? Hinge them back and forth a bit. It''s these little nuances 
that really makes it special. For an analogy: Look at the inside of the Pagani Zonda. They could have just gone and stuck some normal air-vents 
in that cabin, but they didn''t. They made it look like steam-punk plumbing, chromed and polished.</p>
<p>So, since my submission to Indie Cade in May I''ve been working on the ten identified set pieces for Stingray. This isn''t something I can post 
about easily though, since it would essentially be spoiler posts. But I wanted to share what''s currently in the foundry. The process of 
constructing one of these set-pieces, the environment of one of these.</p>
<div class="pictureDiv">
<img src="/content/media/Mine_combined_01.jpg" alt="Mine_combined_01.jpg" /></div>
<p>The first is the wireframe of the in-game model, and next to it is the same model as a solid, but colored based on which bits are mapped 
together. Next is the sculpted seriously high-detail model of the same area (12 million faces). This is used to generate the normal maps, 
which when applied together with other (still WIP textures), forms the last image, which is again the low-poly game model as it would look 
in the game.</p>
<p>As I''ve said before, the most time in development is spent making content. And these sort of set-pieces takes the longest of them all. 
The content itself, the maps, the models. But also the animations and effects, testing, the scale. Does the detail level sit on par with the 
rest of the game? What will be the memory requirements for this bit? There''s a whole lot of planning that has to happen, but the pay-off is 
worth it though. If I pull this off, it would hopefully transform Stingray from a simple game to an epic spectacle with 
pantomime and theatre. Something memorable. Something that, hopefully, you will like and talk about.</p>', 72, CAST(N'2012-09-20 00:00:00.000' AS DateTime), CAST(N'2012-09-20 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (37, NULL, N'Blog Entries', N'You might have noticed that all Stingray''s old blog entries has been transferred to the test sight now. While doing it, however, a funny thing happened.', N'<p>As I moved them, I looked at each entry to make sure the images show correctly, and also reread them all. After a while I realized how awesome an experience it was working on Stingray. And what an achievement it actually is.</p>

<p>It also brought back the many hours of work I put into the preparation for IndieCade, and the subsequent feedback of "This is not really an IndieCade type of game". It dawned on me yesterday how much of a disappointment it really was, how big the psychological blow was. And how it resulted in me practically dropping the project almost all together for a time, to take a break.</p>

<p>At the moment I''m not spending any real time on it. Just here and there, and mostly on content which is tedious. I''m not an artist by any means. For me the whole thing so far has been about the challenge. And now, content wise at least, the challenge has evaporated. So I''m really struggling to keep my motivations up.</p>

<p>Maybe there''s another means of getting there...</p>', NULL, CAST(N'2013-01-17 00:00:00.000' AS DateTime), CAST(N'2013-01-17 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (38, NULL, N'Live!', N'Happiness! The test site has been migrated over to the live site. But is the design complete?', N'<p>It''s been a few months in the making, including a complete re-design and re-write, but I''ve finally put my new site over to my new hosting at <a href="https://www.arvixe.com/">Arvixe</a>.</p>
<p>Functionally the changes are mostly for me own benefit. The new site has a light-weight content management system running which is managed through a built-in Admin section. This allows me to manage and create content from inside the site, even from my mobile.</p>
<p>But there''s also been a visual re-design for your benefit. I wanted to bring the site in-line with the current phase of the web, something easy on the eye. My designer friends tell me it looks crap, but I like it, and I hope you do too. Accessibility has also been re-worked a lot. The site scales much better on the smaller mobile screens for instance.</p>
<p>Interactivity has been boosted by a custom built forum for discussions and what-not, but I''m holding back on a comment feature for blog and news posts until I feel it will add value to the site.</p>
<p>I''m pretty sure you''ll find all sorts of small errors and omissions, but I do hope you like your stay here!</p>', 73, CAST(N'2013-01-21 00:00:00.000' AS DateTime), CAST(N'2013-01-21 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (39, NULL, N'Strike Suit Zero', N'Excuse me for punting <a href="http://www.strikesuitzero.com/">someone else''s game</a> this morning, but I want to share this with you. Found it on GOG, out of the blue.', N'<p>Not so much as even a facebook add (which is a damn good thing) alerted me to this game''s existence. But I love it. It''s a bold attempt at the space sim genre again, and they''ve taken their inspiration from all the good places, places that I hold in great standing. Like <a href="http://www.robotech.com/">Robotech</a>. Anyone remember that? I''ve found over the years that absolutely no-one can bring Macross island and the proto-culture to mind.</p>
<p>I haven''t spent too much time with it yet, but what there is seems to be pretty solid. Even the simple mouse/keyboard controls (the same trick I''m trying to pull with Stingray) is easy to master and lends the game such a low barrier of entry.</p>
<p>And then there''s the visuals - yet another punt for the proprietary engine.</p>
<iframe width="560" height="315" src="http://www.youtube.com/embed/gS50TK_Eqxk" frameborder="0" allowfullscreen></iframe>
<p>Get it on <a href="http://www.gog.com/gamecard/strike_suit_zero">GOG</a> or on <a href="http://store.steampowered.com/app/209540/?snr=1_7_suggest__13">Steam</a></p>', 74, CAST(N'2013-01-28 00:00:00.000' AS DateTime), CAST(N'2013-01-28 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (40, NULL, N'Simulate all the things, part II', N'A while ago I posted about <a href="http://www.helloserve.com/News/News/3">Woodcutter Simulator</a>. Well, they''re back...', N'<p>I just discovered they have another title called "Road Construction Simulator". Honestly, I don''t know when they''ll stop capitalizing on their 3D engine and stop releasing games that are no fun to play, what so ever.</p>

<p>Anyway, here''s the youtube...</p>

<iframe width="560" height="315" src="http://www.youtube.com/embed/AIuQoK-5LC4" frameborder="0" allowfullscreen></iframe>', 75, CAST(N'2013-01-30 00:00:00.000' AS DateTime), CAST(N'2013-01-30 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (41, 1, N'XNA? What XNA?', N'Microsoft announced that "... there are no plans for future versions of the XNA product."', N'<p>Apparently there''s a date of April 2014 around this. But, as they said: <i>"XNA Game Studio remains a supported toolset for developing games for Xbox 360, Windows and Windows Phone,"</i> said the representative. <i>"Many developers have found financial success creating Xbox LIVE Indie Games using XNA. However, there are no plans for future versions of the XNA product."</i> Here the article on <a href="http://indiegames.com/2013/02/its_official_xna_is_dead.html">IndieGames.com</a></p>
<p>As these things go, I''m now flogging a dead horse. If the support is not guarenteed, there''s absolutely no point in carrying on with this framework. So, I have a rather big decision to make. Port, or abandon.</p>
<p>Seeing as the whole world has seemingly adopted Unity, that does look like the best use-case at the moment. Although there''s a lot out there, including Crytek''s offering. But all that effort? Starting from scratch now, while I''ve got two other (non game related) projects already going seems a bit too much.</p>
<p>I can''t even begin to hint at a direction in this blog post. It''s all a bit depressing, really.</p>', 76, CAST(N'2013-02-03 00:00:00.000' AS DateTime), CAST(N'2013-02-03 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (42, NULL, N'Presenting Presentation', N'I''m messing around with WPF and plumbing the XAML depths these days.
', N'<p>Creating Windows forms interfaces used be something I enjoyed immensely back in the day. Now with WPF it''s a different ball game, but the challenge remains the same. Recently I''m been struggling with a hierarchically-bound tree view, and dragging and dropping within the same tree view as well as from outside.</p>
<p>Here''s a list of the most tricky bits:
	<ul>
		<li>Once you initiate the action with <b>DragDrop.DoDragDrop()</b>, it doesn''t exit until you drop.</li>
		<li>Once you have dropped, finding on what you dropped is particularly difficult.</li>
	</ul>
	The result of the first point is that no mouse events are forthcoming during the drag operation, so you cannot keep track of the mouse over your tree view to determine the location with <b>InputHitTest()</b>.
	And even if you could, finding the correct destination is tricky. Using a combination of <b>FrameworkElement</b>, <b>UIElement</b> and the <b>VisualTreeHelper</b> resulted in inconsistent results: sometimes it''s a border, other times a textblock. And the value of the attached DataContext is also pretty shaky.
</p>
<p>
	My solution at the end of the day was to hook up the mouse, drag and drop events onto the tree view item HierarchicalDataTemplate static resources directly. This way, the source and destination of my drag operation was always related directly to the sender parameter.
</p>
<p>
	Whilst keeping the event handlers on the tree view itself too, and setting the <b>e.handled</b> parameter correctly, the context of the drop action is exceptionally well defined. If the user drops on one of the tree items the template events will handle it where possible, else it will bubble back up to the tree view itself, where the context of dropping changes, and the item is added or linked differently. And at the same time the case of dropping onto the blank area of the tree is also handled. This works pretty well, and the remaining challenge is managing the data integrity in the background for consistent storage.
</p>
<p>
	So until I''ve got my project page set up, I''ll post a bit about my challenges on the news feed for now. It''s not like there''s any actual newsworthy events happening here at helloserve Productions anyway :)
</p>', 78, CAST(N'2013-10-23 00:00:00.000' AS DateTime), CAST(N'2013-10-23 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (43, 6, N'A foray into "No service history"', N'From bush-mechanic to a real beauty.', N'<p>When I first started looking at MX-5s, I wanted an up-to-date one. Affordability meant I was looking at the NB model (from about 1998 to 2006), since the NC model (2006 to 2015) was still too expensive. Then one day a colleage arrives at work with this lovely blue NA model (1989 to 1998). It belonged to his girlfriend''s dad, who was mostly out of the country, and he drove it occasionally to basically keep the battery charged.</p>
<p>So I drove the car, and made an appointment with the owner on his next visit to the country, and I bought it from him without hesitation. My wife wasn''t prepared for this sudden gut-punch purchase, but of course, she loved it after the first drive as well.</p>
<div style="text-align:center">
<img src="https://lh4.googleusercontent.com/-SSTUUY7I8Ac/S3cNbL13MMI/AAAAAAAAABE/L0nFNCd8vjM/w563-h422-no/DSCN3310.jpg"/>
<img src="https://lh4.googleusercontent.com/-yI_CCfwdtGw/S3cNmuMZJnI/AAAAAAAAABM/exhKKoa1hFE/w563-h422-no/DSCN3312.jpg"/>
<div>
I couldn''t believe I had it in my garage!
</div>
</div>
<p>Then the real issues started cropping up. First though, some history. I am the fifth owner of this car, as far as I know. Back in 1991, a Malawi gentleman imported five examples directly from Japan. Since then, this car was owned by three people, and driven within Malawi. The last of these owners moved the car to South Africa (along with two others) for safe-keeping at his family home in Tableview. This is where I bought that car. Since it was a direct import from Japan, this car is branded as an Eunos (Mazda''s experimental luxury brand at the time, akin to Lexus from Toyota). It also comes with all the stickers, warning labels and else printed in Japanese. And of course, it is right-hand drive.</p>
<p>At this time, my wife had already bought me the workshop manual, and I was pretty familiar with the theory of maintaining it. I thought that I would have to do the odd fix now and then. Boy was I in for a surprise. It didn''t have a service history prior to arriving in SA. Presumably it was serviced and worked on by non-Mazda workshops through-out its lifetime in Malawi. The radiator was brand new, but apart from that, everything else was pretty old, very dirty and in working condition. For about a week.</p>
<p>We were on our way to watch a show when it just died as we entered the parking lot. It got towed home by a friend (very carefully, on the tie-down hooks!!) where we tried to find the problem. By all conclusions it was an electrical one, but we couldn''t find it. What we did find, however, was by-passed fuses, bridged fuses and an horrendous after-market alarm installation. It quickly became apparent that every single part of this car (including the interior) had been worked on by, presumably, people that didn''t have the foggiest idea of how to dissamble anything. So, it had it''s first trip on a flat-bed pickup to the dealer. They found that the main engine relay had burnt out, and replaced it with one that didn''t look like the original, but worked. This was an omen that I didn''t know to interpret correctly.</p>
<div style="text-align:center">
<img src="https://lh4.googleusercontent.com/-_0_-KUA1mgk/S3cOMeVsl3I/AAAAAAAAABo/BCU6TrDzMUM/w563-h422-no/DSCN3316.jpg"/>
</div>
<p>Soon, it was time to service the car. I instructed the dealer to do a full service, including timing belt. Beforehand I shopped around for brake pads, but couldn''t find any. So, I had to order from the UK, my first of many part imports. The dealer didn''t fit the brake pads correctly. The mechanic either broke, lost or took the custom pad-clips, and the car was returned with the pads rattling within the caliper, and generally not performing very well. I took it back, had a few words, and have never taken the car to another workshop for service. I realised then that I would have to learn to do all of it myself. The dealer mechanics are only trained on the new models, and I later found out they had also partially stripped the thread of the timingbelt tensioner''s bolt, in the aliminium block, during that service. So I imported a brake fitment kit, and set out with my first socket set ever.</p>
<div style="text-align:center">
<img src="https://lh4.googleusercontent.com/-Jd1Cz8sjsrw/S3cOSyI0fyI/AAAAAAAAABs/pJ8HQoOA5Js/w563-h422-no/DSCN3368.jpg"/>
<div>
The difference in the clips were quite apparent.
</div>
</div>
<p>
Most of the rest of the year was quite uneventfull. We did regular trips over weekends, and later joined the local branch of the <a href="http://www.mx5ownersclub.co.za/">South African MX-5 Owner''s club</a>. I performed small tasks, like refit the radio completely and hooked up the seat speakers correctly.
</p>', NULL, CAST(N'2015-04-16 11:30:39.000' AS DateTime), CAST(N'2015-04-24 11:07:00.183' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (44, 6, N'A growing familiarity', N'As I was getting more comfortable with the car, and using its available power more succesfuly and more often, the pleasure I derived from driving it was simply staggering.', N'<p>By now I was reading up on the forums and talking to other club members on what other people were doing with their cars. Everyone had stories and advice, and I took some leads from these as to where to direct my focus. I started with the easy stuff. The gear shifter has it''s own oil, seals and maintenance schedule. Mine was in a catastrpophic state. It''s a quick job to fix it, but it required more part imports (the shifter boots and nylon cup).</p>
<div style="text-align:center">
<img src="https://lh4.googleusercontent.com/-c1YnYvRXC48/S3cPEHW1iWI/AAAAAAAAACQ/4eoKoLH90wA/w422-h563-no/DSCN3424.jpg"/>
<img src="https://lh6.googleusercontent.com/-RMDyvM7ZaEc/S3cPMC7GkkI/AAAAAAAAACU/mimUNt7Pg9E/w704-h528-no/DSCN3427.jpg"/>
<img src="https://lh4.googleusercontent.com/-hr-IIManQh4/S3cPRaozYGI/AAAAAAAAACY/SAj7jYvqgIo/w422-h563-no/DSCN3428.jpg"/>
<img src="https://lh4.googleusercontent.com/-1bQPDGHAjPk/VTZ3jGaSAjI/AAAAAAAADFc/AQesWiBiolg/w667-h889-no/2012-11-17%2B10.37.12.jpg"/>
<div>
I had to fish parts of the smaller shifter boot out with long-nose pliers. The nylon cup was also split in two.
</div>
</div>
<p>The shifter update made a good difference to the feel of the car. The more strenuous rubber of the boot made it feel more accurate. This was the first fix to the car that made me feel like it''s turned into a project car. It was also the first fix where I got told off for my dirty clothes by my wife. Since then, that particular t-shirt has turned into my "mechanic shirt".
</p>
<p>Then one day at the office, a collegue reversed into me. I was in the car at the time, fortunately, but it didn''t help. Her husband (who was uninsured, as he was a car trader and held nothing for more than two weeks) didn''t pay me a cent towards the repairs.
</p>
<div style="text-align:center">
<img src="https://lh6.googleusercontent.com/-kb11kE3ScFg/S3cPgQXc4VI/AAAAAAAAACg/eRL3aUY2z4Q/w704-h528-no/DSCN3436.jpg"/>
<div>
The wing was bent at the start of the arch, and the paint crumbled off of the plastic nose-cone where it took the impact.
</div>
</div>
<p>Since the plastic of the bumper was already 20 years old by this time, I insisted on a new one. The clamps on the old one showed the tell-tale white strain where it had bent. But he would have none of it. In the end, I got a new wing and a new nose, but I paid for it myself. So naturally, I fitted it myself to save costs.
</p>
<div style="text-align:center">
<img src="https://lh6.googleusercontent.com/-y9xMQBfe__o/S3cPxHqq0XI/AAAAAAAAACo/RezJiIfo_MU/w704-h528-no/DSCN3440.jpg"/>
<img src="https://lh4.googleusercontent.com/-PUHtJQwvdsQ/S3cP3zgCXII/AAAAAAAAACs/QGjsd89O88o/w422-h563-no/DSCN3444.jpg"/>
<div>
The fixtures of the nose-cone was rather...extended.
</div>
</div>
<p>Prior to this, there was a vibration that had developed along the drive-train, and by now, this was getting more severe every day. It got better for a while after some hard driving, but would become gradually worse again. This cycle repeated, and shortened as time went on. The workshop manual talked about the two different crank nose designs, and it became clear that I had the early sort, and was suffering from the well documented short-nose crank problem. I concluded that under hard driving, since the engine''s rotation is against the thread of the crank''s centre bolt, it tightned itself sufficiently to alleviate the problem. But at idle and normal driving the strain of the belts was enough to gradually loosen it again. This gave the car somewhat of a mood, and it started to develop a sort of a personality. There were a few solutions to this problem.
</p>
<p>
I opted to try the "lock-tight" fix. This was the first time I had to strip the engine all the way to the crank pulley. It was a tremendous experience, until I came upon that tensioner''s stripped bolt. Anyway, the workshop manual really helped a lot here, and soon I was putting my broken baby back together again. So, a bit of background: this problem with the crank erodes the key that fits the pully to the crank nose itself. So, to perform this fix, you have to fit a new key that holds the pully onto the crank. Naturally I ordered a new one.
</p>
<div style="text-align:center">
<img src="https://lh4.googleusercontent.com/-XM9nllQ_sxo/S3cRJxeTcgI/AAAAAAAAADY/hVSVSIt-jbg/w864-h576-no/IMG_0994.jpg"/>
<div>
The engine stripped down to the crank nose; a similar effort is required to replace the timing belt
</div>
</div>
<p>
This is when I learnt that one of the previous owners had already suffered from this problem and had his bush-mechanic attempt a fix of a completely different nature. I presume they could only find a key that came from a tractor or a pickup truck, because the key I took out, compared to the one I had ordered, was very different in size. So, to make their one fit, they had manually extended the slot in the crank that the key fit in, and manually filed at the crank pully so that it would go over the bigger key. Nothing I could do with the proper sized key would work. So I had a new key custom made to fit the crank slot, and set about with the lock-tight. This fix lasted for about 12 000 Km. And when it started acting up again, it was three times as bad as before. The custom key had completely broken, ruined the pully and almost took out the one wall of the crank slot completely. This crank was done with life.
</p>
<div style="text-align:center">
<img src="https://lh4.googleusercontent.com/-gJjVmR11zJQ/S3cRW0lBH6I/AAAAAAAAADc/JjJX4jFuOwI/w864-h576-no/IMG_0995.jpg"/>
<div>
The old (bigger) key compared to the real (ordered) thing. You can see where the old key was eroded
</div>
</div>
<p>I had the option to replace the crank, which would involve a lot of labour and refitment. It''s a complete engine-out job though. Or I could just get a new engine. In Japan they chop cars up after 80 000 Km, and some companies get hold of these cars'' engines and gear boxes for export. I got a second-hand engine (and gearbox, since all the vibrations had ruined mine) from one of these chops and had it fitted. I also supplied a new clutch. This new engine, since it was a later model, sported the updated long-nose crank, so this problem will never reoccur again. I had my baby back, and it was better than ever with that new clutch. It dawned on my then that what I had actually done by buying this car was take in a rescue dog, and I was busy nursing it back to health.
</p>', NULL, CAST(N'2015-04-16 12:02:50.000' AS DateTime), CAST(N'2015-04-22 07:08:48.323' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (45, 6, N'Getting it road worthy', N'When I bought the car, there was almost a full year left on the plates, and I simply transferred it to my name. But this period was fast approaching, and I made an appointment with the AA for a full inspection.', N'<p>The condition of the car deteriorated with daily use. Knocking noises started appearing in the suspension under cornering and from the back there were some very faint bearing noises. I sort of expected that the car wasn''t actually road worthy, and a test at the AA proved my suspicion. Because I bought the car it did require a new road worthy certificate. Now at least I had a list of immediate issues to look at.</p>
<p>After the calamities with the engine and its subsequent replacement, a few of the items on that list were knocked off, one of which was oil seeping out of the crank seal at the front. Presumably the same happened at the back, since the old clutch was slipping quite often. Mostly though, the problems were all due the car having sat in a garage for a long time. When this happens, the rubbers start to loose their suppleness and become brittle. So when it actually gets to being driven again, the rubbers immediately starts tearing and breaking apart under the stresses.</p>
<p>My shopping list was basically a refitting of the entire undercarriage: New polyurethane bushes and anti roll-bar mounts, new ball joints and new dampers. The old dampers didn''t leak oil, but they were 20 years old by now and had seen a significant amount of gravel road.</p>
<p>The effort of replacing all the bushes was outside the scope of my own abilities and the tools in my garage. The poly bushes are very difficult to get into the wishbone mounts, so I delivered the car to a shop where they could use a press. They didn''t do a very good job though. The bushes were fitted well enough, but the rear anti roll-bar lost one of its bolts and I found another bolt on the one wishbone that also hadn''t been fastened properly either. I then subsequently heard, quite by coincidence, that this shop refurbished the brakes on another car which failed mere days afterwards and resulted in a terrible accident. So of course I double checked everything on the undercarriage. Real sloppy work.</p>
<p>So after the bushes were sorted out, I turned my attention to the bearing noise that permeated into every drive. Initially I wasn''t too sure that it was actually a wheel bearing, and opted to first replace the rear brake disks and pads. They could have been warped at some point and anyway, the pads were done, so it needed doing.</p>
<div style="text-align:center">
<img src="https://lh6.googleusercontent.com/-2UdaE-Yz2MQ/VTZyGq3yN9I/AAAAAAAADFc/P271w-tvf14/w1334-h889-no/IMG_1581.JPG"/>
<div>
The green stuff fitted to the rear.
</div>
</div>
<p>As it turned out, it wasn''t the brakes, but the hubs are rather expensive and weren''t necessary for the road worthy certificate, so I let it be for now.</p>
<p>The lower ball joints on the front are sold separately, so I got those new. But the upper ones are only sold attached to the upper wishbone. This makes it rather expensive (and also rather silly, because the wishbone itself doesn''t really deteriorate). These I had refurbished instead. For the dampers and springs I borrowed a compressing tool and set about building the new dampers and springs. It is rather difficult to manage when you''re working on the floor. I had ordered Koni sport dampers, which are adjustable in height and stiffness. The poly bushes had made the ride so stiff all on their own, that I didn''t really mess with the damper settings at all. Lowering the car would have made the ride way too hard. Remember that this car weighs less than 1 ton, and the poly bushes props it up so well it doesn''t lean into the corners nearly as much as with stock rubber. So, the dampers were really just an extra, and for peace of mind. Also, I''m not into stancing or any of that stuff. This car was meant to be a daily.</p>
<div style="text-align:center">
<img src="https://lh4.googleusercontent.com/-aSTW-f6vGMw/VTZ36TxkNKI/AAAAAAAADFc/qZ_bpuOAj6U/w667-h889-no/2012-09-24%2B10.32.55.jpg"/>
<img src="https://lh4.googleusercontent.com/-PI7m-5RABPU/VTZzYexdLOI/AAAAAAAADFc/6BXtnT1poEA/w1185-h889-no/2011-03-12%2B12.07.08.jpg"/>
<img src="https://lh6.googleusercontent.com/-gzF9Uiv4krw/VTZzBAnXJ2I/AAAAAAAADFc/g_hSdqTpOhg/w1334-h889-no/IMG_1585.JPG"/>
<div>
Working on the dampers and ball joints
</div>
</div>
<p>One of the problems of old cars like this is rusted nuts and fasteners. The one front damper unit wouldn''t loosen on the top cover. It had rusted down so bad that I couldn''t dismantle the damper unit at all. I moved on to the rears units, and by the time I completed and fitted the those it was already mid Saturday afternoon. I needed the car ready on Monday morning for work. At this point I desperately called in some friends to come and help. The solution finally turned out to simply split the rusted nut using a chisel. This of course woke up all the neighbours on the Sunday morning, and the chisel was totally wrecked. And I had lost a nut, which is actually a bigger problem than you might think. You don''t just go to the hardware store and buy nuts to use. These nuts'' and bolts'' thread are not compatible with general hardware store types, and I don''t have a huge stash of lost nuts and bolts like many workshops have. I can''t remember where we got another nut that fit though, but fortunately it was all sorted out by lunch time on the Sunday.</p>
<p>After this the car passed roadworthy, but there were a few other things that needed attention, and naturally some more surprises were in store for me.</p>', NULL, CAST(N'2015-04-16 13:11:55.000' AS DateTime), CAST(N'2015-08-01 19:16:45.327' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (46, 6, N'Driving a (modern) classic', N'Official bodies (racing, club, secretariats) won''t classify the NA Miata as a classic yet, even though it''s now well past 20 years. I''ve heard of it being referred to instead as a "modern classic" though. I don''t really care either way, I''m just here to drive and own this car.', N'<p>Using this car on a daily basis is simply one of the best continual memories I have of it. It being this old however comes with a set of problems that you constantly have to watch out for. One of these is cooling.</p>
<p>When I bought the car, the radiator was almost brand new. I didn''t think I''d have a problem with cooling, ever. But, the South African climate can sometimes roll you a nasty one. It was one of those heat wave periods and while sitting at a light on my way home, I suddenly noticed that the temperature gauge was right off the scale. Something was wrong. But it was the last stop on my way back and I had less than a kilometre to go. So I pushed through. There was still plenty of water left in the radiator as I found out when I made the rookie mistake of immediately taking off the radiator cap to check, which resulted in me almost ending up with third degree burns all over my face.</p>
<p>The problem was the thermostat. It had given up and locked off the circulation so that the water around the engine couldn''t get back to the radiator. Driving around like this is a problem, not least because you''re putting the water pump under tremendous pressure.</p>
<p>So of course I had to order a thermostat. I opted for a cheap Chinese knock-off. It''s still going to this day. Naturally it would take a while to arrive, and I had to get to work the next day, so I simply took out the thermostat. This is no problem, the only effect is that the engine takes much longer to get up to working temperature, so I had to nurse it and curb my enthusiasm for much longer. This added yet another flavour to my driving this car which I remember fondly. A few days later though, still in this heat wave, I noticed a hissing sound while waiting for a friend, and popped the hood right there in the shopping centre parking lot. I found that one of the hoses had sprung a leak. This was obviously because of the pressure build-up on that last stretch home. It was a tiny hole, but it could spell disaster at any time.</p>
<p>Fortunately it was the hose feeding the heater under the dashboard. This meant that it was easy to by-pass it and stick it into the back of the engine to complete the flow. A friend helped to get a specially made hose for this, and I drove around with this by-pass until my order of a complete set of silicone hoses arrived.</p>
<div style="text-align:center">
<img src="https://lh3.googleusercontent.com/-dDVXd9KeNus/TS3vAHpyPCI/AAAAAAAAAlg/j_SycRCRkUg/w691-h461-no/IMG_1540.JPG"/>
<div>
The by-pass hose visible at the back of the engine, and the two ports on the firewall that wasn''t connected any more.
</div>
</div>
<p>The silicone hoses took about a morning to fit, and there are two small pipes that I just couldn''t get too, and thus never replaced. But the I think the yellow pipes offset against the engine and blue bay looks absolutely brilliant. One problem I had was with the hose clips. These things are super finicky (perhaps because of the age) even with hose-clip pliers. In the end I simply replaced all of it with proper plumber fasteners.</p>
<div style="text-align:center">
<img src="https://lh4.googleusercontent.com/-IjBJbMzpYVg/VTZy7GVcEjI/AAAAAAAADFc/TNIfhe_KeDU/w1334-h889-no/IMG_1584.JPG"/>
<div>
Heater hoses being fitted.
</div>
</div>
<p>By now, the wheel bearing noise were much worse, and so I had to get cracking on the hubs. I had to buy a special tool to pull the hubs off of the axles (and almost totally wrecked the one centre bolt completely!). I couldn''t pull the bearings from the hubs myself, or fit the new ones. My father-in-law took it all to a guy that had a press who was kind enough to assist.</p>
<div style="text-align:center">
<img src="https://lh5.googleusercontent.com/bukEwLvPO8Y3bfE_Yw3WkqtNZdnEx6D-ih3ctVV8Smg=w593-h889-no"/>
<div>
The one rear hub pulled off the axle and lying on the ground.
</div>
</div>
<p>So at this point the under carriage was almost completely refurbished or replaced, apart from the front brake disks. I had total confidence in the car''s long range capability now. But before we move on, the fabric top ripped up during a highway blast. Instead of ordering a Robins or OEM top, I took it to a local upholsterer, together with a new rain-rail, who did a fairly decent job for a third of the price (including fitting). It''s still looking neat and is weathering really well almost 3 years later.</p>', NULL, CAST(N'2015-04-22 07:36:29.000' AS DateTime), CAST(N'2015-08-01 19:20:04.973' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (47, 6, N'To Windhoek and back', N'So now we get to our first proper road trip in this car. To date we''ve mainly meandered around the greater Cape Peninsula, and out into the Overberg and surrounds once or twice. This trip was different. It was going to be more than two thousand kilometres in total.', N'<p>We were going up for a wedding, and had decided to make a two week holiday out of it. There were several friends going as well, all on different routes and times and we would meet up in Windhoek. But how do you pack two peoples'' two weeks worth of luggage into an MX-5? You don''t, you use a boot-rack instead. I had borrowed one from a fellow club member. But clothing and toiletries was just the start. There is one thing about the NA MX-5 that no engineer can get around - the size of the wheels. They don''t fit in the boot. The car comes with a minispare (or as we call it, a Marie-biscuit), which is fine. But what do you do with the proper wheel then if you have a passenger? So I opted for a bunch of rescue gear instead, and threw out the spare completely. This included a heavy-duty off-road type compressor (the sort that hooks up directly to the battery instead of plugging into the cigarette lighter), lots of tyre-plugs and also tyre-goop. I was fairly determined not to be stranded in the middle of nowhere.</p>
<div style="text-align:center">
<img src="https://lh4.googleusercontent.com/-bKvBTvlL57w/VTZzwlW4JiI/AAAAAAAADFc/FN75vbclwPA/w1334-h889-no/IMG_1618.JPG"/>
<img src="https://lh6.googleusercontent.com/-Jlh52wdodYw/VTZznLv8iCI/AAAAAAAADFc/7N6262wAddg/w1185-h889-no/2011-04-19%2B10.34.20.jpg"/>
<div>
The boot rack fitted and loaded. The boot itself was chock''n block full of stuff.
</div>
</div>
<p>I decided to take the straight and boring route - the N7. Now, the problem with any of the national routes in South Africa is the cargo hauls. Since the demise of the railways, these routes have become the main arteries of the cargo business. The N7 trails along the west coast where <a href="http://en.wikipedia.org/wiki/Acacia_saligna">port jackson</a> bushes and wheat farmland is exchanged for fruit trees and mountainous greenery as you pass over the Piekenierskloof pass. This is a narrow and terrible pass. There''s been road works on it for as long as I can remember, and there are almost no overtaking areas to get past the slow, thundering and black exhaust-spewing lorries as they try to get over this mountainous area. Four years later and I have yet to cross it again; hopefully it''s better now.</p>
<p>I must state that this car is simply epic on the long road. Sure, it''s engine isn''t a creamy V6 and the gearbox isn''t a smooth and hassle-free auto, but the car is solid on the road and you get tremendous control through the quick steering rack. All of this sounds counter-intuitive, but compare this to something like a simple <a href="http://en.wikipedia.org/wiki/Ford_Figo">Ford Figo</a> for instance (a much more modern car) which rolls around in a cross wind, pitches and dips over any sort of bump and has vague steering which requires constant flailing to ensure you go straight on any sort of road camber. It''s truly tiresome to drive something like the Figo for an extended amount of time. The MX-5 though, not so.</p>
<p>Soon the valley of fruits and honey are given up as you pass Vanrhynsdorp. The end of the Karoo plateau is also on your right now, and the arid climate starts to take over here. Small, grey shrubs dot the landscape, and it''s hot. The little car was simply stellar, the engine was singing and the wind rushing past. We had the roof up for the most part from the sun, but the air conditioning in our car had long ago puffed the last of its gas. The one thing that made up for that is the seats. They are supreme and my wife falls asleep in them in five minutes flat. And so we pulled up in Garies for a loo break. We had driven for the most part of the day, almost 530 kilometres.</p>
<div style="text-align:center">
<img src="https://lh6.googleusercontent.com/-a3lQOJGOzdw/VTZzwL6j5jI/AAAAAAAADFc/9N3gaMlkrP4/w1185-h889-no/2011-04-19%2B17.28.00.jpg"/>
<img src="https://lh6.googleusercontent.com/-1d7AlrUhFqQ/VTZz-EJi5wI/AAAAAAAADFc/zcslTpPlzYE/w1185-h889-no/2011-04-19%2B17.28.24.jpg"/>
<div>
Parked at a fuel station and restaurant in Garies. The sun was low by now.
</div>
</div>
<p>I wanted to reach Springbok before night-fall - another 120 kilometres. So we got back into the car and... nothing. It was dead. It wouldn''t turn over, although all the other electrics were working. I poked around a bit, but there wasn''t anything I could do without tools really. We decided to spend the night at the B&B right next to the fuel station. It was solemn, and my wife tried to console me. The beer helped a bit too. In the morning, a cat had urinated on the soft top, and I got hold of the owner of the fuel station (and the B&B). He had a workshop at the back. As it turned out, he was a former employee at a BMW service centre somewhere in the city, and had retired here and was applying his trade in the old-fashioned way. Meaning he actually fixed stuff instead of simply replacing parts. And of course, I wasn''t his only customer that morning. In a small town, on the edge of the greatest plain in South Africa, this guy was having a Thursday morning to beat any other while we were having breakfast in his restaurant.</p>
<p>He had the fuel-pump out to see if that had given up, but ultimately he found that the main relay had popped. This was an ''a-ha'' moment for me. I had completely forgotten about that first breakdown, and had driven the car constantly since then for almost two years, so this did come as a surprise. To get under way he helped me make up a by-pass for the relay using a 10 Amp fuse. Ultimately I found it rather humorous, this make-shift fix for what had turned out to be this make-shift car.</p>
<div style="text-align:center">
<img src="https://lh6.googleusercontent.com/-WKnOpN3Gu5A/VTZ0SPRLnWI/AAAAAAAADFc/ubyBjs3mmto/w1185-h889-no/2011-04-20%2B11.02.36.jpg"/>
<div>
The by-pass plugged into the relay box. This saw us through the rest of the holiday and all the way back home.
</div>
</div>
<p>At Springbok I tried to shop for a new relay, but to no avail, and we just set off for the border with Namibia where we arrived late afternoon. Here, things almost went awry. I suspect that the South African police officer saw the history of the car on his computer, that it had been imported from Malawi. This he figured gave him an ideal opportunity, since he immediately stated that the car had been reported stolen in Malawi. Of course, I''m not a regular border-crosser, so this came as huge surprise to me (and I only realised later he was surely looking for a bribe). I was on the phone to my colleague from who''s in-laws I bought the car, but fortunately I had all the paper work and clearances for the new engine and everything with me, to which this warrant-officer William surrendered his claim and simply said "It''s fine". We were stamped and through the Namibian side in less than 15 minutes. We spent the night at the Orange River Lodge on the Namibian side.</p>
<p>The next morning we set off for... <a href="http://en.wikipedia.org/wiki/Ai-Ais_Hot_Springs">Ai-Ais</a>. My wife insisted that we should go there, so I turned onto the C-grade gravel road and set off into the reserve. The wash-board roads were terrible, and it shook us to pieces as we went down into the fish river valley. After a while I even stopped caring about the car as we burst out laughing for the insane situation we had put ourselves in. Here we were in probably one of the most unsuitable cars for this trip, on a gravel road in the middle of a reserve, getting our teeth rattled from their sockets. So when we reached the resort, we weren''t at all surprised to have to park between Land Rovers, Cruisers, Fortuners and all manner of off-road trailers attached to each one.</p>
<div style="text-align:center">
<img src="https://lh6.googleusercontent.com/-YXhtiYFkhuE/VTZ4id4bHiI/AAAAAAAADFc/BDinWhAuva8/w1334-h889-no/IMG_1671.JPG"/>
<div>
Waiting at the entrance to Ai-Ais.
</div>
</div>
<p>We got a plethora of comments from the other ''hard-core'' resort visitors in their massive pick ups, mostly the "What the fuck!?" sort. Still, we enjoyed the resort as day visitors. It was rather tranquil, my wife went for a swim in the various pools and hot springs, and we had some drinks. Then it was off again. She wanted to see the Fish-river canyon, we needed to reach Keetmanshoop before nightfall, and I actually had no idea how long or far we were from either. From here the roads became much worse. There had been a massive storm earlier in the week, and it was supposedly still raining in the northern parts of the country. The gravel roads showed the extent of the flash floods and rivers. In general, C-grade roads in Namibia is the equivalent of 80 to 120 km/h tar roads, but not on this occasion. Huge swaths of veld had been taken by the rains and been run over the road, causing muddy or sandy pits, or massive rock-hard settled sand lumps, spanning the entire width of the road. Since the car is low, and rear-wheel drive, I was scared of getting stuck, and no amount of rescue gear would allow me to get us out again on my own. So, unless I was pretty sure it wasn''t the rock-hard sand lumps, I simply floored it, relying on momentum to carry us through the longer stretches of mud or sand. It was truly hair raising, and at one point I basically sand-boarded the car across one one of these pits, flat on it''s engine-cover belly and chassis. Sand went everywhere, and later I wiped some off the top of the engine, from between the cams.</p>
<div style="text-align:center">
<img src="https://lh4.googleusercontent.com/-3C_pDbxg3E0/VTZ4h5UyS3I/AAAAAAAADFc/e-NmX9v0xLM/w1334-h889-no/IMG_1674.JPG"/>
<div>
A supposedly smooth C-Grade road where rocks had been deposited by the flash floods.
</div>
</div>
<p>The C-grade road became so bad that I thought we were driving on a bed of rocks instead. But, soon we had to turn off from this onto a D-grade road to get to the Fish-river canyon lookout point. This road forced me down to 20 or 30 km/h. We didn''t time it, but I reckoned it took us almost 2 hours to do the in and out legs of this 15 kilometre stretch. I was in a pretty foul mood by now. The car was suffering badly, those fancy new Koni sport dampers were getting hammered, the engine and gearbox mountings were getting hammered, and it was hot. It was really hot, and dusty. Our visit to the lookout point was, in a word, disappointing. The lookout point itself was unstaffed and there were no refreshments available in a tuck shop or otherwise. The view though... that was spectacular. The depth and width of the canyon is on a scale that neither I nor a photograph can convey. This is definitely a place that I would want to visit again, and would want to hike as well.</p>
<p>After we made our way out from the lookout, we turned north again and hoped to stumble across some civilisation. By now I had done 250km since the border, which on a smooth tar road would be almost half a tank. On these roads, however, with all the braking and slowing down it wasn''t, and I had no idea how far we still had to go. To make it worse, we encountered several junctions (which I figured we needed to take) that was closed because of the rains and the damage to the roads. So we ploughed on, and while stopping for a loo break, I noticed that the boot rack had given up.</p>
<div style="text-align:center">
<img src="https://lh4.googleusercontent.com/-TwWqJhXecuk/VTZ5VnZcpyI/AAAAAAAADFc/HSzzZWZ-EHc/w1334-h889-no/IMG_1692.JPG"/>
<img src="https://lh4.googleusercontent.com/--9A-8PpBAb0/VTZ5K4o5xTI/AAAAAAAADFc/-Qp64AfVHo4/w1334-h889-no/IMG_1698.JPG"/>
<img src=""/>
<div>
The boot rack with a split strut. Here the roads were better again, in the expected condition. You can see the damage to the paint that the suction cups had caused because of the dust. I twisted the strut around to better support the weight. The strut had also gauged into the paint where it had collapsed.
</div>
</div>
<p>After this calamity, I really couldn''t care any more. There was nothing I wanted more now than an ice cold beer and a shower. Somehow, our moods had improved though, such is the charm of this little car, even on these roads. It was late afternoon, we needed to find petrol soon, and we still had to find a B&B in Keetmanshoop. Then we were forced, due to road closures, back onto a D-grade road again, and suddenly we came upon a dam wall in the middle of nowhere. To me it seemed... magical, something man-made, something major. It was unbelievable. In reality though, it was the Naute reservoir, and we were very close to the tar road linking Keetmanshoop and Luderitz. We had made it!</p>
<div style="text-align:center">
<img src="https://lh4.googleusercontent.com/-6jEFAMKNKmc/VTZ5NBrSm9I/AAAAAAAADFc/isSrEmapvrA/w1334-h889-no/IMG_1700.JPG"/>
<img src="https://lh4.googleusercontent.com/-3AAzGnm7X7Q/VTZ6XpUm57I/AAAAAAAADFc/cPNfn_9eqfI/w1334-h889-no/IMG_1702.JPG"/>
<div>
Notice the water-line bridge we had to cross. Fortunately it wasn''t a full stream of water.
</div>
</div>
<p>That evening we spent in Keetmanshoop and slept well, after a lot of beer. You can buy beer in any shop in Namibia (what a blessing!). In the morning we set off for Windhoek, sticking to the B1 national road. This is a busy cargo road again, and after all those rains, the pot holes could have swallowed any of those trucks whole. It wasn''t easy to maintain pace, I would have absolutely lost a wheel had I struck one of them. It was also getting cooler as we were catching up to that storm that had been raging in the south a few days prior, and in Windhoek itself we encountered a hail storm. My wife parked the car under a tree for the duration, and after arriving at the self-catering, it was covered in leaves and branches.</p>
<div style="text-align:center">
<img src="https://lh4.googleusercontent.com/-b-GA5wLTe0c/VTZ6aNeXDHI/AAAAAAAADFc/127sKVKmU7E/w1334-h889-no/IMG_1825.JPG"/>
</div>
<p>From this point on, after we had met up with everyone, we rented a big Toyota truck to travel together, so we parked the little MX-5 at our friends'' place. Two weeks later and we were on our way back, hauling the tar roads and making lots of progress. I had stuffed most of our luggage in with friends, so we were now travelling without the load on the boot. It was also discovered, at a roadblock, that my license had expired, so my wife was doing all the driving. The problem with the main road seemed that fuel consumption is actually worse, overtaking trucks and other cars. With the small fuel tank, we had to literally stop at every town to refuel, even if there was still half a tank left; there was no guarantee that half a tank would get us to the next town. But it was plain sailing, and within one day we were back down at the lodge at the border, and the following day back home. The car had performed amicably, the undercarriage had stood the test of grade C and D roads superbly, and my only loss was to replace Steve''s boot rack and the damage to the paint. I even drove around with the relay by-pass for at least another 6 months. And then I drove to the shop that night. It had rained just after we arrived back home, so the roads were wet. I turned left on an arrow at the traffic light and promptly when into full opposite lock to get it straight. It must have looked superb from outside, but I immediately knew what had happened.</p>
<div style="text-align:center">
<img src="https://lh4.googleusercontent.com/-rtELjB3yfCA/VTZ0oNQggJI/AAAAAAAADFc/wVuojA5uNnE/w1185-h889-no/2011-05-04%2B10.41.03.jpg"/>
<img src="https://lh4.googleusercontent.com/-im2MbrHGlf4/VTZ00y9XRHI/AAAAAAAADFc/-t-NW8BvYiM/w1185-h889-no/2011-05-04%2B10.41.22.jpg"/>
<div>
My two rear tires where almost slicks.
</div>
</div>
<p>I''m not sure if it was the extra weight over the boot, the softer Bridgestone Potenza compound, or a combination of both, but there was literally nothing left of my two rear tyres. I couldn''t believe that we had completed a 2000km trip, coming out on tires that looked like this in the end. But, we had, and now I had to replace them. It was the most expensive part of the trip, by far.</p>', NULL, CAST(N'2015-04-23 06:41:16.000' AS DateTime), CAST(N'2015-08-01 19:33:34.447' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (48, 6, N'Post-trip maintenance', N'After the beating the car took in Namibia, I was totally expecting multiple failures on multiple components. There were some.', N'<p>Replacing the two rear tyres naturally included a spot of alignment, and the guy showed me some play in the steering rack. It wasn''t a catastrophe, or urgent, by any means but indeed something that needed to be looked at. Later I noticed some oil on the garage floor, at the rear of the car. Closer inspection showed that the differential was leaking, and I could see a small stone (typically used in resurfacing roads) was lodged between the diff and the one axle, obviously damaging the seal. I suspect this was picked up during the road works on the Piekenierskloof pass which we encountered on the way up to Namibia. It had withstood 2000 km worth or travel, so I wasn''t going to delay getting this looked at. At best, the diff was simply low on oil, at worst, the gears would have been ground smooth due to heat build-up. I got new seals from the local dealer and took the car to a workshop. Why did I take it to a shop? Well, there are two things I don''t really feel I can handle - gearboxes and differentials. After this effort though, and while observing the mechanic, I''ll take on the differential by myself now without hesitation.</p>
<p>A little later, while doing rotation, the steering rack issue came up again. By now I''d pretty much committed to keeping the car for ever, and solutions to problems needed to be long-term and lasting. So, I ordered a completely new steering rack. It took some doing removing the old one, and even more doing to fit the new one, but with some help I got it aligned perfectly.</p>
<div style="text-align:center">
<img src="https://lh6.googleusercontent.com/-TCOfmaYacUQ/VTZ01gRKrmI/AAAAAAAADFc/V8R2H2NVAI4/w1185-h889-no/2012-06-17%2B10.14.34.jpg"/>
<div>
The old and new racks next to each other.
</div>
</div>
<p>By now I had quit the owners'' club after a year of being chairman of it to spend every waking hour on <a href="http://www.helloserve.co.za/project/stingray-incursion">Stingray Incursion</a> instead. I was driving the car every day still, right until it popped the new relay I had fitted after the Namibia trip. I got another one, which lasted about 3 months, but by now it wasn''t popping the relays any more, it was just losing connectivity as a whole. It was very frustrating, and it resulted in me not really being able to drive the car reliably, since it would just cut out anywhere at any time. The wiring underneath the main fuse box in the engine bay was shot, corroded and burning. It had to be addressed.</p>
<p>So, I roped in my good electronic engineer friend who, at the time, happened to be working at an auto-electrician workshop. He helped to rewire the main loom and split the main relay out to a new mount we made on the firewall, next to the master brake cylinder. Good as new. But, because of the logistical problems, we used some of our savings to buy a cheap small car as a temporary measure. Now the blue car was practically garaged for weekends only. It also needed a service.</p>', NULL, CAST(N'2015-04-23 13:18:57.000' AS DateTime), CAST(N'2015-08-01 19:38:23.050' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (1048, 6, N'Ceres trip', N'Not so much a road trip this time, but we did do a lot of driving.', N'<p>The Ceres valley is rather unique among the different South African climates. Nestled in between the Matroosberg range and the Koue Bokkeveld plateau, it''s an agriculturally rich area with specific farming specialization, and it''s only accessible via mountain passes. We stayed at the <a href="http://www.cherryfarm.co.za/">Klondyke cherry farm</a>, which is also at the top of a tremendous little mountain pass. In short, the Ceres valley is a bit of a haven for the driving enthusiast</p>
<p>The main road into Ceres is via Mitchell''s pass. This is a wide road that carries a lot of cargo as the farmers truck their loads in and out (Ceres has several cold storage facilities too). As a result, the pass is heavily congested at peak times, but off-peak it''s a super pass to drive. It''s smooth, it''s got excellent bends and great camber. Driving this road is easy and relaxing. You have plenty of time to prepare for the bends and to get your gear changes sorted out to maintain pace through the wide lanes. And then there''s ample acceleration stretches between the bends too.</p>
<div style="text-align:center">
<img src="https://lh4.googleusercontent.com/-JlfUQzyQOU0/VTZ15NJ51hI/AAAAAAAADFc/9nNG6qng9F4/w1334-h889-no/IMG_2273.JPG"/>
<img src="https://lh6.googleusercontent.com/-o24yiLG9hnc/VTZ2BZInT1I/AAAAAAAADFc/KfeWpZnc-l8/w1334-h889-no/IMG_2276.JPG"/>
<div>
The Tolhuis bistro on Mitchell''s pass is pretty good, although they get regular visits from baboons and other wildlife.
</div>
</div>
<p>Remember that Ceres is considered a rural town. At the fuel station everyone stopped to ask questions about the car. I am always surprised at the response this little 1.6L Mazda invokes from folk when we travel. And it''s always funny to see their faces when they realise it''s only a 1.6L, and not some fire-breathing V8 that makes a ton of power. The perception and reality around this little car is very far apart. When we reached the cherry farm, the environment was so tranquil it was actually surreal. It was also bitterly cold. The cherry farm is on top of the Matroosberg range, right next to the reserve, so you''re easily more than 2 km up. And of course, we went in June, hoping to be there when the snow comes. The cottage and the bigger guest house is old, but the fire place and the constant supply of firewood made this a truly romantic experience. There''s also very nice hiking trails on the farm.</p>
<div style="text-align:center">
<img src="https://lh6.googleusercontent.com/-fysaeD5qBpA/VTZ35G2OaMI/AAAAAAAADFc/ejaXskFWXIw/w1334-h889-no/IMG_2292.JPG"/>
<div>
Parked up next to the cottage. The clear air came at a very low temperature. No boot-rack this time.
</div>
</div>
<p>The areas around Ceres is also well worth a visit, and of course it requires driving out via any one of the passes. The R43 between Worcester and Wolseley sports an excellent old train bridge, and an Anglo block house, built by the British very long ago. Tulbagh is picture pretty, and the <a href="http://www.tulbaghwine.co.za/ourwines.php">Paddagang estate</a> sells some excellent wine (when they are open). A bit further out is Riebeeck Kasteel where you''ll find the excellent Grumpy Grouse Ale House, which (used to) double as a classic car dealer and have the stock on display. Towards the north there are two excellent hiking trails, and the Gydo pass, which is well known for the annual King of the Mountain event, which was sadly cancelled after two fatal accidents. The pass itself though is rutted, rough and requires quite some skill to navigate quickly. There are very sharp turns, heavy off-camber sections and uneven tarmac. At the top, there''s a bit of a geological marvel, where persistent individuals will be able to uncover intact sea shells from the mountain side, and also apparently a restaurant which we couldn''t find in the dark.</p>
<div style="text-align:center">
<img src="https://lh4.googleusercontent.com/-RZZs2sDbSSA/VTZ2HpYNpuI/AAAAAAAADFc/-cAVqozW8QE/w1334-h889-no/IMG_2277.JPG"/>
<img src="https://lh3.googleusercontent.com/HD5rRm3xDX1JFL3kUSoYnvWjqSz7T36zJgxtCVC84Sk=w593-h889-no"/>
<img src="https://lh5.googleusercontent.com/sUF_6nupHQyZuAloWeeXoX2Xne4MdS8fWBQDmS6CQzU=w593-h889-no"/>
<div>
You have to trespass to get close to either.
</div>
</div>
<p>After a week we packed up and headed home. We drove down the short pass from the farm, which we had traversed at least twice a day for the whole week, and stepped into a bolt at the bottom. This was unbelievable. I had really thought we would have an incident free trip, but alas it was not to be. So I set about changing the wheel for the minispare.</p>
<div style="text-align:center">
<img src="https://lh4.googleusercontent.com/-TqhUfvILV9M/VTZ1L44YjqI/AAAAAAAADFc/79EW4cOJRW0/w1334-h889-no/IMG_2332.JPG"/>
<div>
I had to unpack the car to get to the spare. And it looks really stupid when on the car. See all the wine we had bought.
</div>
</div>
<p>And then, of course, what do you do with the wheel? I had to flag down someone that could take it into town for me. This car makes friends! After I had put the minispare on, however, I noticed that it was rather flat. I had to drive extremely slowly into town, which was about 15 km away. We finally met up with our friendly wheel transporter at a local tyre shop, where it was confirmed that the tyre wasn''t repairable. And to make matters worse, the 205/50R15 size is not very common, and the actual Bridgestone stock that they could source immediately was no-where near the correct size for my rim. Finally I settled on a Michelin (195/55R16 if I remember correctly), which meant I now had odd-sized tyres at the rear which will cause problems on the drive shaft, axles and hubs in the long run. After the swap, and the spare was pumped up and put back in the boot, we set off home at a gingerly pace. And so of course, I had to buy another new set of rear Potenzas when we got back.</p>', NULL, CAST(N'2015-04-24 11:25:47.000' AS DateTime), CAST(N'2015-04-24 12:22:28.120' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (1049, NULL, N'helloserve.com Update', N'Simpler, quicker, better layout and easier navigation. Those were the four challenges I wanted to address and take-on with this site update and redesign.', N'<p>In fact, it was a complete rewrite, not just a restyle. The new platform is super efficient, simple and much easier to extend. After the <a href="http://www.globalkinetic.com/">Global Kinetic</a>/<a href="https://www.zapper.com">Zapper</a> calamities, I was left with some off-pressure time within which I redesigned this site and code base, give or take about two weeks in total.</p>
<p>The biggest challenge for me was the UI layout and navigation. Building a site based on a design, as opposed to building a design based on what you know how to do, was a bit of paradigm shift for me. Specifically with regards to web and mobile compatibility. So, I set out with an empty project and started building my own CSS from scratch. And now I hear all of you shouting "Why not use stuff like <a href="http://getbootstrap.com/">bootstrap</a>?" Well, I looked at it, and I didn''t like it. I suspect that initially bootstrap was pretty cool, but by now it''s fallen into the same "popular trap" as any of the other usual "solutions" out there: it''s simply become too complicated and bloated for it''s own good. This site uses 30 CSS classes and two @media overrides. It''s simple, well named and easy to consume in one take. Bootstrap is none of this. The second reason is that I need to learn CSS. I still don''t know it after a small project like this obviously, but using bootstrap instead of building it from first principles is not conducive to learning it. This is the same argument I had when I did <a href="http://www.helloserve.co.za/project/stingray-incursion">Stringray</a> on the question "Why don''t you use Unity?!" Because writing your own 3D and game engine is fun, an intellectual challenge and I also learnt a lot of very interesting and useful stuff. Loading assets and dragging relationships in Unity is... first base, boring, and doesn''t further you as a programmer at all. If you are one of those that asked either of those questions, I urge you to read <a href="https://michaelochurch.wordpress.com/2015/03/25/never-invent-here-the-even-worse-sibling-of-not-invented-here/">this blog post</a> about "Never invent here" mentality. Programming is a science, and if the science of it doesn''t excite you, I don''t consider you a programmer, but rather an operator. That is, you are simply operating your IDE (probably through extensions like Re-sharper) and you don''t understand half of what is going on inside LINQ, <a href="https://github.com/dotnet/roslyn">Roslyn</a> or what-ever tech is specific to the platform you''re using.</p>
<p>So now that I''ve got you completely riled up, let''s move on. The other thing I wanted to do was to list projects that are not necessarily IT related. So far there''s only one, <a href="http://www.helloserve.co.za/project/the-blue-car">The Blue Car</a>, and it''s related to my other interest, motoring. This project for me is a very special one, <a href="http://jalopnik.com/you-cant-separate-a-cars-stereotype-from-the-person-dri-1699981666">and of course very subjective</a>, as is the way with motoring enthusiasts. Now, I''m not the sort of guy that runs the numbers. I don''t remember or care to know all the stats of the latest Ferrari, I don''t even know which one was released last, or which one is fastest around the Nordschleife. I do however care about owning the car, what it''s like to live with, to maintain, to experience and to road-trip with. Your car and seeing your country goes hand-in-hand, unless you do a fly-drive holiday, which isn''t a holiday at all (as advocated by the late Top Gear). So, as I''ve owned the car since 2009, I''ve retrospectively posted blog entries because a lot has happened that forms the backbone and context of this project. It''s a lot to read, but might be well worth it.</p>
<p>My son turned 9 months old last week and I''m currently playing either <a href="https://www.elitedangerous.com/">Elite: Dangerous</a> or Skyrim, so I don''t expect to be updating projects and writing a lot of new blogs, but there will be more activity here now that the site is no longer specifically game-dev focused.</p>', NULL, CAST(N'2015-04-28 09:31:54.000' AS DateTime), CAST(N'2015-04-28 11:09:25.367' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (1050, NULL, N'First weeks at Global Kinetic proper', N'So there was the whole thing between Zapper and Global Kinetic, and now, three months since the announcement, and after the dust have settled, we''re all busy at GK''s official offices, the Lighthouse on Esplanade.', N'<p>To be honest, it was with much trepidation that I started on the first of May, but as it turns out, for all the wrong reasons. Comfort zones are a real thing, and most people are averse to change. I don''t count myself as being among them, but sometimes it''s not your decision. So I was totally unprepared for how stale I had become while being stuck on a project that was, when I started on it, already out of date with current .NET and ASP tech.</p>
<p>Unfortunately, a company uses you where they need you, and despite efforts and requests for other projects, I was the team lead for ZapZap''s API for almost two years. I didn''t realize what a blessing the executive whims would be, being axed from the project through the contract termination. So finally there''s some new work, new technologies and new platforms to look forward too. But it hasn''t been easy.</p>
<p>Microsoft has moved MVC and ASP into completely different spaces recently, with Owin self-hosting for example, and the WebAPI stuff. I had not had the chance to look at these given the limited time I have at home, so ramping up on a new project, on new tech, within two weeks (the ever present two-week sprint targets) was a real challenge. And also very unfair, but trail by fire is the only trail I know, and it''s the only trail I''ve ever had. I surely can''t claim to have ever had a relaxing day at work.</p>
<p>So now we''re into the third week, and things are settled. The project template, established by the .NET tech lead, is becoming more familiar, and since troubleshooting some issues with Owin and self-hosting for integration tests, things have become more familiar and the conceptual picture more detailed. So what about all this then? Well, there are a couple of things I encountered here. One is the "turn-key" solution attitude, which I think is ill-advised, and over complicates matters significantly. Each project is different, and each project''s solution should be too. I''ve had to remove more from the template than I added to complete the requirements. The other is statements like "Oh this is code-first, like everything should be". That''s a pretty reckless statement in my opinion. In fact, for the project I''m currently on I have no control over the database, rather it''s an entity that I simply integrate with. Running EF code-first in this scenario is actually rather silly.</p>
<p>But there has been some good things too. Since this is a new project, we don''t have a unit-test backlog, so we can keep our code coverage up. We can employ SOLID principles from the start, and not have to deal with very extensive surgery to correct issues in the code-base. In fact, this is the first time I''m starting on a new project. It''s a totally alien concept for me since I''ve always had to deal with refactoring on every single task, which it''s probably one of my strongest skills. I hope that <em>it</em> doesn''t now become stale as a result!</p>', NULL, CAST(N'2015-05-27 09:57:55.000' AS DateTime), CAST(N'2015-05-27 10:34:48.213' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (1052, 6, N'It is also a family car', N'Albeit one family member at a time.', N'<p>So we got a new baby seat, and I tried to fit it into the car, which it did! Naturally, this called for a drive, so I piled my son in and we set off. Of course, the previous baby seat was a lot smaller, as was my son, so that wasn''t a problem at all. This new seat though, this is much bigger, and supports the backward facing position up to 18Kg. This is required since the boy''s already 13Kg at 10 months.</p>
<div style="text-align:center">
<img src="https://lh3.googleusercontent.com/fNk9AGf4zN8a8IPV-NkuavOSkfioB2lbsRMOZwkYlmY=w1741-h980-no"/>
<img src="https://lh3.googleusercontent.com/SJimIlQFFcO00Kt9yGikIqJq5YpF5tXMuh_tUPoz4U8=w1741-h980-no"/>
<div>
His first time in the car, at about 6 months, in the small seat.
</div>
</div>
<p>Now that he''s older I can take longer drives with him, so I went for gold: Franschhoek pass. It was a superbly cold and misty day, and drizzle every 5 minutes to keep the road conditions just so. I set off out the back from Durbanville, avoiding the N1 and instead opting for a more interesting and slightly bumpy road, the R312. It''s winding, but not tight, and not loaded with traffic. The view over to Paarl mountain is good enough on a clear day. From there I crossed the N1 past <a href="http://www.butterflyworld.co.za/">Butterflyworld</a>, and turned off for Simondium at Klapmuts, past the Anura estate. This is an even more bumpy road, but low traffic makes it the better option. A great place to visit on this road is <a href="http://www.lebonheurcrocfarm.co.za/">Le Bonheur</a>. From here it''s pretty much straight on the R45 to Franschhoek, although you might want to stop off at the <a href="http://www.fmm.co.za/">Franschhoek Motor Museum</a>; it''s well worth it. It has some great banked bends and a really smooth surface, but it is pretty much single-carriage, and the run into town is slow. A lot of other cars join from the Stellenbosch/Pniel road and are sight-seeing and stopping and turning at different estates. It makes this road rather hazardous, and the wet conditions and low visibility on the day also didn''t help.</p>

<p>Then you are into Franschhoek, and boy this town is something else. On a rainy Sunday traffic in general is usually light since everyone''s in a shopping mall somewhere. Franschhoek however is the exception. This town is a tourist trap, even for locals, and there are too many restaurants and never any parking. I never stop here. It''s too expensive, to crowded and too pretentious. It is rather good if you are out supercar-spotting though. On this occasion I saw a black Lamborghini Gallardo, which didn''t disappoint. But despite the parked-up high street you are through it in no time, and at the start of the pass going up the mountain. A GP plated Kia was kind enough to slow down for me to pass, and we were off.</p>

<p>Now, before you complain about irresponsible parenting, note that I do this safely and while focused <span style="font-size:smaller">(unlike you, probably on your mobile texting<sup>*</sup>)</span>, and advanced driver training helps to deal with any conditions, especially wet roads, and to maintain a margin within the car''s capabilities for any unforeseen circumstances. Having my son on-board doesn''t change how I drive, or my approach to driving this car or any other car. <b>As a user of a public road, you always have to drive safe, not just in certain circumstances.</b> So with that out of the way, the pass was clear of traffic, but it was misty and it was wet, and I mean standing water wet. In spite of this, the MX-5 is simply just fun. This is a car you drive, and a car that responds to your driving it.</p>

<p>In contrast to that Lamborghini I saw, in the Miata you go quickly by not going slow. Going uphill I never use the brakes. I simply shift down if required and let gravity slow me enough, stick it in the bend and gently apply throttle to balance any under-steer there might be. Then, as the road straightens out again you push down on the throttle harder, and in the wet especially, you can feel how the back-end tightens up and starts pushing, first the one wheel, then the other. The open-differential is a bonus in this regard. Even in the wet this car''s grip and lack of body roll translates to a flow between the corners. Swap a cog, get off the gas with a burble, turn it in and listen for the swoosh as the excess water washes out from under the additional pressure on the tires, let it settle and then step on it again and feel that prop grab the two rear wheels by the scruff of the neck. It really is automotive poetry.</p>

<p>On the first part of this pass there isn''t really enough space between bends to even reach the 7k red-line on the engine, so there''s no real need to change up. You might push 80 or so on this 1.6l in third, and on these wet roads that''s already plenty. The other thing about this engine: it''s rather restrictive. If you take your foot off the throttle in a low-gear the car slows right down. This is handy both in traffic and on the downhill. The car will easily under-steer if you under-brake for a corner or if you don''t shift to a low-enough gear. The most defining aspect of this car''s driving experience to me is managing the gears, and to enjoy this car the driver has to get to know the ratios and engine speeds. It''s not just the chassis that makes you feel one with the car.</p>

<p>Soon though I spotted a Land Rover a few turns ahead and decided to turn around. It was nearing lunch time, and the boy was bound to wake up grumpy from hunger. I quickly stopped at the viewpoint to take a picture, but alas, the weather drew the curtains.</p>

<div style="text-align:center">
<img src="https://lh3.googleusercontent.com/ACp_uEFNd-xgC_OeM2sLSHfVEqwaxsOejlTRgATP08I=w1741-h980-no"/>
<div>
At the top, misted over and fast asleep
</div>
</div>

<p>The drive back was, as usual, pale in comparison, and I simply lumbered down the N1 to get home in time for lunch. That pass in the wet though.. that''s something else.</p>

<p style="font-size:smaller"><sup>*</sup>Based on anecdotal data</p>', NULL, CAST(N'2015-06-01 10:03:36.000' AS DateTime), CAST(N'2015-06-17 06:21:07.563' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (1054, 5, N'A new format', N'That thing about PDFs?', N'<p>I started looking at some new import options for BadaChing. Until now you could import the CSV format available to download on most internet banking sites, and also the old OFX format popularized by Microsoft Money about 15 years ago and which is still offered by the local banks here. The problem with this is that those statement downloads are only available for the last 3 months, or 150 transactions or so, depending on your bank. So if you miss a month or two, your data is incomplete.</p>
<p>The solution is to read the PDF statements we all get sent on email. I have a label in GMail with every bank statement ever sent to me, so I should be able to import any of these. All I had to do was to read the PDF, which is easier said than done. Depending on the library (free, paid or otherwise) this is unstructured text; it is impossible to know all the different combinations that transactions are printed with, and some people will receive theirs in different languages! But the POC worked well enough. At least for FNB statements. I hope to make this available soon with two additional features: a complete statement overview before committing the import (because its unstructured and not guaranteed), but also to ask about duplicate transactions.</p>
<p>Currently only the OFX format supplies unique transaction identifiers, so if the user imports a CSV or PDF, all the program has to go on is the description, date and amount. I had a situation where the same amount at the same vendor and on the same date was transacted twice. BadaChing failed to correctly import this statement. This check is necessary because the CSV and OFX formats can be downloaded at any time, meaning it could contain the same transactions. The PDF statements are of course based on monthly periods, so using these exclusively means the check is superfluous.</p>
<p>Check back for an update in, oh.. December?</p>', NULL, CAST(N'2015-06-17 06:10:51.000' AS DateTime), CAST(N'2015-06-17 06:14:56.877' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (1055, NULL, N'Got a family bus', N'So while I''m waiting for a petrol filter for the little blue car, I took delivery of our new family car, the Mazda CX-5.', N'<p>Apart from the fact that Mazda just updated it for the 2015 year model with a new grill and interior layout, this isn''t a new car by any stretch of the imagination. But then again, I''m not a motoring journalist either. After the launch of Mazda SA last year I was really disappointed that we don''t have the <a href="http://www.mazda.co.uk/cars/mazda6-tourer/">Mazda 6 Touring</a> available in South Africa. This year, after shopping around for a while, I was back in the Mazda showroom signing the order form. This is the first new car I''ve had since 2003 so you can imagine my excitement, even at a school bus like this one. A school bus though is very far removed from what this car is, dynamically speaking. In fact, it is a deeply impressive vehicle.
</p>
<p>Considering the only wagons still available here are Volvos and Audis (meh on styling and price), I was basically looking at getting a cross-over or an SUV, and as the market are these days, I had a really big list to choose from. There''s the new Qashqai, the new X-Trail, the ix35, the Sportage, the Forester... So what are the reasons I bought this car specifically? Essentially, because of the manual gearbox (on the base model), because the rear seats split 40:20:40, and because the SKYACTIV platform is still naturally aspirated. Those were my practical elements in the decision. The new Qashqai is probably the closest rival, so lets quickly dwell on the differences. Price wise, the Qashqai matches the CX-5 with a measly 1.2L turbo. Output wise, it matches the CX-5 with a R50 000 premium with a 1.6L turbo. There is also a <a href="http://www.roadandtrack.com/car-culture/news/a24691/ferrari-engineers-dont-like-turbocharging/">whole other thing</a> about turbos, for another time. Since both cars offer a manual gearbox (the X-Trail and Forester don''t at all) that''s one all, but then practically the CX-5 has the Qashqai beat. Although the new Qashqai is truly massive compared to the outgoing model, and the boot is significantly bigger than the CX-5''s, the rear seats only splits 60:40. This means that I can''t fit two baby-seats in the car and still have the capability to load something long or oddly shaped, like our super-massive off-road pram. This single difference held the biggest sway for me. And lastly, the CX-5 just looks sublime, inside and out. I don''t even list this as a difference because, quite frankly, nothing out there comes close to matching the lines of anything in Mazda''s range now. And the Nissan''s interior is simply inferior.</p>
<p>So what about the engine? At first it sounds clunky, especially when cold, but quietens down very quickly as it heats up to operating temperature. Without changing gear, it''s got that typical Mazda characteristic where there''s a small tug as you bury your foot, and then surges forward, building up ever quicker until suddenly you realise you have to back-off. Drop it down to third however, and it almost snorts as it pulls forward; it really makes a good noise too. It''s also a very quick revving 4 pot, and the delivery of the power belies the size and weight of this vehicle. Spirited driving still nets me a consumption rate of less than 8L/100km. On a side-note, the new MX-5 in the US comes standard with this 2.0L unit (or something very similar). If this tune is anything to go by, then I''m properly chuffed that the new roadster will, frankly, be epic!</p>

<p>As for handling, you''d be surprised how little you roll about, or back and forth over speed bumps. I''ve been in hatches that are less comfortable or stable over the back-roads or through the Century City boulevards than this. The big wheels help a lot, but the suspension is the most impressive. It''s not hard or sporty by any means, and I''m not going to pretend it''s communicative, but it handles the Cape crosswinds exceptionally well for something this tall, hardly moves about during lane or camber changes, and when pushing 80 in a corner, it doesn''t lean at the limit half as much as you''d expect. Stopping suddenly from low speed does rock the boat a bit though, that is, suddenly in the strict sense.
</p>

<p>
So what is there that I don''t like? Well, it is very high, and my dogs struggle to get into the tailgate on a slope. The reverse gear selection is a bit clunky when you are in a hurry; I''d have preferred a pull-ring or something instead of the knob push-down mechanism. The seat adjusters (on the base model at least) is very low rent and the worst part of the interior. In fact, it''s so far apart from the rest of the excellent trim that it''s a bit hard to fathom... And lastly, the infotainment system comes with the "Auto-download contacts" setting switched on by default, and when you pair your phone it downloads every person you''ve ever sent an email to, and none of the contacts that actually has phone numbers. This was utterly annoying, and after manually downloading the contacts that I wanted to the system, I''m now in the process of deleting the rest. One by one. After two weeks, I''m at N in the alphabet.
</p>

<p>Still, I''m enjoying this car so much, it really is fun to drive. That Mazda chassis heritage is very prevalent in this car, and coupled with that smooth power delivery, makes for an excellent and comfortable daily driver and cruiser. Defying convention might be the best thing this marquee ever did!</p>', NULL, CAST(N'2015-07-13 05:39:35.000' AS DateTime), CAST(N'2015-07-31 17:26:25.610' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (1056, NULL, N'Open sauce', N'It''s now on GitHub.', N'<p>So, a site update. You''ll notice the SVG icons in the footer instead of the previous simple letter links. With this update I''ve also added a link to my GitHub.</p>

<p> I''ve started to push some of my previous work related to these personal projects to GitHub. If you''re interested, check it out. It''s mostly isolated components and small libraries. I might even try to get the more useful ones onto nuget, but don''t bet on it.</p>

<p>Currently there are three repositories. A Windows service based host for <a href="https://minecraft.net/">Minecraft</a> that also manages a mapper through a headless <a href="http://chunky.llbit.se/">Chunky</a> instance and includes a website that hosts a custom tiled <a href="https://developers.google.com/maps/documentation/javascript/">Google Maps</a> implementation. Then there''s a c# tree library. Only the basic binary tree is implemented but is a complete ICollection that supports removing of nodes through tree reorganizing. It doesn''t have any LINQ extensions yet.
And finally a client for integrating with <a href="https://www.random.org/">random.org</a>.
</p>

<p>I''m also going to put up my WPF regular expression tester. It''s probably the most self-used little application I''ve ever written.</p>', NULL, CAST(N'2015-07-28 12:01:10.000' AS DateTime), CAST(N'2015-07-28 12:07:33.690' AS DateTime), 1)
INSERT [dbo].[News] ([NewsID], [FeatureID], [Title], [Cut], [Post], [HeaderImageID], [CreatedDate], [ModifiedDate], [IsPublished]) VALUES (1057, 6, N'The petrol filter replaced', N'So I gave up waiting for the imported filter, and found a close enough match locally.', N'<p>I usually order from <a href="http://www.mx5parts.co.uk">MX5Parts.co.uk</a>, and I''ve never before had an issue with any of their parts, service or delivery. So naturally I had no qualms ordering the fuel filter from them. Especially since locally my only option was getting an OEM one made for almost 4 times the price at the old Ford/Mazda factory.</p>

<p>However it was the first time I imported since the South African postal service strikes last year, which lasted almost six months. Why the postal service? I don''t have a choice. If the parcel is small and light, MX5Parts sends it via Royal Mail, and hence it arrives via our postal service. And to be honest I doubt I''ll ever receive it - it''s probably stolen, stuck or even lost at customs somewhere.</p>

<p>So after a month of waiting I jacked the car up, removed the old filter and took it to the local Midas parts store. They pulled out their catalogues and we started searching for a filter that will fit. There are no after-market fuel or air filters for the MX-5 available locally, only the oil filter. The catalogues'' section for the MX-5 were literally only one line. What was interesting though was that, apart from the MX-6, all the other older Mazdas (323, 626, Astina, Etude) use the same fuel filter. It''s not the same as mine, but not too different either, and the Astina and Etude has pretty much the same engine, mounted sideways for the FWD train.</p>

<p>Replacing the fuel filter on a Miata is super easy. Just pull the fuse so that the pump is disabled and then idle the car until the fuel line is dry. Undo the flap and pull off the fuel lines. Fitting this other Mazda filter didn''t pose a problem, but because the filter''s lines doesn''t match exactly and because the rubber fuel hoses are very short, there was some bending necessary. So it''s on, and it works, and it doesn''t leak, but the hose on the engine side bends a bit too much for my liking and fitting it in the bracket would simply tear the fuel lines off. I''ll try and get some silicone hoses at some point to replace these old line hoses with.</p>

<div style="text-align:center">
<img src="https://lh3.googleusercontent.com/mw6fdQwgR4xEYDeVmW-M45B5E7L03-T4PLAHB84plHs=w496-h881-no"/>
<div>
The GUD after-market E58 filter. Doesn''t fit the bracket, and the fuel line on the right (nearest) makes a nasty twist and will rub against the chassis. Cable-tie replaces the bolt that should hold the bracket.
</div>
</div>

<p>Or, here''s hoping I still get the real filter delivered.</p>', NULL, CAST(N'2015-08-01 18:43:36.000' AS DateTime), CAST(N'2015-08-01 18:55:52.050' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[News] OFF
SET IDENTITY_INSERT [dbo].[Requirement] ON 

INSERT [dbo].[Requirement] ([RequirementID], [Description], [Link], [Icon]) VALUES (1, N'DirectX 10', N'http://www.microsoft.com/en-us/download/details.aspx?id=8109', NULL)
INSERT [dbo].[Requirement] ([RequirementID], [Description], [Link], [Icon]) VALUES (2, N'.NET 4.0', N'http://www.microsoft.com/en-us/download/details.aspx?id=17718', NULL)
INSERT [dbo].[Requirement] ([RequirementID], [Description], [Link], [Icon]) VALUES (3, N'XNA 4.0', N'http://www.microsoft.com/en-za/download/details.aspx?id=20914', NULL)
INSERT [dbo].[Requirement] ([RequirementID], [Description], [Link], [Icon]) VALUES (4, N'Windows Vista / 7', NULL, NULL)
INSERT [dbo].[Requirement] ([RequirementID], [Description], [Link], [Icon]) VALUES (5, N'.NET 4.5', N'http://www.microsoft.com/en-us/download/confirmation.aspx?id=40779', NULL)
SET IDENTITY_INSERT [dbo].[Requirement] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [Username], [Password], [EmailAddress], [ReceiveUpdates], [Administrator], [ActivationToken], [Activated]) VALUES (1, N'helloserve', 0x6EAC1070628A948CF05A7B70FDB9E711, N'helloserve@gmail.com', 0, 1, N'4c6507e8-f465-452c-b3fc-ef6a86428d01', 1)
INSERT [dbo].[User] ([UserID], [Username], [Password], [EmailAddress], [ReceiveUpdates], [Administrator], [ActivationToken], [Activated]) VALUES (2, N'[BAWS]sabie', 0x6EAC1070628A948CF05A7B70FDB9E711, N'csr.sabie@gmail.com', 1, 0, N'507c9fba-5134-4d1b-96fc-554203c2bd7c', 1)
INSERT [dbo].[User] ([UserID], [Username], [Password], [EmailAddress], [ReceiveUpdates], [Administrator], [ActivationToken], [Activated]) VALUES (3, N'Johan', 0x6EAC1070628A948CF05A7B70FDB9E711, N'johan@webserv.com.na', 1, 0, N'c62749ba-1d33-495f-8272-248a316a31a9', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
