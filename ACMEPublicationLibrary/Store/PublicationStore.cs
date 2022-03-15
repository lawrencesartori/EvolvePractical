using ACMEPublicationLibrary.Database;
using ACMEPublicationLibrary.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACMEPublicationLibrary.Store
{
    public class PublicationStore
    {
        private readonly PublicationDbContext _publicationDbContext;
        public PublicationStore(PublicationDbContext publicationDbContext)
        {
            _publicationDbContext = publicationDbContext;
        }

        public List<TblPublication> GetPublications(PublicationFilter filter)
        {
            var qry = _publicationDbContext.Publications.AsQueryable();
            if (filter.IncludePublicationCountries)
            {
                //qry = qry.Include(o => o.PublicationCountries);
            }

            return qry.ToList();
        }

        public IQueryable<TblPublication_Country> GetAllCountryPrintDistributionsAsQueryable()
        {
            return _publicationDbContext.PublicationCountries.AsQueryable();
        }

    }
}
