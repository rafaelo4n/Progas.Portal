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

ALTER TABLE pro_vcab ADD IdDaTransportadoraDeRedespachoCif INT

GO

UPDATE pro_vcab set IdDaTransportadoraDeRedespachoCif = 
(
	select pro_id_fornecedor
	from pro_fornecedor f
	where pro_vcab.Transredcif = f.Codigo
)

GO

ALTER TABLE pro_vcab ADD CONSTRAINT FK_pro_vcab_transportadora_redespacho_cif FOREIGN KEY (IdDaTransportadoraDeRedespachoCif) REFERENCES pro_fornecedor( pro_id_fornecedor)

GO

ALTER TABLE pro_vcab DROP COLUMN Transredcif

GO

--transportadora de redespacho cif fim

--vinculo entre fornecedor e usuário
ALTER TABLE Usuario ADD IdDoFornecedor INT
GO
UPDATE Usuario set IdDoFornecedor = 
(
	select pro_id_fornecedor 
	from pro_fornecedor f
	where f.Codigo = Usuario.CodigoFornecedor 
)	
GO
ALTER TABLE Usuario ADD CONSTRAINT FK_Usuario_Fornecedor FOREIGN KEY (IdDoFornecedor) REFERENCES pro_fornecedor (pro_id_fornecedor)
GO
ALTER TABLE Usuario DROP COLUMN CodigoFornecedor

--vinculo entre material e itens do pedido (inicio)
ALTER TABLE pro_vitem ADD pro_id_material INT
GO
UPDATE pro_vitem set pro_id_material = 
(
	select pro_id_material
	from pro_material p
	where p.Id_material = pro_vitem.Id_material 
)	
GO
ALTER TABLE pro_vitem ADD CONSTRAINT FK_Item_Material FOREIGN KEY (pro_id_material) REFERENCES pro_material (pro_id_material)
GO
ALTER TABLE pro_vitem DROP COLUMN Id_material

GO

ALTER TABLE pro_vitem ALTER COLUMN pro_id_material INT NOT NULL

GO

--vinculo entre material e itens do pedido (inicio)

--vinculo entre pedido e incoterm1 (inicio)
ALTER TABLE pro_vcab ADD pro_id_incotermCab INT

GO

update pro_vcab SET pro_id_incotermCab = 
(
	SELECT pro_id_incotermCab
	FROM pro_incotermcab i
	WHERE i.CodigoIncotermCab = pro_vcab.Inco1
)

GO

ALTER TABLE pro_vcab ADD CONSTRAINT FK_PedidoVenda_Incoterm1 FOREIGN KEY (pro_id_incotermCab) REFERENCES pro_incotermcab(pro_id_incotermCab)

GO

ALTER TABLE pro_vcab ALTER COLUMN pro_id_incotermCab INT NOT NULL

GO
ALTER TABLE pro_vcab DROP COLUMN Inco1

GO

--vinculo entre pedido e incoterm1 (fim)
--vinculo entre pedido e incoterm2 (inicio)
ALTER TABLE pro_vcab ADD pro_id_incotermLinha INT

GO

update pro_vcab SET pro_id_incotermLinha = 
(
	SELECT pro_id_incotermLinha
	FROM pro_incotermlinha i
	WHERE i.IncotermLinha = pro_vcab.Inco2
)

GO

ALTER TABLE pro_vcab ADD CONSTRAINT FK_PedidoVenda_Incoterm2 FOREIGN KEY (pro_id_incotermLinha) REFERENCES pro_incotermlinha(pro_id_incotermLinha)

GO

ALTER TABLE pro_vcab ALTER COLUMN pro_id_incotermLinha INT NOT NULL

GO
ALTER TABLE pro_vcab DROP COLUMN Inco2


GO

--vinculo entre pedido e incoterm2 (fim)

--excluir coluna motivo de recusa do cabeçalho
ALTER TABLE pro_vcab DROP COLUMN Motrec

GO

CREATE TABLE MotivoDeRecusa
(
	Codigo NVARCHAR(2) NOT NULL,
	Descricao NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_MotivoDeRecusa PRIMARY KEY (Codigo)
)

GO

