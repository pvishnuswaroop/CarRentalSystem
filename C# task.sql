-- Step 1: Create the CarRentalDB database 
CREATE DATABASE CarRentalDB;
USE CarRentalDB;

-- Step 2: Create the Vehicle Table
CREATE TABLE Vehicle (
    vehicleID INT PRIMARY KEY,
    make NVARCHAR(50),
    model NVARCHAR(50),
    year INT,
    dailyRate DECIMAL(18, 2),
    status NVARCHAR(20),
    passengerCapacity INT,
    engineCapacity INT
);

-- Step 3: Create the Customer Table
CREATE TABLE Customer (
    customerID INT PRIMARY KEY,
    firstName NVARCHAR(50),
    lastName NVARCHAR(50),
    email NVARCHAR(100),
    phoneNumber NVARCHAR(15)
);

-- Step 4: Insert data into the Vehicle Table
INSERT INTO Vehicle (vehicleID, make, model, year, dailyRate, status, passengerCapacity, engineCapacity)
VALUES 
(1, 'Toyota', 'Corolla', 2020, 50.00, 'available', 5, 1800),
(2, 'Honda', 'Civic', 2019, 45.00, 'available', 5, 2000),
(3, 'Ford', 'Mustang', 2021, 100.00, 'available', 4, 3500),
(4, 'Chevrolet', 'Malibu', 2020, 55.00, 'available', 5, 2400),
(5, 'Nissan', 'Altima', 2018, 40.00, 'available', 5, 2000),
(6, 'Hyundai', 'Sonata', 2022, 60.00, 'available', 5, 2200),
(7, 'Kia', 'Optima', 2021, 58.00, 'available', 5, 2000),
(8, 'Mazda', '3', 2020, 52.00, 'available', 5, 1800),
(9, 'Subaru', 'Outback', 2022, 70.00, 'available', 5, 2500),
(10, 'Volkswagen', 'Jetta', 2019, 48.00, 'available', 5, 1800);

-- Step 5: Insert data into the Customer Table
INSERT INTO Customer (customerID, firstName, lastName, email, phoneNumber)
VALUES 
(1, 'John', 'Doe', 'john.doe@example.com', '1234567890'),
(2, 'Jane', 'Smith', 'jane.smith@example.com', '0987654321'),
(3, 'Michael', 'Johnson', 'michael.johnson@example.com', '5551234567'),
(4, 'Emily', 'Davis', 'emily.davis@example.com', '5559876543'),
(5, 'Daniel', 'Wilson', 'daniel.wilson@example.com', '5555555555'),
(6, 'Sophia', 'Moore', 'sophia.moore@example.com', '5556666666'),
(7, 'James', 'Taylor', 'james.taylor@example.com', '5557777777'),
(8, 'Olivia', 'Anderson', 'olivia.anderson@example.com', '5558888888'),
(9, 'Liam', 'Thomas', 'liam.thomas@example.com', '5559999999'),
(10, 'Ava', 'Jackson', 'ava.jackson@example.com', '5554444444');

-- Step 6: Create the Lease Table with leaseID as auto-incrementing identity
CREATE TABLE Lease (
    leaseID INT IDENTITY(1,1) PRIMARY KEY,  -- Identity column for auto-increment
    vehicleID INT FOREIGN KEY REFERENCES Vehicle(vehicleID),
    customerID INT FOREIGN KEY REFERENCES Customer(customerID),
    startDate DATE,
    endDate DATE,
    type NVARCHAR(20)
);

-- Step 7: Insert data into the Lease Table
INSERT INTO Lease (vehicleID, customerID, startDate, endDate, type)
VALUES 
(1, 1, '2024-01-01', '2024-01-07', 'DailyLease'), 
(2, 2, '2024-01-05', '2024-01-12', 'MonthlyLease'), 
(3, 1, '2024-01-10', '2024-01-15', 'DailyLease'), 
(4, 3, '2024-02-01', '2024-02-07', 'DailyLease'), 
(5, 4, '2024-02-05', '2024-02-19', 'MonthlyLease'), 
(6, 5, '2024-02-15', '2024-02-20', 'DailyLease'), 
(7, 6, '2024-02-18', '2024-02-25', 'MonthlyLease'), 
(8, 2, '2024-03-01', '2024-03-07', 'DailyLease'), 
(9, 7, '2024-03-10', '2024-03-15', 'DailyLease'), 
(10, 8, '2024-03-20', '2024-03-30', 'MonthlyLease');

-- Step 8: Create Payment Table and Insert data
CREATE TABLE Payment (
    paymentID INT PRIMARY KEY,
    leaseID INT FOREIGN KEY REFERENCES Lease(leaseID),
    paymentDate DATE,
    amount DECIMAL(18, 2)
);

-- Insert Payment Data
INSERT INTO Payment (paymentID, leaseID, paymentDate, amount)
VALUES 
(1, 1, '2024-01-01', 300.00),  -- Payment for Lease ID 1
(2, 2, '2024-01-05', 1350.00), -- Payment for Lease ID 2
(3, 3, '2024-01-10', 280.00),  -- Payment for Lease ID 3
(4, 4, '2024-02-01', 400.00),  -- Payment for Lease ID 4
(5, 5, '2024-02-05', 1300.00), -- Payment for Lease ID 5
(6, 6, '2024-02-15', 200.00),  -- Payment for Lease ID 6
(7, 7, '2024-02-18', 1200.00), -- Payment for Lease ID 7
(8, 8, '2024-03-01', 250.00),  -- Payment for Lease ID 8
(9, 9, '2024-03-10', 350.00),  -- Payment for Lease ID 9
(10, 10, '2024-03-20', 1500.00); -- Payment for Lease ID 10


SELECT * FROM Vehicle;
SELECT * FROM Customer;
SELECT * FROM Lease;
SELECT * FROM Payment;





