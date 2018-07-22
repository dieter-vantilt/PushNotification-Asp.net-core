using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PushNotificationShop.Entities;
using PushNotificationShop.Entities.Context;

namespace PushNotificationShop.Pages.Items
{
    public class DetailsModel : PageModel
    {
        private readonly PushNotificationShop.Entities.Context.PushNotificationShopDbContext _context;

        public DetailsModel(PushNotificationShop.Entities.Context.PushNotificationShopDbContext context)
        {
            _context = context;
        }

        public Item Item { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item = await _context.Items.SingleOrDefaultAsync(m => m.Objectid == id);

            if (Item == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
