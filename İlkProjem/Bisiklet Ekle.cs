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
    public partial class Form5 : Form
    {
        
        public Form5()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=FINISTD033\\;Initial Catalog=bısıklet;Integrated Security=True");
        DataSet da = new DataSet();
        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            try
            {
                comboBox2.Items.Clear();
                if (comboBox1.SelectedIndex == 0)
                {
                    comboBox2.Items.Add("Oxylane 120");
                    comboBox2.Items.Add("Original 500");
                    comboBox2.Items.Add("RockRider ST500");
                    comboBox2.Items.Add("RiverSide 100");
                    comboBox2.Items.Add("ST 100");
                }
                else if (comboBox1.SelectedIndex == 1)

                {
                    comboBox2.Items.Add("TOURING 713");
                    comboBox2.Items.Add("TRAVEL 506");
                    comboBox2.Items.Add("RAINBOW 28");
                    comboBox2.Items.Add("MILANO 26");
                    comboBox2.Items.Add("CARMEN 24");
                }
                else if (comboBox1.SelectedIndex == 2)

                {
                    comboBox2.Items.Add("Prowler Glorious");
                    comboBox2.Items.Add("Lite Force");
                    comboBox2.Items.Add("Penny Lane");
                    comboBox2.Items.Add("Life Ride");
                    comboBox2.Items.Add("Life Joy");
                }
                else if (comboBox1.SelectedIndex == 3)

                {
                    comboBox2.Items.Add("İstanbul 27 S 700");
                    comboBox2.Items.Add("Sarajevo 700");
                    comboBox2.Items.Add("Bodrum 26");
                    comboBox2.Items.Add("Üsküp 700");
                    comboBox2.Items.Add("Oxford");
                }

            }
            catch 
            {

                ;
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand veriekle = new SqlCommand("insert into bisiklett(vites,marka,model,lastik,renk,kiraucreti) values (@vites,@marka,@model,@lastik,@renk,@kiraucreti)", baglanti);
            veriekle.Parameters.AddWithValue("@vites", textBox1.Text);
            veriekle.Parameters.AddWithValue("@marka", comboBox1.Text);
            veriekle.Parameters.AddWithValue("@model", comboBox2.Text);
            veriekle.Parameters.AddWithValue("@lastik", textBox2.Text);
            veriekle.Parameters.AddWithValue("@renk", textBox3.Text);
            veriekle.Parameters.AddWithValue("@kiraucreti", textBox4.Text);
           // veriekle.Parameters.AddWithValue("@resim", pictureBox1.ImageLocation);
           // veriekle.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
           // veriekle.Parameters.AddWithValue("@durumu", "BOŞ");
            veriekle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Veri Eklendi");
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
