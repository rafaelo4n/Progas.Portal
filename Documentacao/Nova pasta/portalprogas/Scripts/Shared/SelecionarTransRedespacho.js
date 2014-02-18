SelecionarTransRedespacho = {
    FornecedorSelecionado: null,
    Configurar: function() {
        $('body').append('<div id="divSelecionarTransRedespacho" class="janelaModal"></div>');
        $('#divSelecionarTransRedespacho').customDialog({
            title: 'Selecionar Fornecedor',
            buttons: {
                "Confirmar": function() {
                    if (SelecionarFornecedor.FornecedorSelecionado == null) {
                        Mensagem.ExibirMensagemDeErro("É necessário selecionar uma Transportadora.");
                        return;
                    }
                    $('#Codigotransred').val(SelecionarFornecedor.FornecedorSelecionado.Codigo);
                    $('#transred').val(unescape(SelecionarFornecedor.FornecedorSelecionado.Nome));
                    $(this).dialog("close");
                },
                "Cancelar": function() {
                    $(this).dialog("close");
                }
            }
        });
        $('#btnSelecionarTransred').click(function () {

            $('#divSelecionarTransRedespacho').load(UrlPadrao.SelecionarFornecedor
                + '/?Codigo=' + $('#Codigotransred').val() + '&Nome=' + escape($('#transred').val()),
                function(response, status, xhr) {
                    $('#divSelecionarTransRedespacho').dialog('open');
                });
        });
        
    }
}