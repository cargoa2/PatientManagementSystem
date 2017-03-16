using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PatientManagementSystem.Models
{
    public class Communication
    {
        [Key]
        public int CommId { get; set; }

        public int PatientId { get; set; }

        [NotMapped]
        [Display(Name = "Patient Name")]
        public string FullName { get; set; }

        [Display(Name = "Type")]
        public CommunicationType Type { get; set; }

        [Required(ErrorMessage = "You must enter the date of communication")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime CommDate { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        [MaxLength(255)]
        public string FilePath { get; set; }


        public virtual Patient Patient { get; set; }

    }

    public enum CommunicationType
    {
        Phone,
        Visit,
        Other
    }
}