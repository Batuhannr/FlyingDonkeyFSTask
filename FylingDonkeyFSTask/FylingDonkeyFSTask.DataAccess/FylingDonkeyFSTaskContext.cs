using FylingDonkeyFSTask.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FylingDonkeyFSTask.DataAccess
{
    public class FylingDonkeyFSTaskContext : DbContext
    {
        public DbSet<Todos> Todos { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<TodosTags> TodoTags { get; set; }
        public FylingDonkeyFSTaskContext()
        {
        }
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=FylingDonkeyFSTask;User Id=postgres;Password=10063474;");
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
