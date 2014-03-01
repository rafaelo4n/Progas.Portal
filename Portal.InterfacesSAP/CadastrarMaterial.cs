using System;
using System.Text;
using System.Collections.Generic;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.DadosSap.Entity;
using Portal.DadosSap.Business.Implementation;

namespace Portal.DadosSap
{
    /// <summary>
    /// Summary description for SelecionarMaterial
    /// </summary>
    [TestClass]
    public class CadastrarMaterial
    {
        [TestMethod]
        public void cadastrarMaterial()
        {

            Material material = new Material();
            material.descricao = "Cia Ltda";
            material.id_centro = "100";
            material.tip_mat = "Q";
            material.status_mat = "S";
            material.uni_med = "CX";

            MaterialRepository materialRepository = new MaterialRepository();
            materialRepository.Salvar(material);
        }
    }
}
