using Logistics.GroupFive.Plugins.Repository;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace Logistics.GroupFive.Plugins.Plugins
{
    public class OpportunityManager : IPlugin
    {
        
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            ITracingService trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));


            Entity Opportunity = (Entity)context.InputParameters["Target"];

            if (context.MessageName == "Create")
            {
                Opportunity["alf_newid"] = FormatIdOpportunity(service);
            }
        }

        public string FormatIdOpportunity(IOrganizationService service)
        {

            bool valid = false;

            string identificador = "OPP-";

            do
            {
                identificador = identificador + RandomInt(5) + RandomString(1) + random.Next(1, 9) + RandomString(1) + random.Next(1, 9);

                string id = identificador;

                var opportunitys = RepositoryOpportunity.SearchOpportunityExist(new Guid(id), service);

                valid = opportunitys.Entities.Count == 0;
            } 
            while (valid == false);

            return identificador;
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomInt(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
