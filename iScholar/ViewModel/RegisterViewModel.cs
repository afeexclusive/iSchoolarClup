using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iScholar.ViewModel
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SchoolName { get; set; }
        public string PhoneNumber { get; set; }
        public string State { get; set; }
        public UserType RegisterAs { get; set; }
    }

    public enum UserType
    {
        School,
        Teacher,
        Student
    }
}
