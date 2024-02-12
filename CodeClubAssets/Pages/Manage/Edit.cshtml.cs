using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeClubAssets.Data;
using CodeClubAssets.Models;

namespace CodeClubAssets.Pages.Management
{
    public class EditModel : PageModel
    {
        private readonly CodeClubAssets.Data.CodeClubAssetsContext _context;

        public EditModel(CodeClubAssets.Data.CodeClubAssetsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Item Item { get; set; } = default!;

        public IList<Item> Items { get; set; } = default!;

        [BindProperty]
        public string originalLocation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Item == null)
            {
                return NotFound();
            }

            var item =  await _context.Item.FirstOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }
            originalLocation = item.Location;
            Item = item;
            return Page();
        }

        public void ChangeLocationToParents(List<Item> parents)
        {
            Item currentParent = parents.Last();
            if (parents.Where(p => p == currentParent).Count() > 1) return;
            foreach (var i in Items)
            {
                if (i == currentParent) continue;
                if (i.ParentID == currentParent.ID && i.Location == originalLocation)
                {
                    i.Location = currentParent.Location;
                    _context.Attach(i).State = EntityState.Modified;
                    parents.Add(i);
                    ChangeLocationToParents(parents);
                    parents.Remove(i);
                }
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Item).State = EntityState.Modified;

            if (_context.Item != null)
            {
                Items = await _context.Item.ToListAsync();
            }

            
            
            if(Item != null && Item.Location != null && originalLocation != null && originalLocation != "")
            {
                ChangeLocationToParents(new List<Item>() { Item });
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(Item.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ItemExists(string id)
        {
          return _context.Item.Any(e => e.ID == id);
        }
    }
}
