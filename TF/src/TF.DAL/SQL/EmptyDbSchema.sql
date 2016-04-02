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

/* table to store list of entities */
CREATE TABLE [dbo].[SYSTEM.ENTITY](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[KEY] [nvarchar](50) NOT NULL,
	[NAME] [nvarchar](200),
	[PARENT_GUID] [uniqueidentifier],					-- to build relashinship between entites, in future it will be hepfull for example to have integration with different data providers
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]

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

/*  list of Currencies */
CREATE TABLE [dbo].[BUSINESS.CURRENCY](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[KEY] [nvarchar](50) NOT NULL,
	[NAME] [nvarchar](200),
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]

/*  list of Unit Of Measures */
CREATE TABLE [dbo].[BUSINESS.UOM](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[KEY] [nvarchar](50) NOT NULL,
	[NAME] [nvarchar](200),
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]

/*  table to store physical presense, such as STORE, WAREHOUSE, etc */
CREATE TABLE [BUSINESS.LOCATION](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[BUSINESSUNIT_GUID] [uniqueidentifier] NOT NULL,
	[TYPE] [nvarchar](20) NOT NULL,							-- fixed values: STORE, WAREHOUSE, OFFICE, HOME, WORK, MAIN
	[KEY] [nvarchar](50) NOT NULL,
	[NAME] [nvarchar](200),
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

/* table to store order lines */
CREATE TABLE [BUSINESS.WMS.ORDER_LINE](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[ORDER_GUID] [uniqueidentifier] NOT NULL,
	[PRIORITY] [smallint] NOT NULL,			
	[ITEM_GUID] [uniqueidentifier] NOT NULL,
	[UOM_GUID] [uniqueidentifier] NOT NULL,
	[QTY] FLOAT NOT NULL,
	[BASE_QTY] FLOAT NOT NULL,
	[PRICE] FLOAT NOT NULL,
	[BASE_PRICE] FLOAT NOT NULL,
	[AMOUNT] FLOAT NOT NULL,
	[BASE_AMOUNT] FLOAT NOT NULL,
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]

/* table to store details for order line */
CREATE TABLE [BUSINESS.WMS.ORDER_LINE_DETAIL](
	[GUID_RECORD] [uniqueidentifier] NOT NULL,
	[ORDER_GUID] [uniqueidentifier] NOT NULL,
	[ORDER_LINE_GUID] [uniqueidentifier] NOT NULL,
	[PRIORITY] [smallint] NOT NULL,		
	[ITEM_GUID] [uniqueidentifier] NOT NULL,
	[LOCATION_GUID] [uniqueidentifier] NOT NULL,			-- where to hold an item
	[UOM_GUID] [uniqueidentifier] NOT NULL,
	[QTY] FLOAT NOT NULL,
	[BASE_QTY] FLOAT NOT NULL,
	[NUMBER] nvarchar(50) NOT NULL,							-- could be Serial Number of the item
	[BATCH_GUID] [uniqueidentifier],
	[HIDDEN] [bit] NOT NULL,
	[DELETED] [bit] NOT NULL
) ON [PRIMARY]