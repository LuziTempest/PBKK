using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DataMahasiswa
{
    // Class untuk merepresentasikan data mahasiswa
    internal sealed class Mahasiswa
    {
        public string NIM { get; set; }
        public string Nama { get; set; }
        public string Prodi { get; set; }
        public double IPK { get; set; }

        // Constructor
        public Mahasiswa(string nim, string nama, string prodi, double ipk)
        {
            NIM = nim;
            Nama = nama;
            Prodi = prodi;
            IPK = ipk;
        }
    }

    internal static class Program
    {
        // List untuk menyimpan data mahasiswa
        private static readonly List<Mahasiswa> daftarMahasiswa = new List<Mahasiswa>();

        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            int pilihan;
            do
            {
                TampilkanMenu();
                Console.Write("Pilihan: ");
                string input = Console.ReadLine() ?? string.Empty;
                
                if (!int.TryParse(input, out pilihan))
                {
                    pilihan = 0;
                }
                
                Console.WriteLine();
                
                switch (pilihan)
                {
                    case 1:
                        TambahMahasiswa();
                        break;
                    case 2:
                        TampilkanMahasiswa();
                        break;
                    case 3:
                        CariMahasiswa();
                        break;
                    case 4:
                        HapusMahasiswa();
                        break;
                    case 5:
                        Console.WriteLine("Terima kasih telah menggunakan program.");
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak tersedia!");
                        break;
                }
                
                if (pilihan != 5)
                {
                    Console.WriteLine();
                    Console.WriteLine("Tekan ENTER untuk melanjutkan...");
                    Console.ReadLine();
                }
                
            } while (pilihan != 5);
        }

        // ========================
        // METHOD MENAMPILKAN MENU
        // ========================
        static void TampilkanMenu()
        {
            BersihkanLayar();
            Console.WriteLine("========================================");
            Console.WriteLine("         SISTEM DATA MAHASISWA          ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Tambah Mahasiswa");
            Console.WriteLine("2. Tampilkan Mahasiswa");
            Console.WriteLine("3. Cari Mahasiswa");
            Console.WriteLine("4. Hapus Mahasiswa");
            Console.WriteLine("5. Keluar");
            Console.WriteLine("========================================");
        }

        // ========================
        // METHOD TAMBAH MAHASISWA
        // ========================
        static void TambahMahasiswa()
        {
            BersihkanLayar();
            Console.WriteLine("========================================");
            Console.WriteLine("            TAMBAH MAHASISWA            ");
            Console.WriteLine("========================================");
            
            string nim;
            while (true)
            {
                nim = BacaTeksWajib("NIM: ");
                if (!daftarMahasiswa.Exists(m => m.NIM.Equals(nim, StringComparison.OrdinalIgnoreCase)))
                {
                    break;
                }

                Console.WriteLine("NIM sudah terdaftar. Gunakan NIM lain.");
            }

            string nama = BacaTeksWajib("Nama: ");
            string prodi = BacaTeksWajib("Program Studi: ");
            
            double ipk;
            while (true)
            {
                Console.Write("IPK: ");
                string inputIpk = Console.ReadLine() ?? string.Empty;
                bool valid = double.TryParse(inputIpk, NumberStyles.Float, CultureInfo.CurrentCulture, out ipk)
                    || double.TryParse(inputIpk, NumberStyles.Float, CultureInfo.InvariantCulture, out ipk);

                if (valid)
                {
                    if (ipk >= 0 && ipk <= 4)
                    {
                        break;
                    }
                }
                Console.WriteLine("IPK harus berupa angka 0-4.");
            }
            
            Mahasiswa mahasiswa = new Mahasiswa(nim, nama, prodi, ipk);
            daftarMahasiswa.Add(mahasiswa);
            
            Console.WriteLine();
            Console.WriteLine("Data mahasiswa berhasil ditambahkan.");
        }

        // ========================
        // METHOD MENAMPILKAN DATA
        // ========================
        static void TampilkanMahasiswa()
        {
            BersihkanLayar();
            Console.WriteLine("===============================================================");
            Console.WriteLine("                       DAFTAR MAHASISWA                        ");
            Console.WriteLine("===============================================================");
            
            if (daftarMahasiswa.Count == 0)
            {
                Console.WriteLine("Belum ada data mahasiswa.");
                return;
            }
            
            Console.WriteLine("{0,-12} {1,-20} {2,-20} {3,5}", "NIM", "Nama", "Prodi", "IPK");
            Console.WriteLine("---------------------------------------------------------------");
            
            foreach (Mahasiswa m in daftarMahasiswa)
            {
                Console.WriteLine("{0,-12} {1,-20} {2,-20} {3,5:F2}", m.NIM, m.Nama, m.Prodi, m.IPK);
            }
            Console.WriteLine("---------------------------------------------------------------");
        }

        // ========================
        // METHOD MENCARI MAHASISWA
        // ========================
        static void CariMahasiswa()
        {
            BersihkanLayar();
            Console.WriteLine("========================================");
            Console.WriteLine("             CARI MAHASISWA             ");
            Console.WriteLine("========================================");
            
            Console.Write("Masukkan NIM: ");
            string nimCari = Console.ReadLine()?.Trim() ?? string.Empty;
            Mahasiswa? mahasiswaDitemukan = null;
            
            foreach (Mahasiswa m in daftarMahasiswa)
            {
                if (m.NIM.Equals(nimCari, StringComparison.OrdinalIgnoreCase))
                {
                    mahasiswaDitemukan = m;
                    break;
                }
            }
            
            Console.WriteLine();
            if (mahasiswaDitemukan != null)
            {
                Console.WriteLine("Data ditemukan!");
                Console.WriteLine("NIM: " + mahasiswaDitemukan.NIM);
                Console.WriteLine("Nama: " + mahasiswaDitemukan.Nama);
                Console.WriteLine("Prodi: " + mahasiswaDitemukan.Prodi);
                Console.WriteLine("IPK: " + mahasiswaDitemukan.IPK.ToString("F2"));
            }
            else
            {
                Console.WriteLine("Mahasiswa dengan NIM tersebut tidak ditemukan.");
            }
        }

        // ========================
        // METHOD MENGHAPUS MAHASISWA
        // ========================
        static void HapusMahasiswa()
        {
            BersihkanLayar();
            Console.WriteLine("========================================");
            Console.WriteLine("            HAPUS MAHASISWA             ");
            Console.WriteLine("========================================");
            
            Console.Write("Masukkan NIM: ");
            string nimHapus = Console.ReadLine()?.Trim() ?? string.Empty;
            Mahasiswa? mahasiswaDitemukan = null;
            
            foreach (Mahasiswa m in daftarMahasiswa)
            {
                if (m.NIM.Equals(nimHapus, StringComparison.OrdinalIgnoreCase))
                {
                    mahasiswaDitemukan = m;
                    break;
                }
            }
            
            if (mahasiswaDitemukan != null)
            {
                daftarMahasiswa.Remove(mahasiswaDitemukan);
                Console.WriteLine();
                Console.WriteLine("Data mahasiswa berhasil dihapus.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Data mahasiswa tidak ditemukan.");
            }
        }

        private static string BacaTeksWajib(string label)
        {
            while (true)
            {
                Console.Write(label);
                string nilai = Console.ReadLine()?.Trim() ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(nilai))
                {
                    return nilai;
                }

                Console.WriteLine("Nilai tidak boleh kosong.");
            }
        }

        private static void BersihkanLayar()
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }
        }
    }
}
