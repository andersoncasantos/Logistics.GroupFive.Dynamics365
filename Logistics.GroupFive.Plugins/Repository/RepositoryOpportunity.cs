using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.GroupFive.Plugins.Repository
{
    public class RepositoryOpportunity
    {
        public static EntityCollection SearchOpportunityExist(Guid id, IOrganizationService service)
        {
            QueryExpression query = new QueryExpression("opportunity");
            query.ColumnSet.AddColumns("opportunityid", "name");
            query.Criteria.AddCondition("opportunityid", ConditionOperator.Equal, id.ToString());

            EntityCollection contasCollection = service.RetrieveMultiple(query);
            return contasCollection;
        }
    }
}
