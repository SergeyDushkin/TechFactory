/*  table to store business unit employees */
CREATE TABLE [BUSINESS.EMPLOYEE](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[BUSINESSUNIT_GUID] [uniqueidentifier] NOT NULL,
	[DEPARTMENT_GUID] [uniqueidentifier],
	[POSITION_GUID] [uniqueidentifier],
	[START_DATE] SMALLDATETIME,
	[END_DATE] SMALLDATETIME,
	[PERSON_GUID] [uniqueidentifier] NOT NULL,
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]