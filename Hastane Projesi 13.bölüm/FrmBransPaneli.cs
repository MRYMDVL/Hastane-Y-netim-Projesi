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
    public partial class FrmBransPaneli : Form
    {
        public FrmBransPaneli()
        {
            InitializeComponent();
        }
        SQLbaglantisi bgl = new SQLbaglantisi();
        private void FrmBransPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Branslar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komutekle = new SqlCommand("insert into Tbl_branslar (Bransad) values (@p1)",bgl.baglanti());
            
            komutekle.Parameters.AddWithValue("@p1",TxtBrans.Text);
            komutekle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Yeni Branş Eklendi","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
            TxtBrans.Clear();
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtBrans.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("delete from Tbl_Branslar where Bransid=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1",Txtid.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Başarıyla Silindi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutguncelle = new SqlCommand("update Tbl_Branslar set BransAd=@p1 where Bransid=@p2",bgl.baglanti());
            komutguncelle.Parameters.AddWithValue("@p1",TxtBrans.Text);
            komutguncelle.Parameters.AddWithValue("@p2", Txtid.Text);
            komutguncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Başarıyla Güncellendi","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
