using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb; //Access Bağlantı Dosyaları

namespace otopark_programı
{
    public partial class Form1 : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source= otopark.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        DataSet ds = new DataSet();

        public Form1()
        {
            InitializeComponent();
        }
        void listele()
        {
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * from kiralama", baglanti);
            adtr.Fill(ds, "kiralama");
            dataGridView1.DataSource = ds.Tables["kiralama"];
            adtr.Dispose();
            baglanti.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            komut = new OleDbCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "update kiralama set s_no='" + sno.Text + "', tc='" + tc.Text + "', adsoyad='" + adsoyad.Text + "', tel='" + telefon.Text + "',model='" + model.Text + "',renk='" + renk.Text + "',plaka='" + plaka.Text + "',g_saati='" + girissaati.Text + "',c_saati='" + cikissaati.Text + "',tarih='" + tarih.Text + "',ucret='" + ucret.Text + "',resim='" + pictureBox1.ImageLocation + "' where s_no=" + sno.Text + "";
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("kayıt güncellendi");
            ds.Clear();
            listele();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=otopark.accdb");
            adtr = new OleDbDataAdapter("Select * from kiralama where s_no like '" + sno.Text + "%'", baglanti);
            ds = new DataSet();
            baglanti.Open();
            adtr.Fill(ds, "kiralama");
            dataGridView1.DataSource = ds.Tables["kiralama"];
            baglanti.Close();
        }
        //kayıt silme
        private void button6_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "Delete from kiralama where s_no=" + sno.Text + "";
            komut.ExecuteNonQuery();
            komut.Dispose();
            baglanti.Close();
            MessageBox.Show("kayıt silindi");
            ds.Clear();
            listele();
        }
        //kayıt ekleme
        private void button1_Click(object sender, EventArgs e)
        {
            resim.Text = pictureBox1.ImageLocation;
            if (sno.Text != "" && tc.Text != "" && adsoyad.Text != "" && telefon.Text != "" && renk.Text != "" && plaka.Text != "" && girissaati.Text != "" && renk.Text != "" && cikissaati.Text != "" && tarih.Text != "" && ucret.Text != "" && resim.Text != "")
            {
                komut.Connection = baglanti;
                komut.CommandText = "Insert Into kiralama(s_no,tc,adsoyad,tel,model,renk,plaka,g_saati,c_saati,tarih,ucret,resim) Values ('" + sno.Text + "','" + tc.Text + "','" + adsoyad.Text + "','" + telefon.Text + "','" + model.Text + "','" + renk.Text + "','" + plaka.Text + "','" + girissaati.Text + "','" + cikissaati.Text + "','" + tarih.Text + "','" + ucret.Text + "','" + resim.Text + "')";
                baglanti.Open();
                komut.ExecuteNonQuery();
                komut.Dispose();
                baglanti.Close();
                MessageBox.Show("Kayıt Tamamlandı!");
                ds.Clear();
                listele();
            }
            else
            {
                MessageBox.Show("Boş alan geçmeyiniz!");
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            sno.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            tc.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            adsoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            telefon.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            model.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            renk.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            plaka.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            girissaati.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            cikissaati.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            tarih.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            ucret.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.CurrentRow.Cells[11].Value.ToString();

        }


    }
}

