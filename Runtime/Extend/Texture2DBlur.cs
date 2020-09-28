#if ODIN_INSPECTOR 
using UnityEngine;

namespace Evesoft
{
    public static class Texture2DBlur
    {
        public static Texture2D FastBlur(this Texture2D image, int radius, int iterations)
        {
            Texture2D tex = image;

            for (var i = 0; i < iterations; i++)
            {
                tex = BlurImage(tex, radius, true);
                tex = BlurImage(tex, radius, false);
            }

            return tex;
        }
        public static Texture2D BlurImage(this Texture2D image, int blurSize, bool horizontal)
        {
            Color averageColor = Color.white;
            int blurPixelCount = 0;
            //float avgR,avgG,avgB,avgA ;

            Texture2D blurred = new Texture2D(image.width, image.height);
            int _W = image.width;
            int _H = image.height;
            int xx, yy, x, y;

            if (horizontal)
            {
                for (yy = 0; yy < _H; yy++)
                {
                    for (xx = 0; xx < _W; xx++)
                    {
                        ResetPixel(ref averageColor, ref blurPixelCount);

                        //Right side of pixel

                        for (x = xx; (x < xx + blurSize && x < _W); x++)
                        {
                            AddPixel(ref averageColor, ref blurPixelCount, image.GetPixel(x, yy));
                        }

                        //Left side of pixel

                        for (x = xx; (x > xx - blurSize && x > 0); x--)
                        {
                            AddPixel(ref averageColor, ref blurPixelCount, image.GetPixel(x, yy));
                        }


                        CalcPixel(ref averageColor, blurPixelCount);

                        for (x = xx; x < xx + blurSize && x < _W; x++)
                        {
                            blurred.SetPixel(x, yy, averageColor);
                        }
                    }
                }
            }
            else
            {
                for (xx = 0; xx < _W; xx++)
                {
                    for (yy = 0; yy < _H; yy++)
                    {
                        ResetPixel(ref averageColor, ref blurPixelCount);

                        //Over pixel
                        for (y = yy; (y < yy + blurSize && y < _H); y++)
                        {
                            AddPixel(ref averageColor, ref blurPixelCount, image.GetPixel(xx, y));
                        }
                        //Under pixel

                        for (y = yy; (y > yy - blurSize && y > 0); y--)
                        {
                            AddPixel(ref averageColor, ref blurPixelCount, image.GetPixel(xx, y));
                        }

                        CalcPixel(ref averageColor, blurPixelCount);

                        for (y = yy; y < yy + blurSize && y < _H; y++)
                        {
                            blurred.SetPixel(xx, y, averageColor);

                        }
                    }
                }
            }

            blurred.Apply();
            return blurred;
        }
        private static void AddPixel(ref Color average, ref int blurPixelCount, Color pixel)
        {
            average.r += pixel.r;
            average.g += pixel.g;
            average.b += pixel.b;
            blurPixelCount++;
        }
        private static void ResetPixel(ref Color average, ref int blurPixelCount)
        {
            average.r = 0;
            average.g = 0;
            average.b = 0;
            blurPixelCount = 0;
        }
        private static void CalcPixel(ref Color average, int blurPixelCount)
        {
            average.r = average.r / blurPixelCount;
            average.g = average.g / blurPixelCount;
            average.b = average.b / blurPixelCount;
        }
    }
}
#endif