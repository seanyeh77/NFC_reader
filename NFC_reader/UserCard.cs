using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFC_reader
{
    public class UserCard
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string UID { get; set; }
        [Required]
        public bool freeze { get; set; } = false;
    }
}
