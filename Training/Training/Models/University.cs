using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Training.Models
{
    public class University
    {
        [Key] //terimplementasi sebagai auto increment saat create data
        public int Id { get; set; }

        [Required] //untuk mendeklarasikan NOT NULL
        public string Name { get; set; }

        public bool IsAvailable { get; set; }
    }
}
