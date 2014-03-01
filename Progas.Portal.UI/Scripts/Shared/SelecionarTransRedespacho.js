SelecionarTransRedespacho = {
    FornecedorSelecionado: null,
    Configurar: function() {
        $('body').append('<div id="divSelecionarTransRedespacho" class="janelaModal"></div>');
        $('#divSelecionarTransRedespacho').customDialog({
            title: 'Selecionar Transportadora',
            buttons: {
                "Confirmar": function() {
                    if (SelecionarFornecedor.FornecedorSelecionado == null) {
                        Mensagem.ExibirMensagemDeErro("É necessário selecionar uma Transportadora.");
                        return;
                    }
                    $('#TransportadoraDeRedespacho_Codigo').val(SelecionarFornecedor.FornecedorSelecionado.Codigo);
                    $('#TransportadoraDeRedespacho_Nome').val(unescape(SelecionarFornecedor.FornecedorSelecionado.Nome));
                    $(this).dialog("close");
                },
                "Cancelar": function() {
                    $(this).dialog("close");
                }
            }
        });
        $('#btnSelecionarTransred').click(function () {

            $('#divSelecionarTransRedespacho').load(UrlPadrao.SelecionarFornecedor
                + '/?Codigo=' + $('#TransportadoraDeRedespacho_Codigo').val() + '&Nome=' + escape($('#TransportadoraDeRedespacho_Nome').val()),
                function(response, status, xhr) {
                    $('#divSelecionarTransRedespacho').dialog('open');
                });
        });
        
    }
}