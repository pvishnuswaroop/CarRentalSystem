using System;
using NUnit.Framework;
using CarRentalLibrary.dao;
using CarRentalLibrary.entity;
using CarRentalLibrary.exception;

namespace CarRentalTests
{
    [TestFixture] // Indicates that this class contains tests
    public class CarRentalTests
    {
        private ICarLeaseRepository _repository;

        [SetUp] // This method runs before each test
        public void Setup()
        {
            _repository = new CarLeaseRepositoryImpl(); // Initialize the repository
            
        }

        [Test] // Marks this method as a test case
        public void Test_CarCreatedSuccessfully()
        {
            
            var newCar = new Car
            {
                VehicleID = 13, // Ensure this ID does not conflict with existing IDs
                Make = "Toyota",
                Model = "Corolla",
                Year = 2020,
                DailyRate = 50.00m,
                Status = "available",
                PassengerCapacity = 5,
                EngineCapacity = 1800
            };

            //Add the car to the repository
            _repository.AddCar(newCar);

            //Retrieve the car and check if it was added successfully
            var retrievedCar = _repository.FindCarById(newCar.VehicleID);
            Assert.IsNotNull(retrievedCar);
            Assert.AreEqual(newCar.Make, retrievedCar.Make);
            Assert.AreEqual(newCar.Model, retrievedCar.Model);
            Assert.AreEqual(newCar.Year, retrievedCar.Year);
        }

        [Test]
        public void Test_LeaseCreatedSuccessfully()
        {
            
            var customerID = 1; // Assuming a customer with ID 1 exists
            var carID = 1;      // Assuming a car with ID 1 exists
            var startDate = new DateTime(2024, 10, 15); 
            var endDate = startDate.AddDays(7); // Calculate end date as one week later

            // Create a lease
            var createdLease = _repository.CreateLease(customerID, carID, startDate, endDate);

            //Retrieve the lease using the created LeaseID
            var retrievedLease = _repository.GetLeaseById(createdLease.LeaseID); // Retrieve using the newly created LeaseID
            Assert.IsNotNull(retrievedLease);
            Assert.AreEqual(customerID, retrievedLease.CustomerID);
            Assert.AreEqual(carID, retrievedLease.VehicleID);
            Assert.AreEqual(startDate.Date, retrievedLease.StartDate.Date); 
            Assert.AreEqual(endDate.Date, retrievedLease.EndDate.Date); 
        }

        [Test]
        public void Test_LeaseRetrievedSuccessfully()
        {
            
            var leaseId = 1; // Assuming a lease with ID 1 exists

            //Retrieve the lease by ID
            var lease = _repository.GetLeaseById(leaseId);

            //Check that the lease was found
            Assert.IsNotNull(lease);
            Assert.AreEqual(leaseId, lease.LeaseID); 
        }

        [Test]
        public void Test_ExceptionThrown_When_CarIdNotFound()
        {
            
            int nonExistentCarId = 999;

            //Check that an exception is thrown when trying to find a non-existent car
            var ex = Assert.Throws<CarNotFoundException>(() => _repository.FindCarById(nonExistentCarId));
            Assert.That(ex.Message, Is.EqualTo("Car with ID 999 not found.")); 
        }

        [Test]
        public void Test_ExceptionThrown_When_CustomerIdNotFound()
        {
            
            int nonExistentCustomerId = 999;

            //Check that an exception is thrown when trying to find a non-existent customer
            var ex = Assert.Throws<CustomerNotFoundException>(() => _repository.FindCustomerById(nonExistentCustomerId)); 
            Assert.That(ex.Message, Is.EqualTo("Customer with ID 999 not found.")); 
        }
    }
}
