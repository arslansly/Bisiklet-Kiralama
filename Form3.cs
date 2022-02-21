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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=FINISTD033\\;Initial Catalog=bısıklet;Integrated Security=True");
        DataSet daset = new DataSet();

        void guncelle()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from musterı", baglanti);
            da.Fill(daset, "musterı");
            dataGridView1.DataSource = daset.Tables["musterı"];
            baglanti.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            guncelle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update musterı set ad=@ad, soyad=@soyad, email=@email, kangrubu=@kangrubu where tc=@tc ", baglanti);
            komut.Parameters.AddWithValue("@tc", textBox1.Text);
            komut.Parameters.AddWithValue("@ad", textBox2.Text);
            komut.Parameters.AddWithValue("@soyad", textBox3.Text);
            komut.Parameters.AddWithValue("@email", textBox4.Text);
            komut.Parameters.AddWithValue("@kangrubu", textBox5.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["musterı"].Clear();
            guncelle();
            MessageBox.Show("Müşteri Güncellenmiştir");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["ad"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["soyad"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["kangrubu"].Value.ToString();
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from musterı where tc='" + dataGridView1.CurrentRow.Cells["tc"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["musterı"].Clear();
            guncelle();
            MessageBox.Show("Müşteri Silinmiştir");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from musterı where ad like '%" + textBox7.Text + "%'", baglanti);
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from musterı where tc like '%" + textBox6.Text + "%'", baglanti);
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
    }
}
