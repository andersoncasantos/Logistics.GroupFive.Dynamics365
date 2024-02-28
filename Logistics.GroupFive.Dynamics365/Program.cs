using Microsoft.Xrm.Sdk;
using System;
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
            testeCreate(Connection.GetServiceAmbiente2());
        }

        public static void testeCreate(IOrganizationService service) 
        {

            Entity products2 = new Entity("product");

            products2["name"] = "Criacao Teste";
            products2["productnumber"] = "teste";
            products2["defaultuomscheduleid"] = new EntityReference("uomschedule", new Guid("79197fd0-f71f-eb11-a813-000d3a33f3b4")); 
            products2["defaultuomid"] = new EntityReference("pricelevel", new Guid("65029c08-f01f-eb11-a812-000d3a33e825")); 
            products2["quantitydecimal"] = 2;
          

            service.Create(products2);
        }
    }
}
