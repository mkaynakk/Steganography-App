using System;
using System.Drawing;


namespace WindowsFormsApplication10
{
    class Steganografi
    {

        public static string cikarText(Bitmap bmp)
        {
            int renkIndexi = 0;
            int karakterDegeri = 0;


            string cikartilanYazi = String.Empty;


            for (int i = 0; i < bmp.Height; i++)
            {

                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);

                    for (int n = 0; n < 3; n++)
                    {
                        switch (renkIndexi % 3)
                        {
                            case 0:
                                {


                                    karakterDegeri = karakterDegeri * 2 + pixel.R % 2;
                                } break;
                            case 1:
                                {
                                    karakterDegeri = karakterDegeri * 2 + pixel.G % 2;
                                } break;
                            case 2:
                                {
                                    karakterDegeri = karakterDegeri * 2 + pixel.B % 2;
                                } break;
                        }

                        renkIndexi++;


                        if (renkIndexi % 8 == 0)
                        {


                            int sonuc = 0;
                            int nor = karakterDegeri;

                            for (int x = 0; x < 8; x++)
                            {
                                sonuc = sonuc * 2 + nor % 2;

                                nor /= 2;
                            }

                            karakterDegeri = sonuc;


                            if (karakterDegeri == 0)
                            {
                                return cikartilanYazi;
                            }


                            char c = (char)karakterDegeri;


                            cikartilanYazi += c.ToString();
                        }
                    }
                }
            }

            return cikartilanYazi;
        }

        public static Bitmap gomuluYazi(string text, Bitmap bmp)
        {

            bool anahtar = false;
            int R = 0, G = 0, B = 0;
            int karakterDeger = 0;
            int karakterIndex = 0;
            long pixelElemanIndexi = 0;



            for (int i = 0; i < bmp.Height; i++)
            {

                for (int j = 0; j < bmp.Width; j++)
                {

                    Color piksel = bmp.GetPixel(j, i);
                    R = piksel.R - piksel.R % 2;
                    G = piksel.G - piksel.G % 2;
                    B = piksel.B - piksel.B % 2;


                    for (int n = 0; n < 3; n++)
                    {

                        if (pixelElemanIndexi % 8 == 0)
                        {



                            if (karakterIndex >= text.Length)
                            {
                                anahtar = true;
                            }
                            else
                            {
                                karakterDeger = text[karakterIndex++];
                            }
                        }

                        switch (pixelElemanIndexi % 3)
                        {
                            case 0:
                                {
                                    if (anahtar == false)
                                    {
                                        R += karakterDeger % 2;
                                        karakterDeger /= 2;
                                    }
                                } break;
                            case 1:
                                {
                                    if (anahtar == false)
                                    {
                                        G += karakterDeger % 2;

                                        karakterDeger /= 2;
                                    }
                                } break;
                            case 2:
                                {
                                    if (anahtar == false)
                                    {
                                        B += karakterDeger % 2;

                                        karakterDeger /= 2;
                                    }

                                    bmp.SetPixel(j, i, Color.FromArgb(R, G, B));
                                } break;
                        }

                        pixelElemanIndexi++;


                    }
                }
            }

            return bmp;
        }


    }
}
