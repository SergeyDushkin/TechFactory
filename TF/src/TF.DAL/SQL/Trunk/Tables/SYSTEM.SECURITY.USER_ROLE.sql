/* table to store mapping between users and their roles */
CREATE TABLE [dbo].[SYSTEM.SECURITY.USER_ROLE](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[USER_GUID] [uniqueidentifier] NOT NULL,
	[ROLE_GUID] [uniqueidentifier] NOT NULL,
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]