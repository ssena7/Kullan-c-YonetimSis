using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Projednm.Models;


public class YourDbContext : DbContext
{
    public YourDbContext() : base("name=DefaultConnection") // Web.config'deki bağlantı adı
    {
    }

    public DbSet<User> Users { get; set; } // User sınıfınızı temsil eden DbSet
}
