/* Table which contains links to the files */
CREATE TABLE [dbo].[SYSTEM.LINK](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[REFERENCE_GUID] [uniqueidentifier] NOT NULL,
	[URI] [nvarchar](2000) NOT NULL,
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]