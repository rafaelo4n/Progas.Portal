function SelecionarCliente() {
    this.clienteSelecionado = null;

    var me = this;

    function configurarGridDeSelecao() {

        $("#gridClientes").customKendoGrid({
            dataSource: {
                schema: {
                    data: 'Registros',
                    model: {
                        fields: {
                            Codigo: { type: "string" },
                            Nome: { type: "string" },
                            Cnpj: { type: "string" },
                            Cpf: { type: "string" },
                            Municipio: { type: "string" },
                            Telefone: { type: "string" }
                        }
                    },
                    total: 'QuantidadeDeRegistros'
                },
                serverFiltering: true,
                serverPaging: true,
                transport: {
                    read: {
                        url: UrlPadrao.ListarClientesParaSelecao,
                        type: 'GET',
                        cache: false,
                        data: function () {
                            return $('#filtrosDeFornecedor').serializeObject();
                        }
                    }
                },
                pageSize: 10
            },
            dataBound: function (e) {
                if (me.clienteSelecionado != null) {
                    $('input[name=radioCliente][data-codigocliente=' + me.clienteSelecionado.Codigo + ']').attr('checked', true);
                }
            },
            columns:
            [
                {
                    field: 'Codigo',
                    title: ' ', /*coloco um espaço para deixar o header sem título*/
                    width: 30,
                    sortable: false,
                    template: '<input type="radio" name="radioCliente" data-codigocliente="${Codigo}"></input>'
                },
                {
                    field: "Codigo",
                    width: 80,
                    title: "Codigo"
                },
                {
                    field: "Nome",
                    width: 240,
                    title: "Nome"
                },
                {
                    field: "Cnpj",
                    width: 100,
                    title: "CNPJ"

                },
                {
                    field: "Cpf",
                    width: 90,
                    title: "CPF"
                },
                {
                    field: "municipio",
                    width: 100,
                    title: "Cidade"
                }
            ]
        });

        $('#gridClientes').find('input[name=radioCliente]').die("change");
        $('#gridClientes').find('input[name=radioCliente]').live("change", function () {
            if (!$(this).is(':checked')) {
                return;
            }
            var registroSelecionado = $('#gridClientes').data("kendoGrid").obterRegistroSelecionado();
            me.clienteSelecionado = registroSelecionado;

        });


    };

    function configurarJanelaModal(idDoCampoDoCodigoDoFornecedor, idDaDivDaJanelaDeDialogo, idDoBotaoDeSelecaoDoFornecedor, funcaoParaPreencherOsDadosDeRetorno) {
        $('body').append('<div id="' + idDaDivDaJanelaDeDialogo + '" class="janelaModal"></div>');
        $('#' + idDaDivDaJanelaDeDialogo).customDialog({
            title: 'Selecionar Fornecedor',
            buttons: {
                "Confirmar": function () {
                    if (me.clienteSelecionado == null) {
                        Mensagem.ExibirMensagemDeErro("É necessário selecionar um Cliente.");
                        return;
                    }

                    funcaoParaPreencherOsDadosDeRetorno();

                    me.clienteSelecionado = null;
                    $(this).dialog("close");
                },
                "Cancelar": function () {
                    $(this).dialog("close");
                }
            }
        });

        //evento que acontece quando clica no botão da tela principal para abrir a modal
        $(idDoBotaoDeSelecaoDoFornecedor).click(function () {

            var codigoDoFornecedor = $(idDoCampoDoCodigoDoFornecedor).val();

            if (codigoDoFornecedor) {
                me.clienteSelecionado = {
                    Codigo: codigoDoFornecedor
                };
            }

            $('#' + idDaDivDaJanelaDeDialogo).customLoad(UrlPadrao.SelecionarCliente, configurarGridDeSelecao);
        });

    };

    this.configurar = function (idDoCampoDoCodigoDoFornecedor, idDaDivDaJanelaDeDialogo, idDoBotaoDeSelecaoDoFornecedor, funcaoParaPreencherOsDadosDeRetorno) {
        configurarJanelaModal(idDoCampoDoCodigoDoFornecedor, idDaDivDaJanelaDeDialogo, idDoBotaoDeSelecaoDoFornecedor,funcaoParaPreencherOsDadosDeRetorno);
    };
}