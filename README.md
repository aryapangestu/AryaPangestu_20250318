# Aplikasi Logistik Sederhana

## Deskripsi
Aplikasi ini adalah sistem logistik sederhana yang memungkinkan pengguna untuk membuat dan melacak pengiriman. Aplikasi ini memiliki fitur untuk membuat data shipment baru, memperbarui status pengiriman, dan melihat riwayat status pengiriman.

## Teknologi yang Digunakan
- ASP.NET Core 8 MVC
- SQL Server 2022
- Entity Framework Core
- ASP.NET Identity untuk autentikasi
- JWT Authentication untuk API
- Swagger/OpenAPI untuk dokumentasi API

## Cara Menjalankan Aplikasi

### Prasyarat
- Visual Studio 2019 atau yang lebih baru
- SQL Server 2012 atau yang lebih baru

### Langkah-langkah Instalasi
1. Clone atau extract solution ke folder lokal
2. Buka SQL Server Management Studio
3. Restore dari file AryaPangestu_20250318.bak
4. Buka solution di Visual Studio
5. Update connection string di file appsettings.json sesuai dengan konfigurasi SQL Server Anda
6. Jalankan aplikasi dengan menekan F5 atau klik tombol "IIS Express"

### Kredensial Login
- Username: admin@logistics.com
- Password: P@ssw0rd123

## Fitur Utama

### 1. Membuat Data Shipment Baru
- Buka menu "Shipments" dan klik "Create New"
- Isi form dengan informasi pengirim, penerima, dan paket
- Nomor resi akan dibuat secara otomatis jika tidak diisi

### 2. Memperbarui Status Shipment
- Buka detail shipment yang ingin diperbarui
- Klik tombol "Update Status"
- Pilih status baru (Shipment Pick Up, On Delivery, atau POD)
- Tambahkan catatan jika diperlukan

### 3. Melihat Riwayat Status
- Buka detail shipment
- Scroll ke bagian "Status History" untuk melihat semua perubahan status beserta tanggal dan waktu

### 4. API dan Dokumentasi Swagger
- Akses Swagger UI: `/swagger` untuk melihat dan menguji API
- Autentikasi: POST /api/token (body: username & password)
- Get Shipment: GET /api/shipment/{trackingNumber}
- Get Tracking History: GET /api/shipment/{trackingNumber}/history

## Contoh Data
Aplikasi ini sudah diisi dengan contoh data shipment dengan nomor resi KEID0000001 dan riwayat status lengkap dari Shipment Pick Up hingga POD.

## Troubleshooting
- Jika terjadi error koneksi database, pastikan connection string sudah benar
- Jika login gagal, pastikan database sudah berisi data user
- Untuk masalah lainnya, periksa log aplikasi di folder Logs
