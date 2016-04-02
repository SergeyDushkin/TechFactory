/* table to store list of entities */
CREATE TABLE [dbo].[SYSTEM.ENTITY](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[KEY] [nvarchar](50) NOT NULL,
	[NAME] [nvarchar](200),
	[PARENT_GUID] [uniqueidentifier],					-- to build relashinship between entites, in future it will be hepfull for example to have integration with different data providers
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]