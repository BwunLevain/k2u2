USE [LibraryDB]
GO

/****** Object:  Table [dbo].[BookAuthor]    Script Date: 1/7/2026 8:59:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BookAuthor](
	[BookAuthorId] [int] IDENTITY(100,1) NOT NULL,
	[FkBookId] [int] NOT NULL,
	[FkAuthorId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BookAuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BookAuthor]  WITH CHECK ADD  CONSTRAINT [FK_AuthorBookAuthor] FOREIGN KEY([FkAuthorId])
REFERENCES [dbo].[Author] ([AuthorId])
GO

ALTER TABLE [dbo].[BookAuthor] CHECK CONSTRAINT [FK_AuthorBookAuthor]
GO

ALTER TABLE [dbo].[BookAuthor]  WITH CHECK ADD  CONSTRAINT [FK_BookBookAuthor] FOREIGN KEY([FkBookId])
REFERENCES [dbo].[Book] ([BookId])
GO

ALTER TABLE [dbo].[BookAuthor] CHECK CONSTRAINT [FK_BookBookAuthor]
GO

