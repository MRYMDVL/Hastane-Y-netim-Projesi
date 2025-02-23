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

namespace Hastane_Projesi_13.bölüm
{
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }

        SQLbaglantisi bgl = new SQLbaglantisi();
        public string tc;
        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTC.Text = tc;

            SqlCommand komut = new SqlCommand("select DoktorAd,DoktorSoyad,DoktorTC,DoktorBrans,DoktorSifre from Tbl_Doktorlar where DoktorTC='"+MskTC.Text+"'",bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text = dr[0].ToString();
                TxtSoyad.Text = dr[1].ToString();
                MskTC.Text = dr[2].ToString();
                CmbBrans.Text = dr[3].ToString();
                TxtSifre.Text= dr[4].ToString();
            }
            bgl.baglanti().Close();
        }

        private void BtnKayitGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p4,DoktorSifre=@p5 where DoktorTC=@p3",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTC.Text);
            komut.Parameters.AddWithValue("@p4", CmbBrans.Text);
            komut.Parameters.AddWithValue("@p5", TxtSifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Kaydınız Başarıyla Güncellenmiştir");
            TxtAd.Clear();
            TxtSoyad.Clear();
            TxtSifre.Clear();
            MskTC.Clear();
            CmbBrans.Text = "";
        }
    }
}
