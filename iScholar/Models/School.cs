using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iScholar.Models
{
    public class School
    {
        public Guid SchoolId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string LocalGovernmentArea { get; set; }
        public string NearestBusStop { get; set; }
        public string WhatsAppNumber { get; set; }
       
        public DateTime DateOfOperation { get; set; }
        public string CACNumber { get; set; }
        public string StateApprovalNumber { get; set; }
        public string Website { get; set; }
        public List<Student> Students { get; set; }
        public List<Teacher> Teachers { get; set; }
        

    }
}
