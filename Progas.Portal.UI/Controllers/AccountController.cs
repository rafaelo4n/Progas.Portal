﻿using System;
using System.Web.Mvc;
using Progas.Portal.Common.Exceptions;
using Progas.Portal.Infra.Services.Contracts;
using Progas.Portal.UI.Filters;
using Progas.Portal.ViewModel;

namespace Progas.Portal.UI.Controllers
{
    //Não pode ter SecurityFilter no controller. Colocar apenas nas Actions que for necessário
    //[SecurityFilter]
    public class AccountController:Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            if (LoginInfo.UsuarioEstaLogado)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToLocal(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View("Login");
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVm model, string returnUrl)
        {
            try
            {
                _accountService.Login(model.Usuario, model.Senha);
                if (! string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToLocal(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ExceptionUtil.ExibeDetalhes(ex));
                return View(model);
            }


        }
        //
        // POST: /Account/LogOff

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //WebSecurity.Logout();
            _accountService.Logout();

            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public ActionResult EsqueciMinhaSenha(string login)
        {
            ViewBag.ReturnUrl = "";
            return View("EsqueciMinhaSenha",login);
        }
        
        public ActionResult AlterarSenha()
        {
            return View();
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        #endregion

    }
}
