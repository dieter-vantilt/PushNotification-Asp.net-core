using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PushNotificationShop.Entities;
using PushNotificationShop.Entities.Context;
using WebPush;

namespace PushNotificationShop.Pages.Items
{
    public class CreateModel : PageModel
    {
        private readonly PushNotificationShop.Entities.Context.PushNotificationShopDbContext _context;

        public CreateModel(PushNotificationShop.Entities.Context.PushNotificationShopDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Item Item { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Items.Add(Item);

            await _context.SaveChangesAsync();

            //use your own private and public key:
            //generator here: https://web-push-codelab.glitch.me/ 
            var vapidDetails = new VapidDetails(@"<youremail>",
                "<publickey>",
                "<privatekey>");

            var payload = JsonConvert.SerializeObject(
                new
                {
                    drink = new { Name = "test", Slug = "test1" },
                    brand = new { Name = "testklop", Slug = "jki" }
                });

            var pushSubscriptions = await _context.PushSubscriptions.ToListAsync();

            var webPushClient = new WebPushClient();

            var tasks = new List<Task>();            

            foreach(var pushSubscription in pushSubscriptions)
            {
                var ffdsfs = new WebPush.PushSubscription(pushSubscription.EndPoint, pushSubscription.P256dh, pushSubscription.Auth);

                tasks.Add(webPushClient.SendNotificationAsync(ffdsfs, "test", vapidDetails));
            }

            await Task.WhenAll(tasks);

            return RedirectToPage("./Index");
        }
    }
}