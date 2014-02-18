using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP.Middleware.Connector;
//using Progas.Portal.Infra.Repositories.Contracts;
//using Progas.Portal.Application.Services.Implementations;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Portal.DadosSap.Entity;
using Portal.DadosSap.Business.Implementation;
//using Progas.Portal.Application.Services.Contracts;
using System.ServiceModel;
//using Progas.Portal.ViewModel;

namespace Portal.DadosSap
{
    public class Interfaces
    {
        // SMGW
        public static void conexaoRFC()
        {

            RfcDestinationManager.RegisterDestinationConfiguration(new MyBackendConfig());
            RfcServerManager.RegisterServerConfiguration(new MyServerConfig());
            Type[] handlers = new Type[1] { typeof(MyServerHandler) };
            RfcServer server = RfcServerManager.GetServer("PORTAL_PROGAS", handlers);
            server.Start();
            //Console.WriteLine("Server has been started. Press X to exit.\n");
            //while (true)
            //{
            //    Console.WriteLine("Server has been started. Press X to exit.\n");
            //    if (Console.ReadLine().Equals("X")) break;
            //}
            //
            //server.Shutdown(true); //Shuts down immediately, aborting ongoing requests.
  
        }

            public void recebeDados()
            {
                Console.WriteLine("Teste do Calculo de pedido");
            }
        


