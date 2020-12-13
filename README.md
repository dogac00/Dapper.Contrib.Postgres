# Dapper.Contrib.Postgres

This library is designed for Insert and InsertAsync operations with extensions to `IDbConnection` interface.
Because of the issues is *Dapper.Contrib* library, we can have some problems with insert operations whether or not we use quoted or unquoted identifiers.

Currently supporting `Insert` and `InsertAsync` operations.
``` cs
var item = new Entity { ... };

connection.Insert(item);
```
or
``` cs
var item = new Entity { ... };

await connection.InsertAsync(item);
```

You can use `UseQuotedIdentifiers` attribute to generate your sql with quotes:
``` cs
[UseQuotedIdentifiers]
class Entity
{
  public long Id { get; set; }
  public string Name { get; set; }
}
```

You can use `Table` attribute to generate your sql with specified table:
``` cs
[Table("tbl_Entities")]
class Entity
{
  public long Id { get; set; }
  public string Name { get; set; }
}
```

You can use `Column` attribute to generate your sql with specified columns:
``` cs
class Entity
{
  public long Id { get; set; }
  [Column("Entity_Name")]
  public string Name { get; set; }
}
```

You can use `AutoIncrement` attribute to generate your sql without auto incremented columns:
``` cs
class Entity
{
  [AutoIncrement]
  public long Id { get; set; }
  public string Name { get; set; }
}
```

You can use `Key` attribute to generate your sql with given key (No `Key` is required for columns named `Id`):
``` cs
class Entity
{
  [Key]
  public long EntityId { get; set; }
  public string Name { get; set; }
}
```
