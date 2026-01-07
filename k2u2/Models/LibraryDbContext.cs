using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace k2u2.Models;

public partial class LibraryDbContext : DbContext
{
    public LibraryDbContext()
    {
    }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookAuthor> BookAuthors { get; set; }

    public virtual DbSet<Discharge> Discharges { get; set; }

    public virtual DbSet<LibraryMember> LibraryMembers { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<VwActiveLoan> VwActiveLoans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-G90R9MJ;Initial Catalog=LibraryDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Author__70DAFC34A01F4AAD");

            entity.ToTable("Author");

            entity.Property(e => e.AuthorFirstName).HasMaxLength(50);
            entity.Property(e => e.AuthorLastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Book__3DE0C2071E91357F");

            entity.ToTable("Book");

            entity.Property(e => e.BookTitle).HasMaxLength(60);
            entity.Property(e => e.Isbn).HasColumnName("ISBN");
        });

        modelBuilder.Entity<BookAuthor>(entity =>
        {
            entity.HasKey(e => e.BookAuthorId).HasName("PK__BookAuth__21B24F59B6C1B342");

            entity.ToTable("BookAuthor");

            entity.HasIndex(e => e.FkAuthorId, "IX_BookAuthor_AuthorId");

            entity.HasIndex(e => e.FkBookId, "IX_BookAuthor_BookId");

            entity.HasOne(d => d.FkAuthor).WithMany(p => p.BookAuthors)
                .HasForeignKey(d => d.FkAuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuthorBookAuthor");

            entity.HasOne(d => d.FkBook).WithMany(p => p.BookAuthors)
                .HasForeignKey(d => d.FkBookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookBookAuthor");
        });

        modelBuilder.Entity<Discharge>(entity =>
        {
            entity.HasKey(e => e.DischargeId).HasName("PK__Discharg__CBC080073182981E");

            entity.ToTable("Discharge");

            entity.HasIndex(e => e.FkLoanId, "UQ__Discharg__FE4ACA9D19EBCDCF").IsUnique();

            entity.Property(e => e.DischargeDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.FkLoan).WithOne(p => p.Discharge)
                .HasForeignKey<Discharge>(d => d.FkLoanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoanDischarge");
        });

        modelBuilder.Entity<LibraryMember>(entity =>
        {
            entity.HasKey(e => e.LibraryMemberId).HasName("PK__LibraryM__ED93FB365C0B7D40");

            entity.ToTable("LibraryMember");

            entity.HasIndex(e => e.Email, "UC_Member_Email").IsUnique();

            entity.HasIndex(e => e.PersonalNumber, "UC_Member_PersonalNumber").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Pincode).HasColumnName("PINCode");
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("PK__Loan__4F5AD457232E2229");

            entity.ToTable("Loan");

            entity.HasIndex(e => e.FkBookId, "IX_Loan_BookId");

            entity.HasIndex(e => e.FkLibraryMemberId, "IX_Loan_MemberId");

            entity.Property(e => e.LoanDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.FkBook).WithMany(p => p.Loans)
                .HasForeignKey(d => d.FkBookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_BookLoan");

            entity.HasOne(d => d.FkLibraryMember).WithMany(p => p.Loans)
                .HasForeignKey(d => d.FkLibraryMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_LibraryMemberLoan");
        });

        modelBuilder.Entity<VwActiveLoan>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ActiveLoans");

            entity.Property(e => e.BorrowedDate).HasColumnType("datetime");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.MemberEmail).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(60);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
