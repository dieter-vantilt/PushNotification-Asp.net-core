
namespace PushNotificationShop.Models
{
    public class SubscriptionModel
    {
        public string EndPoint { get; set; }
        public SubscriptionKeyModel Keys { get; set; }
    }

    public class SubscriptionKeyModel
    {
        public string P256dh { get; set; }
        public string Auth { get; set; }
    }
}