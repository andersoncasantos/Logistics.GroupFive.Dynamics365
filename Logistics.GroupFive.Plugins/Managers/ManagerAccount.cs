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
    public class ManagerAccount
    {
        private IOrganizationService Service { get; set; }

        public ManagerAccount(IOrganizationService service)
        {
            Service = service;
        }
        public void ValidateDuplicityCNPJ(Entity account)
        {
            if (account.Contains("alf_cnpj"))
            {
                string cnpj = account["alf_cnpj"].ToString();
                var accounts = RepositoryAccount.SearchAccountByCNPJ(cnpj, Service);

                if (accounts.Entities.Count() > 0)
                {
                    throw new InvalidPluginExecutionException("CNPJ existe no sistema");
                }
            }
        }
    }
}
