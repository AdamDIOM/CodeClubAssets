using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CodeClubAssets.Data;
using CodeClubAssets.Models;

namespace CodeClubAssets.Pages.Management
{
    public class DetailsModel : PageModel
    {
        private readonly CodeClubAssets.Data.CodeClubAssetsContext _context;

        public DetailsModel(CodeClubAssets.Data.CodeClubAssetsContext context)
        {
            _context = context;
        }

      public Item Item { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Item == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FirstOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }
            else 
            {
                Item = item;
            }
            return Page();
        }
    }
}
