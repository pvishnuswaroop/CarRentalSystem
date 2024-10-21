using CarRentalLibrary.entity;
using System;
using System.Collections.Generic;

namespace CarRentalLibrary.dao
{
    // Interface for car lease repository, defining methods for managing cars, customers, leases, and payments
    public interface ICarLeaseRepository
    {
        // Car Management
        void AddCar(Car car);
        void RemoveCar(int carID);
        List<Car> ListAvailableCars();
        List<Car> ListRentedCars();
        Car FindCarById(int carID);

        // Customer Management
        void AddCustomer(Customer customer);
        void RemoveCustomer(int customerID);
        List<Customer> ListCustomers();
        Customer FindCustomerById(int customerID);

        // Lease Management
        Lease CreateLease(int customerID, int carID, DateTime startDate, DateTime endDate);
        Lease ReturnCar(int leaseID);
        List<Lease> ListActiveLeases();
        List<Lease> ListLeaseHistory();
        Lease GetLeaseById(int leaseId);
        Lease FindLeaseById(int leaseID);

        // Payment Handling
        void RecordPayment(Lease lease, double amount);

        // New method to remove leases by Car ID
        void RemoveLeasesByCarId(int carID);
    }
}
