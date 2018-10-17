
using System;
using System.Collections.Generic;
using System.Data.Entity;

using System.Text;

namespace ConsoleEF6_DataLog
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(): base("EF6-Datalog")
        {
            //Database.SetInitializer<ApplicationDbContext>(new CreateDatabaseIfNotExists<ApplicationDbContext>());
            //Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
            Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseAlways<ApplicationDbContext>());
           
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T_Address>().Property(p => p.Line001).HasMaxLength(50);
            modelBuilder.Entity<T_Address>().Property(p => p.Line002).HasMaxLength(50);
            modelBuilder.Entity<T_Address>().Property(p => p.Line003).HasMaxLength(50);
            modelBuilder.Entity<T_Address>().Property(p => p.Line004).HasMaxLength(50);
            modelBuilder.Entity<T_Address>().Property(p => p.Line005).HasMaxLength(50);
            modelBuilder.Entity<T_Address>().Property(p => p.Code).HasMaxLength(12);
            modelBuilder.Entity<T_Address>().Property(p => p.ID).IsRequired();

            modelBuilder.Entity<T_Person>().Property(p => p.Title).HasMaxLength(50);
            modelBuilder.Entity<T_Person>().Property(p => p.FirstName).HasMaxLength(50);
            modelBuilder.Entity<T_Person>().Property(p => p.LastName).HasMaxLength(50);

            modelBuilder.Entity<T_DbLog>().Property(p => p.TableName).HasMaxLength(50);
            modelBuilder.Entity<T_DbLog>().Property(p => p.Event).HasMaxLength(25);
            modelBuilder.Entity<T_DbLog>().HasIndex(I => I.LogDateTime);


        }

        public DbSet<T_DbLog>   DBlog     { get; set; }
        public DbSet<T_Person>  People    { get; set; }
        public DbSet<T_Address> Addresses { get; set; }
        public DbSet<T_Lookup>  lookup    { get; set; }
    }
}
