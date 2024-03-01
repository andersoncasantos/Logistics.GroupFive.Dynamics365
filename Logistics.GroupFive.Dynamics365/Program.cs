using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Documents;

namespace Logistics.GroupFive.Dynamics365
{
    public class Program
    {
        static void Main(string[] args)
        {
            testeConsulta(Connection.GetService());
            //testeCreate(Connection.GetServiceAmbiente2());
            //createNew();

            var queryUnitGroup = SearchUnitGroupExist(new Guid("a7861621-f9a3-47ed-bb50-69bf865f67d5"), Connection.GetServiceAmbiente2());
            var queryMainUnit = SearchMainUnitExist(new Guid("7a69cd9c-1698-44b3-a303-c358a025a107"), Connection.GetServiceAmbiente2());
            var queryPriceList = SearchPriceListExist(new Guid("65029c08-f01f-eb11-a812-000d3a33e825"), Connection.GetServiceAmbiente2());

            if (queryUnitGroup.Entities.Count() == 0)
            {
                CreateUnitGroup(new Guid("a7861621-f9a3-47ed-bb50-69bf865f67d5"));
            }

            if (queryPriceList.Entities.Count() == 0)
            {
                CreatePriceList(new Guid("65029c08-f01f-eb11-a812-000d3a33e825"));
            }

            if (queryMainUnit.Entities.Count() == 0)
            {
                CreateMainUnit(new Guid("7a69cd9c-1698-44b3-a303-c358a025a107"));
            }

        }

        public static void testeConsulta(IOrganizationService service) 
        {
            QueryExpression query = new QueryExpression("product");
            query.ColumnSet.AddColumns("productid", "name", "defaultuomscheduleid", "defaultuomid", "pricelevelid");
            query.Criteria.AddCondition("productid", ConditionOperator.Equal, "79197fd0-f71f-eb11-a813-000d3a33f3b4");

            EntityCollection contasCollection = service.RetrieveMultiple(query);
            var teste = contasCollection;
            //return contasCollection;
        }

        public static void createNew() 
        {
            var service = Connection.GetService();
            
            Entity conta = new Entity("opportunity");           
            conta["name"] = "Primeira conta via Código";
            conta.Attributes.Add("customerid", "opp-344h4343h433");            

            var idConta = service.Create(conta);
        }

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

            QueryExpression query = new QueryExpression("productpricelevel");
            query.ColumnSet.AddColumn("productpricelevelid");
            //query.Criteria.AddCondition("pricelevelid", ConditionOperator.Equal, id.ToString());

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
        private static void CreateUnitGroup(Guid id)
        {
            var service = Connection.GetServiceAmbiente2();
            var query = SearchUnitGroup(id);
            Entity entity = new Entity("uomschedule");

            entity["name"] = query.Entities.First()["name"].ToString();
            entity["description"] = query.Entities.First()["description"].ToString();

            service.Create(entity);
        }
        private static void CreatePriceList(Guid id)
        {
            var service = Connection.GetServiceAmbiente2();
            var query = SearchPriceList(id);
            Entity entity = new Entity("pricelevel");

            var queryUnitGroup = SearchUnitGroupExist(new Guid(query.Entities.First()["uomscheduleid"].ToString()), service);

            if (queryUnitGroup.Entities.Count() == 0)
            {
                CreateUnitGroup(new Guid(query.Entities.First()["uomscheduleid"].ToString()));
            }

            entity["uomscheduleid"] = new EntityReference("uomschedule", new Guid(query.Entities.First()["uomscheduleid"].ToString()));//lookup
            entity["name"] = query.Entities.First()["name"].ToString();
            entity["quantity"] = Convert.ToDecimal(query.Entities.First()["quantity"]);

            service.Create(entity);
        }

        private static void CreateMainUnit(Guid id)
        {
            var service = Connection.GetServiceAmbiente2();
            var query = SearchMainUnit(id);
            Entity entity = new Entity("uom");

            var queryTransitionCurrency = SearchTransactionCurrencyExist(new Guid(query.Entities.First()["transactioncurrencyid"].ToString()), service);

            if (queryTransitionCurrency.Entities.Count() == 0)
            {
                CreateTransationCurrency(new Guid(query.Entities.First()["transactioncurrencyid"].ToString()));
            }

            entity["name"] = query.Entities.First()["name"].ToString();
            entity["begindate"] = Convert.ToDateTime(query.Entities.First()["begindate"].ToString());
            entity["enddate"] = Convert.ToDateTime(query.Entities.First()["enddate"].ToString());
            entity["transactioncurrencyid"] = new EntityReference("transactioncurrency", new Guid(query.Entities.First()["transactioncurrencyid"].ToString()));
            entity["description"] = query.Entities.First()["description"].ToString();


            service.Create(entity);
        }

        private static void CreateTransationCurrency(Guid id)
        {
            var service = Connection.GetServiceAmbiente2();
            var query = SearchTransactionCurrency(id);
            Entity entity = new Entity("transactioncurrency");

            entity["isocurrencycode"] = query.Entities.First()["isocurrencycode"].ToString();
            entity["currencyname"] = query.Entities.First()["currencyname"].ToString();
            entity["currencyprecision"] = Convert.ToInt32(query.Entities.First()["currencyprecision"].ToString());
            entity["currencysymbol"] = query.Entities.First()["currencysymbol"].ToString();
            entity["exchangerate"] = Convert.ToDecimal(query.Entities.First()["exchangerate"]);

            service.Create(entity);
        }

        //public static void CreateProductSecound(Entity product)
        //{
        //    var nService = Connection.GetServiceAmbiente2();

        //    var queryUnitGroup = SearchUnitGroupExist(new Guid(product["defaultuomscheduleid"].ToString()), nService);
        //    var queryMainUnit = SearchMainUnitExist(new Guid(product["defaultuomid"].ToString()), nService);
        //    var queryPriceList = SearchPriceListExist(new Guid(product["pricelevelid"].ToString()), nService);

        //    if (queryUnitGroup.Entities.Count() == 0)
        //    {
        //        CreateUnitGroup(new Guid(product["defaultuomscheduleid"].ToString()));
        //    }

        //    if (queryPriceList.Entities.Count() == 0)
        //    {
        //        CreatePriceList(new Guid(product["pricelevelid"].ToString()));
        //    }

        //    if (queryMainUnit.Entities.Count() == 0)
        //    {
        //        CreateMainUnit(new Guid(product["pricelevelid"].ToString()));
        //    }

        //    Entity products2 = new Entity("product");

        //    products2["name"] = product["name"];
        //    products2["productnumber"] = product["productnumber"];
        //    products2["parentproductid"] = product["parentproductid"];
        //    products2["validfromdate"] = product["validfromdate"];
        //    products2["validtodate"] = product["validtodate"];
        //    products2["description"] = product["description"];
        //    products2["defaultuomscheduleid"] = product["defaultuomscheduleid"];
        //    products2["defaultuomid"] = product["defaultuomid"];
        //    products2["pricelevelid"] = product["pricelevelid"];
        //    products2["quantitydecimal"] = product["quantitydecimal"];
        //    products2["subjectid"] = product["subjectid"];
        //    products2["currentcost"] = product["currentcost"];
        //    products2["standardcost"] = product["standardcost"];
        //    products2["price"] = product["price"];

        //    nService.Create(products2);
        //}
    }
}
