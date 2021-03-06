#if ODIN_INSPECTOR 

using System.Threading;
using UnityEngine;

namespace Evesoft
{
    public static class Texture2DExtend
    {
        public class ThreadData
        {
            public int start;
            public int end;
            public ThreadData(int s, int e)
            {
                start = s;
                end = e;
            }
        }

        private static Color[] texColors, newColors;
        private static float ratioX, ratioY;
        private static int w, w2, finishCount;
        private static Mutex mutex;

        public static void Point(this Texture2D tex, int newWidth, int newHeight)
        {
            ThreadedScale(tex, newWidth, newHeight, false);
        }
        public static void Bilinear(this Texture2D tex, int newWidth, int newHeight)
        {
            ThreadedScale(tex, newWidth, newHeight, true);
        }
        public static void BilinearScale(object obj)
        {
            ThreadData threadData = (ThreadData)obj;
            for (var y = threadData.start; y < threadData.end; y++)
            {
                int yFloor = (int)Mathf.Floor(y * ratioY);
                var y1 = yFloor * w;
                var y2 = (yFloor + 1) * w;
                var yw = y * w2;

                for (var x = 0; x < w2; x++)
                {
                    int xFloor = (int)Mathf.Floor(x * ratioX);
                    var xLerp = x * ratioX - xFloor;
                    newColors[yw + x] = ColorLerpUnclamped(
                        ColorLerpUnclamped(texColors[y1 + xFloor],
                        texColors[y1 + xFloor + 1], xLerp),
                        ColorLerpUnclamped(texColors[y2 + xFloor],
                        texColors[y2 + xFloor + 1], xLerp),
                        y * ratioY - yFloor);
                }
            }

            mutex.WaitOne();
            finishCount++;
            mutex.ReleaseMutex();
        }
        public static void PointScale(object obj)
        {
            ThreadData threadData = (ThreadData)obj;
            for (var y = threadData.start; y < threadData.end; y++)
            {
                var thisY = (int)(ratioY * y) * w;
                var yw = y * w2;
                for (var x = 0; x < w2; x++)
                {
                    newColors[yw + x] = texColors[(int)(thisY + ratioX * x)];
                }
            }

            mutex.WaitOne();
            finishCount++;
            mutex.ReleaseMutex();
        }
        private static void ThreadedScale(this Texture2D tex, int newWidth, int newHeight, bool useBilinear)
        {
            texColors = tex.GetPixels();
            newColors = new Color[newWidth * newHeight];
            if (useBilinear)
            {
                ratioX = 1.0f / ((float)newWidth / (tex.width - 1));
                ratioY = 1.0f / ((float)newHeight / (tex.height - 1));
            }
            else
            {
                ratioX = ((float)tex.width) / newWidth;
                ratioY = ((float)tex.height) / newHeight;
            }
            w = tex.width;
            w2 = newWidth;
            var cores = Mathf.Min(SystemInfo.processorCount, newHeight);
            var slice = newHeight / cores;

            finishCount = 0;
            if (mutex == null)
            {
                mutex = new Mutex(false);
            }
            if (cores > 1)
            {
                int i = 0;
                ThreadData threadData;
                for (i = 0; i < cores - 1; i++)
                {
                    threadData = new ThreadData(slice * i, slice * (i + 1));
                    ParameterizedThreadStart ts = useBilinear ? new ParameterizedThreadStart(BilinearScale) : new ParameterizedThreadStart(PointScale);
                    Thread thread = new Thread(ts);
                    thread.Start(threadData);
                }
                threadData = new ThreadData(slice * i, newHeight);
                if (useBilinear)
                {
                    BilinearScale(threadData);
                }
                else
                {
                    PointScale(threadData);
                }
                while (finishCount < cores)
                {
                    Thread.Sleep(1);
                }
            }
            else
            {
                ThreadData threadData = new ThreadData(0, newHeight);
                if (useBilinear)
                {
                    BilinearScale(threadData);
                }
                else
                {
                    PointScale(threadData);
                }
            }

            tex.Resize(newWidth, newHeight);
            tex.SetPixels(newColors);
            tex.Apply();

            texColors = null;
            newColors = null;
        }
        public static Texture2D CropAndResizeImage(this Texture2D texture, int toSize)
        {
            if (texture == null)
                return null;

            //Check size already the same
            if (texture.width == toSize && texture.height == toSize)
            {
                return texture;
            }

            int x, y;

            int width = toSize;
            int height = toSize;

            if (texture.width > texture.height)
            {
                height = toSize;
                width = (texture.width * toSize / texture.height);
            }

            if (texture.height > texture.width)
            {
                width = toSize;
                height = (texture.height * toSize / texture.width);
            }

            Point(texture, width, height);

            x = (texture.width - toSize) / 2;
            y = (texture.height - toSize) / 2;

            Texture2D result = new Texture2D(toSize, toSize, TextureFormat.ARGB32, false);
            result.SetPixels(texture.GetPixels(x, y, toSize, toSize));
            result.Apply();

            return result;
        }
        private static Color ColorLerpUnclamped(Color c1, Color c2, float value)
        {
            return new Color(c1.r + (c2.r - c1.r) * value,
                                c1.g + (c2.g - c1.g) * value,
                                c1.b + (c2.b - c1.b) * value,
                                c1.a + (c2.a - c1.a) * value);
        }
    }
}

#endif