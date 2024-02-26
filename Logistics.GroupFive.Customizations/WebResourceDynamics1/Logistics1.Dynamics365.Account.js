if (typeof (LogisticsOne) === "undefined") LogisticsOne = {};
if (typeof (LogisticsOne.Account) === "undefined") LogisticsOne.Account = {};



LogisticsOne.Account = {

	Enumerador: {
		Tipo: {
			PessoaFisica: 748910000,
			PessoaJuridica: 748910001,
			Internacional: 748910002
		}
	},

	OnChangeVisibilityCnpjCpf: function (context) {
		let formContext = context.getFormContext();
		let tipoid = formContext.getAttribute("alf_tipo").getValue();

		if (tipoid === LogisticsOne.Account.Enumerador.Tipo.PessoaJuridica) {
			formContext.getControl("alf_cnpj").setVisible(true);
			formContext.getControl("alf_cpf").setVisible(false);
		}
		else if (tipoid === LogisticsOne.Account.Enumerador.Tipo.PessoaFisica) {
			formContext.getControl("alf_cpf").setVisible(true);
			formContext.getControl("alf_cnpj").setVisible(false);
		}
		else if (tipoid === LogisticsOne.Account.Enumerador.Tipo.Internacional) {
			formContext.getControl("alf_cpf").setVisible(false);
			formContext.getControl("alf_cnpj").setVisible(false);
		}
	},

	OnLoadVisibilityCnpjCpf: function (context) {
		LogisticsOne.Account.OnChangeVisibilityCnpjCpf(context);
	},
}