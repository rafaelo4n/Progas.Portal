using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Common;
using Progas.Portal.UI.Filters;
using Progas.Portal.ViewModel;
using Progas.Portal.Application.Services.Contracts;

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
            usuarioConsultaVm.UrlParaSalvar = Url.Action("EditarUsuario");
            ViewBag.TituloDaPagina = "Usuário - Editar";
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
                ViewBag.TituloDaPagina = "Usuário - Novo Cadastro";
                var modelo = new UsuarioConsultaVm
                {
                    UrlParaSalvar = Url.Action("NovoUsuario")
                };

                return View("Cadastro",modelo);
            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult NovoUsuario(UsuarioCadastroVm usuarioCadastroVm, IList<Enumeradores.Perfil> perfis )
        {
            try
            {
                string consultaLogin = _consultaUsuario.ConfirmaLogin(usuarioCadastroVm.Login);
                if (string.IsNullOrEmpty(consultaLogin))
                {
                    _cadastroUsuario.AtualizarUsuario(usuarioCadastroVm,perfis);
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

        [HttpPost]
        public JsonResult EditarUsuario(UsuarioCadastroVm usuarioCadastroVm, IList<Enumeradores.Perfil> perfis)
        {
            try
            {
                _cadastroUsuario.AtualizarUsuario(usuarioCadastroVm, perfis);
                return Json(new { Sucesso = true, Mensagem = "Usuario atualizado com sucesso." });

            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Mensagem = ex.Message });
            }
        }


    }
}
