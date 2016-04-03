/* table to store addresses */
CREATE TABLE [BUSINESS.ADDRESS](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[ENTITY_GUID] [uniqueidentifier] NOT NULL,
	[RECORD_GUID] [uniqueidentifier] NOT NULL,
	[TYPE] [nvarchar](20) NOT NULL,							-- fixed values: MAIN
	[COUNTRY] NVARCHAR(20) NOT NULL,						-- Abbreviation of the country, like RU, US, UK
	[POSTALCODE] NVARCHAR(20),
	[CITY] NVARCHAR(50) NOT NULL,
	[Line1] NVARCHAR(200) NOT NULL,
	[Line2] NVARCHAR(200) NOT NULL,
	[LATITUDE] FLOAT,
	[LONGITUDE] FLOAT,
	[ELEVATION] FLOAT,
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]