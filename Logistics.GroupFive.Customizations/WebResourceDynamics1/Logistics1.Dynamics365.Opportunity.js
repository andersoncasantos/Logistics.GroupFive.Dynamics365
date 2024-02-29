var url = https://orgd43e95aa.crm2.dynamics.com/; // url 
var organizationName = url.split('/')[3];
var environmentName = url.split('/')[5]; //environment

// Check if the current environment is the second environment 
if (environmentName === 'Logistics-Dynamics2A') {
  // If it is the second environment, disable all opportunity forms
  if (Xrm.Page.entityName === 'opportunity') {
    Xrm.Page.ui.controls.forEach(function (control) {
      if (control.getControlType() === "input" || control.getControlType() === "textarea") {
        control.setDisabled(true);
      }
    });
  }
}
