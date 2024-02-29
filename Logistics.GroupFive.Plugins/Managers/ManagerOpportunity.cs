using Logistics.GroupFive.Plugins.Repository;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.GroupFive.Plugins.Managers
{
    public class ManagerOpportunity
    {
        private IOrganizationService Service { get; set; }

        public ManagerOpportunity(IOrganizationService service)
        {
            Service = service;
        }
    }
}
