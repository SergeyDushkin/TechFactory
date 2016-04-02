/* table to store orders header */
CREATE TABLE [BUSINESS.WMS.ORDER](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[TYPE] nvarchar(20) NOT NULL,							-- valid values: SO
	[DUEDATE] smalldatetime NOT NULL,						-- Date/Time when order should be processed
	[NUMBER] nvarchar(20) NOT NULL,
	[DATE] smalldatetime NOT NULL,
	[CUSTOMER_GUID] [uniqueidentifier] NOT NULL,
	[SOURCE_GUID] [uniqueidentifier] NOT NULL,				-- source location
	[DESTINATION_GUID] [uniqueidentifier] NOT NULL,			-- destination location
 	[CURRENCY_GUID] [uniqueidentifier] NOT NULL,
	[LINES] smallint NOT NULL,
	[AMOUNT] FLOAT NOT NULL,
	[BASE_AMOUNT] FLOAT NOT NULL,
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]