using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FplPriceNotificator.Models
    {
    public class EmailInfo
        {
            [Key]
            public int Id { get; set; }
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            public bool WantDrops { get; set; } = true;
            public bool WantRises { get; set; } = true;
            public Threshold Threshold { get; set; } = Threshold.Moderate;
            [Required]
            public int Entry { get; set; }
        }

        public enum Threshold
        {
            Critical=99,
            Moderate=96,
            Low=90
        }
    }
