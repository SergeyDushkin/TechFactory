/* table to store contact information (header) */
CREATE TABLE [BUSINESS.CONTACT](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[ENTITY_GUID] [uniqueidentifier] NOT NULL,
	[RECORD_GUID] [uniqueidentifier] NOT NULL,
	[TYPE] [nvarchar](20) NOT NULL,							-- fixed values: MAIN
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]