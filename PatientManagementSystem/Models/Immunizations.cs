using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PatientManagementSystem.Models
{
    public class Immunizations
    {
        [Key]
        public int ImmId { get; set; }

        public int PatientId { get; set; }

        [NotMapped]
        [Display(Name = "Patient Name")]
        public string FullName { get; set; }

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Immunization Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "You must enter the date of Immunization")]
        public DateTime ImmDate { get; set; }

        [MaxLength(255)]
        [Display(Name = "Immunization")]
        public string Notes { get; set; }

        [Display(Name = "File Path")]
        public string FilePath { get; set; }

        public Byte[] FileContent { get; set; }

        public virtual Patient Patient { get; set; }

    }
}