﻿using System;
using System.Web.Mvc;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.UI.Filters;
using Progas.Portal.ViewModel;

namespace Progas.Portal.UI.Controllers
{
    [SecurityFilter]
    public class FornecedorController : Controller
    {
        private readonly IConsultaFornecedor _consultaFornecedor;
        public FornecedorController(IConsultaFornecedor consultaFornecedor)
        {
            _consultaFornecedor = consultaFornecedor;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        /*
        [HttpGet]
        public JsonResult FornecedoresGerais(PaginacaoVm paginacaoVm, FornecedorDoProdutoFiltro filtro)
        {
            KendoGridVm kendoGridVm = _consultaFornecedor.FornecedoresNaoVinculadosAoProduto(paginacaoVm, filtro);
            return Json(kendoGridVm, JsonRequestBehavior.AllowGet);
        }*/

        [HttpGet]
        public JsonResult Listar(PaginacaoVm paginacaoVm, FornecedorFiltroVm filtro)
        {
            KendoGridVm kendoGridVm = _consultaFornecedor.Listar(paginacaoVm, filtro);
            return Json(kendoGridVm,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ViewResult Cadastro(string codigoFornecedor)
        {
            FornecedorCadastroVm vieModel = _consultaFornecedor.ConsultaPorCodigo(codigoFornecedor);
            return View(vieModel);
        }

        /*[HttpGet]
        public JsonResult ProdutosDoFornecedor(PaginacaoVm paginacaoVm, string codigoFornecedor)
        {
            KendoGridVm kendoGridVm = _consultaFornecedor.ProdutosDoFornecedor(paginacaoVm, codigoFornecedor);
            return Json(kendoGridVm, JsonRequestBehavior.AllowGet);
        }*/

        [HttpGet]
        public JsonResult ConsultaPeloCnpj(string cnpj)
        {
            try
            {
                var nomeDoFornecedor = _consultaFornecedor.ConsultaPorCnpj(cnpj);
                return Json(new {Sucesso = true, Nome = nomeDoFornecedor}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new {Sucesso = false, Mensagem = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
