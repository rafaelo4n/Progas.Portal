function SelecionarProduto() {

    this.produtoSelecionado = null;
    var cliente = null;
    var areaDeVenda = null;

    var me = this;

    function configurarGridDeSelecao() {

        $("#gridProdutos").customKendoGrid({
            dataSource: {
                schema: {
                    data: 'Registros',
                    model: {
                        fields: {
                            Id: {type:"number"},
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
                            var filtro = {
                                Codigo: $('#CodigoFiltro').val(),
                                Descricao: $('#DescricaoFiltro').val(),
                                Tipo: $('#TipoFiltro').val()
                            };

                            if (cliente) {
                                filtro.IdDoCliente = cliente;
                                filtro.ComPrecoAtivo = true;
                            }

                            if (areaDeVenda) {
                                filtro.IdDaAreaDeVenda = areaDeVenda;
                            }

                            return filtro;
                        }
                    }
                }
            },
            dataBound: function () {
                if (me.produtoSelecionado != null) {
                    $('input[name=radioProduto][data-idproduto=' + me.produtoSelecionado.Id + ']').attr('checked', true);
                }
            },
            columns:
            [
                {
                    title: ' ', 
                    width: 30,
                    sortable: false,
                    template: '<input type="radio" name="radioProduto" data-idproduto="${Id}"></input>'
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

    this.configurarJanelaModal = function (idDoCampoDoIdDoProduto, idDaDivDaJanelaDeDialogo, idDoBotaoDeSelecaoDoProduto,
        idDoCampoDoCliente, idDoCampoDaAreaDeVenda, funcaoParaPreencherOsDadosDeRetorno) {

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

            if (idDoCampoDoCliente) {
                
                cliente = $(idDoCampoDoCliente).val();
            }

            if (idDoCampoDaAreaDeVenda) {
                areaDeVenda = $(idDoCampoDaAreaDeVenda).val();
                if (!areaDeVenda) {
                    Mensagem.ExibirMensagemDeErro('Antes de selecionar o Material é necessário selecionar uma Área de Venda para identificarmos o Centro.');
                    return;
                }

            }

            var idDoProduto = $(idDoCampoDoIdDoProduto).val();

            if (idDoProduto) {
                me.produtoSelecionado = {
                    Id: idDoProduto
                };
            }

            $('#' + idDaDivDaJanelaDeDialogo).customLoad(UrlPadrao.SelecionarProduto, configurarGridDeSelecao);

        });

    };

}