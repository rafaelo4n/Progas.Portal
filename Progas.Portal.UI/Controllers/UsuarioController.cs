using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.UI.Filters;
using Progas.Portal.ViewModel;
using Progas.Portal.Application.Services.Contracts;
using Progas.Portal.Infra.Repositories.Contracts;

namespace Progas.Portal.UI.Controllers
{
    [SecurityFilter]
    public class UsuarioController : Controller
    {
        private readonly IConsultaUsuario    _consultaUsuario;
        private readonly ICadastroUsuario    _cadastroUsuario;
        private readonly IGerenciadorUsuario _gerenciadorUsuario;


        public UsuarioController(IConsultaUsuario consultaUsuario, ICadastroUsuario cadastroUsuario, IGerenciadorUsuario gerenciadorUsuario)
        {
            _consultaUsuario    = consultaUsuario;
            _cadastroUsuario    = cadastroUsuario;
            _gerenciadorUsuario = gerenciadorUsuario;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }
        [HttpGet]
        public ViewResult EditarCadastro(string login)
        {
            UsuarioConsultaVm usuarioConsultaVm = _consultaUsuario.ConsultaPorLogin(login);
            return View("Cadastro", usuarioConsultaVm);
        }

        [HttpGet]
        public JsonResult Listar(PaginacaoVm paginacaoVm, UsuarioFiltroVm usuarioFiltroVm)
        {
            KendoGridVm kendoGridVm = _consultaUsuario.Listar(paginacaoVm, usuarioFiltroVm);
            return Json(kendoGridVm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult PerfisDoUsuario(string login)
        {
            try
            {
                IList<PerfilVm> perfis = _consultaUsuario.PerfisDoUsuario(login);
                return Json(new {Sucesso = true, Registros = perfis}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new {Sucesso = false, Mensagem = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        // Cadastro De Usuario
        [HttpGet]
        public ActionResult CadastrarUsuario()
        {
            try
            {
                ViewBag.TituloDaPagina = "Cadastro de Usuários";
                return View("CadastrarUsuario");
            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CadastrarUsuario(UsuarioCadastroVm usuarioCadastroVm)
        {
            try
            {
                string consultaLogin = _consultaUsuario.ConfirmaLogin(usuarioCadastroVm.Login);
                if (consultaLogin == "nao encontrou")
                {
                    _cadastroUsuario.Novo(usuarioCadastroVm);
                    _gerenciadorUsuario.CriarSenha(usuarioCadastroVm.Login);
                    return Json(new { Sucesso = true, Mensagem = "Usuario cadastrado com sucesso." });
                }
                else 
                {
                    return Json(new { Sucesso = false, Mensagem = "O Login informado já é utilizado. Por favor informar outro." });
                }   

            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Mensagem = ex.Message });
            }
        }
    }
}
