using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace Logistics.GroupFive.Dynamics365
{
    public class Connection
    {
        public static IOrganizationService Service { get; set; }

        public static IOrganizationService GetService()
        {

            if (Service == null)
            {
                var user = "alfapeople@logisticsgrupo5.onmicrosoft.com";
                var senha = "grupo5@&2402";
                var url = "https://orgd43e95aa.crm2.dynamics.com";


                CrmServiceClient crmServiceClient = new CrmServiceClient(
                    "AuthType = Office365;" +
                    $"Username={user};" +
                    $"Password={senha};" +
                    $"Url={url};"
                    );

                Service = crmServiceClient.OrganizationWebProxyClient;
            }
            return Service;
        }
    }
}
