# Library Management System (k2u2)

## Business Case Description

The **k2u2 Library System** is a technical solution designed to manage the core operations of a local library. The application allows administrators to manage a digital inventory of books and authors, maintain a secure database of library members, and handle loan and return.

**Core Functionality:**

* **Inventory Management:** Registration of books with ISBN and title tracking.
* **Member Services:** Management of library members using personal identity numbers and possible use of secure PIN codes.
* **Transaction Engine:** A robust loan and return system that ensures books cannot be double-booked.
* **Advanced Search:** A "fuzzy search" feature allowing users to find books by partial titles, ISBN or author keywords.

---

## ER-Diagram

The database schema is designed with normalization in mind to prevent data redundancy and ensure logical relationships.

<img width="4512" height="1842" alt="libraryerd" src="https://github.com/user-attachments/assets/7a15a7b3-5660-4ac6-9fc4-a4d2568dda3c" />

**Key Relationships:**

* **Book ↔ Author:** A Many-to-Many relationship managed through the `BookAuthor` junction table.
* **Member ↔ Loan:** A One-to-Many relationship; one member can have multiple active loans.
* **Loan ↔ Discharge:** A One-to-One relationship; a loan record is only considered "completed" once a matching entry exists in the Discharge table.

---

## Reflection on Optimization and Data Integrity

### Data Integrity

I have implemented multiple layers of protection to ensure the database remains consistent:

1. **Unique Constraints:** Non-clustered unique indexes are applied to the `Email` and `PersonalNumber` columns in the `LibraryMember` table to prevent duplicate accounts.
2. **Referential Integrity:** Foreign Key constraints are strictly enforced. For example, a book cannot be deleted if there is an existing loan history attached to it.
3. **Atomic Transactions:** By using Stored Procedures (e.g., `sp_LoanBook`), the business logic for checking book availability and creating a loan happens in a single transaction. This prevents "race conditions" where two members might try to borrow the last copy of a book at the exact same millisecond.

### Optimization

1. **Strategic Indexing:** I have created non-clustered indexes on all Foreign Key columns (such as `FkBookId` and `FkLibraryMemberId`). This significantly improves the performance of joins when generating reports or viewing active loans.
2. **Server-Side Logic (Views):** The `vw_ActiveLoans` view handles all date arithmetic (calculating due dates and overdue days) directly within SQL Server. This is much faster than pulling thousands of rows into C# and calculating the dates in the application memory.

---

## Execution Plan Analysis

Below is a screenshot of the execution plan for the book search functionality.
<img width="475" height="188" alt="image" src="https://github.com/user-attachments/assets/8c8d376c-a7b9-4fe9-9793-223617a64869" />

**Observations:**
The execution plan reveals that the SQL engine is performing an **Index Seek** on the primary key and indexed columns. This means the engine is navigating a B-Tree structure to find the data directly, rather than performing a "Table Scan" (which would involve reading every single row on the disk). This ensures that the search feature remains near-instant, even if the library collection grows to hundreds of thousands of books.
