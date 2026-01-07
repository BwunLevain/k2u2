USE [LibraryDB]
GO

/****** Object:  StoredProcedure [dbo].[sp_LoanBook]    Script Date: 1/7/2026 9:02:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_LoanBook]
    @MemberId INT,
    @BookId INT,
    @LoanPeriod INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Check if the book is already out (exists in Loan but NOT in Discharge)
        -- We use (UPDLOCK, HOLDLOCK) to prevent other transactions from 
        -- inserting a loan for this book until we are done.
        IF EXISTS (
            SELECT 1 
            FROM Loan l WITH (UPDLOCK, HOLDLOCK)
            LEFT JOIN Discharge d ON l.LoanId = d.FkLoanId
            WHERE l.FkBookId = @BookId AND d.DischargeId IS NULL
        )
        BEGIN
            RAISERROR('This book is currently unavailable (already loaned out).', 16, 1);
        END

        -- If available, insert the new loan
        INSERT INTO Loan (FkLibraryMemberId, FkBookId, LoanDateTime, LoanPeriod)
        VALUES (@MemberId, @BookId, GETDATE(), @LoanPeriod);

        COMMIT TRANSACTION;
        PRINT 'Loan successful.';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        PRINT 'Transaction Failed: ' + @ErrorMessage;
        -- Optional: THROW; 
    END CATCH
END
GO

