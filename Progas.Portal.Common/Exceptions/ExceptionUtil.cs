﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Progas.Portal.ViewModel;

namespace Progas.Portal.Common.Exceptions
{
    public static class ExceptionUtil
    {
        public static ApiResponseMessage GeraExecaoDeErroParaWebApi(Exception ex)
        {
            return new ApiResponseMessage()
            {
                Retorno = new Retorno()
                {
                    Codigo = "500",
                    Texto = "Erro interno. Mensagem: " + ex.Message
                        + (ex.InnerException != null ? " - Excecao Interna: " + ex.InnerException.Message : "")
                        + " - Pilha de Execucao: " + ex.StackTrace
                }

            };
        }

        public static string ExibeDetalhes(Exception ex)
        {
            string mensagem = ex.Message;

            var excecao = ex;

            while (excecao.InnerException != null)
            {
                mensagem += Environment.NewLine + Environment.NewLine + 
                   "Exceção interna: " + excecao.InnerException.Message;
                excecao = excecao.InnerException;
            }
            return mensagem;

        }
    }
}
