using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cinemaAutoFormApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        string ad, soyad;
        int koltukno, sayac = 0, boskoltuk = 20, dolukoltuk = 0;

        int[] dolukoltukdizi = new int[0];

        private void btn_buy_ticket_Click(object sender, EventArgs e)
        {
            if (textisim.Text == "" || txtsoyisim.Text == "" || txtkoltukno.Text == "") MessageBox.Show("Lütfen Boş alanları doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    ad = textisim.Text;
                    soyad = txtsoyisim.Text;
                    koltukno = Convert.ToInt32(txtkoltukno.Text);

                    if (koltukno < 1 || koltukno > 20)
                    {
                        MessageBox.Show("Lütfen geçerli bir koltuk numarası giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtkoltukno.Text = "";
                    }
                    else
                    {

                        if (Array.IndexOf(dolukoltukdizi, koltukno) == -1)
                        {

                            Label koltukara = this.Controls.Find("koltuk" + koltukno.ToString(), true).FirstOrDefault() as Label;

                            if (koltukara != null)
                            {
                                koltukara.Text += "\r" + ad + " " + soyad;
                                koltukara.BackColor = Color.GreenYellow;
                                dolukoltuk++;
                                boskoltuk--;

                                Array.Resize(ref dolukoltukdizi, dolukoltukdizi.Length + 1);
                                dolukoltukdizi[sayac] = koltukno;
                                sayac++;

                                lbldolu.Text = dolukoltuk.ToString();
                                lblbos.Text = boskoltuk.ToString();

                                textisim.Text = "";
                                txtsoyisim.Text = "";
                                txtkoltukno.Text = "";

                                Image bos_koltuk = Properties.Resources.red_chair;
                                koltukara.Image = bos_koltuk;

                                txt_status.Text = koltukno + " Numaralı Koltuk " + ad + " " + soyad + " adına kayıt edildi.";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Girmiş olduğunuz koltuk numarasına ait koltuk dolu", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtkoltukno.Text = "";
                        }
                    }
                }

                catch (Exception)
                {
                    MessageBox.Show("Lütfen geçerli bir koltuk numarası giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtkoltukno.Text = "";
                }
            }
        }

        private void btn_cancel_ticket(object sender, EventArgs e)
        {
            try
            {
                koltukno = Convert.ToInt32(txtiptalkoltukno.Text);

                if (koltukno < 1 || koltukno > 20)
                {
                    MessageBox.Show("Lütfen geçerli bir koltuk numarası giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtiptalkoltukno.Text = "";
                }

                else
                {
                    if (Array.IndexOf(dolukoltukdizi, koltukno) != -1)
                    {

                        Label koltukara = this.Controls.Find("koltuk" + koltukno.ToString(), true).FirstOrDefault() as Label;
                        if (koltukara != null)
                        {
                            koltukara.Text = koltukno + ".koltuk";
                            koltukara.BackColor = Color.FloralWhite;
                            dolukoltuk--;
                            boskoltuk++;

                            int sirano = Array.IndexOf(dolukoltukdizi, koltukno);
                            Array.Clear(dolukoltukdizi, sirano, 1);

                            lbldolu.Text = dolukoltuk.ToString();
                            lblbos.Text = boskoltuk.ToString();
                            txtiptalkoltukno.Text = "";

                            Image bos_koltuk = Properties.Resources.green_chair;
                            koltukara.Image = bos_koltuk;
                        }
                    }
                    else
                    {
                        MessageBox.Show("İptal edilmek istenen koltuk zaten boş!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtiptalkoltukno.Text = "";
                    }
                }

            }

            catch (Exception)
            {
                MessageBox.Show("Lütfen geçerli bir koltuk numarası giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtiptalkoltukno.Text = "";
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lbldolu.Text = dolukoltuk.ToString();
            lblbos.Text = boskoltuk.ToString();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}