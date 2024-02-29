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

	OnChangeViaCep: function (context) {
		const formContext = context.getFormContext();

		let accountCep = formContext.getAttribute("address1_postalcode").getValue();

		if (accountCep) {
			let cepClean = accountCep.replaceAll(".", "").replace("/", "").replace("-", ""); 

			if (cepClean.length == 8 && /\d{8}/.test(cepClean)) {
				let viacep = "https://viacep.com.br/ws/" + cepClean + "/json";

				var request = new XMLHttpRequest();
				request.open("GET", viacep, true);
				request.onreadystatechange = function () {

					if (request.readyState === 4 && request.status === 200) {

						var response = JSON.parse(request.responseText);

						if (!response.erro) {
							formContext.getAttribute("alf_logradouro").setValue(response.logradouro);
							formContext.getAttribute("alf_complemento").setValue(response.complemento);
							formContext.getAttribute("alf_bairro").setValue(response.bairro);
							formContext.getAttribute("alf_localidade").setValue(response.localidade);
							formContext.getAttribute("alf_uf").setValue(response.uf);
							formContext.getAttribute("alf_ibge").setValue(response.ibge);
							formContext.getAttribute("alf_ddd").setValue(response.ddd);
						} else {
							LogisticsOne.Util.Alert("Atencao!", "CEP nao foi encontrado");
							formContext.getAttribute("address1_postalcode").setValue(null);
						}
					}
				};
				request.send();
			}
			else {
				LogisticsOne.Util.Alert("Atencao!", "CEP invalido");
				formContext.getAttribute("address1_postalcode").setValue(null);
			}
		}
	},

	FormatName: function (context) {
		const formContext = context.getFormContext();

		const name = formContext.getAttribute("name").getValue();

		let text = name.toString().toLowerCase().trim();
		var wordT = "";
		var phrase = "";

		var textBroken = text.split(' ');

		textBroken.forEach(function (value, index) {

			let firstL = value.split('');

			firstL.forEach(function (value, index) {

				let letter = value;
				if (index === 0) {
					let wordUpper = value.toUpperCase();
					wordT = wordUpper;
				} else {
					wordT = wordT + letter;
				}

			});
			phrase = phrase + wordT + " ";
		});
		//console.log(phrase.trim());
		formContext.getAttribute("name").setValue(phrase.trim());
	},
	OnChangeVisibilityCnpjCpf: function (context) {
		let formContext = context.getFormContext();
		let tipoid = formContext.getAttribute("alf_tipo").getValue();

		if (tipoid === LogisticsOne.Account.Enumerador.Tipo.PessoaJuridica || tipoid === null) {
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

	OnChangeCNPJ: function (context) {
		const formContext = context.getFormContext();
		let cnpj = formContext.getAttribute("alf_cnpj").getValue();

		if (cnpj) {
			let cnpjClean = cnpj.replaceAll(".", "").replace("/", "").replace("-", "");

			if (cnpjClean.length === 14) {
				let cnpjFormatted = cnpjClean.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5");
				formContext.getAttribute("alf_cnpj").setValue(cnpjFormatted);
			}
			else {
				formContext.getAttribute("alf_cnpj").setValue(null)
				LogisticsOne.Util.Alert("Atencao!", "CNPJ que foi inserido e invalido, digite novamente");
			}
		}
	},

	OnChangeCPF: function (context) {
		const formContext = context.getFormContext();
		let cpf = formContext.getAttribute("alf_cpf").getValue();

		if (cpf) {
			let cpfClean = cpf.replaceAll(".", "").replace("/", "").replace("-", "");

			if (cpfClean.length === 11) {
				let cpfFormatted = cpfClean.replace(/^(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
				formContext.getAttribute("alf_cpf").setValue(cpfFormatted);
			}
			else {
				formContext.getAttribute("alf_cpf").setValue(null)
				LogisticsOne.Util.Alert("Atencao!", "CPF que foi inserido e invalido, digite novamente");
			}
		}
	},
}