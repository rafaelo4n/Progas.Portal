using System.Linq;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Progas.Portal.Application.Queries.Builders;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Application.Queries.Implementations
{
    public class ConsultaMaterial : IConsultaMaterial
    {
        private readonly IMateriais _materiais;
        private readonly IClientes _clientes;
        private readonly IClienteVendas _areasDeVenda;
        private readonly IBuilder<Material, MaterialCadastroVm> _builderMaterial;
        private readonly IUnitOfWorkNh _unitOfWorkNh;
        
        // Recebe dados Interface do repositorio do Tipo pedido e monta a lista com a Entidade + ViewModel
        public ConsultaMaterial(IMateriais materiais, IBuilder<Material, MaterialCadastroVm> builder, IUnitOfWorkNh unitOfWorkNh, IClientes clientes, IClienteVendas areasDeVenda)
        {
            _materiais = materiais;
            _builderMaterial = builder;
            _unitOfWorkNh = unitOfWorkNh;
            _clientes = clientes;
            _areasDeVenda = areasDeVenda;
        }

        // pesquisa da tela
        public KendoGridVm Listar(PaginacaoVm paginacaoVm, MaterialFiltroVm filtro)
        {
            Material material = null;

            IQueryOver<Material,Material> queryOver = _unitOfWorkNh.Session.QueryOver(() => material);

            queryOver.Where(
                Restrictions.Disjunction()
                    .Add(() => material.Eliminacao == null)
                    .Add(() => material.Eliminacao != "X"));


            if (!string.IsNullOrEmpty(filtro.Tipo))
            {
                queryOver = queryOver.Where(m => m.Tip_mat == filtro.Tipo);
            }

            if (!string.IsNullOrEmpty(filtro.Codigo))
            {
                queryOver = queryOver.Where(m => m.Id_material.IsInsensitiveLike(filtro.Codigo, MatchMode.Anywhere));
            }

            if (!string.IsNullOrEmpty(filtro.Descricao))
            {
                queryOver = queryOver.Where(m => m.Descricao.IsInsensitiveLike(filtro.Descricao, MatchMode.Anywhere));
            }

            CondicaoDePrecoGeral condicaoDePrecoGeral = null;

            if (filtro.ComPrecoAtivo)
            {
                ClienteVenda areaDeVenda = _areasDeVenda.ObterPorId(filtro.IdDaAreaDeVenda);

                queryOver = queryOver.Where(m => m.Id_centro == areaDeVenda.Org_vendas);

                QueryOver<CondicaoDePrecoGeral, CondicaoDePrecoGeral> subQueryCondicaoGeral = 
                    QueryOver.Of(() => condicaoDePrecoGeral)
                    .Where(c => c.Org_vendas == areaDeVenda.Org_vendas)
                    .And(c => c.Can_dist == areaDeVenda.Can_dist)
                    .Select(Projections.Property(() => condicaoDePrecoGeral.Id_material));

                var disjuncaoDasSubqueries = Restrictions.Disjunction()
                    .Add(Subqueries.WhereProperty<Material>(m => m.Id_material).In(subQueryCondicaoGeral));

                if (!string.IsNullOrEmpty(filtro.IdDoCliente))
                {
                    CondicaoDePrecoCliente condicaoDePrecoCliente = null;

                    QueryOver<CondicaoDePrecoCliente, CondicaoDePrecoCliente> subQueryCondicaoCliente = 
                        QueryOver.Of(() => condicaoDePrecoCliente)
                        .Where(() => condicaoDePrecoCliente.Id_cliente == filtro.IdDoCliente)
                        .And(c => c.Org_vendas == areaDeVenda.Org_vendas)
                        .And(c => c.Can_dist == areaDeVenda.Can_dist)
                        .Select(Projections.Property(() => condicaoDePrecoCliente.Id_material));

                    disjuncaoDasSubqueries.Add(Subqueries.WhereProperty<Material>(m => m.Id_material).In(subQueryCondicaoCliente) );

                    Cliente cliente = _clientes.BuscaPeloCodigo(filtro.IdDoCliente).Single();

                    CondicaoDePrecoRegiao condicaoDePrecoRegiao = null;

                    QueryOver<CondicaoDePrecoRegiao, CondicaoDePrecoRegiao> subQueryCondicaoRegiao = QueryOver.Of(() => condicaoDePrecoRegiao)
                        .Where(() => condicaoDePrecoRegiao.Regiao == cliente.Uf)
                        .Select(Projections.Property(() => condicaoDePrecoRegiao.Id_material));

                    disjuncaoDasSubqueries.Add(Subqueries.WhereProperty<Material>(m => m.Id_material).In(subQueryCondicaoRegiao));

                }

                queryOver = queryOver.Where(disjuncaoDasSubqueries);
            }

            MaterialCadastroVm materialCadastroVm = null;

            queryOver.SelectList(lista => lista
                .Select(x => material.pro_id_material).WithAlias(() => materialCadastroVm.Id)
                .Select(x => material.Id_material).WithAlias(() => materialCadastroVm.Id_material)
                .Select(x => material.Descricao).WithAlias(() => materialCadastroVm.Descricao)
                .Select(x => material.Id_centro).WithAlias(() => materialCadastroVm.Centro)
                .Select(x => material.Tip_mat).WithAlias(() => materialCadastroVm.Tipo)
                .Select(x => material.Uni_med).WithAlias(() => materialCadastroVm.UnidadeMedida)
                );

            var kendoGridVm = new KendoGridVm()
            {
                QuantidadeDeRegistros = queryOver.RowCount(),
                Registros = queryOver
                    .TransformUsing(Transformers.AliasToBean<MaterialCadastroVm>())
                    .Skip(paginacaoVm.Skip)
                    .Take(paginacaoVm.Take)
                    .List<MaterialCadastroVm>()
                    .Cast<ListagemVm>()
                    .ToList()


            };

            return kendoGridVm;

        }
        
        public IList<MaterialCadastroVm> ListarTodas()
        {
            return _builderMaterial.BuildList(_materiais.List());
        }
       
        public IList<MaterialCadastroVm> ListarCentro()
        {
            var centros = (     from centro in _materiais.GetQuery()
                               group centro by new {centro.Id_centro}                               
                                into agrupador
                          select new
                           { 
                               agrupador.Key                                                  
                           }).ToList();

            return centros.Select(x => new MaterialCadastroVm { Centro = x.Key.Id_centro }).ToList();
 
        }

    }
}
