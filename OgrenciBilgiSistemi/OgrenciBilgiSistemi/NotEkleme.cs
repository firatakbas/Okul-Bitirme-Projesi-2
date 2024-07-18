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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace OgrenciBilgiSistemi
{
    public partial class NotEkleme : Form
    {

        public SqlConnection baglan = new SqlConnection("server=DESKTOP-K05KM2V;initial catalog = OBS; integrated security = SSPI");
        public DataSet ds = new DataSet();

        public NotEkleme()
        {
            InitializeComponent();
        }

        private void NotEkleme_Load(object sender, EventArgs e)
        {
            OgrenciListele();
            DersleriGetir();
            GectiKaldıOrtalamasi();
        }

        void OgrenciListele()
        {
            ds.Clear();
            baglan.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT Ogrenci.numara, Ogrenci.ad, Ogrenci.soyad, OgrenciDers.dersadi, OgrenciDers.vize, OgrenciDers.final, "+ "ROUND((OgrenciDers.vize * 0.4 + OgrenciDers.final * 0.6), 2) AS ortalama" + " FROM Ogrenci LEFT JOIN OgrenciDers ON Ogrenci.numara = OgrenciDers.ogrenci_id", baglan);
            da.Fill(ds, "OgrenciDers");
            dataGridView1.DataSource = ds.Tables["OgrenciDers"];
            baglan.Close();
        }

        void DersleriGetir()
        {
            SqlCommand DersGetir = new SqlCommand("SELECT dersadi FROM Dersler", baglan);
            SqlDataReader dr;

            baglan.Open();

            dr = DersGetir.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["dersadi"]);
            }

            baglan.Close() ;
        }
        
        void NotEkle()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Boş alan bırakmayınız");
            }
            else { 
                baglan.Open();

                SqlCommand NotEkle = new SqlCommand(
                    "INSERT INTO OgrenciDers(ogrenci_id, dersadi, vize, final) " +
                    "values(@ogrenci_id, @dersadi, @vize, @final)", baglan);
                NotEkle.Parameters.AddWithValue("@ogrenci_id", textBox1.Text);
                NotEkle.Parameters.AddWithValue("@dersadi", comboBox1.Text);
                NotEkle.Parameters.AddWithValue("@vize", textBox4.Text);
                NotEkle.Parameters.AddWithValue("@final", textBox5.Text);

                NotEkle.ExecuteNonQuery();
                baglan.Close();
                ds.Clear();
                comboBox1.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";

                MessageBox.Show("Ders Eklendi");
            }
        }
                
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NotEkle();
            OgrenciListele();
            GectiKaldıOrtalamasi();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        void temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            //comboBox1.Text = "";
            comboBox1.SelectedIndex = -1;
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Boş alan olamaz");
            }
            else
            {
                SqlCommand DersSil = new SqlCommand("DELETE FROM OgrenciDers WHERE ogrenci_id=@ogrenci_id", baglan);
                DersSil.Parameters.AddWithValue("@ogrenci_id", textBox1.Text);

                baglan.Open();
                DersSil.ExecuteNonQuery();
                ds.Clear();
                baglan.Close();
                
                textBox3.Text = "";
                MessageBox.Show("Kayıt Silindi..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void notGuncelle()
        {
            SqlCommand notGuncelle = new SqlCommand("UPDATE OgrenciDers SET vize=@vize, final=@final WHERE ogrenci_id=@ogrenci_id ", baglan);
            notGuncelle.Parameters.AddWithValue("@ogrenci_id", textBox1.Text);
            notGuncelle.Parameters.AddWithValue("@vize", textBox4.Text);
            notGuncelle.Parameters.AddWithValue("final", textBox5.Text);

            baglan.Open();
            notGuncelle.ExecuteNonQuery();
            ds.Clear();
            baglan.Close();
            OgrenciListele();
            temizle();
            MessageBox.Show("Not güncellenmiştir");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            notGuncelle();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void AdaGoreListele()
        {
            if (textBox6.Text == "")
            {
                MessageBox.Show("Lütfen aramak istediğiniz öğrenci adını girin.");
            }
            else
            {
                ds.Clear();
                baglan.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT Ogrenci.numara, Ogrenci.ad, Ogrenci.soyad, OgrenciDers.dersadi, OgrenciDers.vize, OgrenciDers.final, " + "ROUND((OgrenciDers.vize * 0.4 + OgrenciDers.final * 0.6), 2) AS ortalama" + " FROM Ogrenci LEFT JOIN OgrenciDers ON Ogrenci.numara = OgrenciDers.ogrenci_id WHERE Ogrenci.ad LIKE '"+textBox6.Text+"' ", baglan);
                da.Fill(ds, "OgrenciDers");
                dataGridView1.DataSource = ds.Tables["OgrenciDers"];
                baglan.Close();
            }
        }

        void NumarayaGoreListele()
        {
            if (textBox7.Text == "")
            {
                MessageBox.Show("Lütfen aramak istediğiniz öğrenci numarasını girin.");
            }
            else
            {
                ds.Clear();
                baglan.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT Ogrenci.numara, Ogrenci.ad, Ogrenci.soyad, OgrenciDers.dersadi, OgrenciDers.vize, OgrenciDers.final, " + "ROUND((OgrenciDers.vize * 0.4 + OgrenciDers.final * 0.6), 2) AS ortalama" + " FROM Ogrenci LEFT JOIN OgrenciDers ON Ogrenci.numara = OgrenciDers.ogrenci_id WHERE Ogrenci.numara LIKE '" + textBox7.Text + "' ", baglan);
                da.Fill(ds, "OgrenciDers");
                dataGridView1.DataSource = ds.Tables["OgrenciDers"];
                baglan.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AdaGoreListele();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            OgrenciListele();
            GectiKaldıOrtalamasi();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            NumarayaGoreListele();
        }

        void GectiKaldıOrtalamasi()
        {
            ds.Clear();
            baglan.Open();
            SqlDataAdapter BolumSorgu = new SqlDataAdapter("SELECT Ogrenci.numara, Ogrenci.ad, Ogrenci.soyad, OgrenciDers.dersadi, OgrenciDers.vize, OgrenciDers.final, " + "  ROUND((OgrenciDers.vize * 0.4 + OgrenciDers.final * 0.6), 2) AS ortalama, " + " CASE WHEN OgrenciDers.vize * 0.4 + OgrenciDers.final * 0.6 >= 60 THEN 'Geçti' " + " ELSE 'Kaldı' " + "    END AS durum " + "FROM Ogrenci " + "LEFT JOIN OgrenciDers ON Ogrenci.numara = OgrenciDers.ogrenci_id", baglan);
            BolumSorgu.Fill(ds, "OgrenciDers");
            dataGridView1.DataSource = ds.Tables["OgrenciDers"];
            baglan.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
