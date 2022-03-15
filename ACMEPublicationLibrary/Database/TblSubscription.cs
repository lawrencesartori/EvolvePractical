using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACMEPublicationLibrary.Database
{
    public class TblSubscription
    {
        public int SubscriptionId { get; set; }
        public int DeliveryAddressId { get; set; }

        public TblDeliveryAddress DeliveryAddress { get; set; }

        public int PublicationId { get; set; }

        public TblPublication Publication { get; set; }

        public bool Active { get; set; }
        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public Decimal Price { get; set; }
    }
}
