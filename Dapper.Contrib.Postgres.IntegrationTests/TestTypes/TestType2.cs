using System;
using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    public class TestType2
    {
        [Key]
        public string Id { get; set; }
        [Column("MyName")]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CloseDate { get; set; }
        public DateTime StartDate { get; set; }
        [Column("MyEndDate")]
        public DateTime EndDate { get; set; }
    }
}