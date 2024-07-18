using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OgrenciBilgiSistemi
{
    public partial class DersListele : Form
    {

        public SqlConnection baglan = new SqlConnection("server=DESKTOP-K05KM2V;initial catalog = OBS; integrated security = SSPI");
        public DataSet ds = new DataSet();


        public DersListele()
        {
            InitializeComponent();
        }

        void listele()
        {
            ds.Clear();
            baglan.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Dersler", baglan);
            da.Fill(ds, "Dersler");
            dataGridView1.DataSource = ds.Tables["Dersler"];
            baglan.Close();
        }

        private void DersEkle_Load(object sender, EventArgs e)
        {
            listele();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Ders Alanı Boş olamaz");
            }
            else
            {
                SqlCommand DersAdiSorgulama = new SqlCommand("SELECT COUNT(dersadi) FROM Dersler WHERE dersadi = @dersadi", baglan);
                DersAdiSorgulama.Parameters.AddWithValue("@dersadi", textBox2.Text);

                baglan.Open();
                int saydir = (int)DersAdiSorgulama.ExecuteScalar();
                baglan.Close();

                if (saydir > 0)
                {
                    MessageBox.Show("Böyle bir ders bulunuyor..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    baglan.Open();
                    
                    SqlCommand DersEkle = new SqlCommand("INSERT INTO Dersler(dersadi) values(@dersadi)", baglan);
                    DersEkle.Parameters.AddWithValue("@dersadi", textBox2.Text);
                    
                    DersEkle.ExecuteNonQuery();
                    ds.Clear();
                    baglan.Close();
                    listele();
                    textBox2.Text = "";

                    MessageBox.Show("Ders Eklendi");
                }
                baglan.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Boş alan olamaz");
            }
            else
            {
                SqlCommand DersSil = new SqlCommand("DELETE FROM Dersler WHERE id=@id", baglan);
                DersSil.Parameters.AddWithValue("@id", textBox3.Text);
                
                baglan.Open();
                DersSil.ExecuteNonQuery();
                ds.Clear();
                baglan.Close();
                listele();
                textBox3.Text = "";
                MessageBox.Show("Kayıt Silindi..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show("Boş alan bırakmayınız");
            }
            else
            {
                SqlCommand DersGuncelle = new SqlCommand("UPDATE Dersler SET dersadi=@dersadi WHERE id=@id ", baglan);
                DersGuncelle.Parameters.AddWithValue("@id", textBox3.Text);
                DersGuncelle.Parameters.AddWithValue("dersadi", textBox4.Text);
                
                baglan.Open();
                DersGuncelle.ExecuteNonQuery();
                ds.Clear();
                baglan.Close();
                listele();

                textBox4.Text = "";
                MessageBox.Show("Ders adı güncellenmiştir");
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
