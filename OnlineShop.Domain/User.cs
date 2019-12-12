using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain
{
    public class User : Entity
    {
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
        public string FullName { get; set; }
        public string NotificationDeviceId { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Cart Cart { get;set;} 
        public virtual ICollection<FavoriteProduct> FavoriteProducts { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
