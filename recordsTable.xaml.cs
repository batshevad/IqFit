using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Threading;

namespace iq_project
{
    /// <summary>
    /// Interaction logic for recordsTable.xaml
    /// </summary>
    public partial class recordsTable : Window
    {/// <summary>
     /// колличество точек в слое
     /// </summary>
        public int count = 75;

        /// <summary>
        /// шаг перемещения точки
        /// </summary>
        public int maxTranform = 1;

        /// <summary>
        /// минимальная длина между токами для создания связи на переднем слое
        /// </summary>
        public float minDistantion = 50;

        /// <summary>
        /// минимальная длина между токами для создания связи на заднем слое
        /// </summary>
        public float minDistantionBackground = 60;

        /// <summary>
        /// минимальная длина между токами для создания связи на втором заднем слое
        /// </summary>
        public float minDistantionBackground2 = 70;

        /// <summary>
        /// максимальный размер точек
        /// </summary>
        public int maxSize = 8;

        /// <summary>
        /// класс с информацией о точке
        /// </summary>
        public class ellipseInfo
        {
            /// <summary>
            /// координата точки
            /// </summary>
            public int x { get; set; }

            /// <summary>
            /// координата точки
            /// </summary>
            public int y { get; set; }

            /// <summary>
            /// фигура точки
            /// </summary>
            public Ellipse ellipse { get; set; }

            /// <summary>
            /// линия точки
            /// </summary>
            public List<Line> lines = new List<Line>();
        }

        /// <summary>
        /// массив точек на переднем плане
        /// </summary>
        public List<ellipseInfo> allEllipse = new List<ellipseInfo>();

        /// <summary>
        /// массив точек на заднем плане
        /// </summary>
        public List<ellipseInfo> allEllipseBackground = new List<ellipseInfo>();

        /// <summary>
        /// массив точек на втором заднем плане
        /// </summary>
        public List<ellipseInfo> allEllipseBackground2 = new List<ellipseInfo>();

        /// <summary>
        /// таймер запускающий функции перемещения точек
        /// </summary>
        DispatcherTimer dispatcherTimer = new DispatcherTimer();


        public recordsTable()
        {
            InitializeComponent();
            try
            {

                Select("SELECT userName,sumScore FROM[dbo].[Users] ORDER BY [sumScore] "); //שליפת פרטי השחקנים+הנקודות, בסדר יורד
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }


            CreateParalaxBackground();
            CreateParalaxBackground2();
            CreateParalax();

            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 30);
            dispatcherTimer.Tick += UpdateParalaxBackground;
            dispatcherTimer.Tick += UpdateParalaxBackground2;
            dispatcherTimer.Tick += UpdateParalax;
            dispatcherTimer.Start();

        }
        public DataTable Select(string selectSQL)
        {
            DataTable dataTable = new DataTable("dataBase");                                                                                         // подключаемся к базе данных
            SqlConnection sqlConnection = new SqlConnection(@"Data Source =(LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Avraham\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\New Database.mdf ");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            recordsGrid.ItemsSource = dataTable.DefaultView;
            return dataTable;


        }

