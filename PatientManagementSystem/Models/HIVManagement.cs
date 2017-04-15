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

        //[NotMapped]
        [Display(Name = "Patient Id")]
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

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VisitDate { get; set; }

        [MaxLength(100)]
        public string Problem { get; set; }

        [MaxLength(100)]
        public string ICD10 { get; set; }

        [Display(Name = "Medication Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> MedicationStart { get; set; }

        [DataType(DataType.MultilineText)]
        public string Medication { get; set; }

        [Display(Name = "D/C")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> MedDiscDate { get; set; }

        public virtual Visits Visit { get; set; }

        public IEnumerable<Visits> Visits { get; set; }

        public virtual Patient Patient { get; set; }

        public IEnumerable<Patient> Patients { get; set; }



    }
}