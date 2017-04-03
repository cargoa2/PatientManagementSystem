using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PatientManagementSystem.Models
{
    public class OtherManagement
    {
        [Key]
        public int  OtherId { get; set; }

        [Display(Name = "Visit Id")]
        public int VisitId { get; set; }

        [NotMapped]
        public int PatientId { get; set; }

        [NotMapped]
        [Display(Name = "Visit Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VisitDate { get; set; }

        [NotMapped]
        [Display(Name = "Patient Name")]
        public string FullName { get; set; }            

        [Display(Name ="T-Cell")]
        public int TCell { get; set; }

        [Display(Name ="Viral Load")]
        public int ViralLoad { get; set; }

        [Display(Name = "WBC")]
        public int WhiteBloodCell { get; set; }

        [Display(Name = "Hgb")]
        public decimal Hemoglobin { get; set; }

        [Display(Name = "PLT")]
        public decimal PlasmaIronTurnover { get; set; }

        [Display(Name = "Weight")]
        public decimal OtherWeight { get; set; }

        [Display(Name = "Trigly")]
        public int Triglycerides { get; set; }

        [Display(Name = "T-Cholesterol")]
        public int TotalCholesterol { get; set; }

        [Display(Name = "Other Important Documents Results")]
        public string OtherDocuments { get; set; }

        public virtual Visits Visit { get; set; }

        public IEnumerable<Visits> Visits { get; set; }

        public virtual Patient Patient { get; set; }

        public IEnumerable<Patient> Patients { get; set; }


    }
}