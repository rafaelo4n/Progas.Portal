SelecionarTransRedespachoCif = {
    FornecedorSelecionado: null,
    Configurar: function() {
        $('body').append('<div id="divSelecionarTransRedespachoCif" class="janelaModal"></div>');
        $('#divSelecionarTransRedespachoCif').customDialog({
            title: 'Selecionar Transportadora',
            buttons: {
                "Confirmar": function() {
                    if (SelecionarFornecedor.FornecedorSelecionado == null) {
                        Mensagem.ExibirMensagemDeErro("É necessário selecionar uma Transportadora.");
                        return;
                    }
                    $('#TransportadoraDeRedespachoCif_Codigo').val(SelecionarFornecedor.FornecedorSelecionado.Codigo);
                    $('#TransportadoraDeRedespachoCif_Nome').val(unescape(SelecionarFornecedor.FornecedorSelecionado.Nome));
                    $(this).dialog("close");
                },
                "Cancelar": function() {
                    $(this).dialog("close");
                }
            }
        });
        $('#btnSelecionarTransredCif').click(function () {

            $('#divSelecionarTransRedespachoCif').load(UrlPadrao.SelecionarFornecedor
                + '/?Codigo=' + $('#TransportadoraDeRedespachoCif_Codigo').val() + '&Nome=' + escape($('#TransportadoraDeRedespachoCif_Nome').val()),
                function(response, status, xhr) {
                    $('#divSelecionarTransRedespachoCif').dialog('open');
                });
        });
        
    }
}