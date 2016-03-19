using System;

namespace TF.Data.Business
{
    /*
    CREATE TABLE [dbo].[BUSINESS.CATEGORY_TREE](
	    [GUID_RECORD] [uniqueidentifier] NOT NULL,
	    [KEY] [nvarchar](50) NOT NULL,
	    [NAME] [nvarchar](200),
	    [PARENT_GUID] [uniqueidentifier],
	    [BATCH_GUID] [uniqueidentifier],
	    [HIDDEN] [bit] NOT NULL,
	    [DELETED] [bit] NOT NULL
    ) ON [PRIMARY]
    */

    public class CategoryTree
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }

        public Guid? ParentId { get; set; }
    }
}
