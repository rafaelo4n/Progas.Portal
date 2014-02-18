SelecionarRepresentante = {
    FornecedorSelecionado: null,
    // inicio configurar
    Configurar: function () {
        $('body').append('<div id="divSelecionarRepresentante" class="janelaModal"></div>');
        $('#divSelecionarRepresentante').customDialog({
            title: 'Selecionar Representante',
            buttons: {
                "Confirmar": function () {
                    if (SelecionarFornecedor.FornecedorSelecionado == null) {
                        Mensagem.ExibirMensagemDeErro("É necessário selecionar um Representante.");
                        return;
                    }
                    $('#Codigo').val(SelecionarFornecedor.FornecedorSelecionado.Codigo);
                    $('#CodigoFornecedor').val(unescape(SelecionarFornecedor.FornecedorSelecionado.Nome));
                    $(this).dialog("close");
                },
                "Cancelar": function () {
                    $(this).dialog("close");
                }
            }
        });
        $('#btnSelecionarRepresentante').click(function () {

            $('#divSelecionarRepresentante').load(UrlPadrao.SelecionarFornecedor
                + '/?Codigo=' + $('#Codigo').val() + '&Nome=' + escape($('#CodigoFornecedor').val()),
                function (response, status, xhr) {
                    $('#divSelecionarRepresentante').dialog('open');
                });
        });

    }, // fim configurar

    // Funcao retornar o Transportadora no Botao COPIAR da tela de Pedidos
    retornaFornecedor: function (p_codigoFornecedor) {

        $('#Codigo').val(p_codigoFornecedor);
    }
}