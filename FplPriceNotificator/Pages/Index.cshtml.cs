using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FplPriceNotificator.Models;
using FplPriceNotificator.Services.FPL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FplPriceNotificator.Pages
    {
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        public IList<Player> Players { get; set; }

        public IList<Player> Droppers { get; set; }

        public IList<Player> Risers { get; set; }

        public void OnGet()
        {
           Players = FplStatisticsService.Players();
            Droppers = Players.Where(p => p.IsAboutToDrop(Threshold.Moderate)).ToList();
            Risers=Players.Where(p => p.IsAboutToRise(Threshold.Moderate)).ToList();
            }
        }
    }
