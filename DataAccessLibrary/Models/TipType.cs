﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class TipType
    {
        public int TipTypeId { get; set; }

        [Required]
        [MaxLength(25)]
        public string TipTypeName { get; set; }

        [Required]
        [MaxLength(25)]
        public string TipTypeFlag { get; set; }

        public List<Tip> Tips { get; set; }
    }
}
