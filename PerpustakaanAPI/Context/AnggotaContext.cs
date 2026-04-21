using Npgsql;
using PerpustakaanAPI.Helper;
using PerpustakaanAPI.Models;

namespace PerpustakaanAPI.Context
{
    public class AnggotaContext
    {
        private string _constr;

        public AnggotaContext(string pConstr)
        {
            _constr = pConstr;
        }

        // GET ALL
        public List<Anggota> GetAll()
        {
            var list = new List<Anggota>();
            string query = "SELECT * FROM anggota ORDER BY id_anggota";

            var db = new SqlDBHelper(_constr);
            try
            {
                var cmd = db.getNpgsqlCommand(query);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Anggota()
                    {
                        id_anggota = reader.GetInt32(reader.GetOrdinal("id_anggota")),
                        nama = reader.GetString(reader.GetOrdinal("nama")),
                        email = reader.GetString(reader.GetOrdinal("email")),
                        no_hp = reader.IsDBNull(reader.GetOrdinal("no_hp")) ? null : reader.GetString(reader.GetOrdinal("no_hp")),
                        alamat = reader.IsDBNull(reader.GetOrdinal("alamat")) ? null : reader.GetString(reader.GetOrdinal("alamat")),
                        created_at = reader.GetDateTime(reader.GetOrdinal("created_at")),
                        updated_at = reader.GetDateTime(reader.GetOrdinal("updated_at"))
                    });
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch { }
            return list;
        }

        // GET BY ID
        public Anggota? GetById(int id)
        {
            Anggota? anggota = null;
            string query = "SELECT * FROM anggota WHERE id_anggota = @id";

            var db = new SqlDBHelper(_constr);
            try
            {
                var cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    anggota = new Anggota()
                    {
                        id_anggota = reader.GetInt32(reader.GetOrdinal("id_anggota")),
                        nama = reader.GetString(reader.GetOrdinal("nama")),
                        email = reader.GetString(reader.GetOrdinal("email")),
                        no_hp = reader.IsDBNull(reader.GetOrdinal("no_hp")) ? null : reader.GetString(reader.GetOrdinal("no_hp")),
                        alamat = reader.IsDBNull(reader.GetOrdinal("alamat")) ? null : reader.GetString(reader.GetOrdinal("alamat")),
                        created_at = reader.GetDateTime(reader.GetOrdinal("created_at")),
                        updated_at = reader.GetDateTime(reader.GetOrdinal("updated_at"))
                    };
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch { }
            return anggota;
        }

        // POST
        public bool Insert(Anggota anggota)
        {
            string query = @"
                INSERT INTO anggota (nama, email, no_hp, alamat)
                VALUES (@nama, @email, @no_hp, @alamat)";

            var db = new SqlDBHelper(_constr);
            try
            {
                var cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", anggota.nama);
                cmd.Parameters.AddWithValue("@email", anggota.email);
                cmd.Parameters.AddWithValue("@no_hp", (object?)anggota.no_hp ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@alamat", (object?)anggota.alamat ?? DBNull.Value);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
                return true;
            }
            catch { return false; }
        }

        // PUT
        public bool Update(int id, Anggota anggota)
        {
            string query = @"
                UPDATE anggota
                SET nama = @nama, email = @email,
                    no_hp = @no_hp, alamat = @alamat, updated_at = NOW()
                WHERE id_anggota = @id";

            var db = new SqlDBHelper(_constr);
            try
            {
                var cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nama", anggota.nama);
                cmd.Parameters.AddWithValue("@email", anggota.email);
                cmd.Parameters.AddWithValue("@no_hp", (object?)anggota.no_hp ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@alamat", (object?)anggota.alamat ?? DBNull.Value);
                int rows = cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
                return rows > 0;
            }
            catch { return false; }
        }

        // DELETE
        public bool Delete(int id)
        {
            string query = "DELETE FROM anggota WHERE id_anggota = @id";

            var db = new SqlDBHelper(_constr);
            try
            {
                var cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                int rows = cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
                return rows > 0;
            }
            catch { return false; }
        }
    }
}