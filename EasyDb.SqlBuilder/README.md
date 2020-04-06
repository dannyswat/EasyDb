# EasyDb.SqlBuilder
Utility to build a SQL query from C# code
The library uses the visitor pattern to build the SQL
The following code is to show how to use the utility

    Query query = new Query();
    query.From = new Table("Products");
    ISqlGenerator generator = new MsSqlGenerator();
    query.Accept(generator);
    Console.WriteLine(generator.GenerateSql());
    
The next milestone is to make the library more user friendly with builder pattern
