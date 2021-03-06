﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Progas.Portal.UI.Helpers
{
    public abstract class Coluna<TModel, TValue>
    {
        private readonly string _textoDaLabel;
        protected readonly Expression<Func<TModel, TValue>> Expressao;
        public HtmlHelper<TModel> HtmlHelper { get; set; }
        //protected readonly string InputClass;
        protected readonly object InputAttributes;
        //public string LabelClass { get; set; }
        protected object LabelAttributes;
        public bool ExibirMensagemDeValidacao { get; protected set; }

        protected Coluna(Expression<Func<TModel, TValue>> expressao, string inputClass, string labelClass, bool exibirMensagemDeValidacao)
        {
            Expressao = expressao;
            ExibirMensagemDeValidacao = exibirMensagemDeValidacao;
            InputAttributes = !string.IsNullOrEmpty(inputClass) ? new { @class = inputClass } : null;
            LabelAttributes = !string.IsNullOrEmpty(labelClass) ? new { @class = labelClass } : null;
        }

        protected Coluna(Expression<Func<TModel, TValue>> expressao, object inputAtributes, object labelAtributes, bool exibirMensagemDeValidacao)
        {
            Expressao = expressao;
            InputAttributes = inputAtributes;
            LabelAttributes = labelAtributes;
            ExibirMensagemDeValidacao = exibirMensagemDeValidacao;
        }

        protected Coluna(Expression<Func<TModel, TValue>> expressao, string textoDaLabel, string inputClass, string labelClass, bool exibirMensagemDeValidacao)
            : this(expressao, inputClass, labelClass, exibirMensagemDeValidacao)
        {
            _textoDaLabel = textoDaLabel;
        }

        protected Coluna(Expression<Func<TModel, TValue>> expressao, string textoDaLabel, object inputAtributes, object labelAtributes, bool exibirMensagemDeValidacao)
            : this(expressao, inputAtributes, labelAtributes, exibirMensagemDeValidacao)
        {
            _textoDaLabel = textoDaLabel;
        }

        public virtual MvcHtmlString GeraLabel()
        {
            return string.IsNullOrEmpty(_textoDaLabel) ?
                System.Web.Mvc.Html.LabelExtensions.LabelFor(HtmlHelper, Expressao, LabelAttributes) :
                System.Web.Mvc.Html.LabelExtensions.Label(HtmlHelper, _textoDaLabel, LabelAttributes);
        }

        public MvcHtmlString GeraMensagemDeValidacao()
        {
            return System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(HtmlHelper, Expressao);
        }

        public abstract MvcHtmlString GeraInput();

        //public void UpdateLabelInput(string labelClass)
        //{
        //    LabelAttributes = new {@class = labelClass};
        //}

        protected string FormatarValor(Expression<Func<TModel, TValue>> expressao, string formatacao)
        {
            var valorFormatado = System.Web.Mvc.Html.DisplayExtensions.DisplayFor(HtmlHelper, Expressao, InputAttributes).ToString(); ;
            if (string.IsNullOrEmpty(formatacao)) return valorFormatado;

            decimal valorConvertido;

            if (decimal.TryParse(valorFormatado, out valorConvertido))
            {
                valorFormatado = valorConvertido.ToString(formatacao);
            }

            return valorFormatado;

        }

    }

    public class ColunaComEditor<TModel, TValue> : Coluna<TModel, TValue>
    {
        public ColunaComEditor(Expression<Func<TModel, TValue>> expressao, string inputClass = "")
            : base(expressao, inputClass, "", true)
        {
        }

        public override MvcHtmlString GeraInput()
        {
            return System.Web.Mvc.Html.EditorExtensions.EditorFor(HtmlHelper, Expressao, InputAttributes);
        }
    }

    public class ColunaComTextBox<TModel, TValue> : Coluna<TModel, TValue>
    {
        public ColunaComTextBox(Expression<Func<TModel, TValue>> expressao, string inputClass)
            : base(expressao, inputClass, "", true)
        {
        }

        public ColunaComTextBox(Expression<Func<TModel, TValue>> expressao, object atributos)
            : base(expressao, atributos, null, true)
        {
        }

        public ColunaComTextBox(Expression<Func<TModel, TValue>> expressao, object inputAttributes, string  textoDaLabel)
            : base(expressao,textoDaLabel, inputAttributes, null, true)
        {
        }


        public override MvcHtmlString GeraInput()
        {
            return System.Web.Mvc.Html.InputExtensions.TextBoxFor(HtmlHelper, Expressao, InputAttributes);
        }
    }

    public class ColunaComBotaoDeBusca<TModel, TValue> : Coluna<TModel, TValue>
    {
        private readonly string _idDoBotaoDeBusca;
        private readonly string _classeDoBotao;
        private readonly string _idDoBotaoParaLimparCampos;

        public ColunaComBotaoDeBusca(Expression<Func<TModel, TValue>> expressao, object inputAttributes, string idDoBotaoDeBusca, string classeDoBotao)
            : this(expressao, inputAttributes, idDoBotaoDeBusca,classeDoBotao,""){}

        public ColunaComBotaoDeBusca(Expression<Func<TModel, TValue>> expressao, object inputAttributes, string idDoBotaoDeBusca, string classeDoBotao, string idDoBotaoParaLimparCampos)
            : this(expressao,inputAttributes,"",idDoBotaoDeBusca,classeDoBotao,idDoBotaoParaLimparCampos){}


        public ColunaComBotaoDeBusca(Expression<Func<TModel, TValue>> expressao, object inputAttributes, string textoDaLabel, string idDoBotaoDeBusca, 
            string classeDoBotao, string idDoBotaoParaLimparCampos)
            : base(expressao,textoDaLabel, inputAttributes,null, true)
        {
            _idDoBotaoDeBusca = idDoBotaoDeBusca;
            _classeDoBotao = classeDoBotao;
            _idDoBotaoParaLimparCampos = idDoBotaoParaLimparCampos;
        }


        public override MvcHtmlString GeraInput()
        {
            string htmlDoBotaoDeBusca = string.Format("<input type=\"button\" id=\"{0}\" class=\"{1}\"/>", _idDoBotaoDeBusca, _classeDoBotao);
            string htmlDoBotaoParaLimparCampos = !string.IsNullOrEmpty(_idDoBotaoParaLimparCampos) ?
                string.Format("<input type=\"button\" id=\"{0}\" class=\"button_rubber\" style=\"margin-left:0.5em;\" title=\"Limpar item selecionado\"/>",_idDoBotaoParaLimparCampos)
                : "";

            return new MvcHtmlString(System.Web.Mvc.Html.InputExtensions.TextBoxFor(HtmlHelper, Expressao, InputAttributes) + htmlDoBotaoDeBusca + htmlDoBotaoParaLimparCampos);
        }

        
    }

    public class ColunaComTextArea<TModel, TValue> : Coluna<TModel, TValue>
    {
        public ColunaComTextArea(Expression<Func<TModel, TValue>> expressao)
            : base(expressao, "", "", true)
        {
        }

        public override MvcHtmlString GeraInput()
        {
            return System.Web.Mvc.Html.TextAreaExtensions.TextAreaFor(HtmlHelper, Expressao, new { @rows = "5" });
        }
    }

    public class ColunaComDropDown<TModel, TValue> : Coluna<TModel, TValue>
    {
        public ColunaComDropDown(Expression<Func<TModel, TValue>> expressao, IEnumerable<SelectListItem> items,
            string nome, string inputClass = "", string optionLabel = "Selecione >>")
            : base(expressao, inputClass, "", true)
        {
            _items = items;
            _nome = nome;
            _optionLabel = optionLabel;
        }

        private readonly IEnumerable<SelectListItem> _items;
        private readonly string _nome;
        private readonly string _optionLabel;

        public override MvcHtmlString GeraInput()
        {
            return System.Web.Mvc.Html.SelectExtensions.DropDownList(HtmlHelper, _nome, _items, _optionLabel);
        }
    }

    public class ColunaComLabel<TModel, TValue> : Coluna<TModel, TValue>
    {
        private readonly string _formatacao;

        public ColunaComLabel(Expression<Func<TModel, TValue>> expressao, string formatacao = null)
            : base(expressao, "", "labelNaLinha", false)
        {
            _formatacao = formatacao;
        }

        public ColunaComLabel(string textoDaLabel, Expression<Func<TModel, TValue>> expressao, string formatacao = null)
            : base(expressao, textoDaLabel, "", "labelNaLinha", false)
        {
            _formatacao = formatacao;
        }

        public ColunaComLabel(Expression<Func<TModel, TValue>> expressao, object labelAtributes)
            : base(expressao,null,labelAtributes,false)
        {
        }




        public override MvcHtmlString GeraInput()
        {
            var valorFormatado = FormatarValor(Expressao, _formatacao);
            return new MvcHtmlString(valorFormatado);
        }
    }



    public class ColunaComLabelEmDestaque<TModel, TValue> : Coluna<TModel, TValue>
    {
        private readonly string _idDoDestaque;
        private readonly string _formatacaoDoDestaque;

        public ColunaComLabelEmDestaque(Expression<Func<TModel, TValue>> expressao, string idDoDestaque, string formatacaoDoDestaque)
            : base(expressao, "", "labelNaLinha", false)
        {
            _idDoDestaque = idDoDestaque;
            _formatacaoDoDestaque = formatacaoDoDestaque;
        }

        public override MvcHtmlString GeraInput()
        {
            var valorDoLabel = FormatarValor(Expressao, _formatacaoDoDestaque);
            var elemento = "<span class=\"labelDestaque\" id=\"" + _idDoDestaque + "\">" + valorDoLabel + "</span>";

            return new MvcHtmlString(elemento);
        }
    }

    public class ColunaComCheckBox<TModel, TValue> : Coluna<TModel, TValue>
    {
        public ColunaComCheckBox(Expression<Func<TModel, TValue>> expressao)
            : base(expressao, "", "labelNaLinha", false)
        {
        }

        public override MvcHtmlString GeraInput()
        {
            return System.Web.Mvc.Html.InputExtensions.CheckBoxFor(HtmlHelper, (Expression<Func<TModel, bool>>)(object)Expressao);
        }
    }

    public class ColunaVazia<TModel, TValue> : Coluna<TModel, TValue>
    {
        public ColunaVazia(Expression<Func<TModel, TValue>> expressao)
            : base(expressao, null, null, false)
        {
        }

        public override MvcHtmlString GeraInput()
        {
            return new MvcHtmlString("");
        }

        public override MvcHtmlString GeraLabel()
        {
            return new MvcHtmlString("");
        }
    }




}