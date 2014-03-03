function SelecionarProduto() {

    this.produtoSelecionado = null;

    var me = this;

    function configurarGridDeSelecao() {

        $("#gridProdutos").customKendoGrid({
            dataSource: {
                schema: {
                    data: 'Registros',
                    model: {
                        fields: {
                            Descricao: { type: "string" },
                            Id_material: { type: "string" },
                            Tipo: { type: "string" },
                            CodigoDaUnidadeDeMedida: { type: "string" }
                        }
                    },
                    total: 'QuantidadeDeRegistros'
                },
                serverFiltering: true,
                serverPaging: true,
                transport: {
                    read: {
                        url: UrlPadrao.ListarProdutos,
                        type: 'GET',
                        cache: false,
                        data: function () {
                            return {
                                Codigo: $('#CodigoFiltro').val(),
                                Descricao: $('#DescricaoFiltro').val(),
                                Tipo: $('#TipoFiltro').val()
                            };
                        }
                    }
                }
            },
            dataBound: function (e) {
                if (me.produtoSelecionado != null) {
                    $('input[name=radioProduto][data-codigoproduto=' + me.produtoSelecionado.Codigo + ']').attr('checked', true);
                }
            },
            columns:
            [
                {
                    title: ' ', 
                    width: 30,
                    sortable: false,
                    template: '<input type="radio" name="radioProduto" data-codigoproduto="${Id_material}"></input>'
                },
                {
                    width: 170,
                    field: "Id_material",
                    title: "Código"
                },
                {
                    field: "Descricao",
                    width: 300,
                    title: "Descrição"
                },
                {
                    field: "Tipo",
                    width: 60
                }
            ]
        });

        $('#gridProdutos').find('input[name=radioProduto]').die("change");
        $('#gridProdutos').find('input[name=radioProduto]').live("change", function () {
            if (!$(this).is(':checked')) {
                return;
            }
            var registroSelecionado = $('#gridProdutos').data("kendoGrid").obterRegistroSelecionado();;
            me.produtoSelecionado = registroSelecionado;

        });


    };

    function configurarJanelaModal(idDoCampoDoCodigoDoProduto, idDaDivDaJanelaDeDialogo, idDoBotaoDeSelecaoDoProduto, funcaoParaPreencherOsDadosDeRetorno) {

        $('body').append('<div id="' + idDaDivDaJanelaDeDialogo + '" class="janelaModal"></div>');
        $('#' + idDaDivDaJanelaDeDialogo).customDialog({
            title: 'Selecionar Produto',
            buttons: {
                "Confirmar": function() {
                    if (me.produtoSelecionado == null) {
                        Mensagem.ExibirMensagemDeErro("É necessário selecionar um Produto.");
                        return;
                    }

                    funcaoParaPreencherOsDadosDeRetorno();

                    me.produtoSelecionado = null;
                    $(this).dialog("close");
                },
                "Cancelar": function() {
                    $(this).dialog("close");
                }
            }
        });
        $(idDoBotaoDeSelecaoDoProduto).click(function () {

            var codigoDoProduto = $(idDoCampoDoCodigoDoProduto).val();

            if (codigoDoProduto) {
                me.produtoSelecionado = {
                    Codigo: codigoDoProduto
                };
            }

            $('#' + idDaDivDaJanelaDeDialogo).customLoad(UrlPadrao.SelecionarProduto, configurarGridDeSelecao);

        });

    };

    this.configurar = function (idDoCampoDoCodigoDoProduto, idDaDivDaJanelaDeDialogo, idDoBotaoDeSelecaoDoProduto, funcaoParaPreencherOsDadosDeRetorno) {
        configurarJanelaModal(idDoCampoDoCodigoDoProduto, idDaDivDaJanelaDeDialogo, idDoBotaoDeSelecaoDoProduto, funcaoParaPreencherOsDadosDeRetorno);
    };
}