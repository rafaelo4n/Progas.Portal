SelecionarTransRedespachoCif = {
    FornecedorSelecionado: null,
    Configurar: function() {
        $('body').append('<div id="divSelecionarTransRedespachoCif" class="janelaModal"></div>');
        $('#divSelecionarTransRedespachoCif').customDialog({
            title: 'Selecionar Fornecedor',
            buttons: {
                "Confirmar": function() {
                    if (SelecionarFornecedor.FornecedorSelecionado == null) {
                        Mensagem.ExibirMensagemDeErro("É necessário selecionar uma Transportadora.");
                        return;
                    }
                    $('#Codigotransredcif').val(SelecionarFornecedor.FornecedorSelecionado.Codigo);
                    $('#transredcif').val(unescape(SelecionarFornecedor.FornecedorSelecionado.Nome));
                    $(this).dialog("close");
                },
                "Cancelar": function() {
                    $(this).dialog("close");
                }
            }
        });
        $('#btnSelecionarTransredCif').click(function () {

            $('#divSelecionarTransRedespachoCif').load(UrlPadrao.SelecionarFornecedor
                + '/?Codigo=' + $('#Codigotransredcif').val() + '&Nome=' + escape($('#transredcif').val()),
                function(response, status, xhr) {
                    $('#divSelecionarTransRedespachoCif').dialog('open');
                });
        });
        
    }
}