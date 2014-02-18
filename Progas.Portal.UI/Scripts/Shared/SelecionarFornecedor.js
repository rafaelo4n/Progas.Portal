SelecionarFornecedor = {
    FornecedorSelecionado: null,
    Configurar: function () {
        $('body').append('<div id="divSelecionarFornecedor" class="janelaModal"></div>');
        $('#divSelecionarFornecedor').customDialog({
            title: 'Selecionar Fornecedor',
            buttons: {
                "Confirmar": function () {
                    if (SelecionarFornecedor.FornecedorSelecionado == null) {
                        Mensagem.ExibirMensagemDeErro("É necessário selecionar uma Transportadora.");
                        return;
                    }
                    $('#CodigoFornecedor').val(SelecionarFornecedor.FornecedorSelecionado.Codigo);
                    $('#trans').val(unescape(SelecionarFornecedor.FornecedorSelecionado.Nome));
                    $(this).dialog("close");
                },
                "Cancelar": function () {
                    $(this).dialog("close");
                }
            }
        });
        $('#btnSelecionarFornecedor').click(function () {

            $('#divSelecionarFornecedor').load(UrlPadrao.SelecionarFornecedor
                + '/?Codigo=' + $('#CodigoFornecedor').val() + '&Nome=' + escape($('#trans').val()),
                function (response, status, xhr) {
                    $('#divSelecionarFornecedor').dialog('open');
                });
        });

    }, // fim configurar

    // Funcao retornar o Transportadora no Botao COPIAR da tela de Pedidos
    retornaFornecedor: function (p_codigo, p_tipo) {

        if (p_tipo == "trans") {
            $('#CodigoFornecedor').val(p_codigo);
            $('#trans').val(p_codigo);
        }

        if (p_tipo == "transred") {
            $('#Codigotransred').val(p_codigo);
            $('#transred').val(p_codigo);
        }

        if (p_tipo == "transredcif") {
            $('#Codigotransredcif').val(p_codigo);
            $('#transredCIF').val(p_codigo);
        }



    }
}