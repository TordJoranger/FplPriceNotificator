using System.Collections.Generic;
using FplPriceNotificator.Models;

namespace FplPriceNotificator.Services.FPL.DataObjects
    {
    public class EntryInfo
        {
        public string Email { get; set; }
        public List<Player> Droppers { get; set; }
        public List<Player> Risers { get; set; }

        }
    }
