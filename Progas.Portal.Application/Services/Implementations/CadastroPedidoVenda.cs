using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Progas.Portal.Application.Services.Contracts;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;
using System.IO;
using SAP.Middleware.Connector;

namespace Progas.Portal.Application.Services.Implementations
{
    public class CadastroPedidoVenda : ICadastroPedidoVenda
    {
        private readonly IUnitOfWork          _unitOfWork;
        private readonly IPedidosVendaLinha   _pedidosVendaLinha;
        private readonly IPedidosVenda        _pedidosVenda;
        private readonly IUsuarios            _usuarios;
        private readonly IConsultaPedidoVenda _consultaPedidoVenda;
        private readonly IClienteVendas       _clienteVendas;
        DateTime datacp;        

        public CadastroPedidoVenda(IUnitOfWork unitOfWork, IPedidosVendaLinha pedidosVendaLinha, IPedidosVenda pedidosVenda, IUsuarios usuarios, IConsultaPedidoVenda consultaPedidoVenda, IClienteVendas clienteVendas)
        {
            _unitOfWork          = unitOfWork;
            _pedidosVenda        = pedidosVenda;
            _pedidosVendaLinha   = pedidosVendaLinha;
            _usuarios            = usuarios;
            _consultaPedidoVenda = consultaPedidoVenda;
            _clienteVendas       = clienteVendas;            
        }               
        // Parametro de Repositorio utilziado na conexao SAP
        public static string repositorydestinationPar = System.Configuration.ConfigurationSettings.AppSettings["RepositoryDestination"];        

