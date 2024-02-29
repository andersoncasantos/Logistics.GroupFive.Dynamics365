using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.GroupFive.Plugins.Repository
{
    public static class RepositoryProduct
    {
        public static EntityCollection SearchUnitGroupExist(Guid id, IOrganizationService service)
        {
            QueryExpression query = new QueryExpression("uomschedule");
            query.ColumnSet.AddColumns("uomscheduleid", "name");
            query.Criteria.AddCondition("uomscheduleid", ConditionOperator.Equal, id.ToString());

            EntityCollection contasCollection = service.RetrieveMultiple(query);
            return contasCollection;
        }

        public static EntityCollection SearchPriceListExist(Guid id, IOrganizationService service)
        {
            QueryExpression query = new QueryExpression("pricelevel");
            query.ColumnSet.AddColumns("pricelevelid", "name");
            query.Criteria.AddCondition("pricelevelid", ConditionOperator.Equal, id.ToString());

            EntityCollection contasCollection = service.RetrieveMultiple(query);
            return contasCollection;
        }

        public static EntityCollection SearchMainUnitExist(Guid id, IOrganizationService service)
        {
            QueryExpression query = new QueryExpression("uom");
            query.ColumnSet.AddColumns("uomid", "name");
            query.Criteria.AddCondition("uomid", ConditionOperator.Equal, id.ToString());

            EntityCollection contasCollection = service.RetrieveMultiple(query);
            return contasCollection;
        }

        public static EntityCollection SearchTransactionCurrencyExist(Guid id, IOrganizationService service)
        {
            QueryExpression query = new QueryExpression("transactioncurrency");
            query.ColumnSet.AddColumns("transactioncurrencyid", "name");
            query.Criteria.AddCondition("transactioncurrencyid", ConditionOperator.Equal, id.ToString());

            EntityCollection contasCollection = service.RetrieveMultiple(query);
            return contasCollection;
        }

        public static EntityCollection SearchUnitGroup(Guid id)
        {
            var service = Connection.GetService();

            QueryExpression query = new QueryExpression("uomschedule");
            query.ColumnSet.AddColumns("uomscheduleid", "name");
            query.Criteria.AddCondition("uomscheduleid", ConditionOperator.Equal, id.ToString());

            EntityCollection contasCollection = service.RetrieveMultiple(query);
            return contasCollection;
        }

        public static EntityCollection SearchPriceList(Guid id)
        {
            var service = Connection.GetService();

            QueryExpression query = new QueryExpression("pricelevel");
            query.ColumnSet.AddColumns("pricelevelid", "name");
            query.Criteria.AddCondition("pricelevelid", ConditionOperator.Equal, id.ToString());

            EntityCollection contasCollection = service.RetrieveMultiple(query);
            return contasCollection;
        }

        public static EntityCollection SearchMainUnit(Guid id)
        {
            var service = Connection.GetService();

            QueryExpression query = new QueryExpression("uom");
            query.ColumnSet.AddColumns("uomid", "name");
            query.Criteria.AddCondition("uomid", ConditionOperator.Equal, id.ToString());

            EntityCollection contasCollection = service.RetrieveMultiple(query);
            return contasCollection;
        }

        public static EntityCollection SearchTransactionCurrency(Guid id)
        {
            var service = Connection.GetService();

            QueryExpression query = new QueryExpression("transactioncurrency");
            query.ColumnSet.AddColumns("transactioncurrencyid", "name");
            query.Criteria.AddCondition("transactioncurrencyid", ConditionOperator.Equal, id.ToString());

            EntityCollection contasCollection = service.RetrieveMultiple(query);
            return contasCollection;
        }
    }
}
