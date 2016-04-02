/* DROP ALL TABLES */
DECLARE
	@table_name nvarchar(200),
	@SQLText nvarchar(max);

DECLARE table_cursor CURSOR
	FOR SELECT NAME FROM sysobjects WHERE xtype = 'U' ORDER BY NAME

OPEN table_cursor

FETCH NEXT FROM table_cursor 
INTO @table_name
WHILE @@FETCH_STATUS = 0
BEGIN
	SET @SQLText = 'DROP TABLE [' + @table_name + ']';
	EXEC(@SQLText);

	FETCH NEXT FROM table_cursor 
	INTO @table_name
END 
CLOSE table_cursor;
DEALLOCATE table_cursor;

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

/* table to store trees data (parent-child) */
CREATE TABLE [dbo].[BUSINESS.CATEGORY_TREE](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[KEY] [nvarchar](50) NOT NULL,
	[NAME] [nvarchar](200),
	[PARENT_GUID] [uniqueidentifier],
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[BUSINESS.WMS.PRODUCT_N_SERVICE](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[TYPE] [nvarchar](20) NOT NULL,						-- fixed values: REGULAR, KIT, DYNAKIT, NONSTOCK, INGREDIENT
	[KEY] [nvarchar](50) NOT NULL,
	[NAME] [nvarchar](200),
	[UOM_GUID] [uniqueidentifier] NOT NULL,
	[ALLOW_NEGATIVE_QTY] BIT NOT NULL,					-- ALLOW NEGATIVE ISSUES
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]

/* table to store prices of products and services */
CREATE TABLE [dbo].[BUSINESS.WMS.PRICE](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[ITEM_GUID] [uniqueidentifier] NOT NULL,
	[CURRENCY_GUID] [uniqueidentifier] NOT NULL,
	[LOCATION_GUID] [uniqueidentifier],					-- NULL means that price applying for all locations
	[PRICE] FLOAT NOT NULL,
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]

/* table data reflects products in categories (only leaf level available) */
CREATE TABLE [dbo].[BUSINESS.WMS.PRODUCT_N_SERVICE_CATEGORY](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[CATEGORY_GUID] [uniqueidentifier] NOT NULL,
	[ITEM_GUID] [uniqueidentifier] NOT NULL,
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]

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

/* table to store user identities */
CREATE TABLE [dbo].[SYSTEM.SECURITY.USER_IDENTITY](
	[USER_GUID] [uniqueidentifier] NOT NULL,
	[PROVIDER] [nvarchar](20) NOT NULL,					-- valid values PASSWORD, GOOGLE, MICROSOFT, FACEBOOK
	[KEY] [nvarchar](200) NOT NULL,						-- for the PASSWORD TYPE i'd like to store in DB not hashed password but during firt login password should be hashed and saved
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]

/* table to store security roles */
CREATE TABLE [dbo].[SYSTEM.SECURITY.ROLE](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[KEY] [nvarchar](50) NOT NULL,
	[NAME] [nvarchar](200),
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]

/* table to store mapping between users and their roles */
CREATE TABLE [dbo].[SYSTEM.SECURITY.USER_ROLE](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[USER_GUID] [uniqueidentifier] NOT NULL,
	[ROLE_GUID] [uniqueidentifier] NOT NULL,
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]

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

/* table to store contact information (header) */
CREATE TABLE [BUSINESS.CONTACT](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[ENTITY_GUID] [uniqueidentifier] NOT NULL,
	[RECORD_GUID] [uniqueidentifier] NOT NULL,
	[TYPE] [nvarchar](20) NOT NULL,							-- fixed values: MAIN
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]

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