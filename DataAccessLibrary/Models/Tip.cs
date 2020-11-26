using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Tip
    {
        public int TipId { get; set; }

        [Required]
        [MaxLength(25)]
        public string TipName { get; set; }

        [Required]
        [MaxLength(25)]
        public string TipFlag { get; set; }
    }
}
