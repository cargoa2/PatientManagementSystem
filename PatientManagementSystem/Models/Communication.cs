using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatientManagementSystem.Models
{
    public class Communication
    {
        [Key]
        public int CommId { get; set; }

        [Display(Name = "Communication Type")]
        public CommunicationType Type { get; set; }

        [Required(ErrorMessage = "You must enter the date of communication")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime CommDate { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        public virtual Patient Patient { get; set; }

    }

    public enum CommunicationType
    {
        Phone,
        Visit,
        Other
    }
}