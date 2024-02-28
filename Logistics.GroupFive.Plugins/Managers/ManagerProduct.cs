using Logistics.GroupFive.Plugins.Plugins;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var nService = ProductManager.GetServiceAmbiente2();
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
    }
}
