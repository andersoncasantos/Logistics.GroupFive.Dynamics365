using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.GroupFive.Plugins.Repository
{
    public static class RepositoryAccount
    {
        public static EntityCollection SearchAccountByCNPJ(string cnpj, IOrganizationService service)
        {
            QueryExpression query = new QueryExpression("account");
            query.ColumnSet.AddColumns("accountid", "name");
            query.Criteria.AddCondition("alf_cnpj", ConditionOperator.Equal, cnpj);

            EntityCollection contasCollection = service.RetrieveMultiple(query);
            return contasCollection;
        }
    }
}
