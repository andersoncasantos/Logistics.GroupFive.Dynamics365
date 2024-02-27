using Logistics.GroupFive.Plugins.Repository;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.GroupFive.Plugins.Managers
{
    public class ManagerContact
    {
        private IOrganizationService Service { get; set; }

        public ManagerContact(IOrganizationService service)
        {
            Service = service;
        }
        public void ValidateDuplicityCPF(Entity contact)
        {
            if (contact.Contains("alf_cpfcontato"))
            {
                string cpf = contact["alf_cpfcontato"].ToString();
                var contacts = RepositoryContact.SearchAccountByCPF(cpf, Service);

                if (contacts.Entities.Count() > 0)
                {
                    throw new InvalidPluginExecutionException("CPF existe no sistema");
                }
            }
        }
    }
}
