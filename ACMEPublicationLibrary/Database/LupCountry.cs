using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACMEPublicationLibrary.Database
{
    public class LupCountry
    {
        public int CountryID { get; set; }
        public string Name { get; set; }

        public List<LupState> States { get; set; }

        public List<TblPublication_Country> PublicationCountries { get; set; }
    }
}
