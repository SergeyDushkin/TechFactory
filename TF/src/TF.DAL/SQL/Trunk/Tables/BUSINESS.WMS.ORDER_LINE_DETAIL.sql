/* table to store details for order line */
CREATE TABLE [BUSINESS.WMS.ORDER_LINE_DETAIL](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[ORDER_GUID] [uniqueidentifier] NOT NULL,
	[ORDER_LINE_GUID] [uniqueidentifier] NOT NULL,
	[PRIORITY] [smallint] NOT NULL,		
	[ITEM_GUID] [uniqueidentifier] NOT NULL,
	[LOCATION_GUID] [uniqueidentifier] NOT NULL,			-- where to hold an item
	[UOM_GUID] [uniqueidentifier] NOT NULL,
	[QTY] FLOAT NOT NULL,
	[BASE_QTY] FLOAT NOT NULL,
	[NUMBER] nvarchar(50) NOT NULL,							-- could be Serial Number of the item
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]