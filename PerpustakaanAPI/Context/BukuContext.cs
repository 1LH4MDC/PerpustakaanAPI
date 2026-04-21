using Npgsql;
using PerpustakaanAPI.Helper;
using PerpustakaanAPI.Models;

namespace PerpustakaanAPI.Context
{
    public class BukuContext
    {
        private string _constr;

        public BukuContext(string pConstr)
        {
            _constr = pConstr;
        }

        // GET ALL
        public List<Buku> GetAll()
        {
            var list = new List<Buku>();
            string query = @"
                SELECT b.id_buku, b.judul, b.pengarang, b.penerbit,
                       b.tahun_terbit, b.stok, b.id_kategori,
                       k.nama_kategori, b.created_at, b.updated_at
                FROM buku b
                INNER JOIN kategori k ON b.id_kategori = k.id_kategori
                ORDER BY b.id_buku";

            var db = new SqlDBHelper(_constr);
            try
            {
                var cmd = db.getNpgsqlCommand(query);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Buku()
                    {
                        id_buku = reader.GetInt32(reader.GetOrdinal("id_buku")),
                        judul = reader.GetString(reader.GetOrdinal("judul")),
                        pengarang = reader.GetString(reader.GetOrdinal("pengarang")),
                        penerbit = reader.IsDBNull(reader.GetOrdinal("penerbit")) ? null : reader.GetString(reader.GetOrdinal("penerbit")),
                        tahun_terbit = reader.IsDBNull(reader.GetOrdinal("tahun_terbit")) ? null : reader.GetInt32(reader.GetOrdinal("tahun_terbit")),
                        stok = reader.GetInt32(reader.GetOrdinal("stok")),
                        id_kategori = reader.GetInt32(reader.GetOrdinal("id_kategori")),
                        nama_kategori = reader.GetString(reader.GetOrdinal("nama_kategori")),
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
        public Buku? GetById(int id)
        {
            Buku? buku = null;
            string query = @"
                SELECT b.id_buku, b.judul, b.pengarang, b.penerbit,
                       b.tahun_terbit, b.stok, b.id_kategori,
                       k.nama_kategori, b.created_at, b.updated_at
                FROM buku b
                INNER JOIN kategori k ON b.id_kategori = k.id_kategori
                WHERE b.id_buku = @id";

            var db = new SqlDBHelper(_constr);
            try
            {
                var cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    buku = new Buku()
                    {
                        id_buku = reader.GetInt32(reader.GetOrdinal("id_buku")),
                        judul = reader.GetString(reader.GetOrdinal("judul")),
                        pengarang = reader.GetString(reader.GetOrdinal("pengarang")),
                        penerbit = reader.IsDBNull(reader.GetOrdinal("penerbit")) ? null : reader.GetString(reader.GetOrdinal("penerbit")),
                        tahun_terbit = reader.IsDBNull(reader.GetOrdinal("tahun_terbit")) ? null : reader.GetInt32(reader.GetOrdinal("tahun_terbit")),
                        stok = reader.GetInt32(reader.GetOrdinal("stok")),
                        id_kategori = reader.GetInt32(reader.GetOrdinal("id_kategori")),
                        nama_kategori = reader.GetString(reader.GetOrdinal("nama_kategori")),
                        created_at = reader.GetDateTime(reader.GetOrdinal("created_at")),
                        updated_at = reader.GetDateTime(reader.GetOrdinal("updated_at"))
                    };
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch { }
            return buku;
        }

        // POST - tambah buku
        public bool Insert(Buku buku)
        {
            string query = @"
                INSERT INTO buku (judul, pengarang, penerbit, tahun_terbit, stok, id_kategori)
                VALUES (@judul, @pengarang, @penerbit, @tahun_terbit, @stok, @id_kategori)";

            var db = new SqlDBHelper(_constr);
            try
            {
                var cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@judul", buku.judul);
                cmd.Parameters.AddWithValue("@pengarang", buku.pengarang);
                cmd.Parameters.AddWithValue("@penerbit", (object?)buku.penerbit ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@tahun_terbit", (object?)buku.tahun_terbit ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@stok", buku.stok);
                cmd.Parameters.AddWithValue("@id_kategori", buku.id_kategori);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
                return true;
            }
            catch { return false; }
        }

        // PUT - update buku
        public bool Update(int id, Buku buku)
        {
            string query = @"
                UPDATE buku
                SET judul = @judul, pengarang = @pengarang, penerbit = @penerbit,
                    tahun_terbit = @tahun_terbit, stok = @stok,
                    id_kategori = @id_kategori, updated_at = NOW()
                WHERE id_buku = @id";

            var db = new SqlDBHelper(_constr);
            try
            {
                var cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@judul", buku.judul);
                cmd.Parameters.AddWithValue("@pengarang", buku.pengarang);
                cmd.Parameters.AddWithValue("@penerbit", (object?)buku.penerbit ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@tahun_terbit", (object?)buku.tahun_terbit ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@stok", buku.stok);
                cmd.Parameters.AddWithValue("@id_kategori", buku.id_kategori);
                int rows = cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
                return rows > 0;
            }
            catch { return false; }
        }

        // DELETE - hapus buku
        public bool Delete(int id)
        {
            string query = "DELETE FROM buku WHERE id_buku = @id";

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