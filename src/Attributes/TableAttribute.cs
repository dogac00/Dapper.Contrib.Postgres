using System;

namespace Dapper.Contrib.Postgres.Attributes
{
    public class TableAttribute : Attribute
    {
        public string Name { get; }

        public TableAttribute(string name)
        {
            Name = name;
        }
    }
}