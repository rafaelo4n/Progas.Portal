using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Progas.Portal.UI.Helpers
{
    public static class CustomHelpers
    {

        private static string GeraColuna<TModel, TValue>(Coluna<TModel, TValue> coluna, string divColunaClass)
        {
            string html = @coluna.GeraLabel().ToHtmlString() +
                          @coluna.GeraInput().ToHtmlString();

            if (coluna.ExibirMensagemDeValidacao)
            {
                html +=
                "<p class=\"mensagemErro\">" +
                @coluna.GeraMensagemDeValidacao() +
                "</p>";

            }

            return
                "<div" + (string.IsNullOrEmpty(divColunaClass) ? "": " class=\"" +  divColunaClass + "\"" )  + ">" +
                    html +
                "</div>";
        }

        public static IHtmlString LinhaComUmaColuna<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
                                                                    Coluna<TModel, TValue> coluna)
        {
            coluna.HtmlHelper = htmlHelper;
            string retorno =
                "<div class=\"linha\"> " +
                    GeraColuna(coluna, "") +
                "</div>";
            return new HtmlString(retorno);
        }


        public static IHtmlString LinhaComDuasColunas<TModel1, TValue1, TValue2>(this HtmlHelper<TModel1> html, 
            Coluna<TModel1, TValue1> coluna1, Coluna<TModel1, TValue2> coluna2)
        {
            coluna1.HtmlHelper = html;
            coluna2.HtmlHelper = html; 
            string retorno =
                "<div class=\"linha\"> " +
                    GeraColuna(coluna1,"coluna") +
                    GeraColuna(coluna2,"coluna") +
                "</div>";
            return new HtmlString(retorno);
        }

        public static IHtmlString LinhaComTresColunas<TModel1, TValue1, TValue2>(this HtmlHelper<TModel1> html,
            Coluna<TModel1, TValue1> coluna1, Coluna<TModel1, TValue2> coluna2, Coluna<TModel1, TValue2> coluna3)
        {
            coluna1.HtmlHelper = html;
            coluna2.HtmlHelper = html;
            coluna3.HtmlHelper = html;
            string retorno =
                "<div class=\"linha\"> " +
                    GeraColuna(coluna1, "coluna3") +
                    GeraColuna(coluna2, "coluna3") +
                    GeraColuna(coluna3, "coluna3") +
                "</div>";
            return new HtmlString(retorno);
        }

        public static IHtmlString LinhaComQuatroColunas<TModel1, TValue1, TValue2>(this HtmlHelper<TModel1> html,
            Coluna<TModel1, TValue1> coluna1, Coluna<TModel1, TValue2> coluna2, Coluna<TModel1, TValue2> coluna3,
            Coluna<TModel1, TValue2> coluna4)
        {
            coluna1.HtmlHelper = html;
            coluna2.HtmlHelper = html;
            coluna3.HtmlHelper = html;
            coluna4.HtmlHelper = html;
            string retorno =
                "<div class=\"linha\"> " +
                    GeraColuna(coluna1, "coluna4") +
                    GeraColuna(coluna2, "coluna4") +
                    GeraColuna(coluna3, "coluna4") +
                    GeraColuna(coluna4, "coluna4") +
                "</div>";
            return new HtmlString(retorno);
        }

        public static IHtmlString LinhaComCincoColunas<TModel1, TValue1, TValue2>(this HtmlHelper<TModel1> html,
            Coluna<TModel1, TValue1> coluna1, Coluna<TModel1, TValue2> coluna2, Coluna<TModel1, TValue2> coluna3,
            Coluna<TModel1, TValue2> coluna4, Coluna<TModel1, TValue2> coluna5)
        {
            coluna1.HtmlHelper = html;
            coluna2.HtmlHelper = html;
            coluna3.HtmlHelper = html;
            coluna4.HtmlHelper = html;
            coluna5.HtmlHelper = html;
            
            string retorno =
                "<div class=\"linha\"> " +
                    GeraColuna(coluna1, "coluna5") +
                    GeraColuna(coluna2, "coluna5") +
                    GeraColuna(coluna3, "coluna5") +
                    GeraColuna(coluna4, "coluna5") +
                    GeraColuna(coluna5, "coluna5") +
                "</div>";
            return new HtmlString(retorno);
        }

    }


}
