using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.GroupFive.Plugins.Repository
{
    public static class RepositoryContact
    {
        public static EntityCollection SearchAccountByCPF(string cpf, IOrganizationService service)
        {
            QueryExpression query = new QueryExpression("contact");
            query.ColumnSet.AddColumns("contactid", "fullname");
            query.Criteria.AddCondition("alf_cpfcontato", ConditionOperator.Equal, cpf);


            EntityCollection contasCollection = service.RetrieveMultiple(query);
            return contasCollection;
        }
    }
}
