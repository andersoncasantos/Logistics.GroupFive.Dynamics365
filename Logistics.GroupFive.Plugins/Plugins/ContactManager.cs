using Logistics.GroupFive.Plugins.Managers;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.GroupFive.Plugins.Plugins
{
    public class ContactManager : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            ITracingService trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));


            Entity contact = (Entity)context.InputParameters["Target"];


            ManagerContact managerContact = new ManagerContact(service);
            managerContact.ValidateDuplicityCPF(contact);
        }

    }
}