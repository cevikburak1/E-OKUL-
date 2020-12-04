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

namespace E_okul
{
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-CNLTPU11;Initial Catalog=BonusOkul;Integrated Security=True");
        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();
        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListesi();
           
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLKULUPLER", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbKulüp.DisplayMember = "KULUPAD";
            cmbKulüp.ValueMember = "KULUPID";
            cmbKulüp.DataSource = dt;
            baglanti.Close();
        }
        string c = "";
        private void BtnEkle_Click(object sender, EventArgs e)
        {
           
          
            ds.OgrencıEkle(txtAd.Text, txtSoyad.Text, byte.Parse(cmbKulüp.SelectedValue.ToString()), c);
            MessageBox.Show("Ögrenci Başarı İle Eklendi");
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListesi();
        }

        private void cmbKulüp_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtKulupID.Text = cmbKulüp.SelectedValue.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            ds.OgrenciSil(int.Parse(txtKulupID.Text));
            MessageBox.Show("Ögrenci Silindi");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKulupID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbKulüp.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            ds.OgrencıGuncelle(txtAd.Text,txtSoyad.Text,byte.Parse(cmbKulüp.SelectedValue.ToString()),c,int.Parse(txtKulupID.Text));
            MessageBox.Show("GÜNCELLENDİ");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                c = "ERKEK";
            }
           
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                c = "KIZ";
            }
           
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
          dataGridView1.DataSource=  ds.OgrenciGetir(txtAra.Text);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciGetir(txtAra.Text);
        }
    }
}
