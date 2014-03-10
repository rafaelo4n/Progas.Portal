ALTER TABLE pro_vcab ADD pro_id_cliente_vendas int

GO

ALTER TABLE pro_vcab ADD CONSTRAINT FK_cliente_vendas_vcab FOREIGN KEY
(pro_id_cliente_vendas) REFERENCES pro_cliente_vendas(pro_id_cliente_vendas)

GO

CREATE TABLE pro_item_condicaopreco
(
	Id  INT NOT NULL IDENTITY,
	pro_id_item INT NOT NULL,
	Nivel VARCHAR(3) NOT NULL,
	Tipo VARCHAR(4)  not null,
	Base decimal(15,2)  not null,
	Montante decimal(11,2) not null,
	Valor decimal (13,2) not null,
	constraint PK_pro_item_condicaopreco PRIMARY KEY (Id),
	constraint FK_pro_item_condicaopreco FOREIGN KEY (pro_id_item) REFERENCES  pro_vitem(pro_id_item)

)

GO


ALTER TABLE pro_vcab
ALTER COLUMN Inco2 nvarchar(60)

GO

ALTER TABLE pro_vitem ADD CONSTRAINT FK_pro_vitem FOREIGN KEY (Id_cotacao) REFERENCES pro_vcab(id_cotacao)

GO


ALTER TABLE pro_vcab ADD pro_id_cliente INT

GO

UPDATE pro_vcab set pro_id_cliente = 
(
	select pro_id_cliente
	from pro_cliente c
	where pro_vcab.Id_cliente = c.Id_cliente
)

GO

ALTER TABLE pro_vcab ALTER COLUMN pro_id_cliente INT NOT NULL;

GO

ALTER TABLE pro_vcab ADD CONSTRAINT FK_pro_vcab_pro_cliente FOREIGN KEY (pro_id_cliente) REFERENCES pro_cliente( pro_id_cliente)

GO

ALTER TABLE pro_vcab DROP COLUMN Id_cliente

GO

--transportadora inicio

ALTER TABLE pro_vcab ADD IdDaTransportadora INT

GO

UPDATE pro_vcab set IdDaTransportadora = 
(
	select pro_id_fornecedor
	from pro_fornecedor f
	where pro_vcab.Trans = f.Codigo
)

GO

ALTER TABLE pro_vcab ADD CONSTRAINT FK_pro_vcab_transportadora FOREIGN KEY (IdDaTransportadora) REFERENCES pro_fornecedor( pro_id_fornecedor)

GO

ALTER TABLE pro_vcab DROP COLUMN Trans

GO

--transportadora fim


--transportadora de redespacho inicio

ALTER TABLE pro_vcab ADD IdDaTransportadoraDeRedespacho INT

GO

UPDATE pro_vcab set IdDaTransportadoraDeRedespacho = 
(
	select pro_id_fornecedor
	from pro_fornecedor f
	where pro_vcab.Transred = f.Codigo
)

GO

ALTER TABLE pro_vcab ADD CONSTRAINT FK_pro_vcab_transportadora_redespacho FOREIGN KEY (IdDaTransportadoraDeRedespacho) REFERENCES pro_fornecedor( pro_id_fornecedor)

GO

ALTER TABLE pro_vcab DROP COLUMN Transred

GO

--transportadora de redespacho fim

--transportadora de redespacho cif inicio

ALTER TABLE pro_vcab ADD IdDaTransportadora INT

GO

UPDATE pro_vcab set IdDaTransportadora = 
(
	select pro_id_fornecedor
	from pro_fornecedor f
	where pro_vcab.Trans = f.Codigo
)

GO

ALTER TABLE pro_vcab ADD CONSTRAINT FK_pro_vcab_transportadora FOREIGN KEY (IdDaTransportadora) REFERENCES pro_fornecedor( pro_id_fornecedor)

GO

ALTER TABLE pro_vcab DROP COLUMN Trans

GO

--transportadora de redespacho cif fim



