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

                switch (context.FunctionName)
                {
                    case "ZFXI_SD08C":
                        Console.WriteLine("Pedido");
                        MyServerHandlerExecute.StfcInterfacePedido(context, function);
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
                // Flag da interface que de Limpar tabela de dados
                Char deletar = function.GetChar("I_REFRESH");
                // exibe se o mesmo foi flegado 
                Console.WriteLine(deletar);

                // Implementa repositorio antes do Foreach para evitar duplicações
                ClienteRepository clienteRepository = new ClienteRepository();
                Cliente cliente = new Cliente();
                ClienteVendasRepository clienteVendasRepository = new ClienteVendasRepository();
                ClienteVendas clienteVendas = new ClienteVendas();
                ClienteCondicaoLiberadaRepository clienteCondicaoLiberadaRepository = new ClienteCondicaoLiberadaRepository();
                ClienteCondicaoLiberada clienteCondicaoLiberada = new ClienteCondicaoLiberada();
                ClienteTransportadoraLiberadaRepository clienteTransportadoraLiberadaRepository = new ClienteTransportadoraLiberadaRepository();
                ClienteTransportadoraLiberada clienteTransportadoraLiberada = new ClienteTransportadoraLiberada();

                // ZTBSD056 - ZTBXI_101
                IRfcTable it_cliente = function.GetTable("IT_CLIENTE");

                // Implementa Repositorio Rfc de resposta
                RfcRepository rep = context.Repository;

                // RETORNO
                RfcStructureMetadata bapiret2 = rep.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno = bapiret2.CreateStructure();


                // ZTBSD057 - ZTBXI_101
                IRfcTable it_cliente_vendas = function.GetTable("IT_CLIENTE_AV");

                // Implementa Repositorio Rfc de resposta
                RfcRepository repVendas = context.Repository;

                // RETORNO
                RfcStructureMetadata bapiret2Vendas = repVendas.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno_vendas = bapiret2Vendas.CreateStructure();


                // ZTBSD058 - ZTBXI_101
                IRfcTable it_cliente_condicao = function.GetTable("IT_CLIENTE_CP");

                // Implementa Repositorio Rfc de resposta
                RfcRepository repcondicao = context.Repository;

                // RETORNO
                RfcStructureMetadata bapiret2Condicao = repcondicao.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno_condicao  = bapiret2Condicao.CreateStructure();

                
                // ZTBSD085 - ZTBXI_101
                IRfcTable it_cliente_trans_lib = function.GetTable("IT_CLIENTE_TL");

                // Implementa Repositorio Rfc de resposta
                RfcRepository repclienteTrans = context.Repository;

                // RETORNO
                RfcStructureMetadata bapiret2Trans = repclienteTrans.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno_trans  = bapiret2Trans.CreateStructure();

                // Se estiver espaco em branco na variavel, não limpa a tabela da interface.
                if (deletar != ' ')
                {
                    int count = 0;
                    // Se a interface de cliente estiver marcada para Reiniciar "X" marca os registros das 3 tabebas como Eliminados.
                    IList<Cliente> fromDBcliente = clienteRepository.ObterTodos();
                    IList<ClienteVendas> fromDBclienteV = clienteVendasRepository.ObterTodos();
                    IList<ClienteCondicaoLiberada> fromDBclienteCond = clienteCondicaoLiberadaRepository.ObterTodos();

                    foreach (Cliente dados in fromDBcliente)
                    {
                        count = count + 1;
                        if (count == 1)
                        {
                            foreach (IRfcStructure row in it_cliente)
                            {
                                dados.Pacote = row.GetString("PACOTE");
                                dados.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                                dados.Hora_criacao = row.GetString("ERZET");
                                break;
                            }
                        }
                        dados.Eliminacao = "X";
                        clienteRepository.Alterar(dados);
                    }

                    count = 0;
                    foreach (ClienteVendas dados in fromDBclienteV)
                    {
                        count = count + 1;
                        if (count == 1)
                        {
                            foreach (IRfcStructure row in it_cliente_vendas)
                            {
                                dados.Pacote = row.GetString("PACOTE");
                                dados.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                                dados.Hora_criacao = row.GetString("ERZET");
                                break;
                            }
                        }
                        dados.Eliminacao = "X";
                        clienteVendasRepository.Alterar(dados);
                    }

                    count = 0;
                    foreach (ClienteCondicaoLiberada dados in fromDBclienteCond)
                    {
                        count = count + 1;
                        if (count == 1)
                        {
                            foreach (IRfcStructure row in it_cliente_condicao)
                            {
                                dados.Pacote = row.GetString("PACOTE");
                                dados.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                                dados.Hora_criacao = row.GetString("ERZET");
                                break;
                            }
                        }
                        dados.Eliminacao = "X";
                        clienteCondicaoLiberadaRepository.Alterar(dados);
                    }
                }

                int v_cont = 0;
                foreach (IRfcStructure row in it_cliente)
                {
                    cliente.Id_cliente = row.GetString("KUNNR");
                    cliente.Nome = row.GetString("NAME1");
                    cliente.Cpf = row.GetString("STCD2");
                    cliente.Cnpj = row.GetString("STCD1");
                    cliente.Nr_ie_cli = row.GetString("STCD3");
                    cliente.Cep = row.GetString("POST_CODE");
                    cliente.Endereco = row.GetString("STREET");
                    cliente.Numero = row.GetString("HOUSE_NUM1");
                    cliente.Complemento = row.GetString("HOUSE_NUM2");
                    cliente.Municipio = row.GetString("CITY1");
                    cliente.Bairro = row.GetString("CITY2");
                    cliente.Uf = row.GetString("UF");
                    cliente.Pais = row.GetString("COUNTRY");
                    cliente.Tel_res = row.GetString("TELF1");
                    cliente.Tel_cel = row.GetString("TELF2");
                    cliente.Fax = row.GetString("TELFX");
                    cliente.Email = row.GetString("EMAIL");
                    cliente.Pacote = row.GetString("PACOTE");
                    cliente.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                    cliente.Hora_criacao = row.GetString("ERZET");
                    cliente.Eliminacao = row.GetString("LOEVM");

                    // Obtem todas as condicoes do banco para o cliente que esta sendo processado
                    IList<ClienteCondicaoLiberada> fromCondicao = clienteCondicaoLiberadaRepository.ObterRegistrosUmCampo("Id_cliente", cliente.Id_cliente);
                    // Atualiza para Eliminado todas as condicoes do cliente
                    foreach (ClienteCondicaoLiberada dados in fromCondicao)
                    {
                        dados.Pacote = row.GetString("PACOTE");
                        dados.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                        dados.Hora_criacao = row.GetString("ERZET");
                        dados.Eliminacao = "X";
                        clienteCondicaoLiberadaRepository.Alterar(dados);
                    }

                    v_cont = v_cont + 1;
                    try
                    {
                        // Se o registro existir, ele é atualizado, se não ele é inserido.                                                            
                        IList<Cliente> fromDB = clienteRepository.ObterRegistrosUmCampo("Id_cliente", cliente.Id_cliente);
                        if (fromDB.Count == 0)
                        {
                            clienteRepository.Salvar(cliente);
                        }
                        else
                        {
                            clienteRepository.Alterar(cliente);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Em caso de erro retorna o erro
                        Console.Write("Erro ao inserir o Cliente, Mensagem: " + ex);
                        IRfcTable retorno = function.GetTable("IT_RETURN");
                        linha_retorno.SetValue("TYPE", "E");
                        linha_retorno.SetValue("MESSAGE", ex.Message);
                        linha_retorno.SetValue("MESSAGE", "Erro ao inserir o Cliente Dados Gerais: " + cliente.Nome + " - Id: " + cliente.Id_cliente);
                        retorno.Insert(linha_retorno);
                    }
                }

                // FIM CLIENTE

                //
                // CLIENTE VENDAS
                //                                

                int v_cont_vendas = 0;
                foreach (IRfcStructure row in it_cliente_vendas)
                {
                    clienteVendas.Id_cliente = row.GetString("KUNNR");
                    clienteVendas.Org_vendas = row.GetString("VKORG");
                    clienteVendas.Can_dist = row.GetString("VTWEG");
                    clienteVendas.Set_ativ = row.GetString("SPART");
                    clienteVendas.Grupo_cli = row.GetString("KDGRP");
                    clienteVendas.Id_fornecedor = row.GetString("LIFNR");
                    clienteVendas.Denominacao = row.GetString("VKORG_TXT");
                    clienteVendas.Pacote = row.GetString("PACOTE");
                    clienteVendas.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                    clienteVendas.Hora_criacao = row.GetString("ERZET");
                    clienteVendas.Eliminacao = row.GetString("LOEVM");

                    v_cont_vendas = v_cont_vendas + 1;
                    try
                    {
                        IList<ClienteVendas> fromDB = clienteVendasRepository.ObterRegistrosQuatroCampos("Id_cliente", clienteVendas.Id_cliente, "Org_vendas", clienteVendas.Org_vendas, "Can_dist", clienteVendas.Can_dist, "Set_ativ", clienteVendas.Set_ativ);
                        if (fromDB.Count == 0)
                        {
                            clienteVendasRepository.Salvar(clienteVendas);
                        }
                        else
                        {
                            foreach (ClienteVendas dados in fromDB)
                            {
                                clienteVendas.pro_id_cliente_vendas = dados.pro_id_cliente_vendas;
                            }
                            clienteVendasRepository.Alterar(clienteVendas);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Em caso de erro retorna o erro
                        Console.Write("Erro ao inserir o Cliente Vendas, Mensagem: " + ex);
                        IRfcTable retorno = function.GetTable("IT_RETURN");
                        linha_retorno_vendas.SetValue("TYPE", "E");
                        linha_retorno_vendas.SetValue("MESSAGE", ex.Message);
                        linha_retorno_vendas.SetValue("MESSAGE", "Erro ao inserir o Cliente Área de Vendas: " + cliente.Nome + " - Id: " + cliente.Id_cliente);
                        retorno.Insert(linha_retorno_vendas);
                    }
                }

                // FIM CLIENTE VENDAS

                //
                // CLIENTE CONDICAO
                //                                

                int v_cont_condicao = 0;
                foreach (IRfcStructure row in it_cliente_condicao)
                {
                    clienteCondicaoLiberada.Id_cliente        = row.GetString("KUNNR");
                    clienteCondicaoLiberada.Chave_condicao    = row.GetString("ZTERM");
                    clienteCondicaoLiberada.Data_fim_condicao = Convert.ToDateTime(row.GetString("DATBI"));
                    clienteCondicaoLiberada.Pacote            = row.GetString("PACOTE");
                    clienteCondicaoLiberada.Data_criacao      = Convert.ToDateTime(row.GetString("ERDAT"));
                    clienteCondicaoLiberada.Hora_criacao      = row.GetString("ERZET");

                    v_cont_condicao = v_cont_condicao + 1;
                    try
                    {
                        IList<ClienteCondicaoLiberada> fromDB = clienteCondicaoLiberadaRepository.ObterRegistrosDoisCampos("Id_cliente", clienteCondicaoLiberada.Id_cliente, "Chave_condicao", clienteCondicaoLiberada.Chave_condicao);
                        if (fromDB.Count == 0)
                        {
                            clienteCondicaoLiberadaRepository.Salvar(clienteCondicaoLiberada);
                        }
                        else
                        {
                            clienteCondicaoLiberadaRepository.Alterar(clienteCondicaoLiberada);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Em caso de erro retorna o erro
                        Console.Write("Erro ao inserir a Condicao do Cliente, Mensagem: " + ex);
                        IRfcTable retorno = function.GetTable("IT_RETURN");
                        linha_retorno_condicao.SetValue("TYPE", "E");
                        linha_retorno_condicao.SetValue("MESSAGE", ex.Message);
                        linha_retorno_condicao.SetValue("MESSAGE", "Erro ao inserir o Cliente Condições Pagto " + clienteCondicaoLiberada.Id_cliente + " - Condicao: " + clienteCondicaoLiberada.Chave_condicao);
                        retorno.Insert(linha_retorno_condicao);
                    }
                }
                // FIM CLIENTE CONDICAO


                //
                // CLIENTE TRANSPORTADORAS LIBERADAS
                //                                

                int v_cont_tras_lib = 0;
                foreach (IRfcStructure row in it_cliente_trans_lib)
                {
                    clienteTransportadoraLiberada.Id_cliente          = row.GetString("KUNNR");
                    clienteTransportadoraLiberada.Funcao_parceiro     = row.GetString("PARVW");
                    clienteTransportadoraLiberada.Numero_agente_frete = row.GetString("TDLNR");
                    String v_padrao                                   = row.GetString("PADRAO");
                    if (v_padrao != "")
                    {
                        clienteTransportadoraLiberada.Padrao = Convert.ToBoolean(1);
                    }
                    clienteTransportadoraLiberada.Pacote       = row.GetString("PACOTE");
                    clienteTransportadoraLiberada.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                    clienteTransportadoraLiberada.Hora_criacao = row.GetString("ERZET");

                    v_cont_tras_lib = v_cont_tras_lib + 1;
                    try
                    {
                        IList<ClienteTransportadoraLiberada> fromDB = clienteTransportadoraLiberadaRepository.ObterRegistrosDoisCampos("Id_cliente", clienteTransportadoraLiberada.Id_cliente, "Numero_agente_frete", clienteTransportadoraLiberada.Numero_agente_frete);
                        if (fromDB.Count == 0)
                        {
                            clienteTransportadoraLiberadaRepository.Salvar(clienteTransportadoraLiberada);
                        }
                        else
                        {
                            foreach (ClienteTransportadoraLiberada dados in fromDB)
                            {
                                clienteTransportadoraLiberada.Id = dados.Id;
                            }
                            clienteTransportadoraLiberadaRepository.Alterar(clienteTransportadoraLiberada);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Em caso de erro retorna o erro
                        Console.Write("Erro ao inserir a Tranpostadora do Cliente, Mensagem: " + ex);
                        IRfcTable retorno = function.GetTable("IT_RETURN");
                        linha_retorno_trans.SetValue("TYPE", "E");
                        linha_retorno_trans.SetValue("MESSAGE", ex.Message);
                        linha_retorno_trans.SetValue("MESSAGE", "Erro ao inserir a Tranpostadora do Cliente " + clienteTransportadoraLiberada.Id_cliente + " - Numero Agente Frete: " + clienteTransportadoraLiberada.Numero_agente_frete);
                        retorno.Insert(linha_retorno_trans);
                    }
                }
                // FIM CLIENTE TRANSPORTADORAS LIBERADAS

                // Envia o retorno dos registros inseridos com sucesso Cliente Transportadoras
                IRfcTable retornoSucessoTrans = function.GetTable("IT_RETURN");
                linha_retorno_trans.SetValue("TYPE", "S");
                linha_retorno_trans.SetValue("MESSAGE", "Registros com Sucesso Tranpostadora Cliente: " + v_cont_tras_lib);
                retornoSucessoTrans.Insert(linha_retorno_trans);         

                // Envia o retorno dos registros inseridos com sucesso Condicoes Pagto
                IRfcTable retornoSucessoCondicao = function.GetTable("IT_RETURN");
                linha_retorno_condicao.SetValue("TYPE", "S");
                linha_retorno_condicao.SetValue("MESSAGE", "Registros com Sucesso Condições Pagto: " + v_cont_condicao);
                retornoSucessoCondicao.Insert(linha_retorno_condicao);

                // Envia o retorno dos registros inseridos com sucesso Area de Vendas
                IRfcTable retornoSucessoVendas = function.GetTable("IT_RETURN");
                linha_retorno_vendas.SetValue("TYPE", "S");
                linha_retorno_vendas.SetValue("MESSAGE", "Registros com Sucesso Área de Vendas: " + v_cont_vendas);
                retornoSucessoVendas.Insert(linha_retorno_vendas);

                // Envia o retorno dos registros inseridos com sucesso Geral
                IRfcTable retornoSucesso = function.GetTable("IT_RETURN");
                linha_retorno.SetValue("TYPE", "S");
                linha_retorno.SetValue("MESSAGE", "Registros com Sucesso Dados Gerais: " + v_cont);
                retornoSucesso.Insert(linha_retorno);
            }

            // SD02 - Inteface de fornecedor - Comunicação
            // funcao - ZFXI_SD02C
            //[RfcServerFunction(Name = "ZGXI_SD02")]
            public static void StfcInterfaceFornecedor(RfcServerContext context,
            IRfcFunction function)
            {
                // Exibe no console a interface que será executada
                Console.WriteLine("Received function call {0} from system {1}.", function.Metadata.Name, context.SystemAttributes.SystemID);
                // Flag da interface que de Limpar tabela de dados
                Char deletar = function.GetChar("I_REFRESH");
                // exibe se o mesmo foi flegado
                Console.WriteLine(deletar);

                // Implementa Repositório dos dados
                FornecedorRepository fornecedorRepository = new FornecedorRepository();
                Fornecedor fornecedor = new Fornecedor();
                FornecedorEmpresaRepository fornecedorEmpresaRepository = new FornecedorEmpresaRepository();
                FornecedorEmpresa fornecedorEmpresa = new FornecedorEmpresa();

                FornecedorTransportadoraLiberadaRepository fornecedorTransportadoraLiberadaRepository = new FornecedorTransportadoraLiberadaRepository();
                FornecedorTransportadoraLiberada fornecedorTransportadoraLiberada = new FornecedorTransportadoraLiberada();


                // ZTBSD060
                IRfcTable it_fornecedor = function.GetTable("IT_FORNECEDOR");

                // Implementa Repositorio Rfc de resposta
                RfcRepository rep = context.Repository;

                // RETORNO - BAPIRET2
                RfcStructureMetadata bapiret2 = rep.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno = bapiret2.CreateStructure();

                // ZTBSD079
                IRfcTable it_fornecedor_emp = function.GetTable("IT_FORNECEDOR_EMP");

                // Implementa Repositorio Rfc de resposta
                RfcRepository repEmp = context.Repository;

                // RETORNO - BAPIRET2
                RfcStructureMetadata bapiret2emp = repEmp.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno_emp = bapiret2emp.CreateStructure();

                // ZTBSD086 - ZTBXI_101
                IRfcTable it_fornecedor_trans_lib = function.GetTable("IT_FORNECEDOR_TL");

                // Implementa Repositorio Rfc de resposta
                RfcRepository repfornecedorTrans = context.Repository;

                // RETORNO
                RfcStructureMetadata bapiret2Trans = repfornecedorTrans.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno_trans = bapiret2Trans.CreateStructure();

                // Se a interface de Fornecedor estiver marcada para Reiniciar "X" marca os registros das 2 tabebas como Eliminados.
                if (deletar != ' ')
                {
                    // Apaga todos os registros da tabela pro_fornecedor
                    IList<Fornecedor> fromDB = fornecedorRepository.ObterTodos();
                    IList<FornecedorEmpresa> fromDBemp = fornecedorEmpresaRepository.ObterTodos();
                    foreach (Fornecedor dados in fromDB)
                    {
                        foreach (IRfcStructure row in it_fornecedor)
                        {
                            dados.Pacote = row.GetString("PACOTE");
                            dados.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                            dados.Hora_criacao = row.GetString("ERZET");
                        }
                        dados.Eliminacao = "X";
                        fornecedorRepository.Alterar(dados);
                    }

                    foreach (FornecedorEmpresa dados in fromDBemp)
                    {
                        foreach (IRfcStructure row in it_fornecedor_emp)
                        {
                            dados.Pacote = row.GetString("PACOTE");
                            dados.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                            dados.Hora_criacao = row.GetString("ERZET");
                        }
                        dados.Eliminacao = "X";
                        fornecedorEmpresaRepository.Alterar(dados);
                    }
                }

                //
                // FORNECEDOR
                //                 

                //Char deletar = function.GetChar("I_REFRESH");                              

                int v_cont = 0;
                foreach (IRfcStructure row in it_fornecedor)
                {
                    fornecedor.Codigo = row.GetString("LIFNR");
                    fornecedor.Nome = row.GetString("NAME1");
                    fornecedor.Cpf = row.GetString("STCD2");
                    fornecedor.Cnpj = row.GetString("STCD1");
                    fornecedor.Nr_ie_for = row.GetString("STCD3");
                    fornecedor.Cep = row.GetString("POST_CODE");
                    fornecedor.Endereco = row.GetString("STREET");
                    fornecedor.Numero = row.GetString("HOUSE_NUM1");
                    fornecedor.Municipio = row.GetString("CITY1");
                    fornecedor.Bairro = row.GetString("CITY2");
                    fornecedor.Uf = row.GetString("UF");
                    fornecedor.Pais = row.GetString("COUNTRY");
                    fornecedor.Tel_res = row.GetString("TELF1");
                    fornecedor.Tel_res = row.GetString("TELF2");
                    fornecedor.Tel_cel = row.GetString("TELF1");
                    fornecedor.Fax = row.GetString("TELFX");
                    fornecedor.Email = row.GetString("EMAIL");
                    fornecedor.Grupo_contas = row.GetString("KTOKK");
                    fornecedor.Pacote = row.GetString("PACOTE");
                    fornecedor.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                    fornecedor.Hora_criacao = row.GetString("ERZET");
                    fornecedor.Eliminacao = row.GetString("LOEVM");

                    v_cont = v_cont + 1;
                    try
                    {
                        IList<Fornecedor> fromDB = fornecedorRepository.ObterRegistrosUmCampo("Codigo", fornecedor.Codigo);
                        if (fromDB.Count == 0)
                        {
                            fornecedorRepository.Salvar(fornecedor);
                        }
                        else
                        {
                            fornecedorRepository.Alterar(fornecedor);
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
                }
               
                //
                // FIM FORNECEDOR

                //
                // FORNECEDOR EMPRESA
                //                 

                int v_cont_emp = 0;
                foreach (IRfcStructure row in it_fornecedor_emp)
                {
                    fornecedorEmpresa.Empresa = row.GetString("BUKRS");
                    fornecedorEmpresa.Codigo = row.GetString("LIFNR");
                    fornecedorEmpresa.Pacote = row.GetString("PACOTE");
                    fornecedorEmpresa.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                    fornecedorEmpresa.Hora_criacao = row.GetString("ERZET");
                    fornecedorEmpresa.Eliminacao = row.GetString("LOEVM");

                    v_cont_emp = v_cont_emp + 1;
                    try
                    {
                        IList<FornecedorEmpresa> fromDB = fornecedorEmpresaRepository.ObterRegistrosDoisCampos("Empresa", fornecedorEmpresa.Empresa, "Codigo", fornecedorEmpresa.Codigo);
                        if (fromDB.Count == 0)
                        {
                            fornecedorEmpresaRepository.Salvar(fornecedorEmpresa);
                        }
                        else
                        {
                            fornecedorEmpresaRepository.Alterar(fornecedorEmpresa);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write("Erro ao inserir o Fornecedor Empresa, Mensagem:" + ex);
                        IRfcTable retorno = function.GetTable("IT_RETURN");
                        linha_retorno_emp.SetValue("TYPE", "E");
                        linha_retorno_emp.SetValue("MESSAGE", ex.Message);
                        linha_retorno_emp.SetValue("MESSAGE", "Erro ao inserir o Forn. Empresa: " + fornecedorEmpresa.Empresa + "Fornecedor: " + fornecedorEmpresa.Codigo);
                        retorno.Insert(linha_retorno_emp);
                    }
                }

                //
                // FIM FORNECEDOR EMPRESA

                //
                // FORNECEDOR TRANSPORTADORAS LIBERADAS
                //                                

                int v_cont_tras_lib = 0;
                foreach (IRfcStructure row in it_fornecedor_trans_lib)
                {
                    fornecedorTransportadoraLiberada.Codigo = row.GetString("LIFNR");
                    fornecedorTransportadoraLiberada.Funcao_parceiro = row.GetString("PARVW");
                    fornecedorTransportadoraLiberada.Numero_agente_frete = row.GetString("TDLNR");
                    String v_padrao = row.GetString("PADRAO");
                    if (v_padrao != "")
                    {
                        fornecedorTransportadoraLiberada.Padrao = Convert.ToBoolean(1);
                    }
                    fornecedorTransportadoraLiberada.Pacote = row.GetString("PACOTE");
                    fornecedorTransportadoraLiberada.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                    fornecedorTransportadoraLiberada.Hora_criacao = row.GetString("ERZET");

                    v_cont_tras_lib = v_cont_tras_lib + 1;
                    try
                    {
                        IList<FornecedorTransportadoraLiberada> fromDB = fornecedorTransportadoraLiberadaRepository.ObterRegistrosDoisCampos("Codigo", fornecedorTransportadoraLiberada.Codigo, "Numero_agente_frete", fornecedorTransportadoraLiberada.Numero_agente_frete);
                        if (fromDB.Count == 0)
                        {
                            fornecedorTransportadoraLiberadaRepository.Salvar(fornecedorTransportadoraLiberada);
                        }
                        else
                        {
                            foreach (FornecedorTransportadoraLiberada dados in fromDB)
                            {
                                fornecedorTransportadoraLiberada.Id = dados.Id;
                            }
                            fornecedorTransportadoraLiberadaRepository.Alterar(fornecedorTransportadoraLiberada);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Em caso de erro retorna o erro
                        Console.Write("Erro ao inserir a Tranpostadora do Fornecedor, Mensagem: " + ex);
                        IRfcTable retorno = function.GetTable("IT_RETURN");
                        linha_retorno_trans.SetValue("TYPE", "E");
                        linha_retorno_trans.SetValue("MESSAGE", ex.Message);
                        linha_retorno_trans.SetValue("MESSAGE", "Erro ao inserir a Tranpostadora do Fornecedor " + fornecedorTransportadoraLiberada.Codigo + " - Numero Agente Frete: " + fornecedorTransportadoraLiberada.Numero_agente_frete);
                        retorno.Insert(linha_retorno_trans);
                    }
                }
                // FIM FORNECEDOR TRANSPORTADORAS LIBERADAS


                IRfcTable retornoSucesso = function.GetTable("IT_RETURN");
                linha_retorno.SetValue("TYPE", "S");
                linha_retorno.SetValue("MESSAGE", "Registros com Sucesso Fornecedor: " + v_cont);
                retornoSucesso.Insert(linha_retorno);

                IRfcTable retornoSucessoEmp = function.GetTable("IT_RETURN");
                linha_retorno_emp.SetValue("TYPE", "S");
                linha_retorno_emp.SetValue("MESSAGE", "Registros com Sucesso Forn. Empresa: " + v_cont_emp);
                retornoSucessoEmp.Insert(linha_retorno_emp);
            }

            // SD03 - Inteface de materiais - Comunicação
            // funcao - ZFXI_SD03C
            //[RfcServerFunction(Name = "ZFXI_SD03C")]
            public static void StfcInterfaceMaterial(RfcServerContext context, IRfcFunction function)
            {
                // Exibe no console a interface que será executada
                Console.WriteLine("Received function call {0} from system {1}.", function.Metadata.Name, context.SystemAttributes.SystemID);

                // Flag da interface que de Limpar tabela de dados
                Char deletar = function.GetChar("I_REFRESH");

                // exibe se o mesmo foi flegado
                Console.WriteLine(deletar);

                // Implementa Repositório dos dados
                MaterialRepository materialRepository = new MaterialRepository();
                Material material = new Material();

                IRfcTable it_material = function.GetTable("IT_MATERIAL");

                // Implementa Repositorio Rfc de resposta
                RfcRepository rep = context.Repository;

                // RETORNO - BAPIRET2
                RfcStructureMetadata bapiret2 = rep.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno = bapiret2.CreateStructure();

                // Se estiver espaco em branco na variavel, não limpa a tabela da interface.
                if (deletar != ' ')
                {
                    int count = 0;
                    // Apaga todos os registros da tabela pro_fornecedor
                    IList<Material> fromDB = materialRepository.ObterTodos();
                    foreach (Material dados in fromDB)
                    {
                        count = count + 1;
                        if (count == 1)
                        {
                            foreach (IRfcStructure row in it_material)
                            {
                                dados.Pacote = row.GetString("PACOTE");
                                dados.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                                dados.Hora_criacao = row.GetString("ERZET");
                                break;
                            }
                        }
                        dados.Eliminacao = "X";
                        materialRepository.Alterar(dados);
                    }
                }

                int v_cont = 0;
                foreach (IRfcStructure row in it_material)
                {
                    material.Id_material = row.GetString("MATNR");
                    material.Descricao = row.GetString("MAKTX");
                    material.Id_centro = row.GetString("WERKS");
                    material.Tip_mat = row.GetString("MTART");
                    material.Status_mat = row.GetString("MSTAE");
                    material.Uni_med = row.GetString("MEINS");
                    material.Status_mat = row.GetString("MSTAE");
                    material.Pacote = row.GetString("PACOTE");
                    material.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                    material.Hora_criacao = row.GetString("ERZET");
                    material.Eliminacao = row.GetString("LVORM");

                    v_cont = v_cont + 1;
                    try
                    {
                        IList<Material> fromDB = materialRepository.ObterRegistrosDoisCampos("Id_material", material.Id_material, "Id_centro", material.Id_centro);
                        if (fromDB.Count == 0)
                        {
                            materialRepository.Salvar(material);
                        }
                        else
                        {
                            foreach (Material dados in fromDB)
                            {
                                material.pro_id_material = dados.pro_id_material;
                            }
                            materialRepository.Alterar(material);
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
                }
                IRfcTable retornoSucesso = function.GetTable("IT_RETURN");
                linha_retorno.SetValue("TYPE", "S");
                linha_retorno.SetValue("MESSAGE", "Registros com Sucesso Material: " + v_cont);
                retornoSucesso.Insert(linha_retorno);
            }


            // SD04 - Inteface de cond. de pag. - Comunicação
            // Funcao - ZFXI_SD04C
            //[RfcServerFunction(Name = "ZGXI_SD04")]
            public static void StfcInterfaceCondPag(RfcServerContext context, IRfcFunction function)
            {
                // Exibe no console a interface que será executada
                Console.WriteLine("Received function call {0} from system {1}.", function.Metadata.Name, context.SystemAttributes.SystemID);

                // Flag da interface que de Limpar tabela de dados
                Char deletar = function.GetChar("I_REFRESH");

                // exibe se o mesmo foi flegado
                Console.WriteLine(deletar);

                // Mandar salvar o Condicao de Pagamento
                CondicaoPagamentoRepository condicaoPagamentoRepository = new CondicaoPagamentoRepository();

                // Implementa repositorio antes do Foreach para evitar duplicações
                CondicaoPagamento condicaoPagamento = new CondicaoPagamento();

                // ZTBSD060
                IRfcTable it_condicaoPagamento = function.GetTable("IT_CONDPAG");

                // Implementa Repositorio Rfc de resposta
                RfcRepository rep = context.Repository;

                // RETORNO - BAPIRET2
                RfcStructureMetadata bapiret2 = rep.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno = bapiret2.CreateStructure();

                // Se estiver espaco em branco na variavel, não limpa a tabela da interface.
                if (deletar != ' ')
                {
                    // Apaga todos os registros da tabela pro_fornecedor
                    IList<CondicaoPagamento> fromDB = condicaoPagamentoRepository.ObterTodos();
                    int count = 0;
                    foreach (CondicaoPagamento dados in fromDB)
                    {
                        count = count + 1;
                        if (count == 1)
                        {
                            foreach (IRfcStructure row in it_condicaoPagamento)
                            {
                                dados.pacote = row.GetString("PACOTE");
                                dados.data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                                dados.hora_criacao = row.GetString("ERZET");
                                break;
                            }
                        }
                        dados.Eliminacao = "X";
                        condicaoPagamentoRepository.Alterar(dados);
                    }
                }

                int v_cont = 0;
                foreach (IRfcStructure row in it_condicaoPagamento)
                {
                    condicaoPagamento.Codigo = row.GetString("ZTERM");
                    condicaoPagamento.Descricao = row.GetString("VTEXT");
                    condicaoPagamento.pacote = row.GetString("PACOTE");
                    condicaoPagamento.data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                    condicaoPagamento.hora_criacao = row.GetString("ERZET");
                    // condicaoPagamento.Eliminacao   = row.GetString("LOEVM");

                    v_cont = v_cont + 1;
                    try
                    {
                        IList<CondicaoPagamento> fromDB = condicaoPagamentoRepository.ObterRegistrosUmCampo("Codigo", condicaoPagamento.Codigo);
                        if (fromDB.Count == 0)
                        {
                            condicaoPagamentoRepository.Salvar(condicaoPagamento);
                        }
                        else
                        {
                            condicaoPagamentoRepository.Alterar(condicaoPagamento); ;
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
                }
                IRfcTable retornoSucesso = function.GetTable("IT_RETURN");
                linha_retorno.SetValue("TYPE", "S");
                linha_retorno.SetValue("MESSAGE", "Registros com Sucesso Condicao de Pagamento: " + v_cont);
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
                    unidadeMedida.Descricao = row.GetString("MSEHL");
                    unidadeMedida.Id_unidademedida = row.GetString("MSEHI");
                    unidadeMedida.pacote = row.GetString("PACOTE");
                    unidadeMedida.Dimensao = row.GetString("DIMID");
                    unidadeMedida.Aprestecnica = row.GetString("MSEH3");
                    unidadeMedida.data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                    unidadeMedida.hora_criacao = row.GetString("ERZET");

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
                        Console.Write("Erro ao Inserir a Unidade de Medida, Mensagem: " + ex);
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
                linha_retorno.SetValue("MESSAGE", "Registros com Sucesso Unidade de Medida: " + v_cont);
                retornoSucesso.Insert(linha_retorno);


                // Pedido 




            }

            // SD07 - Inteface de Incoterms - Comunicação
            // funcao - ZFXI_SD07C
            //[RfcServerFunction(Name = "ZFXI_SD07C")]
            public static void StfcInterfaceIncoterms(RfcServerContext context,
            IRfcFunction function)
            {
                // Exibe no console a interface que será executada
                Console.WriteLine("Received function call {0} from system {1}.", function.Metadata.Name, context.SystemAttributes.SystemID);

                // Flag da interface que de Limpar tabela de dados
                Char deletar = function.GetChar("I_REFRESH");

                // exibe se o mesmo foi flegado
                Console.WriteLine(deletar);

                // Implementa repositorio antes do Foreach para evitar duplicações
                IncotermsCabRepository incotermsCabRepository = new IncotermsCabRepository();
                IncotermsCab incotermsCab = new IncotermsCab();
                IncotermsLinhaRepository incotermsLinhaRepository = new IncotermsLinhaRepository();
                IncotermsLinhas incotermsLinhas = new IncotermsLinhas();

                // ZTBSD058
                IRfcTable it_incotermCab = function.GetTable("IT_INCO1");

                // Implementa Repositorio Rfc de resposta
                RfcRepository rep = context.Repository;

                // RETORNO
                RfcStructureMetadata bapiret2 = rep.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno = bapiret2.CreateStructure();

                // ZTBSD059
                IRfcTable it_incotermLinhas = function.GetTable("IT_INCO2");

                // Implementa Repositorio Rfc de resposta
                RfcRepository repLinhas = context.Repository;

                // RETORNO
                RfcStructureMetadata bapiret2Linha = rep.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno_inc_linha = bapiret2Linha.CreateStructure();

                // Se estiver espaco em branco na variavel, não limpa a tabela da interface.
                if (deletar != ' ')
                {
                    IList<IncotermsCab> fromDB = incotermsCabRepository.ObterTodos();
                    IList<IncotermsLinhas> fromDBlinha = incotermsLinhaRepository.ObterTodos();

                    int count = 0;
                    foreach (IncotermsCab dados in fromDB)
                    {
                        count = count + 1;
                        if (count == 1)
                        {
                            foreach (IRfcStructure row in it_incotermCab)
                            {
                                dados.Pacote       = row.GetString("PACOTE");
                                dados.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                                dados.Hora_criacao = row.GetString("ERZET");
                                break;
                            }
                        }
                        dados.Eliminacao = "X";
                        incotermsCabRepository.Alterar(dados);
                    }

                    count = 0;
                    foreach (IncotermsLinhas dados in fromDBlinha)
                    {
                        count = count + 1;
                        if (count == 1)
                        {
                            foreach (IRfcStructure row in it_incotermLinhas)
                            {
                                dados.Pacote = row.GetString("PACOTE");
                                dados.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                                dados.Hora_criacao = row.GetString("ERZET");
                                break;
                            }
                        }
                        dados.Eliminacao = "X";
                        incotermsLinhaRepository.Alterar(dados);
                    }
                }

                //
                // INCOTERMS - PARTE 1 - CABECALHO
                //

                int v_cont = 0;
                foreach (IRfcStructure row in it_incotermCab)
                {
                    incotermsCab.CodigoIncotermCab = row.GetString("INCO1");
                    incotermsCab.Descricao         = row.GetString("BEZEI");
                    incotermsCab.Pacote            = row.GetString("PACOTE");
                    incotermsCab.Data_criacao      = Convert.ToDateTime(row.GetString("ERDAT"));
                    incotermsCab.Hora_criacao      = row.GetString("ERZET");
                    //incotermsCab.Eliminacao        = row.GetString("LOEVM"); row.GetString("LOEVM"); Falta acrescentar esse campo no ABAP

                    // Obtem todas as Incoterms parte 2 do Codigo da Incoterm Parte 1
                    IList<IncotermsLinhas> fromLinha = incotermsLinhaRepository.ObterRegistrosUmCampo("CodigoIncotermCab", incotermsCab.CodigoIncotermCab);
                    // Atualiza para Eliminado todas as condicoes do cliente
                    foreach (IncotermsLinhas dados in fromLinha)
                    {
                        dados.Pacote = row.GetString("PACOTE");
                        dados.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                        dados.Hora_criacao = row.GetString("ERZET");
                        dados.Eliminacao = "X";
                        incotermsLinhaRepository.Alterar(dados);
                    }

                    v_cont = v_cont + 1;
                    try
                    {
                        IList<IncotermsCab> fromDB = incotermsCabRepository.ObterRegistrosUmCampo("CodigoIncotermCab", incotermsCab.CodigoIncotermCab);
                        if (fromDB.Count == 0)
                        {
                            incotermsCabRepository.Salvar(incotermsCab);
                        }
                        else
                        {
                            foreach (IncotermsCab dados in fromDB)
                            {
                                incotermsCab.pro_id_incotermCab = dados.pro_id_incotermCab;
                            }
                            incotermsCabRepository.Alterar(incotermsCab);
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
                }

                IRfcTable retornoSucesso = function.GetTable("IT_RETURN");
                linha_retorno.SetValue("TYPE", "S");
                linha_retorno.SetValue("MESSAGE", "Registros com Sucesso Incoterm - Parte 1: " + v_cont);
                retornoSucesso.Insert(linha_retorno);

                // FIM INCOTERMS - PARTE 1 - CABECALHO

                //
                // INCOTERMS - PARTE 2 - LINHAS
                //                                      

                int v_cont_linha = 0;
                foreach (IRfcStructure row in it_incotermLinhas)
                {
                    incotermsLinhas.CodigoIncotermCab = row.GetString("INCO1");
                    incotermsLinhas.IncotermLinha     = row.GetString("INCO2");
                    incotermsLinhas.Pacote            = row.GetString("PACOTE");
                    incotermsLinhas.Data_criacao      = Convert.ToDateTime(row.GetString("ERDAT"));
                    incotermsLinhas.Hora_criacao      = row.GetString("ERZET");
                    String v_parc_redesp_cif          = row.GetString("PARC_REDESP_CIF");
                    String v_parc_redesp_fob          = row.GetString("PARC_REDESP_FOB");

                    if (v_parc_redesp_cif != "")
                    {
                        incotermsLinhas.parc_redesp_cif = Convert.ToBoolean(1);
                    }

                    if (v_parc_redesp_fob != "")
                    {
                        incotermsLinhas.parc_redesp_fob = Convert.ToBoolean(1);
                    }

                    v_cont_linha = v_cont_linha + 1;
                    try
                    {
                        IList<IncotermsLinhas> fromDB = incotermsLinhaRepository.PesquisaIncotermLinha("CodigoIncotermCab", incotermsLinhas.CodigoIncotermCab, "IncotermLinha", incotermsLinhas.IncotermLinha);
                        if (fromDB.Count == 0)
                        {
                            incotermsLinhaRepository.Salvar(incotermsLinhas);
                        }
                        else
                        {
                            foreach (IncotermsLinhas dados in fromDB)
                            {
                                incotermsLinhas.pro_id_incotermLinha = dados.pro_id_incotermLinha;
                            }
                            incotermsLinhaRepository.Alterar(incotermsLinhas);
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
                }

                IRfcTable retornoSucessoLinha = function.GetTable("IT_RETURN");
                linha_retorno_inc_linha.SetValue("TYPE", "S");
                linha_retorno_inc_linha.SetValue("MESSAGE", "Registros com Sucesso Incoterm - Parte 2: " + v_cont_linha);
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
                RfcStructureMetadata bapiret2 = repCliente.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retornoCliente = bapiret2.CreateStructure();

                int v_contCliente = 0;
                foreach (IRfcStructure row in it_condicaoDePrecoCliente)
                {
                    condicaoDePrecoCliente.Org_vendas = row.GetString("VKORG");
                    condicaoDePrecoCliente.Can_dist = row.GetString("VTWEG");
                    condicaoDePrecoCliente.Id_cliente = row.GetString("KUNNR");
                    condicaoDePrecoCliente.Id_material = row.GetString("MATNR");
                    condicaoDePrecoCliente.NumeroRegistroCondicao = row.GetString("KNUMH");
                    condicaoDePrecoCliente.Montante = Convert.ToDecimal(row.GetString("KBETR"));
                    condicaoDePrecoCliente.UnidadeCondicao = row.GetString("KONWA");
                    condicaoDePrecoCliente.Pacote = row.GetString("PACOTE");
                    condicaoDePrecoCliente.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                    condicaoDePrecoCliente.Hora_criacao = row.GetString("ERZET");
                    try
                    {
                        v_contCliente = v_contCliente + 1;
                        if (deletar == ' ')
                        {
                            IList<CondicaoDePrecoCliente> fromDB = condicaoDePrecoClienteRepository.ObterRegistrosQuatroCampos("Org_vendas", condicaoDePrecoCliente.Org_vendas, "Can_dist", condicaoDePrecoCliente.Can_dist, "Id_cliente", condicaoDePrecoCliente.Id_cliente, "Id_material", condicaoDePrecoCliente.Id_material);
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
                IRfcStructure linha_retornoRegiao = bapiret2Regiao.CreateStructure();

                int v_contRegiao = 0;
                foreach (IRfcStructure row in it_condicaoDePrecoRegiao)
                {
                    condicaoDePrecoRegiao.Regiao = row.GetString("REGIO");
                    condicaoDePrecoRegiao.Id_material = row.GetString("MATNR");
                    condicaoDePrecoRegiao.NumeroRegistroCondicao = row.GetString("KNUMH");
                    condicaoDePrecoRegiao.Id_material = row.GetString("MATNR");
                    condicaoDePrecoRegiao.NumeroRegistroCondicao = row.GetString("KNUMH");
                    condicaoDePrecoRegiao.Montante = Convert.ToDecimal(row.GetString("KBETR"));
                    condicaoDePrecoRegiao.UnidadeCondicao = row.GetString("KONWA");
                    condicaoDePrecoRegiao.Pacote = row.GetString("PACOTE");
                    condicaoDePrecoRegiao.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                    condicaoDePrecoRegiao.Hora_criacao = row.GetString("ERZET");
                    try
                    {
                        v_contRegiao = v_contRegiao + 1;
                        if (deletar == ' ')
                        {
                            IList<CondicaoDePrecoRegiao> fromDB = condicaoDePrecoRegiaoRepository.ObterRegistrosDoisCampos("Regiao", condicaoDePrecoRegiao.Regiao, "Id_material", condicaoDePrecoRegiao.Id_material);
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
                IRfcTable it_condicaoDePrecoGeral = function.GetTable("IT_PRECO_GERAL");

                // Implementa Repositorio Rfc de resposta
                RfcRepository repGeral = context.Repository;

                // RETORNO - BAPIRET2
                RfcStructureMetadata bapiret2Geral = repGeral.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retornoGeral = bapiret2Geral.CreateStructure();

                int v_contGeral = 0;
                foreach (IRfcStructure row in it_condicaoDePrecoGeral)
                {
                    condicaoDePrecoGeral.Org_vendas = row.GetString("VKORG");
                    condicaoDePrecoGeral.Can_dist = row.GetString("VTWEG");
                    condicaoDePrecoGeral.Id_material = row.GetString("MATNR");
                    condicaoDePrecoGeral.NumeroRegistroCondicao = row.GetString("KNUMH");
                    condicaoDePrecoGeral.Id_material = row.GetString("MATNR");
                    condicaoDePrecoGeral.NumeroRegistroCondicao = row.GetString("KNUMH");
                    condicaoDePrecoGeral.Montante = Convert.ToDecimal(row.GetString("KBETR"));
                    condicaoDePrecoGeral.UnidadeCondicao = row.GetString("KONWA");
                    condicaoDePrecoGeral.Pacote = row.GetString("PACOTE");
                    condicaoDePrecoGeral.Data_criacao = Convert.ToDateTime(row.GetString("ERDAT"));
                    condicaoDePrecoGeral.Hora_criacao = row.GetString("ERZET");
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

            // SD08 - Inteface de Pedido - Comunicação
            // funcao - ZFXI_SD08C
            //[RfcServerFunction(Name = "ZFXI_SD08C")]
            public static void StfcInterfacePedido(RfcServerContext context, IRfcFunction function)
            {
                // Exibe no console a interface que será executada
                Console.WriteLine("Received function call {0} from system {1}.", function.Metadata.Name, context.SystemAttributes.SystemID);

                Char teste = function.GetChar("E_STATUS");

                string status = function.GetString("E_STATUS");

                // Implementa repositorio antes do Foreach para evitar duplicações
                PedidoVendaRepository pedidoVendaRepository = new PedidoVendaRepository();
                PedidoVenda pedidoCabecalho = new PedidoVenda();
                PedidoVendaLinhaRepository pedidoVendaLinhaRepository = new PedidoVendaLinhaRepository();
                PedidoVendaLinha pedidoLinha = new PedidoVendaLinha();

                // ZSTSD011
                IRfcTable it_pedidoLinhas = function.GetTable("TE_ITEM");

                // Implementa Repositorio Rfc de resposta
                RfcRepository rep = context.Repository;

                // RETORNO
                RfcStructureMetadata bapiret2 = rep.GetStructureMetadata("BAPIRET2");
                IRfcStructure linha_retorno = bapiret2.CreateStructure();

                if (status != "")
                {
                    //
                    // PEDIDO LINHAS
                    //

                    int v_cont = 0;
                    foreach (IRfcStructure row in it_pedidoLinhas)
                    {
                        pedidoLinha.Id_cotacao = row.GetString("COTACAO");
                        pedidoLinha.Id_item = row.GetString("POSNR");
                        pedidoLinha.MotivoDeRecusa = row.GetString("ABGRU");

                        // Atualiza o Status do Cabecalho do Pedido
                        v_cont = v_cont + 1;
                        try
                        {
                            // Obtem a Linha do Pedido que será atualizada
                            IList<PedidoVenda> fromCabecalho = pedidoVendaRepository.ObterRegistrosUmCampo("Id_cotacao", pedidoLinha.Id_cotacao);
                            // Atualiza o status da Linha

                            foreach (PedidoVenda dados in fromCabecalho)
                            {
                                dados.Status = Convert.ToString(function.GetChar("E_STATUS"));
                                pedidoVendaRepository.Alterar(dados);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Em caso de erro retorna o erro
                            Console.Write("Erro ao Atualizar o Status do Cabecalho do Pedido: " + ex);
                            IRfcTable retorno = function.GetTable("TI_RETURN");
                            linha_retorno.SetValue("TYPE", "E");
                            linha_retorno.SetValue("MESSAGE", ex.Message);
                            linha_retorno.SetValue("MESSAGE", "Erro ao Atualizar o Status do Cabecalho do Pedido: " + pedidoLinha.Id_cotacao);
                            retorno.Insert(linha_retorno);
                        }

                        //Atualiza o Status e o Motivo Recusa da Linha do Pedido
                        try
                        {
                            // Obtem a Linha do Pedido que será atualizada
                            IList<PedidoVendaLinha> fromLinha = pedidoVendaLinhaRepository.ObterRegistrosDoisCampos("Id_cotacao", pedidoLinha.Id_cotacao, "Id_item", pedidoLinha.Id_item);

                            // Atualiza o status da Linha
                            foreach (PedidoVendaLinha dados in fromLinha)
                            {
                                //dados.Status = pedidoLinha.Status;
                                if (pedidoLinha.MotivoDeRecusa != "")
                                {
                                    dados.MotivoDeRecusa = pedidoLinha.MotivoDeRecusa;
                                }
                                else
                                {
                                    dados.MotivoDeRecusa = null;
                                }
                                pedidoVendaLinhaRepository.Alterar(dados);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Em caso de erro retorna o erro
                            Console.Write("Erro ao atualizar o Status da Linha do Pedido: " + ex);
                            IRfcTable retorno = function.GetTable("TI_RETURN");
                            linha_retorno.SetValue("TYPE", "E");
                            linha_retorno.SetValue("MESSAGE", ex.Message);
                            linha_retorno.SetValue("MESSAGE", "Erro ao Atualizar o Status da Linha do Pedido: " + pedidoLinha.Id_cotacao + " - Na linha: " + pedidoLinha.Id_item);
                            retorno.Insert(linha_retorno);
                        }
                    }

                    IRfcTable retornoSucesso = function.GetTable("TI_RETURN");
                    linha_retorno.SetValue("TYPE", "S");
                    linha_retorno.SetValue("MESSAGE", "Registros atualizados com Sucesso Pedido Vendas: " + v_cont);
                    retornoSucesso.Insert(linha_retorno);
                    // FIM PEDIDO LINHAS
                }
                else
                {
                    IRfcTable retornoErro = function.GetTable("TI_RETURN");
                    linha_retorno.SetValue("TYPE", "E");
                    linha_retorno.SetValue("MESSAGE", "Status recebido inválido");
                    retornoErro.Insert(linha_retorno);
                    return;
                }
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
