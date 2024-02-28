using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.GroupFive.PluginsDyn2.Plugins
{
    public class ProductManager : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            ITracingService trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            Entity product = (Entity)context.InputParameters["Target"];
            bool productIsIntegration = (bool)product["dyn2_isintegrationTeste"];

            if (!productIsIntegration)
            {
                throw new InvalidPluginExecutionException("Um produto não pode ser cadastrado diretamente no Dynamics 2");
            }
        }
    }
}
