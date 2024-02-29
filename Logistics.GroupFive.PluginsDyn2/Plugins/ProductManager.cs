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
                if (context.MessageName.ToLower() == "create" && context.PrimaryEntityName.ToLower() == "product")
                {
                    if (!UserHasPermissionToCreateAccount(context))
                    {
                        throw new InvalidPluginExecutionException("Um produto não pode ser cadastrado diretamente no Dynamics 2");
                    }
                }
            }

            private bool UserHasPermissionToCreateAccount(IPluginExecutionContext context)
            {
                return false;
            }
        }
}
