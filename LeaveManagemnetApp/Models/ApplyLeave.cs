using System;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagemnetApp.Models
{
    public class ApplyLeave
    {
        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Reason")]
        public string Reason { get; set; }
    }
}