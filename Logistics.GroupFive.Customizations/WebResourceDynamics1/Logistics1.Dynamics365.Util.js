if (typeof (LogisticsOne) === "undefined") LogisticsOne = {};
if (typeof (LogisticsOne.Util) === "undefined") LogisticsOne.Util = {};



LogisticsOne.Util = {
    Alert: function (title, description) {
        const configurationText = {
            confirmButtonLabel: "Ok",
            title: title,
            text: description
        };

        const configurationOptions = {
            height: 120,
            widht: 200
        };

        Xrm.Navigation.openAlertDialog(configurationText, configurationOptions);
    }
};