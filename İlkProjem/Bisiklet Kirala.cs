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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=FINISTD033\\;Initial Catalog=bısıklet;Integrated Security=True");
        
        private void Form6_Load(object sender, EventArgs e)
        {
            DataSet mus = new DataSet();
            baglanti.Open();
            
            SqlCommand musteri = new SqlCommand("select * from musterı", baglanti);
            
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(musteri);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Ad,Soyad";
            comboBox1.ValueMember = "id";
            /*SqlDataReader muster = musteri.ExecuteReader();
            while (muster.Read())
            {
                //MessageBox.Show(muster["Ad"]+" "+muster["Soyad"]);
                comboBox1.Items.Add(muster["Ad"] + " " + muster["Soyad"]);
            }
            muster.Close();*/
            SqlCommand bisikletsorgu  = new SqlCommand("select * from bisiklett where durum='BOŞ'", baglanti);
            SqlDataAdapter bsDataAdapter = new SqlDataAdapter(bisikletsorgu);
            DataTable bsdt = new DataTable();
            bsDataAdapter.Fill(bsdt);
            comboBox2.DataSource = bsdt;
            comboBox2.DisplayMember = "Marka";
            comboBox2.ValueMember = "id";
            /*SqlDataReader bisiklet  = bisikletsorgu.ExecuteReader();
            while (bisiklet.Read())
            {
                //MessageBox.Show(muster["Ad"]+" "+muster["Soyad"]);
                comboBox2.Items.Add(bisiklet["marka"] + " " + bisiklet["model"] + " " + bisiklet["vites"] + " " + bisiklet["renk"] + " " + bisiklet["kiraucreti"]);
            }
            bisiklet.Close();
            baglanti.Close();
            */
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand sqlCommand = new SqlCommand("insert into kirala(musteri_id,bisiklet_id,verilis_tarihi) values (@musteri_id,@bisiklet_id,@verilis_tarihi)", baglanti);
            sqlCommand.Parameters.AddWithValue("@musteri_id", comboBox1.SelectedValue);
            sqlCommand.Parameters.AddWithValue("@bisiklet_id", comboBox2.SelectedValue);
            sqlCommand.Parameters.AddWithValue("@verilis_tarihi", dateTimePicker1.Value);
            sqlCommand.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Veri Kaydedildi");
            baglanti.Open();
            SqlCommand add = new SqlCommand("UPDATE bisiklett set durum=@durum where id='"+ comboBox2.SelectedValue+"'" , baglanti);
            add.Parameters.AddWithValue("@durum", "DOLU");
            add.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncellendi");
            Form6_Load(null, EventArgs.Empty);

        }

        private void comboBox1_Format(object sender, ListControlConvertEventArgs e)
        {
            string Ad = ((DataRowView)e.ListItem)["Ad"].ToString();
            string Soyad = ((DataRowView)e.ListItem)["Soyad"].ToString();
            e.Value = Ad + " " + Soyad;

        }

        private void comboBox2_Format(object sender, ListControlConvertEventArgs e)
        {
            string Marka = ((DataRowView)e.ListItem)["marka"].ToString();
            string Model = ((DataRowView)e.ListItem)["model"].ToString();
            string Vites = ((DataRowView)e.ListItem)["vites"].ToString();
            string Renk = ((DataRowView)e.ListItem)["renk"].ToString();
            string KiraÜcreti = ((DataRowView)e.ListItem)["kiraucreti"].ToString();
            e.Value = Marka + " " + Model + " " + " " + Vites + " " + Renk + " " + KiraÜcreti;
        }
    }
}
