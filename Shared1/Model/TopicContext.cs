using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;

namespace Model
{
    public class TopicContext : DbContext
    {
        public DbSet<Topic> Topics { get; set; } // Assuming 'Topic' is your entity type
        public DbSet<Comment> Comment { get; set; } // Assuming 'Topic' is your entity type

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=mysql25.unoeuro.com;Database=chilinh_dk_db;User=chilinh_dk;Password=f4kr6dabEHnxgGpzRAec;");
        }
    }
}
