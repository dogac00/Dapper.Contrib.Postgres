using System;

namespace Dapper.Contrib.Postgres.Attributes
{
    public class ColumnAttribute : Attribute
    {
        public string Name { get; }

        public ColumnAttribute(string name)
        {
            Name = name;
        }
    }
}