using Dapper;
using System;
using TF.Data.Business;

namespace TF.DAL.Query
{
    class AddressQuery
    {
        public static CommandDefinition All()
        {
            return new CommandDefinition(@"SELECT [GUID_RECORD] Id,
                                            [TYPE],
                                            [COUNTRY],
                                            [POSTALCODE],
                                            [CITY],
                                            [Line1],
                                            [Line2],
                                            [LATITUDE],
                                            [LONGITUDE],
                                            [ELEVATION]
                                           FROM [BUSINESS.ADDRESS] WHERE [DELETED] = 0");
        }

        public static CommandDefinition ById(Guid id)
        {
            return new CommandDefinition(@"SELECT [GUID_RECORD] Id,
                                            [TYPE],
                                            [COUNTRY],
                                            [POSTALCODE],
                                            [CITY],
                                            [Line1],
                                            [Line2],
                                            [LATITUDE],
                                            [LONGITUDE],
                                            [ELEVATION]
                                           FROM [BUSINESS.ADDRESS] WHERE  [GUID_RECORD]=@id AND [DELETED] = 0", new { id });
        }

        public static CommandDefinition Update(Address record)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.ADDRESS]
                SET [TYPE] = @TYPE,
                [COUNTRY] = @COUNTRY,
                [POSTALCODE] = @POSTALCODE,
                [CITY] = @CITY,
                [Line1] = @Line1,
                [Line2] = @Line2,
                [LATITUDE] = @LATITUDE,
                [LONGITUDE] = @LONGITUDE,
                [ELEVATION] = @ELEVATION
                WHERE GUID_RECORD = @GUID_RECORD", new
                {
                    TYPE = record.Type,
                    COUNTRY = record.Country,
                    POSTALCODE = record.Postalcode,
                    CITY = record.City,
                    Line1 = record.Line1,
                    Line2 = record.Line2,
                    LATITUDE = record.Latitude,
                    LONGITUDE = record.Longitude,
                    ELEVATION =  record.Elevation,
                    GUID_RECORD = record.Id
                });
        }

        public static CommandDefinition Insert(Address record)
        {
            return new CommandDefinition(
                @"INSERT INTO [BUSINESS.ADDRESS] ([GUID_RECORD],
                    [TYPE],
                    [COUNTRY],
                    [POSTALCODE],
                    [CITY],
                    [Line1],
                    [Line2],
                    [LATITUDE],
                    [LONGITUDE],
                    [ELEVATION],
                    [BATCH_GUID],
                    [ENTITY_GUID],
	                [RECORD_GUID], 
                    [HIDDEN],
                    [DELETED]) 
                VALUES (@GUID_RECORD,
                    @TYPE,
                    @COUNTRY,
                    @POSTALCODE,
                    @CITY,
                    @Line1,
                    @Line2,
                    @LATITUDE,
                    @LONGITUDE,
                    @ELEVATION,
                    @BATCH_GUID,
                    @ENTITY_GUID,
	                @RECORD_GUID,
                    @HIDDEN,
                    @DELETED)", new
                {
                    GUID_RECORD = record.Id,
                    TYPE = record.Type,
                    COUNTRY = record.Country,
                    POSTALCODE = record.Postalcode,
                    CITY = record.City,
                    Line1 = record.Line1,
                    Line2 = record.Line2,
                    LATITUDE = record.Latitude,
                    LONGITUDE = record.Longitude,
                    ELEVATION = record.Elevation,
                    BATCH_GUID = (Guid?)null,
                    ENTITY_GUID = Guid.Empty,
                    RECORD_GUID = Guid.Empty,
                    HIDDEN = 0,
                    DELETED = 0
                });
        }

        public static CommandDefinition Delete(Guid id)
        {
            return new CommandDefinition(
                @"UPDATE [BUSINESS.Address] SET [DELETED] = 1 WHERE [GUID_RECORD] = @GUID_RECORD", new
                {
                    GUID_RECORD = id
                });
        }
    }
}
