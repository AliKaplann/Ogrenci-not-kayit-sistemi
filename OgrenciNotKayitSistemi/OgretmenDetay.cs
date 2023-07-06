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

namespace OgrenciNotKayitSistemi
{
    public partial class OgretmenDetay : Form
    {
        public OgretmenDetay()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-TFCL3EH;Initial Catalog=DbNotKayit;Integrated Security=True");
        string connectionString = "Data Source=DESKTOP-TFCL3EH;Initial Catalog=DbNotKayit;Integrated Security=True";

        private void OgretmenDetay_Load(object sender, EventArgs e)
        {
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);
            GetStudentStatistics();
        }
        private void GetStudentStatistics()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand passCountCommand = new SqlCommand("SELECT COUNT(*) AS GecenSayisi FROM TBLDERS WHERE DURUM = 'True'", connection);
                int passCount = Convert.ToInt32(passCountCommand.ExecuteScalar());

                SqlCommand failCountCommand = new SqlCommand("SELECT COUNT(*) AS KalanSayisi FROM TBLDERS WHERE DURUM = 'False'", connection);
                int failCount = Convert.ToInt32(failCountCommand.ExecuteScalar());

                lblgecen.Text = passCount.ToString();
                lblkalan.Text = failCount.ToString();

                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            string durum;
            s1 = Convert.ToDouble(txtsnv1.Text);
            s2 = Convert.ToDouble(txtsnv2.Text);
            s3 = Convert.ToDouble(txtsnv3.Text);
            ortalama = (s1 + s2 + s3) / 3;
            lblortalama.Text = ortalama.ToString();

            if (ortalama>=50)
            {
                durum = "True";
            }
            else
            {
                durum = "False";
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("update TBLDERS set OGRS1=@P1,OGRS2=@P2,OGRS3=@P3,ORTALAMA=@P4,DURUM=@P5 WHERE OGRNUMARA=@P6", baglanti);
            komut.Parameters.AddWithValue("@P1", txtsnv1.Text);
            komut.Parameters.AddWithValue("@P2", txtsnv2.Text);
            komut.Parameters.AddWithValue("@P3", txtsnv3.Text);
            komut.Parameters.AddWithValue("@P4", decimal.Parse(lblortalama.Text));
            komut.Parameters.AddWithValue("@P5", durum);
            komut.Parameters.AddWithValue("@P6", msknumara.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Notları Güncellendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLDERS (OGRNUMARA,OGRAD,OGRSOYAD) values(@P1,@P2,@P3)", baglanti);
            komut.Parameters.AddWithValue("@P1", msknumara.Text);
            komut.Parameters.AddWithValue("@P2", txtad.Text);
            komut.Parameters.AddWithValue("@P3", txtsoyad.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            msknumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtsoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtsnv1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtsnv2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtsnv3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }
    }
    }
