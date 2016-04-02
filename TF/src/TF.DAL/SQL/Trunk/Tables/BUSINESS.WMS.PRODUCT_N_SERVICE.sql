/* table to store inventory items (products) */
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