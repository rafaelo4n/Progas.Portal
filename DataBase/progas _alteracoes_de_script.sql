ALTER TABLE pro_vcab ADD pro_id_cliente_vendas int

GO

ALTER TABLE pro_vcab ADD CONSTRAINT FK_cliente_vendas_vcab FOREIGN KEY
(pro_id_cliente_vendas) REFERENCES pro_cliente_vendas(pro_id_cliente_vendas)
