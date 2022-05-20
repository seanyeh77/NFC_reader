using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFC_reader
{
    public class UserData
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int grade { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public bool freeze { get; set; } = false;
    }
}
