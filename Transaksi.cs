using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FinalProjectVispro
{
    public partial class Transaksi : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;
        private DataSet ds = new DataSet();
        private string query;

        public Transaksi()
        {
            string alamat = "server=localhost; database=db_finalproject; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Hide();
        }

        private void Transaksi_Load(object sender, EventArgs e)
        {
            koneksi.Open();
            query = "SELECT * FROM tb_transaksi";
            adapter = new MySqlDataAdapter(query, koneksi);
            ds = new DataSet();
            adapter.Fill(ds, "tb_transaksi");
            dataGridView1.DataSource = ds.Tables["tb_transaksi"];
            koneksi.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Pastikan koneksi dibuka
                koneksi.Open();

                // Query untuk memasukkan data transaksi baru ke dalam tabel tb_transaksi
                query = "INSERT INTO tb_transaksi (no_transaksi, tgl_pembayaran, bayar, kode_pelanggan, no_order) " +
                        "VALUES (@no_transaksi, @tgl_pembayaran, @bayar, @kode_pelanggan, @no_order)";

                // Menyiapkan command dengan query yang sudah didefinisikan
                perintah = new MySqlCommand(query, koneksi);

                // Menambahkan parameter untuk mencegah SQL Injection dan menyisipkan data dari TextBox
                perintah.Parameters.AddWithValue("@no_transaksi", textBox1.Text);       // No transaksi
                perintah.Parameters.AddWithValue("@tgl_pembayaran", dateTimePicker1.Value); // Tanggal pembayaran
                perintah.Parameters.AddWithValue("@bayar", textBox2.Text);               // Bayar
                perintah.Parameters.AddWithValue("@kode_pelanggan", textBox3.Text);      // Kode pelanggan
                perintah.Parameters.AddWithValue("@no_order", textBox4.Text);            // No order

                // Menjalankan query
                perintah.ExecuteNonQuery();

                // Menampilkan pesan sukses setelah data berhasil disimpan
                MessageBox.Show("Data transaksi berhasil disimpan!");

                // Memuat ulang data transaksi di DataGridView
                LoadDataTransaksi();

                // Membersihkan input pada TextBox
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
            finally
            {
                // Pastikan koneksi selalu ditutup
                koneksi.Close();
            }
        }

        private void LoadDataTransaksi()
        {
            // Method untuk memuat data transaksi di DataGridView
            query = "SELECT * FROM tb_transaksi";
            adapter = new MySqlDataAdapter(query, koneksi);
            ds = new DataSet();
            adapter.Fill(ds, "tb_transaksi");
            dataGridView1.DataSource = ds.Tables["tb_transaksi"];
        }

        private void ClearFields()
        {
            // Method untuk membersihkan semua input TextBox
            textBox1.Clear();
            dateTimePicker1.Value = DateTime.Now;
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
    }
}
