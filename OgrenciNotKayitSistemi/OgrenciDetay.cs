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

namespace OgrenciNotKayitSistemi
{
    public partial class OgrenciDetay : Form
    {
        public OgrenciDetay()
        {
            InitializeComponent();
        }
        public string numara;

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-TFCL3EH;Initial Catalog=DbNotKayit;Integrated Security=True");

        private void label5_Click(object sender, EventArgs e) {

        }

        private void OgrenciDetay_Load(object sender, EventArgs e)
        {
            LblNumara.Text = numara;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from TBLDERS where OGRNUMARA=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[2].ToString() + " " + dr[3].ToString();
                LblSinav1.Text = dr[4].ToString();
                LblSinav2.Text = dr[5].ToString();
                LblSinav3.Text = dr[6].ToString();
                LblOrtalama.Text = dr[7].ToString();
                LblDurum.Text = dr[8].ToString();
            }
            baglanti.Close();
        }
    }
}
