using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using System.Windows.Media.Media3D;

namespace iq_project
{
    /// <summary>
    /// Interaction logic for ThreeDBoard.xaml
    /// </summary>
    public partial class ThreeDBoard : Window//בניה של צורה תלת מימדית גדולה
    {

        Viewport3D myViewport = new Viewport3D();

        private void WindowLoaded(object sender, EventArgs e)
        {
            DrawSomeModels();
        }



        private void DrawSomeModels()
        {
            myViewport.Name = "myViewport";
            ModelVisual3D myModelVisual = new ModelVisual3D();
            myViewport.Children.Add(myModelVisual);
            myViewport.Focus();
        }

        XDocument doc;
        int partNumber;//מספר צורה
        int directionNumber;//מספר כיוון
        public int drNumToCardBoard;
        ThreeDGumotGadol[,] Boal = new ThreeDGumotGadol[4, 4];//יצירת מטריצה 4 על 4
        int simun = 0;

        public ThreeDBoard()
        {
            doc = XDocument.Load(Environment.CurrentDirectory + "\\..\\..\\Parts.xml");//שליפה של הצורה מתוך דף ה-xml
            InitializeComponent();
        }

        public void KeyDownHandlerForCamera(object sender, KeyEventArgs e)//כאשר לוחצים על המקשים ->- <-
        {
            switch (e.Key)
            {

                case Key.Left:
                    NewnewMatLeft(partNumber, directionNumber);
                    break;
                case Key.Right:
                    NewnewMatRight(directionNumber, partNumber);
                    break;
                case Key.Return://כאשר לוחצים על אנטר הדף נסגר
                    NewMat(partNumber, directionNumber);
                    this.Close();
                    break;
            }

        }

        public void NewnewMatLeft(int num, int dr)//בדיקה שזה לא הכיוון האחרון- מספר 8 
        {
            if (dr != 8)//ואם זה לא, מעבר לכיוון הבא
            {
                NewMat(num, dr + 1);
                drNumToCardBoard = dr + 1;
            }
            else
            {
                NewMat(num, 1);
                drNumToCardBoard = 1;
            }
        }

        public void NewnewMatRight(int dr, int num)//בדיקה שזה לא הכיווון הראשון- מספר 1
        {
            if (dr != 1)//ואם זה לא, מעבר לכיוון הקודם
            {
                NewMat(num, dr - 1);
                drNumToCardBoard = dr - 1;
            }
            else
            {
                NewMat(num, 8);
                drNumToCardBoard = 8;
            }
        }


        public ThreeDBoard NewMat(int prNum, int drNum)//בניית המטריצה
        {
            partNumber = prNum;
            directionNumber = drNum;
            simun++;//בדיקה שאין כלום בלוח
            if (simun != 0)
                gridush.Children.Clear();
            ThreeDGumotGadol[,] Boal = new ThreeDGumotGadol[4, 4];//יצירת מטריצה 4 על 4

            foreach (var item in doc.Descendants("part"))//מציאת החלק המתאים
            {
                int num = int.Parse(item.Attribute("number").Value);
                if (num == prNum)
                {
                    string col = item.Attribute("color").Value.ToString();//שמירת צבע הצורה
                    foreach (var item1 in item.Descendants("direction"))//מציאת הכיוון המתאים
                    {

                        int str = int.Parse(item1.Attribute("directionNum").Value);
                        if (str == drNum)//כשמצאנו את הכיוון המתאים
                        {
                            long dr = long.Parse(item1.Attribute("dr").Value);
                            long ez_dr = dr;

                            //בדיקה האם זו צורה מסדר 3 על 3 או 4 על 4
                            int len = (int)Math.Log10(ez_dr);
                            int ezi_j = (int)Math.Sqrt(len);

                            long slicer;

                            if (ezi_j == 3)  //אם זה מסדר 3 על 3
                            {
                                //עדכון העזרים בהתאם
                                slicer = 100000000;
                                dr = dr % 1000000000;
                            }
                            else  //אם זה מסדר 4 על 4
                            {
                                //עדכון העזרים בהתאם
                                slicer = 1000000000000000;
                                dr = dr % 10000000000000000;
                            }
                            for (int i = 0; i < ezi_j; i++)
                            {
                                for (int j = 0; j < ezi_j; j++)
                                {
                                    long y = dr / slicer;

                                    if (y == 1)//אם לפי ה-xml יש אחד מציבים בלוח
                                    {
                                        Boal[i, j] = new ThreeDGumotGadol();//יצירת גומה

                                        switch (col)//צביעה בצבע המתאים
                                        {
                                            case "dark green":
                                                Boal[i, j].cadur.Color = Brushes.Green.Color;
                                                break;
                                            case "purple":
                                                Boal[i, j].cadur.Color = Brushes.Purple.Color;
                                                break;
                                            case "light azure":
                                                Boal[i, j].cadur.Color = Brushes.Azure.Color;
                                                break;
                                            case "light green":
                                                Boal[i, j].cadur.Color = Brushes.LightGreen.Color;
                                                break;
                                            case "azure":
                                                Boal[i, j].cadur.Color = Brushes.Blue.Color;
                                                break;
                                            case "blue":
                                                Boal[i, j].cadur.Color = Brushes.DarkBlue.Color;
                                                break;
                                            case "yellow":
                                                Boal[i, j].cadur.Color = Brushes.Yellow.Color;
                                                break;
                                            case "pink":
                                                Boal[i, j].cadur.Color = Brushes.Pink.Color;
                                                break;
                                            case "orange":
                                                Boal[i, j].cadur.Color = Brushes.Orange.Color;
                                                break;
                                            case "red":
                                                Boal[i, j].cadur.Color = Brushes.Red.Color;
                                                break;
                                        }
                                        Grid.SetRow(Boal[i, j], i);
                                        Grid.SetColumn(Boal[i, j], j);
                                        gridush.Children.Add(Boal[i, j]);
                                    }
                                    dr = dr % slicer;
                                    if (slicer / 10 != 0)
                                        slicer = slicer / 10;
                                }
                            }
                        }
                    }
                }
            }
            return this;//החזר הצורה הבנויה
        }
    }
}

;