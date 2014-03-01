using System.Linq;
using System.Collections.Generic;
using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaCliente : IConsultaCliente
    {
        private readonly IClientes _clientes;
        private readonly IBuilder<Cliente, ClienteCadastroVm> _builderCliente;

        // Recebe dados Interface do repositorio do Tipo pedido e monta a lista com a Entidade + ViewModel
        public ConsultaCliente(IClientes clientes, IBuilder<Cliente, ClienteCadastroVm> builder)
        {
            _clientes = clientes;
            _builderCliente = builder;
        }

        // pesquisa da tela
        public KendoGridVm Listar(PaginacaoVm paginacaoVm, ClienteFiltroVm filtro)
        {
            _clientes
                .CodigoContendo(filtro.Codigo)
                .NomeContendo(filtro.Nome)
                .ComCnpj(filtro.Cnpj)
                .ComCpf(filtro.Cpf)
                .MunicipioContendo(filtro.Municipio);

            var kendoGridVmn = new KendoGridVm()
            {
                QuantidadeDeRegistros = _clientes.Count(),
                Registros =
                    _builderCliente.BuildList(_clientes.Skip(paginacaoVm.Skip)
                    .Take(paginacaoVm.Take).List())
                            .Cast<ListagemVm>()
                            .ToList()

            };
            return kendoGridVmn;
        }

        public KendoGridVm ListarParaSelecao(PaginacaoVm paginacaoVm, ClienteFiltroVm filtro)
        {
            _clientes
                .CodigoContendo(filtro.Codigo)
                .NomeContendo(filtro.Nome)
                .ComCnpj(filtro.Cnpj)
                .ComCpf(filtro.Cpf)
                .MunicipioContendo(filtro.Municipio);

            var kendoGridVmn = new KendoGridVm()
            {
                QuantidadeDeRegistros = _clientes.Count(),
                Registros = _clientes.GetQuery()
                    .Select(c => new ClienteParaSelecaoVm
                    {
                        Codigo = c.Id_cliente,
                        Nome = c.Nome,
                        Cnpj = c.Cnpj,
                        Cpf =  c.Cpf,
                        Municipio = c.Municipio,
                        Telefone = c.Tel_res
                    })
                    .Skip(paginacaoVm.Skip)
                    .Take(paginacaoVm.Take)
                    .Cast<ListagemVm>()
                    .ToList()

            };
            return kendoGridVmn;
        }

        public IList<ClienteCadastroVm> Listar(PaginacaoVm paginacaoVm, ClienteCadastroVm filtro)
        {
            if (!string.IsNullOrEmpty(filtro.id_cliente))
            {
                _clientes.BuscaPeloCodigo(filtro.id_cliente);

            }

            if (!string.IsNullOrEmpty(filtro.nome))
            {
                _clientes.FiltraPelaDescricao(filtro.nome);
            }
            int skip = (paginacaoVm.Page - 1) * paginacaoVm.PageSize;

            //paginacaoVm.TotalRecords = _condicoesDePagamento.Count();

            return _builderCliente.BuildList(_clientes.Skip(skip).Take(paginacaoVm.Take).List());

        }


        public IList<ClienteCadastroVm> Listar()
        {
            return _builderCliente.BuildList(_clientes.List());

        }

    }
}
