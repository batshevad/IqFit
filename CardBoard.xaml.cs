
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace iq_project
{
    /// <summary>
    /// Interaction logic for CardBoard.xaml
    /// </summary>
    public partial class CardBoard : UserControl //דף שמסדר את הצורות המטריצה בסדר הנכון ובונה את הצורות הנותרות בלוח שלהן
    {

        public CardBoard()
        {
            InitializeComponent();
            doc = XDocument.Load(Environment.CurrentDirectory + "\\..\\..\\Parts.xml");//שולף את ה- xml  שבו שמורים הפרטים של הצורות
            OzherGumut(ClsGlobal.shapesUse);
        }

        double elementLeft, elementTop;
        bool IsDragging;

        Point selectedElementOrigins;
        Point currentPosition;
        Point startPoint;
        Point startPlace;

        UIElement OriginalElement = new UIElement();
        UIElement selectedElement;

        private XDocument doc;

        MainWindow mw;



        public void OzherGumut(int[] help)//בניית שאר הצורות שלא היו בלוח
        {

            int position = 0;
            for (int u = 0; u < 10; u++)
            {
                if (help[u] == 0)//בדיקה במערך העזר האם הצורה המסויימת נמצאת בלוח או לא
                {
                  
                    doc = XDocument.Load(Environment.CurrentDirectory + "\\..\\..\\Parts.xml");//שליפה של דף ה-xml עם פרטי הצורות
                    foreach (var item in doc.Descendants("part"))//ריצה על הצורות שבדף ה-xml
                    {
                        var num = int.Parse(item.Attribute("number").Value);
                        if (num - 1 == u)//אם זו הצורה שאנחנו צריכים
                        {
                            var col = item.Attribute("color").Value.ToString();//שמירת הצבע של הצורה
                            foreach (var item1 in item.Descendants("direction"))
                            {
                                var str = int.Parse(item1.Attribute("directionNum").Value);
                                if (str == ClsGlobal.numDirection)//הכיוון של הצורה נקבע כל פעם מחדש בעזרת ה- ClsGlobal
                                {
                                    ShearTzurot tsura = new ShearTzurot();
                                    tsura = tsura.newMat(u + 1, ClsGlobal.numDirection,false);//בניית הצורה
                                    for (position = 0; ClsGlobal.orderShapes[position] != 0 && position < 7; position++) ;
                                    tsura.Tag = num + "" + str;//שמירת מס' הצורה ומס' הכיוון 
                                    switch (position)//בדיקה לאן אפשר להכניס את הצורה על פני הלוח, כך שלא תעלה על צורה אחרת
                                    {
                                        case 0:
                                            canv1.Children.Add(tsura);
                                            Canvas.SetTop(tsura, 0);
                                            Canvas.SetLeft(tsura, 0);
                                            break;
                                        case 1:
                                            canv1.Children.Add(tsura);
                                            Canvas.SetTop(tsura, 0);
                                            Canvas.SetLeft(tsura, 160);
                                            break;
                                        case 2:
                                            canv1.Children.Add(tsura);
                                            Canvas.SetTop(tsura, 0);
                                            Canvas.SetLeft(tsura, 320);
                                            break;
                                        case 3:
                                            canv1.Children.Add(tsura);
                                            Canvas.SetTop(tsura, 0);
                                            Canvas.SetLeft(tsura, 480);
                                            break;
                                        case 4:
                                            canv1.Children.Add(tsura);
                                            Canvas.SetTop(tsura, 173);
                                            Canvas.SetLeft(tsura, 0);
                                            break;
                                        case 5:
                                            canv1.Children.Add(tsura);
                                            Canvas.SetTop(tsura, 173);
                                            Canvas.SetLeft(tsura, 160);
                                            break;
                                        case 6:
                                            canv1.Children.Add(tsura);
                                            Canvas.SetTop(tsura, 173);
                                            Canvas.SetLeft(tsura, 320);
                                            break;
                                        case 7:
                                            canv1.Children.Add(tsura);
                                            Canvas.SetTop(tsura, 173);
                                            Canvas.SetLeft(tsura, 480);
                                            break;
                                        default:
                                            break;
                                    }
                                    ClsGlobal.orderShapes[position]++;//סימון המקום, כדי לא לשים שם עוד צורה
                                }
                            }
                        }
                    }
                    ClsGlobal.numDirection++;//כיוון הצורה נקבע כל פעם ע"פ ה-ClsGlobal

                    if (ClsGlobal.numDirection > 8)//אין יותר משמונה כיוונים לכל צורה
                    {
                        ClsGlobal.numDirection = 1;//ואין פחות מאחד
                    }
                }
            }
       
            for (int i = 0; i < 8; i++)
            {
                ClsGlobal.orderShapes[i] = 0;
            }
        }


        //for (int i = 0; i < 10; i++)
        //{
        //    ClsGlobal.shapesUse[i] = 0;
        //}

        private void Canv1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)//פונ' שמופעלת כאשר צורה נלחצת- התחלת גרירה
        {
            if (e.Source == canv1)
                return;

            if (!IsDragging)
            {
                mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                startPlace = e.GetPosition(mw.b);
                startPoint = e.GetPosition(canv1);
                selectedElement = e.Source as UIElement;
                canv1.CaptureMouse();
                IsDragging = true;
                selectedElementOrigins =
                     new Point(
                        x: Canvas.GetLeft(selectedElement),
                        y: Canvas.GetTop(selectedElement));
            }
            e.Handled = true;
        }

        private void Canv1_PreviewMouseMove(object sender, MouseEventArgs e)//פונ' שמופעלת בזמן גרירה
        {
            if (canv1.IsMouseCaptured)
            {
                if (IsDragging)
                {
                    currentPosition = e.GetPosition(canv1);
                    elementLeft = (currentPosition.X - startPoint.X) +
                       selectedElementOrigins.X;
                    elementTop = (currentPosition.Y - startPoint.Y) +
                       selectedElementOrigins.Y;
                    Canvas.SetLeft(selectedElement, elementLeft);
                    Canvas.SetTop(selectedElement, elementTop);
                }
            }
        }

        private void Canv1_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)//פונ' שמופעלת כאשר העכבר עוזב את הצורה, וצריך לחשב אם להשאיר אותו במקום או להחזיר למקום הקודם
        {
            ShearTzurot tsura = new ShearTzurot();
            foreach (UIElement child in canv1.Children)
            {
                if (child.Equals(selectedElement))
                {
                    tsura = (ShearTzurot)selectedElement;
                    break;
                }
            }

            if (canv1.IsMouseCaptured)
            {

                Point pBoard = e.GetPosition(mw.b);//שומר את נקודת המיקום יחסית ללוח
                Point pCanv1 = e.GetPosition(selectedElement);//שומר את ערכי המיקום יחסית לצורה עצמה, בתוך הצורה               

                if (!(pBoard.Y > mw.limitLabel && startPlace.Y > mw.limitLabel))//תמיד צריך לבדוק, חוץ ממתי שנגרר מלמטה ללמטה, אז אין צורך
                {

                    mw.b.InBoard(Convert.ToInt32(tsura.Tag), pBoard, pCanv1, startPlace);
                    if (ClsGlobal.flagForTheDrag)
                    {
                        Canvas.SetLeft(selectedElement, elementLeft);
                        Canvas.SetTop(selectedElement, elementTop);
                        ClsGlobal.numOfSteps++;//מספר הצעדים גדל
                        mw.b.EndLevel();
                    }

                    else
                    {
                        Canvas.SetLeft(selectedElement, pCanv1.X);
                        Canvas.SetTop(selectedElement, pCanv1.Y);
                    }
                }
            }
            IsDragging = false;
            canv1.ReleaseMouseCapture();
            e.Handled = true;
            ClsGlobal.flagForTheDrag = false;
        }
        public ShearTzurot Hint(string partDir)
        {
            ShearTzurot tsura = new ShearTzurot();
            foreach (UIElement child in canv1.Children)
            {
                tsura = (ShearTzurot)child;
                if (tsura.Tag.ToString() == partDir)
                {
                    tsura.Hint();
                    return tsura;
                }
            }
            return tsura;
        }
    }
}




