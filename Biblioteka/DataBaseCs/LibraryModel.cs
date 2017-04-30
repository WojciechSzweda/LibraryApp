namespace Biblioteka
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LibraryModel : DbContext
    {
        public LibraryModel()
            : base("name=LibraryModelSettings")
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Book_Category> Book_Category { get; set; }
        public virtual DbSet<Book_State> Book_State { get; set; }
        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<Card_State> Card_State { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Copy> Copy { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Publisher> Publisher { get; set; }
        public virtual DbSet<Rent> Rent { get; set; }
        public virtual DbSet<Return> Return { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(e => e.Authors)
                .WithRequired(e => e.Author)
                .HasForeignKey(e => e.ID_Author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Authors)
                .WithRequired(e => e.Book)
                .HasForeignKey(e => e.ID_Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Book_Category)
                .WithRequired(e => e.Book)
                .HasForeignKey(e => e.ID_Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Copy)
                .WithRequired(e => e.Book)
                .HasForeignKey(e => e.ID_Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book_State>()
                .HasMany(e => e.Copy)
                .WithRequired(e => e.Book_State1)
                .HasForeignKey(e => e.Book_State)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book_State>()
                .HasMany(e => e.Return)
                .WithOptional(e => e.Book_State)
                .HasForeignKey(e => e.State_After_Return);

            modelBuilder.Entity<Book_State>()
                .HasMany(e => e.Return1)
                .WithOptional(e => e.Book_State1)
                .HasForeignKey(e => e.State_Pre_Return);

            modelBuilder.Entity<Card>()
                .Property(e => e.Postal_Code)
                .IsFixedLength();

            modelBuilder.Entity<Card>()
                .HasMany(e => e.Rent)
                .WithRequired(e => e.Card)
                .HasForeignKey(e => e.ID_Card)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Card_State>()
                .HasMany(e => e.Card)
                .WithRequired(e => e.Card_State)
                .HasForeignKey(e => e.State)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Book_Category)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.ID_Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Copy>()
                .HasMany(e => e.Rent)
                .WithRequired(e => e.Copy)
                .HasForeignKey(e => e.ID_Copy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Publisher>()
                .HasMany(e => e.Book)
                .WithRequired(e => e.Publisher)
                .HasForeignKey(e => e.ID_Publisher)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rent>()
                .HasOptional(e => e.Return)
                .WithRequired(e => e.Rent);
        }
    }
}
