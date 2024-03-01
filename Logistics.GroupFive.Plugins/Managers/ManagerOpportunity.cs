using System;
using System.Collections.Generic;
using Logistics.GroupFive.Plugins.Managers;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;

namespace OpportunityIntegration
{
    public class OpportunityManager: IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // Setting up the source and target environments
            CrmServiceClient sourceClient = new CrmServiceClient("Logistics-Dynamics");
            CrmServiceClient targetClient = new CrmServiceClient("Logistics-Dynamics2A");

            Guid opportunityId = new Guid("OpportunityId");
            Entity opportunity = sourceClient.Retrieve("opportunity", opportunityId, new ColumnSet(true));

            Entity targetOpportunity = new Entity("opportunity");

            // Maping the fields 
            targetOpportunity["ownerid"] = opportunity["ownerid"];
            targetOpportunity["transactioncurrencyid"] = opportunity["transactioncurrencyid"];
            targetOpportunity["parentcontactid"] = opportunity["parentcontactid"];
            targetOpportunity["purchasetimeframe"] = opportunity["purchasetimeframe"];
            targetOpportunity["budgetamount"] = opportunity["budgetamount"];
            targetOpportunity["purchaseprocess"] = opportunity["purchaseprocess"];
            targetOpportunity["msdyn_forecastcategory"] = opportunity["msdyn_forecastcategory"];
            targetOpportunity["description"] = opportunity["description"];
            targetOpportunity["currentsituation"] = opportunity["currentsituation"];
            targetOpportunity["customerneed"] = opportunity["customerneed"];
            targetOpportunity["proposedsolution"] = opportunity["proposedsolution"];
            targetOpportunity["isrevenuesystemcalculated"] = opportunity["isrevenuesystemcalculated"];
            targetOpportunity["pricelevelid"] = opportunity["pricelevelid"];
            targetOpportunity["contactid"] = opportunity["contactid"];
            targetOpportunity["totallineitemamount"] = opportunity["totallineitemamount"];
            targetOpportunity["totallineitemamount"] = opportunity["totallineitemamount"];
            targetOpportunity["discountpercentage"] = opportunity["discountpercentage"];
            targetOpportunity["discountamount"] = opportunity["discountamount"];
            targetOpportunity["totalamountlessfreight"] = opportunity["totalamountlessfreight"];
            targetOpportunity["freightamount"] = opportunity["freightamount"];
            targetOpportunity["totaltax"] = opportunity["totaltax"];
            targetOpportunity["totalamount"] = opportunity["totalamount"];
            targetOpportunity["userId"] = opportunity["userId"];

            Guid targetOpportunityId = targetClient.Create(targetOpportunity); // Creating the opportunity in the target environment

            Console.WriteLine("Opportunity integrated successfully.");
        }
    }
}
