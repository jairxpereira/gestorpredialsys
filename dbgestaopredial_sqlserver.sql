USE [dbgestaopredial]
GO
/****** Object:  Table [dbo].[morador]    Script Date: 27/04/2022 16:16:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[morador]') AND type in (N'U'))
DROP TABLE [dbo].[morador]
GO
/****** Object:  Table [dbo].[familia]    Script Date: 27/04/2022 16:16:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[familia]') AND type in (N'U'))
DROP TABLE [dbo].[familia]
GO
/****** Object:  Table [dbo].[condominio]    Script Date: 27/04/2022 16:16:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[condominio]') AND type in (N'U'))
DROP TABLE [dbo].[condominio]
GO
USE [master]
GO
/****** Object:  Database [dbgestaopredial]    Script Date: 27/04/2022 16:16:06 ******/
DROP DATABASE [dbgestaopredial]
GO
/****** Object:  Database [dbgestaopredial]    Script Date: 27/04/2022 16:16:06 ******/
CREATE DATABASE [dbgestaopredial]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbgestorpredial', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\dbgestorpredial.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbgestorpredial_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\dbgestorpredial_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [dbgestaopredial] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbgestaopredial].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbgestaopredial] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbgestaopredial] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbgestaopredial] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbgestaopredial] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbgestaopredial] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbgestaopredial] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbgestaopredial] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbgestaopredial] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbgestaopredial] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbgestaopredial] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbgestaopredial] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbgestaopredial] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbgestaopredial] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbgestaopredial] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbgestaopredial] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbgestaopredial] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbgestaopredial] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbgestaopredial] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbgestaopredial] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbgestaopredial] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbgestaopredial] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbgestaopredial] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbgestaopredial] SET RECOVERY FULL 
GO
ALTER DATABASE [dbgestaopredial] SET  MULTI_USER 
GO
ALTER DATABASE [dbgestaopredial] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbgestaopredial] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbgestaopredial] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbgestaopredial] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbgestaopredial] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbgestaopredial] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'dbgestaopredial', N'ON'
GO
ALTER DATABASE [dbgestaopredial] SET QUERY_STORE = OFF
GO
USE [dbgestaopredial]
GO
/****** Object:  Table [dbo].[condominio]    Script Date: 27/04/2022 16:16:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[condominio](
	[id] [int] NOT NULL,
	[nome] [nvarchar](50) NULL,
	[bairro] [nvarchar](50) NULL,
	[area_total] [float] NULL,
	[valor_iptu] [money] NULL,
 CONSTRAINT [PK_condominio] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[familia]    Script Date: 27/04/2022 16:16:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[familia](
	[id] [int] NOT NULL,
	[nome] [nvarchar](50) NULL,
	[id_condominio] [int] NOT NULL,
	[apto] [int] NOT NULL,
	[area_apto] [float] NULL,
	[fracao_ideal] [float] NULL,
	[valor_iptu_prop] [money] NULL,
 CONSTRAINT [PK_familia] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[morador]    Script Date: 27/04/2022 16:16:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[morador](
	[id] [int] NOT NULL,
	[id_familia] [int] NOT NULL,
	[nome] [nvarchar](50) NULL,
	[idade] [int] NOT NULL
) ON [PRIMARY]
GO
INSERT [dbo].[condominio] ([id], [nome], [bairro], [area_total], [valor_iptu]) VALUES (1, N'Serra Negra', N'Vila Nova', 500, 10000.0000)
INSERT [dbo].[condominio] ([id], [nome], [bairro], [area_total], [valor_iptu]) VALUES (2, N'Casa Branca', N'Moema', 500, 10000.0000)
INSERT [dbo].[condominio] ([id], [nome], [bairro], [area_total], [valor_iptu]) VALUES (3, N'Bom Recanto', N'Vila Guarani', 500, 10000.0000)
INSERT [dbo].[condominio] ([id], [nome], [bairro], [area_total], [valor_iptu]) VALUES (4, N'Imaré', N'Capuava', 500, 10000.0000)
INSERT [dbo].[condominio] ([id], [nome], [bairro], [area_total], [valor_iptu]) VALUES (5, N'Andorinha', N'Jardim América', 500, 10000.0000)
GO
INSERT [dbo].[familia] ([id], [nome], [id_condominio], [apto], [area_apto], [fracao_ideal], [valor_iptu_prop]) VALUES (1, N'Silva', 2, 10, 50, 2, 1000.0000)
INSERT [dbo].[familia] ([id], [nome], [id_condominio], [apto], [area_apto], [fracao_ideal], [valor_iptu_prop]) VALUES (2, N'Novaes', 2, 45, 50, 2, 1000.0000)
INSERT [dbo].[familia] ([id], [nome], [id_condominio], [apto], [area_apto], [fracao_ideal], [valor_iptu_prop]) VALUES (3, N'Nobrega', 4, 110, 50, 2, 1000.0000)
INSERT [dbo].[familia] ([id], [nome], [id_condominio], [apto], [area_apto], [fracao_ideal], [valor_iptu_prop]) VALUES (4, N'Campineli', 1, 712, 50, 2, 1000.0000)
INSERT [dbo].[familia] ([id], [nome], [id_condominio], [apto], [area_apto], [fracao_ideal], [valor_iptu_prop]) VALUES (5, N'Souza', 1, 715, 50, 2, 1000.0000)
INSERT [dbo].[familia] ([id], [nome], [id_condominio], [apto], [area_apto], [fracao_ideal], [valor_iptu_prop]) VALUES (6, N'Gonçalvez', 3, 640, 50, 2, 1000.0000)
INSERT [dbo].[familia] ([id], [nome], [id_condominio], [apto], [area_apto], [fracao_ideal], [valor_iptu_prop]) VALUES (7, N'Camargo', 3, 301, 50, 2, 1000.0000)
INSERT [dbo].[familia] ([id], [nome], [id_condominio], [apto], [area_apto], [fracao_ideal], [valor_iptu_prop]) VALUES (8, N'Brito', 5, 507, 50, 2, 1000.0000)
INSERT [dbo].[familia] ([id], [nome], [id_condominio], [apto], [area_apto], [fracao_ideal], [valor_iptu_prop]) VALUES (9, N'Oliveira', 3, 530, 50, 2, 1000.0000)
INSERT [dbo].[familia] ([id], [nome], [id_condominio], [apto], [area_apto], [fracao_ideal], [valor_iptu_prop]) VALUES (10, N'Jovanelli', 4, 507, 50, 2, 1000.0000)
INSERT [dbo].[familia] ([id], [nome], [id_condominio], [apto], [area_apto], [fracao_ideal], [valor_iptu_prop]) VALUES (11, N'Vieira', 5, 310, 50, 2, 1000.0000)
GO
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (1, 1, N'Valmir', 65)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (2, 3, N'Lucia', 27)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (3, 2, N'Marcelo', 35)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (4, 2, N'Irene', 78)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (5, 5, N'Marta', 31)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (6, 11, N'Alberto', 56)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (7, 8, N'Lucas', 10)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (8, 4, N'Maria', 25)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (9, 9, N'Mateus', 5)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (10, 10, N'Julia', 8)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (11, 5, N'Bernardo', 2)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (12, 7, N'Rosa', 18)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (13, 3, N'Helena', 23)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (14, 1, N'Willian', 15)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (15, 1, N'José', 42)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (16, 3, N'Priscila', 13)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (17, 7, N'Amanda', 29)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (18, 5, N'Guilherme', 22)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (19, 4, N'Roberta', 2)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (20, 4, N'Ricardo', 30)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (21, 6, N'Giovane', 81)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (22, 6, N'Flavia', 11)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (23, 11, N'Fabiana', 43)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (24, 8, N'Marcio', 20)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (25, 7, N'Roberto', 1)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (26, 9, N'Marcos', 4)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (27, 4, N'Rafael', 3)
INSERT [dbo].[morador] ([id], [id_familia], [nome], [idade]) VALUES (28, 10, N'Bruna', 1)
GO
USE [master]
GO
ALTER DATABASE [dbgestaopredial] SET  READ_WRITE 
GO
