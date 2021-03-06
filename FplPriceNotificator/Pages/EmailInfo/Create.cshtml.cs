﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FplPriceNotificator.Data;
using Microsoft.AspNetCore.Authorization;

namespace FplPriceNotificator.Pages.EmailInfo
{
    [AllowAnonymous]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.EmailInfo EmailInfo { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.EmailInfo.Add(EmailInfo);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}