USE [master]
GO
/****** Object:  Database [DapperExample]    Script Date: 3/08/2019 3:26:01 PM ******/
CREATE DATABASE [DapperExample]
GO
USE [DapperExample]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 3/08/2019 3:26:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventLocationId] [int] NOT NULL,
	[EventName] [nvarchar](200) NOT NULL,
	[EventDate] [datetime] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventLocation]    Script Date: 3/08/2019 3:26:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventLocation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LocationName] [nvarchar](max) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_EventLocation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Event] ON 

GO
INSERT [dbo].[Event] ([Id], [EventLocationId], [EventName], [EventDate], [DateCreated]) VALUES (1, 1, N'David Bowie', CAST(N'2019-01-11T00:00:00.000' AS DateTime), CAST(N'2019-01-20T02:11:46.453' AS DateTime))
GO
INSERT [dbo].[Event] ([Id], [EventLocationId], [EventName], [EventDate], [DateCreated]) VALUES (2, 1, N'Iggy Pop', CAST(N'2019-01-12T00:00:00.000' AS DateTime), CAST(N'2019-01-20T02:12:14.153' AS DateTime))
GO
INSERT [dbo].[Event] ([Id], [EventLocationId], [EventName], [EventDate], [DateCreated]) VALUES (3, 2, N'The Smiths', CAST(N'2019-01-11T00:00:00.000' AS DateTime), CAST(N'2019-01-20T02:12:24.850' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Event] OFF
GO
SET IDENTITY_INSERT [dbo].[EventLocation] ON 

GO
INSERT [dbo].[EventLocation] ([Id], [LocationName], [DateCreated]) VALUES (1, N'Superdome', CAST(N'2019-01-20T02:10:51.593' AS DateTime))
GO
INSERT [dbo].[EventLocation] ([Id], [LocationName], [DateCreated]) VALUES (2, N'Vector Arena', CAST(N'2019-01-20T02:11:31.073' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[EventLocation] OFF
GO
ALTER TABLE [dbo].[Event] ADD  CONSTRAINT [DF_Event_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[EventLocation] ADD  CONSTRAINT [DF_EventLocation_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_EventLocation] FOREIGN KEY([EventLocationId])
REFERENCES [dbo].[EventLocation] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_EventLocation]
GO