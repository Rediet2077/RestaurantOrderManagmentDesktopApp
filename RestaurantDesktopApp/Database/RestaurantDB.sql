-- ========================================================
-- RestaurantDB Schema
-- Designed by the Backend Team
-- ========================================================

CREATE DATABASE IF NOT EXISTS RestaurantDB;
USE RestaurantDB;

-- 1. Users Table (Stores Admins, Staff, and Registered Customers)
CREATE TABLE IF NOT EXISTS Users (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Username VARCHAR(50) UNIQUE,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Phone VARCHAR(20),
    Password VARCHAR(255) NOT NULL,
    Role ENUM('Admin', 'Staff', 'Customer') DEFAULT 'Customer',
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- 2. MenuCategories Table
CREATE TABLE IF NOT EXISTS MenuCategories (
    CategoryID INT AUTO_INCREMENT PRIMARY KEY,
    CategoryName VARCHAR(50) NOT NULL UNIQUE
);

-- 3. MenuItems Table
CREATE TABLE IF NOT EXISTS MenuItems (
    ItemID INT AUTO_INCREMENT PRIMARY KEY,
    CategoryID INT,
    Name VARCHAR(100) NOT NULL,
    Description TEXT,
    Price DECIMAL(10, 2) NOT NULL,
    ImageURL VARCHAR(255),
    IsAvailable BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (CategoryID) REFERENCES MenuCategories(CategoryID) ON DELETE SET NULL
);

-- 4. Tables List (Physical restaurant tables)
CREATE TABLE IF NOT EXISTS Tables (
    TableID INT AUTO_INCREMENT PRIMARY KEY,
    TableNumber VARCHAR(10) NOT NULL UNIQUE,
    Capacity INT NOT NULL,
    Status ENUM('Available', 'Occupied', 'Reserved') DEFAULT 'Available'
);

-- 5. Orders Table
CREATE TABLE IF NOT EXISTS Orders (
    OrderID INT AUTO_INCREMENT PRIMARY KEY,
    CustomerID INT,      -- NULL if Guest
    TableID INT,         -- NULL if Takeout
    OrderType ENUM('Dine-In', 'Takeout', 'Delivery') DEFAULT 'Dine-In',
    TotalAmount DECIMAL(10, 2) NOT NULL,
    Status ENUM('Pending', 'Preparing', 'Ready', 'Completed', 'Cancelled') DEFAULT 'Pending',
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CustomerID) REFERENCES Users(UserID) ON DELETE SET NULL,
    FOREIGN KEY (TableID) REFERENCES Tables(TableID) ON DELETE SET NULL
);

-- 6. OrderDetails Table
CREATE TABLE IF NOT EXISTS OrderDetails (
    OrderDetailID INT AUTO_INCREMENT PRIMARY KEY,
    OrderID INT NOT NULL,
    ItemID INT NOT NULL,
    Quantity INT NOT NULL,
    Subtotal DECIMAL(10, 2) NOT NULL,
    SpecialInstructions VARCHAR(255),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID) ON DELETE CASCADE,
    FOREIGN KEY (ItemID) REFERENCES MenuItems(ItemID)
);

-- 7. Payments Table
CREATE TABLE IF NOT EXISTS Payments (
    PaymentID INT AUTO_INCREMENT PRIMARY KEY,
    OrderID INT NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    PaymentMethod ENUM('Cash', 'Card', 'MobileMoney') NOT NULL,
    Status ENUM('Successful', 'Failed', 'Refunded') DEFAULT 'Successful',
    PaymentDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);

-- Insert Default Admin
INSERT IGNORE INTO Users (FullName, Username, Email, Password, Role) 
VALUES ('System Admin', 'admin', 'admin@gmail.com', '12345678', 'Admin');
