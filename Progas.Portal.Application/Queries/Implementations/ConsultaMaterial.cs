using System.Linq;
using System.Collections.Generic;
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
        private readonly IBuilder<Material, MaterialCadastroVm> _builderMaterial;
        
        // Recebe dados Interface do repositorio do Tipo pedido e monta a lista com a Entidade + ViewModel
        public ConsultaMaterial(IMateriais materiais, IBuilder<Material, MaterialCadastroVm> builder)
        {
            _materiais = materiais;
            _builderMaterial = builder;
        }

        // pesquisa da tela
        public KendoGridVm Listar(PaginacaoVm paginacaoVm, MaterialFiltroVm filtro)
        {
            _materiais
                .CodigoContendo(filtro.Codigo)
                .NomeContendo(filtro.Descricao)
                .DoTipo(filtro.Tipo)
                .DoCentro(filtro.Centro);
            var kendoGridVmn = new KendoGridVm()
            {
                QuantidadeDeRegistros = _materiais.Count(),
                Registros =
                    _builderMaterial.BuildList(_materiais.Skip(paginacaoVm.Skip)
                    .Take(paginacaoVm.Take).List())
                            .Cast<ListagemVm>()
                            .ToList()

            };
            return kendoGridVmn;
        }
        //



        public IList<MaterialCadastroVm> Listar(PaginacaoVm paginacaoVm, MaterialCadastroVm filtro)
        {
            if (!string.IsNullOrEmpty(filtro.Id_material))
            {
                _materiais.BuscaPeloCodigo(filtro.Id_material);

            }

            if (!string.IsNullOrEmpty(filtro.Descricao))
            {
                _materiais.FiltraPelaDescricao(filtro.Descricao);
            }
            int skip = (paginacaoVm.Page - 1) * paginacaoVm.PageSize;

            //paginacaoVm.TotalRecords = _condicoesDePagamento.Count();

            return _builderMaterial.BuildList(_materiais.Skip(skip).Take(paginacaoVm.Take).List());

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
