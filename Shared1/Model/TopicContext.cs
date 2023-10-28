using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Model
{
    public class TopicContext : DbContext
    {
        public DbSet<Topic> Topics { get; set; } // Assuming 'Topic' is your entity type
        public DbSet<Comment> Comment { get; set; } // Assuming 'Topic' is your entity type

        //// Bruges måske senere
        public string DbPath { get; }

        public TopicContext(DbContextOptions<TopicContext> options)
             : base(options)
        {
            // Den her er tom. Men ": base(options)" sikre at constructor
            // på DbContext super-klassen bliver kaldt.
        }
    }
}
