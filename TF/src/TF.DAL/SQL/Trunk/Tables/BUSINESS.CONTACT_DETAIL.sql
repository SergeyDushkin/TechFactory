/* table to store contact information (details) */
CREATE TABLE [BUSINESS.CONTACT_DETAIL](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[CONTACT_GUID] [uniqueidentifier] NOT NULL,
	[PRIORITY] [smallint] NOT NULL,							-- order of importance
	[TYPE] [nvarchar](20) NOT NULL,							-- fixed values: EMAIL,PHONE,CELL,SKYPE,VIBER,URL,FAX
	[VALUE] [nvarchar](50) NOT NULL,
	[VERIFIED] BIT NOT NULL,
	[ALLOW] BIT NOT NULL,									-- allow to communicate using this channel
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]