INSERT INTO MotivoDeRecusa (Codigo, Descricao) VALUES ('00', 'Substituição do Produto')
INSERT INTO MotivoDeRecusa (Codigo, Descricao) VALUES ('01', 'Desistência do Cliente')
INSERT INTO MotivoDeRecusa (Codigo, Descricao) VALUES ('02', 'Atraso na Entrega')
INSERT INTO MotivoDeRecusa (Codigo, Descricao) VALUES ('03', 'Depósito não Efetuado')
INSERT INTO MotivoDeRecusa (Codigo, Descricao) VALUES ('04', 'Cheque não Enviado')
INSERT INTO MotivoDeRecusa (Codigo, Descricao) VALUES ('05', 'Nota Fiscal Cancelada')
INSERT INTO MotivoDeRecusa (Codigo, Descricao) VALUES ('10', 'Solicitação do cliente não justificada')
INSERT INTO MotivoDeRecusa (Codigo, Descricao) VALUES ('11', 'Saldo de Ordem')
INSERT INTO MotivoDeRecusa (Codigo, Descricao) VALUES ('12', 'Sem definição da Assistência Técnica')
INSERT INTO MotivoDeRecusa (Codigo, Descricao) VALUES ('50', 'Questão está sendo resolvida')
INSERT INTO MotivoDeRecusa (Codigo, Descricao) VALUES ('51', 'Não Liberado pelo financeiro')
INSERT INTO MotivoDeRecusa (Codigo, Descricao) VALUES ('52', 'Cancelamentos Remessas (acerto)')
INSERT INTO MotivoDeRecusa (Codigo, Descricao) VALUES ('53', 'Faturamento Intercompany')
INSERT INTO MotivoDeRecusa (Codigo, Descricao) VALUES ('54', 'Pedido em Duplicidade')
INSERT INTO MotivoDeRecusa (Codigo, Descricao) VALUES ('98', 'Ordem Substituída')
INSERT INTO MotivoDeRecusa (Codigo, Descricao) VALUES ('99', 'Política Comercial (Alçada)')

GO

update pro_vitem set Motrec = null
where Motrec = ''

GO

ALTER TABLE pro_vitem ADD CONSTRAINT FK_PedidoVendaItem_MotivoDeRecusa FOREIGN KEY (Motrec) REFERENCES MotivoDeRecusa (Codigo)

GO

ALTER TABLE pro_vitem ADD Status CHAR(1)

GO

ALTER TABLE pro_vitem DROP COLUMN Id_pedido

GO

 -- ULTIMAS ALTERACOES
ALTER TABLE pro_fornecedor
ADD Grupo_contas nchar(4) null;

GO

ALTER TABLE pro_fornecedor
ADD Codigo_eliminacao nchar(1) null;

GO

ALTER TABLE pro_cliente_vendas
ADD Denominacao nchar(40) null;

GO

CREATE TABLE [dbo].[pro_condicaopreco_cliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Org_vendas] [nvarchar](4) NULL,
	[Can_dist] [nvarchar](2) NULL,
	[Id_cliente] [nchar](10) NULL,
	[Id_material] [nvarchar](18) NULL,
	[NumeroRegistroCondicao] [nvarchar](10) NULL,
	[Montante] [decimal](11, 2) NULL,
	[UnidadeCondicao] [nvarchar](5) NULL,
	[Pacote] [nchar](30) NULL,
	[Data_criacao] [date] NULL,
	[Hora_criacao] [nchar](8) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[pro_condicaopreco_geral](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Org_vendas] [nvarchar](4) NULL,
	[Can_dist] [nvarchar](2) NULL,
	[Id_material] [nvarchar](18) NULL,
	[NumeroRegistroCondicao] [nvarchar](10) NULL,
	[Montante] [decimal](11, 2) NULL,
	[UnidadeCondicao] [nvarchar](5) NULL,
	[Pacote] [nchar](30) NULL,
	[Data_criacao] [date] NULL,
	[Hora_criacao] [nchar](8) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[pro_condicaopreco_regiao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Regiao] [nvarchar](3) NULL,
	[Id_material] [nvarchar](18) NULL,
	[NumeroRegistroCondicao] [nvarchar](10) NULL,
	[Montante] [decimal](11, 2) NULL,
	[UnidadeCondicao] [nvarchar](5) NULL,
	[Pacote] [nchar](30) NULL,
	[Data_criacao] [date] NULL,
	[Hora_criacao] [nchar](8) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]



