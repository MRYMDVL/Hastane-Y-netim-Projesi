using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace Hastane_Projesi_13.bölüm
{
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        public string Tc;
        SQLbaglantisi bgl = new SQLbaglantisi();
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = Tc;

            //ad-soyad çekme
            SqlCommand komut = new SqlCommand("Select SekreterAdSoyad from Tbl_Sekreter where SekreterTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",LblTC.Text);
            SqlDataReader rd = komut.ExecuteReader();
            while (rd.Read())
            {
                LblAdSoyad.Text = rd[0].ToString();
            }
            bgl.baglanti().Close();

            //Branşları çekme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Branslar",bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
            //Doktorlari cekme
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select (DoktorAd + ' ' + DoktorSoyad) as 'Doktorlar',DoktorBrans from Tbl_Doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Branşları Cmbye aktarma
            SqlCommand komut2 = new SqlCommand("select BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader rd2 = komut2.ExecuteReader();
            while (rd2.Read())
            {
                CmbBrans.Items.Add(rd2[0].ToString());
            }

            //deneme yapiyorum
            
            

        }
       
        private void ButonKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@p1,@p2,@p3,@p4)", bgl.baglanti());           
            komutkaydet.Parameters.AddWithValue("@p1",MskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@p2",MskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@p3",CmbBrans.Text);
            komutkaydet.Parameters.AddWithValue("@p4",CmbDoktor.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu");
            MskTarih.Clear();
            MskSaat.Clear();
            CmbBrans.Text = "";
            CmbDoktor.Text = "";
          
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            SqlCommand kom = new SqlCommand("select (DoktorAd+' '+DoktorSoyad) from Tbl_Doktorlar where DoktorBrans=@p1",bgl.baglanti());
            kom.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader drs = kom.ExecuteReader();
            while (drs.Read())
            {
                CmbDoktor.Items.Add(drs[0].ToString());
            }

            bgl.baglanti().Close();
        }

        private void BtnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_duyurular (Duyuru) values (@p1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",RchDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu");
            RchDuyuru.Clear();
        }

        private void BtnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli fr  = new FrmDoktorPaneli();
            fr.Show();
        }

        private void BtnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBransPaneli fr = new FrmBransPaneli();
            fr.Show();
        }

        private void BtnListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi fr = new FrmRandevuListesi();
            fr.Show();
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmSekreterGiris fr = new FrmSekreterGiris();
            fr.Show();
            this.Close();
        }
    }
}
