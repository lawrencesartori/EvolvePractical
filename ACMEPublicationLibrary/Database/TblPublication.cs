using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACMEPublicationLibrary.Database
{
    public class TblPublication
    {
        public int PublicationId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public List<TblSubscription> Subscriptions { get; set; }

        public List<TblPublication_Country> PublicationCountries { get; set; }
    }
}
