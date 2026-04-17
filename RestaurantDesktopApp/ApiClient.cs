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
        public static string BaseUrl { get; set; } = "http://localhost:5201/api";

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
                return null;
            }
            catch { return null; }
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
                decimal.TryParse(price, out decimal p);
                var response = await _http.PostAsJsonAsync("menu",
                    new { Name = name, Price = p, Category = category, ImagePath = imagePath });
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public static async Task<bool> UpdateMenuItemAsync(int id, string name, string price, string category, string imagePath)
        {
            try
            {
                decimal.TryParse(price, out decimal p);
                var response = await _http.PutAsJsonAsync($"menu/{id}",
                    new { Name = name, Price = p, Category = category, ImagePath = imagePath });
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
                var tables = await _http.GetFromJsonAsync<List<TableDto>>("tables", _jsonOptions);
                return ToDataTable(tables);
            }
            catch { return new DataTable(); }
        }

        public static async Task<List<TableDto>> GetAvailableTablesAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<List<TableDto>>("tables/available", _jsonOptions) ?? new();
            }
            catch { return new(); }
        }

        public static async Task<bool> UpdateTableStatusAsync(int tableId, string status)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"tables/{tableId}/status", new { Status = status });
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        // ── Orders ───────────────────────────────────────────────────────

        public static async Task<int> CreateOrderAsync(int? customerId, int tableId, decimal totalAmount, List<OrderItemDto> items)
        {
            try
            {
                var request = new
                {
                    CustomerID = customerId,
                    TableID = tableId,
                    TotalAmount = totalAmount,
                    Status = "Pending",
                    Items = items
                };
                var response = await _http.PostAsJsonAsync("orders", request);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                    return result.GetProperty("orderID").GetInt32();
                }
                return -1;
            }
            catch { return -1; }
        }

        public static async Task<DataTable> GetPendingOrdersTableAsync()
        {
            try
            {
                var orders = await _http.GetFromJsonAsync<List<OrderDto>>("orders/pending", _jsonOptions);
                return ToDataTable(orders);
            }
            catch { return new DataTable(); }
        }

        public static async Task<List<OrderDto>> GetCustomerOrdersAsync(int userId)
        {
            try
            {
                return await _http.GetFromJsonAsync<List<OrderDto>>($"orders/customer/{userId}", _jsonOptions) ?? new();
            }
            catch { return new(); }
        }

        public static async Task<List<CustomerDto>> GetCustomersAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<List<CustomerDto>>("orders/customers", _jsonOptions) ?? new();
            }
            catch { return new(); }
        }

        // ── Payments ─────────────────────────────────────────────────────

        public static async Task<bool> ProcessPaymentAsync(int orderId, decimal amount, string method = "Cash")
        {
            try
            {
                var response = await _http.PostAsJsonAsync("payments",
                    new { OrderID = orderId, Amount = amount, PaymentMethod = method });
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        // ── Users / Staff ────────────────────────────────────────────────

        public static async Task<DataTable> GetStaffTableAsync()
        {
            try
            {
                var staff = await _http.GetFromJsonAsync<List<StaffDto>>("users", _jsonOptions);
                return ToDataTable(staff);
            }
            catch { return new DataTable(); }
        }

        public static async Task<bool> AddStaffAsync(string username, string password, string role)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("users",
                    new { Username = username, Password = password, Role = role });
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public static async Task<bool> DeleteStaffAsync(string username)
        {
            try
            {
                var response = await _http.DeleteAsync($"users/{username}");
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        // ── Stats ────────────────────────────────────────────────────────

        public static async Task<DashboardStatsDto?> GetStatsAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<DashboardStatsDto>("stats", _jsonOptions);
            }
            catch { return null; }
        }

        public static async Task<DataTable> GetDailyReportTableAsync()
        {
            try
            {
                var rows = await _http.GetFromJsonAsync<List<DailyReportDto>>("stats/daily-report", _jsonOptions);
                return ToDataTable(rows);
            }
            catch { return new DataTable(); }
        }

        // ── Settings ─────────────────────────────────────────────────────

        public static async Task<SettingsDto?> GetSettingsAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<SettingsDto>("settings", _jsonOptions);
            }
            catch { return null; }
        }

        public static async Task<bool> SaveSettingsAsync(string restaurantName, string currency, string darkMode)
        {
            try
            {
                var response = await _http.PutAsJsonAsync("settings",
                    new { RestaurantName = restaurantName, Currency = currency, DarkMode = darkMode });
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        // ── Helper: Convert list to DataTable ────────────────────────────

        private static DataTable ToDataTable<T>(List<T>? items)
        {
            var dt = new DataTable();
            if (items == null || items.Count == 0) return dt;

            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                var colType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                dt.Columns.Add(prop.Name, colType);
            }

            foreach (var item in items)
            {
                var row = dt.NewRow();
                foreach (var prop in props)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                dt.Rows.Add(row);
            }
            return dt;
        }
    }

    // ─── DTO classes for the desktop app ─────────────────────────────────

    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; } = "";
        public string Role { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public int UserID { get; set; }
        public string Message { get; set; } = "";
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

    public class TableDto
    {
        public int TableID { get; set; }
        public string TableNumber { get; set; } = "";
        public int Capacity { get; set; }
        public string Status { get; set; } = "";
    }

    public class OrderDto
    {
        public int OrderID { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "";
        public DateTime OrderDate { get; set; }
    }

    public class OrderItemDto
    {
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class CustomerDto
    {
        public int CustomerID { get; set; }
        public string Name { get; set; } = "";
    }

    public class StaffDto
    {
        public int UserID { get; set; }
        public string Username { get; set; } = "";
        public string Role { get; set; } = "";
    }

    public class DashboardStatsDto
    {
        public string TotalRevenue { get; set; } = "0.00";
        public int TotalOrders { get; set; }
        public int AvailableTables { get; set; }
        public int TotalTables { get; set; }
        public int ActiveStaff { get; set; }
        public int PendingOrders { get; set; }
        public int MenuItemCount { get; set; }
    }

    public class DailyReportDto
    {
        public string Date { get; set; } = "";
        public int TotalOrders { get; set; }
        public decimal TotalSales { get; set; }
    }

    public class SettingsDto
    {
        public string RestaurantName { get; set; } = "LAUNCH";
        public string Currency { get; set; } = "Birr (ETB)";
        public string DarkMode { get; set; } = "False";
    }
}
