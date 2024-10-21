using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient; 
using CarRentalLibrary.util;
using CarRentalLibrary.exception;
using CarRentalLibrary.entity;

namespace CarRentalLibrary.dao
{
    
    public class CarLeaseRepositoryImpl : ICarLeaseRepository
    {
        
        public void AddCar(Car car)
        {
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "INSERT INTO Vehicle (vehicleID, make, model, year, dailyRate, status, passengerCapacity, engineCapacity) VALUES (@VehicleID, @Make, @Model, @Year, @DailyRate, @Status, @PassengerCapacity, @EngineCapacity)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", car.VehicleID);
                    command.Parameters.AddWithValue("@Make", car.Make);
                    command.Parameters.AddWithValue("@Model", car.Model);
                    command.Parameters.AddWithValue("@Year", car.Year);
                    command.Parameters.AddWithValue("@DailyRate", car.DailyRate);
                    command.Parameters.AddWithValue("@Status", car.Status);
                    command.Parameters.AddWithValue("@PassengerCapacity", car.PassengerCapacity);
                    command.Parameters.AddWithValue("@EngineCapacity", car.EngineCapacity);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Retrieves a lease by its ID
        public Lease GetLeaseById(int leaseId)
        {
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Lease WHERE leaseID = @LeaseID"; 
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LeaseID", leaseId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Lease
                            {
                                LeaseID = (int)reader["leaseID"], 
                                CustomerID = (int)reader["customerID"],
                                VehicleID = (int)reader["vehicleID"],
                                StartDate = (DateTime)reader["startDate"],
                                EndDate = (DateTime)reader["endDate"]
                            };
                        }
                    }
                }
            }
            return null;
        }

        // Removes a car and its associated leases
        public void RemoveCar(int carID)
        {

            RemoveLeasesByCarId(carID); 

            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "DELETE FROM Vehicle WHERE vehicleID = @VehicleID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", carID);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Lists all available cars
        public List<Car> ListAvailableCars()
        {
            List<Car> availableCars = new List<Car>();
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Vehicle WHERE status = 'available'";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Car car = new Car
                            {
                                VehicleID = (int)reader["vehicleID"],
                                Make = (string)reader["make"],
                                Model = (string)reader["model"],
                                Year = (int)reader["year"],
                                DailyRate = (decimal)reader["dailyRate"],
                                Status = (string)reader["status"],
                                PassengerCapacity = (int)reader["passengerCapacity"],
                                EngineCapacity = (int)reader["engineCapacity"]
                            };
                            availableCars.Add(car); 
                        }
                    }
                }
            }
            return availableCars;
        }

        // Lists all rented cars
        public List<Car> ListRentedCars()
        {
            List<Car> rentedCars = new List<Car>();
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Vehicle WHERE status = 'notAvailable'";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Car car = new Car
                            {
                                VehicleID = (int)reader["vehicleID"],
                                Make = (string)reader["make"],
                                Model = (string)reader["model"],
                                Year = (int)reader["year"],
                                DailyRate = (decimal)reader["dailyRate"],
                                Status = (string)reader["status"],
                                PassengerCapacity = (int)reader["passengerCapacity"],
                                EngineCapacity = (int)reader["engineCapacity"]
                            };
                            rentedCars.Add(car);
                        }
                    }
                }
            }
            return rentedCars;
        }

        // Removes all leases associated with a specific car ID
        public void RemoveLeasesByCarId(int carID)
        {
            using (var connection = DBConnUtil.GetConnection())
            {
                string deleteLeasesQuery = "DELETE FROM Lease WHERE vehicleID = @VehicleID";
                using (var command = new SqlCommand(deleteLeasesQuery, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", carID);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Finds a car by its ID
        public Car FindCarById(int carID)
        {
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Vehicle WHERE vehicleID = @VehicleID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", carID);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Car
                            {
                                VehicleID = (int)reader["vehicleID"],
                                Make = (string)reader["make"],
                                Model = (string)reader["model"],
                                Year = (int)reader["year"],
                                DailyRate = (decimal)reader["dailyRate"],
                                Status = (string)reader["status"],
                                PassengerCapacity = (int)reader["passengerCapacity"],
                                EngineCapacity = (int)reader["engineCapacity"]
                            };
                        }
                    }
                }
            }
            throw new CarNotFoundException($"Car with ID {carID} not found.");
        }

        // Adds a new customer to the database
        public void AddCustomer(Customer customer)
        {
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "INSERT INTO Customer (customerID, firstName, lastName, email, phoneNumber) VALUES (@CustomerID, @FirstName, @LastName, @Email, @PhoneNumber)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Removes a customer and their associated leases
        public void RemoveCustomer(int customerID)
        {
            using (var connection = DBConnUtil.GetConnection())
            {
                
                string deleteLeasesQuery = "DELETE FROM Lease WHERE customerID = @CustomerID";
                using (var command = new SqlCommand(deleteLeasesQuery, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", customerID);
                    command.ExecuteNonQuery();
                }

                
                string deleteCustomerQuery = "DELETE FROM Customer WHERE customerID = @CustomerID";
                using (var command = new SqlCommand(deleteCustomerQuery, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", customerID);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Lists all customers
        public List<Customer> ListCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Customer";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer
                            {
                                CustomerID = (int)reader["customerID"],
                                FirstName = (string)reader["firstName"],
                                LastName = (string)reader["lastName"],
                                Email = (string)reader["email"],
                                PhoneNumber = (string)reader["phoneNumber"]
                            };
                            customers.Add(customer);
                        }
                    }
                }
            }
            return customers;
        }

        // Finds a customer by their ID
        public Customer FindCustomerById(int customerID)
        {
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Customer WHERE customerID = @CustomerID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", customerID);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customer
                            {
                                CustomerID = (int)reader["customerID"],
                                FirstName = (string)reader["firstName"],
                                LastName = (string)reader["lastName"],
                                Email = (string)reader["email"],
                                PhoneNumber = (string)reader["phoneNumber"]
                            };
                        }
                    }
                }
            }
            throw new CustomerNotFoundException($"Customer with ID {customerID} not found."); 
        }


        
        public Lease CreateLease(int customerID, int carID, DateTime startDate, DateTime endDate)
        {
            Lease lease = null;
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "INSERT INTO Lease (vehicleID, customerID, startDate, endDate, type) VALUES (@VehicleID, @CustomerID, @StartDate, @EndDate, @Type); SELECT SCOPE_IDENTITY();";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", carID);
                    command.Parameters.AddWithValue("@CustomerID", customerID);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@Type", (endDate - startDate).TotalDays > 30 ? "Monthly" : "Daily");

                    
                    int newLeaseID = Convert.ToInt32(command.ExecuteScalar());

                    
                    lease = new Lease(newLeaseID, carID, customerID, startDate, endDate, (endDate - startDate).TotalDays > 30 ? "Monthly" : "Daily");
                }
            }
            return lease; 
        }


        public Lease ReturnCar(int leaseID)
        {
            using (var connection = DBConnUtil.GetConnection())
            {
                
                string query = "SELECT * FROM Lease WHERE leaseID = @LeaseID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LeaseID", leaseID);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Lease lease = new Lease
                            {
                                LeaseID = (int)reader["leaseID"],
                                VehicleID = (int)reader["vehicleID"],
                                CustomerID = (int)reader["customerID"],
                                StartDate = (DateTime)reader["startDate"],
                                EndDate = (DateTime)reader["endDate"],
                                Type = (string)reader["type"]
                            };

                            
                            UpdateCarStatus(lease.VehicleID, "available");
                            return lease;
                        }
                    }
                }
            }
            throw new LeaseNotFoundException($"Lease with ID {leaseID} not found."); 
        }

        private void UpdateCarStatus(int vehicleID, string status)
        {
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "UPDATE Vehicle SET status = @Status WHERE vehicleID = @VehicleID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@VehicleID", vehicleID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Lease> ListActiveLeases()
        {
            List<Lease> leases = new List<Lease>();
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Lease";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lease lease = new Lease
                            {
                                LeaseID = (int)reader["leaseID"],
                                VehicleID = (int)reader["vehicleID"],
                                CustomerID = (int)reader["customerID"],
                                StartDate = (DateTime)reader["startDate"],
                                EndDate = (DateTime)reader["endDate"],
                                Type = (string)reader["type"]
                            };
                            leases.Add(lease);
                        }
                    }
                }
            }
            return leases;
        }

        public List<Lease> ListLeaseHistory()
        {
            List<Lease> leaseHistory = new List<Lease>();
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Lease"; 
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lease lease = new Lease
                            {
                                LeaseID = (int)reader["leaseID"],
                                VehicleID = (int)reader["vehicleID"],
                                CustomerID = (int)reader["customerID"],
                                StartDate = (DateTime)reader["startDate"],
                                EndDate = (DateTime)reader["endDate"],
                                Type = (string)reader["type"]
                            };
                            leaseHistory.Add(lease);
                        }
                    }
                }
            }
            return leaseHistory;
        }

        public Lease FindLeaseById(int leaseID)
        {
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Lease WHERE leaseID = @LeaseID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LeaseID", leaseID);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Lease
                            {
                                LeaseID = (int)reader["leaseID"],
                                VehicleID = (int)reader["vehicleID"],
                                CustomerID = (int)reader["customerID"],
                                StartDate = (DateTime)reader["startDate"],
                                EndDate = (DateTime)reader["endDate"],
                                Type = (string)reader["type"]
                            };
                        }
                    }
                }
            }
            throw new LeaseNotFoundException($"Lease with ID {leaseID} not found."); 
        }



        public void RecordPayment(Lease lease, double amount)
        {
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "INSERT INTO Payment (leaseID, paymentDate, amount) VALUES (@LeaseID, @PaymentDate, @Amount)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LeaseID", lease.LeaseID); 
                    command.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
