using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace dz23_06
{
    class Program
    {
        private static string connectionString = @"Data Source=DIMAMONDESKTOP\SQLEXPRESS;Initial Catalog=Newsletter;Integrated Security=True";

        static void Main(string[] args)
        {
            if (ConnectToDB())
            {
                Console.WriteLine("1. Display all customers.");
                Console.WriteLine("2. Display emails of all customers.");
                Console.WriteLine("3. Display list of sections.");
                Console.WriteLine("4. Display list of promotional products.");
                Console.WriteLine("5. Display all cities.");
                Console.WriteLine("6. Display all countries.");
                Console.WriteLine("7. Display customers from a specific city.");
                Console.WriteLine("8. Display customers from a specific country.");
                Console.WriteLine("9. Display promotions for a specific country.");
                Console.WriteLine("Select an option:");

                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        DisplayAllCustomers();
                        break;
                    case 2:
                        DisplayAllEmails();
                        break;
                    case 3:
                        DisplayAllSections();
                        break;
                    case 4:
                        DisplayAllPromotions();
                        break;
                    case 5:
                        DisplayAllCities();
                        break;
                    case 6:
                        DisplayAllCountries();
                        break;
                    case 7:
                        Console.WriteLine("Enter city:");
                        string city = Console.ReadLine();
                        DisplayCustomersByCity(city);
                        break;
                    case 8:
                        Console.WriteLine("Enter country:");
                        string country = Console.ReadLine();
                        DisplayCustomersByCountry(country);
                        break;
                    case 9:
                        Console.WriteLine("Enter country:");
                        string promoCountry = Console.ReadLine();
                        DisplayPromotionsByCountry(promoCountry);
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private static bool ConnectToDB()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection is open");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                finally
                {
                    connection.Close();
                    Console.WriteLine("Connection is closed");
                }
            }
        }

        private static void DisplayAllCustomers()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var customers = connection.Query("SELECT * FROM Customers");
                foreach (var customer in customers)
                {
                    Console.WriteLine($"{customer.CustomerId}: {customer.FullName}, {customer.BirthDate}, {customer.Gender}, {customer.Email}, {customer.Country}, {customer.City}");
                }
            }
        }

        private static void DisplayAllEmails()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var emails = connection.Query("SELECT Email FROM Customers");
                foreach (var email in emails)
                {
                    Console.WriteLine(email.Email);
                }
            }
        }

        private static void DisplayAllSections()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sections = connection.Query("SELECT DISTINCT Interest FROM CustomerInterests");
                foreach (var section in sections)
                {
                    Console.WriteLine(section.Interest);
                }
            }
        }

        private static void DisplayAllPromotions()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var promotions = connection.Query("SELECT * FROM Promotions");
                foreach (var promotion in promotions)
                {
                    Console.WriteLine($"{promotion.PromotionId}: {promotion.Country}, {promotion.Section}, {promotion.StartDate} - {promotion.EndDate}, {promotion.Description}");
                }
            }
        }

        private static void DisplayAllCities()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var cities = connection.Query("SELECT DISTINCT City FROM Customers");
                foreach (var city in cities)
                {
                    Console.WriteLine(city.City);
                }
            }
        }

        private static void DisplayAllCountries()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var countries = connection.Query("SELECT DISTINCT Country FROM Customers");
                foreach (var country in countries)
                {
                    Console.WriteLine(country.Country);
                }
            }
        }

        private static void DisplayCustomersByCity(string city)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var customers = connection.Query("SELECT * FROM Customers WHERE City = @City", new { City = city });
                foreach (var customer in customers)
                {
                    Console.WriteLine($"{customer.CustomerId}: {customer.FullName}, {customer.BirthDate}, {customer.Gender}, {customer.Email}, {customer.Country}, {customer.City}");
                }
            }
        }

        private static void DisplayCustomersByCountry(string country)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var customers = connection.Query("SELECT * FROM Customers WHERE Country = @Country", new { Country = country });
                foreach (var customer in customers)
                {
                    Console.WriteLine($"{customer.CustomerId}: {customer.FullName}, {customer.BirthDate}, {customer.Gender}, {customer.Email}, {customer.Country}, {customer.City}");
                }
            }
        }

        private static void DisplayPromotionsByCountry(string country)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var promotions = connection.Query("SELECT * FROM Promotions WHERE Country = @Country", new { Country = country });
                foreach (var promotion in promotions)
                {
                    Console.WriteLine($"{promotion.PromotionId}: {promotion.Country}, {promotion.Section}, {promotion.StartDate} - {promotion.EndDate}, {promotion.Description}");
                }
            }
        }
    }
}
