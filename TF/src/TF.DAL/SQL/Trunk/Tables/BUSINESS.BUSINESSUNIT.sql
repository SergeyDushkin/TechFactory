/* table to store business unit data (company) */
CREATE TABLE [dbo].[BUSINESS.BUSINESSUNIT](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[KEY] [nvarchar](50) NOT NULL,
	[NAME] [nvarchar](200),
	[PARENT_GUID] [uniqueidentifier],					-- IS NULL for the company itself, all other records is an external companies 
	[INTEGRATION] [nvarchar](20) NOT NULL,				-- for future purpose, to have data integration between companies, valid values NONE/APIv1/EMAIL/FILE 
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]