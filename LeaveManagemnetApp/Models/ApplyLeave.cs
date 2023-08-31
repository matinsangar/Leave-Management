using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LeaveManagemnetApp.Models
{
    public class ApplyLeave
    {
        [Required] [MinLength(3)] public string Name { get; set; }
        [BsonId] [Required] [MinLength(3)] public string EmployeeID { get; set; }

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
        [MinLength(5)]
        public string Reason { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}