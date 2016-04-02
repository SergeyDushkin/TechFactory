/* table to store prices of products and services */
CREATE TABLE [dbo].[BUSINESS.WMS.PRICE](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[ITEM_GUID] [uniqueidentifier] NOT NULL,
	[CURRENCY_GUID] [uniqueidentifier] NOT NULL,
	[LOCATION_GUID] [uniqueidentifier],					-- NULL means that price applying for all locations
	[PRICE] FLOAT NOT NULL,
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]