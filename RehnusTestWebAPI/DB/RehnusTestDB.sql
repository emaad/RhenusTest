USE [master]
GO
/****** Object:  Database [RehnusTestDB]    Script Date: 3/27/2022 12:40:27 AM ******/
CREATE DATABASE [RehnusTestDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RehnusTestDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER2019\MSSQL\DATA\RehnusTestDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RehnusTestDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER2019\MSSQL\DATA\RehnusTestDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [RehnusTestDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RehnusTestDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RehnusTestDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RehnusTestDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RehnusTestDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RehnusTestDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RehnusTestDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [RehnusTestDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RehnusTestDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RehnusTestDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RehnusTestDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RehnusTestDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RehnusTestDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RehnusTestDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RehnusTestDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RehnusTestDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RehnusTestDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RehnusTestDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RehnusTestDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RehnusTestDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RehnusTestDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RehnusTestDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RehnusTestDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RehnusTestDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RehnusTestDB] SET RECOVERY FULL 
GO
ALTER DATABASE [RehnusTestDB] SET  MULTI_USER 
GO
ALTER DATABASE [RehnusTestDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RehnusTestDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RehnusTestDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RehnusTestDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RehnusTestDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RehnusTestDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'RehnusTestDB', N'ON'
GO
ALTER DATABASE [RehnusTestDB] SET QUERY_STORE = OFF
GO
USE [RehnusTestDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/27/2022 12:40:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/27/2022 12:40:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](450) NOT NULL,
	[UserPoints] [bigint] NOT NULL,
	[CreatedDateTime] [datetime2](7) NOT NULL,
	[UpdatedDateTime] [datetime2](7) NULL,
	[UserName] [nvarchar](max) NULL,
	[NormalizedUserName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[NormalizedEmail] [nvarchar](max) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220326184344_RehnusTestWebAPI.DataModels.User', N'6.0.3')
GO
INSERT [dbo].[Users] ([Id], [UserPoints], [CreatedDateTime], [UpdatedDateTime], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'49c6f8d8-c51c-4182-8f38-0a57150be5d5', 0, CAST(N'2022-03-26T19:47:27.3344614' AS DateTime2), CAST(N'2022-03-26T20:32:12.5181847' AS DateTime2), N'string', N'STRING', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEA6JuSTHVEF39LtZZRL5ik8BlB6jlSTmrxicKVwWTSfEJSCwIeZ/o+TMcrWRm16uuw==', N'2HFRHS5I46PUYEMIPODIFSED74J7QWDK', N'8db99a22-c5aa-4c36-a081-53dfec314c89', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[Users] ([Id], [UserPoints], [CreatedDateTime], [UpdatedDateTime], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'ba7072b7-c462-4afd-a479-6fd4a1f632d4', 8890, CAST(N'2022-03-26T23:55:02.9675302' AS DateTime2), CAST(N'2022-03-27T00:12:07.6102704' AS DateTime2), N'string33', N'STRING33', NULL, NULL, 0, N'AQAAAAEAACcQAAAAECEI3dlnnOBeiN8DCofxI8dZ4Odv5nXpN3xwRHbkfkaIG5YLiDpR+BsMlAGLdQk0JQ==', N'XZAA47M646OHFZ7ACL6JE65ZH3JAT57E', N'10682e6a-609c-411c-ad3b-6461f6dddc81', NULL, 0, 0, NULL, 1, 0)
GO
USE [master]
GO
ALTER DATABASE [RehnusTestDB] SET  READ_WRITE 
GO
