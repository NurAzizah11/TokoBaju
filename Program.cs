using System;
using System.Data;
using System.Data.SqlClient;

namespace TokoBaju
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi ke Database\n");
                    Console.WriteLine("Masukkan User ID :");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan Password :");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan database tujuan :");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk Terhubung ke Database :");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source = LAPTOP-JFGCC1VG\\MSSQLSERVER01; " +
                                    "initial catalog = {0}; " + "User ID = {1}; password = {2}";

                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Ubah Data");
                                        Console.WriteLine("4. Delete Data");
                                        Console.WriteLine("5. Cari Data");
                                        Console.WriteLine("6. Keluar");
                                        Console.Write("\nEnter your choice (1-6): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("DATA BARANG\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INPUT DATA BARANG\n");
                                                    Console.WriteLine("Masukkan Id Barang :");
                                                    string Id_barang = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Nama Barang :");
                                                    string Nmabrg = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Harga Barang");
                                                    string Hargabrg = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Stok :");
                                                    string stok = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(Id_barang, Nmabrg, Hargabrg, stok, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki " + "akses untuk menambah data");
                                                    }
                                                }
                                                break;
                                            case '3':
                                                conn.Close();
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid option");
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Menggunakan User Tersebut\n");
                    Console.ResetColor();
                }
            }
        }

        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select * From dbo.Barang", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
            r.Close();
        }
        public void insert(string Id_barang, string Nmabrg,string Hargabrg, string stok, SqlConnection con)
        {

            string str = "";
            str = "insert into dbo.Barang (Id_barang, Nmabarang, Hargabrg, stok)" + "values(@idbarang,nmabarang,@hargabrg,@stok)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("idbrg", Id_barang));
            cmd.Parameters.Add(new SqlParameter("nmabrg", Nmabrg));
            cmd.Parameters.Add(new SqlParameter("hrgbrg", Hargabrg));
            cmd.Parameters.Add(new SqlParameter("stok", stok));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }
    }
}
