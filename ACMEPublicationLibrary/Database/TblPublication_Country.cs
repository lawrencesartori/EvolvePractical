using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACMEPublicationLibrary.Database
{
    public class TblPublication_Country
    {
        public int PublicationId { get; set; }
        public TblPublication Publication {get;set;}
        public int CountryId { get; set; }
        public LupCountry Country { get; set; }
        public int PrintDistributorId { get; set; }
        public TblPrintDistributor PrintDistributor { get; set; }
    }
}
