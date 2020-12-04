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
    public partial class FrmSınavNotlar : Form
    {
        public FrmSınavNotlar()
        {
            InitializeComponent();
        }
        DataSet1TableAdapters.TBLNOTLARTableAdapter ds = new DataSet1TableAdapters.TBLNOTLARTableAdapter();
        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-CNLTPU11;Initial Catalog=BonusOkul;Integrated Security=True");
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.NotListesi(int.Parse(txtOgrencıID.Text));
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void FrmSınavNotlar_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLDERSLER", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbDers.DisplayMember = "DERSAD";
            cmbDers.ValueMember = "DERSID";
            cmbDers.DataSource = dt;
            baglanti.Close();
        }
        int notid;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            notid=int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtOgrencıID.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSınav1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSınav2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtSınav3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtProje.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtOrtalama.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtDurum.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
        }
        int sinav1, sinav2, sinav3, proje;
        double ortalama;

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            ds.OgrencıSil(int.Parse(txtOgrencıID.Text));
            MessageBox.Show("ÖGRENCİ NOTU SİLİNDİ TEKRAR 'ARA'basınız");
        }

        private void BtnHesapla_Click(object sender, EventArgs e)
        {
            
            //string durum;
            sinav1 = Convert.ToInt16(txtSınav1.Text);
            sinav2 = Convert.ToInt16(txtSınav2.Text);
            sinav3 = Convert.ToInt16(txtSınav3.Text);
            proje = Convert.ToInt16(txtProje.Text);
            ortalama = (sinav1 + sinav2 + sinav3 + proje) / 4;
            txtOrtalama.Text = ortalama.ToString();
            if (ortalama >= 50)
            {
                txtDurum.Text = "True";
            }
            else
            {
                txtDurum.Text = "false"; 
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            ds.NotGuncelle(byte.Parse(cmbDers.SelectedValue.ToString()), int.Parse(txtOgrencıID.Text),byte.Parse(txtSınav1.Text), byte.Parse(txtSınav2.Text), byte.Parse(txtSınav3.Text), byte.Parse(txtProje.Text), decimal.Parse(txtOrtalama.Text), bool.Parse(txtDurum.Text), notid);
            MessageBox.Show("GÜNCELLENDİ TEKRAR  'ARA' BASINIZ ");
        }
    }
}
