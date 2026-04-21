namespace PerpustakaanAPI.Models
{
    public class Anggota
    {
        public int id_anggota { get; set; }
        public string nama { get; set; } = "";
        public string email { get; set; } = "";
        public string? no_hp { get; set; }
        public string? alamat { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}