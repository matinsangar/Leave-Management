using System.ComponentModel.DataAnnotations;

namespace LeaveManagemnetApp.Views.Account
{
    public class AdminPanel
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } // Pending, Approved, Rejected
    }
}