        //private void GetLineColor(Line line)
        //{
        //    int r;
        //    //for random coloring
        //    Random rand = new Random();
        //    r = rand.Next(1, 10);
        //    switch (r)
        //    {
        //        case 1:
        //            line.Stroke = new SolidColorBrush(Colors.Green);// אם זה צבע ירוק 
        //            break;
        //        case 2:
        //            line.Stroke = new SolidColorBrush(Colors.Red);// אם זה צבע סגול
        //            break;
        //        case 3:
        //            line.Stroke = new SolidColorBrush(Colors.Blue);// אם זה צבע תכלת
        //            break;
        //        case 4:
        //            line.Stroke = new SolidColorBrush(Colors.Yellow);// אם זה צבע ירוק בהיר 
        //            break;
        //        case 5:
        //            line.Stroke = new SolidColorBrush(Colors.Blue);// אם זה צבע כחול
        //            break;
        //        case 6:
        //            line.Stroke = new SolidColorBrush(Colors.DarkBlue);// אם זה צבע כחול כהה
        //            break;
        //        case 7:
        //            line.Stroke = new SolidColorBrush(Colors.Yellow);// אם זה צבע צהוב
        //            break;
        //        case 8:
        //            line.Stroke = new SolidColorBrush(Colors.Pink); // אם זה צבע ורוד
        //            break;
        //        case 9:
        //            line.Stroke = new SolidColorBrush(Colors.Orange); // אם זה צבע כתום
        //            break;
        //        case 10:
        //            line.Stroke = new SolidColorBrush(Colors.Red); // אם זה צבע אדום
        //            break;
        //    }
        //}
        //private void GetEllipceColor(Ellipse ellipse)
        //{
        //    int r;
        //    //for random coloring
        //    Random rand = new Random();
        //    r = rand.Next(1, 10);
        //    switch (r)
        //    {
        //        case 1:
        //            ellipse.Fill = new SolidColorBrush(Colors.Green);// אם זה צבע ירוק 
        //            break;
        //        case 2:
        //            ellipse.Fill = new SolidColorBrush(Colors.Purple);// אם זה צבע סגול
        //            break;
        //        case 3:
        //            ellipse.Fill = new SolidColorBrush(Colors.SkyBlue);// אם זה צבע תכלת
        //            break;
        //        case 4:
        //            ellipse.Fill = new SolidColorBrush(Colors.LightGreen);// אם זה צבע ירוק בהיר 
        //            break;
        //        case 5:
        //            ellipse.Fill = new SolidColorBrush(Colors.Blue);// אם זה צבע כחול
        //            break;
        //        case 6:
        //            ellipse.Fill = new SolidColorBrush(Colors.DarkBlue);// אם זה צבע כחול כהה
        //            break;
        //        case 7:
        //            ellipse.Fill = new SolidColorBrush(Colors.Yellow);// אם זה צבע צהוב
        //            break;
        //        case 8:
        //            ellipse.Fill = new SolidColorBrush(Colors.Pink); // אם זה צבע ורוד
        //            break;
        //        case 9:
        //            ellipse.Fill = new SolidColorBrush(Colors.Orange); // אם זה צבע כתום
        //            break;
        //        case 10:
        //            ellipse.Fill = new SolidColorBrush(Colors.Red); // אם זה צבע אדום
        //            break;
        //    }
        //}


        /// <summary>
        /// Обновление позиции точек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateParalax(object sender, EventArgs e)
        {
            // יצירת רנדום
            Random random = new Random();

            // קליטת קואורדינטות העכבר
            allEllipse[count].x = (int)Mouse.GetPosition(canvas).X;
            allEllipse[count].y = (int)Mouse.GetPosition(canvas).Y;

            // עוברים על הנקודות שברמה המתאימה
            for (int i = 0; i < allEllipse.Count; i++)
            {
                // אם הנקודה הספציפית ביניהם
                if (i < count)
                {
                    // מגבילים את התזוזה כדי שלא יצא מהמסך
                    if (allEllipse[i].x < 20)
                    {
                        // מזיזים את הנקודה
                        allEllipse[i].x += random.Next(0, maxTranform);
                    }
                    else if (allEllipse[i].x > Width - 20)
                    {
                        // מעבירים אותה למקום החדש
                        allEllipse[i].x += random.Next(-maxTranform, 0);
                    }
                    else
                        // מעבירים למקום החדש
                        allEllipse[i].x += random.Next(-maxTranform, maxTranform + 1);

                    // מגבילים את התזוזה כדי שלא יצא מהמסך
                    if (allEllipse[i].y < 20)
                    {
                        // מזיזים את הנקודה
                        allEllipse[i].y += random.Next(0, maxTranform);
                    }
                    else if (allEllipse[i].y > Height - 20)
                    {
                        //  מעבירים אותה למקום החדש
                        allEllipse[i].y += random.Next(-maxTranform, 0);
                    }
                    else
                        //  מעבירים אותה למקום החדש
                        allEllipse[i].y += random.Next(-maxTranform, maxTranform + 1);
                }

                // מעבירים את הנקודה על פני הקנבס
                Canvas.SetLeft(allEllipse[i].ellipse, allEllipse[i].x);
                Canvas.SetTop(allEllipse[i].ellipse, allEllipse[i].y);


                // מבטלים את הקווים שהיו קודם
                for (int j = 0; j < allEllipse[i].lines.Count; j++)
                {
                    canvas.Children.Remove(allEllipse[i].lines[j]);
                    allEllipse[i].lines.Remove(allEllipse[i].lines[j]);
                }

                // עוברים על שאר העיגולים
                for (int j = i + 1; j < allEllipse.Count; j++)
                {
                    // קואורדינטות נקודה מס1
                    double x1 = allEllipse[i].x + allEllipse[i].ellipse.Width / 2;
                    double y1 = allEllipse[i].y + allEllipse[i].ellipse.Width / 2;

                    // קואורדינטות נקודה מס2
                    double x2 = allEllipse[j].x + allEllipse[j].ellipse.Width / 2;
                    double y2 = allEllipse[j].y + allEllipse[j].ellipse.Width / 2;

                    double distantion = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
                    if (distantion < minDistantion)
                    {
                        Line line = new Line();
                        line.X1 = x1;
                        line.Y1 = y1;
                        line.X2 = x2;
                        line.Y2 = y2;
                        line.Stroke = new SolidColorBrush(Colors.Red);
                     
                        line.StrokeThickness = 1;

                        canvas.Children.Add(line);
                        allEllipse[i].lines.Add(line);
                    }
                }
            }
        }

