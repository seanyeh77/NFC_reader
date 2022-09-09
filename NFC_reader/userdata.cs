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
        public string ID { get; set; }
        [Required]
        public string ChineseName { get; set; }
        [Required]
        public string position { get; set; }
        [Required]
        public bool freeze { get; set; } = false;
        [Required]
        public bool state { get; set; } = false;
    }
}