        public class MyServerHandler // MyServerHandlerExecute
        {
            [RfcServerFunction(Default = true)]
            public static void StfcConnection(RfcServerContext context, IRfcFunction function)
            {
                //Console.WriteLine("Entreo na seleção de interfaces");
                //Console.WriteLine(context.FunctionName);

                switch (context.FunctionName)
                {
                    case "ZFXI_SD01C":
                        Console.WriteLine("Interface de Cliente");
                        MyServerHandlerExecute.StfcInterfaceCliente(context, function);
                        break;
                }

                switch (context.FunctionName)
                {
                    case "ZFXI_SD02C":
                        Console.WriteLine("Interface de Fornecedor");
                        MyServerHandlerExecute.StfcInterfaceFornecedor(context, function);
                        break;
                }

                switch (context.FunctionName)
                {

                    case "ZFXI_SD03C":
                        Console.WriteLine("Interface de Material");
                        MyServerHandlerExecute.StfcInterfaceMaterial(context, function);
                        break;
                }

                switch (context.FunctionName)
                {
                    case "ZFXI_SD04C":
                        Console.WriteLine("Interface de Condicao de Pagamento");
                        MyServerHandlerExecute.StfcInterfaceCondPag(context, function);
                        break;
                }

                switch (context.FunctionName)
                {
                    case "ZFXI_SD05C":
                        Console.WriteLine("Unidade de Medida");
                        MyServerHandlerExecute.StfcInterfaceUM(context, function);
                        break;
                }
            }
        }
        
        
        public class MyServerHandlerExecute
        {
            // SD01 - Inteface de cliente - Comunicação
            // funcao - ZFXI_SD01C
            //[RfcServerFunction(Name = "ZFXI_SD01C")]
            public static void StfcInterfaceCliente(RfcServerContext context,
            IRfcFunction function)
            {
                //
                // CLIENTE
                //

                // Exibe no console a interface que será executada
                Console.WriteLine("Received function call {0} from system {1}.", function.Metadata.Name, context.SystemAttributes.SystemID);                

                // Implementa repositorio antes do Foreach para evitar duplicações
                ClienteRepository clienteRepository = new ClienteRepository();

                // Implementa repositorio antes do Foreach para evitar duplicações
                Cliente cliente = new Cliente();

                // Flag da interface que de Limpar tabela de dados
                Char deletar = function.GetChar("I_REFRESH");

                // exibe se o mesmo foi flegado
                Console.WriteLine(deletar);

                // Se estiver espaco em branco na variavel, não limpa a tabela da interface.
                if (deletar != ' ')
                {
                    // Apaga todos os registros da tabela pro_fornecedor
                    IList<Cliente> fromDB = clienteRepository.ObterTodos();
                    foreach (Cliente dados in fromDB)
                    {
                        clienteRepository.Excluir(dados);
                    }
                }

                // ZTBSD056 - ZTBXI_101
                IRfcTable it_cliente = function.GetTable("IT_CLIENTE");

                // Implementa Repositorio Rfc de resposta
                RfcRepository rep = context.Repository;

                // RETORNO
                RfcStructureMetadata bapiret2 = rep.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno = bapiret2.CreateStructure();

                int v_cont = 0;
                foreach (IRfcStructure row in it_cliente)
                {                    
                    cliente.Id_cliente   = row.GetString("KUNNR");
                    cliente.Nome         = row.GetString("NAME1");
                    cliente.Cpf          = row.GetString("STCD2");
                    cliente.Cnpj         = row.GetString("STCD1");
                    cliente.Nr_ie_cli    = row.GetString("STCD3");
                    cliente.Cep          = row.GetString("POST_CODE");
                    cliente.Endereco     = row.GetString("STREET");
                    cliente.Numero       = row.GetString("HOUSE_NUM1");
                    cliente.Complemento  = row.GetString("HOUSE_NUM2");
                    cliente.Municipio    = row.GetString("CITY1");
                    cliente.Bairro       = row.GetString("CITY2");
                    cliente.Uf           = row.GetString("UF");
                    cliente.Pais         = row.GetString("COUNTRY");
                    cliente.Tel_res      = row.GetString("TELF1");
                    cliente.Tel_cel      = row.GetString("TELF2");
                    cliente.Fax          = row.GetString("TELFX");
                    cliente.Email        = row.GetString("EMAIL");
                    // Pacote
                    cliente.Pacote       = row.GetString("PACOTE");
                    // Data Cricao
                    string v_data_Cricao = row.GetString("ERDAT");
                    cliente.Data_criacao = Convert.ToDateTime(v_data_Cricao);
                    // Hora de Criacao
                    cliente.Hora_criacao = row.GetString("ERZET");                                

                    try
                    {
                        v_cont = v_cont + 1;
                        if (deletar == ' ')
                        {
                            IList<Cliente> fromDB = clienteRepository.ObterTodosComCampo("Id_cliente", cliente.Id_cliente);
                            foreach (Cliente dados in fromDB)
                            {
                                clienteRepository.Excluir(dados);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Em caso de erro retorna o erro
                        Console.Write("Erro ao inserir o Cliente, Mensagem: " + ex);

                        IRfcTable retorno = function.GetTable("IT_RETURN");
                        linha_retorno.SetValue("TYPE", "E");
                        linha_retorno.SetValue("MESSAGE", ex.Message);
                        linha_retorno.SetValue("MESSAGE", "Erro ao inserir o Cliente: " + cliente.Nome + " - Id: " + cliente.Id_cliente);
                        retorno.Insert(linha_retorno);
                    }

                    clienteRepository.Salvar(cliente);
                    String PACOTE = row.GetString("PACOTE");
                    String ERDAT = row.GetString("ERDAT");
                    String ERZET = row.GetString("ERZET");
                }

                IRfcTable retornoSucesso = function.GetTable("IT_RETURN");
                linha_retorno.SetValue("TYPE", "S");
                linha_retorno.SetValue("MESSAGE", "Registros com Sucesso: " + v_cont);
                retornoSucesso.Insert(linha_retorno);

                // FIM CLIENTE

                //
                // CLIENTE VENDAS
                //

                // Implementa repositorio antes do Foreach para evitar duplicações
                ClienteVendasRepository clienteVendasRepository = new ClienteVendasRepository();

                // Implementa repositorio antes do Foreach para evitar duplicações
                ClienteVendas clienteVendas = new ClienteVendas();

                // Se estiver espaco em branco na variavel, não limpa a tabela da interface.
                if (deletar != ' ')
                {
                    // Apaga todos os registros da tabela pro_cliente_vendas
                    IList<ClienteVendas> fromDB = clienteVendasRepository.ObterTodos();
                    foreach (ClienteVendas dados in fromDB)
                    {
                        clienteVendasRepository.Excluir(dados);
                    }
                }

                // ZTBSD056 - ZTBXI_101
                IRfcTable it_cliente_vendas = function.GetTable("IT_CLIENTE_AV");

                // Implementa Repositorio Rfc de resposta
                RfcRepository repVendas = context.Repository;

                // RETORNO
                RfcStructureMetadata bapiret2Vendas = repVendas.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno_vendas = bapiret2Vendas.CreateStructure();

                int v_cont_vendas = 0;
                foreach (IRfcStructure row in it_cliente_vendas)
                {

                    clienteVendas.Id_cliente    = row.GetString("KUNNR");
                    clienteVendas.Org_vendas    = row.GetString("VKORG");
                    clienteVendas.Can_dist      = row.GetString("VTWEG");
                    clienteVendas.Set_ativ      = row.GetString("SPART");
                    clienteVendas.Grupo_cli     = row.GetString("KDGRP");
                    clienteVendas.Id_fornecedor = row.GetString("LIFNR");
                    // Pacote
                    clienteVendas.Pacote = row.GetString("PACOTE");
                    // Data Cricao
                    string v_data_Cricao = row.GetString("ERDAT");
                    clienteVendas.Data_criacao = Convert.ToDateTime(v_data_Cricao);
                    // Hora de Criacao
                    clienteVendas.Hora_criacao = row.GetString("ERZET");

                    try
                    {
                        v_cont_vendas = v_cont_vendas + 1;
                        if (deletar == ' ')
                        {
                            IList<ClienteVendas> fromDB = clienteVendasRepository.PesquisaClienteVendas("Id_cliente", clienteVendas.Id_cliente, "Org_vendas", clienteVendas.Org_vendas);
                            foreach (ClienteVendas dados in fromDB)
                            {
                                clienteVendasRepository.Excluir(dados);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Em caso de erro retorna o erro
                        Console.Write("Erro ao inserir o Cliente Vendas, Mensagem: " + ex);

                        IRfcTable retorno = function.GetTable("IT_RETURN");
                        linha_retorno_vendas.SetValue("TYPE", "E");
                        linha_retorno_vendas.SetValue("MESSAGE", ex.Message);
                        linha_retorno_vendas.SetValue("MESSAGE", "Erro ao inserir o Cliente: " + cliente.Nome + " - Id: " + cliente.Id_cliente);
                        retorno.Insert(linha_retorno_vendas);
                    }

                    clienteVendasRepository.Salvar(clienteVendas);
                    String PACOTE = row.GetString("PACOTE");
                    String ERDAT = row.GetString("ERDAT");
                    String ERZET = row.GetString("ERZET");
                }

                IRfcTable retornoSucessoVendas = function.GetTable("IT_RETURN");
                linha_retorno_vendas.SetValue("TYPE", "S");
                linha_retorno_vendas.SetValue("MESSAGE", "Registros com Sucesso Grupo de Vendas: " + v_cont_vendas);
                retornoSucessoVendas.Insert(linha_retorno_vendas);

                // FIM CLIENTE VENDAS


            }

            // SD02 - Inteface de fornecedor - Comunicação
            // funcao - ZFXI_SD02C
            //[RfcServerFunction(Name = "ZGXI_SD02")]
            public static void StfcInterfaceFornecedor(RfcServerContext context,
            IRfcFunction function)
            {
                // Exibe no console a interface que será executada
                Console.WriteLine("Received function call {0} from system {1}.", function.Metadata.Name, context.SystemAttributes.SystemID);
               
                // Implementa Repositório dos dados
                FornecedorRepository fornecedorRepository = new FornecedorRepository();

                // Implementa repositorio antes do Foreach para evitar duplicações
                Fornecedor fornecedor = new Fornecedor();
                
                // Flag da interface que de Limpar tabela de dados
                Char deletar = function.GetChar("I_REFRESH");

                // exibe se o mesmo foi flegado
                Console.WriteLine(deletar);

                // Se estiver espaco em branco na variavel, não limpa a tabela da interface.
                if (deletar != ' ')
                {
                    // Apaga todos os registros da tabela pro_fornecedor
                    IList<Fornecedor> fromDB = fornecedorRepository.ObterTodos();
                    foreach (Fornecedor dados in fromDB)
                    {
                        fornecedorRepository.Excluir(dados);
                    }                    
                }

                // ZTBSD060
                IRfcTable it_fornecedor = function.GetTable("IT_FORNECEDOR");
                    
                // Implementa Repositorio Rfc de resposta
                RfcRepository rep = context.Repository;

                // RETORNO - BAPIRET2
                RfcStructureMetadata bapiret2 = rep.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno = bapiret2.CreateStructure();                                                                 

                int v_cont = 0;
                foreach (IRfcStructure row in it_fornecedor)
                {
                    fornecedor.Codigo     = row.GetString("LIFNR");
                    fornecedor.Nome       = row.GetString("NAME1");
                    fornecedor.Cpf        = row.GetString("STCD2");
                    fornecedor.Cnpj       = row.GetString("STCD1");
                    fornecedor.Nr_ie_for  = row.GetString("STCD3");
                    fornecedor.Cep        = row.GetString("POST_CODE");
                    fornecedor.Endereco   = row.GetString("STREET");
                    fornecedor.Numero     = row.GetString("HOUSE_NUM1");
                    fornecedor.Municipio  = row.GetString("CITY1");
                    fornecedor.Bairro     = row.GetString("CITY2");
                    fornecedor.Uf         = row.GetString("UF");
                    fornecedor.Pais       = row.GetString("COUNTRY");
                    fornecedor.Tel_res    = row.GetString("TELF1");
                    fornecedor.Tel_res    = row.GetString("TELF2");
                    fornecedor.Tel_cel    = row.GetString("TELF1");
                    fornecedor.Fax        = row.GetString("TELFX");
                    fornecedor.Email      = row.GetString("EMAIL");
                    // Pacote
                    fornecedor.Pacote = row.GetString("PACOTE");
                    // Data Cricao
                    string v_data_Cricao = row.GetString("ERDAT");
                    fornecedor.Data_criacao = Convert.ToDateTime(v_data_Cricao);
                    // Hora de Criacao
                    fornecedor.Hora_criacao = row.GetString("ERZET");                                    

                    try
                    {
                        v_cont = v_cont + 1;
                        if (deletar == ' ')
                        {
                            IList<Fornecedor> fromDB = fornecedorRepository.ObterTodosComCampo("Codigo", fornecedor.Codigo);
                            foreach (Fornecedor dados in fromDB)
                            {
                                fornecedorRepository.Excluir(dados);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Em caso de erro retorna o erro e a descricao do material
                        Console.Write("Erro ao inserir o Fornecedor, Mensagem:" + ex);

                        IRfcTable retorno = function.GetTable("IT_RETURN");
                        linha_retorno.SetValue("TYPE", "E");
                        linha_retorno.SetValue("MESSAGE", ex.Message);
                        linha_retorno.SetValue("MESSAGE", "Erro ao inserir o Fornecedor: " + fornecedor.Nome);
                        retorno.Insert(linha_retorno);
                    }

                    fornecedorRepository.Salvar(fornecedor);
                    String PACOTE = row.GetString("PACOTE");
                    String ERDAT = row.GetString("ERDAT");
                    String ERZET = row.GetString("ERZET");
                }

                IRfcTable retornoSucesso = function.GetTable("IT_RETURN");
                linha_retorno.SetValue("TYPE", "S");
                linha_retorno.SetValue("MESSAGE", "Registros com Sucesso: " + v_cont);
                retornoSucesso.Insert(linha_retorno);
            }

            // SD03 - Inteface de materiais - Comunicação
            // funcao - ZFXI_SD03C
            //[RfcServerFunction(Name = "ZFXI_SD03C")]
            public static void StfcInterfaceMaterial(RfcServerContext context,
            IRfcFunction function)
            {
                // Exibe no console a interface que será executada
                Console.WriteLine("Received function call {0} from system {1}.", function.Metadata.Name, context.SystemAttributes.SystemID);

                // Implementa Repositório dos dados
                MaterialRepository materialRepository = new MaterialRepository();

                // Implementa repositorio antes do Foreach para evitar duplicações
                Material material = new Material();

                // Flag da interface que de Limpar tabela de dados
                Char deletar = function.GetChar("I_REFRESH");

                // exibe se o mesmo foi flegado
                Console.WriteLine(deletar);

                // Se estiver espaco em branco na variavel, não limpa a tabela da interface.
                if (deletar != ' ')
                {
                    // Apaga todos os registros da tabela pro_fornecedor
                    IList<Material> fromDB = materialRepository.ObterTodos();
                    foreach (Material dados in fromDB)
                    {
                        materialRepository.Excluir(dados);       
                    }
                }

                IRfcTable it_material = function.GetTable("IT_MATERIAL");

                // Implementa Repositorio Rfc de resposta
                RfcRepository rep = context.Repository;

                // RETORNO - BAPIRET2
                RfcStructureMetadata bapiret2 = rep.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno = bapiret2.CreateStructure();   

                int v_cont = 0;
                foreach (IRfcStructure row in it_material)
                {

                    material.Id_material = row.GetString("MATNR");
                    material.Descricao   = row.GetString("MAKTX");
                    material.Id_centro   = row.GetString("WERKS");
                    material.Tip_mat     = row.GetString("MTART");
                    material.Status_mat  = row.GetString("MSTAE");
                    material.Uni_med     = row.GetString("MEINS");
                    material.Status_mat = row.GetString("MSTAE");
                    // Pacote
                    material.Pacote = row.GetString("PACOTE");
                    // Data Cricao
                    string v_data_Cricao = row.GetString("ERDAT");
                    material.Data_criacao = Convert.ToDateTime(v_data_Cricao);
                    // Hora de Criacao
                    material.Hora_criacao = row.GetString("ERZET");            
                    
                    try
                    {                        
                        v_cont = v_cont + 1;
                        if (deletar == ' ')
                        {
                            IList<Material> fromDB = materialRepository.ObterTodosComCampo("Id_material", material.Id_material);
                            foreach (Material dados in fromDB)
                            {       
                                materialRepository.Excluir(dados);                                
                            }                            
                        }
                    }
                    catch (Exception ex)
                    {
                        // Em caso de erro retorna o erro e a descricao do material
                        Console.Write("Erro ao inserir o Material, Mensagem: ");

                        IRfcTable retorno = function.GetTable("IT_RETURN");
                        linha_retorno.SetValue("TYPE", "E");
                        linha_retorno.SetValue("MESSAGE", ex.Message);
                        linha_retorno.SetValue("MESSAGE", "Erro ao inserir o Material: " + material.Descricao);
                        retorno.Insert(linha_retorno);
                    }

                    materialRepository.Salvar(material);
                    String PACOTE = row.GetString("PACOTE");
                    String ERDAT = row.GetString("ERDAT");
                    String ERZET = row.GetString("ERZET");
                }

                IRfcTable retornoSucesso = function.GetTable("IT_RETURN");
                linha_retorno.SetValue("TYPE", "S");
                linha_retorno.SetValue("MESSAGE", "Registros com Sucesso: " + v_cont);
                retornoSucesso.Insert(linha_retorno);
            }


            // SD04 - Inteface de cond. de pag. - Comunicação
            // Funcao - ZFXI_SD04C
            //[RfcServerFunction(Name = "ZGXI_SD04")]
            public static void StfcInterfaceCondPag(RfcServerContext context,
            IRfcFunction function)
            {
                // Exibe no console a interface que será executada
                Console.WriteLine("Received function call {0} from system {1}.", function.Metadata.Name, context.SystemAttributes.SystemID);

                // Mandar salvar o Condicao de Pagamento
                CondicaoPagamentoRepository condicaoPagamentoRepository = new CondicaoPagamentoRepository();

                // Implementa repositorio antes do Foreach para evitar duplicações
                CondicaoPagamento condicaoPagamento = new CondicaoPagamento();

                // Flag da interface que de Limpar tabela de dados
                Char deletar = function.GetChar("I_REFRESH");

                // exibe se o mesmo foi flegado
                Console.WriteLine(deletar);

                // Se estiver espaco em branco na variavel, não limpa a tabela da interface.
                if (deletar != ' ')
                {
                    // Apaga todos os registros da tabela pro_fornecedor
                    IList<CondicaoPagamento> fromDB = condicaoPagamentoRepository.ObterTodos();
                    foreach (CondicaoPagamento dados in fromDB)
                    {
                        condicaoPagamentoRepository.Excluir(dados);
                    }
                }

                // ZTBSD060
                IRfcTable it_condicaoPagamento = function.GetTable("IT_CONDPAG");

                // Implementa Repositorio Rfc de resposta
                RfcRepository rep = context.Repository;

                // RETORNO - BAPIRET2
                RfcStructureMetadata bapiret2 = rep.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno = bapiret2.CreateStructure();

                int v_cont = 0;
                foreach (IRfcStructure row in it_condicaoPagamento)
                {                    
                    condicaoPagamento.Codigo = row.GetString("ZTERM");
                    condicaoPagamento.Descricao = row.GetString("VTEXT");
                    // Pacote
                    condicaoPagamento.pacote = row.GetString("PACOTE");
                    // Data Cricao
                    string v_data_Cricao = row.GetString("ERDAT");
                    condicaoPagamento.data_criacao = Convert.ToDateTime(v_data_Cricao);
                    // Hora de Criacao
                    //string v_hora_Cricao = row.GetString("ERZET");
                    condicaoPagamento.hora_criacao = row.GetString("ERZET");//Convert.ToDateTime(v_hora_Cricao);                   

                    try
                    {
                        v_cont = v_cont + 1;
                        if (deletar == ' ')
                        {
                            IList<CondicaoPagamento> fromDB = condicaoPagamentoRepository.ObterTodosComCampo("Codigo", condicaoPagamento.Codigo);
                            foreach (CondicaoPagamento dados in fromDB)
                            {
                                condicaoPagamentoRepository.Excluir(dados);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Em caso de erro retorna o erro e a descricao do material
                        Console.Write("Erro ao inserir a Condição de Pagamento, Mensagem: ");

                        IRfcTable retorno = function.GetTable("IT_RETURN");
                        linha_retorno.SetValue("TYPE", "E");
                        linha_retorno.SetValue("MESSAGE", ex.Message);
                        linha_retorno.SetValue("MESSAGE", "Erro ao inserir a Condicao de Pagamento: " + condicaoPagamento.Descricao + " - Id: " + condicaoPagamento.Codigo);
                        retorno.Insert(linha_retorno);
                    }

                    condicaoPagamentoRepository.Salvar(condicaoPagamento);
                    String PACOTE = row.GetString("PACOTE");
                    String ERDAT = row.GetString("ERDAT");
                    String ERZET = row.GetString("ERZET");
                }

                IRfcTable retornoSucesso = function.GetTable("IT_RETURN");
                linha_retorno.SetValue("TYPE", "S");
                linha_retorno.SetValue("MESSAGE", "Registros com Sucesso: " + v_cont);
                retornoSucesso.Insert(linha_retorno);
            }

            // SD05 - Inteface de UM - Comunicação
            // Funcao - ZFXI_SD05C
            //[RfcServerFunction(Name = "ZGXI_SD05")]
            public static void StfcInterfaceUM(RfcServerContext context, IRfcFunction function)
            {
                // Exibe no console a interface que será executada
                Console.WriteLine("Received function call {0} from system {1}.", function.Metadata.Name, context.SystemAttributes.SystemID);

                // Mandar salvar o Condicao de Pagamento
                UnidadeMedidaRepository unidadeMedidaRepository = new UnidadeMedidaRepository();

                // Implementa repositorio antes do Foreach para evitar duplicações
                UnidadeMedida unidadeMedida = new UnidadeMedida();

                // Flag da interface que de Limpar tabela de dados
                Char deletar = function.GetChar("I_REFRESH");

                // exibe se o mesmo foi flegado
                Console.WriteLine(deletar);

                // Se estiver espaco em branco na variavel, não limpa a tabela da interface.
                if (deletar != ' ')
                {
                    // Apaga todos os registros da tabela pro_fornecedor
                    IList<UnidadeMedida> fromDB = unidadeMedidaRepository.ObterTodos();
                    foreach (UnidadeMedida dados in fromDB)
                    {
                        unidadeMedidaRepository.Excluir(dados);
                    }
                }

                // ZTBSD063
                IRfcTable it_unidadeMedida = function.GetTable("IT_UM");

                // Implementa Repositorio Rfc de resposta
                RfcRepository rep = context.Repository;

                // RETORNO - BAPIRET2
                RfcStructureMetadata bapiret2 = rep.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno = bapiret2.CreateStructure();

                int v_cont = 0;
                foreach (IRfcStructure row in it_unidadeMedida)
                {                    
                                       
                    unidadeMedida.Descricao        = row.GetString("MSEHL");
                    unidadeMedida.Id_unidademedida = row.GetString("MSEHI");  
                    // Pacote
                    unidadeMedida.pacote           = row.GetString("PACOTE");
                    unidadeMedida.Dimensao         = row.GetString("DIMID");
                    unidadeMedida.Aprestecnica     = row.GetString("MSEH3");
                    // Data Cricao
                    string v_data_Cricao           = row.GetString("ERDAT");
                    unidadeMedida.data_criacao     = Convert.ToDateTime(v_data_Cricao);
                    // Hora de Criacao
                    unidadeMedida.hora_criacao     = row.GetString("ERZET");

                    //Console.WriteLine("CODIGO: " + unidadeMedida.id_unidademedida + " DESCRICAO: " + unidadeMedida.dimensao);               
                    try
                    {
                        v_cont = v_cont + 1;
                        if (deletar == ' ')
                        {
                            IList<UnidadeMedida> fromDB = unidadeMedidaRepository.ObterTodosComCampo("Id_unidademedida", unidadeMedida.Id_unidademedida);
                            foreach (UnidadeMedida dados in fromDB)
                            {
                                unidadeMedidaRepository.Excluir(dados);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Em caso de erro retorna o erro e a descricao do material
                        Console.Write("Erro ao Inserir a Condicao de Pagamento, Mensagem: " + ex);

                        IRfcTable retorno = function.GetTable("IT_RETURN");
                        linha_retorno.SetValue("TYPE", "E");
                        linha_retorno.SetValue("MESSAGE", ex.Message);
                        linha_retorno.SetValue("MESSAGE", "Erro ao inserir a Unidade de Medida: " + unidadeMedida.Descricao + " - Id: " + unidadeMedida.Id_unidademedida);
                        retorno.Insert(linha_retorno);
                    }

                    unidadeMedidaRepository.Salvar(unidadeMedida);
                    String PACOTE = row.GetString("PACOTE");
                    String ERDAT = row.GetString("ERDAT");
                    String ERZET = row.GetString("ERZET");
                }

                IRfcTable retornoSucesso = function.GetTable("IT_RETURN");
                linha_retorno.SetValue("TYPE", "S");
                linha_retorno.SetValue("MESSAGE", "Registros com Sucesso: " + v_cont);
                retornoSucesso.Insert(linha_retorno);
            }           

        }      
        
        public class MyServerConfig : IServerConfiguration
        {
            public RfcConfigParameters GetParameters(String serverName)
            {
                if ("PORTAL_PROGAS".Equals(serverName))
                {
                    RfcConfigParameters parms = new RfcConfigParameters();
                    parms.Add(RfcConfigParameters.GatewayService, "sapgw00");
                    parms.Add(RfcConfigParameters.SAPRouter, "/H/201.35.69.162/S/3299/H/");
                    parms.Add(RfcConfigParameters.GatewayHost, "10.10.0.93");// 10.10.0.94 SAP01
                    parms.Add(RfcConfigParameters.ProgramID, "ServicoPortalInterfaces");
                    parms.Add(RfcConfigParameters.RepositoryDestination, "DEV");
                    parms.Add(RfcConfigParameters.RegistrationCount, "5");
                    return parms;
                }
                else return null;
            }
            // The following two are not used in this example:
            public bool ChangeEventsSupported()
            {
                return false;
            }
            public event RfcServerManager.ConfigurationChangeHandler
            ConfigurationChanged;
        }

        public class MyBackendConfig : IDestinationConfiguration
        {
            public RfcConfigParameters GetParameters(String destinationName)
            {
                if ("DEV".Equals(destinationName))
                {
                    RfcConfigParameters parms = new RfcConfigParameters();
                    parms.Add(RfcConfigParameters.AppServerHost, "10.10.0.93");// "172.16.83.113"
                    parms.Add(RfcConfigParameters.SAPRouter, "/H/201.35.69.162/S/3299/H/");//"/H/200.198.107.10/H/"
                    parms.Add(RfcConfigParameters.SystemNumber, "00");//"10"
                    parms.Add(RfcConfigParameters.SystemID, "DEV");//"D00"
                    parms.Add(RfcConfigParameters.User, "fusion");// "fus_ruy"
                    parms.Add(RfcConfigParameters.Password, "noisuf");//"fusion123"
                    parms.Add(RfcConfigParameters.Client, "400");// "400"                    
                    parms.Add(RfcConfigParameters.PoolSize, "5");//"5"
                    //parms.Add(RfcConfigParameters.IdleTimeout, "600");//"600"

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
