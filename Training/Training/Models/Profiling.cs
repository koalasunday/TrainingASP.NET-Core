using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Training.Models
{
    public class Profiling
    {
        [Key]
        public string NIK { get; set; }

        [ForeignKey("Education")] //membuat foreignkey
        public int Education_Id { get; set; }

        //untuk mebuat relation di mycontext
        public virtual Education Education { get; set; } 

        public virtual Account Account { get; set; }
    }
}
