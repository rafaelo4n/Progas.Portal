﻿function SelecionarFornecedor() {
    this.fornecedorSelecionado = null;

    var me = this;

    function configurarGridDeSelecao() {

        $("#gridFornecedores").customKendoGrid({
            dataSource: {
                schema: {
                    data: 'Registros',
                    model: {
                        fields: {
                            Codigo: { type: "string" },
                            Nome: { type: "string" },
                            Cnpj: { type: "string" },
                            nr_ie_for: { type: "string" },
                            Cpf: { type: "string" },
                            endereco: { type: "string" },
                            numero: { type: "string" },
                            complemento: { type: "string" },
                            municipio: { type: "string" },
                            uf: { type: "string" },
                            pais: { type: "string" },
                            tel_res: { type: "string" },
                            tel_cel: { type: "string" },
                            email: { type: "string" }
                        }
                    },
                    total: 'QuantidadeDeRegistros'
                },
                serverFiltering: true,
                serverPaging: true,
                transport: {
                    read: {
                        url: UrlPadrao.ListarFornecedoresParaSelecao,
                        type: 'GET',
                        cache: false,
                        data: function () {
                            return {
                                Codigo: $('#CodigoFiltro').val(),
                                Nome: $('#NomeFiltro').val()
                            };
                        }
                    }
                },
                pageSize: 10
            },
            dataBound: function (e) {
                if (me.fornecedorSelecionado != null) {
                    $('input[name=radioFornecedor][data-codigofornecedor=' + me.fornecedorSelecionado.Codigo + ']').attr('checked', true);
                }
            },
            columns:
            [
                {
                    field: 'Codigo',
                    title: ' ', /*coloco um espaço para deixar o header sem título*/
                    width: 30,
                    sortable: false,
                    template: '<input type="radio" name="radioFornecedor" data-codigofornecedor="${Codigo}"></input>'
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
                    field: "nr_ie_for",
                    width: 80,
                    title: "Insc.Estadual"
                },
                {
                    field: "Cpf",
                    width: 90,
                    title: "CPF"
                },
                {
                    field: "endereco",
                    width: 150,
                    title: "Endereço"
                },
                {
                    field: "numero",
                    width: 55,
                    title: "Numero"
                },
                {
                    field: "complemento",
                    width: 90,
                    title: "Complemento"
                },
                {
                    field: "municipio",
                    width: 100,
                    title: "Cidade"
                },
                {
                    field: "uf",
                    width: 30,
                    title: "UF"
                }, {
                    field: "pais",
                    width: 40,
                    title: "País"
                }, {
                    field: "tel_res",
                    width: 80,
                    title: "Tel.Res"
                }, {
                    field: "tel_cel",
                    width: 80,
                    title: "Tel.Cel"
                },
                {
                    field: "email",
                    width: 200,
                    title: "Email"
                }

            ]
        });

        $('#gridFornecedores').find('input[name=radioFornecedor]').die("change");
        $('#gridFornecedores').find('input[name=radioFornecedor]').live("change", function () {
            if (!$(this).is(':checked')) {
                return;
            }
            var grid = $('#gridFornecedores').data("kendoGrid");
            var dataItem = grid.dataItem(grid.select());
            me.fornecedorSelecionado = { Codigo: dataItem.Codigo, Nome: dataItem.Nome };

        });


    };

    function configurarJanelaModal(idDoCampoDoCodigoDoFornecedor, idDoCampoDoNomeDoFornecedor, idDaDivDaJanelaDeDialogo, idDoBotaoDeSelecaoDoFornecedor) {
        $('body').append('<div id="' + idDaDivDaJanelaDeDialogo + '" class="janelaModal"></div>');
        $('#' + idDaDivDaJanelaDeDialogo).customDialog({
            title: 'Selecionar Fornecedor',
            buttons: {
                "Confirmar": function () {
                    if (me.fornecedorSelecionado == null) {
                        Mensagem.ExibirMensagemDeErro("É necessário selecionar um Fornecedor.");
                        return;
                    }
                    $(idDoCampoDoCodigoDoFornecedor).val(me.fornecedorSelecionado.Codigo);
                    if (idDoCampoDoNomeDoFornecedor) {
                        $(idDoCampoDoNomeDoFornecedor).val(unescape(me.fornecedorSelecionado.Nome));
                    }

                    me.fornecedorSelecionado = null;
                    $(this).dialog("close");
                },
                "Cancelar": function () {
                    $(this).dialog("close");
                }
            }
        });
        $(idDoBotaoDeSelecaoDoFornecedor).click(function () {

            var codigoDoFornecedor = $(idDoCampoDoCodigoDoFornecedor).val();
            var nomeDoFornecedor = '';

            if (idDoCampoDoNomeDoFornecedor) {
                nomeDoFornecedor = escape($(idDoCampoDoNomeDoFornecedor).val());
            }

            if (codigoDoFornecedor) {
                me.fornecedorSelecionado = {
                    Codigo: codigoDoFornecedor,
                    Nome: nomeDoFornecedor
                };
            }

            $('#' + idDaDivDaJanelaDeDialogo).customLoad(UrlPadrao.SelecionarFornecedor
                + '/?Codigo=' + codigoDoFornecedor + '&Nome=' + escape(nomeDoFornecedor), configurarGridDeSelecao);
        });

    };

    this.configurar = function (idDoCampoDoCodigoDoFornecedor, idDoCampoDoNomeDoFornecedor, idDaDivDaJanelaDeDialogo, idDoBotaoDeSelecaoDoFornecedor) {
        configurarJanelaModal(idDoCampoDoCodigoDoFornecedor, idDoCampoDoNomeDoFornecedor, idDaDivDaJanelaDeDialogo, idDoBotaoDeSelecaoDoFornecedor);
    };
}