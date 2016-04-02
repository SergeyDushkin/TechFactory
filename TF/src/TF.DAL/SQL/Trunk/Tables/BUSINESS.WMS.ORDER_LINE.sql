/* table to store order lines */
CREATE TABLE [BUSINESS.WMS.ORDER_LINE](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[ORDER_GUID] [uniqueidentifier] NOT NULL,
	[PRIORITY] [smallint] NOT NULL,			
	[ITEM_GUID] [uniqueidentifier] NOT NULL,
	[UOM_GUID] [uniqueidentifier] NOT NULL,
	[QTY] FLOAT NOT NULL,
	[BASE_QTY] FLOAT NOT NULL,
	[PRICE] FLOAT NOT NULL,
	[BASE_PRICE] FLOAT NOT NULL,
	[AMOUNT] FLOAT NOT NULL,
	[BASE_AMOUNT] FLOAT NOT NULL,
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]