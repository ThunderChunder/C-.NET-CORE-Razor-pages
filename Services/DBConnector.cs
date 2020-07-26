using barter_razor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace barter_razor.Services
{
    public class DBConnector : IdentityDbContext<IdentityUser> 
    {
        public DbSet<ItemUploader> ItemUploaderRecords { get; set; }
        public DbSet<IdentityUser> UserNames {get;set;}
        public DBConnector(DbContextOptions<DBConnector> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>().ToTable("Item");
        }   
    }
}