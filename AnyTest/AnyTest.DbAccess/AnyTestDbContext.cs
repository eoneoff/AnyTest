using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyTest.DbAccess
{
    public class AnyTestDbContext : DbContext
    {
        public AnyTestDbContext(DbContextOptions<AnyTestDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder model)
        {

        }
    }
}
