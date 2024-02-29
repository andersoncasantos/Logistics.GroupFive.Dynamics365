using Logistics.GroupFive.Plugins.Plugins;
using Logistics.GroupFive.Plugins.Repository;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace Logistics.GroupFive.Plugins.Managers
{
    public class ManagerProduct
    {
        private IOrganizationService Service { get; set; }

        public ManagerProduct(IOrganizationService service)
        {
            Service = service;
        }

        public static void CreateProductSecound(Entity product)
        {
            var nService = Connection.GetServiceAmbiente2();

            var queryUnitGroup = RepositoryProduct.SearchUnitGroupExist(new Guid(product["defaultuomscheduleid"].ToString()), nService);
            var queryMainUnit = RepositoryProduct.SearchMainUnitExist(new Guid(product["defaultuomid"].ToString()), nService);
            var queryPriceList = RepositoryProduct.SearchPriceListExist(new Guid(product["pricelevelid"].ToString()), nService);

            if (queryUnitGroup.Entities.Count() == 0)
            {
                CreateUnitGroup(new Guid(product["defaultuomscheduleid"].ToString()));
            }

            if (queryPriceList.Entities.Count() == 0)
            {
                CreatePriceList(new Guid(product["pricelevelid"].ToString()));
            }

            if (queryMainUnit.Entities.Count() == 0)
            {
                CreateMainUnit(new Guid(product["pricelevelid"].ToString()));
            }

            Entity products2 = new Entity("product");

            products2["name"] = product["name"];
            products2["productnumber"] = product["productnumber"];
            products2["parentproductid"] = product["parentproductid"];
            products2["validfromdate"] = product["validfromdate"];
            products2["validtodate"] = product["validtodate"];
            products2["description"] = product["description"];
            products2["defaultuomscheduleid"] = product["defaultuomscheduleid"];
            products2["defaultuomid"] = product["defaultuomid"];
            products2["pricelevelid"] = product["pricelevelid"];
            products2["quantitydecimal"] = product["quantitydecimal"];
            products2["subjectid"] = product["subjectid"];
            products2["currentcost"] = product["currentcost"];
            products2["standardcost"] = product["standardcost"];
            products2["price"] = product["price"];

            nService.Create(products2);
        }

        private static void CreateUnitGroup(Guid id)
        {
            var service = Connection.GetServiceAmbiente2();
            var query = RepositoryProduct.SearchUnitGroup(id);
            Entity entity = new Entity("uomschedule");

            entity["name"] = query.Entities.First()["name"].ToString();
            entity["description"] = query.Entities.First()["description"].ToString();

            service.Create(entity);
        }

        private static void CreatePriceList(Guid id)
        {
            var service = Connection.GetServiceAmbiente2();
            var query = RepositoryProduct.SearchPriceList(id);
            Entity entity = new Entity("pricelevel");

            var queryUnitGroup = RepositoryProduct.SearchUnitGroupExist(new Guid(query.Entities.First()["uomscheduleid"].ToString()), service);

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
            var query = RepositoryProduct.SearchMainUnit(id);
            Entity entity = new Entity("uom");

            var queryTransitionCurrency = RepositoryProduct.SearchTransactionCurrencyExist(new Guid(query.Entities.First()["transactioncurrencyid"].ToString()), service);

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
            var query = RepositoryProduct.SearchTransactionCurrency(id);
            Entity entity = new Entity("transactioncurrency");

            entity["isocurrencycode"] = query.Entities.First()["isocurrencycode"].ToString();
            entity["currencyname"] = query.Entities.First()["currencyname"].ToString();
            entity["currencyprecision"] = Convert.ToInt32(query.Entities.First()["currencyprecision"].ToString());
            entity["currencysymbol"] = query.Entities.First()["currencysymbol"].ToString();
            entity["exchangerate"] = Convert.ToDecimal(query.Entities.First()["exchangerate"]);

            service.Create(entity);
        }
    }
}
