USE [master]
GO
/****** Object:  Database [ZOLA_PROJECK]    Script Date: 11/5/2017 10:42:43 PM ******/
CREATE DATABASE [ZOLA_PROJECK]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ZOLA_PROJECK', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\ZOLA_PROJECK.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ZOLA_PROJECK_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\ZOLA_PROJECK_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ZOLA_PROJECK] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ZOLA_PROJECK].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ZOLA_PROJECK] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET ARITHABORT OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ZOLA_PROJECK] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ZOLA_PROJECK] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ZOLA_PROJECK] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ZOLA_PROJECK] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET RECOVERY FULL 
GO
ALTER DATABASE [ZOLA_PROJECK] SET  MULTI_USER 
GO
ALTER DATABASE [ZOLA_PROJECK] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ZOLA_PROJECK] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ZOLA_PROJECK] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ZOLA_PROJECK] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ZOLA_PROJECK] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ZOLA_PROJECK', N'ON'
GO
ALTER DATABASE [ZOLA_PROJECK] SET QUERY_STORE = OFF
GO
USE [ZOLA_PROJECK]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [ZOLA_PROJECK]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 11/5/2017 10:42:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[sender] [varchar](50) NOT NULL,
	[receiver] [varchar](50) NOT NULL,
	[time_sent] [datetime] NOT NULL,
	[message] [nvarchar](max) NOT NULL,
	[status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageStatus]    Script Date: 11/5/2017 10:42:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageStatus](
	[id] [int] NOT NULL,
	[status] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Relations]    Script Date: 11/5/2017 10:42:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Relations](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[foo] [varchar](50) NOT NULL,
	[bar] [varchar](50) NOT NULL,
	[relation] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RelationType]    Script Date: 11/5/2017 10:42:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RelationType](
	[id] [int] NOT NULL,
	[relation_name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/5/2017 10:42:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[gender] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Messages] ON 

INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (257, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T07:38:59.970' AS DateTime), N'adf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (258, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T07:39:01.023' AS DateTime), N'ádf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (259, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T07:39:01.407' AS DateTime), N'ádf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (260, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T07:39:28.353' AS DateTime), N'sdfgádfsad', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (261, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T07:39:29.340' AS DateTime), N'ádfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (262, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T07:39:30.290' AS DateTime), N'ádfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (263, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T09:44:48.427' AS DateTime), N'asdfasdfasdkjfhasd', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (264, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T09:44:48.803' AS DateTime), N'asdfas', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (265, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T09:44:49.143' AS DateTime), N'dfasd', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (266, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T09:44:50.283' AS DateTime), N'fasfd', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (267, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:45:02.970' AS DateTime), N'asdfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (268, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:45:03.730' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (269, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:45:04.513' AS DateTime), N'adsf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (270, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:45:14.937' AS DateTime), N'asdfasdfas', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (271, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:45:15.973' AS DateTime), N'dfasfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (272, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:46:33.897' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (273, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:46:37.007' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (274, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:47:09.510' AS DateTime), N'a', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (275, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T09:50:36.040' AS DateTime), N'asdfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (276, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T09:50:51.453' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (277, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:52:24.623' AS DateTime), N'a', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (278, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:53:11.103' AS DateTime), N'a', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (279, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:53:19.157' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (280, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:54:30.947' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (281, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:56:15.657' AS DateTime), N'asdfsd', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (282, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:56:19.013' AS DateTime), N'asdfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (283, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:56:19.637' AS DateTime), N'asdfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (284, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:56:20.310' AS DateTime), N'asdfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (285, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:56:25.027' AS DateTime), N'asdfasd', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (286, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:56:25.623' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (287, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:56:28.613' AS DateTime), N'adsf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (288, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:56:29.207' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (289, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:56:29.660' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (290, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:56:30.133' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (291, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:58:22.803' AS DateTime), N'asdfsadf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (292, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:58:23.397' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (293, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T09:58:35.343' AS DateTime), N'asdfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (294, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T13:18:31.323' AS DateTime), N'asdfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (295, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T13:18:32.923' AS DateTime), N'asdfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (296, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T13:18:33.340' AS DateTime), N'asdfasd', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (297, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T13:18:33.763' AS DateTime), N'fasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (298, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T13:18:34.250' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (299, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T13:18:34.697' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (300, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T13:22:00.757' AS DateTime), N'asdfasdfasasiudjflkj ahsdkfjh askljddh flkj', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (301, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T19:52:47.747' AS DateTime), N'asdfasdfasdfasdlkfhjasduhflkjasddhfkjs', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (302, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:26:12.903' AS DateTime), N'sdfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (303, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:26:13.413' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (304, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:26:13.813' AS DateTime), N'asdfasd', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (305, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:26:14.247' AS DateTime), N'fasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (306, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:27:43.660' AS DateTime), N'sadfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (307, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:28:45.037' AS DateTime), N'sdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (308, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:29:51.083' AS DateTime), N'sdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (309, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:29:53.873' AS DateTime), N'asdfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (310, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:29:56.780' AS DateTime), N'asdfsd', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (311, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:30:07.757' AS DateTime), N'asdfasdkjfhasdkjfh lasjdkkh flaksjdh flkasjddh flkajsddh flkjasdh flkajsdh flkajsdh faksljdh flaksjd fklasjdh fklajsdh fklajsdh flkasjdh flkajsdhf lakjsdhf laksdjh flkajsdhf laksjdh flaksjd hflakjsdh flakjsd hflkjasshdhd lfkjhdasd', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (312, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:31:48.277' AS DateTime), N'adskfja sl;kdfj alskdfj asldkf jasldkfj aslkdjfh aslkdjhf aklsjdhf lkasjdhf lskdjahf lkasdjh fklsjdahf lkasdjh fkasldjhf askldjh fasldkjh faslkdjhf laskdjhf laskdjh lsakdjh falksdjhf alksdjhf laskdjh flasdkjh fslkadj hflasdkjh sadkljh falsdkjhf lasdkjfh s', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (313, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:32:54.067' AS DateTime), N'asldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddlj', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (314, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:33:38.757' AS DateTime), N'asldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddlj', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (315, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:33:43.620' AS DateTime), N'asldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddljasldkjfh alsdkjf aslkdjfh laskdjhf alskdjfh aslkdjhf lsadkjhf asldkjhf alkdsjhf askddlj', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (316, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:36:29.787' AS DateTime), N'dsfasd flkjasd;l kffjasd;lk fj;alsdkkj f;laksddj f;lsdk a jaj ;lfk jasdl;kfj as;dlkjf ;lskfdj ;dsfasd flkjasd;l kffjasd;lk fj;alsdkkj f;laksddj f;lsdk a jaj ;lfk jasdl;kfj as;dlkjf ;lskfdj ;dsfasd flkjasd;l kffjasd;lk fj;alsdkkj f;laksddj f;lsdk a jaj ;lfk jasdl;kfj as;dlkjf ;lskfdj ;dsfasd flkjasd;l kffjasd;lk fj;alsdkkj f;laksddj f;lsdk a jaj ;lfk jasdl;kfj as;dlkjf ;lskfdj ;dsfasd flkjasd;l kffjasd;lk fj;alsdkkj f;laksddj f;lsdk a jaj ;lfk jasdl;kfj as;dlkjf ;lskfdj ;dsfasd flkjasd;l kffjasd;lk fj;alsdkkj f;laksddj f;lsdk a jaj ;lfk jasdl;kfj as;dlkjf ;lskfdj ;dsfasd flkjasd;l kffjasd;lk fj;alsdkkj f;laksddj f;lsdk a jaj ;lfk jasdl;kfj as;dlkjf ;lskfdj ;dsfasd flkjasd;l kffjasd;lk fj;alsdkkj f;laksddj f;lsdk a jaj ;lfk jasdl;kfj as;dlkjf ;lskfdj ;', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (317, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:36:32.147' AS DateTime), N'dsfasd flkjasd;l kffjasd;lk fj;alsdkkj f;laksddj f;lsdk a jaj ;lfk jasdl;kfj as;dlkjf ;lskfdj ;dsfasd flkjasd;l kffjasd;lk fj;alsdkkj f;laksddj f;lsdk a jaj ;lfk jasdl;kfj as;dlkjf ;lskfdj ;dsfasd flkjasd;l kffjasd;lk fj;alsdkkj f;laksddj f;lsdk a jaj ;lfk jasdl;kfj as;dlkjf ;lskfdj ;dsfasd flkjasd;l kffjasd;lk fj;alsdkkj f;laksddj f;lsdk a jaj ;lfk jasdl;kfj as;dlkjf ;lskfdj ;', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (318, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:36:33.290' AS DateTime), N'dsfasd flkjasd;l kffjasd;lk fj;alsdkkj f;laksddj f;lsdk a jaj ;lfk jasdl;kfj as;dlkjf ;lskfdj ;dsfasd flkjasd;l kffjasd;lk fj;alsdkkj f;laksddj f;lsdk a jaj ;lfk jasdl;kfj as;dlkjf ;lskfdj ;dsfasd flkjasd;l kffjasd;lk fj;alsdkkj f;laksddj f;lsdk a jaj ;lfk jasdl;kfj as;dlkjf ;lskfdj ;dsfasd flkjasd;l kffjasd;lk fj;alsdkkj f;laksddj f;lsdk a jaj ;lfk jasdl;kfj as;dlkjf ;lskfdj ;', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (319, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:36:33.890' AS DateTime), N'dsfasd flkjasd;l kffjasd;lk fj;alsdkkj f;laksddj f;lsdk a jaj ;lfk jasdl;kfj as;dlkjf ;lskfdj ;', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (320, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:36:34.787' AS DateTime), N'dsfasd flkjasd;l kffjasd;lk fj;alsdkkj f;laksddj f;lsdk a jaj ;lfk jasdl;kfj as;dlkjf ;lskfdj ;', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (321, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:36:35.513' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (322, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:36:50.490' AS DateTime), N'sdfasd', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (323, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:36:51.530' AS DateTime), N'fsdgasd', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (324, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:36:52.833' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (325, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:36:54.980' AS DateTime), N'sadf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (326, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:36:56.107' AS DateTime), N'sdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (327, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:36:56.980' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (328, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:36:57.683' AS DateTime), N'fdhfgdj', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (329, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:36:58.170' AS DateTime), N'hfhgkjhg', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (330, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:36:58.940' AS DateTime), N'sdfg', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (331, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:37:12.973' AS DateTime), N'sdfasdfasdfasd', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (332, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:37:15.467' AS DateTime), N'sdfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (333, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:37:16.387' AS DateTime), N'asdfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (334, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:37:16.643' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (335, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:37:16.980' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (336, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:37:17.313' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (337, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:37:18.900' AS DateTime), N'faikjsd ;ofijasd', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (338, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:37:19.540' AS DateTime), N'asdf as', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (339, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:37:19.987' AS DateTime), N'asdf ', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (340, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:37:21.617' AS DateTime), N'asdfai sdjfl;kaj sd', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (341, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:39:58.483' AS DateTime), N'asdfa', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (342, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:40:02.523' AS DateTime), N'asdfa', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (343, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:40:02.810' AS DateTime), N'sdfa', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (344, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:40:03.113' AS DateTime), N'sdfas', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (345, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:40:03.403' AS DateTime), N'dfas', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (346, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:40:03.707' AS DateTime), N'dfasd', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (347, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T20:40:04.530' AS DateTime), N'f', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (348, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:40:14.947' AS DateTime), N'sdfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (349, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:40:15.290' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (350, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:40:15.610' AS DateTime), N'asdfasd', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (351, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T20:40:15.913' AS DateTime), N'f', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (352, N'tuandapchai', N'girlxinh2k', CAST(N'2017-11-04T21:37:34.520' AS DateTime), N'alo', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (353, N'girlxinh2k', N'tuandapchai', CAST(N'2017-11-04T21:37:43.663' AS DateTime), N'alo', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (354, N'girlxinh2k', N'tuandapchai', CAST(N'2017-11-04T21:37:46.757' AS DateTime), N'how are you', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (355, N'girlxinh2k', N'vippro97', CAST(N'2017-11-04T21:38:13.487' AS DateTime), N'alo', 1)
GO
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (356, N'vippro97', N'girlxinh2k', CAST(N'2017-11-04T21:38:17.750' AS DateTime), N'alo', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (357, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T21:38:21.767' AS DateTime), N'asdfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (358, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T21:38:22.710' AS DateTime), N'asdfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (359, N'vippro97', N'tuandapchai', CAST(N'2017-11-04T21:38:23.447' AS DateTime), N'asdfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (360, N'tuandapchai', N'vippro97', CAST(N'2017-11-04T21:38:26.637' AS DateTime), N'asdfasd', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (361, N'tuandapchai', N'girlxinh2k', CAST(N'2017-11-04T21:38:29.030' AS DateTime), N'asdfasdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (362, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T00:44:26.537' AS DateTime), N'alo', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (363, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T00:44:34.517' AS DateTime), N'how are you', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (364, N'tuandapchai', N'vippro97', CAST(N'2017-11-05T00:44:43.007' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (365, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T00:44:47.373' AS DateTime), N'im finr', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (366, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T00:44:48.877' AS DateTime), N'thanks', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (367, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T00:44:53.220' AS DateTime), N'haha', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (368, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T00:44:57.237' AS DateTime), N'Done!!!', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (369, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T00:45:02.967' AS DateTime), N'Done', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (370, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T00:45:04.167' AS DateTime), N'!!!', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (371, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T00:45:13.407' AS DateTime), N'Yeahhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (372, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T00:45:26.070' AS DateTime), N'Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!Cool!!!!', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (373, N'tuandapchai', N'vippro97', CAST(N'2017-11-05T00:46:26.300' AS DateTime), N'good bye', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (374, N'tuandapchai', N'aaaa', CAST(N'2017-11-05T00:58:45.430' AS DateTime), N'fyhdfhdfh', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (375, N'tuandapchai', N'aaaa', CAST(N'2017-11-05T00:58:54.947' AS DateTime), N'lkdjfgasl;kdj fasdfj a;lsdkj f;alskdj f;alsdkj fl;aksdj fasdlkf asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (376, N'aaaa', N'tuandapchai', CAST(N'2017-11-05T00:59:13.653' AS DateTime), N'5654646465456465', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (377, N'tuandapchai', N'vippro97', CAST(N'2017-11-05T13:22:31.847' AS DateTime), N'Hello', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (378, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T13:22:57.807' AS DateTime), N'Hello', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (379, N'tuandapchai', N'vippro97', CAST(N'2017-11-05T13:27:37.373' AS DateTime), N'Hello', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (380, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T13:27:44.883' AS DateTime), N'how are you', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (381, N'tuandapchai', N'vippro97', CAST(N'2017-11-05T13:27:49.590' AS DateTime), N'im fine', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (382, N'tuandapchai', N'vippro97', CAST(N'2017-11-05T13:27:52.843' AS DateTime), N'thank you', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (383, N'tuandapchai', N'vippro97', CAST(N'2017-11-05T13:27:54.653' AS DateTime), N'and you?', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (384, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T13:28:00.547' AS DateTime), N'im fine', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (385, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T13:28:01.613' AS DateTime), N'bye', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (386, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T18:13:19.760' AS DateTime), N'Helllo', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (387, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T18:13:24.670' AS DateTime), N'Helloooooo!!!!!', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (388, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T20:07:30.857' AS DateTime), N'alo', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (389, N'tuandapchai', N'vippro97', CAST(N'2017-11-05T20:07:39.390' AS DateTime), N'alo', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (390, N'tuandapchai', N'vippro97', CAST(N'2017-11-05T20:07:41.463' AS DateTime), N'asdf', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (391, N'tuandapchai', N'vippro97', CAST(N'2017-11-05T20:07:46.640' AS DateTime), N'asd', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (392, N'tuandapchai', N'vippro97', CAST(N'2017-11-05T20:07:50.973' AS DateTime), N'fghfg', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (393, N'tuandapchai', N'vippro97', CAST(N'2017-11-05T20:07:55.383' AS DateTime), N'new line', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (394, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T20:21:01.720' AS DateTime), N'hello', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (395, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T20:21:07.807' AS DateTime), N'Hi', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (396, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T20:21:16.580' AS DateTime), N'hi', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (397, N'tuandapchai', N'vippro97', CAST(N'2017-11-05T20:21:40.397' AS DateTime), N'H', 1)
INSERT [dbo].[Messages] ([id], [sender], [receiver], [time_sent], [message], [status]) VALUES (398, N'vippro97', N'tuandapchai', CAST(N'2017-11-05T20:21:42.327' AS DateTime), N';lk;kj', 1)
SET IDENTITY_INSERT [dbo].[Messages] OFF
INSERT [dbo].[MessageStatus] ([id], [status]) VALUES (0, N'Sent')
INSERT [dbo].[MessageStatus] ([id], [status]) VALUES (1, N'Received')
SET IDENTITY_INSERT [dbo].[Relations] ON 

INSERT [dbo].[Relations] ([id], [foo], [bar], [relation]) VALUES (1, N'tuandapchai', N'vippro97', 0)
INSERT [dbo].[Relations] ([id], [foo], [bar], [relation]) VALUES (5, N'kiet', N'tuandapchai', 0)
INSERT [dbo].[Relations] ([id], [foo], [bar], [relation]) VALUES (7, N'vippro97', N'kiet', 0)
INSERT [dbo].[Relations] ([id], [foo], [bar], [relation]) VALUES (16, N'aaaa', N'tuandapchai', 0)
INSERT [dbo].[Relations] ([id], [foo], [bar], [relation]) VALUES (17, N'tungga', N'tuandapchai', 0)
INSERT [dbo].[Relations] ([id], [foo], [bar], [relation]) VALUES (18, N'tungga', N'aaaa', 0)
INSERT [dbo].[Relations] ([id], [foo], [bar], [relation]) VALUES (19, N'tuandapchai', N'nhoxbanh2k1', 1)
INSERT [dbo].[Relations] ([id], [foo], [bar], [relation]) VALUES (20, N'tuandapchai', N'girlcute2k1', 1)
INSERT [dbo].[Relations] ([id], [foo], [bar], [relation]) VALUES (21, N'tuandapchai', N'girlxinh2k', 0)
INSERT [dbo].[Relations] ([id], [foo], [bar], [relation]) VALUES (22, N'vippro97', N'girlxinh2k', 0)
INSERT [dbo].[Relations] ([id], [foo], [bar], [relation]) VALUES (23, N'nhoxbanh2k1', N'vippro97', 1)
INSERT [dbo].[Relations] ([id], [foo], [bar], [relation]) VALUES (24, N'tester', N'tester2', 0)
SET IDENTITY_INSERT [dbo].[Relations] OFF
INSERT [dbo].[RelationType] ([id], [relation_name]) VALUES (0, N'Friend')
INSERT [dbo].[RelationType] ([id], [relation_name]) VALUES (1, N'Pending')
INSERT [dbo].[Users] ([username], [password], [name], [gender]) VALUES (N'aaaa', N'123456789', N'aaaa', NULL)
INSERT [dbo].[Users] ([username], [password], [name], [gender]) VALUES (N'girlcute2k1', N'123', N'Gái Nè', 0)
INSERT [dbo].[Users] ([username], [password], [name], [gender]) VALUES (N'girlxinh2k', N'123', N'Gái', 0)
INSERT [dbo].[Users] ([username], [password], [name], [gender]) VALUES (N'keit', N'123456', N'kiet', NULL)
INSERT [dbo].[Users] ([username], [password], [name], [gender]) VALUES (N'kiet', N'1234', N'kiet', 1)
INSERT [dbo].[Users] ([username], [password], [name], [gender]) VALUES (N'nhoxbanh2k1', N'123', N'Bảnh', 1)
INSERT [dbo].[Users] ([username], [password], [name], [gender]) VALUES (N'tester', N'tester', N'Tester', NULL)
INSERT [dbo].[Users] ([username], [password], [name], [gender]) VALUES (N'tester2', N'tester2', N'Tester 2', NULL)
INSERT [dbo].[Users] ([username], [password], [name], [gender]) VALUES (N'tester2`', N'tester2', N'Tester2', NULL)
INSERT [dbo].[Users] ([username], [password], [name], [gender]) VALUES (N'tuandapchai', N'123', N'Tuấn đập chai nè!!!!', 1)
INSERT [dbo].[Users] ([username], [password], [name], [gender]) VALUES (N'tungga', N'8527894561230', N'tungga', 1)
INSERT [dbo].[Users] ([username], [password], [name], [gender]) VALUES (N'vippro97', N'123', N'Trẩu', 1)
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD FOREIGN KEY([receiver])
REFERENCES [dbo].[Users] ([username])
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD FOREIGN KEY([sender])
REFERENCES [dbo].[Users] ([username])
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD FOREIGN KEY([status])
REFERENCES [dbo].[MessageStatus] ([id])
GO
ALTER TABLE [dbo].[Relations]  WITH CHECK ADD FOREIGN KEY([bar])
REFERENCES [dbo].[Users] ([username])
GO
ALTER TABLE [dbo].[Relations]  WITH CHECK ADD FOREIGN KEY([foo])
REFERENCES [dbo].[Users] ([username])
GO
ALTER TABLE [dbo].[Relations]  WITH CHECK ADD FOREIGN KEY([relation])
REFERENCES [dbo].[RelationType] ([id])
GO
USE [master]
GO
ALTER DATABASE [ZOLA_PROJECK] SET  READ_WRITE 
GO
