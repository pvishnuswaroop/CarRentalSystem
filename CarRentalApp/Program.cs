using CarRentalLibrary.dao;
using CarRentalLibrary.entity;
using CarRentalLibrary.exception;
using CarRentalLibrary.util;
using Microsoft.Data.SqlClient;
using System;

namespace CarRentalApp
{
    public class MainModule
    {
        public static void Main(string[] args)
        {
            ICarLeaseRepository repository = new CarLeaseRepositoryImpl();
            bool running = true;

            while (running)
            {
                
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("\tWelcome to Car Rental System");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine();
                Console.WriteLine("Please choose an operation:");
                Console.WriteLine();
                Console.WriteLine("1. Add Car");
                Console.WriteLine("2. Remove Car");
                Console.WriteLine("3. List Available Cars");
                Console.WriteLine("4. List Rented Cars");
                Console.WriteLine("5. Find Car by ID");
                Console.WriteLine("6. Add Customer");
                Console.WriteLine("7. Remove Customer");
                Console.WriteLine("8. List Customers");
                Console.WriteLine("9. Find Customer by ID");
                Console.WriteLine("10. Create Lease");
                Console.WriteLine("11. Return Car");
                Console.WriteLine("12. List Active Leases");
                Console.WriteLine("13. List Lease History");
                Console.WriteLine("14. Record Payment");
                Console.WriteLine("15. Run Tests");
                Console.WriteLine("16. Exit");
                Console.WriteLine();
                Console.WriteLine("----------------------------------------");
                Console.Write("Enter your choice: ");


                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1: // Add Car
                        AddCar(repository);
                        break;

                    case 2: // Remove Car
                        RemoveCar(repository);
                        break;

                    case 3: // List Available Cars
                        ListAvailableCars(repository);
                        break;

                    case 4: // List Rented Cars
                        ListRentedCars(repository);
                        break;

                    case 5: // Find Car by ID
                        FindCarById(repository);
                        break;

                    case 6: // Add Customer
                        AddCustomer(repository);
                        break;

                    case 7: // Remove Customer
                        RemoveCustomer(repository);
                        break;

                    case 8: // List Customers
                        ListCustomers(repository);
                        break;

                    case 9: // Find Customer by ID
                        FindCustomerById(repository);
                        break;

                    case 10: // Create Lease
                        CreateLease(repository);
                        break;

                    case 11: // Return Car
                        ReturnCar(repository);
                        break;

                    case 12: // List Active Leases
                        ListActiveLeases(repository);
                        break;

                    case 13: // List Lease History
                        ListLeaseHistory(repository);
                        break;
                    case 14: // Record Payment
                        RecordPayment(repository);
                        break;

                    case 15: // Run Tests
                        RunTests(repository);
                        break;

                    case 16: // Exit
                        running = false;
                        Console.WriteLine("Exiting the application...");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine(); 
            }
        }

