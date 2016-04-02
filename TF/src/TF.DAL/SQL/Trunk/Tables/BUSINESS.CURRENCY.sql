/*  list of Currencies */
CREATE TABLE [dbo].[BUSINESS.CURRENCY](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[KEY] [nvarchar](50) NOT NULL,
	[NAME] [nvarchar](200),
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]