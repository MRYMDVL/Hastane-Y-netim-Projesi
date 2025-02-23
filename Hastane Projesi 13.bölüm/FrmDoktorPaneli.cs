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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }

        SQLbaglantisi bgl = new SQLbaglantisi();
        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            //datagrid 
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Doktorlar",bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //brans cekme
            SqlCommand cmd = new SqlCommand("select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CmbBrans.Items.Add(reader.GetString(0));
            }
            bgl.baglanti().Close();

           
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into Tbl_Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values (@p1,@p2,@p3,@p4,@p5)",bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1",TxtAd.Text);
            cmd.Parameters.AddWithValue("@p2",TxtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3",CmbBrans.Text);
            cmd.Parameters.AddWithValue("@p4",MskTC.Text);
            cmd.Parameters.AddWithValue("@p5",TxtSifre.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Yeni Doktor Başarıyla Eklenmiştir");
            TxtAd.Clear();
            TxtSoyad.Clear();
            TxtSifre.Clear();
            MskTC.Clear();
            CmbBrans.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("delete from Tbl_Doktorlar where DoktorTC=@p1",bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1",MskTC.Text);
            komutsil.ExecuteNonQuery();
            MessageBox.Show("Doktor Kaydı Başarıyla Silindi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            TxtAd.Clear();
            TxtSoyad.Clear();
            TxtSifre.Clear();
            MskTC.Clear();
            CmbBrans.Text = "";
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutguncelle = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p3,DoktorSifre=@p5 where DoktorTC=@p4 ", bgl.baglanti());
            komutguncelle.Parameters.AddWithValue("@p1",TxtAd.Text);
            komutguncelle.Parameters.AddWithValue("@p2",TxtSoyad.Text);
            komutguncelle.Parameters.AddWithValue("@p3",CmbBrans.Text);
            komutguncelle.Parameters.AddWithValue("@p4", MskTC.Text);
            komutguncelle.Parameters.AddWithValue("@p5",TxtSifre.Text);
            komutguncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Kaydı Başarıyla Güncellendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            TxtAd.Clear();
            TxtSoyad.Clear();
            TxtSifre.Clear();
            MskTC.Clear();
            CmbBrans.Text = "";
        }
    }
}
