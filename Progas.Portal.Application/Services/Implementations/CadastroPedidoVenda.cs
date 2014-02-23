using System;
using System.Collections.Generic;
using System.Linq;
using Progas.Portal.Application.Services.Contracts;
using Progas.Portal.Application.Queries.Contracts;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using Progas.Portal.ViewModel;
//using SAP.Middleware.Connector;

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

        // Realiza a Conexão no SAP conforme os dados do arquivo de configuração.
        /*public class SAPConnect : IDestinationConfiguration
        {
            public bool ChangeEventsSupported()
            {
                return true;
            }
            public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;            

            public RfcConfigParameters GetParameters(string destinationName)
            {
                string filename            = Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "configSAP.txt";
                string[] lines             = File.ReadAllLines(filename);
                RfcConfigParameters config = new RfcConfigParameters();
                SAP.Middleware.Connector.RfcConfigParameters conf = new SAP.Middleware.Connector.RfcConfigParameters();

                try
                {
                    if (destinationName == "DEV")
                    {
                        if (lines != null)
                        {
                            foreach (string s in lines)
                            {
                                string[] parameters = s.Split('=', ' ');
                                if (parameters.Length > 1)
                                {
                                    if (parameters[0] == "AppServerHost")
                                    {
                                        config.Add(RfcConfigParameters.AppServerHost, parameters[1]);
                                    }
                                    else if (parameters[0] == "SAPRouter")
                                    {
                                        config.Add(RfcConfigParameters.SAPRouter, parameters[1]);
                                    }
                                    else if (parameters[0] == "Client")
                                    {
                                        config.Add(RfcConfigParameters.Client, parameters[1]);
                                    }
                                    else if (parameters[0] == "SystemID")
                                    {
                                        config.Add(RfcConfigParameters.SystemID, parameters[1]);
                                    }
                                    else if (parameters[0] == "Username")
                                    {
                                        config.Add(RfcConfigParameters.User, parameters[1]);
                                    }
                                    else if (parameters[0] == "Password")
                                    {
                                        config.Add(RfcConfigParameters.Password, parameters[1]);
                                    }
                                    else if (parameters[0] == "SystemNumber")
                                    {
                                        config.Add(RfcConfigParameters.SystemNumber, parameters[1]);
                                    }
                                }
                            }
                        }
                    }
                    return conf;
                }
                catch (RfcInvalidStateException rfcEx)
                {
                    throw rfcEx;
                }
            }
        }
         */

        public void Salvar(IList<PedidoVendaSalvarVm> pedidoVendaSalvarVm)
        {
            /*
            SAPConnect con = new SAPConnect();
            RfcDestinationManager.RegisterDestinationConfiguration(con);
            RfcDestination dest = RfcDestinationManager.GetDestination("DEV");
            RfcRepository repo = dest.Repository;
            

            
            IRfcFunction fReadTable = repo.CreateFunction("ZFXI_SD06");
            IRfcStructure i_cabecalho = fReadTable.GetStructure("I_CABECALHO");
            RfcStructureMetadata t_item = repo.GetStructureMetadata("ZSTSD007");
            IRfcStructure linha_envio_item = t_item.CreateStructure();
            IRfcTable envio_item = fReadTable.GetTable("TI_ITEM");
            RfcStructureMetadata t_condicoes = repo.GetStructureMetadata("ZSTSD008");
            IRfcStructure linha_envio_cond = t_condicoes.CreateStructure();
            IRfcTable envio_condicao = fReadTable.GetTable("TI_CONDICOES");
            /*
             
            /*
            int v_cont = 1;
            var usuarioConectado = _usuarios.UsuarioConectado();

            foreach (var dados in pedidoVendaSalvarVm)
            {
                if (v_cont == 1)
                {

                    var clienteVendas = _clienteVendas.ConsultaAtivDistribuicao(dados.Cliente, dados.Centro);

                    // DADOS DO CABECALLHO
                    fReadTable.SetValue("I_TIPO_ENVIO", dados.Tipo);
                    i_cabecalho.SetValue("BSTKD", dados.NumeroPedido);
                    i_cabecalho.SetValue("AUART", dados.CodigoTipoPedido);
                    i_cabecalho.SetValue("WERKS", dados.Centro); // Centro
                    i_cabecalho.SetValue("VKORG", dados.Centro);// Organização de vendas = Org_Vendas
                    i_cabecalho.SetValue("VTWEG", Convert.ToString(clienteVendas.Can_dist)); // Canal de distribuição = Can_dist                      
                    i_cabecalho.SetValue("SPART", Convert.ToString(clienteVendas.Set_ativ));   // Setor de atividade = Set_ativ                          
                    i_cabecalho.SetValue("KUNNR", dados.Cliente);
                    //i_cabecalho.SetValue("ERDAT", DateTime.Now);   // Datacp
                    i_cabecalho.SetValue("ZTERM", dados.CodigoCondpgto);
                    i_cabecalho.SetValue("INCO1", dados.Inco1);
                    i_cabecalho.SetValue("INCO2", dados.Inco2);
                    i_cabecalho.SetValue("TRANS", dados.trans);
                    i_cabecalho.SetValue("TRANSRED", dados.transred);
                    i_cabecalho.SetValue("TRANSREDCIF", dados.transredcif);
                    i_cabecalho.SetValue("REPRE", Convert.ToString(usuarioConectado.CodigoFornecedor));
                    i_cabecalho.SetValue("OBSERVACAO", dados.Observacao);

                    DateTime datap = Convert.ToDateTime(dados.datap);
                    //cabecalhoPedido = new PedidoVenda(dados.Tipo,
                    //                                       dados.NumeroPedido,
                    //                                       dados.CodigoTipoPedido,
                    //                                       dados.Centro,
                    //                                       dados.Cliente,
                    //                                       DateTime.Now,
                    //                                       dados.NumeroPedido,
                    //                                       datap,
                    //                                       dados.CodigoCondpgto,
                    //                                       dados.Inco1,
                    //                                       dados.Inco2,
                    //                                       dados.trans,
                    //                                       dados.transred,
                    //                                       dados.transredcif,
                    //                                       Convert.ToString(usuarioConectado.CodigoFornecedor),
                    //                                       dados.Observacao
                    //                                      );
                }
                // LINHAS  
                linha_envio_item.SetValue("POSNR", v_cont); //id_item);
                linha_envio_item.SetValue("MATNR", dados.CodigoMaterial); //dados.CodigoMaterial);
                linha_envio_item.SetValue("MENGE", dados.Quantidade); //dados.Quantidade);
                linha_envio_item.SetValue("MEINS", dados.UnidadeMedida); //dados.UnidadeMedida);
                linha_envio_item.SetValue("PLTYP", dados.listapreco); //dados.listapreco);
                envio_item.Insert(linha_envio_item);

                // CONDICAO
                linha_envio_cond.SetValue("POSNR", v_cont); //id_item);
                linha_envio_cond.SetValue("KWERT", dados.Desconto);
                //linha_envio_cond.SetValue("ZTERM", dados.CodigoMaterial); //dados.CodigoMaterial);
                envio_condicao.Insert(linha_envio_cond);

                string id_item = Convert.ToString(v_cont);
                //var linhaPedido = new PedidoVendaLinha(dados.NumeroPedido,
                //                                         id_item,
                //                                         dados.NumeroPedido,
                //                                         dados.CodigoMaterial,

                //                                         dados.Quantidade,
                //                                         dados.UnidadeMedida,
                //                                         dados.listapreco,
                //                                         dados.Desconto);
                //linhasPedido.Add(linhaPedido);

            }

            try
            {
                fReadTable.Invoke(dest);
                IRfcTable retorno_itens = fReadTable.GetTable("TE_ITEM");
                IRfcTable retorno_condicoes = fReadTable.GetTable("TE_CONDICOES");


                //_unitOfWork.Commit();
                //_unitOfWork.Dispose();

            }
            catch (Exception e)
            {

            }*/
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

                        PedidoVenda pedidoVenda = _pedidosVenda.CotacaoPedidoContendo(dados.NumeroPedido, Convert.ToString(usuarioConectado.CodigoDoFornecedor));
                        datacp = Convert.ToDateTime(pedidoVenda.Datacp);

                        // Deleta a linha antiga antes de inserir a nova
                        if (pedidoVenda != null)
                        {
                            _pedidosVenda.Delete(pedidoVenda);
                        }
                        
                        IList<PedidoVendaLinha> LinhasSalvas = _pedidosVendaLinha.CotacaoPedidoContendo(dados.NumeroPedido).List();

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
                foreach (var dados in pedidoVendaSalvarVm)
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
                                                               Convert.ToString(usuarioConectado.CodigoDoFornecedor),
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
                }
            }
            catch (Exception)
            {
                _unitOfWork.RollBack();
                throw;
            }
        }

        
     
    }
}
