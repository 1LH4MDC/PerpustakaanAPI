namespace PerpustakaanAPI.Models
{
    public class Buku
    {
        public int id_buku { get; set; }
        public string judul { get; set; } = "";
        public string pengarang { get; set; } = "";
        public string? penerbit { get; set; }
        public int? tahun_terbit { get; set; }
        public int stok { get; set; }
        public int id_kategori { get; set; }
        public string? nama_kategori { get; set; }  
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}