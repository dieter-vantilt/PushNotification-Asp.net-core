using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PushNotificationShop.Entities;

namespace PushNotificationShop.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly PushNotificationShop.Entities.Context.PushNotificationShopDbContext _context;

        public IndexModel(PushNotificationShop.Entities.Context.PushNotificationShopDbContext context)
        {
            _context = context;
        }

        public IList<Item> Item { get;set; }

        public async Task OnGetAsync()
        {
            Item = await _context.Items.ToListAsync();
        }
    }
}
