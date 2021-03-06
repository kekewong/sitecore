USE [master]
GO
/****** Object:  Database [SiteCoreTest]    Script Date: 13/5/2018 3:18:07 AM ******/
CREATE DATABASE [SiteCoreTest]
GO
USE [SiteCoreTest]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 13/5/2018 3:18:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](4000) NULL,
	[Role] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Users] ([Id], [Username], [Password], [Role]) VALUES (N'1c7d8bee-6545-4dad-9a0c-1fd2bcc2af0c', N'Standard', N'Standard', N'Standard')
GO
INSERT [dbo].[Users] ([Id], [Username], [Password], [Role]) VALUES (N'b04ceb54-48da-40ad-b107-43ed33d82d9d', N'Admin', N'Admin', N'Admin')
GO
USE [master]
GO
ALTER DATABASE [SiteCoreTest] SET  READ_WRITE 
GO
