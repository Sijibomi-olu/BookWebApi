using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Siji_BookWebApi.Entities
{
    public class Characteristics
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public double Lengthincm { get; set; }
        public double Widthincm { get; set; }
        public int Numofpages { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
