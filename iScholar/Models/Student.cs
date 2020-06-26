using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iScholar.Models
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ParentPhone { get; set; }
        public string Address { get; set; }
        
        public string Gender { get; set; }
        public string Class { get; set; }
        public string ParentsWhatsAppNumber { get; set; }
        public string ParentName { get; set; }
        public string ParentEmail { get; set; }
        public Guid SchoolId { get; set; }
        public School School { get; set; }
    }
}
