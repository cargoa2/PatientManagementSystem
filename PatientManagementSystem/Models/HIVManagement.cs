using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PatientManagementSystem.Models
{
    public class HIVManagement
    {
        [Key]
        public int HIVManagmentId { get; set; }

        [Display(Name = "Visit Id")]
        public int VisitId { get; set; }

        [NotMapped]
        public int PatientId { get; set; }

        [NotMapped]
        [Display(Name = "Patient Name")]
        public string FullName { get; set; }

        [NotMapped]
        public DateTime HIVDiagnosisDate { get; set; }
        
        [NotMapped]
        public int TCellAtDiagnosis { get; set; }

        [NotMapped]
        public int ViralLoadAtDiagnosis { get; set; }

        [NotMapped]
        [Display(Name = "Problem Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VisitDate { get; set; }

        [MaxLength(100)]
        public string Problem { get; set; }

        [MaxLength(10)]
        public string ICD10 { get; set; }

        [Display(Name = "Medication Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime MedicationStart { get; set; }

        [MaxLength(50)]       
        public string Medication { get; set; }

        [Display(Name = "D/C")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime MedDiscDate { get; set; }

        public virtual Visits Visit { get; set; }

        public IEnumerable<Visits> Visits { get; set; }

        public virtual Patient Patient { get; set; }

        public IEnumerable<Patient> Patients { get; set; }



    }
}