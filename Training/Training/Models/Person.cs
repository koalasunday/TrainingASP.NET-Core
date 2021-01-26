using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Training.Models
{
    public class Person
    {
        [Key]
        public string NIK { get; set; }
        [Required] //untuk mendeklarasikan NOT NULL
        public string FirstName { get; set; }
        [Required] //untuk mendeklarasikan NOT NULL
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }

        //untuk menghentikan looping data
        public virtual Account Account { get; set; } 
    }
}
