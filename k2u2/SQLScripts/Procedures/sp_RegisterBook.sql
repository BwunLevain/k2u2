USE [LibraryDB]
GO

/****** Object:  StoredProcedure [dbo].[sp_RegisterBook]    Script Date: 1/7/2026 9:03:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_RegisterBook]
    @ISBN BIGINT,
    @Title NVARCHAR(60)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Book (ISBN, BookTitle)
        VALUES (@ISBN, @Title);

        DECLARE @NewBookId INT = SCOPE_IDENTITY();
        
        COMMIT TRANSACTION;

        PRINT 'Book registered successfully with ID: ' + CAST(@NewBookId AS VARCHAR);
        SELECT @NewBookId AS NewBookId;
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;

        -- If a duplicate ISBN is entered, the error will land here
        DECLARE @ErrMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR('Error during registration: %s', 16, 1, @ErrMessage);
    END CATCH
END
GO

