using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using WebStore.Models.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebStore.Models.DAL
{
    public class WebStoreContext : DbContext
    {
        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UsersRole> UsersRole { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
