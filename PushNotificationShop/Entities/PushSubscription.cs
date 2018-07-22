using System.ComponentModel.DataAnnotations;

namespace PushNotificationShop.Entities
{
    public class PushSubscription
    {
        [Key]
        public int ObjectId { get; set; }

        public string EndPoint { get; set; }
        public string P256dh { get; set; }
        public string Auth { get; set; }
    }
}