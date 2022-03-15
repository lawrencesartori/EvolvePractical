using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACMEPublicationLibrary.Models.Filters
{
    public class SubscriptionFilter
    {
        public bool ActiveOnly { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        public bool IncludeDeliveryAddressDependencies { get; set; }

        public bool IncludePublication { get; set; }
    }
}
