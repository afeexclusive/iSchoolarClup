using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iScholar.Models
{
    public class Teacher
    {
        public Guid TeacherId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
       
        public string Gender { get; set; }
        public MaritalStatus Married { get; set; }
        public string WhatsAppNumber { get; set; }
        public Guid SchoolId { get; set; }
        public School School { get; set; }
    }

    public enum MaritalStatus
    {
        Married,
        Single,
        Widow,
        Widower,
        Other
    }
}
