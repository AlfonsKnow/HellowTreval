
CONFIGURATION
-------------

Edit the file `WebUI/Web.config` with real data, for example:

```xml
  <connectionStrings>
    <add name="EFDbContext" connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\username\DB\myDB.mdf;Integrated Security=True;Connect Timeout=30" providerName="System.Data.SqlClient" />
  </connectionStrings>
```
