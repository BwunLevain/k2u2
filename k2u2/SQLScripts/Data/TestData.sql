USE [LibraryDB]
GO

--Clean
DELETE FROM Discharge;
DELETE FROM Loan;
DELETE FROM BookAuthor;
DELETE FROM Book;
DELETE FROM Author;
DELETE FROM LibraryMember;

--Insert Authors
INSERT INTO Author (AuthorFirstName, AuthorLastName) VALUES 
('Terry', 'Pratchett'),
('Neil', 'Gaiman'),
('George', 'Orwell'),
('Karin', 'Smirnoff');

-- Insert Bools
EXEC sp_RegisterBook @ISBN = 9780552167390, @Title = 'Good Omens';  -- BookId 100
EXEC sp_RegisterBook @ISBN = 9780451524935, @Title = '1984';        -- BookId 101
EXEC sp_RegisterBook @ISBN = 9789170379925, @Title = 'My Brother';  -- BookId 102

--Linking authors to books
INSERT INTO BookAuthor (FkBookId, FkAuthorId) VALUES 
(100, 10), (100, 11), -- Good Omens has TWO authors
(101, 12),           -- 1984
(102, 13);           -- My Brother

--Insert LibraryMembers
EXEC sp_RegisterLibraryMember @PersonalNumber = 199001011234, @PINCode = 1234, @Email = 'alice@example.com';
EXEC sp_RegisterLibraryMember @PersonalNumber = 198505055678, @PINCode = 5678, @Email = 'bob@example.com';

--Simulate loans
EXEC sp_LoanBook @MemberId = 1, @BookId = 100, @LoanPeriod = 14; -- Alice borrows Good Omens
EXEC sp_LoanBook @MemberId = 2, @BookId = 101, @LoanPeriod = 7;  -- Bob borrows 1984

--Simulate Discharge
EXEC sp_ReturnBook @LoanId = 1000; 

PRINT 'Test data successfully populated.';