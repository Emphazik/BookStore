USE [BookStoreH]
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 31.05.2024 11:26:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[idAuthor] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idAuthor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 31.05.2024 11:26:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[idBook] [int] IDENTITY(1,1) NOT NULL,
	[ISBN] [varchar](17) NOT NULL,
	[Title] [varchar](255) NOT NULL,
	[year_of_publication] [int] NOT NULL,
	[idPublisher] [int] NULL,
	[idAuthor] [int] NULL,
	[idCategory] [int] NULL,
	[Size] [varchar](50) NULL,
	[Weight] [decimal](10, 2) NULL,
	[Pages] [int] NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[Description] [text] NULL,
	[ImageURL] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[idBook] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 31.05.2024 11:26:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[idCategory] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publishers]    Script Date: 31.05.2024 11:26:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publishers](
	[idPublisher] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idPublisher] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 31.05.2024 11:26:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[idRole] [int] IDENTITY(1,1) NOT NULL,
	[nameRole] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[idRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 31.05.2024 11:26:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[idUser] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Role] [int] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[idUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Authors] ON 

INSERT [dbo].[Authors] ([idAuthor], [Name]) VALUES (1, N'Джоан Роулинг')
INSERT [dbo].[Authors] ([idAuthor], [Name]) VALUES (2, N'Джордж Р.Р. Мартин')
INSERT [dbo].[Authors] ([idAuthor], [Name]) VALUES (3, N'Лев Толстой')
INSERT [dbo].[Authors] ([idAuthor], [Name]) VALUES (4, N'Федор Достоевский')
INSERT [dbo].[Authors] ([idAuthor], [Name]) VALUES (5, N'Антон Чехов')
SET IDENTITY_INSERT [dbo].[Authors] OFF
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([idBook], [ISBN], [Title], [year_of_publication], [idPublisher], [idAuthor], [idCategory], [Size], [Weight], [Pages], [Price], [Description], [ImageURL]) VALUES (1, N'978-5-17-118366-7', N'Гарри Поттер и философский камень', 1997, 1, 1, 1, N'20x13x2 см', CAST(0.50 AS Decimal(10, 2)), 223, CAST(450.00 AS Decimal(10, 2)), N'Первая книга о мальчике, который выжил.', N'harry_potter.jpg')
INSERT [dbo].[Books] ([idBook], [ISBN], [Title], [year_of_publication], [idPublisher], [idAuthor], [idCategory], [Size], [Weight], [Pages], [Price], [Description], [ImageURL]) VALUES (2, N'978-5-04-116340-8', N'Игра престолов', 1996, 2, 2, 1, N'24x16x4 см', CAST(0.80 AS Decimal(10, 2)), 694, CAST(750.00 AS Decimal(10, 2)), N'Первая книга из серии "Песнь льда и огня".', N'game_of_thrones.jpg')
INSERT [dbo].[Books] ([idBook], [ISBN], [Title], [year_of_publication], [idPublisher], [idAuthor], [idCategory], [Size], [Weight], [Pages], [Price], [Description], [ImageURL]) VALUES (3, N'978-5-4461-0923-8', N'Война и мир', 1869, 3, 3, 3, N'21x14x5 см', CAST(1.20 AS Decimal(10, 2)), 1225, CAST(1200.00 AS Decimal(10, 2)), N'Эпопея о войне 1812 года.', N'war_and_peace.jpg')
INSERT [dbo].[Books] ([idBook], [ISBN], [Title], [year_of_publication], [idPublisher], [idAuthor], [idCategory], [Size], [Weight], [Pages], [Price], [Description], [ImageURL]) VALUES (4, N'978-5-699-87512-7', N'Преступление и наказание', 1866, 2, 4, 3, N'21x14x3 см', CAST(0.70 AS Decimal(10, 2)), 672, CAST(650.00 AS Decimal(10, 2)), N'Роман о моральных терзаниях студента Раскольникова.', N'crime_and_punishment.jpg')
INSERT [dbo].[Books] ([idBook], [ISBN], [Title], [year_of_publication], [idPublisher], [idAuthor], [idCategory], [Size], [Weight], [Pages], [Price], [Description], [ImageURL]) VALUES (5, N'978-5-17-095558-6', N'Три сестры', 1901, 1, 5, 3, N'20x13x1 см', CAST(0.30 AS Decimal(10, 2)), 160, CAST(300.00 AS Decimal(10, 2)), N'Пьеса о жизни трех сестер в провинциальном городе.', N'three_sisters.jpg')
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([idCategory], [Name]) VALUES (1, N'Фэнтези')
INSERT [dbo].[Categories] ([idCategory], [Name]) VALUES (2, N'Научная фантастика')
INSERT [dbo].[Categories] ([idCategory], [Name]) VALUES (3, N'Классическая литература')
INSERT [dbo].[Categories] ([idCategory], [Name]) VALUES (4, N'Детективы')
INSERT [dbo].[Categories] ([idCategory], [Name]) VALUES (5, N'Приключения')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Publishers] ON 

INSERT [dbo].[Publishers] ([idPublisher], [Name]) VALUES (1, N'Издательство "АСТ"')
INSERT [dbo].[Publishers] ([idPublisher], [Name]) VALUES (2, N'Издательство "Эксмо"')
INSERT [dbo].[Publishers] ([idPublisher], [Name]) VALUES (3, N'Издательство "Питер"')
INSERT [dbo].[Publishers] ([idPublisher], [Name]) VALUES (4, N'Издательство "Миф"')
INSERT [dbo].[Publishers] ([idPublisher], [Name]) VALUES (5, N'Издательство "Росмэн"')
SET IDENTITY_INSERT [dbo].[Publishers] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([idRole], [nameRole]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([idRole], [nameRole]) VALUES (2, N'User')
INSERT [dbo].[Roles] ([idRole], [nameRole]) VALUES (3, N'Manager')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([idUser], [Login], [Username], [Password], [Role], [Email], [Phone]) VALUES (3, N'Admin', N'Vladushuik', N'Admin', 1, N'adminBookStore@gmail.com', N'+79046486548')
INSERT [dbo].[Users] ([idUser], [Login], [Username], [Password], [Role], [Email], [Phone]) VALUES (4, N'Andrushka', N'Andrey', N'andrushaplusha', 2, N'andrushaplusha@gmail.com', N'+79001235544')
INSERT [dbo].[Users] ([idUser], [Login], [Username], [Password], [Role], [Email], [Phone]) VALUES (5, N'11', N'TolikNolik', N'111', 2, N'111@google.com', N'+1111111111')
INSERT [dbo].[Users] ([idUser], [Login], [Username], [Password], [Role], [Email], [Phone]) VALUES (6, N'Tolyan', N'Anatoliy', N'123', 2, N'anatoliy@google.com', N'+79046005544')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Books__447D36EAFC49861C]    Script Date: 31.05.2024 11:26:30 ******/
ALTER TABLE [dbo].[Books] ADD UNIQUE NONCLUSTERED 
(
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD FOREIGN KEY([idAuthor])
REFERENCES [dbo].[Authors] ([idAuthor])
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD FOREIGN KEY([idCategory])
REFERENCES [dbo].[Categories] ([idCategory])
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD FOREIGN KEY([idPublisher])
REFERENCES [dbo].[Publishers] ([idPublisher])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([Role])
REFERENCES [dbo].[Roles] ([idRole])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
