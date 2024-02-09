using CodeClubAssets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace CodeClubAssets.Pages
{
    public class LoanModel : PageModel
    {
        private readonly CodeClubAssets.Data.CodeClubAssetsContext _context;
        public IList<Item> Item { get; set; } = default!;
        [BindProperty]
        public Loans Loan { get; set; }
        public LoanModel(CodeClubAssets.Data.CodeClubAssetsContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            if (_context.Item != null)
            {
                Item = await _context.Item.ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Item != null)
            {
                Item = await _context.Item.ToListAsync();
            }
            IList<Loans> loans;
            if(_context.Loans != null)
            {
                loans = await _context.Loans.ToListAsync();
            }

            if(Loan.AssetID != null)
            {
                Item? item = Item.Where(i => i.ID == Loan.AssetID).FirstOrDefault();

                if(item != null)
                {
                    item.Out = true;
                    _context.Attach(item).State = EntityState.Modified;

                    Loan.DateBorrowed = DateTime.Now;
                    _context.Loans.Add(Loan) ;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch { }
                }
            }

            return RedirectToPage();
            // set the 'out' to 'in'
            // add record to loan table (member, item, date, length)
        }
    }
}
