using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB
{
    public class LIB
    {
        /*
        * Converts an image to Format8bppIndexed. The pallete that is used is Grayscale pallete.
        * Works with 24bppRgb and 32Argb.
        */
        public Bitmap ToGreyscale(Bitmap OriginalImage)
        {
            switch (OriginalImage.PixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    Bitmap b = ToGreyscale24Rgb(OriginalImage);
                    AForge.Imaging.Image.SetGrayscalePalette(b);
                    return b;
                case PixelFormat.Format32bppArgb:
                    Bitmap c = ToGreyscale32Argb(OriginalImage);
                    AForge.Imaging.Image.SetGrayscalePalette(c);
                    return c;
                case PixelFormat.Format8bppIndexed:
                    AForge.Imaging.Image.SetGrayscalePalette(OriginalImage);
                    return OriginalImage;
                default:
                    throw new ArgumentException("Wrong Image Format. Try 24RGB or 32ARGB");
            }
        }

        private unsafe Bitmap ToGreyscale24Rgb(Bitmap OriginalImage)
        {
            Bitmap OutputImage = new System.Drawing.Bitmap(OriginalImage.Width, OriginalImage.Height,
                PixelFormat.Format8bppIndexed);
            BitmapData dataO = OutputImage.LockBits(new Rectangle(0, 0, OutputImage.Width, OutputImage.Height),
                ImageLockMode.WriteOnly, OutputImage.PixelFormat);
            BitmapData data =
                OriginalImage.LockBits(new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height),
                    ImageLockMode.ReadOnly, OriginalImage.PixelFormat);
            byte* PtrO = (byte*) dataO.Scan0.ToPointer();
            byte* Ptr = (byte*) data.Scan0.ToPointer();
            for (int x = 0; x < OriginalImage.Width; x++)
            {
                for (int y = 0; y < OriginalImage.Height; y++)
                {
                    int gs =
                        (int)
                            ((*(Ptr + y*data.Stride + x*3)*0.2125) + (*(Ptr + y*data.Stride + x*3 + 1)*0.7154) +
                             (*(Ptr + y*data.Stride + x*3 + 2)*0.0721));
                    *(PtrO + dataO.Stride*y + x) = (byte) gs;

                }
            }
            OriginalImage.UnlockBits(data);
            OutputImage.UnlockBits(dataO);
            return OutputImage;
        }

        private unsafe Bitmap ToGreyscale32Argb(Bitmap OriginalImage)
        {
            Bitmap OutputImage = new System.Drawing.Bitmap(OriginalImage.Width, OriginalImage.Height,
                PixelFormat.Format8bppIndexed);
            BitmapData dataO = OutputImage.LockBits(new Rectangle(0, 0, OutputImage.Width, OutputImage.Height),
                ImageLockMode.WriteOnly, OutputImage.PixelFormat);
            BitmapData data =
                OriginalImage.LockBits(new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height),
                    ImageLockMode.ReadOnly, OriginalImage.PixelFormat);
            byte* PtrO = (byte*) dataO.Scan0.ToPointer();
            byte* Ptr = (byte*) data.Scan0.ToPointer();
            for (int x = 0; x < OriginalImage.Width; x++)
            {
                for (int y = 0; y < OriginalImage.Height; y++)
                {
                    int gs =
                        (int)
                            ((*(Ptr + y*data.Stride + x*4 + 1)*0.2125) + (*(Ptr + y*data.Stride + x*4 + 2)*0.7154) +
                             (*(Ptr + y*data.Stride + x*4 + 3)*0.0721));
                    *(PtrO + dataO.Stride*y + x) = (byte) gs;

                }
            }
            OriginalImage.UnlockBits(data);
            OutputImage.UnlockBits(dataO);
            return OutputImage;
        }

        /*
        * Converts to 8bppIndexed. Instead of using the formula to convert to grayscale, it takes the maximum of RGB and palces it as grayscale value.
        */
        public Bitmap ToGreyscaleHSV(Bitmap OriginalImage)
        {
            switch (OriginalImage.PixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    Bitmap b = ToGreyscale24RgbHSV(OriginalImage);
                    AForge.Imaging.Image.SetGrayscalePalette(b);
                    return b;
                case PixelFormat.Format32bppArgb:
                    Bitmap c = ToGreyscale32ArgbHSV(OriginalImage);
                    AForge.Imaging.Image.SetGrayscalePalette(c);
                    return c;
                case PixelFormat.Format8bppIndexed:
                    AForge.Imaging.Image.SetGrayscalePalette(OriginalImage);
                    return OriginalImage;
                default:
                    throw new ArgumentException("Wrong Image Format. Try 24RGB or 32ARGB");
            }
        }

        private unsafe Bitmap ToGreyscale24RgbHSV(Bitmap OriginalImage)
        {
            Bitmap OutputImage = new System.Drawing.Bitmap(OriginalImage.Width, OriginalImage.Height,
                PixelFormat.Format8bppIndexed);
            BitmapData dataO = OutputImage.LockBits(new Rectangle(0, 0, OutputImage.Width, OutputImage.Height),
                ImageLockMode.WriteOnly, OutputImage.PixelFormat);
            BitmapData data =
                OriginalImage.LockBits(new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height),
                    ImageLockMode.ReadOnly, OriginalImage.PixelFormat);
            byte* PtrO = (byte*)dataO.Scan0.ToPointer();
            byte* Ptr = (byte*)data.Scan0.ToPointer();
            for (int x = 0; x < OriginalImage.Width; x++)
            {
                for (int y = 0; y < OriginalImage.Height; y++)
                {
                    *(PtrO + y*dataO.Stride + x) = Math.Max(Math.Max(*(Ptr + y * data.Stride + x*3),*(Ptr + y * data.Stride + x * 3 + 1)), *(Ptr + y*data.Stride + x * 3 + 2));
                }
            }
            OriginalImage.UnlockBits(data);
            OutputImage.UnlockBits(dataO);
            return OutputImage;
        }

        private unsafe Bitmap ToGreyscale32ArgbHSV(Bitmap OriginalImage)
        {
            Bitmap OutputImage = new System.Drawing.Bitmap(OriginalImage.Width, OriginalImage.Height,
                PixelFormat.Format8bppIndexed);
            BitmapData dataO = OutputImage.LockBits(new Rectangle(0, 0, OutputImage.Width, OutputImage.Height),
                ImageLockMode.WriteOnly, OutputImage.PixelFormat);
            BitmapData data =
                OriginalImage.LockBits(new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height),
                    ImageLockMode.ReadOnly, OriginalImage.PixelFormat);
            byte* PtrO = (byte*)dataO.Scan0.ToPointer();
            byte* Ptr = (byte*)data.Scan0.ToPointer();
            for (int x = 0; x < OriginalImage.Width; x++)
            {
                for (int y = 0; y < OriginalImage.Height; y++)
                {
                    *(PtrO + y * dataO.Stride + x) = Math.Max(Math.Max(*(Ptr + y * data.Stride + x * 3 + 1), *(Ptr + y * data.Stride + x * 3 + 2)), *(Ptr + y * data.Stride + x * 3 + 3));
                }
            }
            OriginalImage.UnlockBits(data);
            OutputImage.UnlockBits(dataO);
            return OutputImage;
        }
        /*
        * Crops the image, defined by the given coordinates of the top left corner and bottom right corner.
        */
        public Bitmap Crop(Bitmap OriginalImage, int PixCoordX1, int PixCoordY1, int PixCoordX2, int PixCoordY2)
        {
            if ((PixCoordX1 < 0 || PixCoordX2 < 0 || (PixCoordY1 < 0 || PixCoordY2 < 0)) &&
                (PixCoordX1 > OriginalImage.Height || PixCoordX2 > OriginalImage.Height ||
                 PixCoordY1 > OriginalImage.Width && PixCoordY2 > OriginalImage.Width))
                throw new ArgumentException("Parameter cannot be less than zero or higher than the original size");
            Point F = new Point(PixCoordX1, PixCoordY1);
            Point S = new Point(PixCoordX2, PixCoordY2);
            if (OriginalImage.PixelFormat != PixelFormat.Format8bppIndexed)
            {
                Bitmap result = new Bitmap(S.X, S.Y, OriginalImage.PixelFormat);
                Graphics g = Graphics.FromImage(result);
                g.DrawImage(OriginalImage, new Rectangle(0, 0, result.Width, result.Height), new Rectangle(F.X, F.Y, S.X, S.Y), GraphicsUnit.Pixel);
                return result;
            }

            return CropGreyScale8bppIndexed(OriginalImage, F, S);
        }

        private unsafe Bitmap CropGreyScale8bppIndexed(Bitmap OriginalImage, Point F, Point S)
        {
            S.X += F.X;
            S.Y += F.Y;
            Bitmap bitmap = new Bitmap((int) ((double) S.X - (double) F.X), (int) ((double) S.Y - (double) F.Y),
                OriginalImage.PixelFormat);
            BitmapData bitmapdata1 =
                OriginalImage.LockBits(new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height),
                    ImageLockMode.ReadWrite, OriginalImage.PixelFormat);
            byte* numPtr1 = (byte*) bitmapdata1.Scan0.ToPointer();
            BitmapData bitmapdata2 = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);
            byte* numPtr2 = (byte*) bitmapdata2.Scan0.ToPointer();
            for (int index1 = (int) F.Y; index1 < (int) S.Y; ++index1)
            {
                for (int index2 = (int) F.X; index2 < (int) S.X; ++index2)
                    (numPtr2 + (index1 - (int) F.Y)*bitmapdata2.Stride)[((index2 - (int) F.X))] =
                        (numPtr1 + index1*bitmapdata1.Stride)[index2];
            }
            OriginalImage.UnlockBits(bitmapdata1);
            bitmap.UnlockBits(bitmapdata2);
            return bitmap;
        }
        /*
        * If the pixel value is higher than the treshold, it is set to 255, otherwise it is set to 0.
        */
        public unsafe void Nullify(ref Bitmap OriginalImage, int treshold)
        {
            Bitmap bitmap1 = OriginalImage;
            int width = bitmap1.Width;
            int height = bitmap1.Height;
            BitmapData bitmapdata1 =
                OriginalImage.LockBits(new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height),
                    ImageLockMode.ReadWrite, OriginalImage.PixelFormat);
            byte* numPtr1 = (byte*) bitmapdata1.Scan0.ToPointer();
            for (int index1 = 0; index1 < width; ++index1)
            {
                for (int index2 = 0; index2 < height; ++index2)
                    if ((numPtr1 + index2*bitmapdata1.Stride)[index1] < treshold)
                        (numPtr1 + index2*bitmapdata1.Stride)[index1] = (byte) 0;
                    else
                        (numPtr1 + index2*bitmapdata1.Stride)[index1] = 255;
            }
            OriginalImage.UnlockBits(bitmapdata1);
            return;
        }
        /*
         * Returns Bitmap with 8bppIndexed pixel format. Picture that is RGB is turned to black and white picture.
         * Pixel is turned to white if the maximum of its RGB values is higher than the given treshold.
         */
        public unsafe Bitmap NullifyRGB( Bitmap OriginalImage, int treshold)
        {
            Bitmap res = new Bitmap(OriginalImage.Width,OriginalImage.Height, PixelFormat.Format8bppIndexed);
            int width = OriginalImage.Width;
            int height = OriginalImage.Height;
            BitmapData data =
                OriginalImage.LockBits(new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height),
                    ImageLockMode.ReadWrite, OriginalImage.PixelFormat);
            byte* Ptr = (byte*)data.Scan0.ToPointer();
            BitmapData dataO = res.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, res.PixelFormat);
            byte* PtrO = (byte*) dataO.Scan0.ToPointer();
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                    if (
                        Math.Max(Math.Max(*(Ptr + data.Stride*j + i*3), *(Ptr + data.Stride*j + i*3 + 1)),
                            *(Ptr + data.Stride*j + i*3 + 2)) > treshold)
                        *(PtrO + dataO.Stride*j + i) = 255;
                        }
            OriginalImage.UnlockBits(data);
            res.UnlockBits(dataO);
            return res;
        }
        /*
        * If the pixel value is higher than the treshold, it is set to 255, otherwise it is set to 0.
        * Returns a new bitmap.
        */
        public unsafe Bitmap NullifyValue(Bitmap OriginalImage, int treshold)
        {
            int width = OriginalImage.Width;
            int height = OriginalImage.Height;
            Bitmap OutPut = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            BitmapData data =
                OriginalImage.LockBits(new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height),
                    ImageLockMode.ReadWrite, OriginalImage.PixelFormat);
            byte* ptr = (byte*) data.Scan0.ToPointer();
            BitmapData datao =
                OutPut.LockBits(new Rectangle(0, 0, OutPut.Width, OutPut.Height),
                    ImageLockMode.ReadWrite, OutPut.PixelFormat);
            byte* ptro = (byte*) datao.Scan0.ToPointer();
            for (int i = 0; i < OriginalImage.Width - 1; ++i)
                for (int j = 0; j < OriginalImage.Height - 1; ++j)
                {
                    var r = *(ptr + j*data.Stride + 3*i);
                    var g = *(ptr + j*data.Stride + 3*i + 1);
                    var b = *(ptr + j*data.Stride + 3*i + 2);
                    if (Math.Max(Math.Max(r, g), b) > treshold)
                        *(ptro + j*datao.Stride + i) = 255;
                    else
                        *(ptro + j*datao.Stride + i) = 0;
                }
            OriginalImage.UnlockBits(data);
            OutPut.UnlockBits(datao);
            return OutPut;
        }
        /*
        * Requires the bitmap to be in 8bppIndexed and values either 0 or 255.
        * Returns coordinates of pixels that the object covers.
        * Tries diagonals as well.
        */
        public unsafe Queue<Point> FloodFillDiagonal(Bitmap OriginalImage, ref int[][] visited, int x, int y, int lookFor,
          BitmapData data)
        {
            byte* Ptr = (byte*)data.Scan0.ToPointer();
            Bitmap bitmap = OriginalImage;
            int width = bitmap.Width;
            int height = bitmap.Height;
            Queue<Point> Q = new Queue<Point>();
            Queue<Point> Track = new Queue<Point>();
            Point point = Point.Empty;
            point.X = x;
            point.Y = y;
            visited[point.X][point.Y] = 1;
            Q.Enqueue(point);
            Track.Enqueue(point);
            while ((uint)Q.Count > 0U)
            {
                int cx = (int)Q.Peek().X;
                int cy = (int)Q.Peek().Y;
                Q.Dequeue();
                if (cy - 1 >= 0 && visited[cx][cy - 1] == 0 && (*(Ptr + (cy - 1) * data.Stride + cx)) == lookFor)
                {
                    visited[cx][cy - 1] = 1;
                    Q.Enqueue(new Point(cx, (cy - 1)));
                    Track.Enqueue(new Point(cx, (cy - 1)));
                }
                if (cy + 1 < height && visited[cx][cy + 1] == 0 && *(Ptr + (cy + 1) * data.Stride + cx) == lookFor)
                {
                    visited[cx][cy + 1] = 1;
                    Q.Enqueue(new Point(cx, (cy + 1)));
                    Track.Enqueue(new Point(cx, (cy + 1)));
                }
                if (cx - 1 >= 0 && visited[cx - 1][cy] == 0 && *(Ptr + cy * data.Stride + cx - 1) == lookFor)
                {
                    visited[cx - 1][cy] = 1;
                    Q.Enqueue(new Point((cx - 1), cy));
                    Track.Enqueue(new Point((cx - 1), cy));
                }
                if (cx + 1 < width && visited[cx + 1][cy] == 0 && *(Ptr + cy * data.Stride + cx + 1) == lookFor)
                {
                    visited[cx + 1][cy] = 1;
                    Q.Enqueue(new Point((cx + 1), cy));
                    Track.Enqueue(new Point((cx + 1), cy));
                }
                if (cy - 1 >= 0 && cx-1>=0 && visited[cx-1][cy - 1] == 0 && (*(Ptr + (cy - 1) * data.Stride + cx-1)) == lookFor)
                {
                    visited[cx-1][cy - 1] = 1;
                    Q.Enqueue(new Point((cx-1), (cy - 1)));
                    Track.Enqueue(new Point((cx-1), (cy - 1)));
                }
                if (cy + 1 < height && cx-1>=0 && visited[cx-1][cy + 1] == 0 && *(Ptr + (cy + 1) * data.Stride + cx-1) == lookFor)
                {
                    visited[cx-1][cy + 1] = 1;
                    Q.Enqueue(new Point((cx-1), (cy + 1)));
                    Track.Enqueue(new Point((cx-1), (cy + 1)));
                }
                if (cx + 1 >= 0 && cy+1<height && visited[cx + 1][cy + 1] == 0 && *(Ptr + (cy + 1) * data.Stride + cx + 1) == lookFor)
                {
                    visited[cx + 1][cy + 1] = 1;
                    Q.Enqueue(new Point((cx - 1), (cy + 1)));
                    Track.Enqueue(new Point((cx - 1), (cy + 1)));
                }
                if (cx + 1 < width && cy - 1 >=0 && visited[cx + 1][cy-1] == 0 && *(Ptr + (cy - 1) * data.Stride + cx + 1) == lookFor)
                {
                    visited[cx + 1][cy-1] = 1;
                    Q.Enqueue(new Point((cx + 1), (cy-1)));
                    Track.Enqueue(new Point((cx + 1), (cy-1)));
                }
            }
            return Track;
        }
        /*
        * Does not try diagonals.
        */
        private unsafe Queue<Point> FloodFill(Bitmap OriginalImage, ref int[][] visited, int x, int y, int lookFor,
            BitmapData data)
        {
            byte* Ptr = (byte*) data.Scan0.ToPointer();
            Bitmap bitmap = OriginalImage;
            int width = bitmap.Width;
            int height = bitmap.Height;
            Queue<Point> Q = new Queue<Point>();
            Queue<Point> Track = new Queue<Point>();
            Point point = Point.Empty;
            point.X = x;
            point.Y = y;
            visited[point.X][point.Y] = 1;
            Q.Enqueue(point);
            Track.Enqueue(point);
            while ((uint) Q.Count > 0U)
            {
                int cx = (int) Q.Peek().X;
                int cy = (int) Q.Peek().Y;
                Q.Dequeue();
                if (cy - 1 >= 0 && visited[cx][cy - 1] == 0 && (*(Ptr + (cy - 1)*data.Stride + cx)) == lookFor)
                {
                    visited[cx][cy - 1] = 1;
                    Q.Enqueue(new Point(cx, (cy - 1)));
                    Track.Enqueue(new Point(cx, (cy - 1)));
                }
                if (cy + 1 < height && visited[cx][cy + 1] == 0 && *(Ptr + (cy + 1)*data.Stride + cx) == lookFor)
                {
                    visited[cx][cy + 1] = 1;
                    Q.Enqueue(new Point(cx, (cy + 1)));
                    Track.Enqueue(new Point(cx, (cy + 1)));
                }
                if (cx - 1 >= 0 && visited[cx - 1][cy] == 0 && *(Ptr + cy*data.Stride + cx - 1) == lookFor)
                {
                    visited[cx - 1][cy] = 1;
                    Q.Enqueue(new Point((cx - 1), cy));
                    Track.Enqueue(new Point((cx - 1), cy));
                }
                if (cx + 1 < width && visited[cx + 1][cy] == 0 && *(Ptr + cy*data.Stride + cx + 1) == lookFor)
                {
                    visited[cx + 1][cy] = 1;
                    Q.Enqueue(new Point((cx + 1), cy));
                    Track.Enqueue(new Point((cx + 1), cy));
                }
            }
            return Track;
        }

        /*
        * Returns the pixel that is the most distant from the center.
        * If the object is smaller than treshold, removes the object.
        */
        public unsafe double FloodFillD(ref Bitmap OriginalImage, ref int[][] visited, int x, int y, int treshold,
            BitmapData data)
        {
            if (OriginalImage.PixelFormat != PixelFormat.Format8bppIndexed)
                throw new System.ArgumentException("Wrong pixel format. Please convert to 8bppIndexed");
            byte* Ptr = (byte*)data.Scan0.ToPointer();
            int width = OriginalImage.Width;
            int height = OriginalImage.Height;
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            double num4 = 0.0;
            Queue<Point> Q = new Queue<Point>(FloodFill(OriginalImage, ref visited, x, y, 255, data));
            if (Q.Count < treshold)
            {
                while ((uint)Q.Count > 0U)
                {
                    int index = (int)Q.Peek().X;
                    int num5 = (int)Q.Peek().Y;
                    (Ptr + num5 * data.Stride)[index] = (byte)0; //0
                    Q.Dequeue();
                }
                return 0.0;
            }
            while ((uint)Q.Count > 0U)
            {
                int cx = (int)Q.Peek().X;
                int cy = (int)Q.Peek().Y;
                Q.Dequeue();
                num3 = Math.Abs(cy);
                num2 = Math.Abs(cx);
                double num5 =
                    Math.Sqrt((double)((num2 - width / 2) * (num2 - width / 2) + (num3 - height / 2) * (num3 - height / 2)));
                if (num5 > num4)
                    num4 = num5;
            }
            return num4;
        }
        /*
        * Removes the object if its size is less than the threshold.
        */
        public unsafe void FloodFillR(ref Bitmap OriginalImage, ref int[][] visited, int x, int y, int treshold,
            BitmapData data)
        {
            if (OriginalImage.PixelFormat != PixelFormat.Format8bppIndexed)
                throw new System.ArgumentException("Wrong pixel format. Please convert to greyscale");
            byte* Ptr = (byte*) data.Scan0.ToPointer();
            Queue<Point> Q = new Queue<Point>(FloodFill(OriginalImage, ref visited, x, y, 255, data));
            if (Q.Count < treshold)
            {
                while ((uint)Q.Count > 0U)
                {
                    int index = (int)Q.Peek().X;
                    int num5 = (int)Q.Peek().Y;
                    (Ptr + num5 * data.Stride)[index] = (byte)0; //0
                    Q.Dequeue();
                }
            }
        }
        /*
        * Makes a star, that is defined with the list of pixels it covers, as well as its width and center.
        */
        public unsafe Zvezda FloodFillL(Bitmap OriginalImage, ref int[][] visited, int x, int y, BitmapData data)
        {
            byte* Ptr = (byte*) data.Scan0.ToPointer();
            Queue<Point> Q = FloodFill(OriginalImage, ref visited, x, y, 255, data);
            int n = Q.Count;
            List<Point> L = new List<Point>();
            Point P = Point.Empty;
            int maxx = 0, minx = OriginalImage.Width;
            for (int i = 0; i < n; ++i)
            {
                Point Curr = Q.Peek();
                Q.Dequeue();
                if (Curr.X < minx)
                    minx = Curr.X;
                if (Curr.X > maxx)
                    maxx = Curr.X;
                P.X += Curr.X;
                P.Y += Curr.Y;
                L.Add(Curr);
            }
            return new Zvezda(L, maxx - minx, new Point((int) (P.X/L.Count), (int) (P.Y/L.Count)));
        }
        /*
        * Draws a line for every white object on the image.
        */
        public unsafe Tuple<double,double> L(List<Point> L)
        {
            int n = L.Count;
            double n1=0, k=0;
            Point brunt = Point.Empty;
            for (int i = 0; i < n; ++i)
            {
                brunt.X += L[i].X;
                brunt.Y += L[i].Y;
            }
            brunt.X = (int) (brunt.X/n);
            brunt.Y = (int) (brunt.Y/n);
            long divident = 0, numerator = 0;
            for (int l = 0; l < n; ++l)
            {
                divident += (L[l].X - brunt.X)*(L[l].X - brunt.X);
                numerator += (L[l].X - brunt.X)*(L[l].Y - brunt.Y);
            }
            if (divident == 0)
                k = 0;
            else
                k = (double) numerator/divident;
            n1 = brunt.Y - k*brunt.X;
            return new Tuple<double,double>(k, n1);
        }
        /*
        * Draws line on image Img, which has a function f(X) = k*x + m.
        */
        public unsafe void drawLine(ref Bitmap Img, Tuple<double,double> function, Point brunt, int length, BitmapData data)
        {
            double k = function.Item1;
            double m = function.Item2;
            byte* Ptr = (byte*) data.Scan0.ToPointer();
            if ((k < 1) && (k > -1))
            {
                for (int i = brunt.X; i < Math.Min(brunt.X+length, Img.Width); ++i)
                {
                    double y = k*i + m;
                    if (!OutOfBoundaries(5, (int) y, Img.Width, Img.Height))
                        *(Ptr + (int) y*data.Stride + i) = 255;
                }
            }
            else
                for (int j = brunt.Y; j < Math.Min(brunt.Y+length,Img.Height); ++j)
                {
                    double x = (j - m)/k;
                    if (!OutOfBoundaries((int) x, 3, Img.Width, Img.Height))
                        *(Ptr + j*data.Stride + (int) x) = 255;
                }
        }

        public bool OutOfBoundaries(int x, int y, int Width, int Height)
        {
            if ((x < 0) || (y < 0) || (x > Width) || (y > Height))
                return true;
            return false;
        }
        /*
        Draws a circle, from the point C and with radius of R, on bitmap OriginalImage
        */
        public unsafe void DrawCircle(ref Bitmap OriginalImage, Point C, double R)
        {
            double num1 = 0.0;

            BitmapData data = OriginalImage.LockBits(new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height),
                ImageLockMode.WriteOnly, OriginalImage.PixelFormat);
            byte* bytePtr = (byte*) data.Scan0.ToPointer();
            while (num1 < 360.0)
            {
                double num2 = num1*Math.PI/180.0;
                int x = (int) ((double) (C.X + R*Math.Cos(num2)));
                int y = (int) ((double) (C.Y + R*Math.Sin(num2)));
                if (x >= OriginalImage.Width)
                    --x;
                if (y >= OriginalImage.Height)
                    --y;
                *(bytePtr + y*data.Stride + x) = 255;
                num1 += 0.02;
            }
            OriginalImage.UnlockBits(data);
        }
        /*
        Overlaps two pictures and returns the result.
        */
        public unsafe void OverLapBoth(ref Bitmap First, Bitmap Second)
        {
            if (First.PixelFormat != PixelFormat.Format8bppIndexed ||
                Second.PixelFormat != PixelFormat.Format8bppIndexed)
                throw new ArgumentException("Pictures are in wrong format.");
            BitmapData dataf = First.LockBits(new Rectangle(0, 0, First.Width, First.Height), ImageLockMode.ReadWrite,
                First.PixelFormat);
            BitmapData datas = Second.LockBits(new Rectangle(0, 0, Second.Width, Second.Height), ImageLockMode.ReadOnly,
                Second.PixelFormat);
            byte* Ptrf = (byte*) dataf.Scan0.ToPointer();
            byte* Ptrs = (byte*) datas.Scan0.ToPointer();
            for (int index1 = 0; index1 < First.Height ; ++index1)
                for (int index2 = 0; index2 < First.Width; ++index2)
                    *(Ptrf + index1*dataf.Stride + index2) = Math.Max(*(Ptrf + index1*dataf.Stride + index2),
                        *(Ptrs + index1*datas.Stride + index2));
            First.UnlockBits(dataf);
            Second.UnlockBits(datas);
        }
        /*
        Overlaps two pictures, and the changes are saved n first.
        */
        public unsafe void OverLapBoth(Bitmap First, Bitmap Second, BitmapData bitmapdata1, BitmapData bitmapdata2)
        {
            if (First.Height != Second.Height || First.Width != Second.Width)
                throw new ArgumentException("Pictures are not the same size.");
            if (First.PixelFormat != PixelFormat.Format8bppIndexed ||
                Second.PixelFormat != PixelFormat.Format8bppIndexed)
                throw new ArgumentException("Pictures are in wrong format.");
            byte* numPtr1 = (byte*) bitmapdata1.Scan0.ToPointer();
            byte* numPtr2 = (byte*) bitmapdata2.Scan0.ToPointer();
            for (int index1 = 0; index1 < First.Height - 1; ++index1)
            {
                for (int index2 = 0; index2 < First.Width - 1; ++index2)
                {
                    if (*(numPtr1 + index1*bitmapdata1.Stride + index2) <
                        *(numPtr2 + index1*bitmapdata2.Stride + index2))
                        *(numPtr1 + index1*bitmapdata1.Stride + index2) =
                            *(numPtr2 + index1*bitmapdata2.Stride + index2);
                }
            }
        }
        /*
        Applies the created mask.
        Everything that is white on the mask and is connected to it on the image is removed.
        */
        public unsafe void ApplyMask(ref Bitmap Image, Bitmap Mask)
        {
            if ((Image.PixelFormat != PixelFormat.Format8bppIndexed) &&
                (Mask.PixelFormat != PixelFormat.Format8bppIndexed))
                throw new ArgumentException("Wrong image format. Convert to 8bppIndexed.");
            BitmapData data = Image.LockBits(new Rectangle(0, 0, Image.Width, Image.Height), ImageLockMode.ReadWrite,
                Image.PixelFormat);
            BitmapData dataM = Mask.LockBits(new Rectangle(0, 0, Mask.Width, Mask.Height), ImageLockMode.ReadWrite,
                Mask.PixelFormat);
            byte* Ptr = (byte*) data.Scan0.ToPointer();
            byte* PtrM = (byte*) dataM.Scan0.ToPointer();
            int[][] visited = new int[Image.Width][];
            for (int i = 0; i < Image.Height; ++i)
                visited[i] = new int[Image.Height];
            for (int x = 0; x < Image.Width; ++x)
                for (int y = 0; y < Image.Height; ++y)
                {
                    if ((*(PtrM + y*dataM.Stride + x) == 255) && (visited[x][y] == 0))
                    {
                        Queue<Point> Q = FloodFill(Image, ref visited, x, y, 255, data);
                        while (Q.Count != 0)
                        {
                            Point P = Q.Peek();
                            Q.Dequeue();
                            *(Ptr + P.Y*data.Stride + P.X) = 0;
                        }
                    }
                }
            Image.UnlockBits(data);
            Mask.UnlockBits(dataM);
        }
        /*
        Calculated the distance between two points
        */
        private double Distance(Point F, Point S)
        {
            return Math.Sqrt((double) ((F.X - S.X)*(F.X - S.X) + (F.Y - S.Y)*(F.Y - S.Y)));
        }
        /*
        Only takes into account objects that are bigger than the given size.
        Uses two methods.
        First one is to find all white objects and find avarage distance between furthest points of
        white objects from the center.
        The second one finds all black pixels that are next to white ones. This method is needed due to clouds.
        The second method relies on the fact that there is a strong possibility that the cloud is shaped into a circle, finding all the
        black pixels that are on its edge.
        Returns the smaller of the two values.
        */
        private unsafe double CalculateR(ref Bitmap img, BitmapData data, int size)
        {
            double R = 0, RT = 0;
            byte* Ptr = (byte*) data.Scan0.ToPointer();
            int[][] visited = new int[img.Width][];
            int objects = 0;
            for (int i = 0; i < img.Width; ++i)
                visited[i] = new int[img.Height];
            for (int i = 0; i < img.Width; ++i)
                for (int j = 0; j < img.Height; ++j)
                {
                    if ((*(Ptr + j*data.Stride + i) > 200) && (visited[i][j] == 0))
                    {
                        R = RT;
                        RT += FloodFillD(ref img, ref visited, i, j, size, data);
                        if (RT - R > 0.0000001)
                            ++objects;
                    }
                }
            R = RT/objects;
            visited = new int[img.Width][];
            for (int i = 0; i < img.Width; ++i)
                visited[i] = new int[img.Height];
            RT = 0;
            Queue<Point> Q = FloodFill(img, ref visited, img.Width/2, img.Height/2, 0, data);
            int n = Q.Count;
            for (int i = 0; i < n; ++i)
                RT += Distance(new Point(img.Width/2, img.Height/2), Q.Dequeue());
            R = Math.Min(R, RT);
            return R;
        }
        /*
        Finds treshold using histogram of the highest values of RGB.
        After that, treshold is equal to the point on histogram in which both areas, the left and the right side are equal.
        */
        public unsafe int Treshold(Bitmap img)
        {
            int[] hist = new int[256];
            int i;
            if (img.PixelFormat != PixelFormat.Format24bppRgb)
                throw new ArgumentException("Please convert the image to 24RGB");
            BitmapData data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly,
                img.PixelFormat);
            byte* ptr = (byte*) data.Scan0.ToPointer();
            for (i = 0; i < img.Width - 1; ++i)
                for (int j = 0; j < img.Height - 1; ++j)
                {
                    var r = *(ptr + j*data.Stride + 3*i);
                    var g = *(ptr + j*data.Stride + 3*i + 1);
                    var b = *(ptr + j*data.Stride + 3*i + 2);
                    ++hist[Math.Max(Math.Max(r, g), b)];
                }
            img.UnlockBits(data);
            double[] chist = new double[256], cmom = new double[256];
            double hist_max = 0.0;
            double chist_max = 0.0;
            double cmom_max = 0.0;
            double bvar_max = 0.0;
            int threshold = 127;

            hist_max = hist[0];
            chist[0] = hist[0];
            cmom[0] = 0;

            for (i = 1; i <= 255; i++)
            {
                if (hist[i] > hist_max)
                    hist_max = hist[i];

                chist[i] = chist[i - 1] + hist[i];
                cmom[i] = cmom[i - 1] + i*hist[i];
            }

            chist_max = chist[255];
            cmom_max = cmom[255];
            bvar_max = 0;

            for (i = 0; i < 255; ++i)
                if (chist[i] > 0 && chist[i] < chist_max)
                {
                    double bvar;

                    bvar = (double) cmom[i]/chist[i];
                    bvar -= (double) (cmom_max - cmom[i])/(chist_max - chist[i]);
                    bvar *= bvar;
                    bvar *= chist[i];
                    bvar *= chist_max - chist[i];

                    if (bvar > bvar_max)
                    {
                        bvar_max = bvar;
                        threshold = i;
                    }
                }
            return threshold;
        }
        /*
        Creates a mask.
        First, all the pixels that are belove tresholded value become 0, all abouve 255.
        Then, a circle is drawn. Everything outside the circle get value 255.
        After that, morphological operation dialation is applied.
        This is due to cloud, to remove as much of its pixels as possible.
        Following this, everything around the circle gets value 0.
        */
        public unsafe Bitmap CreateMask(Bitmap img, int treshold, int size, int numDil)
        {
            Bitmap mask = new Bitmap(img.Width,img.Height,PixelFormat.Format8bppIndexed);
            OverLapBoth(ref mask,img);
            BitmapData data = mask.LockBits(new Rectangle(0, 0, mask.Width, mask.Height), ImageLockMode.ReadWrite,
                mask.PixelFormat);
            byte* Ptr = (byte*) data.Scan0.ToPointer();
            double R = Math.Min(mask.Height/2, CalculateR(ref mask, data, size));
            mask.UnlockBits(data);
            DrawCircle(ref mask, new Point(mask.Width/2, mask.Height/2), (int) R);
            mask = Crop(mask, mask.Width/2 - mask.Height/2, 0, mask.Height,mask.Height);
            data = mask.LockBits(new Rectangle(0, 0, mask.Width, mask.Height), ImageLockMode.ReadWrite,
                mask.PixelFormat);
            Ptr = (byte*) data.Scan0.ToPointer();
            int[][] visited = new int[mask.Width][];
            for (int i = 0; i < mask.Width; ++i)
                visited[i] = new int[mask.Height];

            Queue<Point> Q = FloodFill(mask, ref visited, 0, 0, 0, data);
            Q.Clear();
            Q=FloodFill(mask, ref visited, mask.Width-1, 0, 0, data);
            Q.Clear();
            Q=FloodFill(mask, ref visited, mask.Width-1, mask.Height-1, 0, data);
            Q.Clear();
            Q=FloodFill(mask, ref visited, 0, mask.Height-1, 0, data);
            Q.Clear();

            for (int i = 0; i < mask.Width; ++i)
                for (int j = 0; j < mask.Height; ++j)
                    if (visited[i][j] == 1)
                        *(Ptr + j*data.Stride + i) = 255;
            mask.UnlockBits(data);
            OverLapBoth(ref mask,Dilatation(mask, numDil));
            data = mask.LockBits(new Rectangle(0, 0, mask.Width, mask.Height), ImageLockMode.ReadWrite, mask.PixelFormat);
            Ptr = (byte*) data.Scan0.ToPointer();
            visited = new int[mask.Width][];
            for (int i=0;i<mask.Width;++i)
                visited[i] = new int[mask.Height];
            Q = FloodFill(mask, ref visited, mask.Width/2, mask.Height/2, 0, data);
            Q.Clear();
            for (int i = 0; i < mask.Width; ++i)
                for (int j = 0; j < mask.Height; ++j)
                    if (visited[i][j] == 0)
                        *(Ptr + j*data.Stride + i) = 255;
            mask.UnlockBits(data);
            return mask;
        }
        /*
        * This function calculates the curve values between the control points
        * point2 and point3, taking the potentially existing neighbors point1 and point4 into
        * account.
        *
        * This function uses a cubic bezier curve for the individual segments and
        * calculates the necessary intermediate control points depending on the
        * neighbor curve control points.
        */
        public double[] bezijer(
            Point point1,
            Point point2,
            Point point3,
            Point point4)
        {
            int i;
            double x0, x3;
            double y0, y1 = 0, y2 = 0, y3;
            double dx, dy;
            double slope;

            /* the outer control points for the bezier curve. */
            x0 = point2.X;
            y0 = point2.Y;
            x3 = point3.X;
            y3 = point3.Y;

            /*
             * the x values of the inner control points are fixed at
             * x1 = 2/3*x0 + 1/3*x3   and  x2 = 1/3*x0 + 2/3*x3
             * this ensures that the x values increase linearily with the
             * parameter t and enables us to skip the calculation of the x
             * values altogehter - just calculate y(t) evenly spaced.
             */

            dx = x3 - x0;
            dy = y3 - y0;

            if (point1 == point2 && point3 != point4)
            {
                /* only the right neighbor is available. Make the tangent at the
                 * right endpoint parallel to the line between the left endpoint
                 * and the right neighbor. Then point the tangent at the left towards
                 * the control handle of the right tangent, to ensure that the curve
                 * does not have an inflection point.
                 */
                slope = (point4.Y - y0)/(point4.X - x0);

                y2 = y3 - slope*dx/3.0; 
                y1 = y0 + (y2 - y0)/2.0;
            }
            else if (point1 != point2 && point3 == point4)
            {
                /* see previous case */
                slope = (y3 - point1.Y)/(x3 - point1.X);

                y1 = y0 + slope*dx/3.0;
                y2 = y3 + (y1 - y3)/2.0;
            }

            /*
             * finally calculate the y(t) values for the given bezier values. We can
             * use homogenously distributed values for t, since x(t) increases linearily.
             */
            double[] samples = new double[256];
            for (i = 0; i <= (int) (dx*255); i++)
            {
                double y, t;
                int index;

                t = i/dx/(double) 255;
                y = y0*(1 - t)*(1 - t)*(1 - t) +
                    3*y1*(1 - t)*(1 - t)*t +
                    3*y2*(1 - t)*t*t +
                    y3*t*t*t;

                index = i + (int) (x0*(double) 255);

                if (index < 256)
                    samples[index] = Math.Max(Math.Min(y, 1.0), 0.0);
            }
            samples[255] = 255;
            return samples;
        }
        /*
         * Applies dilatation over white squares n times.
         * Whenever a white pixel is fount, an area of 4*n^2 is set to white.
         * This way, instead of calling dilatation n times, passing through image n times,
         * the image will be passed through only once.
         */
        public unsafe Bitmap Dilatation(Bitmap img, int n)
        {
            Bitmap result = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);
            Graphics gr = Graphics.FromImage(result);
            Bitmap square = new Bitmap(n,n,PixelFormat.Format24bppRgb);
            BitmapData dataS = square.LockBits(new Rectangle(0, 0, square.Width, square.Height), ImageLockMode.ReadWrite,
                square.PixelFormat);
            byte* PtrS = (byte*) dataS.Scan0.ToPointer();
            for (int i=0;i<n;++i)
                for (int j = 0; j < n; ++j)
                {
                    *(PtrS + j*dataS.Stride + i*3) = 255;
                    *(PtrS + j * dataS.Stride + i * 3 + 1) = 255;
                    *(PtrS + j * dataS.Stride + i * 3 + 2) = 255;
                }
            square.UnlockBits(dataS);
            BitmapData data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite,
                img.PixelFormat);
            byte* Ptr = (byte*) data.Scan0.ToPointer();
            for (int i=0;i<img.Width;++i)
                for (int j = 0; j < img.Height; ++j)
                {
                    if (*(Ptr + j*data.Stride + i) > 75)
                        if (((j - 1 >= 0) &&
                             ((*(Ptr + (j - 1)*data.Stride + i) < 75) ||
                              ((i - 1 >= 0) && (*(Ptr + (j - 1)*data.Stride + i - 1) < 75)) ||
                              ((i + 1 < img.Width) && (*(Ptr + (j - 1)*data.Stride + i + 1) < 75))))
                            ||
                            ((j + 1 < img.Height) &&
                             ((*(Ptr + (j + 1)*data.Stride + i) < 75) ||
                              ((i - 1 >= 0) && (*(Ptr + (j + 1)*data.Stride + i - 1) < 75)) ||
                              ((i + 1 < img.Width) && (*(Ptr + (j + 1)*data.Stride + i + 1) < 75))))
                            || ((i + 1 < img.Width) && (*(Ptr + j*data.Stride + i + 1) < 75))
                            || ((i - 1 >= 0) && (*(Ptr + j*data.Stride + i - 1) < 75)))
                            gr.DrawImage(square, new Rectangle(i, j, n, n));
                }
            img.UnlockBits(data);
            return ToGreyscale(result);
        }
    }
}