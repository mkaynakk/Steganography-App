using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace WindowsFormsApplication10
{
    public partial class sifreleme : Form
    {
        int anahtar;
        private Bitmap bmp = null;
        private string cikartilanYazi = string.Empty;


        public sifreleme()
        {
            InitializeComponent();
        }

        private void sifreleme_Load(object sender, EventArgs e)
        {
            AesSifrele.ReadOnly = true;
            AesCoz.ReadOnly = true;
            Metin2.ReadOnly = true;
            Metin3.ReadOnly = true;
            Metin4.ReadOnly = true;
            resimyolu.ReadOnly = true;
            resimyolu2.ReadOnly = true;

        }

        private void Metin_MouseClick(object sender, EventArgs e)
        {

            Metin.Clear();

        }

        private void btnSifrele_Click(object sender, EventArgs e)
        {
            if (Anahtar.Text == "" || Metin.Text == "")
            {
                MessageBox.Show("Bir Anahtar veya Metin Girilmedi!");
            }
            else
            {
                AesSifrele.Text = sifrele.SifreleAES(Metin.Text, Anahtar.Text);
                Metin2.Text = AesSifrele.Text;

            }
        }




        private void btnSifreyiCoz_Click(object sender, EventArgs e)
        {
            if (AesSifrele.Text == "" || Anahtar.Text == string.Empty)
            {
                MessageBox.Show("Çözülecek Bir Data veya Şifre Girilmedi!");
            }

            else
            {
                AesCoz.Text = sifrele.SifreyiCozAES(AesSifrele.Text, Anahtar.Text);
            }


        }



        private void Btnresimsec_Click(object sender, EventArgs e)
        {
            {
                OpenFileDialog open_dialog = new OpenFileDialog();
                open_dialog.Filter = "Image Files (*.jpeg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

                if (open_dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePictureBox.Image = Image.FromFile(open_dialog.FileName);

                    resimyolu.Text = open_dialog.FileName.ToString();
                    imagePictureBox.ImageLocation = resimyolu.Text;
                }
            }
        }

        


        private void BtnResimSifrele_Click(object sender, EventArgs e)
        {
            if (imagePictureBox.Image == null)
            {
                MessageBox.Show("Resim Bulunamadı!");
            }
            else
            {

                bmp = (Bitmap)imagePictureBox.Image;

                string text = Metin2.Text;

                if (text.Equals(""))
                {
                    MessageBox.Show("Şifrelenecek Data Yoktur!");

                    return;
                }



                bmp = Steganografi.gomuluYazi(text, bmp);

                MessageBox.Show("Şifrelendi");

                lblnot.Text = "Not:Şifrelenen Resmi Kaydediniz";
                lblnot.ForeColor = Color.OrangeRed;

                anahtar = 1;
            }
        }






        private void kaydet_Click(object sender, EventArgs e)
        {
            if (resimyolu.Text == string.Empty)
            {
                MessageBox.Show("Lütfen Resim Seçiniz!");
            }

            else
            {
                if (imagePictureBox.Image == null)
                {
                    MessageBox.Show("Kaydedilecek Bir Resim Yok!");
                }

                if (anahtar == 0)
                {
                    MessageBox.Show("Resimi Önce Şifreleyiniz!");
                }

                else
                {


                    SaveFileDialog save_dialog = new SaveFileDialog();
                    save_dialog.Filter = "Png Image|*.png|Bitmap Image|*.bmp";

                    if (save_dialog.ShowDialog() == DialogResult.OK)
                    {
                        switch (save_dialog.FilterIndex)
                        {
                            case 1:
                                {
                                    if (bmp != null)
                                    {
                                        bmp.Save(save_dialog.FileName, ImageFormat.Png);
                                        MessageBox.Show("Kaydedildi");
                                    }

                                    else
                                    {
                                        MessageBox.Show("Resimi Önce Şifreleyiniz!");
                                    }

                                } break;

                            case 2:
                                {
                                    if (bmp != null)
                                    {
                                        bmp.Save(save_dialog.FileName, ImageFormat.Bmp);
                                        MessageBox.Show("Kaydedildi");
                                    }

                                    else
                                    {
                                        MessageBox.Show("Resimi önce şifreleyiniz");
                                    }

                                } break;
                        }


                    }
                }




            }
        }

        private void BtnResimsec2_Click(object sender, EventArgs e)
        {
            {
                OpenFileDialog open_dialog = new OpenFileDialog();
                open_dialog.Filter = "Image Files (*.jpeg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

                if (open_dialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox2.Image = Image.FromFile(open_dialog.FileName);

                    resimyolu2.Text = open_dialog.FileName.ToString();
                    pictureBox2.ImageLocation = resimyolu2.Text;
                }
            }




        }
        private void BtnSifreAl_Click(object sender, EventArgs e)
        {
            if (resimyolu2.Text == string.Empty)
            {
                MessageBox.Show("Lütfen Resim Seçiniz !");
            }

            else  if (pictureBox2.Image == null)
            {
                MessageBox.Show("Resim Bulunamadı !");
            }

            else
            {
                bmp = (Bitmap)pictureBox2.Image;

                string cikartilanYazi = Steganografi.cikarText(bmp);
                Metin3.Text = cikartilanYazi;
            }
        }



        private void BtnSifreCoz_Click(object sender, EventArgs e)
        {
            try
            {
                if (Metin3.Text == "" || SifreResim.Text == "")
                {
                    MessageBox.Show("Çözülecek Bir Data Girilmedi veya Şifre Girilmedi !");
                }
                else
                {

                    Metin4.Text = sifrele.SifreyiCozAES(Metin3.Text, SifreResim.Text);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Girilen Şifre Yanlış !");
            }



        }

    

        
    }
}

















































