using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Training.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        [Required] //untuk mendeklarasikan NOT NULL
        public string Degree { get; set; }
        [Required] //untuk mendeklarasikan NOT NULL
        public string GPA { get; set; }
        [Required] //untuk mendeklarasikan NOT NULL

        [ForeignKey("University")] //membuat foreignkey
        public int University_Id { get; set; }

        public virtual University University { get; set; }
    }
}
