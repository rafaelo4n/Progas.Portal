SelecionarFornecedor = {
    FornecedorSelecionado: null,
    Configurar: function() {
        $('body').append('<div id="divSelecionarFornecedor" class="janelaModal"></div>');
        $('#divSelecionarFornecedor').customDialog({
            title: 'Selecionar Fornecedor',
            buttons: {
                "Confirmar": function() {
                    if (SelecionarFornecedor.FornecedorSelecionado == null) {
                        Mensagem.ExibirMensagemDeErro("É necessário selecionar uma Transportadora.");
                        return;
                    }
                    $('#CodigoFornecedor').val(SelecionarFornecedor.FornecedorSelecionado.Codigo);
                    $('#trans').val(unescape(SelecionarFornecedor.FornecedorSelecionado.Nome));
                    $(this).dialog("close");
                },
                "Cancelar": function() {
                    $(this).dialog("close");
                }
            }
        });
        $('#btnSelecionarFornecedor').click(function() {

            $('#divSelecionarFornecedor').load(UrlPadrao.SelecionarFornecedor
                + '/?Codigo=' + $('#CodigoFornecedor').val() + '&Nome=' + escape($('#trans').val()),
                function(response, status, xhr) {
                    $('#divSelecionarFornecedor').dialog('open');
                });
        });
        
    }
}