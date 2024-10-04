# Week 3

Week 3 Software developer intern (Entity Framework)

1. Using the linq queries retrieve a list of all customers from the database who don't have sales

2. Insert a new customer with a sale record

3. Add a new store 

4. Find the list of all stores that have sales 

Command line syntax

dotnet-ef dbcontext scaffold "Server=localhost;Initial Catalog=IndustryConnectWeek2;Integrated Security=True;TrustServerCertificate=Yes" Microsoft.EntityFrameworkCore.SqlServer -o Models -f