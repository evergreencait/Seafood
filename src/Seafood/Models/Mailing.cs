﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seafood.Models
{
    [Table("Mailings")]
    public class Mailing
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }

    }
}