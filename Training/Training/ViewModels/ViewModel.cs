using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.Models;

namespace Training.ViewModels
{
    public class ViewModel
    {
        public int Id { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public int EducationId { get; set; }
        public string NIK { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }

        public Education education { get; set; }
        public University university { get; set; }
        public Profiling profiling { get; set; }
        public Person person { get; set; }
        public Account account { get; set; }
    }
}
