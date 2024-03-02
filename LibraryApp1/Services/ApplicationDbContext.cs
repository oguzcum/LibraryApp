using LibraryApp1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection.Emit;


public class ApplicationDbContext : IdentityDbContext<UserInfo>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Product { get; set; }

    public DbSet<UserProduct> UserProducts { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>().HasKey(p => p.ProductId);
        builder.Entity<UserInfo>().HasKey(u => u.Id);
        builder.Entity<UserProduct>().HasKey(up => up.ProcessId);


        builder.Entity<UserProduct>()
             .HasKey(po => new { po.ProductId, po.Id });
        builder.Entity<UserProduct>()
            .HasOne(p => p.Product)
            .WithMany(pc => pc.UserProducts)
            .HasForeignKey(p => p.ProductId);
        builder.Entity<UserProduct>()
            .HasOne(p => p.UserInfo)
            .WithMany(pc => pc.UserProducts)
            .HasForeignKey(c => c.Id);



        var admin = new IdentityRole("admin");
        admin.NormalizedName = "admin";

        var client = new IdentityRole("client");
        client.NormalizedName = "client";

        builder.Entity<IdentityRole>().HasData(admin, client);


    }

    

  

}
