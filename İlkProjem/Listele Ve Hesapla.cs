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
    public partial class Listele_Ve_Hesapla : Form
    {
        private DateTime verilistarihi;
        private int kiralamaid;
        public Listele_Ve_Hesapla()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=FINISTD033\\;Initial Catalog=bısıklet;Integrated Security=True");
        DataSet daset = new DataSet();

        void guncelle()
        {
            daset.Clear();
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select m.id , m.Tc , m.Ad , m.Soyad, k.bisiklet_id ,k.verilis_tarihi,k.id as kiralamaid from musterı m INNER JOIN kirala k ON m.id=k.musteri_id where alis_tarihi IS NULL ", baglanti);
            da.Fill(daset, "musterı");
            dataGridView1.DataSource = daset.Tables["musterı"];
            baglanti.Close();
        }
        private void Listele_Ve_Hesapla_Load(object sender, EventArgs e)
        {
            guncelle();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kirala where musteri_id like '" + textBox1.Text+ "'" , baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox1.Text = read["musteri_id"].ToString();
                textBox2.Text = read["bisiklet_id"].ToString();
                //textBox3.Text = read["kiraucreti"].ToString();
                
            }
            baglanti.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            verilistarihi = (DateTime)dataGridView1.CurrentRow.Cells["verilis_tarihi"].Value;
            textBox1.Text = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["bisiklet_id"].Value.ToString();
            TimeSpan timeSpan = DateTime.Now - verilistarihi;
            textBox3.Text = timeSpan.TotalMinutes.ToString("0,##");
            kiralamaid = Convert.ToInt32( dataGridView1.CurrentRow.Cells["kiralamaid"].Value);
            SqlConnection baglanti = new SqlConnection("Data Source=FINISTD033\\;Initial Catalog=bısıklet;Integrated Security=True");
            /*baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from bisiklett where id ='" + textBox2.Text + "'", baglanti);
            var sa = komut.ExecuteReader();
            if (sa.Read())
            {
               // double ucret =Convert.ToDouble(sa["kiraucreti"].ToString().Replace("TL", ""));
               //MessageBox.Show(sa["kiraucreti"].ToString().Replace("TL", ""));
               // MessageBox.Show(Convert.ToString(ucret*Convert.ToDouble(textBox3.Text)));
            }*/
            

            
            
            
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Data Source=FINISTD033\\;Initial Catalog=bısıklet;Integrated Security=True");
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from bisiklett where id ='" + textBox2.Text + "'", baglanti);
            var sa = komut.ExecuteReader();
            if (sa.Read())
            {
                double ucret = Convert.ToDouble(sa["kiraucreti"].ToString().Replace("TL", ""));
                MessageBox.Show("Dakikalık Ücret: "+sa["kiraucreti"].ToString()+"\nToplamTutar: "+ Convert.ToString(ucret * Convert.ToDouble(textBox3.Text) + " TL"));
                
                baglanti.Close();
                baglanti.Open();
                SqlCommand add = new SqlCommand("UPDATE bisiklett set durum=@durum where id = '" + textBox2.Text + "'", baglanti);
                add.Parameters.AddWithValue("@durum", "BOŞ");
                add.ExecuteNonQuery();
                baglanti.Close();
                baglanti.Open();
                SqlCommand addd = new SqlCommand("UPDATE kirala set alis_tarihi=@alis_tarihi,ucret=@tutar where id = '" + kiralamaid + "'", baglanti);
                addd.Parameters.AddWithValue("@alis_tarihi", DateTime.Now);
                addd.Parameters.AddWithValue("@tutar", ucret * Convert.ToDouble(textBox3.Text));
                addd.ExecuteNonQuery();
                baglanti.Close();
                            
                MessageBox.Show("Güncellendi");
                Listele_Ve_Hesapla_Load(null, EventArgs.Empty);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
    }
}
