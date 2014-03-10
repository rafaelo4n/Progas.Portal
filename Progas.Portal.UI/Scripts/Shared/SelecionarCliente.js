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
                            Id: {type: "number"},
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
            dataBound: function () {
                if (me.clienteSelecionado) {
                    $('input[name=radioCliente][data-idcliente=' + me.clienteSelecionado.Id + ']').attr('checked', true);
                }
            },
            columns:
            [
                {
                    title: ' ', /*coloco um espaço para deixar o header sem título*/
                    width: 30,
                    sortable: false,
                    template: '<input type="radio" name="radioCliente" data-idcliente="${Id}"></input>'
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

    function configurarJanelaModal(idDoCampoDoIdDoCliente, idDaDivDaJanelaDeDialogo, idDoBotaoDeSelecaoDoCliente, funcaoParaPreencherOsDadosDeRetorno) {
        $('body').append('<div id="' + idDaDivDaJanelaDeDialogo + '" class="janelaModal"></div>');
        $('#' + idDaDivDaJanelaDeDialogo).customDialog({
            title: 'Selecionar Cliente',
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
        $(idDoBotaoDeSelecaoDoCliente).click(function () {

            var idDoCliente = $(idDoCampoDoIdDoCliente).val();

            if (idDoCliente) {
                me.clienteSelecionado = {
                    Id: idDoCliente
                };
            }

            $('#' + idDaDivDaJanelaDeDialogo).customLoad(UrlPadrao.SelecionarCliente, configurarGridDeSelecao);
        });

    };

    this.configurar = function (idDoCampoDoIdDoCliente, idDaDivDaJanelaDeDialogo, idDoBotaoDeSelecaoDoCliente, funcaoParaPreencherOsDadosDeRetorno) {
        configurarJanelaModal(idDoCampoDoIdDoCliente, idDaDivDaJanelaDeDialogo, idDoBotaoDeSelecaoDoCliente,funcaoParaPreencherOsDadosDeRetorno);
    };
}