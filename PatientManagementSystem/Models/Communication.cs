﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "You must enter the date of communication")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime CommDate { get; set; }

        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Notes { get; set; }

        public virtual Patient Patient { get; set; }

    }
}