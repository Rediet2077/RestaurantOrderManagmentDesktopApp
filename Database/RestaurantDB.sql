-- ========================================================
-- RestaurantDB Schema
-- Designed by the Backend Team
-- Updated for SQL Server
-- ========================================================

-- If you are running this in SSMS, uncomment the next two lines to create the DB first
-- CREATE DATABASE RestaurantDB;
-- GO
-- USE RestaurantDB;
-- GO

-- 1. Users Table (Stores Admins, Staff, and Registered Customers)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' and xtype='U')
BEGIN
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Username VARCHAR(50) UNIQUE,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Phone VARCHAR(20),
    Password VARCHAR(255) NOT NULL,
    Role VARCHAR(20) DEFAULT 'Customer' CHECK (Role IN ('Admin', 'Staff', 'Customer')),
    CreatedAt DATETIME DEFAULT GETDATE()
);
END

-- 2. MenuItems Table (simplified — Category stored as text)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='MenuItems' and xtype='U')
BEGIN
CREATE TABLE MenuItems (
    ItemID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Category VARCHAR(50),
    ImagePath VARCHAR(255),
    Description TEXT,
    IsAvailable BIT DEFAULT 1
);
END

-- 3. Tables (Physical restaurant tables)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Tables' and xtype='U')
BEGIN
CREATE TABLE Tables (
    TableID INT IDENTITY(1,1) PRIMARY KEY,
    TableNumber VARCHAR(10) NOT NULL UNIQUE,
    Capacity INT NOT NULL,
    Status VARCHAR(20) DEFAULT 'Available' CHECK (Status IN ('Available', 'Occupied', 'Reserved'))
);
END

-- 4. Orders Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Orders' and xtype='U')
BEGIN
CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NULL,
    TableID INT NULL,
    OrderType VARCHAR(20) DEFAULT 'Dine-In' CHECK (OrderType IN ('Dine-In', 'Takeout', 'Delivery')),
    TotalAmount DECIMAL(10, 2) NOT NULL,
    Status VARCHAR(20) DEFAULT 'Pending' CHECK (Status IN ('Pending', 'Preparing', 'Ready', 'Completed', 'Paid', 'Cancelled')),
    OrderDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CustomerID) REFERENCES Users(UserID) ON DELETE SET NULL,
    FOREIGN KEY (TableID) REFERENCES Tables(TableID) ON DELETE SET NULL
);
END

-- 5. OrderDetails Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='OrderDetails' and xtype='U')
BEGIN
CREATE TABLE OrderDetails (
    OrderDetailID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    ItemID INT NOT NULL,
    Quantity INT NOT NULL,
    Subtotal DECIMAL(10, 2) NOT NULL,
    SpecialInstructions VARCHAR(255),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID) ON DELETE CASCADE,
    FOREIGN KEY (ItemID) REFERENCES MenuItems(ItemID)
);
END

-- 6. Payments Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Payments' and xtype='U')
BEGIN
CREATE TABLE Payments (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    PaymentMethod VARCHAR(20) NOT NULL DEFAULT 'Cash' CHECK (PaymentMethod IN ('Cash', 'Card', 'MobileMoney')),
    Status VARCHAR(20) DEFAULT 'Successful' CHECK (Status IN ('Successful', 'Failed', 'Refunded')),
    PaymentDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);
END

-- 7. AppSettings Table (for system configuration)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AppSettings' and xtype='U')
BEGIN
CREATE TABLE AppSettings (
    SettingKey VARCHAR(100) PRIMARY KEY,
    SettingValue VARCHAR(255)
);
END
GO

-- ========================================================
-- Seed Data
-- ========================================================

-- Default Admin
IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'admin')
BEGIN
INSERT INTO Users (FullName, Username, Email, Password, Role) 
VALUES ('System Admin', 'admin', 'admin@gmail.com', '12345678', 'Admin');
END

-- Default Settings
IF NOT EXISTS (SELECT * FROM AppSettings WHERE SettingKey = 'RestaurantName')
    INSERT INTO AppSettings (SettingKey, SettingValue) VALUES ('RestaurantName', 'BEST RESTAURANTS');
IF NOT EXISTS (SELECT * FROM AppSettings WHERE SettingKey = 'Currency')
    INSERT INTO AppSettings (SettingKey, SettingValue) VALUES ('Currency', 'Birr (ETB)');
IF NOT EXISTS (SELECT * FROM AppSettings WHERE SettingKey = 'DarkMode')
    INSERT INTO AppSettings (SettingKey, SettingValue) VALUES ('DarkMode', 'False');

-- Sample Tables
IF NOT EXISTS (SELECT * FROM Tables)
BEGIN
INSERT INTO Tables (TableNumber, Capacity, Status) VALUES
('T1', 4, 'Available'),
('T2', 4, 'Available'),
('T3', 2, 'Available'),
('T4', 6, 'Available'),
('T5', 8, 'Available'),
('T6', 4, 'Available'),
('T7', 2, 'Available'),
('T8', 4, 'Available'),
('T9', 6, 'Available'),
('T10', 4, 'Available');
END

-- Sample Menu Items
IF NOT EXISTS (SELECT * FROM MenuItems)
BEGIN
INSERT INTO MenuItems (Name, Price, Category, ImagePath, IsAvailable) VALUES
('Chicken Burger', 120.00, 'Burgers', 'img/burger.jpg', 1),
('Beef Burger', 150.00, 'Burgers', 'img/burger.jpg', 1),
('Margherita Pizza', 200.00, 'Pizza', 'img/pizza.jpg', 1),
('Pepperoni Pizza', 250.00, 'Pizza', 'img/pizza.jpg', 1),
('Caesar Salad', 80.00, 'Salads', 'img/salad.jpg', 1),
('Coca Cola', 30.00, 'Beverages', '', 1),
('Fresh Juice', 50.00, 'Beverages', '', 1),
('Pasta Carbonara', 180.00, 'Pasta', '', 1),
('Grilled Fish', 220.00, 'Seafood', '', 1),
('Tibs (Special)', 280.00, 'Ethiopian', '', 1);
END
GO
