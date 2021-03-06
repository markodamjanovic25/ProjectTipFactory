﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Country
    {
        public Country()
        {
            Leagues = new HashSet<League>();
        }
        public int CountryId { get; set; }

        [Required]
        [MaxLength(25)]
        public string CountryName { get; set; }

        [Required]
        [MaxLength(25)]
        [Column(TypeName = "varchar(50)")]
        public string CountryFlag { get; set; }

        public ICollection<League> Leagues { get; set; }

    }
}
