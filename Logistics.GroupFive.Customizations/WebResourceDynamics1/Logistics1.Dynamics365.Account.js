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

	ValidateCNPJ: function (context) {
		debugger;
		var formContext = context.getFormContext();
		var cnpjField = "alf_cnpj";
		var cnpj = formContext.getAttribute("alf_cnpj").getValue();

		if (
			!cnpj ||
			cnpj.length != 14 ||
			cnpj == "00000000000000" ||
			cnpj == "11111111111111" ||
			cnpj == "22222222222222" ||
			cnpj == "33333333333333" ||
			cnpj == "44444444444444" ||
			cnpj == "55555555555555" ||
			cnpj == "66666666666666" ||
			cnpj == "77777777777777" ||
			cnpj == "88888888888888" ||
			cnpj == "99999999999999"
		) {
			formContext.getAttribute("alf_cnpj").setValue("");
			LogisticsOne.Util.Alert("CNPJ Invalido ", " Insira um CNPJ valido");


			return false
		}
		cnpj = cnpj.replace(/[^\d]+/g, '');

		// Calcular os dígitos verificadores

		var soma = 0;
		var peso = 2;

		for (var i = 11; i >= 0; i--) {
			soma += parseInt(cnpj.charAt(i)) * peso;
			peso = peso === 9 ? 2 : peso + 1;
		}

		var resto = soma % 11;
		var digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

		soma = 0;
		peso = 2;

		for (var i = 12; i >= 0; i--) {
			soma += parseInt(cnpj.charAt(i)) * peso;
			peso = peso === 9 ? 2 : peso + 1;
		}

		resto = soma % 11;
		var digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

		// Verificar se os dígitos verificadores estão corretos
		if (parseInt(cnpj.charAt(12)) !== digitoVerificador1 || parseInt(cnpj.charAt(13)) !== digitoVerificador2) {
			formContext.getAttribute("alf_cnpj").setValue("");
			LogisticsOne.Util.Alert("CNPJ Inválido ", " Insira um CNPJ válido");

			return false;
		}
		cnpj = cnpj.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$/, '$1.$2.$3/$4-$5');
		formContext.getAttribute(cnpjField).setValue(cnpj);
		return true;
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

			let cnpjLimpo = cnpj.replaceAll(".", "").replace("/", "").replace("-", "");

			if (cnpjLimpo.length === 14) {

				let cnpjFormatado = cnpjLimpo.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5");
				formContext.getAttribute("alf_cnpj").setValue(cnpjFormatado);

			} else {

				formContext.getAttribute("alf_cnpj").setValue(null);
				LogisticsOne.Util.Alert("Atenção!", "CNPJ que foi inserido é inválido, tente novamente!");
			}
		}
	},
}