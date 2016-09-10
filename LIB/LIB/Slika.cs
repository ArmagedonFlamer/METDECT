using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB
{
    /*
    * This class is used for storing every white object that is left after initial processing.
    */
        public class Slika
        {
            public static List<Zvezda> Zvezde = new List<Zvezda>();
            public Slika(List<Zvezda> L)
            {
               Zvezde = L;
            }
            //Adds a Star(Zvezda)
            public static void AddZvezda(Zvezda Zvezda)
            {
                Zvezde.Add(Zvezda);
            }
            /*
            * This function takes every white object and checks if there is an object next to it and has similar orientation as it.
            * If so, it leaves the object otherwise it deletes it.
            */
            public unsafe static void Check(List<Zvezda> Z, Bitmap Img, BitmapData data)
            {
                byte* Ptr = (byte*) data.Scan0.ToPointer();
                bool objekat = false;
                var LIB = new LIB();
                int[][] visited = new int[Img.Width][];
                for (int i = 0; i < Img.Width; ++i)
                    visited[i] = new int[Img.Height];
                for (int i=0;i<Z.Count;++i)
                {
                    objekat = false;
                    for (int j = 0; j < Z[i].Coords.Count; ++j)
                        visited[Z[i].Coords[j].X][Z[i].Coords[j].Y] = 1;
                    Tuple<double, double> function = LIB.L(Z[i].Coords);
                    double k = function.Item1, m = function.Item2;
                    if ((k < 1) && (k > -1))
                    {
                        for (int j = Z[i].brunt.X; j < Math.Min(Z[i].brunt.X + Z[i].length, Img.Width); ++j)
                        {
                            double y = k * j + m;
                            if ((!LIB.OutOfBoundaries(5, (int)y, Img.Width, Img.Height)) && (visited[j][(int)y]==0) && (*(Ptr + (int)y * data.Stride + j) == 255))
                                objekat = true;
                        }
                    }
                    else
                        for (int j = Z[i].brunt.Y; j < Math.Min(Z[i].brunt.Y + Z[i].length, Img.Height); ++j)
                        {
                            double x = (j - m) / k;
                            if ((!LIB.OutOfBoundaries((int)x, 3, Img.Width, Img.Height))&&(visited[(int)x][j]==0)&&(*(Ptr + j * data.Stride + (int)x) == 255))
                                objekat = true;
                        }
                    if(!objekat)
                    {
                        for (int j = 0; j < Zvezde[i].Coords.Count; ++j)
                            *(Ptr + Zvezde[i].Coords[j].Y*data.Stride + Zvezde[i].Coords[j].X) = 0;
                        Zvezde[i].Delete();
                        --i;
                    }
                }
            }
            /*
            * Draws the remaining white objects on a new bitmap.
            */
            public unsafe static Bitmap DrawImage(List<Zvezda>Zvezde, Size S)
            {
                Bitmap btm = new Bitmap(S.Width,S.Height, PixelFormat.Format8bppIndexed);
                AForge.Imaging.Image.SetGrayscalePalette(btm);
                var LIB = new LIB();       
                BitmapData data = btm.LockBits(new Rectangle(0, 0, btm.Width, btm.Height), ImageLockMode.ReadWrite,
                    btm.PixelFormat);
                byte* Ptr = (byte*) data.Scan0.ToPointer();
                for (int i = 0; i < Zvezde.Count; ++i)
                {
                   // LIB.drawLine(ref btm, LIB.L(Zvezde[i].Coords),Zvezde[i].brunt, Zvezde[i].length, data);
                    for (int j = 0; j < Zvezde[i].Coords.Count; ++j)
                    {
                        *(Ptr + Zvezde[i].Coords[j].Y * data.Stride + Zvezde[i].Coords[j].X) = 255;
                    }
                }
                btm.UnlockBits(data);
               // Zvezde = new List<Zvezda>();
                return btm;
            }
        }
    /*
    * Zveda = Star
    * Is defined by its length, pixels it covers and brunt.
    */
    public class Zvezda
    {
        public List<Point> Coords;
        public int length;
        public Point brunt;
        public Zvezda()
        {
            Coords = new List<Point>();
            length = 0;
        }

        public Zvezda(List<Point> L, int c, Point p)
        {
            Coords = new List<Point>(L);
            length = c;
            brunt = p;
        }

        public void Delete()
        {
            Slika.Zvezde.Remove(this);
        }
    }
}
