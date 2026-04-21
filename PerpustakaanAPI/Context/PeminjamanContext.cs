using Npgsql;
using PerpustakaanAPI.Helper;
using PerpustakaanAPI.Models;

namespace PerpustakaanAPI.Context
{
    public class PeminjamanContext
    {
        private string _constr;

        public PeminjamanContext(string pConstr)
        {
            _constr = pConstr;
        }

        // GET ALL
        public List<Peminjaman> GetAll()
        {
            var list = new List<Peminjaman>();
            string query = @"
                SELECT pm.id_peminjaman, pm.id_buku, b.judul AS judul_buku,
                       pm.id_anggota, a.nama AS nama_anggota,
                       pm.tanggal_pinjam, pm.tanggal_kembali,
                       pm.status, pm.created_at, pm.updated_at
                FROM peminjaman pm
                INNER JOIN buku b    ON pm.id_buku    = b.id_buku
                INNER JOIN anggota a ON pm.id_anggota = a.id_anggota
                ORDER BY pm.id_peminjaman";

            var db = new SqlDBHelper(_constr);
            try
            {
                var cmd = db.getNpgsqlCommand(query);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Peminjaman()
                    {
                        id_peminjaman = reader.GetInt32(reader.GetOrdinal("id_peminjaman")),
                        id_buku = reader.GetInt32(reader.GetOrdinal("id_buku")),
                        judul_buku = reader.GetString(reader.GetOrdinal("judul_buku")),
                        id_anggota = reader.GetInt32(reader.GetOrdinal("id_anggota")),
                        nama_anggota = reader.GetString(reader.GetOrdinal("nama_anggota")),
                        tanggal_pinjam = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("tanggal_pinjam")),
                        tanggal_kembali = reader.IsDBNull(reader.GetOrdinal("tanggal_kembali")) ? null : reader.GetFieldValue<DateOnly>(reader.GetOrdinal("tanggal_kembali")),
                        status = reader.GetString(reader.GetOrdinal("status")),
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
        public Peminjaman? GetById(int id)
        {
            Peminjaman? item = null;
            string query = @"
                SELECT pm.id_peminjaman, pm.id_buku, b.judul AS judul_buku,
                       pm.id_anggota, a.nama AS nama_anggota,
                       pm.tanggal_pinjam, pm.tanggal_kembali,
                       pm.status, pm.created_at, pm.updated_at
                FROM peminjaman pm
                INNER JOIN buku b    ON pm.id_buku    = b.id_buku
                INNER JOIN anggota a ON pm.id_anggota = a.id_anggota
                WHERE pm.id_peminjaman = @id";

            var db = new SqlDBHelper(_constr);
            try
            {
                var cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    item = new Peminjaman()
                    {
                        id_peminjaman = reader.GetInt32(reader.GetOrdinal("id_peminjaman")),
                        id_buku = reader.GetInt32(reader.GetOrdinal("id_buku")),
                        judul_buku = reader.GetString(reader.GetOrdinal("judul_buku")),
                        id_anggota = reader.GetInt32(reader.GetOrdinal("id_anggota")),
                        nama_anggota = reader.GetString(reader.GetOrdinal("nama_anggota")),
                        tanggal_pinjam = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("tanggal_pinjam")),
                        tanggal_kembali = reader.IsDBNull(reader.GetOrdinal("tanggal_kembali")) ? null : reader.GetFieldValue<DateOnly>(reader.GetOrdinal("tanggal_kembali")),
                        status = reader.GetString(reader.GetOrdinal("status")),
                        created_at = reader.GetDateTime(reader.GetOrdinal("created_at")),
                        updated_at = reader.GetDateTime(reader.GetOrdinal("updated_at"))
                    };
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch { }
            return item;
        }

        // POST
        public bool Insert(Peminjaman pm)
        {
            string query = @"
                INSERT INTO peminjaman (id_buku, id_anggota, tanggal_pinjam, status)
                VALUES (@id_buku, @id_anggota, @tanggal_pinjam, 'dipinjam')";

            var db = new SqlDBHelper(_constr);
            try
            {
                var cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_buku", pm.id_buku);
                cmd.Parameters.AddWithValue("@id_anggota", pm.id_anggota);
                cmd.Parameters.AddWithValue("@tanggal_pinjam", pm.tanggal_pinjam);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
                return true;
            }
            catch { return false; }
        }

        // PUT - update status (kembalikan buku)
        public bool Update(int id, Peminjaman pm)
        {
            string query = @"
                UPDATE peminjaman
                SET status = @status,
                    tanggal_kembali = @tanggal_kembali,
                    updated_at = NOW()
                WHERE id_peminjaman = @id";

            var db = new SqlDBHelper(_constr);
            try
            {
                var cmd = db.getNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@status", pm.status);
                cmd.Parameters.AddWithValue("@tanggal_kembali", (object?)pm.tanggal_kembali ?? DBNull.Value);
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
            string query = "DELETE FROM peminjaman WHERE id_peminjaman = @id";

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