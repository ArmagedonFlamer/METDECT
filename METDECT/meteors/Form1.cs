using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging.Filters;
using LIB;
using System.Security;

namespace meteors
{
    public unsafe partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private LIB.LIB lib = new LIB.LIB();

        private Bitmap first, second, third, fourth, of, os, ot, maskf, masks, maskt, maskc;

        private BitmapData dataF, dataS, dataT, dataC, dataM;

        private void minObjS_Leave(object sender, EventArgs e)
        {
            if (minObjS.Text == "200")
                minObjS.ForeColor = Color.SlateGray;
        }

        private void minObjS_Enter(object sender, EventArgs e)
        {
            if (minObjS.Text == "200")
                minObjS.ForeColor = Color.Black;
        }

        private void minStarS_Leave(object sender, EventArgs e)
        {
            if (minStarS.Text == "20")
                minStarS.ForeColor = Color.SlateGray;
        }

        private void minStarS_Enter(object sender, EventArgs e)
        {
            minStarS.ForeColor = Color.Black;
        }

        private void numDil_Leave(object sender, EventArgs e)
        {
            if (numDil.Text == "50")
                numDil.ForeColor = Color.SlateGray;
        }

        private void numDil_Enter(object sender, EventArgs e)
        {
            numDil.ForeColor = Color.Black;
        }

        private byte* PtrF, PtrS, PtrT, PtrC, PtrM;

        private int treshold, i, j;

        private int[][] visited;

        private string Open, Save;

        private int objectS, starS, dilNum;

        private DirectoryInfo dir;

        DateTime T;

