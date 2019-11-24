using System.Linq;
using System.Threading.Tasks;
using FplPriceNotificator.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FplPriceNotificator.Pages.EmailInfo
    {
    [AllowAnonymous]
    public class EditModel : PageModel
        {
        private readonly ApplicationDbContext _context;
        public EditModel(ApplicationDbContext context)
            {
            _context=context;
            }
        [BindProperty]
        public Models.EmailInfo EmailInfo { get; set; }

        public IActionResult OnGet(int? id)
            {
            EmailInfo=_context.EmailInfo.FirstOrDefault(ei => ei.Id==id);

            return Page();
            }

        public async Task<IActionResult> OnPost()
            {
            if (!ModelState.IsValid)
                {
                return Page();
                }
            _context.Attach(EmailInfo).State=EntityState.Modified;

            try
                {
                await _context.SaveChangesAsync();
                }
            catch (DbUpdateConcurrencyException)
                {
                if (!Exists(EmailInfo.Id))
                    return NotFound();
                throw;
                }
            return RedirectToPage("./Index");
            }

        private bool Exists(int id)
            {
            return _context.EmailInfo.Any(e => e.Id==id);
            }

        }
    }