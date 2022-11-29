using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTaskForNB.Models;

namespace TestTaskForNB.Data
{
    public class PostsDbContext : DbContext
    {
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Rubric> Rubrics => Set<Rubric>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PostsDbForNB;Trusted_Connection=True;");
        }
    }
}
