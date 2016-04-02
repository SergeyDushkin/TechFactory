/*  table to store physical presense, such as STORE, WAREHOUSE, etc */
CREATE TABLE [BUSINESS.LOCATION](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[BUSINESSUNIT_GUID] [uniqueidentifier] NOT NULL,
	[TYPE] [nvarchar](20) NOT NULL,							-- fixed values: STORE, WAREHOUSE, OFFICE, HOME, WORK, MAIN
	[KEY] [nvarchar](50) NOT NULL,
	[NAME] [nvarchar](200),
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]