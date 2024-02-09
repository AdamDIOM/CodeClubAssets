using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CodeClubAssets.Data;
using CodeClubAssets.Models;
using System.Xml.Linq;

namespace CodeClubAssets.Pages.Management
{
    public class CreateModel : PageModel
    {
        private readonly CodeClubAssets.Data.CodeClubAssetsContext _context;

        [BindProperty(SupportsGet = true)]
        public string? name { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? description { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? location { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? parent { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? tags { get; set; }

        public CreateModel(CodeClubAssets.Data.CodeClubAssetsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Item = new Item { Name = name, Description = description, Location = location, ParentID = parent, Tags = tags};
            return Page();
        }

        [BindProperty]
        public Item Item { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Item.Add(Item);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
