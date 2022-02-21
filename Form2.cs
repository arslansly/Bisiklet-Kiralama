using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace İlkProjem
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=FINISTD033\\;Initial Catalog=bısıklet;Integrated Security=True");
        DataSet daset = new DataSet();
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand veriekle = new SqlCommand("insert into musterı(tc,ad,soyad,email,kangrubu) values (@tc,@ad,@soyad,@email,@kangrubu)", baglanti);
            veriekle.Parameters.AddWithValue("@tc", textBox1.Text);
            veriekle.Parameters.AddWithValue("@ad", textBox2.Text);
            veriekle.Parameters.AddWithValue("@soyad", textBox3.Text);
            veriekle.Parameters.AddWithValue("@email", textBox4.Text);
            veriekle.Parameters.AddWithValue("@kangrubu", textBox5.Text);
            veriekle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Veri Eklendi");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
