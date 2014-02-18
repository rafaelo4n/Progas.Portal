﻿using System;
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


        private static string GeraColunaComUmaLinha<TModel, TValue>(Coluna<TModel, TValue> coluna)
        {
            coluna.LabelClass = "labelNaLinha";
            return
                "<div>" +
                    @coluna.GeraLabel() +
                    @coluna.GeraInput() +
                "</div>";
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
                    GeraColuna(coluna1, "coluna") +
                    GeraColuna(coluna2, "coluna") +
                    GeraColuna(coluna3, "coluna") +
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
                    GeraColuna(coluna1, "coluna") +
                    GeraColuna(coluna2, "coluna") +
                    GeraColuna(coluna3, "coluna") +
                    GeraColuna(coluna4, "coluna") +
                "</div>";
            return new HtmlString(retorno);
        }


        public static IHtmlString LinhaComUmaColuna<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, 
                                                                    Coluna<TModel, TValue> coluna)
        {
            coluna.HtmlHelper = htmlHelper;
            string retorno =
                "<div class=\"linha\"> " +
                    GeraColuna(coluna,"") +
                "</div>";
            return new HtmlString(retorno);
        }

    }


}