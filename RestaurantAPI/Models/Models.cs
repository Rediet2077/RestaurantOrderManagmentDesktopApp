namespace RestaurantAPI.Models
{
    // ─── Request DTOs ────────────────────────────────────────────────────

    public class LoginRequest
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class RegisterRequest
    {
        public string FullName { get; set; } = "";
        public string Username { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class MenuItemRequest
    {
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Category { get; set; } = "";
        public string ImagePath { get; set; } = "";
    }

    public class CreateOrderRequest
    {
        public int? CustomerID { get; set; }
        public int? TableID { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public List<OrderDetailItem> Items { get; set; } = new();
    }

    public class OrderDetailItem
    {
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateTableStatusRequest
    {
        public string Status { get; set; } = "";
    }

    public class AddStaffRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = "Staff";
    }

    public class ProcessPaymentRequest
    {
        public int OrderID { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = "Cash";
    }

    public class SaveSettingsRequest
    {
        public string RestaurantName { get; set; } = "LAUNCH";
        public string Currency { get; set; } = "Birr (ETB)";
        public string DarkMode { get; set; } = "False";
    }

    public class ReceiptRequest
    {
        public int OrderID { get; set; }
        public string ReceiptContent { get; set; } = "";
    }

    // ─── Response DTOs ───────────────────────────────────────────────────

    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; } = "";
        public string Role { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public int UserID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; } = "";
    }

    public class DashboardStats
    {
        public string TotalRevenue { get; set; } = "0.00";
        public int TotalOrders { get; set; }
        public int AvailableTables { get; set; }
        public int TotalTables { get; set; }
        public int ActiveStaff { get; set; }
        public int PendingOrders { get; set; }
        public int MenuItemCount { get; set; }
    }

    public class DailyReportRow
    {
        public string Date { get; set; } = "";
        public int TotalOrders { get; set; }
        public decimal TotalSales { get; set; }
    }

    // ─── Entity Models ───────────────────────────────────────────────────

    public class UserModel
    {
        public int UserID { get; set; }
        public string FullName { get; set; } = "";
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Role { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }

    public class MenuItemModel
    {
        public int ItemID { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Category { get; set; } = "";
        public string ImagePath { get; set; } = "";
        public bool IsAvailable { get; set; } = true;
    }

    public class TableModel
    {
        public int TableID { get; set; }
        public string TableNumber { get; set; } = "";
        public int Capacity { get; set; }
        public string Status { get; set; } = "";
    }

    public class OrderModel
    {
        public int OrderID { get; set; }
        public int? CustomerID { get; set; }
        public int? TableID { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "";
        public DateTime OrderDate { get; set; }
    }

    public class OrderDetailModel
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; } = "";
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class CustomerModel
    {
        public int CustomerID { get; set; }
        public string Name { get; set; } = "";
    }

    public class SettingsModel
    {
        public string RestaurantName { get; set; } = "LAUNCH";
        public string Currency { get; set; } = "Birr (ETB)";
        public string DarkMode { get; set; } = "False";
    }

    public class ReceiptModel
    {
        public int ReceiptID { get; set; }
        public int OrderID { get; set; }
        public string ReceiptContent { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
