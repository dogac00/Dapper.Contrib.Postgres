# Dapper.Contrib.Postgres

Currently supporting `Insert` and `InsertAsync` operations.
``` cs
var item = new ...

connection.Insert(item);
```
or
``` cs
var item = new ...

await connection.InsertAsync(item);
```

You can use `UseQuotedIdentifiers` attribute to generate your sql with quotes:
```
[UseQuotedIdentifiers]
class Employee
{
  public long Id { get; set; }
  public string Name { get; set; }
}

var employee = new Employee ...

await connection.InsertAsync(employee);
```
