# Dapper.Contrib.Postgres

Currently supporting `Insert` and `InsertAsync` operations.
```
var item = new ...

connection.Insert(item);
```
or
```
var item = new ...

await connection.InsertAsync(item);
```
