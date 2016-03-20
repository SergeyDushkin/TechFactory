/* table to store trees data (parent-child) */
CREATE TABLE [dbo].[BUSINESS.CATEGORY_TREE](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[KEY] [nvarchar](50) NOT NULL,
	[NAME] [nvarchar](200),
	[PARENT_GUID] [uniqueidentifier],
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[BUSINESS.WMS.PRODUCT_N_SERVICE](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[TYPE] [nvarchar](20) NOT NULL,						-- fixed values: REGULAR, KIT, DYNAKIT, NONSTOCK, INGREDIENT
	[KEY] [nvarchar](50) NOT NULL,
	[NAME] [nvarchar](200),
	[UOM_GUID] [uniqueidentifier] NOT NULL,
	[ALLOW_NEGATIVE_QTY] BIT NOT NULL,					-- ALLOW NEGATIVE ISSUES
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]

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

/* table data reflects products in categories (only leaf level available) */
CREATE TABLE [dbo].[BUSINESS.WMS.PRODUCT_N_SERVICE_CATEGORY](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[CATEGORY_GUID] [uniqueidentifier] NOT NULL,
	[ITEM_GUID] [uniqueidentifier] NOT NULL,
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]
