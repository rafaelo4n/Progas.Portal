﻿using System.Linq;
using Progas.Portal.Domain.Entities;
using Progas.Portal.Infra.Repositories.Contracts;
using System;

namespace Progas.Portal.Infra.Repositories.Implementations
{
    public class PedidosVenda : CompleteRepositoryNh<PedidoVenda>, IPedidosVenda
    {
        public PedidosVenda(IUnitOfWorkNh unitOfWork) : base(unitOfWork)
        {
        }

        public IPedidosVenda ClienteCodigoContendo(string filtroCodigo)
        {
            if (!string.IsNullOrEmpty(filtroCodigo))
            {
                Query = Query.Where(x => x.Cliente.Id_cliente.ToLower().Contains(filtroCodigo.ToLower()));
            }

            return this;
        }

        // DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
        public IPedidosVenda DataCriacaoContendo(string filtroDataCriacao)
        {
            if (!string.IsNullOrEmpty(filtroDataCriacao))
            {                
                DateTime data = Convert.ToDateTime(filtroDataCriacao); 
                Query = Query.Where(x => x.Datacp.Date == data);
            }

            return this;
        }

        public IPedidosVenda PedidoCodigoContendo(string filtroPedido)
        {
            if (!string.IsNullOrEmpty(filtroPedido))
            {
                Query = Query.Where(x => x.NumeroDoPedidoDoRepresentante.Contains(filtroPedido));
            }

            return this;
        }

        public IPedidosVenda DataPedidoContendo(string filtroDataPedido)
        {
            if (!string.IsNullOrEmpty(filtroDataPedido))
            {
                DateTime data = Convert.ToDateTime(filtroDataPedido);
                Query = Query.Where(x => x.Datap.Date == data);
            }

            return this;
        }

        public PedidoVenda CotacaoPedidoContendo(string filtroCotacao, string representante)
        {
            return Query.SingleOrDefault(x => x.Id_cotacao == filtroCotacao && x.Representante.Codigo == representante);
        }

        public IPedidosVenda CotacaoRepresentante(string id_representante)
        {   
            if (!string.IsNullOrEmpty(id_representante))
            {
                Query = Query.Where(x => x.Representante.Codigo.Contains(id_representante)) ;
            }
            return this;
        }

        public IPedidosVenda FiltraPorId(string idDaCotacao)
        {
            Query = Query.Where(x => x.Id_cotacao == idDaCotacao);
            return this;
        }

        public IPedidosVenda ContendoMaterial(int idDoMaterial)
        {
            //Query = Query.Where(pedido => pedido.Itens.Select(item => item.Material.pro_id_material).Contains(idDoMaterial));
            Query = Query.Where(pedido => pedido.Itens.Any(item => item.Material.pro_id_material == idDoMaterial));
            return this;
        }

        public IPedidosVenda DoCliente(string codigoDoCliente)
        {
            Query = Query.Where(pedido => pedido.Cliente.Id_cliente == codigoDoCliente);
            return this;
        }

        public IPedidosVenda NoStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                Query = Query.Where(pedido => pedido.Status.Codigo == status);
            }

            return this;
        }

        public IPedidosVenda OrdenarPeloUltimoPedidoCriado()
        {
            Query = Query.OrderByDescending(x => x.Datacp);
            return this;
        }
    }
}
