using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FinalProjectVispro
{
    public partial class Pelanggan : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;
        private DataSet ds = new DataSet();
        private string query;

        public Pelanggan()
        {
            // Correct connection string
            string alamat = "server=localhost; database=db_finalproject; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
        }

        private void LoadDataPelanggan()
        {
            try
            {
                koneksi.Open();
                query = "SELECT * FROM tb_pelanggan";
                adapter = new MySqlDataAdapter(query, koneksi);
                ds = new DataSet();
                adapter.Fill(ds, "tb_pelanggan");
                dataGridView1.DataSource = ds.Tables["tb_pelanggan"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                koneksi.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();
                // Prepare SQL INSERT/UPDATE command
                query = string.Format("select * from tb_pelanggan where username = '{0}'");
                ds.Clear();
                koneksi.Open();
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                adapter.Fill(ds);
                koneksi.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow kolom in ds.Tables[0].Rows)
                    {
                        textBox1.Text = kolom["kode_pelanggan"].ToString();
                        textBox2.Text = kolom["nama_pelanggan"].ToString();
                        textBox3.Text = kolom["alamat"].ToString();
                        textBox4.Text = kolom["no_telepon"].ToString();
                    }
                }

                // Execute command
                perintah.ExecuteNonQuery();
                MessageBox.Show("Data added successfully!");

                // Refresh the data grid view
                LoadDataPelanggan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                koneksi.Close();
            }
        }

        private void Pelanggan_Load(object sender, EventArgs e)
        {
            LoadDataPelanggan();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox1.Focus();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnClear.Enabled = false;
            btnSave.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Jasa jasa = new Jasa();
            jasa.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: Code to handle cell clicks, e.g., load selected row's data into textboxes
        }

        // btnSave: Menyimpan data baru ke dalam database
        // btnSave: Menyimpan data baru ke dalam database
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Pastikan koneksi dibuka
                koneksi.Open();

                // Query untuk memasukkan data pelanggan baru ke dalam tabel tb_pelanggan
                query = "INSERT INTO tb_pelanggan (kode_pelanggan, nama_pelanggan, alamat_pelanggan, no_telepon) " +
                        "VALUES (@kode_pelanggan, @nama_pelanggan, @alamat_pelanggan, @no_telepon)";

                // Menyiapkan command dengan query yang sudah didefinisikan
                perintah = new MySqlCommand(query, koneksi);

                // Menambahkan parameter untuk mencegah SQL Injection dan menyisipkan data dari TextBox
                perintah.Parameters.AddWithValue("@kode_pelanggan", textBox1.Text);  // Kode pelanggan
                perintah.Parameters.AddWithValue("@nama_pelanggan", textBox2.Text);  // Nama pelanggan
                perintah.Parameters.AddWithValue("@alamat_pelanggan", textBox3.Text);         // Alamat
                perintah.Parameters.AddWithValue("@no_telepon", textBox4.Text);     // No telepon

                // Menjalankan query
                perintah.ExecuteNonQuery();

                // Menampilkan pesan sukses setelah data berhasil disimpan
                MessageBox.Show("Data pelanggan berhasil disimpan!");

                // Memuat ulang data pelanggan di DataGridView
                LoadDataPelanggan();

                // Membersihkan input pada TextBox
                ClearFields();
            }
            finally
            {
                // Pastikan koneksi selalu ditutup
                koneksi.Close();
            }
        }


        // btnUpdate: Memperbarui data yang ada
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();
                query = "UPDATE tb_pelanggan SET nama_pelanggan = @nama_pelanggan, alamat_pelanggan = @alamat_pelanggan, no_telepon = @no_telepon " +
                        "WHERE kode_pelanggan = @kode_pelanggan";
                perintah = new MySqlCommand(query, koneksi);
                perintah.Parameters.AddWithValue("@kode_pelanggan", textBox1.Text);
                perintah.Parameters.AddWithValue("@nama_pelanggan", textBox2.Text);
                perintah.Parameters.AddWithValue("@alamat", textBox3.Text);
                perintah.Parameters.AddWithValue("@no_telepon", textBox4.Text);
                perintah.ExecuteNonQuery();

                MessageBox.Show("Data pelanggan berhasil diperbarui!");
                LoadDataPelanggan(); // Refresh DataGridView
                ClearFields(); // Kosongkan input
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                koneksi.Close();
            }
        }

        // btnDelete: Menghapus data pelanggan berdasarkan kode pelanggan
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();
                query = "DELETE FROM tb_pelanggan WHERE kode_pelanggan = @kode_pelanggan";
                perintah = new MySqlCommand(query, koneksi);
                perintah.Parameters.AddWithValue("@kode_pelanggan", textBox1.Text);
                perintah.ExecuteNonQuery();

                MessageBox.Show("Data pelanggan berhasil dihapus!");
                LoadDataPelanggan(); // Refresh DataGridView
                ClearFields(); // Kosongkan input
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                koneksi.Close();
            }
        }

        // btnClear: Membersihkan semua input
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        // Fungsi untuk Mengosongkan Input TextBox
        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox1.Focus();
        }

        // btnSearch: Mencari data pelanggan berdasarkan nama pelanggan
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();
                query = "SELECT * FROM tb_pelanggan WHERE nama_pelanggan LIKE @nama_pelanggan";
                adapter = new MySqlDataAdapter(query, koneksi);
                adapter.SelectCommand.Parameters.AddWithValue("@nama_pelanggan", "%" + textBox2.Text + "%");
                ds = new DataSet();
                adapter.Fill(ds, "tb_pelanggan");
                dataGridView1.DataSource = ds.Tables["tb_pelanggan"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                koneksi.Close();
            }
        }
    }
}