using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatientManagementSystem.Models
{
    public class InsuranceCards
    {
        [Key]
        public int InsCardId { get; set; }

        [Display(Name = "Effective Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "You must enter the effective date.")]
        public DateTime EffectiveDate { get; set; }

        public string Comment { get; set; }

        [Display(Name ="File Path")]
        [Required(ErrorMessage = "You must chose a file.")]
        [MaxLength(255)]
        public string FilePath { get; set; }

        public virtual Patient Patient { get; set; }
    }
}