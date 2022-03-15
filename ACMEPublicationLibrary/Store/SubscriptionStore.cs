using ACMEPublicationLibrary.Database;
using ACMEPublicationLibrary.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACMEPublicationLibrary.Store
{
    public class SubscriptionStore
    {
        private readonly PublicationDbContext _publicationDbContext;

        public SubscriptionStore(PublicationDbContext publicationDbContext)
        {
            _publicationDbContext = publicationDbContext;
        }

        public List<TblSubscription> GetSubscriptions(SubscriptionFilter filter)
        {
            var qry = _publicationDbContext.Subscriptions.AsQueryable();
            // qry = qry.Include(o=>o.Publication).Include(o=>o.DeliveryAddress)

            if (filter.IncludePublication)
            {
                //qry = qry.Include(o => o.Publication);
            }

            if (filter.IncludeDeliveryAddressDependencies)
            {
                //qry = qry.Include(o => o.DeliveryAddress.Customer).Include(d=>d.DeliveryAddress).ThenInclude(s>s.State).ThenInclude(c=>c.Country);
            }

            if (filter.DateStart != null) 
            {
                qry = qry.Where(o => o.DateStart >= filter.DateStart);
            }
            if (filter.DateEnd != null)
            {
                qry = qry.Where(o => o.DateEnd <= filter.DateEnd);
            }
            if (filter.ActiveOnly)
            {
                qry = qry.Where(o => o.Active);
            }

            return qry.ToList();
        }
    }
}
