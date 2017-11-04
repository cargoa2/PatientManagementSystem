using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PatientManagementSystem.Models
{
    public class Documents
    {
        [Key]
        public int DocFileId { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "You must enter the date of documentation.")]
        public DateTime DocDate { get; set; }

        public int PatientId { get; set; }

        [NotMapped]
        [Display(Name = "Patient Name")]
        public string FullName { get; set; }

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Document Type")]
        public DocumentType DocType { get; set; }

        [MaxLength(255)]
        public string Notes { get; set; }

        [Display(Name = "File Path")]
        [StringLength(255)]
        //[Required(ErrorMessage = "You must select a file")]
        public string FilePath { get; set; }

        public Byte[] FileContent { get; set; }

        public virtual Patient Patient { get; set; }

        public IEnumerable<Patient> Patients { get; set; }

    }

    public enum DocumentType
    {   
        Insurance,  
        Laboratory,
        Radiology,
        Other
    }
}