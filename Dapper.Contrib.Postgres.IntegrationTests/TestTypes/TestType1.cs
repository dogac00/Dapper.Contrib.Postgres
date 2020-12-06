using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.TestTypes
{
    [UseQuotedIdentifiers]
    public class TestType1
    {
        [AutoIncrement]
        [Key]
        public long Id { get; set; }
        public long RelatedId { get; set; }
        public long UserId { get; set; }
    }
}