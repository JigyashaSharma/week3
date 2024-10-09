using EntityModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using Week3EntityFramework.Dtos;

var context = new IndustryConnectWeek2Context();

//var customer = new Customer
//{
//    DateOfBirth = DateTime.Now.AddYears(-20)
//};


//Console.WriteLine("Please enter the customer firstname?");

//customer.FirstName = Console.ReadLine();

//Console.WriteLine("Please enter the customer lastname?");

//customer.LastName = Console.ReadLine();


//var customers = context.Customers.ToList();

//foreach (Customer c in customers)
//{   
//    Console.WriteLine("Hello I'm " + c.FirstName);
//}

//Console.WriteLine($"Your new customer is {customer.FirstName} {customer.LastName}");

//Console.WriteLine("Do you want to save this customer to the database?");

//var response = Console.ReadLine();

//if (response?.ToLower() == "y")
//{
//    context.Customers.Add(customer);
//    context.SaveChanges();
//}



var sales = context.Sales.Include(c => c.Customer)
    .Include(p => p.Product).ToList();

var salesDto = new List<SaleDto>();

foreach (Sale s in sales)
{
    salesDto.Add(new SaleDto(s));
}



//context.Sales.Add(new Sale
//{
//    ProductId = 1,
//    CustomerId = 1,
//    StoreId = 1,
//    DateSold = DateTime.Now
//});


//context.SaveChanges();




Console.WriteLine("Which customer record would you like to update?");

var response = Convert.ToInt32(Console.ReadLine());

var customer = context.Customers.Include(s => s.Sales)
    .ThenInclude(p => p.Product)
    .FirstOrDefault(c => c.Id == response);


var total = customer?.Sales.Select(s => s.Product.Price).Sum();


var customerSales = context.CustomerSales.ToList();

//var totalsales = customer.Sales



//Console.WriteLine($"The customer you have retrieved is {customer?.FirstName} {customer?.LastName}");

//Console.WriteLine($"Would you like to updated the firstname? y/n");

//var updateResponse = Console.ReadLine();

//if (updateResponse?.ToLower() == "y")
//{

//    Console.WriteLine($"Please enter the new name");

//    customer.FirstName = Console.ReadLine();
//    context.Customers.Add(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
//    context.SaveChanges();
//}


/*********************************************Task 1********************************************************/
//Task 1 Using the linq queries retrieve a list of all customers from the database who don't have sales
//Trying to learn different ways

//a. using Method Syntax
Console.WriteLine("\n\n*************Task1*************\n");
Console.WriteLine("\nCustomers with No Sale Using Method Syntax");
var custNoSale = context.Customers
    .Where(c => c.Sales.Count == 0)
    .Select(c => new CustomerDto(c))
    .ToList();
if (custNoSale.Count != 0)
{
    foreach (var cust in custNoSale)
    {
        Console.WriteLine(cust.CustomerName);
    }
}
else
{
    Console.WriteLine("All the customers have Sale!!!");
}


//b. Using Query Syntax
Console.WriteLine("\nCustomers with No Sale Using Query Syntax");
var custNoSale2 = from c in context.Customers
                  where c.Sales.Count == 0
                  select new CustomerDto(c);
if (custNoSale2.Count() != 0)
{
    foreach (var cust in custNoSale2)
    {
        Console.WriteLine(cust.CustomerName);
    }
}
else
{
    Console.WriteLine("All the customers have Sale!!!");
}

Console.WriteLine("\nCustomers with No Sale Using Query Syntax 2");
custNoSale2 = from c in context.Customers
              join s in context.Sales on c.Id equals s.CustomerId into CustomerSaleGroup
              from cs1 in CustomerSaleGroup.DefaultIfEmpty()
              where cs1.Customer == null
              select new CustomerDto(c);

if (custNoSale2.Count() != 0)
{
    foreach (var cust in custNoSale2)
    {
        Console.WriteLine(cust.CustomerName);
    }
}
else
{
    Console.WriteLine("All the customers have Sale!!!");
}
/*************************************************Task 1 Ends****************************************************/

/*************************************************Task 2 ********************************************************/
//Task2: Insert a new customer with a sale record

Console.WriteLine("\n\n*************Task2*************\n");
Console.WriteLine("Enter the Customer Details");
Console.WriteLine("FirstName?");
var fName = Console.ReadLine();
Console.WriteLine("LastName?");
var lName = Console.ReadLine();

bool flag = true;
//Run the loop till DOB is not in correct format
while (flag)
{
    Console.WriteLine("Valid Date of Birth in yyyy-mm-dd format?");
    var inDate = Console.ReadLine();

    CultureInfo cinfo = CultureInfo.InvariantCulture;
    string dateFormat = "yyyy-MM-dd";

    //Checks for date validity
    if (DateTime.TryParseExact(inDate, dateFormat, cinfo, DateTimeStyles.None, out DateTime dob)
        && ((dob.Year < DateTime.Today.Year ||( dob.Year == DateTime.Today.Year && dob.Month <= DateTime.Today.Month && dob.Day <= DateTime.Today.Day)))
        && (dob.Month >= 1 || dob.Month <= 12)
        && (dob.Day > 0 || dob.Day < DateTime.DaysInMonth(dob.Year,dob.Month)))
    {
        customer = new Customer { FirstName = fName, LastName = lName, DateOfBirth = dob };
        context.Customers.Add(customer);
        context.SaveChanges();

        var sale = new Sale { CustomerId = customer.Id, ProductId = 2, StoreId = 2, DateSold = DateTime.Now };
        context.Sales.Add(sale);
        context.SaveChanges();

        Console.WriteLine($"Customer \"{customer.FirstName} {customer.LastName}\" and Sale added.");
        flag = false;
    }
    else
    {
        Console.WriteLine("Dob was not proper: Check the format yyyy-mm-dd or date should be before today's date.");
    }
}

/*************************************************Task 2 Ends ********************************************************/

/*************************************************Task 3 ********************************************************/
//Task3: Add a new store

Console.WriteLine("\n\n*************Task3*************\n");

Console.WriteLine("Enter the Store Details");
Console.WriteLine("Store Name?");
var sName = Console.ReadLine();
Console.WriteLine("Store Location?");
var sLocation = Console.ReadLine();

var store = new Store { Name = sName, Location = sLocation };
context.Stores.Add(store);
context.SaveChanges();

Console.WriteLine($"Store \"{store.Name}\" Added.");

/*************************************************Task 3 Ends ********************************************************/

/*************************************************Task 4 ********************************************************/
//Task4: Find the list of all stores that have sales

Console.WriteLine("\n\n*************Task4*************\n");
var storeDto = context.Stores
    .Where(s => s.Sales.Count() > 0)
    .Select(s => new StoreDto
    {
        Info = s.Name + ", " + s.Location
    });

if (storeDto.Count() != 0)
{
    Console.WriteLine("List of Stores with Sales\n");
    foreach (var sd in storeDto)
    {
        Console.WriteLine(sd.Info);
    }
}
else
{
    Console.WriteLine("No stores has done Sale.");
}

/*************************************************Task 4 Ends ********************************************************/
Console.ReadLine();









