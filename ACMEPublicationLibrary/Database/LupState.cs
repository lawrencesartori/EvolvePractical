using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACMEPublicationLibrary.Database
{
    public class LupState
    {
        public int StateId { get; set; }
        public int CountryId { get; set; }

        public LupCountry Country { get; set; }
    }
}
