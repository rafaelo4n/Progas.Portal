using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Engine.Query.Sql;
using Portal.DadosSap.Business.Repository;
using Portal.DadosSap.Entity;
using NHibernate.Criterion;

namespace Portal.DadosSap.Business.Implementation
{

    // classe que implementa a inferface do repository(IRepositoryBase)
    // Salvar – Salva uma nova entidade no banco
    // Alterar – Altera uma entidade ja existente no banco
    // Excluir – Exclui uma entidade no banco
    // ObterPorId – Recupera uma entidade pelo código (PK)
    // ObterTodos – Recupera todas as entidades
    public class RepositoryBase<T> : IRepositoryBase<T>
    {
        /// <summary>
        /// Sessão da conexão
        /// </summary>
        private static ISessionFactory sessionFactory;

        /// <summary>
        /// Gets SessionFactory.
        /// </summary>
        public static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    configuration.AddAssembly(typeof(T).Assembly);
                    sessionFactory = configuration.BuildSessionFactory();
                }
                return sessionFactory;
            }
        }

        /// <summary>
        /// Método para salvar uma entidade
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// A própria entidade
        /// </returns>
        /// <exception cref="Exception">
        /// Retorna uma exception para quem chamou
        /// </exception>
        public T Salvar(T entity)
        {
            try
            {
                using (ISession session = SessionFactory.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(entity);
                        transaction.Commit();
                    }
                }

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Método para alterar uma entidade
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// A própria entidade
        /// </returns>
        /// <exception cref="Exception">
        /// Retorna uma exception para quem chamou
        /// </exception>
        public T Alterar(T entity)
        {
            try
            {
                using (ISession session = SessionFactory.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Update(entity);
                        transaction.Commit();
                    }
                }

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Método para exclui uma entidade
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <exception cref="Exception">
        /// Retorna uma exception para quem chamou
        /// </exception>
        public void Excluir(T entity)
        {              

            try
            {
                using (ISession session = SessionFactory.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(entity);
                        transaction.Commit();

                        //String deletar = "delete from pro_fornecedor";
                        //IQuery query = session.CreateQuery(deletar);
                        //query.ExecuteUpdate();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Método para recuperar uma entidade específica
        /// </summary>
        /// <param name="id">
        /// Código da entidade (PK)
        /// </param>
        /// <returns>
        /// Uma entidade
        /// </returns>
        /// <exception cref="Exception">
        /// Retorna uma exception para quem chamou
        /// </exception>
        public T ObterPorId(string id)
        {
            try
            {
                using (ISession session = SessionFactory.OpenSession())
                {
                    
                    
                    return session.Get<T>(id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        /// <summary>
        /// Método para recuperar todas as entidades
        /// </summary>
        /// <returns>
        /// Uma lista de entidades
        /// </returns>
        /// <exception cref="Exception">
        /// Retorna uma exception para quem chamou
        /// </exception>
        public IList<T> ObterTodos()
        {
            IList<T> lista;

            try
            {
                using (ISession session = SessionFactory.OpenSession())
                {
                    //lista = new List<T>(session.CreateCriteria(typeof(T)).AddOrder(Order.Asc("Codigo")).List<T>());
                    lista = session.CreateCriteria(typeof(T)).List<T>();
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public IList<T> ObterRegistrosUmCampo(string campo, string busca)
        {
            IList<T> lista;

            try
            {
                using (ISession session = SessionFactory.OpenSession())
                {
                    //lista = new List<T>(session.CreateCriteria(typeof(T)).AddOrder(Order.Asc("Codigo")).List<T>());
                    lista = session.CreateCriteria(typeof(T)).Add(Restrictions.Like(campo.Trim(), busca.Trim())).List<T>();

                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<T> ObterRegistrosDoisCampos(String campo1, String busca1, String campo2, String busca2)
        {
            IList<T> lista;

            try
            {
                using (ISession session = SessionFactory.OpenSession())
                {
                    lista = session.CreateCriteria(typeof(T)).Add(Restrictions.Like(campo1, busca1)).Add(Restrictions.Like(campo2, busca2)).List<T>();

                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<T> ObterRegistrosTresCampos(string campo1, string busca1, string campo2, string busca2, string campo3, string busca3)
        {
            IList<T> lista;

            try
            {
                using (ISession session = SessionFactory.OpenSession())
                {
                    lista = session.CreateCriteria(typeof(T)).Add(Restrictions.Like(campo1, busca1)).Add(Restrictions.Like(campo2, busca2)).Add(Restrictions.Like(campo3, busca3)).List<T>();
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<T> ObterRegistrosQuatroCampos(string campo1, string busca1, string campo2, string busca2, string campo3, string busca3, string campo4, string busca4)
        {
            IList<T> lista;

            try
            {
                using (ISession session = SessionFactory.OpenSession())
                {                    
                    lista = session.CreateCriteria(typeof(T)).Add(Restrictions.Like(campo1, busca1)).Add(Restrictions.Like(campo2, busca2)).Add(Restrictions.Like(campo3, busca3)).Add(Restrictions.Like(campo4, busca4)).List<T>();
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
        // Pesquisa de Inco1 + Inco2
        public IList<T> PesquisaIncotermLinha(String CampoCodigoIncotermCab, String campoIncotermLinha, String valorCodigoIncotermCab, String valorIncotermLinha)
        {
            IList<T> lista;

            try
            {
                using (ISession session = SessionFactory.OpenSession())
                {
                    lista = session.CreateCriteria(typeof(T)).Add(Restrictions.Like(CampoCodigoIncotermCab, campoIncotermLinha)).Add(Restrictions.Like(valorCodigoIncotermCab, valorIncotermLinha)).List<T>();
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
     }
}
