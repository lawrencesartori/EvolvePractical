using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACMEPublicationLibrary.Database
{
    public class TblPrintDistributor
    {
        public int PrintDistributorId { get; set; }
        public string Name { get; set; }
        public string RequestEndpoint { get; set; }
        public string RequestUserName { get; set; }
        public string RequestPassword { get; set; }
        public string RequestToken { get; set; }
        public DateTime? RequestTokenExpiryDate { get; set; }

        public List<TblPublication_Country> PublicationCountries { get; set; }
    }
}
