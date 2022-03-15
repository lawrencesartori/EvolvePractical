using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACMEPublicationLibrary.Database
{
    /*
       This is just a mock version of a database context. Typically I would use a db first approach when generating the database.
       I will treat any code that touches this context as close as I can as if I was using entity framework
     */
    public class PublicationDbContext
    {

        public PublicationDbContext()
        {
            Customers = new List<TblCustomer>();
            DeliveryAddresses = new List<TblDeliveryAddress>();
            Subscriptions = new List<TblSubscription>();
            Publications = new List<TblPublication>();
            States = new List<LupState>();
            Countries = new List<LupCountry>();
            PrintDistributors = new List<TblPrintDistributor>();
            PublicationCountries = new List<TblPublication_Country>();
        }
        public List<TblCustomer> Customers { get; set; }
        public List<TblDeliveryAddress> DeliveryAddresses { get; set; }
        public List<TblSubscription> Subscriptions { get; set; }

        public List<TblPublication> Publications { get; set; }
        public List<LupState> States { get; set; }
        public List<LupCountry> Countries { get; set; }
        public List<TblPrintDistributor> PrintDistributors { get; set; }
        public List<TblPublication_Country> PublicationCountries { get; set; }
    }
}
