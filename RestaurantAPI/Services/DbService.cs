using Microsoft.Data.SqlClient;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class DbService
    {
        private readonly string _connectionString;

        public DbService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RestaurantDB")
                ?? "Server=(localdb)\\MSSQLLocalDB;Database=RestaurantDB;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=60;MultiSubnetFailover=True;";
        }

        private SqlConnection GetConnection() => new SqlConnection(_connectionString);

        // ── Auth ─────────────────────────────────────────────────────────

        public async Task<UserModel?> AuthenticateAsync(string email, string password)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand(
                "SELECT UserID, FullName, Username, Email, Phone, Role, CreatedAt FROM Users WHERE Email=@email AND Password=@password", conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new UserModel
                {
                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                    FullName = reader.IsDBNull(reader.GetOrdinal("FullName")) ? "" : reader.GetString(reader.GetOrdinal("FullName")),
                    Username = reader.IsDBNull(reader.GetOrdinal("Username")) ? "" : reader.GetString(reader.GetOrdinal("Username")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? "" : reader.GetString(reader.GetOrdinal("Phone")),
                    Role = reader.GetString(reader.GetOrdinal("Role")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                };
            }
            return null;
        }

        public async Task<bool> RegisterUserAsync(RegisterRequest req)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            // Check if email already exists
            using var checkCmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email=@email", conn);
            checkCmd.Parameters.AddWithValue("@email", req.Email);
            var count = Convert.ToInt32(await checkCmd.ExecuteScalarAsync());
            if (count > 0) return false;

            using var cmd = new SqlCommand(
                "INSERT INTO Users (FullName, Username, Phone, Email, Password, Role) VALUES (@fn, @un, @ph, @em, @pw, 'Customer')", conn);
            cmd.Parameters.AddWithValue("@fn", req.FullName);
            cmd.Parameters.AddWithValue("@un", string.IsNullOrEmpty(req.Username) ? (object)DBNull.Value : req.Username);
            cmd.Parameters.AddWithValue("@ph", string.IsNullOrEmpty(req.Phone) ? (object)DBNull.Value : req.Phone);
            cmd.Parameters.AddWithValue("@em", req.Email);
            cmd.Parameters.AddWithValue("@pw", req.Password);
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task EnsureDatabaseSchemaAsync()
        {
            try
            {
                using var conn = GetConnection();
                await conn.OpenAsync();

                string sql = @"
-- 1. Users Table
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

-- 2. MenuItems Table
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

-- 3. Tables
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
    OrderType VARCHAR(20) DEFAULT 'Dine-In',
    TotalAmount DECIMAL(10, 2) NOT NULL,
    Status VARCHAR(20) DEFAULT 'Pending',
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
    PaymentMethod VARCHAR(20) NOT NULL DEFAULT 'Cash',
    Status VARCHAR(20) DEFAULT 'Successful',
    PaymentDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);
END

-- 7. AppSettings Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AppSettings' and xtype='U')
BEGIN
CREATE TABLE AppSettings (
    SettingKey VARCHAR(100) PRIMARY KEY,
    SettingValue VARCHAR(255)
);
END

-- 8. Receipts Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Receipts' and xtype='U')
BEGIN
CREATE TABLE Receipts (
    ReceiptID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    ReceiptContent TEXT NOT NULL,
    SentToAdmin BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);
END

-- Seed Default Admin
IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'admin')
BEGIN
INSERT INTO Users (FullName, Username, Email, Password, Role) 
VALUES ('System Admin', 'admin', 'admin@gmail.com', '12345678', 'Admin');
END

-- Seed Default Settings
IF NOT EXISTS (SELECT * FROM AppSettings WHERE SettingKey = 'RestaurantName')
    INSERT INTO AppSettings (SettingKey, SettingValue) VALUES ('RestaurantName', 'BEST RESTAURANTS');

-- Force rebranding for existing databases
UPDATE AppSettings SET SettingValue = 'BEST RESTAURANTS' WHERE SettingValue LIKE '%DBU%';
UPDATE Receipts SET ReceiptContent = REPLACE(CAST(ReceiptContent AS VARCHAR(MAX)), 'DBU Restaurant', 'BEST Restaurant');
UPDATE Receipts SET ReceiptContent = REPLACE(CAST(ReceiptContent AS VARCHAR(MAX)), 'DBU RESTAURANT', 'BEST RESTAURANT');

IF NOT EXISTS (SELECT * FROM AppSettings WHERE SettingKey = 'Currency')
    INSERT INTO AppSettings (SettingKey, SettingValue) VALUES ('Currency', 'Birr (ETB)');
IF NOT EXISTS (SELECT * FROM AppSettings WHERE SettingKey = 'DarkMode')
    INSERT INTO AppSettings (SettingKey, SettingValue) VALUES ('DarkMode', 'False');

-- Seed Sample Tables if empty
IF NOT EXISTS (SELECT * FROM Tables)
BEGIN
    INSERT INTO Tables (TableNumber, Capacity, Status) VALUES
    ('Counter', 0, 'Available'),
    ('Table 1', 4, 'Available'), 
    ('Table 2', 4, 'Available'), 
    ('Table 3', 2, 'Available'), 
    ('Table 4', 6, 'Available'),
    ('Table 5', 4, 'Available');
END

-- Seed Premium Menu Items individually if they don't exist
IF NOT EXISTS (SELECT * FROM MenuItems WHERE Name = 'Special Doro Wat')
    INSERT INTO MenuItems (Name, Description, Price, Category, ImagePath, IsAvailable) VALUES ('Special Doro Wat', 'Traditional Ethiopian spicy chicken stew', 120.00, 'Ethiopian', 'https://images.unsplash.com/photo-1585937421612-70a008356fbe?w=800', 1);
IF NOT EXISTS (SELECT * FROM MenuItems WHERE Name = 'Premium Ribeye Steak')
    INSERT INTO MenuItems (Name, Description, Price, Category, ImagePath, IsAvailable) VALUES ('Premium Ribeye Steak', 'Grilled steak with herb butter', 250.00, 'International', 'https://images.unsplash.com/photo-1600891964599-f61ba0e24092?w=800', 1);
IF NOT EXISTS (SELECT * FROM MenuItems WHERE Name = 'Gourmet Wagyu Burger')
    INSERT INTO MenuItems (Name, Description, Price, Category, ImagePath, IsAvailable) VALUES ('Gourmet Wagyu Burger', 'Double wagyu beef with truffle aioli', 180.00, 'International', 'https://images.unsplash.com/photo-1568901346375-23c9450c58cd?w=800', 1);
IF NOT EXISTS (SELECT * FROM MenuItems WHERE Name = 'Pasta Carbonara')
    INSERT INTO MenuItems (Name, Description, Price, Category, ImagePath, IsAvailable) VALUES ('Pasta Carbonara', 'Creamy Italian pasta', 140.00, 'International', 'https://images.unsplash.com/photo-1612874742237-6526221588e3?w=800', 1);
IF NOT EXISTS (SELECT * FROM MenuItems WHERE Name = 'Traditional Coffee')
    INSERT INTO MenuItems (Name, Description, Price, Category, ImagePath, IsAvailable) VALUES ('Traditional Coffee', 'Authentic Ethiopian coffee ceremony', 25.00, 'Beverages', 'https://images.unsplash.com/photo-1559525839-b184a4d698c7?w=800', 1);

-- Fix existing images to use premium web URLs if they are local/missing
UPDATE MenuItems SET ImagePath = 'https://images.unsplash.com/photo-1568901346375-23c9450c58cd?w=800' WHERE Name LIKE '%Burger%' AND (ImagePath IS NULL OR ImagePath NOT LIKE 'http%');
UPDATE MenuItems SET ImagePath = 'https://images.unsplash.com/photo-1513104890138-7c749659a591?w=800' WHERE Name LIKE '%Pizza%' AND (ImagePath IS NULL OR ImagePath NOT LIKE 'http%');
UPDATE MenuItems SET ImagePath = 'https://images.unsplash.com/photo-1585937421612-70a008356fbe?w=800' WHERE Name LIKE '%Doro%' OR Name LIKE '%Ethiopian%' OR Name LIKE '%Tibs%';
UPDATE MenuItems SET ImagePath = 'https://images.unsplash.com/photo-1612874742237-6526221588e3?w=800' WHERE Name LIKE '%Pasta%' OR Name LIKE '%Carbonara%';
UPDATE MenuItems SET ImagePath = 'https://images.unsplash.com/photo-1546069901-ba9599a7e63c?w=800' WHERE Name LIKE '%Fish%' OR Name LIKE '%Seafood%';
UPDATE MenuItems SET ImagePath = 'https://images.unsplash.com/photo-1559525839-b184a4d698c7?w=800' WHERE Name LIKE '%Coffee%' OR Name LIKE '%Drink%' OR Name LIKE '%Coca%';
UPDATE MenuItems SET ImagePath = 'https://images.unsplash.com/photo-1482049016688-2d3e1b311543?w=800' WHERE Name LIKE '%Juice%' OR Name LIKE '%Fresh%';

-- Set a default high-quality food image for anything else missing an image
UPDATE MenuItems SET ImagePath = 'https://images.unsplash.com/photo-1540189549336-e6e99c3679fe?w=800' WHERE ImagePath IS NULL OR ImagePath = '' OR ImagePath NOT LIKE 'http%';
";
                using var cmd = new SqlCommand(sql, conn);
                await cmd.ExecuteNonQueryAsync();
                Console.WriteLine("Database schema initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database initialization failed: {ex.Message}");
            }
        }

        public async Task<int> DeleteAllCustomersAsync()
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand("DELETE FROM Users WHERE Role='Customer'", conn);
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<MenuItemModel>> GetMenuItemsAsync()
        {
            var items = new List<MenuItemModel>();
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand("SELECT ItemID, Name, Price, Category, ImagePath, IsAvailable FROM MenuItems", conn);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                items.Add(new MenuItemModel
                {
                    ItemID = reader.GetInt32(reader.GetOrdinal("ItemID")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                    Category = reader.IsDBNull(reader.GetOrdinal("Category")) ? "" : reader.GetString(reader.GetOrdinal("Category")),
                    ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? "" : reader.GetString(reader.GetOrdinal("ImagePath")),
                    IsAvailable = reader.GetBoolean(reader.GetOrdinal("IsAvailable"))
                });
            }
            return items;
        }

        public async Task<bool> AddMenuItemAsync(MenuItemRequest req)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand(
                "INSERT INTO MenuItems (Name, Price, Category, ImagePath, IsAvailable) VALUES (@nm, @pr, @cat, @img, 1)", conn);
            cmd.Parameters.AddWithValue("@nm", req.Name);
            cmd.Parameters.AddWithValue("@pr", req.Price);
            cmd.Parameters.AddWithValue("@cat", req.Category);
            cmd.Parameters.AddWithValue("@img", req.ImagePath);
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateMenuItemAsync(int id, MenuItemRequest req)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand(
                "UPDATE MenuItems SET Name=@nm, Price=@pr, Category=@cat, ImagePath=@img WHERE ItemID=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nm", req.Name);
            cmd.Parameters.AddWithValue("@pr", req.Price);
            cmd.Parameters.AddWithValue("@cat", req.Category);
            cmd.Parameters.AddWithValue("@img", req.ImagePath);
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteMenuItemAsync(int id)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand("DELETE FROM MenuItems WHERE ItemID=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<List<TableModel>> GetTablesAsync(string? status = null)
        {
            var tables = new List<TableModel>();
            using var conn = GetConnection();
            await conn.OpenAsync();
            string sql = "SELECT TableID, TableNumber, Capacity, Status FROM Tables";
            if (!string.IsNullOrEmpty(status)) sql += " WHERE Status = @st";
            
            using var cmd = new SqlCommand(sql, conn);
            if (!string.IsNullOrEmpty(status)) cmd.Parameters.AddWithValue("@st", status);
            
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                tables.Add(new TableModel
                {
                    TableID = reader.GetInt32(reader.GetOrdinal("TableID")),
                    TableNumber = reader.GetString(reader.GetOrdinal("TableNumber")),
                    Capacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
                    Status = reader.GetString(reader.GetOrdinal("Status"))
                });
            }
            return tables;
        }

        public async Task<bool> UpdateTableStatusAsync(int id, string status)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand("UPDATE Tables SET Status=@st WHERE TableID=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@st", status);
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<int> CreateOrderAsync(CreateOrderRequest req)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var trans = conn.BeginTransaction();
            try
            {
                using var cmd = new SqlCommand(
                    "INSERT INTO Orders (CustomerID, TableID, TotalAmount, Status) OUTPUT INSERTED.OrderID VALUES (@cid, @tid, @tot, @st)", conn, trans);
                cmd.Parameters.AddWithValue("@cid", req.CustomerID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@tid", req.TableID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@tot", req.TotalAmount);
                cmd.Parameters.AddWithValue("@st", req.Status);
                int orderId = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                foreach (var item in req.Items)
                {
                    using var dCmd = new SqlCommand(
                        "INSERT INTO OrderDetails (OrderID, ItemID, Quantity, Subtotal) VALUES (@oid, @iid, @qty, @sub)", conn, trans);
                    dCmd.Parameters.AddWithValue("@oid", orderId);
                    dCmd.Parameters.AddWithValue("@iid", item.ItemID);
                    dCmd.Parameters.AddWithValue("@qty", item.Quantity);
                    dCmd.Parameters.AddWithValue("@sub", item.Price * item.Quantity);
                    await dCmd.ExecuteNonQueryAsync();
                }

                // If table is selected, mark it as Reserved or Occupied
                if (req.TableID.HasValue)
                {
                    using var tCmd = new SqlCommand("UPDATE Tables SET Status='Reserved' WHERE TableID=@tid", conn, trans);
                    tCmd.Parameters.AddWithValue("@tid", req.TableID.Value);
                    await tCmd.ExecuteNonQueryAsync();
                }

                trans.Commit();
                return orderId;
            }
            catch
            {
                trans.Rollback();
                return -1;
            }
        }

        public async Task<List<OrderModel>> GetPendingOrdersAsync()
        {
            var orders = new List<OrderModel>();
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand("SELECT OrderID, TotalAmount, Status, OrderDate FROM Orders WHERE Status='Pending'", conn);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                orders.Add(new OrderModel
                {
                    OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                    TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                    Status = reader.GetString(reader.GetOrdinal("Status")),
                    OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate"))
                });
            }
            return orders;
        }

        public async Task<List<OrderModel>> GetCustomerOrdersAsync(int userId)
        {
            var orders = new List<OrderModel>();
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand("SELECT OrderID, TotalAmount, Status, OrderDate FROM Orders WHERE CustomerID=@uid ORDER BY OrderDate DESC", conn);
            cmd.Parameters.AddWithValue("@uid", userId);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                orders.Add(new OrderModel
                {
                    OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                    TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                    Status = reader.GetString(reader.GetOrdinal("Status")),
                    OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate"))
                });
            }
            return orders;
        }

        public async Task<List<OrderDetailModel>> GetOrderDetailsAsync(int orderId)
        {
            var details = new List<OrderDetailModel>();
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand(@"
                SELECT od.ItemID, m.Name, od.Quantity, od.Subtotal 
                FROM OrderDetails od
                JOIN MenuItems m ON od.ItemID = m.ItemID
                WHERE od.OrderID = @oid", conn);
            cmd.Parameters.AddWithValue("@oid", orderId);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                details.Add(new OrderDetailModel
                {
                    ItemID = reader.GetInt32(reader.GetOrdinal("ItemID")),
                    ItemName = reader.GetString(reader.GetOrdinal("Name")),
                    Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                    Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal"))
                });
            }
            return details;
        }

        public async Task<List<CustomerModel>> GetCustomersAsync()
        {
            var customers = new List<CustomerModel>();
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand("SELECT UserID, FullName FROM Users WHERE Role='Customer'", conn);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                customers.Add(new CustomerModel
                {
                    CustomerID = reader.GetInt32(reader.GetOrdinal("UserID")),
                    Name = reader.GetString(reader.GetOrdinal("FullName"))
                });
            }
            return customers;
        }

        public async Task<bool> ProcessPaymentAsync(ProcessPaymentRequest req)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var trans = conn.BeginTransaction();
            try
            {
                using var cmd = new SqlCommand(
                    "INSERT INTO Payments (OrderID, Amount, PaymentMethod) VALUES (@oid, @amt, @met)", conn, trans);
                cmd.Parameters.AddWithValue("@oid", req.OrderID);
                cmd.Parameters.AddWithValue("@amt", req.Amount);
                cmd.Parameters.AddWithValue("@met", req.PaymentMethod);
                await cmd.ExecuteNonQueryAsync();

                using var oCmd = new SqlCommand("UPDATE Orders SET Status='Paid' WHERE OrderID=@oid", conn, trans);
                oCmd.Parameters.AddWithValue("@oid", req.OrderID);
                await oCmd.ExecuteNonQueryAsync();
                
                // Set table back to Available
                using var tCmd = new SqlCommand("UPDATE Tables SET Status='Available' WHERE TableID = (SELECT TableID FROM Orders WHERE OrderID=@oid)", conn, trans);
                tCmd.Parameters.AddWithValue("@oid", req.OrderID);
                await tCmd.ExecuteNonQueryAsync();

                trans.Commit();
                return true;
            }
            catch
            {
                trans.Rollback();
                return false;
            }
        }

        public async Task<bool> AddReceiptAsync(ReceiptRequest req)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand(
                "INSERT INTO Receipts (OrderID, ReceiptContent) VALUES (@oid, @con)", conn);
            cmd.Parameters.AddWithValue("@oid", req.OrderID);
            cmd.Parameters.AddWithValue("@con", req.ReceiptContent);
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<List<ReceiptModel>> GetReceiptsAsync()
        {
            var receipts = new List<ReceiptModel>();
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand("SELECT ReceiptID, OrderID, ReceiptContent, CreatedAt FROM Receipts ORDER BY CreatedAt DESC", conn);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                receipts.Add(new ReceiptModel
                {
                    ReceiptID = reader.GetInt32(reader.GetOrdinal("ReceiptID")),
                    OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                    ReceiptContent = reader.GetString(reader.GetOrdinal("ReceiptContent")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                });
            }
            return receipts;
        }

        public async Task<List<UserModel>> GetStaffAsync()
        {
            var users = new List<UserModel>();
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand("SELECT UserID, FullName, Username, Email, Phone, Role FROM Users WHERE Role != 'Customer'", conn);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                users.Add(new UserModel
                {
                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                    FullName = reader.IsDBNull(reader.GetOrdinal("FullName")) ? "" : reader.GetString(reader.GetOrdinal("FullName")),
                    Username = reader.IsDBNull(reader.GetOrdinal("Username")) ? "" : reader.GetString(reader.GetOrdinal("Username")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? "" : reader.GetString(reader.GetOrdinal("Phone")),
                    Role = reader.GetString(reader.GetOrdinal("Role"))
                });
            }
            return users;
        }

        public async Task<bool> AddStaffAsync(AddStaffRequest req)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand(
                "INSERT INTO Users (FullName, Username, Password, Role, Email) VALUES (@un, @un, @pw, @rl, @un + '@staff.com')", conn);
            cmd.Parameters.AddWithValue("@un", req.Username);
            cmd.Parameters.AddWithValue("@pw", req.Password);
            cmd.Parameters.AddWithValue("@rl", req.Role);
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteStaffAsync(string username)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand("DELETE FROM Users WHERE Username=@un", conn);
            cmd.Parameters.AddWithValue("@un", username);
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<DashboardStats> GetDashboardStatsAsync()
        {
            var stats = new DashboardStats();
            using var conn = GetConnection();
            await conn.OpenAsync();

            using (var cmd = new SqlCommand("SELECT SUM(TotalAmount) FROM Orders WHERE Status='Paid'", conn))
                stats.TotalRevenue = (await cmd.ExecuteScalarAsync() as decimal? ?? 0).ToString("N2");

            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Orders", conn))
                stats.TotalOrders = Convert.ToInt32(await cmd.ExecuteScalarAsync());

            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Tables WHERE Status='Available'", conn))
                stats.AvailableTables = Convert.ToInt32(await cmd.ExecuteScalarAsync());

            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Tables", conn))
                stats.TotalTables = Convert.ToInt32(await cmd.ExecuteScalarAsync());

            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Role='Staff' OR Role='Admin'", conn))
                stats.ActiveStaff = Convert.ToInt32(await cmd.ExecuteScalarAsync());

            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Orders WHERE Status='Pending'", conn))
                stats.PendingOrders = Convert.ToInt32(await cmd.ExecuteScalarAsync());

            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM MenuItems", conn))
                stats.MenuItemCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());

            return stats;
        }

        public async Task<List<DailyReportRow>> GetDailyReportAsync()
        {
            var rows = new List<DailyReportRow>();
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand(@"
                SELECT 
                    CAST(OrderDate AS DATE) as ReportDate, 
                    COUNT(OrderID) as TotalOrders, 
                    SUM(TotalAmount) as TotalSales 
                FROM Orders 
                WHERE Status = 'Paid' 
                AND CAST(OrderDate AS DATE) = CAST(GETDATE() AS DATE)
                GROUP BY CAST(OrderDate AS DATE)", conn);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                rows.Add(new DailyReportRow
                {
                    Date = reader.GetDateTime(reader.GetOrdinal("ReportDate")).ToString("yyyy-MM-dd"),
                    TotalOrders = reader.GetInt32(reader.GetOrdinal("TotalOrders")),
                    TotalSales = reader.GetDecimal(reader.GetOrdinal("TotalSales"))
                });
            }
            return rows;
        }

        // ── Settings ─────────────────────────────────────────────────────

        public async Task InitializeSettingsAsync()
        {
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand(@"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AppSettings' and xtype='U')
                BEGIN
                    CREATE TABLE AppSettings (
                        SettingKey VARCHAR(100) PRIMARY KEY, 
                        SettingValue VARCHAR(255)
                    );
                END
                IF NOT EXISTS (SELECT * FROM AppSettings WHERE SettingKey = 'RestaurantName')
                    INSERT INTO AppSettings (SettingKey, SettingValue) VALUES ('RestaurantName', 'BEST RESTAURANTS');
                IF NOT EXISTS (SELECT * FROM AppSettings WHERE SettingKey = 'Currency')
                    INSERT INTO AppSettings (SettingKey, SettingValue) VALUES ('Currency', 'Birr (ETB)');
                IF NOT EXISTS (SELECT * FROM AppSettings WHERE SettingKey = 'DarkMode')
                    INSERT INTO AppSettings (SettingKey, SettingValue) VALUES ('DarkMode', 'False');", conn);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<SettingsModel> GetSettingsAsync()
        {
            await InitializeSettingsAsync();
            var settings = new SettingsModel();
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand("SELECT SettingKey, SettingValue FROM AppSettings", conn);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                string key = reader.GetString(reader.GetOrdinal("SettingKey"));
                string val = reader.GetString(reader.GetOrdinal("SettingValue"));
                if (key == "RestaurantName") settings.RestaurantName = val;
                else if (key == "Currency") settings.Currency = val;
                else if (key == "DarkMode") settings.DarkMode = val;
            }
            return settings;
        }

        public async Task SaveSettingsAsync(SaveSettingsRequest req)
        {
            await InitializeSettingsAsync();
            using var conn = GetConnection();
            await conn.OpenAsync();
            using var cmd = new SqlCommand(@"
                UPDATE AppSettings SET SettingValue = @name WHERE SettingKey = 'RestaurantName';
                UPDATE AppSettings SET SettingValue = @curr WHERE SettingKey = 'Currency';
                UPDATE AppSettings SET SettingValue = @dark WHERE SettingKey = 'DarkMode';", conn);
            cmd.Parameters.AddWithValue("@name", req.RestaurantName);
            cmd.Parameters.AddWithValue("@curr", req.Currency);
            cmd.Parameters.AddWithValue("@dark", req.DarkMode);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
