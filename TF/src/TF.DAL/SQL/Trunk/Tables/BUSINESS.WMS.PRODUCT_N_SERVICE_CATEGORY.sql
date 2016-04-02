/* table data reflects products in categories (only leaf level available) */
CREATE TABLE [dbo].[BUSINESS.WMS.PRODUCT_N_SERVICE_CATEGORY](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[CATEGORY_GUID] [uniqueidentifier] NOT NULL,
	[ITEM_GUID] [uniqueidentifier] NOT NULL,
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]