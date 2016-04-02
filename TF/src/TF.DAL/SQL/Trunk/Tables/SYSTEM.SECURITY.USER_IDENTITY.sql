/* table to store user identities */
CREATE TABLE [dbo].[SYSTEM.SECURITY.USER_IDENTITY](
	[USER_GUID] [uniqueidentifier] NOT NULL,
	[PROVIDER] [nvarchar](20) NOT NULL,					-- valid values PASSWORD, GOOGLE, MICROSOFT, FACEBOOK
	[KEY] [nvarchar](200) NOT NULL,						-- for the PASSWORD TYPE i'd like to store in DB not hashed password but during firt login password should be hashed and saved
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]