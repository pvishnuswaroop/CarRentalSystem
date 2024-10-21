using System;
using NUnit.Framework;
using CarRentalLibrary.dao;
using CarRentalLibrary.entity;
using CarRentalLibrary.exception;

namespace CarRentalTests
{
    [TestFixture] 
    public class CarRentalTests
    {
        private ICarLeaseRepository _repository;

        [SetUp] 
        public void Setup()
        {
            _repository = new CarLeaseRepositoryImpl(); 
            
        }

        [Test] 
        public void Test_CarCreatedSuccessfully()
        {
            
            var newCar = new Car
            {
                VehicleID = 13, 
                Make = "Toyota",
                Model = "Corolla",
                Year = 2020,
                DailyRate = 50.00m,
                Status = "available",
                PassengerCapacity = 5,
                EngineCapacity = 1800
            };

            
            _repository.AddCar(newCar);

            
            var retrievedCar = _repository.FindCarById(newCar.VehicleID);
            Assert.IsNotNull(retrievedCar);
            Assert.AreEqual(newCar.Make, retrievedCar.Make);
            Assert.AreEqual(newCar.Model, retrievedCar.Model);
            Assert.AreEqual(newCar.Year, retrievedCar.Year);
        }

        [Test]
        public void Test_LeaseCreatedSuccessfully()
        {
            
            var customerID = 1; 
            var carID = 1;      
            var startDate = new DateTime(2024, 10, 15); 
            var endDate = startDate.AddDays(7); 

            
            var createdLease = _repository.CreateLease(customerID, carID, startDate, endDate);

            
            var retrievedLease = _repository.GetLeaseById(createdLease.LeaseID); 
            Assert.IsNotNull(retrievedLease);
            Assert.AreEqual(customerID, retrievedLease.CustomerID);
            Assert.AreEqual(carID, retrievedLease.VehicleID);
            Assert.AreEqual(startDate.Date, retrievedLease.StartDate.Date); 
            Assert.AreEqual(endDate.Date, retrievedLease.EndDate.Date); 
        }

        [Test]
        public void Test_LeaseRetrievedSuccessfully()
        {
            
            var leaseId = 1;

            
            var lease = _repository.GetLeaseById(leaseId);

            
            Assert.IsNotNull(lease);
            Assert.AreEqual(leaseId, lease.LeaseID); 
        }

        [Test]
        public void Test_ExceptionThrown_When_CarIdNotFound()
        {
            
            int nonExistentCarId = 999;

            
            var ex = Assert.Throws<CarNotFoundException>(() => _repository.FindCarById(nonExistentCarId));
            Assert.That(ex.Message, Is.EqualTo("Car with ID 999 not found.")); 
        }

        [Test]
        public void Test_ExceptionThrown_When_CustomerIdNotFound()
        {
            
            int nonExistentCustomerId = 999;

            
            var ex = Assert.Throws<CustomerNotFoundException>(() => _repository.FindCustomerById(nonExistentCustomerId)); 
            Assert.That(ex.Message, Is.EqualTo("Customer with ID 999 not found.")); 
        }
    }
}