        // Method to add a new car to the system
        private static void AddCar(ICarLeaseRepository repository)
        {
            try
            {
                Console.Write("Enter Vehicle ID: ");
                int vehicleID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Make: ");
                string make = Console.ReadLine();
                Console.Write("Enter Model: ");
                string model = Console.ReadLine();
                Console.Write("Enter Year: ");
                int year = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Daily Rate: ");
                decimal dailyRate = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Enter Status (available/notAvailable): ");
                string status = Console.ReadLine();
                Console.Write("Enter Passenger Capacity: ");
                int passengerCapacity = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Engine Capacity: ");
                int engineCapacity = Convert.ToInt32(Console.ReadLine());

                
                Car car = new Car(vehicleID, make, model, year, dailyRate, status, passengerCapacity, engineCapacity);
                repository.AddCar(car); 
                Console.WriteLine("Car added successfully!");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Method to remove a car from the system
        private static void RemoveCar(ICarLeaseRepository repository)
        {
            try
            {
                Console.Write("Enter Car ID to remove: ");
                int carID = Convert.ToInt32(Console.ReadLine());
                repository.RemoveCar(carID); 
                Console.WriteLine("Car removed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Method to list available cars
        private static void ListAvailableCars(ICarLeaseRepository repository)
        {
            var availableCars = repository.ListAvailableCars();
            Console.WriteLine("Available Cars:");
            foreach (var car in availableCars)  
            {
                Console.WriteLine($"{car.VehicleID}: {car.Make} {car.Model}, Year: {car.Year}, Rate: {car.DailyRate}, Status: {car.Status}");
            }
        }

        // Method to list rented cars
        private static void ListRentedCars(ICarLeaseRepository repository)
        {
            var rentedCars = repository.ListRentedCars();
            Console.WriteLine("Rented Cars:");
            foreach (var car in rentedCars)  
            {
                Console.WriteLine($"{car.VehicleID}: {car.Make} {car.Model}, Year: {car.Year}, Rate: {car.DailyRate}, Status: {car.Status}");
            }
        }

        // Method to find a car by its ID
        private static void FindCarById(ICarLeaseRepository repository)
        {
            try
            {
                Console.Write("Enter Car ID to find: ");
                int findCarID = Convert.ToInt32(Console.ReadLine());
                Car foundCar = repository.FindCarById(findCarID); 
                Console.WriteLine($"Found Car: {foundCar.Make} {foundCar.Model}, Year: {foundCar.Year}");
            }
            catch (CarNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Method to add a new customer to the system
        private static void AddCustomer(ICarLeaseRepository repository)
        {
            try
            {
                Console.Write("Enter Customer ID: ");
                int customerID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter First Name: ");
                string firstName = Console.ReadLine();
                Console.Write("Enter Last Name: ");
                string lastName = Console.ReadLine();
                Console.Write("Enter Email: ");
                string email = Console.ReadLine();
                Console.Write("Enter Phone Number: ");
                string phoneNumber = Console.ReadLine();

                // Create a new Customer object
                Customer customer = new Customer(customerID, firstName, lastName, email, phoneNumber);
                repository.AddCustomer(customer);
                Console.WriteLine("Customer added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Method to remove a customer from the system
        private static void RemoveCustomer(ICarLeaseRepository repository)
        {
            try
            {
                Console.Write("Enter Customer ID to remove: ");
                int customerID = Convert.ToInt32(Console.ReadLine());
                repository.RemoveCustomer(customerID); 
                Console.WriteLine("Customer removed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        // Method to list all customers
        private static void ListCustomers(ICarLeaseRepository repository)
        {
            var customers = repository.ListCustomers(); 
            Console.WriteLine("Customers:");
            foreach (var customer in customers)  
            {
                Console.WriteLine($"{customer.CustomerID}: {customer.FirstName} {customer.LastName}, Email: {customer.Email}");
            }
        }

        // Method to find a customer by their ID
        private static void FindCustomerById(ICarLeaseRepository repository)
        {
            try
            {
                Console.Write("Enter Customer ID to find: ");
                int customerID = Convert.ToInt32(Console.ReadLine());
                Customer foundCustomer = repository.FindCustomerById(customerID); 
                Console.WriteLine($"Found Customer: {foundCustomer.FirstName} {foundCustomer.LastName}, Email: {foundCustomer.Email}");
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Method to create a new lease
        private static void CreateLease(ICarLeaseRepository repository)
        {
            try
            {
                Console.Write("Enter Customer ID: ");
                int customerID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Car ID: ");
                int carID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Start Date (yyyy-mm-dd): ");
                DateTime startDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter End Date (yyyy-mm-dd): ");
                DateTime endDate = DateTime.Parse(Console.ReadLine());

                Lease lease = repository.CreateLease(customerID, carID, startDate, endDate); 
                Console.WriteLine("Lease created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Method to return a car and finalize the lease
        private static void ReturnCar(ICarLeaseRepository repository)
        {
            try
            {
                Console.Write("Enter Lease ID to return car: ");
                int leaseID = Convert.ToInt32(Console.ReadLine());
                repository.ReturnCar(leaseID); 
                Console.WriteLine("Car returned successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Method to list active leases
        private static void ListActiveLeases(ICarLeaseRepository repository)
        {
            var activeLeases = repository.ListActiveLeases(); 
            Console.WriteLine("Active Leases:");
            foreach (var lease in activeLeases) 
            {
                Console.WriteLine($"Lease ID: {lease.LeaseID}, Car ID: {lease.VehicleID}, Customer ID: {lease.CustomerID}, Start: {lease.StartDate}, End: {lease.EndDate}");
            }
        }

        // Method to list lease history
        private static void ListLeaseHistory(ICarLeaseRepository repository)
        {
            var leaseHistory = repository.ListLeaseHistory(); 
            Console.WriteLine("Lease History:");
            foreach (var lease in leaseHistory) 
            {
                Console.WriteLine($"Lease ID: {lease.LeaseID}, Car ID: {lease.VehicleID}, Customer ID: {lease.CustomerID}, Start: {lease.StartDate}, End: {lease.EndDate}");
            }
        }

        private static void RecordPayment(ICarLeaseRepository repository)
        {
            try
            {
                Console.Write("Enter Lease ID for payment: ");
                int leaseID = Convert.ToInt32(Console.ReadLine());

                // Retrieve the Lease by its ID
                Lease lease = repository.FindLeaseById(leaseID);  

                if (lease != null)
                {
                    Console.Write("Enter Payment Amount: ");
                    decimal amount = Convert.ToDecimal(Console.ReadLine());
                    repository.RecordPayment(lease, (double)amount);
                    Console.WriteLine("Payment recorded successfully!");
                }
                else
                {
                    Console.WriteLine($"Error: No lease found with ID {leaseID}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        private static void RunTests(ICarLeaseRepository repository)
        {
            try
            {
                // Test Case 1: Add Car and check if created successfully
                int testCarID = (int)(DateTime.Now.Ticks % int.MaxValue); 
                var car = new Car(testCarID, "Tata", "Nexon", 2023, 60.00M, "available", 5, 1200);
                repository.AddCar(car);

                
                var retrievedCar = repository.FindCarById(testCarID);
                Console.WriteLine(retrievedCar != null ? "Test Passed: Car created successfully." : "Test Failed: Car not found.");

                // Test Case 2: Create a customer
                int testCustomerID = (int)(DateTime.Now.Ticks % int.MaxValue); 
                var customer = new Customer(testCustomerID, "John", "Doe", "john.doe@example.com", "1234567890");
                repository.AddCustomer(customer);

                
                Lease lease = repository.CreateLease(testCustomerID, testCarID, DateTime.Now, DateTime.Now.AddDays(7));
                Console.WriteLine(lease != null ? "Test Passed: Lease created successfully." : "Test Failed: Lease not created.");

                // Test Case 3: Retrieve Lease and check if retrieved successfully
                var retrievedLease = repository.FindLeaseById(lease.LeaseID);
                Console.WriteLine(retrievedLease != null ? "Test Passed: Lease retrieved successfully." : "Test Failed: Lease not found.");

                // Test Case 4: Test Exception Handling for Non-Existent Car
                try
                {
                    repository.FindCarById(99); 
                    Console.WriteLine("Test Failed: Expected exception not thrown for non-existent car.");
                }
                catch (CarNotFoundException)
                {
                    Console.WriteLine("Test Passed: Exception correctly thrown for non-existent car.");
                }

                // Test Case 5: Test Exception Handling for Non-Existent Customer
                try
                {
                    repository.FindCustomerById(999); 
                    Console.WriteLine("Test Failed: Expected exception not thrown for non-existent customer.");
                }
                catch (CustomerNotFoundException)
                {
                    Console.WriteLine("Test Passed: Exception correctly thrown for non-existent customer.");
                }

                // Test Case 6: Test Exception Handling for Non-Existent Lease
                try
                {
                    repository.FindLeaseById(98); 
                    Console.WriteLine("Test Failed: Expected exception not thrown for non-existent lease.");
                }
                catch (LeaseNotFoundException)
                {
                    Console.WriteLine("Test Passed: Exception correctly thrown for non-existent lease.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test Failed: {ex.Message}");
            }
        }

    }
}
