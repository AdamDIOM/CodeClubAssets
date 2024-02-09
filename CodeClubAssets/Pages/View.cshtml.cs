using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CodeClubAssets.Data;
using CodeClubAssets.Models;

namespace CodeClubAssets.Pages
{
    public class ViewModel : PageModel
    {
        private readonly CodeClubAssets.Data.CodeClubAssetsContext _context;
        [BindProperty(SupportsGet = true)]
        public string qry { get; set; }
        public ViewModel(CodeClubAssets.Data.CodeClubAssetsContext context)
        {
            _context = context;
        }

        public IList<Item> Item { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if(qry == null) qry = "";
            if (_context.Item != null)
            {
                Item = await _context.Item.ToListAsync();
                {
                    Item = Item.Where( i =>
                        i.Name.ToLower().Contains(qry.ToLower()) ||
                        i.Description.ToLower().Contains(qry.ToLower()) ||
                        i.ID.ToLower().Contains(qry.ToLower()) ||
                        i.Tags != null && i.Tags.ToLower().Contains(qry.ToLower())
                    ).ToList();
                }
            }
        }
    }
}
