/* table to store users */
CREATE TABLE [dbo].[SYSTEM.SECURITY.USER](
	[GUID_RECORD] [uniqueidentifier] NOT NULL ,
	[KEY] [nvarchar](50) NOT NULL,
	[LAST_LOGIN] smalldatetime NOT NULL,
	[LOGIN_ATTEMPT_COUNT] smallint NOT NULL, 
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]