        private void SaveF_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog sfd = new FolderBrowserDialog();
            sfd.ShowDialog();
            Save = sfd.SelectedPath;
        }

        TimeSpan TS = new TimeSpan();

        private void OpenF_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog ofd = new FolderBrowserDialog();
            ofd.ShowDialog();
            Open = ofd.SelectedPath;
        }

        private unsafe void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {           
            double[] bezijer = lib.bezijer(new Point(0, 0), new Point(0, 0), new Point(100, 255), new Point(255, 255));
           
            foreach (FileInfo flInfo in dir.GetFiles())
            {
                String Addr = flInfo.FullName;
                if (first == null)
                {
                    first = new Bitmap(Addr);
                    int[] histogram = new int[256];
                    dataF = first.LockBits(new Rectangle(0, 0, first.Width, first.Height), ImageLockMode.ReadWrite,
                        first.PixelFormat);
                    PtrF = (byte*)dataF.Scan0.ToPointer();
                    for (i = 0; i < first.Width; ++i)
                        for (j = 0; j < first.Height; ++j)
                        {
                            ++histogram[Math.Max(*(PtrF + j * dataF.Stride + i * 3), Math.Max(*(PtrF + j * dataF.Stride + i * 3 + 1), *(PtrF + j * dataF.Stride + i * 3 + 2)))];
                            *(PtrF + j * dataF.Stride + i * 3) = (byte)((int)255 * bezijer[*(PtrF + j * dataF.Stride + i * 3)]);
                        }
                    first.UnlockBits(dataF);
                    treshold = lib.Treshold(first);
                    first = lib.Crop(first, first.Width / 2 - first.Height / 2, 0, first.Height, first.Height);
                    first = lib.NullifyRGB(first, treshold);
                    maskf = lib.CreateMask(first, treshold, starS, dilNum);
                    dataF = first.LockBits(new Rectangle(0, 0, first.Width, first.Height), ImageLockMode.ReadWrite,
                        first.PixelFormat);
                    PtrF = (byte*)dataF.Scan0.ToPointer();
                    visited = new int[first.Width][];
                    for (int i = 0; i < first.Width; ++i)
                        visited[i] = new int[first.Height];
                    for (int i = 0; i < first.Height; ++i)
                        for (int j = 0; j < first.Width; ++j)
                            if ((*(PtrF + i * dataF.Stride + j) == 255) && (visited[j][i] == 0))
                                lib.FloodFillR(ref first, ref visited, j, i, starS, dataF);
                    first.UnlockBits(dataF);
                    continue;
                }
                if (second == null)
                {
                    second = new Bitmap(Addr);
                    dataS = second.LockBits(new Rectangle(0, 0, second.Width, second.Height), ImageLockMode.ReadWrite,
                        second.PixelFormat);
                    PtrS = (byte*)dataS.Scan0.ToPointer();
                    for (int i = 0; i < second.Width; ++i)
                        for (int j = 0; j < second.Height; ++j)
                            *(PtrS + j * dataS.Stride + i * 3) = (byte)((int)255 * bezijer[*(PtrS + j * dataS.Stride + i * 3)]);
                    second.UnlockBits(dataS);
                    treshold = lib.Treshold(second);
                    second = lib.Crop(second, second.Width / 2 - second.Height / 2, 0, second.Height, second.Height);
                    second = lib.NullifyRGB(second, treshold);
                    masks = lib.CreateMask(second, treshold, starS, dilNum);
                    dataS = second.LockBits(new Rectangle(0, 0, second.Width, second.Height), ImageLockMode.ReadWrite,
                        second.PixelFormat);
                    PtrS = (byte*)dataS.Scan0.ToPointer();
                    visited = new int[second.Width][];
                    for (int i = 0; i < second.Width; ++i)
                        visited[i] = new int[second.Height];
                    for (int i = 0; i < second.Width; ++i)
                        for (int j = 0; j < second.Height; ++j)
                            if ((*(PtrS + j * dataS.Stride + i) == 255) && (visited[i][j] == 0))
                                lib.FloodFillR(ref second, ref visited, i, j, starS, dataS);
                    second.UnlockBits(dataS);
                    continue;
                }
                if (third == null)
                {
                    third = new Bitmap(Addr);
                    dataT = third.LockBits(new Rectangle(0, 0, third.Width, third.Height), ImageLockMode.ReadWrite,
                        third.PixelFormat);
                    PtrT = (byte*)dataT.Scan0.ToPointer();
                    for (int i = 0; i < third.Width; ++i)
                        for (int j = 0; j < third.Height; ++j)
                            *(PtrT + j * dataT.Stride + i * 3) = (byte)((int)255 * bezijer[*(PtrT + j * dataT.Stride + i * 3)]);
                    third.UnlockBits(dataT);
                    treshold = lib.Treshold(third);
                    third = lib.Crop(third, third.Width / 2 - third.Height / 2, 0, third.Height, third.Height);
                    third = lib.NullifyRGB(third, treshold);
                    maskt = lib.CreateMask(third, treshold, starS, dilNum);
                    dataT = third.LockBits(new Rectangle(0, 0, third.Width, third.Height), ImageLockMode.ReadWrite,
                        third.PixelFormat);
                    PtrT = (byte*)dataT.Scan0.ToPointer();
                    visited = new int[third.Width][];
                    for (int i = 0; i < third.Width; ++i)
                        visited[i] = new int[third.Height];
                    for (int i = 0; i < third.Width; ++i)
                        for (int j = 0; j < third.Height; ++j)
                            if ((*(PtrT + j * dataT.Stride + i) == 255) && (visited[i][j] == 0))
                                lib.FloodFillR(ref third, ref visited, i, j, starS, dataT);
                    third.UnlockBits(dataT);

                    lib.OverLapBoth(ref first, second);
                    lib.OverLapBoth(ref maskf, masks);
                    lib.OverLapBoth(ref first, maskf);
                    lib.ApplyMask(ref first, maskf);

                    of = new Bitmap(first);
                    of = lib.ToGreyscale(of);
                    first = new Bitmap(second);
                    first = lib.ToGreyscale(first);

                    lib.OverLapBoth(ref second, third);
                    lib.OverLapBoth(ref masks, maskt);
                    lib.OverLapBoth(ref second, masks);
                    lib.ApplyMask(ref second, masks);

                    os = new Bitmap(second);
                    os = lib.ToGreyscale(os);
                    second = new Bitmap(third);
                    second = lib.ToGreyscale(second);
                    continue;
                }
                if (fourth == null)
                {
                    fourth = new Bitmap(Addr);
                    dataC = fourth.LockBits(new Rectangle(0, 0, fourth.Width, fourth.Height), ImageLockMode.ReadWrite,
                        fourth.PixelFormat);
                    PtrC = (byte*)dataC.Scan0.ToPointer();
                    for (int i = 0; i < third.Width; ++i)
                        for (int j = 0; j < third.Height; ++j)
                        {
                            *(PtrC + j * dataC.Stride + i * 3) = (byte)((int)255 * bezijer[*(PtrC + j * dataC.Stride + i * 3)]);
                        }
                    fourth.UnlockBits(dataT);
                    treshold = lib.Treshold(fourth);
                    fourth = lib.Crop(fourth, fourth.Width / 2 - fourth.Height / 2, 0, fourth.Height, fourth.Height);
                    fourth = lib.NullifyRGB(fourth, treshold);
                    maskc = lib.CreateMask(fourth, treshold, starS, dilNum);
                    dataC = fourth.LockBits(new Rectangle(0, 0, fourth.Width, fourth.Height), ImageLockMode.ReadWrite,
                        fourth.PixelFormat);
                    PtrC = (byte*)dataC.Scan0.ToPointer();
                    visited = new int[fourth.Width][];
                    for (int i = 0; i < fourth.Width; ++i)
                        visited[i] = new int[fourth.Height];
                    for (int i = 0; i < fourth.Width; ++i)
                        for (int j = 0; j < fourth.Height; ++j)
                            if ((*(PtrC + j * dataC.Stride + i) == 255) && (visited[i][j] == 0))
                                lib.FloodFillR(ref fourth, ref visited, i, j, starS, dataC);
                    fourth.UnlockBits(dataC);

                    lib.OverLapBoth(ref third, fourth);
                    lib.OverLapBoth(ref maskt, maskc);
                    lib.OverLapBoth(ref third, maskt);
                    lib.ApplyMask(ref third, maskt);

                    ot = new Bitmap(third);
                    ot = lib.ToGreyscale(ot);
                    third = new Bitmap(fourth);
                    third = lib.ToGreyscale(third);

                    fourth = null;

                    GC.Collect();

                    //Inicijalizovanje pozicija pocetnih zvezda na obradjenoj slici
                    dataF = of.LockBits(new Rectangle(0, 0, of.Width, of.Height), ImageLockMode.ReadWrite,
                            of.PixelFormat);
                    PtrF = (byte*)dataF.Scan0.ToPointer();
                    visited = new int[of.Width][];
                    for (int i = 0; i < of.Width; ++i)
                        visited[i] = new int[of.Height];
                    for (int i = 0; i < of.Width; ++i)
                        for (int j = 0; j < of.Height; ++j)
                        {
                            if ((*(PtrF + j * dataF.Stride + i) > 200) && (visited[i][j] == 0))
                            {
                                Zvezda Z = lib.FloodFillL(of, ref visited, i, j, dataF);
                                Z.length *= 2;
                                Slika.AddZvezda(Z);
                            }
                        }
                    of.UnlockBits(dataF);
                    //poredjenje sa druge dve slike
                    int n = Slika.Zvezde.Count;
                    dataF = of.LockBits(new Rectangle(0, 0, of.Width, of.Height), ImageLockMode.ReadWrite,
                        of.PixelFormat);
                    dataS = os.LockBits(new Rectangle(0, 0, os.Width, os.Height), ImageLockMode.ReadWrite,
                        os.PixelFormat);
                    dataT = ot.LockBits(new Rectangle(0, 0, ot.Width, ot.Height), ImageLockMode.ReadWrite,
                        ot.PixelFormat);
                    PtrF = (byte*)dataF.Scan0.ToPointer();
                    PtrS = (byte*)dataS.Scan0.ToPointer();
                    PtrT = (byte*)dataT.Scan0.ToPointer();
                    for (int i = 0; i < Slika.Zvezde.Count; ++i)
                    {
                        int tmp = i;
                        for (int j = 0; j < Slika.Zvezde[i].Coords.Count; ++j)
                        {
                            Point P = Slika.Zvezde[i].Coords[j];
                            if ((*(PtrS + P.Y * dataS.Stride + P.X) < 200) || (*(PtrT + P.Y * dataT.Stride + P.X) > 200))
                            {
                                Slika.Zvezde[i].Delete();
                                --i;
                                break;
                            }
                        }
                        if (Slika.Zvezde.Count == 0)
                            break;
                    }

                    of.UnlockBits(dataF);
                    os.UnlockBits(dataS);
                    ot.UnlockBits(dataT);

                    if (Slika.Zvezde.Count != 0)
                    {
                        Bitmap bmp = Slika.DrawImage(Slika.Zvezde, of.Size);
                        BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite,
                            bmp.PixelFormat);
                        Slika.Check(Slika.Zvezde, bmp, data);
                        bmp.UnlockBits(data);
                        bmp = Slika.DrawImage(Slika.Zvezde, of.Size);
                        if (Slika.Zvezde.Count!=0)
                            bmp.Save(Save + "\\" + flInfo.Name);
                    }                 

                    //prebacujem sliku koja se analizira na sledecu i na njoj nalazim zvezde

                    of = os;
                    of = lib.ToGreyscale(of);
                    dataF = of.LockBits(new Rectangle(0, 0, of.Width, of.Height), ImageLockMode.ReadWrite,
                    of.PixelFormat);
                    byte* Ptr = (byte*)dataF.Scan0.ToPointer();
                    visited = new int[of.Width][];
                    for (int i = 0; i < of.Width; ++i)
                        visited[i] = new int[of.Height];
                    for (int i = 0; i < of.Width; ++i)
                        for (int j = 0; j < of.Height; ++j)
                        {
                            if ((*(Ptr + j * dataF.Stride + i) > 200) && (visited[i][j] == 0))
                            {
                                Zvezda Z = lib.FloodFillL(of, ref visited, i, j, dataF);
                                Z.length *= 2;
                                Slika.AddZvezda(Z);
                            }
                        }
                    of.UnlockBits(dataF);
                    os = ot;
                    ot = null;
                    fourth = null;
                    maskt = maskc;
                    maskc = null;
                    Console.WriteLine(flInfo.Name);
                }
                GC.Collect();
            }
            DateTime T1 = DateTime.Now;
            TS = T1 - T;
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label1.Text = TS.ToString();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            T = DateTime.Now;
            try
            {
                objectS = Convert.ToInt32(minObjS.Text);
                starS = Convert.ToInt32(minStarS.Text);
                dilNum = Convert.ToInt32(numDil.Text);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Please configure variable so that all are represented as Int32.");
                return;
            }
            catch (FormatException)
            {
                MessageBox.Show("Please puto nly number in textboxes.");
                return;
            }
            try
            {
                dir = new DirectoryInfo(Open);
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Please select an valid adress.");
                return;
            }
            catch (SecurityException)
            {
                MessageBox.Show("You do not have access to that folder. Please select the one that you have access to.");
                return;
            }
            catch (PathTooLongException)
            {
                MessageBox.Show("Path is too long.");
                return;
            }
            try
            {
                FileInfo f = new FileInfo(Save);
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Please select an valid adress.");
                return;
            }
            catch (SecurityException)
            {
                MessageBox.Show("You do not have access to that folder. Please select the one that you have access to.");
                return;
            }
            catch (PathTooLongException)
            {
                MessageBox.Show("Path is too long.");
                return;
            }
            backgroundWorker1.RunWorkerAsync();
            
        }
    }
}
