if (typeof (LogisticsOne) === "undefined") LogisticsOne = {};
if (typeof (LogisticsOne.Contact) === "undefined") LogisticsOne.Contact = {};



LogisticsOne.Contact = {

	Enumerador: {
		Tipo: {
			PessoaFisica: 748920000,
			PessoaJuridica: 748920001,
			Internacional: 748920002
		}
	},

	OnChangeVisibilityCnpjCpf: function (context) {
		let formContext = context.getFormContext();
		let tipoid = formContext.getAttribute("alf_tipocontato").getValue();

		if (tipoid === LogisticsOne.Contact.Enumerador.Tipo.PessoaFisica || tipoid === null) {
			formContext.getControl("alf_cpfcontato").setVisible(true);
			formContext.getControl("alf_cnpjcontato").setVisible(false);
		}

		else if (tipoid === LogisticsOne.Contact.Enumerador.Tipo.PessoaJuridica) {
			formContext.getControl("alf_cnpjcontato").setVisible(true);
			formContext.getControl("alf_cpfcontato").setVisible(false);
		}

		else if (tipoid === LogisticsOne.Contact.Enumerador.Tipo.Internacional) {
			formContext.getControl("alf_cpfcontato").setVisible(false);
			formContext.getControl("alf_cnpjcontato").setVisible(false);
		}
	},

	OnLoadVisibilityCnpjCpf: function (context) {
		LogisticsOne.Contact.OnChangeVisibilityCnpjCpf(context);
	},
}