using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SAP.Middleware.Connector;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Portal.DadosSap.Entity;
using Portal.DadosSap.Business.Implementation;
using System.ServiceModel;
using System.IO;

namespace Portal.DadosSap
{
    public class Interfaces
    {
        public static void conexaoRFC()
        {
            string nome_rfc = System.Configuration.ConfigurationSettings.AppSettings["nome_RFC"];
            RfcDestinationManager.RegisterDestinationConfiguration(new SAPConnect());
            RfcServerManager.RegisterServerConfiguration(new MyServerConfig());             
            Type[] handlers = new Type[1] { typeof(MyServerHandler) };                         
            RfcServer server = RfcServerManager.GetServer(nome_rfc, handlers);
            server.Start();
        }
         
        public class MyServerHandler // MyServerHandlerExecute
        {
            [RfcServerFunction(Default = true)]
            public static void StfcConnection(RfcServerContext context, IRfcFunction function)
            {
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

                switch (context.FunctionName)
                {
                    case "ZFXI_SD07C":
                        Console.WriteLine("Incoterms");
                        MyServerHandlerExecute.StfcInterfaceIncoterms(context, function);
                        break;
                }

                switch (context.FunctionName)
                {
                    case "ZFXI_SD09C":
                        Console.WriteLine("Precos");
                        MyServerHandlerExecute.StfcInterfacePreco(context, function);
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
                            IList<Cliente> fromDB = clienteRepository.ObterRegistrosUmCampo("Id_cliente", cliente.Id_cliente);
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
                    String ERDAT  = row.GetString("ERDAT");
                    String ERZET  = row.GetString("ERZET");
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
                    clienteVendas.Denominacao  = row.GetString("VKORG_TXT");
                        
                    try
                    {
                        v_cont_vendas = v_cont_vendas + 1;
                        if (deletar == ' ')
                        {
                            IList<ClienteVendas> fromDB = clienteVendasRepository.ObterRegistrosDoisCampos("Id_cliente", clienteVendas.Id_cliente, "Org_vendas", clienteVendas.Org_vendas);
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
                    fornecedor.Grupo_contas = row.GetString("KTOKK");
                    fornecedor.Codigo_eliminacao = row.GetString("LOEVM");      

                    try
                    {
                        v_cont = v_cont + 1;
                        if (deletar == ' ')
                        {
                            IList<Fornecedor> fromDB = fornecedorRepository.ObterRegistrosUmCampo("Codigo", fornecedor.Codigo);
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
                            IList<Material> fromDB = materialRepository.ObterRegistrosUmCampo("Id_material", material.Id_material);
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
                            IList<CondicaoPagamento> fromDB = condicaoPagamentoRepository.ObterRegistrosUmCampo("Codigo", condicaoPagamento.Codigo);
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
                            IList<UnidadeMedida> fromDB = unidadeMedidaRepository.ObterRegistrosUmCampo("Id_unidademedida", unidadeMedida.Id_unidademedida);
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

            // SD07 - Inteface de Incoterms - Comunicação
            // funcao - ZFXI_SD07C
            //[RfcServerFunction(Name = "ZFXI_SD07C")]
            public static void StfcInterfaceIncoterms(RfcServerContext context,
            IRfcFunction function)
            {
                //
                // INCOTERMS - PARTE 1 - CABECALHO
                //

                // Exibe no console a interface que será executada
                Console.WriteLine("Received function call {0} from system {1}.", function.Metadata.Name, context.SystemAttributes.SystemID);

                // Implementa repositorio antes do Foreach para evitar duplicações
                IncotermsCabRepository incotermsCabRepository = new IncotermsCabRepository(); 

                // Implementa repositorio antes do Foreach para evitar duplicações
                IncotermsCab incotermsCab = new IncotermsCab();

                // Flag da interface que de Limpar tabela de dados
                Char deletar = function.GetChar("I_REFRESH");

                // exibe se o mesmo foi flegado
                Console.WriteLine(deletar);

                // Se estiver espaco em branco na variavel, não limpa a tabela da interface.
                if (deletar != ' ')
                {
                    // Apaga todos os registros da tabela pro_fornecedor
                    IList<IncotermsCab> fromDB = incotermsCabRepository.ObterTodos();
                    foreach (IncotermsCab dados in fromDB)
                    {
                        incotermsCabRepository.Excluir(dados);
                    }
                }

                // ZTBSD058
                IRfcTable it_incotermCab = function.GetTable("IT_INCO1");

                // Implementa Repositorio Rfc de resposta
                RfcRepository rep = context.Repository;

                // RETORNO
                RfcStructureMetadata bapiret2 = rep.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno = bapiret2.CreateStructure();

                int v_cont = 0;
                foreach (IRfcStructure row in it_incotermCab)
                {
                    incotermsCab.CodigoIncotermCab = row.GetString("INCO1");
                    incotermsCab.Descricao         = row.GetString("BEZEI");                        
                    incotermsCab.Pacote            = row.GetString("PACOTE");                    
                    string v_data_Cricao           = row.GetString("ERDAT");
                    incotermsCab.Data_criacao      = Convert.ToDateTime(v_data_Cricao);                    
                    incotermsCab.Hora_criacao      = row.GetString("ERZET");

                    try
                    {
                        v_cont = v_cont + 1;
                        if (deletar == ' ')
                        {
                            IList<IncotermsCab> fromDB = incotermsCabRepository.ObterRegistrosUmCampo("CodigoIncotermCab", incotermsCab.CodigoIncotermCab);
                            foreach (IncotermsCab dados in fromDB)
                            {
                                incotermsCabRepository.Excluir(dados);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Em caso de erro retorna o erro
                        Console.Write("Erro ao inserir ao inserir a Incoterm Parte 1, Mensagem: " + ex);

                        IRfcTable retorno = function.GetTable("IT_RETURN");
                        linha_retorno.SetValue("TYPE", "E");
                        linha_retorno.SetValue("MESSAGE", ex.Message);
                        linha_retorno.SetValue("MESSAGE", "Erro ao inserir a Incoterm Parte 1: " + incotermsCab.Descricao + " - Id: " + incotermsCab.CodigoIncotermCab);
                        retorno.Insert(linha_retorno);
                    }

                    incotermsCabRepository.Salvar(incotermsCab);
                    String PACOTE = row.GetString("PACOTE");
                    String ERDAT = row.GetString("ERDAT");
                    String ERZET = row.GetString("ERZET");
                }

                IRfcTable retornoSucesso = function.GetTable("IT_RETURN");
                linha_retorno.SetValue("TYPE", "S");
                linha_retorno.SetValue("MESSAGE", "Registros com Sucesso: " + v_cont);
                retornoSucesso.Insert(linha_retorno);

                // FIM INCOTERMS - PARTE 1 - CABECALHO

                //
                // INCOTERMS - PARTE 2 - LINHAS
                //

                // Implementa repositorio antes do Foreach para evitar duplicações
                IncotermsLinhaRepository incotermsLinhaRepository = new IncotermsLinhaRepository();

                // Implementa repositorio antes do Foreach para evitar duplicações
                IncotermsLinhas incotermsLinhas = new IncotermsLinhas();

                // Se estiver espaco em branco na variavel, não limpa a tabela da interface.
                if (deletar != ' ')
                {
                    // Apaga todos os registros da tabela pro_cliente_vendas
                    IList<IncotermsLinhas> fromDB = incotermsLinhaRepository.ObterTodos();
                    foreach (IncotermsLinhas dados in fromDB)
                    {
                        incotermsLinhaRepository.Excluir(dados);                       
                    }
                }

                // ZTBSD059
                IRfcTable it_incotermLinhas = function.GetTable("IT_INCO2");

                // Implementa Repositorio Rfc de resposta
                RfcRepository repLinhas = context.Repository;

                // RETORNO
                RfcStructureMetadata bapiret2Linha    = rep.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno_inc_linha = bapiret2Linha.CreateStructure();

                int v_cont_linha = 0;
                foreach (IRfcStructure row in it_incotermLinhas)
                {
                    incotermsLinhas.CodigoIncotermCab = row.GetString("INCO1");
                    incotermsLinhas.IncotermLinha     = row.GetString("INCO2");
                    // Pacote
                    incotermsLinhas.Pacote            = row.GetString("PACOTE");
                    // Data Cricao
                    string v_data_Cricao              = row.GetString("ERDAT");
                    incotermsLinhas.Data_criacao      = Convert.ToDateTime(v_data_Cricao);
                    // Hora de Criacao
                    incotermsLinhas.Hora_criacao      = row.GetString("ERZET");

                    try
                    {
                        v_cont_linha = v_cont_linha + 1;
                        if (deletar == ' ')
                        {
                            IList<IncotermsLinhas> fromDB = incotermsLinhaRepository.PesquisaIncotermLinha("CodigoIncotermCab", incotermsLinhas.CodigoIncotermCab, "IncotermLinha", incotermsLinhas.IncotermLinha);
                            foreach (IncotermsLinhas dados in fromDB)
                            {
                                incotermsLinhaRepository.Excluir(dados);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Em caso de erro retorna o erro
                        Console.Write("Erro ao inserir ao inserir a Incoterm Parte 2, Mensagem: " + ex);
                        IRfcTable retorno = function.GetTable("IT_RETURN");
                        linha_retorno_inc_linha.SetValue("TYPE", "E");
                        linha_retorno_inc_linha.SetValue("MESSAGE", ex.Message);
                        linha_retorno_inc_linha.SetValue("MESSAGE", "Erro ao inserir a Incoterm Parte 2: " + incotermsLinhas.IncotermLinha + " - Id: " + incotermsLinhas.CodigoIncotermCab);
                        retorno.Insert(linha_retorno_inc_linha);
                    }

                    incotermsLinhaRepository.Salvar(incotermsLinhas);
                    String PACOTE = row.GetString("PACOTE");
                    String ERDAT = row.GetString("ERDAT");
                    String ERZET = row.GetString("ERZET");
                }

                IRfcTable retornoSucessoLinha = function.GetTable("IT_RETURN");
                linha_retorno_inc_linha.SetValue("TYPE", "S");
                linha_retorno_inc_linha.SetValue("MESSAGE", "Registros com Sucesso: " + v_cont_linha);
                retornoSucessoLinha.Insert(linha_retorno_inc_linha);

                // FIM INCOTERMS
            }

            // SD09 - Inteface de Preco- Comunicação
            // Funcao - ZFXI_SD09C
            //[RfcServerFunction(Name = "ZGXI_SD09")]
            public static void StfcInterfacePreco(RfcServerContext context, IRfcFunction function)
            {
                //
                // PRECOS CLIENTE
                //

                // Exibe no console a interface que será executada
                Console.WriteLine("Received function call {0} from system {1}.", function.Metadata.Name, context.SystemAttributes.SystemID);

                // Mandar salvar o Preco do Cliente
                CondicaoDePrecoClienteRepository condicaoDePrecoClienteRepository = new CondicaoDePrecoClienteRepository();

                // Implementa repositorio antes do Foreach para evitar duplicações
                CondicaoDePrecoCliente condicaoDePrecoCliente = new CondicaoDePrecoCliente();

                // Flag da interface que de Limpar tabela de dados
                Char deletar = function.GetChar("I_REFRESH");

                // exibe se o mesmo foi flegado
                Console.WriteLine(deletar);

                // Se estiver espaco em branco na variavel, não limpa a tabela da interface.
                if (deletar != ' ')
                {
                    // Apaga todos os registros da tabela pro_fornecedor
                    IList<CondicaoDePrecoCliente> fromDB = condicaoDePrecoClienteRepository.ObterTodos();
                    foreach (CondicaoDePrecoCliente dados in fromDB)
                    {
                        condicaoDePrecoClienteRepository.Excluir(dados);
                    }
                }

                // ZTBSD074
                IRfcTable it_condicaoDePrecoCliente = function.GetTable("IT_PRECO_CLIENTE");

                // Implementa Repositorio Rfc de resposta
                RfcRepository repCliente = context.Repository;

                // RETORNO - BAPIRET2
                RfcStructureMetadata bapiret2      = repCliente.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retornoCliente = bapiret2.CreateStructure();

                int v_contCliente = 0;
                foreach (IRfcStructure row in it_condicaoDePrecoCliente)
                {
                    condicaoDePrecoCliente.Org_vendas             = row.GetString("VKORG");
                    condicaoDePrecoCliente.Can_dist               = row.GetString("VTWEG");
                    condicaoDePrecoCliente.Id_cliente             = row.GetString("KUNNR");
                    condicaoDePrecoCliente.Id_material            = row.GetString("MATNR");
                    condicaoDePrecoCliente.NumeroRegistroCondicao = row.GetString("KNUMH");
                    condicaoDePrecoCliente.Montante               = Convert.ToDecimal(row.GetString("KBETR"));
                    condicaoDePrecoCliente.UnidadeCondicao        = row.GetString("KONWA");                   
                    condicaoDePrecoCliente.Pacote                 = row.GetString("PACOTE");
                    condicaoDePrecoCliente.Data_criacao           = Convert.ToDateTime(row.GetString("ERDAT"));                    
                    condicaoDePrecoCliente.Hora_criacao           = row.GetString("ERZET");                             
                    try
                    {
                        v_contCliente = v_contCliente + 1;
                        if (deletar == ' ')
                        {
                            IList<CondicaoDePrecoCliente> fromDB = condicaoDePrecoClienteRepository.ObterRegistrosQuatroCampos("Org_vendas", condicaoDePrecoCliente.Org_vendas,"Can_dist", condicaoDePrecoCliente.Can_dist,"Id_cliente", condicaoDePrecoCliente.Id_cliente, "Id_material", condicaoDePrecoCliente.Id_material);
                            foreach (CondicaoDePrecoCliente dados in fromDB)
                            {
                                condicaoDePrecoClienteRepository.Excluir(dados);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write("Erro ao Inserir o Preco do Cliente, Mensagem: " + ex);
                        IRfcTable retorno = function.GetTable("IT_RETURN");
                        linha_retornoCliente.SetValue("TYPE", "E");
                        linha_retornoCliente.SetValue("MESSAGE", ex.Message);
                        linha_retornoCliente.SetValue("MESSAGE", "Erro ao inserir o Preco do Cliente: " + condicaoDePrecoCliente.Id_cliente + " - Material: " + condicaoDePrecoCliente.Id_material + " - Org.: " + condicaoDePrecoCliente.Org_vendas + " - Canal:" + condicaoDePrecoCliente.Can_dist);
                        retorno.Insert(linha_retornoCliente);
                    }
                    condicaoDePrecoClienteRepository.Salvar(condicaoDePrecoCliente);
                    String PACOTE = row.GetString("PACOTE");
                    String ERDAT = row.GetString("ERDAT");
                    String ERZET = row.GetString("ERZET");
                }

                IRfcTable retornoSucessoCliente = function.GetTable("IT_RETURN");
                linha_retornoCliente.SetValue("TYPE", "S");
                linha_retornoCliente.SetValue("MESSAGE", "Registros com Sucesso(CLIENTE): " + v_contCliente);
                retornoSucessoCliente.Insert(linha_retornoCliente);

                //
                // PRECOS REGIAO
                //

                // Mandar salvar o Preco da Regiao
                CondicaoDePrecoRegiaoRepository condicaoDePrecoRegiaoRepository = new CondicaoDePrecoRegiaoRepository();

                // Implementa repositorio antes do Foreach para evitar duplicações
                CondicaoDePrecoRegiao condicaoDePrecoRegiao = new CondicaoDePrecoRegiao();

                // Se estiver espaco em branco na variavel, não limpa a tabela da interface.
                if (deletar != ' ')
                {
                    // Apaga todos os registros da tabela pro_fornecedor
                    IList<CondicaoDePrecoRegiao> fromDB = condicaoDePrecoRegiaoRepository.ObterTodos();
                    foreach (CondicaoDePrecoRegiao dados in fromDB)
                    {
                        condicaoDePrecoRegiaoRepository.Excluir(dados);
                    }
                }

                // ZTBSD075
                IRfcTable it_condicaoDePrecoRegiao = function.GetTable("IT_PRECO_REGIAO");

                // Implementa Repositorio Rfc de resposta
                RfcRepository repRegiao = context.Repository;

                // RETORNO - BAPIRET2
                RfcStructureMetadata bapiret2Regiao = repRegiao.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retornoRegiao   = bapiret2Regiao.CreateStructure();

                int v_contRegiao = 0;
                foreach (IRfcStructure row in it_condicaoDePrecoRegiao)
                {
                    condicaoDePrecoRegiao.Regiao                 = row.GetString("REGIO");
                    condicaoDePrecoRegiao.Id_material            = row.GetString("MATNR");
                    condicaoDePrecoRegiao.NumeroRegistroCondicao = row.GetString("KNUMH");
                    condicaoDePrecoRegiao.Id_material            = row.GetString("MATNR");
                    condicaoDePrecoRegiao.NumeroRegistroCondicao = row.GetString("KNUMH");
                    condicaoDePrecoRegiao.Montante               = Convert.ToDecimal(row.GetString("KBETR"));
                    condicaoDePrecoRegiao.UnidadeCondicao        = row.GetString("KONWA");          
                    condicaoDePrecoRegiao.Pacote                 = row.GetString("PACOTE");                    
                    condicaoDePrecoRegiao.Data_criacao           = Convert.ToDateTime(row.GetString("ERDAT"));
                    condicaoDePrecoRegiao.Hora_criacao           = row.GetString("ERZET");
                    try
                    {
                        v_contRegiao = v_contRegiao + 1; 
                        if (deletar == ' ')
                        {
                            IList<CondicaoDePrecoRegiao> fromDB = condicaoDePrecoRegiaoRepository.ObterRegistrosDoisCampos("Regiao",condicaoDePrecoRegiao.Regiao, "Id_material", condicaoDePrecoRegiao.Id_material);
                            foreach (CondicaoDePrecoRegiao dados in fromDB)
                            {
                                condicaoDePrecoRegiaoRepository.Excluir(dados);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Em caso de erro retorna o erro e a descricao do material
                        Console.Write("Erro ao Inserir a Condicao de Preco Regiao, Mensagem: " + ex);
                        IRfcTable retorno = function.GetTable("IT_RETURN");
                        linha_retornoRegiao.SetValue("TYPE", "E");
                        linha_retornoRegiao.SetValue("MESSAGE", ex.Message);
                        linha_retornoRegiao.SetValue("MESSAGE", "Erro ao inserir a Condicao de Preco Regiao: " + condicaoDePrecoRegiao.Regiao + " - Material: " + condicaoDePrecoRegiao.Id_material);
                        retorno.Insert(linha_retornoRegiao);
                    }

                    condicaoDePrecoRegiaoRepository.Salvar(condicaoDePrecoRegiao);
                    String PACOTE = row.GetString("PACOTE");
                    String ERDAT = row.GetString("ERDAT");
                    String ERZET = row.GetString("ERZET");
                }

                IRfcTable retornoSucessoRegiao = function.GetTable("IT_RETURN");
                linha_retornoRegiao.SetValue("TYPE", "S");
                linha_retornoRegiao.SetValue("MESSAGE", "Registros com Sucesso(REGIAO): " + v_contRegiao);
                retornoSucessoRegiao.Insert(linha_retornoRegiao);

                //
                // PRECOS GERAL
                //

                // Mandar salvar o Preco da Regiao
                CondicaoDePrecoGeralRepository condicaoDePrecoGeralRepository = new CondicaoDePrecoGeralRepository();

                // Implementa repositorio antes do Foreach para evitar duplicações
                CondicaoDePrecoGeral condicaoDePrecoGeral = new CondicaoDePrecoGeral();

                // Se estiver espaco em branco na variavel, não limpa a tabela da interface.
                if (deletar != ' ')
                {
                    // Apaga todos os registros da tabela pro_fornecedor
                    IList<CondicaoDePrecoGeral> fromDB = condicaoDePrecoGeralRepository.ObterTodos();
                    foreach (CondicaoDePrecoGeral dados in fromDB)
                    {
                        condicaoDePrecoGeralRepository.Excluir(dados);
                    }
                }

                // ZTBSD076
                IRfcTable it_condicaoDePrecoGeral= function.GetTable("IT_PRECO_GERAL");

                // Implementa Repositorio Rfc de resposta
                RfcRepository repGeral = context.Repository;

                // RETORNO - BAPIRET2
                RfcStructureMetadata bapiret2Geral = repGeral.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retornoGeral   = bapiret2Geral.CreateStructure();

                int v_contGeral = 0;
                foreach (IRfcStructure row in it_condicaoDePrecoGeral)
                {
                    condicaoDePrecoGeral.Org_vendas             = row.GetString("VKORG");
                    condicaoDePrecoGeral.Can_dist               = row.GetString("VTWEG");
                    condicaoDePrecoGeral.Id_material            = row.GetString("MATNR");
                    condicaoDePrecoGeral.NumeroRegistroCondicao = row.GetString("KNUMH");
                    condicaoDePrecoGeral.Id_material            = row.GetString("MATNR");
                    condicaoDePrecoGeral.NumeroRegistroCondicao = row.GetString("KNUMH");
                    condicaoDePrecoGeral.Montante               = Convert.ToDecimal(row.GetString("KBETR"));
                    condicaoDePrecoGeral.UnidadeCondicao        = row.GetString("KONWA");
                    condicaoDePrecoGeral.Pacote                 = row.GetString("PACOTE");
                    condicaoDePrecoGeral.Data_criacao           = Convert.ToDateTime(row.GetString("ERDAT"));
                    condicaoDePrecoGeral.Hora_criacao           = row.GetString("ERZET");
                    try
                    {
                        v_contGeral = v_contGeral + 1; 
                        if (deletar == ' ')
                        {
                            IList<CondicaoDePrecoGeral> fromDB = condicaoDePrecoGeralRepository.ObterRegistrosTresCampos("Org_vendas", condicaoDePrecoGeral.Org_vendas, "Can_dist", condicaoDePrecoGeral.Can_dist, "Id_material", condicaoDePrecoGeral.Id_material);
                            foreach (CondicaoDePrecoGeral dados in fromDB)
                            {
                                condicaoDePrecoGeralRepository.Excluir(dados);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Em caso de erro retorna o erro e a descricao do material
                        Console.Write("Erro ao Inserir a Condicao de Preco Geral, Mensagem: " + ex);
                        IRfcTable retorno = function.GetTable("IT_RETURN");
                        linha_retornoRegiao.SetValue("TYPE", "E");
                        linha_retornoRegiao.SetValue("MESSAGE", ex.Message);
                        linha_retornoRegiao.SetValue("MESSAGE", "Erro ao inserir a Condicao de Preco Geral para o Material: " + condicaoDePrecoGeral.Id_material + " - Organização: " + condicaoDePrecoGeral.Org_vendas + " - Canal: " + condicaoDePrecoGeral.Can_dist);
                        retorno.Insert(linha_retornoRegiao);
                    }

                    condicaoDePrecoGeralRepository.Salvar(condicaoDePrecoGeral);
                    String PACOTE = row.GetString("PACOTE");
                    String ERDAT = row.GetString("ERDAT");
                    String ERZET = row.GetString("ERZET");
                }

                IRfcTable retornoSucessoGeral = function.GetTable("IT_RETURN");
                linha_retornoGeral.SetValue("TYPE", "S");
                linha_retornoGeral.SetValue("MESSAGE", "Registros com Sucesso(GERAL): " + v_contGeral);
                retornoSucessoGeral.Insert(linha_retornoGeral);
            }
        }
        
        // Conexão com o SAP, Adiciona todos os parametros necessarios para conexão com o SAP e com o Server da RFC
        public class MyServerConfig : IServerConfiguration
        {           
           public static string gatewayService        = System.Configuration.ConfigurationSettings.AppSettings["GatewayService"];
           public static string saprouter             = System.Configuration.ConfigurationSettings.AppSettings["SAPRouter"];
           public static string gatewayhost           = System.Configuration.ConfigurationSettings.AppSettings["GatewayHost"];
           public static string programid             = System.Configuration.ConfigurationSettings.AppSettings["ProgramID"];
           public static string repositorydestination = System.Configuration.ConfigurationSettings.AppSettings["RepositoryDestination"];
           public static string registrationcount     = System.Configuration.ConfigurationSettings.AppSettings["RegistrationCount"];

            public RfcConfigParameters GetParameters(String serverName)
            {
                if ("PORTAL_PROGAS".Equals(serverName))
                {
                    RfcConfigParameters parms = new RfcConfigParameters();
                    parms.Add(RfcConfigParameters.GatewayService, gatewayService);
                    parms.Add(RfcConfigParameters.SAPRouter, saprouter);
                    parms.Add(RfcConfigParameters.GatewayHost, gatewayhost);
                    parms.Add(RfcConfigParameters.ProgramID, programid);
                    parms.Add(RfcConfigParameters.RepositoryDestination, repositorydestination);
                    parms.Add(RfcConfigParameters.RegistrationCount, registrationcount);
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

        public class SAPConnect : IDestinationConfiguration
        {
            public static string appserverhost  = System.Configuration.ConfigurationSettings.AppSettings["AppServerHost"];
            public static string saprouter      = System.Configuration.ConfigurationSettings.AppSettings["SAPRouter"];
            public static string systemnumber   = System.Configuration.ConfigurationSettings.AppSettings["SystemNumber"];
            public static string systemid       = System.Configuration.ConfigurationSettings.AppSettings["SystemID"];
            public static string user           = System.Configuration.ConfigurationSettings.AppSettings["User"];
            public static string password       = System.Configuration.ConfigurationSettings.AppSettings["Password"];
            public static string client         = System.Configuration.ConfigurationSettings.AppSettings["Client"];
            public static string poolsize       = System.Configuration.ConfigurationSettings.AppSettings["PoolSize"];
            public static string repositorydestination = System.Configuration.ConfigurationSettings.AppSettings["RepositoryDestination"];

            public RfcConfigParameters GetParameters(String destinationName)
            {
                if (destinationName == repositorydestination)
                {
                    RfcConfigParameters parms = new RfcConfigParameters();
                    parms.Add(RfcConfigParameters.AppServerHost, appserverhost);
                    parms.Add(RfcConfigParameters.SAPRouter, saprouter);
                    parms.Add(RfcConfigParameters.SystemNumber, systemnumber);
                    parms.Add(RfcConfigParameters.SystemID, systemid);
                    parms.Add(RfcConfigParameters.User, user);
                    parms.Add(RfcConfigParameters.Password, password);
                    parms.Add(RfcConfigParameters.Client, client);                  
                    parms.Add(RfcConfigParameters.PoolSize, poolsize);
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
