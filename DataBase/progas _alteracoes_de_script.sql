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