using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Portal.DadosSap.Entity;

namespace Portal.DadosSap
{
    /// <summary>
    /// Summary description for ConexaoNHibernateTeste
    /// </summary>
    [TestClass]
    public class ConexaoNHibernateTeste
    {
        public ConexaoNHibernateTeste()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        [TestMethod]
        public void GerarScript()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(Cliente).Assembly);

            var schemaExport = new SchemaExport(cfg);
            schemaExport.Create(true, false);
        }

        [TestMethod]
        public void GerarDataBase()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(Cliente).Assembly);

            var schemaExport = new SchemaExport(cfg);
            schemaExport.Create(true, true);
        }
    }
}
