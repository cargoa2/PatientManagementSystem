using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PatientManagementSystem.Models
{
    public class LabFiles
    {
        [Key]
        public int LabFileId { get; set; }

        public int VisitId { get; set; }

        [NotMapped]
        public string FullName { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        public virtual Visits Visit { get; set; }
        public virtual Patient Patient { get; set; }



    }
}