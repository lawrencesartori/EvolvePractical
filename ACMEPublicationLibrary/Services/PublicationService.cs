using ACMEPublicationLibrary.Database;
using ACMEPublicationLibrary.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACMEPublicationLibrary.Services
{
    public class PublicationService
    {
        private readonly PublicationStore _publicationStore;

        public PublicationService(PublicationStore publicationStore)
        {
            _publicationStore = publicationStore;
        }

        public Dictionary<(int, int), TblPrintDistributor> GetAllCountryPrintDistributions()
        {
            return _publicationStore.GetAllCountryPrintDistributionsAsQueryable().ToDictionary(o => (o.CountryId, o.PublicationId), o => o.PrintDistributor);
        }
    }
}