        /// <summary>
        /// Обновление позиции точек заднего фона
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateParalaxBackground(object sender, EventArgs e)
        {
            Random random = new Random();

            allEllipseBackground[count].x = (int)Mouse.GetPosition(canvas).X;
            allEllipseBackground[count].y = (int)Mouse.GetPosition(canvas).Y;

            for (int i = 0; i < allEllipseBackground.Count; i++)
            {

                if (i < count)
                {
                    // מגבילים את התזוזה כדי שלא יצא מהמסך
                    if (allEllipseBackground[i].x < 20)
                    {
                        allEllipseBackground[i].x += random.Next(0, maxTranform);
                    }
                    else if (allEllipseBackground[i].x > Width - 20)
                    {
                        allEllipseBackground[i].x += random.Next(-maxTranform, 0);
                    }
                    else
                        allEllipseBackground[i].x += random.Next(-maxTranform, maxTranform + 1);

                    // מגבילים את התזוזה כדי שלא יצא מהמסך
                    if (allEllipseBackground[i].y < 20)
                    {
                        allEllipseBackground[i].y += random.Next(0, maxTranform);
                    }
                    else if (allEllipseBackground[i].y > Height - 20)
                    {
                        allEllipseBackground[i].y += random.Next(-maxTranform, 0);
                    }
                    else
                        allEllipseBackground[i].y += random.Next(-maxTranform, maxTranform + 1);
                }


                Canvas.SetLeft(allEllipseBackground[i].ellipse, allEllipseBackground[i].x);
                Canvas.SetTop(allEllipseBackground[i].ellipse, allEllipseBackground[i].y);


                for (int j = 0; j < allEllipseBackground[i].lines.Count; j++)
                {
                    canvas.Children.Remove(allEllipseBackground[i].lines[j]);
                    allEllipseBackground[i].lines.Remove(allEllipseBackground[i].lines[j]);
                }

                for (int j = i + 1; j < allEllipse.Count; j++)
                {
                    double x1 = allEllipseBackground[i].x + allEllipseBackground[i].ellipse.Width / 2;
                    double y1 = allEllipseBackground[i].y + allEllipseBackground[i].ellipse.Width / 2;

                    double x2 = allEllipseBackground[j].x + allEllipseBackground[j].ellipse.Width / 2;
                    double y2 = allEllipseBackground[j].y + allEllipseBackground[j].ellipse.Width / 2;

                    double distantion = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
                    if (distantion < minDistantionBackground)
                    {
                        Line line = new Line();
                        line.X1 = x1;
                        line.Y1 = y1;
                        line.X2 = x2;
                        line.Y2 = y2;
                        line.Stroke = new SolidColorBrush(Colors.Green);
             
                        line.StrokeThickness = 1;

                        canvas.Children.Add(line);
                        allEllipseBackground[i].lines.Add(line);
                    }
                }
            }
        }

