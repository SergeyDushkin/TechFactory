/*  table contains specification for a kit items */
CREATE TABLE [dbo].[BUSINESS.WMS.KIT_SPEC](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[PARENT_GUID] [uniqueidentifier] NOT NULL,			-- reference to the parent product/service
	[CHILD_GUID] [uniqueidentifier] NOT NULL,			-- reference to the child product/service (ingredient)
	[CHILD_UOM_GUID] [uniqueidentifier] NOT NULL,		-- UOM of child product
	[BASE_QTY] FLOAT NOT NULL,									
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]