        public void Salvar(IList<PedidoVendaSalvarVm> pedidoVendaSalvarVm)
        {
           
            SAPConnect           con               = new SAPConnect();            
            RfcDestinationManager.RegisterDestinationConfiguration(con);
            RfcDestination       dest              = RfcDestinationManager.GetDestination(repositorydestinationPar);            
            RfcRepository        repo              = dest.Repository;  
            IRfcFunction         fReadTable        = repo.CreateFunction("ZFXI_SD06");
            IRfcStructure        i_cabecalho       = fReadTable.GetStructure("I_CABECALHO");
            RfcStructureMetadata t_item            = repo.GetStructureMetadata("ZSTSD007");
            IRfcStructure        linha_envio_item  = t_item.CreateStructure();
            IRfcTable            envio_item        = fReadTable.GetTable("TI_ITEM");
            RfcStructureMetadata t_condicoes       = repo.GetStructureMetadata("ZSTSD008");
            IRfcStructure        linha_envio_cond  = t_condicoes.CreateStructure();
            IRfcTable            envio_condicao    = fReadTable.GetTable("TI_CONDICOES");
                         
            int v_cont = 1;
            var usuarioConectado = _usuarios.UsuarioConectado();
            
            foreach (var dados in pedidoVendaSalvarVm)
            {
                // DADOS DO CABECALLHO (Estrutura tipo Parametro no SAP)
                if (v_cont == 1)
                {
                    var clienteVendas = _clienteVendas.ConsultaAtivDistribuicao(dados.Cliente, dados.Centro);                    
                    fReadTable.SetValue("I_TIPO_ENVIO", dados.Tipo);
                    i_cabecalho.SetValue("BSTKD",       dados.NumeroPedido);
                    i_cabecalho.SetValue("AUART",       dados.CodigoTipoPedido);
                    i_cabecalho.SetValue("WERKS",       dados.Centro);
                    i_cabecalho.SetValue("VKORG",       dados.Centro);
                    i_cabecalho.SetValue("VTWEG",       Convert.ToString(clienteVendas.Can_dist));
                    i_cabecalho.SetValue("SPART",       Convert.ToString(clienteVendas.Set_ativ));
                    i_cabecalho.SetValue("KUNNR",       dados.Cliente);                    
                    i_cabecalho.SetValue("ZTERM",       dados.CodigoCondpgto);
                    i_cabecalho.SetValue("INCO1",       dados.Inco1);
                    i_cabecalho.SetValue("INCO2",       dados.Inco2);
                    i_cabecalho.SetValue("TRANS",       dados.trans);
                    i_cabecalho.SetValue("TRANSRED",    dados.transred);
                    i_cabecalho.SetValue("TRANSREDCIF", dados.transredcif);
                    i_cabecalho.SetValue("REPRE",       Convert.ToString(usuarioConectado.CodigoFornecedor));
                    i_cabecalho.SetValue("OBSERVACAO",  dados.Observacao);                   
                }                
                // LINHAS (Estrutura tipo tabela)
                linha_envio_item.SetValue("POSNR", v_cont);
                linha_envio_item.SetValue("MATNR", dados.CodigoMaterial);
                linha_envio_item.SetValue("MENGE", dados.Quantidade);
                linha_envio_item.SetValue("MEINS", dados.UnidadeMedida);
                linha_envio_item.SetValue("PLTYP", dados.listapreco);
                envio_item.Insert(linha_envio_item);

                // CONDICAO (Estrutura tipo tabela) 
                linha_envio_cond.SetValue("POSNR", v_cont);
                linha_envio_cond.SetValue("KWERT", dados.Desconto);
                linha_envio_cond.SetValue("KSCHL", dados.CodigoCondpgto);
                envio_condicao.Insert(linha_envio_cond);                
                // Apos inserir o ultimo item na estrutura do SAP, realiza a chamada da Função e salva para o tipo Gravação e retornar as linhas.
                v_cont++;
                if (dados == pedidoVendaSalvarVm.Last())
                {
                    try
                    {                        
                        fReadTable.Invoke(dest);
                        IRfcTable retorno_itens     = fReadTable.GetTable("TE_ITEM");
                        IRfcTable retorno_condicoes = fReadTable.GetTable("TE_CONDICOES");
                        // Se for do tipo gravacao, recebe o retorno do calculo e salva os dados
                        if (dados.Tipo == "G")
                        {
                            foreach(IRfcStructure retornoItens in retorno_itens)
                            {
                                string numeroItem = retornoItens.GetString("POSNR"); 
                                if (numeroItem == "000001")
                                {
                                    DateTime datap = Convert.ToDateTime(dados.datap);
                                    var cabecalhoPedido = new PedidoVenda( dados.Tipo,
                                                                           retornoItens.GetString("COTACAO"),
                                                                           dados.CodigoTipoPedido,
                                                                           dados.Centro,
                                                                           dados.Cliente,
                                                                           DateTime.Now,
                                                                           dados.NumeroPedido,
                                                                           datap,
                                                                           dados.CodigoCondpgto,
                                                                           dados.Inco1,
                                                                           dados.Inco2,
                                                                           dados.trans,
                                                                           dados.transred,
                                                                           dados.transredcif,
                                                                           Convert.ToString(usuarioConectado.CodigoFornecedor),
                                                                           dados.Observacao);
                                    _pedidosVenda.Save(cabecalhoPedido);
                                }
                                var linhasPedido = new PedidoVendaLinha( retornoItens.GetString("COTACAO"),
                                                                         retornoItens.GetString("POSNR"),
                                                                         dados.NumeroPedido,
                                                                         dados.CodigoMaterial,
                                                                         dados.Quantidade,
                                                                         dados.UnidadeMedida,                                                                         
                                                                         dados.listapreco,
                                                                         Convert.ToDecimal(retornoItens.GetString("VLRTAB")),// valtab
                                                                         Convert.ToDecimal(retornoItens.GetString("VLRPOL")),// valpol
                                                                         dados.Desconto,
                                                                         retornoItens.GetString("ABGRU")// valpol
                                                                         );
                                    _pedidosVendaLinha.Save(linhasPedido);
                            }
                                // retornar apenas a lista de itens                                
                                _unitOfWork.Commit();                                
                                _unitOfWork.Dispose();
                                RfcDestinationManager.UnregisterDestinationConfiguration(con);                                
                            }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }            
        }
             
        public void Atualizar(IList<PedidoVendaSalvarVm> pedidoVendaSalvarVm)
        {
            try
            {
                int v_cont = 0;
                int v_cont2 = 0;                
                // Deleta as linhas antes de inserir as novas
                foreach (var dados in pedidoVendaSalvarVm)
                {
                    _unitOfWork.BeginTransaction();
                    var usuarioConectado = _usuarios.UsuarioConectado();
                    v_cont2 = v_cont2 + 1;
                    if (v_cont2 == 1)
                    {
                        PedidoVenda pedidoVenda = _pedidosVenda.CotacaoPedidoContendo(dados.NumeroPedido, Convert.ToString(usuarioConectado.CodigoFornecedor));
                        datacp = Convert.ToDateTime(pedidoVenda.Datacp);
                        // Deleta a linha antiga antes de inserir a nova
                        if (pedidoVenda != null)
                        {
                            _pedidosVenda.Delete(pedidoVenda);
                        }                        
                        IList<PedidoVendaLinha> LinhasSalvas        = _pedidosVendaLinha.CotacaoPedidoContendo(dados.NumeroPedido).List();
                        IList<PedidoVendaLinha> LinhasParaAtualizar = LinhasSalvas.Where(ls => pedidoVendaSalvarVm.All(
                             lc => lc.NumeroPedido.Trim() == ls.Id_pedido.Trim()
                                 )).ToList();

                        foreach (var linhas in LinhasParaAtualizar)
                        {
                            _pedidosVendaLinha.Delete(linhas);
                        }
                        _unitOfWork.Commit();
                        _unitOfWork.Dispose();
                    }
                }
                    
                // Salva a Edição do pedido após excluir
                /*foreach (var dados in pedidoVendaSalvarVm)
                {
                    _unitOfWork.BeginTransaction();
                    var usuarioConectado = _usuarios.UsuarioConectado();
                    v_cont = v_cont + 1;
                    if (v_cont == 1)
                    {
                        DateTime datap = Convert.ToDateTime(dados.datap);
                        var cabecalhoPedido = new PedidoVenda( dados.Tipo,
                                                               dados.NumeroPedido,
                                                               dados.CodigoTipoPedido,
                                                               dados.Centro,
                                                               dados.Cliente,
                                                               DateTime.Now,
                                                               dados.NumeroPedido,
                                                               datap,
                                                               dados.CodigoCondpgto,
                                                               dados.Inco1,
                                                               dados.Inco2,
                                                               dados.trans,
                                                               dados.transred,
                                                               dados.transredcif,
                                                               Convert.ToString(usuarioConectado.CodigoFornecedor),
                                                               dados.Observacao
                                                              );
                        _pedidosVenda.Save(cabecalhoPedido);
                    }
                    string id_item = Convert.ToString(v_cont);
                    var linhasPedido = new PedidoVendaLinha(dados.NumeroPedido,
                                                             id_item,
                                                             dados.NumeroPedido,
                                                             dados.CodigoMaterial,
                                                             dados.Quantidade,
                                                             dados.UnidadeMedida,
                                                             dados.listapreco,
                                                             dados.Desconto);
                    _pedidosVendaLinha.Save(linhasPedido);
                    _unitOfWork.Commit();
                    _unitOfWork.Dispose();
                }*/
            }
            catch (Exception)
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        // Realiza a Conexão no SAP conforme os dados do arquivo Web.config.
        public class SAPConnect : IDestinationConfiguration
        {   // Dados de Parametros do Web.config
            public static string appserverhost         = System.Configuration.ConfigurationSettings.AppSettings["AppServerHost"];
            public static string saprouter             = System.Configuration.ConfigurationSettings.AppSettings["SAPRouter"];
            public static string systemnumber          = System.Configuration.ConfigurationSettings.AppSettings["SystemNumber"];
            public static string systemid              = System.Configuration.ConfigurationSettings.AppSettings["SystemID"];
            public static string user                  = System.Configuration.ConfigurationSettings.AppSettings["User"];
            public static string password              = System.Configuration.ConfigurationSettings.AppSettings["Password"];
            public static string client                = System.Configuration.ConfigurationSettings.AppSettings["Client"];
            public static string poolsize              = System.Configuration.ConfigurationSettings.AppSettings["PoolSize"];
            public static string repositorydestination = System.Configuration.ConfigurationSettings.AppSettings["RepositoryDestination"];

            public RfcConfigParameters GetParameters(String destinationName)
            {                
                if (repositorydestination == destinationName)
                {

                    RfcConfigParameters parms = new RfcConfigParameters();
                    parms.Add(RfcConfigParameters.AppServerHost, appserverhost);
                    parms.Add(RfcConfigParameters.SAPRouter,     saprouter);
                    parms.Add(RfcConfigParameters.SystemNumber,  systemnumber);
                    parms.Add(RfcConfigParameters.SystemID,      systemid);
                    parms.Add(RfcConfigParameters.User,          user);
                    parms.Add(RfcConfigParameters.Password,      password);
                    parms.Add(RfcConfigParameters.Client,        client);
                    parms.Add(RfcConfigParameters.PoolSize,      poolsize);
                    return parms;
                }
                else return null;
            }
            // The following two are not used in this example:
            public bool ChangeEventsSupported()
            {
                return false;
            }
            public event RfcDestinationManager.ConfigurationChangeHandler
            ConfigurationChanged;
        }
    }
}
