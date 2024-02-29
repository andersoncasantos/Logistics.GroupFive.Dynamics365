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
    ValidateCPF: function (context) { /
    var formContext = context.getFormContext();
    var cpfField = "alf_cpfcontato"; 
    var cpf = formContext.getAttribute(cpfField).getValue();

    if (!cpf) {
        LogisticsOne.Contact.DynamicsCustomAlert("CPF inválido", "O campo CPF é obrigatório");
        return false; // Verificar se o campo está vazio 
    }

    if (!cpf || cpf.length != 11 || isNaN(cpf)) {
        LogisticsOne.Contact.DynamicsCustomAlert("CPF inválido", "Por favor, insira um CPF válido");
        return false; // Começo da lógica de validação
    }

    // Verificação dos dígitos verificadores
    var soma = 0;
    var resto;

    for (var i = 0; i < 9; i++) {
        soma += parseInt(cpf.charAt(i)) * (10 - i);
    }

    resto = (soma * 10) % 11;

    if (resto == 10 || resto == 11) {
        resto = 0;
    }

    if (resto != parseInt(cpf.charAt(9))) {
        LogisticsOne.Contact.DynamicsCustomAlert("CPF inválido", "Por favor, insira um CPF válido");
        return false;
    }

    soma = 0;

    for (var i = 0; i < 10; i++) {
        soma += parseInt(cpf.charAt(i)) * (11 - i);
    }

    resto = (soma * 10) % 11;

    if (resto == 10 || resto == 11) {
        resto = 0;
    }

    if (resto != parseInt(cpf.charAt(10))) {
        LogisticsOne.Contact.DynamicsCustomAlert("CPF inválido", "Por favor, insira um CPF válido");
        return false;
    }

    return true;
}

	// Formatando o CPF:
	function formatarCPF(cpf) {
    cpf = cpf.replace(/\D/g, ''); // Removendo todos os caracteres que não sejam dígitos

    // Aplicando a formatação do CPF
    cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2');
    cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2');
    cpf = cpf.replace(/(\d{3})(\d{1,2})$/, '$1-$2');

    return cpf;
}

	ValidateCNPJ: function (context) {
		debugger;
		var formContext = context.getFormContext();
		var cnpjField = "alf_cnpjcontato";
		var cnpj = formContext.getAttribute("alf_cnpjcontato").getValue();

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
			formContext.getAttribute("alf_cnpjcontato").setValue("");
			LogisticsOne.Util.DynamicsCustomAlert("Atenção!", "CNPJ que foi inserido é inválido, tente novamente!");


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
			formContext.getAttribute("alf_cnpjcontato").setValue("");
			LogisticsOne.Util.DynamicsCustomAlert("Atenção!", "CNPJ que foi inserido é inválido, tente novamente!");

			return false;
		}
		cnpj = cnpj.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$/, '$1.$2.$3/$4-$5');
		formContext.getAttribute(cnpjField).setValue(cnpj);
		return true;
	},

	OnChangeCNPJ: function (context) {

		const formContext = context.getFormContext();
		let cnpj = formContext.getAttribute("alf_cnpjcontato").getValue();

		if (cnpj) {

			let cnpjLimpo = cnpj.replaceAll(".", "").replace("/", "").replace("-", "");

			if (cnpjLimpo.length === 14) {

				let cnpjFormatado = cnpjLimpo.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5");
				formContext.getAttribute("alf_cnpjcontato").setValue(cnpjFormatado);

			} else {

				formContext.getAttribute("alf_cnpjcontato").setValue(null);
				LogisticsOne.Util.DynamicsCustomAlert("Atenção!", "CNPJ que foi inserido é inválido, tente novamente!");


			}
		}
	},

	DynamicsCustomAlert: function (alertText, alertTitle) {
		var alertStrings = {
			confirmButtonLabel: "OK",
			text: alertText,
			title: alertTitle
		};

		var alertOptions = {
			height: 120,
			width: 200
		};

		Xrm.Navigation.openAlertDialog(alertStrings, alertOptions);
	}
}
