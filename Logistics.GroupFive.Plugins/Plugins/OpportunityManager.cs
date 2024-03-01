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
    public class OpportunityManager : PluginCore
    {
        public Entity Oportunidade { get; set; }
        public CrmServiceClient ServiceClient { get; set; }

        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            var service = Connection.GetServiceAmbiente2();

            this.Oportunidade = (Entity)this.Context.InputParameters["Target"];
            this.Oportunidade.Attributes.Add("grp_idprincipal", FormatIdOpportunity(ServiceClient));
            
            this.TracingService.Trace("Serviço recuperado com sucesso");

            service.Create(SetNewOportunidadeAttributes());
            this.TracingService.Trace("Oportunidade nova criada");
        }


        //public void Execute(IServiceProvider serviceProvider)
        //{
        //    IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
        //    IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

        //    IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

        //    ITracingService trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));


        //    Entity Opportunity = (Entity)context.InputParameters["Target"];

        //    if (context.MessageName == "Create")
        //    {
        //        Opportunity.Attributes.Add("NewIdGroup", FormatIdOpportunity(service));
        //    }
        //}

        private Entity SetNewOportunidadeAttributes()
        {
            Entity oportunidadeToCreate = new Entity("opportunity");

            string[] oportunidadeAtributos = new string[]
            {
                "name",
                "purchasetimeframe",
                "description",
                "budgetamount_base",
                "purchaseprocess",
                "currentsituation",
                "customerneed",
                "proposedsolution"
            };
            foreach (string att in oportunidadeAtributos)
            {
                if (this.Oportunidade.Attributes.TryGetValue(att, out object value))
                {
                    oportunidadeToCreate[att] = value;
                }
            }

            oportunidadeToCreate["dyn2_idprincipal"] = this.Oportunidade["grp_idprincipal"];


            return oportunidadeToCreate;
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
