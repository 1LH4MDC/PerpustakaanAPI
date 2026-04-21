using Npgsql;

namespace PerpustakaanAPI.Helper
{
    public class SqlDBHelper
    {
        private string _constr;
        private NpgsqlConnection _conn;

        public SqlDBHelper(string pConstr)
        {
            _constr = pConstr;
            _conn = new NpgsqlConnection(_constr);
            _conn.Open();
        }

        public NpgsqlCommand getNpgsqlCommand(string query)
        {
            return new NpgsqlCommand(query, _conn);
        }

        public void closeConnection()
        {
            if (_conn != null && _conn.State == System.Data.ConnectionState.Open)
            {
                _conn.Close();
            }
        }
    }
}