#  PerpustakaanAPI

REST API untuk sistem manajemen perpustakaan yang dibangun menggunakan ASP.NET Core.

## 👤 Identitas
- **Nama:** Ilham Dwi Cahya
- **NIM:** 242410102081
- **Kelas:** PAA B

## 🎥 Video Presentasi
LINK : https://youtu.be/Hcl3ODnyayU

## 📖 Deskripsi Project
API ini mengelola data perpustakaan meliputi data buku, anggota,
dan transaksi peminjaman buku. Sistem ini memungkinkan operasi
CRUD lengkap pada setiap entitas dengan format response JSON
yang konsisten dan error handling yang baik.

## Teknologi yang Digunakan

| Komponen  | Teknologi                              |
|-----------|----------------------------------------|
| Bahasa    | C#                                     |
| Framework | ASP.NET Core 8.0                       |
| Database  | PostgreSQL 17                          |
| Library   | Npgsql                                 |
| Tools     | Visual Studio 2022, pgAdmin 4, Swagger |

## Struktur Tabel Database

| Tabel        | Keterangan                              |
|--------------|-----------------------------------------|
| `kategori`   | Kategori buku                           |
| `buku`       | Data buku (FK ke kategori)              |
| `anggota`    | Data anggota perpustakaan               |
| `peminjaman` | Transaksi pinjam (FK ke buku & anggota) |

##  Langkah Instalasi

### 1. Clone Repository
```bash
git clone https://github.com/1LH4MDC/PerpustakaanAPI.git
cd PerpustakaanAPI
```

### 2. Import Database
- Buka pgAdmin 4
- Buat database baru bernama `PerpustakaanPAA`
- Buka Query Tool
- Buka file `database.sql`
- Jalankan semua query

### 3. Konfigurasi Koneksi
Buka file `appsettings.json`, sesuaikan bagian berikut:
```json
"ConnectionStrings": {
  "koneksi": "Host=localhost;Port=5432;Database=PerpustakaanPAA;Username=postgres;Password=PASSWORD_KAMU"
}
```

### 4. Jalankan Project
```bash
dotnet run
```
Atau tekan `F5` di Visual Studio 2022.

### 5. Buka Swagger UI
https://localhost:7017/swagger

## 📋 Daftar Endpoint

###  Buku

| Method | URL | Keterangan |
|--------|-----|------------|
| GET | `/api/buku` | Ambil semua buku |
| GET | `/api/buku/{id}` | Ambil buku by ID |
| POST | `/api/buku` | Tambah buku baru |
| PUT | `/api/buku/{id}` | Update data buku |
| DELETE | `/api/buku/{id}` | Hapus buku |

###  Anggota

| Method | URL | Keterangan |
|--------|-----|------------|
| GET | `/api/anggota` | Ambil semua anggota |
| GET | `/api/anggota/{id}` | Ambil anggota by ID |
| POST | `/api/anggota` | Tambah anggota baru |
| PUT | `/api/anggota/{id}` | Update data anggota |
| DELETE | `/api/anggota/{id}` | Hapus anggota |

###  Peminjaman

| Method | URL | Keterangan |
|--------|-----|------------|
| GET | `/api/peminjaman` | Ambil semua peminjaman |
| GET | `/api/peminjaman/{id}` | Ambil peminjaman by ID |
| POST | `/api/peminjaman` | Tambah peminjaman baru |
| PUT | `/api/peminjaman/{id}` | Update status peminjaman |
| DELETE | `/api/peminjaman/{id}` | Hapus peminjaman |

##  Format Response

### Sukses (List)
```json
{
  "status": "success",
  "data": [...],
  "meta": { "total": 6 }
}
```

### Sukses (Single)
```json
{
  "status": "success",
  "data": { ... }
}
```

### Error
```json
{
  "status": "error",
  "message": "Pesan error disini"
}
```

