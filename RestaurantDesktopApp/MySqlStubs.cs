using System;
using System.Data;

namespace MySql.Data.MySqlClient
{
    // A completely mock namespace to sever the real MySQL connection while keeping
    // the project fully compilable without having to rewrite 20 forms of SQL logic.

    public class MySqlConnection : IDisposable
    {
        public ConnectionState State { get; set; } = ConnectionState.Closed;

        public MySqlConnection(string connectionString) { }

        public void Open() { State = ConnectionState.Open; }
        public void Close() { State = ConnectionState.Closed; }
        public MySqlTransaction BeginTransaction() { return new MySqlTransaction(); }

        public void Dispose() { }
    }

    public class MySqlTransaction : IDisposable
    {
        public void Commit() { }
        public void Rollback() { }
        public void Dispose() { }
    }

    public class MySqlCommand : IDisposable
    {
        public MySqlParameterCollection Parameters { get; } = new MySqlParameterCollection();
        
        public MySqlCommand() { }
        public MySqlCommand(string cmdText, MySqlConnection connection) { }
        public MySqlCommand(string cmdText, MySqlConnection connection, MySqlTransaction transaction) { }

        public int ExecuteNonQuery() { return 1; }
        public object ExecuteScalar() { return null; }
        public MySqlDataReader ExecuteReader() { return new MySqlDataReader(); }

        public void Dispose() { }
    }

    public class MySqlParameterCollection
    {
        public void AddWithValue(string parameterName, object value) { }
    }

    public class MySqlDataReader : IDisposable, IDataReader
    {
        public bool Read() { return false; }
        public object this[string name] { get { return string.Empty; } }
        public object this[int i] { get { return string.Empty; } }
        public int FieldCount => 0;

        public void Close() { }
        public void Dispose() { }

        // IDataReader implementation stubs
        public string GetName(int i) => string.Empty;
        public string GetDataTypeName(int i) => string.Empty;
        public Type GetFieldType(int i) => typeof(string);
        public object GetValue(int i) => null;
        public int GetValues(object[] values) => 0;
        public int GetOrdinal(string name) => 0;
        public bool GetBoolean(int i) => false;
        public byte GetByte(int i) => 0;
        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) => 0;
        public char GetChar(int i) => ' ';
        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) => 0;
        public Guid GetGuid(int i) => Guid.Empty;
        public short GetInt16(int i) => 0;
        public int GetInt32(int i) => 0;
        public long GetInt64(int i) => 0;
        public float GetFloat(int i) => 0;
        public double GetDouble(int i) => 0;
        public string GetString(int i) => string.Empty;
        public decimal GetDecimal(int i) => 0;
        public DateTime GetDateTime(int i) => DateTime.MinValue;
        public IDataReader GetData(int i) => null;
        public bool IsDBNull(int i) => true;

        public int Depth => 0;
        public bool IsClosed => true;
        public int RecordsAffected => 0;
        public bool NextResult() => false;
        public DataTable GetSchemaTable() => new DataTable();
    }

    public class MySqlDataAdapter : IDisposable
    {
        public MySqlDataAdapter(string selectCommandText, MySqlConnection selectConnection) { }
        public MySqlDataAdapter(MySqlCommand selectCommand) { }

        public int Fill(DataTable dataTable) { return 0; }
        public void Dispose() { }
    }
}
