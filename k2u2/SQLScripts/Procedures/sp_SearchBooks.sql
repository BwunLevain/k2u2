USE [LibraryDB]
GO

/****** Object:  StoredProcedure [dbo].[sp_SearchBooks]    Script Date: 1/7/2026 10:09:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_SearchBooks]
    @SearchTitle NVARCHAR(100),
    @SearchAuthor NVARCHAR(100)
AS
BEGIN
    SELECT b.*
    FROM Book b
    -- Use JOINs if you want to search by Author Name
    WHERE b.BookTitle LIKE @SearchTitle 
    OR b.ISBN LIKE @SearchTitle -- Optional: search ISBN too
END
GO

