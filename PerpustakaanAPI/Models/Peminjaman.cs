namespace PerpustakaanAPI.Models
{
    public class Peminjaman
    {
        public int id_peminjaman { get; set; }
        public int id_buku { get; set; }
        public string? judul_buku { get; set; }     
        public int id_anggota { get; set; }
        public string? nama_anggota { get; set; }   
        public DateOnly tanggal_pinjam { get; set; }
        public DateOnly? tanggal_kembali { get; set; }
        public string status { get; set; } = "dipinjam";
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}