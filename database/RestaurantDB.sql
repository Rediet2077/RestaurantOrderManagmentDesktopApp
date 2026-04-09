CREATE DATABASE IF NOT EXISTS RestaurantDB;
USE RestaurantDB;

CREATE TABLE Users (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Role VARCHAR(50) NOT NULL,
    Username VARCHAR(50) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL
);

CREATE TABLE Customers (
    CustomerID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Phone VARCHAR(20)
);

CREATE TABLE Tables (
    TableID INT AUTO_INCREMENT PRIMARY KEY,
    Status VARCHAR(20) DEFAULT 'Available' -- Available, Reserved, Occupied
);

CREATE TABLE MenuItems (
    ItemID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Category VARCHAR(50),
    ImagePath VARCHAR(255)
);

CREATE TABLE Orders (
    OrderID INT AUTO_INCREMENT PRIMARY KEY,
    CustomerID INT,
    TableID INT,
    OrderDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    TotalAmount DECIMAL(10, 2) DEFAULT 0,
    Status VARCHAR(20) DEFAULT 'Pending', -- Pending, Paid, Cancelled
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (TableID) REFERENCES Tables(TableID)
);

CREATE TABLE OrderDetails (
    OrderDetailID INT AUTO_INCREMENT PRIMARY KEY,
    OrderID INT,
    ItemID INT,
    Quantity INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ItemID) REFERENCES MenuItems(ItemID)
);

CREATE TABLE Payments (
    PaymentID INT AUTO_INCREMENT PRIMARY KEY,
    OrderID INT,
    Amount DECIMAL(10, 2) NOT NULL,
    PaymentDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);

-- Seed initial data
INSERT INTO Tables (Status) VALUES ('Available'), ('Available'), ('Available'), ('Available'), ('Available');
INSERT INTO MenuItems (Name, Price, Category, ImagePath) VALUES 
('Premium Burger', 150.00, 'Main', 'Resources/burger.png'),
('Pizza Margherita', 250.00, 'Main', 'Resources/logo.png'), 
('Pasta Alfredo', 180.00, 'Main', 'Resources/logo.png'),
('Fresh Coke', 50.00, 'Drink', 'Resources/logo.png');
INSERT INTO Users (Name, Role, Username, Password) VALUES 
('Administrator', 'Admin', 'admin', 'admin123'),
('Staff User', 'User', 'user', 'user123');

