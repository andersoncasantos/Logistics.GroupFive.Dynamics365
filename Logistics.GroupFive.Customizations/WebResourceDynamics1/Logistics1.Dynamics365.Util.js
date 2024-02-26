if (typeof (LogisticsOne) === "undefined") LogisticsOne = {};
if (typeof (LogisticsOne.Util) === "undefined") LogisticsOne.Util = {};



LogisticsOne.Util = {
    Alert: function (title, description) {
        const configuracaoTexto = {
            confirmButtonLabel: "Ok",
            title: title,
            text: description
        };

        const configuracaoOpcoes = {
            height: 120,
            widht: 200
        };

        Xrm.Navigation.openAlertDialog(configurationText, configurationOptions);
    }
};