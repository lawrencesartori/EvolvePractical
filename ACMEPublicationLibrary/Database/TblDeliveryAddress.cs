using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACMEPublicationLibrary.Database
{
    public class TblDeliveryAddress
    {
        public int DeliveryAddressId { get; set; }
        public int CustomerId { get; set; }
        public TblCustomer Customer { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Suburb { get; set; }

        public int StateId { get; set; }

        public LupState State { get; set; }

        public string Postcode { get; set; }

        public List<TblSubscription> Subscriptions { get; set; }

    }
}
