USE [LibraryDB]
GO

/****** Object:  View [dbo].[vw_ActiveLoans]    Script Date: 1/7/2026 9:01:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vw_ActiveLoans] AS
SELECT 
    l.LoanId,
    m.Email AS MemberEmail,
    b.BookTitle AS Title,
    l.LoanDateTime AS BorrowedDate,
    DATEADD(day, l.LoanPeriod, l.LoanDateTime) AS DueDate,
    DATEDIFF(day, DATEADD(day, l.LoanPeriod, l.LoanDateTime), GETDATE()) AS DaysOverdue
FROM dbo.Loan l
JOIN dbo.Book b ON l.FkBookId = b.BookId
JOIN dbo.LibraryMember m ON l.FkLibraryMemberId = m.LibraryMemberId
LEFT JOIN dbo.Discharge d ON l.LoanId = d.FkLoanId
WHERE d.DischargeId IS NULL;
GO

