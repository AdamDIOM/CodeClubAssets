using CodeClubAssets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;

namespace CodeClubAssets.Pages
{
    public class ReturnModel : PageModel
    {
        private readonly CodeClubAssets.Data.CodeClubAssetsContext _context;
        public IList<Item> Item { get; set; } = default!;
        public IList<Loans> Loans { get; set; } = default!;
        [BindProperty]
        public int LoanID { get; set; }
        public ReturnModel(CodeClubAssets.Data.CodeClubAssetsContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            if (_context.Item != null)
            {
                Item = await _context.Item.ToListAsync();
            }
            if (_context.Loans != null)
            {
                Loans = await _context.Loans.ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Item != null)
            {
                Item = await _context.Item.ToListAsync();
            }
            
            if(_context.Loans != null)
            {
                Loans = await _context.Loans.ToListAsync();
            }

            if(LoanID != null)
            {
                Loans? loan = Loans.Where(l => l.ID == LoanID).FirstOrDefault();
                if (loan != null)
                {
                    loan.History = true;
                    _context.Attach(loan).State = EntityState.Modified;

                    Item? asset = Item.Where(i => i.ID == loan.AssetID).FirstOrDefault();
                    if(asset != null)
                    {
                        asset.Out = false;
                        _context.Attach(asset).State = EntityState.Modified;
                    }
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