        /// <summary>
        /// Обновление позиции точек заднего фона
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateParalaxBackground2(object sender, EventArgs e)
        {
            Random random = new Random();

            allEllipseBackground2[count].x = (int)Mouse.GetPosition(canvas).X;
            allEllipseBackground2[count].y = (int)Mouse.GetPosition(canvas).Y;

            for (int i = 0; i < allEllipseBackground2.Count; i++)
            {

                if (i < count)
                {
                    // מגבילים את התזוזה כדי שלא יצא מהמסך
                    if (allEllipseBackground2[i].x < 20)
                    {
                        allEllipseBackground2[i].x += random.Next(0, maxTranform);
                    }
                    else if (allEllipseBackground2[i].x > Width - 20)
                    {
                        allEllipseBackground2[i].x += random.Next(-maxTranform, 0);
                    }
                    else
                        allEllipseBackground2[i].x += random.Next(-maxTranform, maxTranform + 1);

                    // מגבילים את התזוזה כדי שלא יצא מהמסך
                    if (allEllipseBackground2[i].y < 20)
                    {
                        allEllipseBackground2[i].y += random.Next(0, maxTranform);
                    }
                    else if (allEllipseBackground2[i].y > Height - 20)
                    {
                        allEllipseBackground2[i].y += random.Next(-maxTranform, 0);
                    }
                    else
                        allEllipseBackground2[i].y += random.Next(-maxTranform, maxTranform + 1);
                }


                Canvas.SetLeft(allEllipseBackground2[i].ellipse, allEllipseBackground2[i].x);
                Canvas.SetTop(allEllipseBackground2[i].ellipse, allEllipseBackground2[i].y);


                for (int j = 0; j < allEllipseBackground2[i].lines.Count; j++)
                {
                    canvas.Children.Remove(allEllipseBackground2[i].lines[j]);
                    allEllipseBackground2[i].lines.Remove(allEllipseBackground2[i].lines[j]);
                }

                for (int j = i + 1; j < allEllipse.Count; j++)
                {
                    double x1 = allEllipseBackground2[i].x + allEllipseBackground2[i].ellipse.Width / 2;
                    double y1 = allEllipseBackground2[i].y + allEllipseBackground2[i].ellipse.Width / 2;

                    double x2 = allEllipseBackground2[j].x + allEllipseBackground2[j].ellipse.Width / 2;
                    double y2 = allEllipseBackground2[j].y + allEllipseBackground2[j].ellipse.Width / 2;

                    double distantion = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
                    if (distantion < minDistantionBackground2)
                    {
                        Line line = new Line();
                        line.X1 = x1;
                        line.Y1 = y1;
                        line.X2 = x2;
                        line.Y2 = y2;
                        line.Stroke = new SolidColorBrush(Colors.Yellow);//it was gray
                        //GetLineColor(line);
                        line.StrokeThickness = 1;

                        canvas.Children.Add(line);
                        allEllipseBackground2[i].lines.Add(line);
                    }
                }
            }
        }

        /// <summary>
        /// Метод создающий первоначальные точки для paralax
        /// </summary>
        public void CreateParalax()
        {
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                ellipseInfo newElement = new ellipseInfo();
                newElement.x = random.Next(20, (int)Width - 20);
                newElement.y = random.Next(20, (int)Height - 20);

                Ellipse ellipse = new Ellipse();
                Canvas.SetLeft(ellipse, newElement.x);
                Canvas.SetTop(ellipse, newElement.y);

                int widthEllips = random.Next(3, maxSize);

                ellipse.Width = widthEllips;
                ellipse.Height = widthEllips;
                ellipse.Fill = new SolidColorBrush(Colors.Blue);//it was black
                //GetEllipceColor(ellipse);

                newElement.ellipse = ellipse;
                canvas.Children.Add(newElement.ellipse);
                allEllipse.Add(newElement);

            }

            ellipseInfo mouseElement = new ellipseInfo();
            mouseElement.x = (int)Mouse.GetPosition(canvas).X;
            mouseElement.y = (int)Mouse.GetPosition(canvas).Y;

