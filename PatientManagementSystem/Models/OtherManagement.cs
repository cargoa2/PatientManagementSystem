﻿using System;
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

        public int PatientId { get; set; }

        [Display(Name = "Visit Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VisitDate { get; set; }

        [NotMapped]
        [Display(Name = "Patient Name")]
        public string FullName { get; set; }

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }


        [Display(Name ="T-Cell")]
        [MaxLength(100)]
        public string TCell { get; set; }

        [Display(Name ="Viral Load")]
        [MaxLength(100)]
        public string ViralLoad { get; set; }

        [Display(Name = "WBC")]
        [MaxLength(100)]
        public string WhiteBloodCell { get; set; }
        [Display(Name = "Hgb")]
        public decimal Hemoglobin { get; set; }

        [Display(Name = "PLT")]
        public decimal PlasmaIronTurnover { get; set; }

        [Display(Name = "Weight")]
        [MaxLength(100)]
        public string OtherWeight { get; set; }

        [Display(Name = "Trigly")]
        public int? Triglycerides { get; set; }

        [Display(Name = "T-Cholesterol")]
        public int? TotalCholesterol { get; set; }

        [Display(Name = "Other Important Documents Results")]
        public string OtherDocuments { get; set; }

        public virtual Patient Patient { get; set; }

        public IEnumerable<Patient> Patients { get; set; }

    }
}