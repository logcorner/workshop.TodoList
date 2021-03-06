USE [WorkShopDB]
GO
/****** Object:  Table [dbo].[ToDo]    Script Date: 1/22/2022 10:35:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToDo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title ] [nvarchar](50) NOT NULL,
	[Description  ] [nvarchar](50) NOT NULL,
	[Status ] [int] NOT NULL,
	[ImageUrl] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ToDoItem]    Script Date: 1/22/2022 10:35:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToDoItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title ] [nvarchar](50) NOT NULL,
	[Description  ] [nvarchar](50) NOT NULL,
	[Status ] [int] NOT NULL,
	[AssignedTo] [nvarchar](50) NOT NULL,
	[TodoId] [int] NOT NULL
) ON [PRIMARY]
GO
