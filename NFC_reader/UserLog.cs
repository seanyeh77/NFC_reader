using System;
using System.ComponentModel.DataAnnotations;

namespace NFC_reader
{
    public class UserLog
    {
        [Required]
        public string UID { get; set; }

        [Required]
        public DateTime time { get; set; }
    }
}
