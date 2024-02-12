using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CodeClubAssets.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CodeClubAssets.Data
{
    public class CodeClubAssetsContext : IdentityDbContext<ApplicationUser>
    {
        public CodeClubAssetsContext (DbContextOptions<CodeClubAssetsContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Item { get; set; } = default!;
        public DbSet<PATTest> PAT { get; set; } = default!;
        public DbSet<Loans> Loans { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().ToTable("Assets");
            modelBuilder.Entity<PATTest>().ToTable("PATTests");
            modelBuilder.Entity<Loans>().ToTable("OnLoan");
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Ignore<IdentityUserLogin<string>>();
        }
    }
}
