/*  table to store persons/people/employee/user etc */
CREATE TABLE [BUSINESS.PERSON](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[FIRSTNAME] [nvarchar](50) NOT NULL,
	[LASTNAME] [nvarchar](50) NOT NULL,
	[MIDNAME] [nvarchar](50),
	[BIRTHDATE] SMALLDATETIME,
	[USER_GUID] [uniqueidentifier],
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]