            Ellipse mouseEllipse = new Ellipse();
            mouseEllipse.Width = 2;
            mouseEllipse.Height = 2;
            Canvas.SetLeft(mouseEllipse, mouseElement.x);
            Canvas.SetTop(mouseEllipse, mouseElement.y);

            mouseElement.ellipse = mouseEllipse;
            canvas.Children.Add(mouseElement.ellipse);
            allEllipse.Add(mouseElement);
        }

        /// <summary>
        /// Метод создающий первоначальные точки для paralax
        /// </summary>
        public void CreateParalaxBackground()
        {
            // יוצר רנדום
            Random random = new Random();

            // יצירת נקודות
            for (int i = 0; i < count; i++)
            {
                // יצירת האלמנט
                ellipseInfo newElement = new ellipseInfo();
                // יצירת קואורדינטות רנדומליות
                newElement.x = random.Next(20, (int)Width - 20);
                newElement.y = random.Next(20, (int)Height - 20);

                // יצירת עיגול
                Ellipse ellipse = new Ellipse();
                // קואורדינטות
                Canvas.SetLeft(ellipse, newElement.x);
                Canvas.SetTop(ellipse, newElement.y);

                // גובה+רוחב רנדומלי
                int widthEllips = random.Next(3, maxSize);

                // שמירת הגובה והרוחב כמשתנים של הצורה
                ellipse.Width = widthEllips;
                ellipse.Height = widthEllips;
                // צביעה
                ellipse.Fill = new SolidColorBrush(Colors.Orange);
               

                // שומרים את הנתונים 
                newElement.ellipse = ellipse;
                // מוסיפים את העיגול לקנבס
                canvas.Children.Add(newElement.ellipse);
                // שומרים במערך העיגולים
                allEllipseBackground.Add(newElement);

            }

            // מידע על העיגול בשביל העכבר
            ellipseInfo mouseElement = new ellipseInfo();
            // הקואורדינטות של העיגול
            mouseElement.x = (int)Mouse.GetPosition(canvas).X;
            mouseElement.y = (int)Mouse.GetPosition(canvas).Y;

            // יצירת עיגול
            Ellipse mouseEllipse = new Ellipse();
            // גובה+רוחב של העיגול
            mouseEllipse.Width = 2;
            mouseEllipse.Height = 2;
            Canvas.SetLeft(mouseEllipse, mouseElement.x);
            Canvas.SetTop(mouseEllipse, mouseElement.y);

            // שומרים את הנתונים 
            mouseElement.ellipse = mouseEllipse;
            // מוסיפים את העיגול לקנבס
            canvas.Children.Add(mouseElement.ellipse);
            // שומרים במערך העיגולים
            allEllipseBackground.Add(mouseElement);
        }

        /// <summary>
        /// Метод создающий первоначальные точки для paralax
        /// </summary>
        public void CreateParalaxBackground2()
        {
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                ellipseInfo newElement = new ellipseInfo();
                newElement.x = random.Next(20, (int)Width - 20);
                newElement.y = random.Next(20, (int)Height - 20);

                Ellipse ellipse = new Ellipse();
                Canvas.SetLeft(ellipse, newElement.x);
                Canvas.SetTop(ellipse, newElement.y);

                int widthEllips = random.Next(3, maxSize);

                ellipse.Width = widthEllips;
                ellipse.Height = widthEllips;
                ellipse.Fill = new SolidColorBrush(Colors.Pink);//it was Gray
                //GetEllipceColor(ellipse);

                newElement.ellipse = ellipse;
                canvas.Children.Add(newElement.ellipse);
                allEllipseBackground2.Add(newElement);

            }

            ellipseInfo mouseElement = new ellipseInfo();
            mouseElement.x = (int)Mouse.GetPosition(canvas).X;
            mouseElement.y = (int)Mouse.GetPosition(canvas).Y;

            Ellipse mouseEllipse = new Ellipse();
            mouseEllipse.Width = 2;
            mouseEllipse.Height = 2;
            Canvas.SetLeft(mouseEllipse, mouseElement.x);
            Canvas.SetTop(mouseEllipse, mouseElement.y);

            mouseElement.ellipse = mouseEllipse;
            canvas.Children.Add(mouseElement.ellipse);
            allEllipseBackground2.Add(mouseElement);
        }

    }
}
    

