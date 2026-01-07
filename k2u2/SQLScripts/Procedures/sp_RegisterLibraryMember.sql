USE [LibraryDB]
GO

/****** Object:  StoredProcedure [dbo].[sp_RegisterLibraryMember]    Script Date: 1/7/2026 9:03:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_RegisterLibraryMember]
    @PersonalNumber BIGINT,
    @PINCode INT,
    @Email NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF @Email NOT LIKE '%@%.%'
    BEGIN
        RAISERROR('Invalid email format.', 16, 1);
        RETURN;
    END

    IF @PINCode < 1000 OR @PINCode > 9999
    BEGIN
        RAISERROR('PIN code must be exactly 4 digits.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
        
        INSERT INTO LibraryMember (PersonalNumber, PINCode, Email)
        VALUES (@PersonalNumber, @PINCode, @Email);

        DECLARE @NewMemberId INT = SCOPE_IDENTITY();

        COMMIT TRANSACTION;

        PRINT 'Member registered successfully with ID: ' + CAST(@NewMemberId AS VARCHAR);
        SELECT @NewMemberId AS NewLibraryMemberId;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;

        DECLARE @ErrNum INT = ERROR_NUMBER();
        DECLARE @ErrMessage NVARCHAR(4000) = ERROR_MESSAGE();

        -- Handling specific Duplicate Key errors (Constraint violations)
        IF @ErrNum = 2627 OR @ErrNum = 2601
        BEGIN
            RAISERROR('Registration failed: Personal Number or Email already exists.', 16, 1);
        END
        ELSE
        BEGIN
            RAISERROR('Error during registration: %s', 16, 1, @ErrMessage);
        END
    END CATCH
END
GO

