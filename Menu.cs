using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProjectVispro
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Pelanggan pelanggan = new Pelanggan();
            pelanggan.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Transaksi transaksi = new Transaksi();
            transaksi.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CekPakaian cekPakaian = new CekPakaian();
            cekPakaian.Show();
            this.Hide();
        }
    }
}
