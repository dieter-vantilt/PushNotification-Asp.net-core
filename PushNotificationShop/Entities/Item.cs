
using System.ComponentModel.DataAnnotations;

namespace PushNotificationShop.Entities
{
    public class Item
    {
        [Key]
        public int Objectid { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
    }
}