using System;
using Dapper.Contrib.Postgres.Attributes;

namespace Dapper.Contrib.Postgres.IntegrationTests.TestTypes
{
    [UseQuotedIdentifiers]
    public class TestType23
    {
        public long Id { get; set; }
        public long RelatedId { get; set; }
        public long UserId { get; set; }
        public string UserEmail { get; set; }
        public TimeSpan AuditDate { get; set; }
        public string AuditType { get; set; }
    }
}