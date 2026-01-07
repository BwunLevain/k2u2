USE [LibraryDB]
GO

/****** Object:  StoredProcedure [dbo].[sp_ReturnBook]    Script Date: 1/7/2026 9:03:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ReturnBook]
    @LoanId INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF EXISTS (SELECT 1 FROM Discharge WHERE FkLoanId = @LoanId)
        BEGIN
            RAISERROR('This loan has already been discharged.', 16, 1);
        END

        INSERT INTO Discharge (FkLoanId, DischargeDateTime)
        VALUES (@LoanId, GETDATE());

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

