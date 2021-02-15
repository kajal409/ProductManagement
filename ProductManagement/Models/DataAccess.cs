using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class DataAccess : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> User { get; set; }
    }
}