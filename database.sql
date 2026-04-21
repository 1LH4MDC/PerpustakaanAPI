-- ==============================================
-- database: perpustakaanpaa
-- ==============================================

-- drop tables
drop table if exists peminjaman;
drop table if exists buku;
drop table if exists kategori;
drop table if exists anggota;

-- ==============================================
-- table: kategori
-- ==============================================
create table kategori (
    id_kategori   serial primary key,
    nama_kategori varchar(50) not null,
    deskripsi     varchar(200),
    created_at    timestamp not null default now(),
    updated_at    timestamp not null default now()
);

create index idx_kategori_nama on kategori(nama_kategori);

-- ==============================================
-- table: buku
-- ==============================================
create table buku (
    id_buku      serial primary key,
    judul        varchar(200) not null,
    pengarang    varchar(100) not null,
    penerbit     varchar(100),
    tahun_terbit int,
    stok         int not null default 0,
    id_kategori  int not null,
    created_at   timestamp not null default now(),
    updated_at   timestamp not null default now(),
    constraint fk_buku_kategori foreign key (id_kategori)
        references kategori(id_kategori)
);

create index idx_buku_judul     on buku(judul);
create index idx_buku_pengarang on buku(pengarang);
create index idx_buku_kategori  on buku(id_kategori);

-- ==============================================
-- table: anggota
-- ==============================================
create table anggota (
    id_anggota serial primary key,
    nama       varchar(100) not null,
    email      varchar(100) unique not null,
    no_hp      varchar(20),
    alamat     varchar(200),
    created_at timestamp not null default now(),
    updated_at timestamp not null default now()
);

create index idx_anggota_email on anggota(email);
create index idx_anggota_nama  on anggota(nama);

-- ==============================================
-- table: peminjaman
-- ==============================================
create table peminjaman (
    id_peminjaman   serial primary key,
    id_buku         int not null,
    id_anggota      int not null,
    tanggal_pinjam  date not null default current_date,
    tanggal_kembali date,
    status          varchar(20) not null default 'dipinjam',
    created_at      timestamp not null default now(),
    updated_at      timestamp not null default now(),
    constraint fk_peminjaman_buku    foreign key (id_buku)
        references buku(id_buku),
    constraint fk_peminjaman_anggota foreign key (id_anggota)
        references anggota(id_anggota),
    constraint chk_status check (status in ('dipinjam', 'dikembalikan'))
);

create index idx_peminjaman_buku    on peminjaman(id_buku);
create index idx_peminjaman_anggota on peminjaman(id_anggota);
create index idx_peminjaman_status  on peminjaman(status);

-- ==============================================
-- sample data: kategori
-- ==============================================
insert into kategori (nama_kategori, deskripsi) values
('Fiksi',             'Novel dan cerita rekaan'),
('Sains',             'Buku ilmu pengetahuan alam'),
('Teknologi',         'Buku pemrograman dan IT'),
('Sejarah',           'Buku sejarah dan budaya'),
('Pengembangan Diri', 'Motivasi dan self-improvement');

-- ==============================================
-- sample data: buku
-- ==============================================
insert into buku (judul, pengarang, penerbit, tahun_terbit, stok, id_kategori) values
('Laskar Pelangi',         'Andrea Hirata',    'Bentang Pustaka', 2005, 5, 1),
('Fisika Dasar',           'Halliday',         'Erlangga',        2010, 3, 2),
('Clean Code',             'Robert C. Martin', 'Prentice Hall',   2008, 4, 3),
('Sapiens',                'Yuval Noah Harari','Gramedia',        2015, 2, 4),
('Atomic Habits',          'James Clear',      'Gramedia',        2018, 6, 5),
('Bumi Manusia',           'Pramoedya',        'Hasta Mitra',     1980, 3, 1);

-- ==============================================
-- sample data: anggota
-- ==============================================
insert into anggota (nama, email, no_hp, alamat) values
('Ilham Dwi Cahya', 'ilham@gmail.com', '081234567890', 'Banyuwangi'),
('Ahmad',           'ahmad@gmail.com', '082345678901', 'Jember'),
('Siti',            'siti@gmail.com',  '083456789012', 'Malang'),
('Budi',            'budi@gmail.com',  '084567890123', 'Surabaya'),
('Dewi',            'dewi@gmail.com',  '085678901234', 'Bondowoso');

-- ==============================================
-- sample data: peminjaman
-- ==============================================
insert into peminjaman (id_buku, id_anggota, tanggal_pinjam, tanggal_kembali, status) values
(1, 1, '2025-04-01', '2025-04-08', 'dikembalikan'),
(2, 2, '2025-04-05', null,         'dipinjam'),
(3, 3, '2025-04-10', null,         'dipinjam'),
(4, 4, '2025-04-12', '2025-04-19', 'dikembalikan'),
(5, 5, '2025-04-15', null,         'dipinjam');