using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestaurantDesktopApp
{
    /// <summary>
    /// Central HTTP client that communicates with the RestaurantAPI backend.
    /// All forms should use this instead of direct MySQL connections.
    /// </summary>
    public static class ApiClient
    {
        private static readonly HttpClient _http;
        public static string BaseUrl { get; set; } = "http://localhost:49393/api";

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        static ApiClient()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl.TrimEnd('/') + "/"),
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        // ── Auth ─────────────────────────────────────────────────────────

        public static async Task<LoginResponse?> LoginAsync(string email, string password)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("auth/login", new { Email = email, Password = password });
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<LoginResponse>(_jsonOptions);
                
                var errorResult = await response.Content.ReadFromJsonAsync<LoginResponse>(_jsonOptions);
                return errorResult ?? new LoginResponse { Success = false, Message = "Invalid email or password." };
            }
            catch (HttpRequestException)
            {
                return new LoginResponse { Success = false, Message = "Cannot connect to the API server. Please ensure the backend is running." };
            }
            catch 
            { 
                return new LoginResponse { Success = false, Message = "An unexpected error occurred while communicating with the server." }; 
            }
        }

        public static async Task<bool> RegisterAsync(string fullName, string username, string phone, string email, string password)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("auth/register",
                    new { FullName = fullName, Username = username, Phone = phone, Email = email, Password = password });
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        // ── Menu Items ───────────────────────────────────────────────────

        public static async Task<DataTable> GetMenuItemsTableAsync()
        {
            try
            {
                var items = await _http.GetFromJsonAsync<List<MenuItemDto>>("menu", _jsonOptions);
                return ToDataTable(items);
            }
            catch { return new DataTable(); }
        }

        public static async Task<List<MenuItemDto>> GetMenuItemsAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<List<MenuItemDto>>("menu", _jsonOptions) ?? new();
            }
            catch { return new(); }
        }

        public static async Task<bool> AddMenuItemAsync(string name, string price, string category, string imagePath)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("menu", new { Name = name, Price = decimal.Parse(price), Category = category, ImagePath = imagePath });
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public static async Task<bool> UpdateMenuItemAsync(int id, string name, string price, string category, string imagePath)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"menu/{id}", new { Name = name, Price = decimal.Parse(price), Category = category, ImagePath = imagePath });
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public static async Task<bool> DeleteMenuItemAsync(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"menu/{id}");
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        // ── Tables ───────────────────────────────────────────────────────

        public static async Task<DataTable> GetTablesTableAsync()
        {
            try
            {
                var tables = await _http.GetFromJsonAsync<List<TableModel>>("tables", _jsonOptions);
                return ToDataTable(tables);
            }
            catch { return new DataTable(); }
        }

        public static async Task<bool> UpdateTableStatusAsync(int tableId, string status)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"tables/{tableId}/status", status);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        // ── Orders ───────────────────────────────────────────────────────

        public static async Task<int> CreateOrderAsync(CreateOrderRequest req)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("orders", req);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OrderCreatedResponse>(_jsonOptions);
                    return result?.OrderID ?? -1;
                }
                return -1;
            }
            catch { return -1; }
        }

        public static async Task<DataTable> GetPendingOrdersTableAsync()
        {
            try
            {
                var orders = await _http.GetFromJsonAsync<List<OrderModel>>("orders/pending", _jsonOptions);
                return ToDataTable(orders);
            }
            catch { return new DataTable(); }
        }

        public static async Task<List<OrderModel>> GetCustomerOrdersAsync(int customerId)
        {
            try
            {
                return await _http.GetFromJsonAsync<List<OrderModel>>($"orders/customer/{customerId}", _jsonOptions) ?? new();
            }
            catch { return new(); }
        }

        public static async Task<List<OrderDetailModel>> GetOrderDetailsAsync(int orderId)
        {
            try
            {
                return await _http.GetFromJsonAsync<List<OrderDetailModel>>($"orders/{orderId}/details", _jsonOptions) ?? new();
            }
            catch { return new(); }
        }

        // ── Payments ─────────────────────────────────────────────────────

        public static async Task<bool> ProcessPaymentAsync(int orderId, decimal amount, string method)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("payments/process", new { OrderID = orderId, Amount = amount, PaymentMethod = method });
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public static async Task<bool> SubmitReceiptAsync(int orderId, string content)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("payments/receipt", 
                    new ReceiptRequest { OrderID = orderId, ReceiptContent = content });
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public static async Task<DataTable> GetReceiptsTableAsync()
        {
            try
            {
                var receipts = await _http.GetFromJsonAsync<List<ReceiptModel>>("payments/receipts", _jsonOptions);
                return ToDataTable(receipts);
            }
            catch { return new DataTable(); }
        }

        // ── Staff ────────────────────────────────────────────────────────

        public static async Task<DataTable> GetStaffTableAsync()
        {
            try
            {
                var staff = await _http.GetFromJsonAsync<List<UserModel>>("users/staff", _jsonOptions);
                return ToDataTable(staff);
            }
            catch { return new DataTable(); }
        }

        public static async Task<bool> AddStaffAsync(string username, string password, string role)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("users/staff", new { Username = username, Password = password, Role = role });
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public static async Task<bool> DeleteStaffAsync(string username)
        {
            try
            {
                var response = await _http.DeleteAsync($"users/staff/{username}");
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        // ── Stats ────────────────────────────────────────────────────────

        public static async Task<DashboardStats?> GetStatsAsync()
        {
            try { return await _http.GetFromJsonAsync<DashboardStats>("stats", _jsonOptions); }
            catch { return null; }
        }

        public static async Task<DataTable> GetDailyReportTableAsync()
        {
            try
            {
                var report = await _http.GetFromJsonAsync<List<DailyReportRow>>("stats/daily-report", _jsonOptions);
                return ToDataTable(report);
            }
            catch { return new DataTable(); }
        }

        // ── Settings ─────────────────────────────────────────────────────

        public static async Task<SettingsModel?> GetSettingsAsync()
        {
            try { return await _http.GetFromJsonAsync<SettingsModel>("settings", _jsonOptions); }
            catch { return null; }
        }

        public static async Task<bool> SaveSettingsAsync(SettingsModel settings)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("settings", settings);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public static async Task<DataTable> GetCustomersTableAsync()
        {
            try
            {
                var customers = await _http.GetFromJsonAsync<List<CustomerModel>>("users/customers", _jsonOptions);
                return ToDataTable(customers);
            }
            catch { return new DataTable(); }
        }

        // ── Helpers ───────────────────────────────────────────────────────

        private static DataTable ToDataTable<T>(List<T>? items)
        {
            DataTable dt = new DataTable(typeof(T).Name);
            if (items == null || items.Count == 0) return dt;

            var props = typeof(T).GetProperties();
            foreach (var p in props) dt.Columns.Add(p.Name, Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType);

            foreach (var item in items)
            {
                var values = new object?[props.Length];
                for (int i = 0; i < props.Length; i++) values[i] = props[i].GetValue(item);
                dt.Rows.Add(values);
            }
            return dt;
        }
    }

    // ── DTO Models ────────────────────────────────────────────────────

    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public AppUser? User { get; set; }
    }

    public class MenuItemDto
    {
        public int ItemID { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Category { get; set; } = "";
        public string ImagePath { get; set; } = "";
        public bool IsAvailable { get; set; }
    }

    public class TableModel
    {
        public int TableID { get; set; }
        public string TableNumber { get; set; } = "";
        public int Capacity { get; set; }
        public string Status { get; set; } = "";
    }

    public class CreateOrderRequest
    {
        public int? CustomerID { get; set; }
        public int? TableID { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public List<CartItemDto> Items { get; set; } = new();
    }

    public class CartItemDto
    {
        public int ItemID { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderCreatedResponse { public int OrderID { get; set; } }

    public class OrderModel
    {
        public int OrderID { get; set; }
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

    public class DashboardStats
    {
        public string TotalRevenue { get; set; } = "0";
        public int TotalOrders { get; set; }
        public int AvailableTables { get; set; }
        public int TotalTables { get; set; }
        public int PendingOrders { get; set; }
        public int MenuItemCount { get; set; }
        public int ActiveStaff { get; set; }
    }

    public class DailyReportRow
    {
        public string Date { get; set; } = "";
        public int TotalOrders { get; set; }
        public decimal TotalSales { get; set; }
    }

    public class SettingsModel
    {
        public string RestaurantName { get; set; } = "BEST Restaurant";
        public string Currency { get; set; } = "Birr (ETB)";
        public string DarkMode { get; set; } = "False";
    }

    public class ReceiptRequest
    {
        public int OrderID { get; set; }
        public string ReceiptContent { get; set; } = "";
    }

    public class ReceiptModel
    {
        public int ReceiptID { get; set; }
        public int OrderID { get; set; }
        public string ReceiptContent { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }

    public class CustomerModel
    {
        public int CustomerID { get; set; }
        public string Name { get; set; } = "";
    }

    public class UserModel
    {
        public int UserID { get; set; }
        public string FullName { get; set; } = "";
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "";
        public string Phone { get; set; } = "";
    }
}
