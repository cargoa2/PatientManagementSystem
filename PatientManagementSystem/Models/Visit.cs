using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PatientManagementSystem.Models
{
    public class Visits
    {
        [Key]
        [Display(Name = "Visit Id")]
        public int VisitId { get; set; }

        [Display(Name = "Patient Id")]
        public int PatientId { get; set; }

        [NotMapped]
        [Display(Name = "Patient Name")]
        public string FullName { get; set; }

        public bool Initial { get; set; }

        [Required(ErrorMessage = "Visit type is required")]
        [Display(Name = "Visit Type")]
        public VisitTypes VisitType { get; set; }

        [Display(Name = "Visit Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VisitDate{ get; set; }

        [Display(Name = "Diagnosis Code")]
        [Required(ErrorMessage = "ICD10 Diagnosis Codes is required.")]
        [MaxLength(10)]
        public string DiagnosisCode { get; set; }

        [Display(Name = "Co-Pay Amount")]
        public decimal CoPay { get; set; }

        [Display(Name = "Payment Type")]
        public PaymentType PaymentType { get; set; }

        [Display(Name = "Check#")]
        [MaxLength(10)]
        public string CheckNumber { get; set; }

        [Display(Name = "Total Paid Today")]
        public decimal TotalPaid { get; set; }

        [Display(Name = "Medical Allergy")]
        [DataType(DataType.MultilineText)]
        public string MedicalAllergy { get; set; }

        [Display(Name = "Referral Reason")]
        [DataType(DataType.MultilineText)]
        public string ReferralReason { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "History of present illness")]
        public string History { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "PMH" )]
        public string PastHistory { get; set; }

        [DataType(DataType.MultilineText)]
        public string Epidemiology { get; set; }  // Could be a list and that list could be added to if not avaiDocumentsle in the list.  Check on that.

        [DataType(DataType.MultilineText)]
        [Display(Name = "Fhx")]
        public string FamilyHistory { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Shx")]
        public string SocialHistory { get; set; }

        #region Review of Systems ROS

        [Display(Name = "General")]
        [MaxLength(50)]
        public string RosGeneral { get; set; }

        [MaxLength(50)]
        [Display(Name = "HEENT")]      
        public string RosHeent { get; set; } // head, eye, ear, nose, and throat

        [MaxLength(50)]
        public string Respiratory { get; set; }

        [MaxLength(50)]
        [Display(Name = "CV")]
        public string Cardiovascular { get; set; }

        [MaxLength(50)]
        [Display(Name = "GI")]
        public string Gastrointestinal { get; set; }

        [MaxLength(50)]
        [Display(Name = "GU")]
        public string Genitourniary { get; set; }

        [MaxLength(50)]
        [Display(Name = "Neurological")]
        public string RosNeurological { get; set; }

        [MaxLength(50)]
        public string psychosocial { get; set; }

        [DataType(DataType.MultilineText)]
        public string Medications { get; set; } 
        #endregion

        #region Physical Exam PE

        [MaxLength(50)]
        [Display(Name = "General")]
        public string PeGeneral { get; set; }

        [MaxLength(50)]
        [Display(Name = "BP")]
        public string BloodPressure { get; set; }

        [Display(Name ="Heart Rate")]
        public int HeartRate { get; set; }

        public decimal Tempurature { get; set; }

        public decimal Weight { get; set; }

        [MaxLength(50)]
        [Display(Name = "HEENT")]
        public string PeHeent { get; set; }

        [MaxLength(50)]
        public string Neck { get; set; }

        [MaxLength(50)]
        public string Skin { get; set; }

        [MaxLength(50)]
        public string Lungs { get; set; }

        [MaxLength(50)]
        public string Heart { get; set; }

        [MaxLength(50)]
        public string Abdomen { get; set; }

        [MaxLength(50)]
        public string Musculoskeletal { get; set; }

        [MaxLength(100)]
        [Display(Name = "Neurological")]
        public string PeNeurological { get; set; }

        [MaxLength(100)]
        [Display(Name = "Additional Findings")]
        public string Additional { get; set; }

        #endregion
        [DataType(DataType.MultilineText)]
        public string Documentsoratory { get; set; }

        [DataType(DataType.MultilineText)]
        public string Assessment { get; set; }

        [DataType(DataType.MultilineText)]
        public string Plan { get; set; }

        public virtual Patient patient { get; set; }

        public IEnumerable<Patient> Patients { get; set; }
        
        [Display(Name = "HIV Management")]
        public bool IsHIVManagement { get; set; }

        [Display(Name = "Other Management")]
        public bool IsOtherManagement { get; set; }


    }

    public enum VisitTypes
    {
        N99201,
        N99202,
        N99203,
        N99204, 
        N99205, 
        E99211,
        E99212,
        E99213,
        E99214,
        E99215
    }

    public enum PaymentType
    {
        Cash,
        Check
    }

}