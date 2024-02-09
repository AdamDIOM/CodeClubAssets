using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CodeClubAssets.Data;
using CodeClubAssets.Models;

namespace CodeClubAssets.Pages.PAT
{
    public class IndexModel : PageModel
    {
        private readonly CodeClubAssets.Data.CodeClubAssetsContext _context;

        public IndexModel(CodeClubAssets.Data.CodeClubAssetsContext context)
        {
            _context = context;
        }

        public IList<Item> Item { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Item != null)
            {
                Item = await _context.Item.Where(i => i.PATRequired).ToListAsync();
            }
        }
    }
}
