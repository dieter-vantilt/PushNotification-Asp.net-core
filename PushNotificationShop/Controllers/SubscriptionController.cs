using Microsoft.AspNetCore.Mvc;
using PushNotificationShop.Entities;
using PushNotificationShop.Entities.Context;
using PushNotificationShop.Models;
using System.Linq;

namespace PushNotificationShop.Controllers
{
    [Route("api/[controller]")]
    public class SubscriptionController 
        : Controller
    {
        private readonly PushNotificationShopDbContext _context;

        public SubscriptionController(PushNotificationShopDbContext dbContext)
        {
            _context = dbContext;
        }

        // POST api/<controller>
        //this subscriptionmodel is achieved by json stringfying the subscription object directly. see main.js
        [HttpPost]
        public void Post([FromBody] SubscriptionModel model)
        {
            _context.PushSubscriptions.Add(new PushSubscription { Auth = model.Keys.Auth, EndPoint = model.EndPoint, P256dh = model.Keys.P256dh });
            _context.SaveChanges();
        }

        // DELETE: api/<controller>?endPoint={endPoint}
        // One thing that could be improved is when the user has a subscription and then blocks the notifications. The push subscription will then not be deleted from the db
        [HttpDelete]
        public IActionResult Delete(string endPoint)
        {
            var pushSubscription = _context.PushSubscriptions.SingleOrDefault(x => x.EndPoint == endPoint);
            
            if (pushSubscription == null)
            {
                return NotFound();
            }

            _context.PushSubscriptions.Remove(pushSubscription);
            _context.SaveChanges();

            return NoContent();
        }
    }
}