using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace PatientManagementSystem.Models
{
    public class PatientContext : DbContext
    {
        //public PatientContext() : base("PatientContext")
        //{
        //    Database.SetInitializer<PatientContext>(null);
        //}
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visits> Visits { get; set; }
        public DbSet<Communication> Communication { get; set; }
        public DbSet<HIVManagement> HIVManegements { get; set; }
        public DbSet<OtherManagement> OtherManagements { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<Immunizations> Immunizations { get; set; }
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        //public System.Data.Entity.DbSet<PatientManagementSystem.Models.UserAccount> UserAccounts { get; set; }
    }

    
}