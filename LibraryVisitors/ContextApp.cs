using Microsoft.EntityFrameworkCore;

namespace LibraryVisitors
{
    public sealed class ContextApp: DbContext
    {
        public ContextApp() 
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Style> Сategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<BookToAutors>()
            //    .HasKey(hk => new { hk.AutorId, hk.BookId });
            //modelBuilder.Entity<BookToAutors>()
            //    .HasOne(ho => ho.Author)
            //    .WithMany(wm => wm.BookToAutors)
            //    .HasForeignKey(hfk => hfk.AutorId);
            //modelBuilder.Entity<BookToAutors>()
            //    .HasOne(ho => ho.Book)
            //    .WithMany(wm => wm.BookToAutors)
            //    .HasForeignKey(hfk => hfk.BookId);
        }
    }
}
