
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace iq_project
{
    /// <summary>
    /// Interaction logic for ShearTzurot.xaml
    /// </summary>

    public partial class ShearTzurot : UserControl
    {
        XDocument doc;
        int partNumber = 1;//מספר צורה
        int directionNumber = 1;//מספר כיוון
        int simun = 0;

        Gumot[,] Boal = new Gumot[4, 4];//יצירת מטריצה 4 על 4

        UIElement OriginalElement = new UIElement();

        public Int64 TagDr;
        public int ezer;
        public string col;

        public ShearTzurot()
        {
            doc = XDocument.Load(Environment.CurrentDirectory + "\\..\\..\\Parts.xml");//שליפה של הצורה מתוך דף ה-xml
            InitializeComponent();
            newMat(partNumber, directionNumber,false);//שליחה לפונקציה עם מספר הצורה ומספר הכיוון

        }

        public void newMatForHint()
        {
            int part = 0, dir = 0;           
            int partdir = Convert.ToInt32(Tag);
            part = partdir / 10;
            dir = partdir % 10;
        }
        public ShearTzurot newMat(int prNum, int drNum, bool forHint)//בניית המטריצה
        {
            if (!forHint)
            {
                ClsGlobal.shapesDr[prNum - 1] = drNum;
                Tag = prNum + "" + drNum;
            }
            simun++;//בדיקה שאין כלום בלוח
            if (simun != 0)
            {
                gridush.Children.Clear();
            }

            foreach (XElement item in doc.Descendants("part"))//מציאת החלק המתאים
            {
                int num = int.Parse(item.Attribute("number").Value);
                if (num == prNum)
                {
                    col = item.Attribute("color").Value.ToString();//שמירת צבע הצורה
                    foreach (XElement item1 in item.Descendants("direction"))//מציאת הכיוון המתאים
                    {

                        int str = int.Parse(item1.Attribute("directionNum").Value);
                        if (str == drNum)//כשמצאנו את הכיוון המתאים
                        {
                            long dr = long.Parse(item1.Attribute("dr").Value);
                            long ez_dr = dr;
                            TagDr = dr;

                            int len = (int)Math.Log10(ez_dr);
                            int ezi_j = (int)Math.Sqrt(len);


                            long slicer;
                            if (ezi_j == 3)
                            {
                                slicer = 100000000;
                                dr = dr % 1000000000;
                            }
                            else
                            {
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

                                        Boal[i, j] = new Gumot
                                        {
                                            Width = 55,
                                            Height = 50,
                                        };//יצירת גומה                                      
                                        Boal[i, j].elipse.StrokeThickness = 3;


                                        switch (col)//צביעה בצבע המתאים
                                        {
                                            case "dark green":
                                                Boal[i, j].elipse.Fill = Brushes.Green;
                                                break;
                                            case "purple":
                                                Boal[i, j].elipse.Fill = Brushes.Purple;
                                                break;
                                            case "light azure":
                                                Boal[i, j].elipse.Fill = Brushes.SkyBlue;
                                                break;
                                            case "light green":
                                                Boal[i, j].elipse.Fill = Brushes.LightGreen;
                                                break;
                                            case "azure":
                                                Boal[i, j].elipse.Fill = Brushes.Blue;
                                                break;
                                            case "blue":
                                                Boal[i, j].elipse.Fill = Brushes.DarkBlue;
                                                break;
                                            case "yellow":
                                                Boal[i, j].elipse.Fill = Brushes.Yellow;
                                                break;
                                            case "pink":
                                                Boal[i, j].elipse.Fill = Brushes.Pink;
                                                break;
                                            case "orange":
                                                Boal[i, j].elipse.Fill = Brushes.Orange;
                                                break;
                                            case "red":
                                                Boal[i, j].elipse.Fill = Brushes.Red;
                                                break;
                                        }


                                        Grid.SetRow(Boal[i, j], i);
                                        Grid.SetColumn(Boal[i, j], j);
                                        gridush.Children.Add(Boal[i, j]);
                                    }

                                    dr = dr % slicer;
                                    if (slicer / 10 != 0)
                                    {
                                        slicer = slicer / 10;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return this;
        }

        private void Tsura_MouseDoubleClick(object sender, MouseButtonEventArgs e)//לחיצה על הצורה
        {
            int part = 0, dir = 0;
            ShearTzurot st = sender as ShearTzurot;
            int partdir = Convert.ToInt32(st.Tag);
            part = partdir / 10;
            dir = partdir % 10;
            ThreeDBoard newthreedboard = new ThreeDBoard();
            newthreedboard.NewMat(part, dir);
            newthreedboard.ShowDialog();
            newMat(part, newthreedboard.drNumToCardBoard,false);
        }

        public void Hint()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Boal[i, j] != null)
                        Boal[i, j].elipse.StrokeThickness = 5;
                }
            }
        }

        public void backToBlack()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Boal[i, j] != null)
                        Boal[i, j].elipse.StrokeThickness = 3;
                }
            }
        }
    }
}


