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
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visits> Visits { get; set; }

        public DbSet<HIVManagement> HIVManegements { get; set; }

        public DbSet<OtherManagement> OtherManagements { get; set; }
        public DbSet<LabFiles> LabFiles { get; set; }
        //public DbSet<UserAccount> UserAccount { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        //public System.Data.Entity.DbSet<PatientManagementSystem.Models.UserAccount> UserAccounts { get; set; }
    }

    
}