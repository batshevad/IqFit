
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;


namespace iq_project
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    /// 
    public partial class Board : UserControl //בניית לוח המשחק- עם צורות שלא ניתנות להזזה
    {
        public Gumot[,] GOfBoard { get; set; } = new Gumot[5, 10];

        MainWindow mw;

        long DR;
        long slicer;

        int matrixSize;
        int ii;//פונקצית הרמז
        int jj;//לפונקצית הרמז

        string Col;

        int firstPartForHint;
        int dirForHint;
        int xForHint;
        int yForHint;
        public Board() //פונקציה שמחשבת ע"פ ה-xml את מיקום הצורות והצבעים
        {

            InitializeComponent();
            for (int i = 0; i < 5; i++)//הצבה של הגומות שיצרנו קודם בלוח 
            {
                for (int j = 0; j < 10; j++)
                {
                    GOfBoard[i, j] = new Gumot();
                    Grid.SetRow(GOfBoard[i, j], i);
                    Grid.SetColumn(GOfBoard[i, j], j);
                    GOfBoard[i, j].elipse.Fill = Brushes.White;//וצביעה בצבע לבן
                    gBoard.Children.Add(GOfBoard[i, j]);
                }
            }
            XDocument doc = XDocument.Load(Environment.CurrentDirectory + "\\..\\..\\Cards.xml");//שליפה של דף ה-xml שבו שמורים פרטי הכרטיסים 
            foreach (XElement item in doc.Descendants("card"))
            {
                var numOfCard = int.Parse(item.Attribute("number").Value);//שליפת מספר כרטיס
                if (numOfCard == ClsGlobal.numOfCard)//אם מספר הכרטיס שווה למספר הכרטיס שאנחנו צריכים 
                {
                    foreach (var item1 in item.Descendants("part"))//שליפת מספר צורה
                    {
                        int numPart = int.Parse(item1.Attribute("number").Value);
                        ClsGlobal.shapesUse[numPart - 1]++;//עדכון מערך העזר, כדי שנדע שהשתמשנו בצורה הזו כבר
                        int numDirection = int.Parse(item1.Attribute("directionNum").Value);//שליפת מספר הכיוון הרצוי של הצורה
                        int place = int.Parse(item1.Attribute("place").Value);//שליפת המיקום הרצוי של על פני הלוח שבו הצורה צריכה להיות
                        NewMatInTheGrid(numPart, numDirection, place);//שליחה לפונקציה שבונה את הצורה במדויק בתוך המטריצה הנ"ל 
                    }
                }
            }

        }

        public long FindDr(int part, int dir)//פונקציה ששולפת מתוך דפי XML את הקוד של הצורה, ע"י מספר צורה ומספר כיוון
        {

            XDocument doc = XDocument.Load(Environment.CurrentDirectory + "\\..\\..\\Parts.xml");//שליפה של דף ה-xml שבו שמורים פרטי הכרטיסים 

            foreach (var item in doc.Descendants("part"))//ריצה על דף ה-xml
            {
                var num = int.Parse(item.Attribute("number").Value);//בדיקה האם זו הצורה שאנחנו צריכים
                if (num == part)
                {
                    Col = item.Attribute("color").Value.ToString();//שמירה של הצבע 
                    foreach (XElement item1 in item.Descendants("direction"))//ריצה על כל הכיוונים האפשריים של הצורה
                    {

                        var str = int.Parse(item1.Attribute("directionNum").Value);//בדיקה האם זה הכיוון שאנחנו צריכים
                        if (str == dir)
                        {
                            DR = long.Parse(item1.Attribute("dr").Value);//שמירה של פרטי הכיוון של הצורה- שמופיעים בצורה של אפסים ואחדות
                        }
                    }
                }
            }
            return DR;
        }

        public void FindMatrixSize(int part)
        {
            switch (part)//בודקים אם זו צורה מסדר 4 על 4 או 3 על 3
            {
                case 1:
                case 2:
                case 4:
                case 6:
                    matrixSize = 3;
                    slicer = 100000000;
                    break;
                default:
                    matrixSize = 4;
                    slicer = 1000000000000000;
                    break;
            }
        }

        public void NewMatInTheGrid(int numG, int drG, int place)// פונקציה שמקבלת מספר צורה ומספר כיוון ומסדרת במטריצה
        {


            long dr = FindDr(numG, drG);

            //בדיקה אם זו צורה מסדר 4 על 4 או 3 על 3
            FindMatrixSize(numG);

            int i = place % 10;
            int j = place / 10;

            for (int k = 0; k < matrixSize; k++)//הצבה בלוח
            {
                for (int l = 0; l < matrixSize; l++)
                {

                    long y = (dr / slicer) % 10;//ובודקים האם המיקום הזה שווה לאפס או לאחד ע"פ ה-xml

                    if (y == 1)//אם הוא שווה אחד
                    {
                        switch (Col)
                        {
                            case "dark green":
                                GOfBoard[i + k, j + l].elipse.Fill = Brushes.Green;// אם זה צבע ירוק 
                                break;
                            case "purple":
                                GOfBoard[i + k, j + l].elipse.Fill = Brushes.Purple;// אם זה צבע סגול
                                break;
                            case "light azure":
                                GOfBoard[i + k, j + l].elipse.Fill = Brushes.SkyBlue;// אם זה צבע תכלת
                                break;
                            case "light green":
                                GOfBoard[i + k, j + l].elipse.Fill = Brushes.LightGreen;// אם זה צבע ירוק בהיר 
                                break;
                            case "azure":
                                GOfBoard[i + k, j + l].elipse.Fill = Brushes.Blue;// אם זה צבע כחול
                                break;
                            case "blue":
                                GOfBoard[i + k, j + l].elipse.Fill = Brushes.DarkBlue;// אם זה צבע כחול כהה
                                break;
                            case "yellow":
                                GOfBoard[i + k, j + l].elipse.Fill = Brushes.Yellow;// אם זה צבע צהוב
                                break;
                            case "pink":
                                GOfBoard[i + k, j + l].elipse.Fill = Brushes.Pink; // אם זה צבע ורוד
                                break;
                            case "orange":
                                GOfBoard[i + k, j + l].elipse.Fill = Brushes.Orange; // אם זה צבע כתום
                                break;
                            case "red":
                                GOfBoard[i + k, j + l].elipse.Fill = Brushes.Red; // אם זה צבע אדום
                                break;
                        }
                    }
                    slicer = slicer / 10;
                }
            }
        }

        public void InBoard(int partDir, Point mikum, Point mikumPnimi, Point startPlace)//פונקציה שבודקת האם הצורה נגררה למקום חוקי או לא
        {
            mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            bool toCardBoardFlag = mikum.Y > mw.limitLabel;

            int j = (int)(mikum.X / mw.spaceRelativelyBoard_X);//כך מחשבים את נקודת הנחיתה של הצורה
            int i = (int)(mikum.Y / mw.spaceRelativelyBoard_Y);
            if (j < 10)
            {
                Gumot g;
                if (toCardBoardFlag)//אם הצורה נגררה ללוח התחתון המטרה של הבדיקה היא רק לבקוד האם יש צורך לשנות את הסימון של הגומות, ולצורך כך צריך "לאסוף" פרמטרים על הצורה
                {
                    g = new Gumot();
                }
                else
                    g = GOfBoard[i, j];

                int part = partDir / 10;//מחלצים את מספר הצורה 
                int dir = partDir % 10;//מחלצים את מספר הכיוון של הצורה

                long DR = FindDr(part, dir);
                FindMatrixSize(part);

                j = j - (int)(mikumPnimi.X / mw.spaceBetweenGumot);
                i = i - (int)(mikumPnimi.Y / mw.spaceBetweenGumot);
                long slicer1 = slicer;
                bool flag = true;//בא לסמן שהמקום לא מתאים לצורה, וצריך לצאת מהפונקציה
                long y;

                if (mikum.Y < mw.limitLabel)
                {
                    for (int k = 0; k < matrixSize; k++)
                    {
                        for (int l = 0; l < matrixSize; l++)
                        {
                            y = (DR / slicer1) % 10;
                            if (y == 1 && flag)
                            {
                                if (i + k < 0 || j + l < 0 || i + k >= 5 || j + l >= 10 || GOfBoard[i + k, j + l].elipse.Fill != Brushes.White || GOfBoard[i + k, j + l].IsTaken == true)
                                {
                                    flag = false;
                                    k = matrixSize;
                                }
                            }
                            slicer1 /= 10;
                        }
                    }
                }
                bool fromBoard = false;
                if (!flag)
                {
                    ClsGlobal.flagForTheDrag = false;//אם לא, היא תחזור בחזרה                                     
                    if (startPlace.Y < mw.limitLabel)//אם הצורה נגררה מהלוח העליון, צריך לשנות את המקור שלה ללבן רגיל
                    {//
                        j = (int)(startPlace.X / mw.spaceRelativelyBoard_X) - (int)(mikumPnimi.X / mw.spaceBetweenGumot); //כך מחשבים את נקודת המקור של הצורה, מאיפה היא נגררה
                        i = (int)(startPlace.Y / mw.spaceRelativelyBoard_Y) - (int)(mikumPnimi.Y / mw.spaceBetweenGumot);
                        fromBoard = true;
                        IsTaken(matrixSize, i, j, DR, slicer, fromBoard);
                        //  ClsGlobal.shapesUse[part-1] = 0;
                    }
                    //זה במקרה שנגרר מלמטה ללמעלה ולא נשאר- לא צובעים כלום
                    //או במקרה שנגרר מלמעלה ללמעלה ולא נשאר במקום החדש-צובעים את המקום הישן בלבן
                }
                else
                {
                    ClsGlobal.flagForTheDrag = true;//אם זה מקום חוקי, הצורה תשאר היכן שגררו אותה                                
                    if (startPlace.Y < mw.limitLabel)//אם הצורה נגררה מהלוח העליון, צריך לשנות את המקור שלה ללבן רגיל
                    {//במקרה שנגרר מלמעלה ללמעלה ונשאר צובעים את המקום החדש, ומחזירים את הישן ליהות כמו קודם
                        int jj = (int)(startPlace.X / mw.spaceRelativelyBoard_X) - (int)(mikumPnimi.X / mw.spaceBetweenGumot); //כך מחשבים את נקודת המקור של הצורה, מאיפה היא נגררה
                        int ii = (int)(startPlace.Y / mw.spaceRelativelyBoard_Y) - (int)(mikumPnimi.Y / mw.spaceBetweenGumot);
                        fromBoard = true;
                        IsTaken(matrixSize, ii, jj, DR, slicer, fromBoard);
                        fromBoard = false;
                    }
                    if (mikum.Y < mw.limitLabel)//במקרה שנגרר מלמעלה ללמטה וכן נשאר הישן חוזר ליהות כמו קודם ושום דבר חדש לא נצבע
                    {
                        IsTaken(matrixSize, i, j, DR, slicer, fromBoard);//זה במקרה שנגרר מלמטה ללמעלה וכן נשאר- צובעים את המיקום החדש 
                                                                         //ClsGlobal.shapesUse[part-1] = 1;
                    }
                    //  else
                    //  ClsGlobal.shapesUse[part-1] = 0;

                }
            }
        }

        public void IsTaken(int godelMatriza, int i, int j, long DR, long slicer, bool fromBoard)
        {
            for (int k = 0; k < godelMatriza; k++)
            {
                for (int l = 0; l < godelMatriza; l++)
                {
                    var y = (DR / slicer) % 10;
                    if (y == 1)
                    {
                        if (fromBoard)
                            GOfBoard[i + k, j + l].IsTaken = false;
                        else
                            GOfBoard[i + k, j + l].IsTaken = true;
                    }
                    slicer /= 10;
                }
            }
        }

        public void EndLevel()
        {
            bool flag = true;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 10; j++)
                    if (GOfBoard[i, j].elipse.Fill == Brushes.White && GOfBoard[i, j].IsTaken == false)
                        flag = false;
            if (flag)
            {
                mw.endTime = mw.startTime - mw.time;
                EndLevel end = new EndLevel(mw.endTime.Seconds, mw.endTime.Minutes, mw.allTimeInSeconds, ClsGlobal.numOfSteps);
                ClsGlobal.numOfSteps = 0;
                ClsGlobal.sumScore += mw.score;
                end.Show();
                mw.End();
            }
        }

        public void GoodHint(
            int dir, int[] arrToHint, int part, bool isFirstShape)//משתנה ראשון- בא לסמן את כיוון הצורה, מתחילים מהכיוון הראשון, וממשיכים עד שמוצאים משהו מתאים
        {//המשתנה השני בא במקום המשתנה הגלובלי (נראה לי עדיף)
         //המשתנה השלישי בא לסמן אם עובדים שוב על אותה צורה, א שכבר על צורה אחרת (אם הוא שווה אפס אז על צורה אחרת, אחרת עליו)

            bool IsShape = false;//בא לבדוק האם סיימנו לחפש את כל הצורות שהיו למטה, או שעוד לא
            bool flagForThisPlace = false;//בא לסמן אם מצאנו מקום מתאים לצורה
            ShearTzurot tzurot = null;

            for (int i = 0; i < 10 && !IsShape; i++)
            {
                //בדיקה האם לא סיימנו לעבור על כל הצורות שיש, אם סיימנו והכל טוב, מחזירים את הצורה שהיתה ראשונה 
                if (arrToHint[i] == 0)
                    IsShape = true;
            }
            if (!IsShape)//זה אומר שסיימנו לעבור על כל המערך! ברכותי! צריך להחזיר את הצורה הנכונה, עם הכיוון שעליו נעצרנו, ועם המקום ששם נעצרנו
            {
                FindMatrixSize(firstPartForHint);
                long DRForHint = FindDr(firstPartForHint, dirForHint);
                for (int i = xForHint; i < xForHint + matrixSize; i++)
                    for (int j = yForHint; j < yForHint + matrixSize; j++)
                    {
                        long y = (DRForHint / slicer) % 10;
                        if (y == 1)
                            GOfBoard[i, j].elipse.Stroke = Brushes.Gold;//אז צובעים את מסגרת הגומות שמתאימות לרמז בצבע זהב (לשלוש שניות)
                        slicer /= 10;
                    }
                // MessageBox.Show("part number " + firstPartForHint + "in dr number" + dirForHint + "start in place" + xForHint + "," + yForHint);
               // MessageBox.Show("Turn the " + Col + " shape to the gold place");
                HintMessageBox HMB = new HintMessageBox(Col, "Turn the ", " shape to the gold place");
                HMB.ShowDialog();
                for (int i = 0; i < 10; i++)
                    ClsGlobal.previosShapeToHint[i] = null;//איפוס המערך ששומר בשביל כל צורה את הפרטים על הצורה הקודמת ברקורסיה
                return;
            }

            FindMatrixSize(part);// מוצאים את סדר הגודל של הצורה (3*3 או 4*4 וזה נשמר במשתנה גלובלי
            long DR = FindDr(part, dir);//מוצאים את הקוד של הצורה+הכיוון שלה

            long slicer1 = slicer;

            for (int i = 0; i < 5; i++)//סריקה של הלוח העליון, על מנת למצוא מקום מתאים לצורה
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int k = i; k < (i + matrixSize); k++)
                    {
                        for (int l = j; l < (j + matrixSize); l++)
                        {
                            flagForThisPlace = true;
                            long y = (DR / slicer) % 10;
                            if (y == 1)//משווים את הצורה שמלטה ואת המקום על פני הלוח למעלה
                            {
                                if (k >= 5 || l >= 10)//אם יצאנו מגבול המטריצה, יוצאים מהלולאה ישר, כי אין טעם להמשיך לבדוק
                                {
                                    flagForThisPlace = false;
                                    k = i + matrixSize;
                                    l = j + matrixSize; 
                                }
                                else
                                if (ClsGlobal.previosShapeToHint[part - 1].BoardOnEnter[k, l] == true)
                                {
                                    flagForThisPlace = false;//אם המקום לא מתאים יוצאים מהלולאה, וממשיכים לחפש מהמקום הבא
                                    k = i + matrixSize;
                                    l = j + matrixSize;
                                }
                            }
                            if (flagForThisPlace)
                                slicer /= 10;
                            else
                                slicer = slicer1;
                        }
                    }
                    slicer = slicer1;

                    if (flagForThisPlace)//אם המקום הזה טוב, כלומר הוא מתאים לצורה שאותה בדקנו
                    {
                        arrToHint[part - 1] = 1;//מסמנים במערך שאנו סיימנו כעת לעבוד על הצורה הזו (כדי לא לחפש אותה שוב במקרה שלא נמצא לה מקום ונצטרך לחפש צורה אחרת)
                        if (isFirstShape)
                        {
                            firstPartForHint = part;
                            dirForHint = dir;
                            xForHint = i;
                            yForHint = j;
                        }

                        for (int k = i; k < (i + matrixSize); k++)
                        {
                            for (int l = j; l < (j + matrixSize); l++)
                            {
                                long y = (DR / slicer1) % 10;
                                if (y == 1 && k < 5 && l < 10)
                                    ClsGlobal.previosShapeToHint[part - 1].BoardOnExit[k, l] = true;//מסמנים את הלוח המתאים כך שישמר לנו המקום של הצורה+הכיוון שבודקים עכשיו
                                slicer1 /= 10;
                            }
                        }
                        i = 5;//יציאה מהלולאה
                        j = 10;//יציאה מהלולאה
                        int nextPart=0;//המשך בדיקה, עם הצורה הבאה
                        bool flagToNextPart = false;
                        for (int p = 0; p < 10 && flagToNextPart == false; p++)
                        {
                            if (arrToHint[p] == 0)
                            {
                                nextPart = p;
                                flagToNextPart = true;
                            }
                        }
                        ClsGlobal.previosShapeToHint[nextPart] = new Hint();
                        ClsGlobal.previosShapeToHint[nextPart].PreviosPart = part;//שמירת הצורה הקודמת
                        ClsGlobal.previosShapeToHint[nextPart].BoardOnEnter = ClsGlobal.previosShapeToHint[part - 1].BoardOnExit;//הלוח שביציאה של הצורה הראשונה זה הלוח בכניסה של הצורה הבאה
                        ClsGlobal.previosShapeToHint[nextPart].BoardOnExit = ClsGlobal.previosShapeToHint[part - 1].BoardOnExit;//הלוח שביציאה של הצורה השניה צריך בהתחלה להיות שווה ללוח שבכניסה, ואח"כ עוד מוסיפים לו את הבדיקות העכשוויות
                        ClsGlobal.previosShapeToHint[part - 1].LastDir = dir;//שמירת הכיוון האחרון
                        GoodHint(1, arrToHint, nextPart + 1, false);
                    }
                }
            }
            if (!flagForThisPlace)
            {
                if (dir < 8)//יש סה"כ 8 כיוונים, אז אם עברנו אותם ולא מצאנו כלום צריך לבדוק מה הצורה שהיתה קודם, ולחפש בשבילה כיוון אחר
                {
                    ClsGlobal.previosShapeToHint[part - 1].LastDir = dir + 1;//שמירת הכיוון האחרון
                    GoodHint(dir + 1, arrToHint, part, isFirstShape);
                }
                else
                {
                    int PrPart = ClsGlobal.previosShapeToHint[part - 1].PreviosPart-1;
                    arrToHint[PrPart] = 0;//מסמנים את הצורה שהיתה קודם ברקורסיה, כמו צורה שעדיין לא נבדקה
                    ClsGlobal.previosShapeToHint[PrPart].BoardOnExit = ClsGlobal.previosShapeToHint[PrPart].BoardOnEnter;//משנים את לוח היציאה של הצורה הקודמת כך שיהיה שווה ללוח הכניסה, כדי שאפשר יהיה להתייחס לצורה כאילו לא נבדק הכיוון הקודם
                    GoodHint(ClsGlobal.previosShapeToHint[PrPart].LastDir + 1, arrToHint, ClsGlobal.previosShapeToHint[part - 1].PreviosPart, ClsGlobal.previosShapeToHint[PrPart].IsFirstShape);//זה מקרה שצריך לחזור אחורה ברקורסיה, ולבדוק שוב צורה קודמת
                }
            }
            //return tzurot;
        }





        public void Hint()
        {
            int[] arrToHint = new int[10];
            int part;
            for (int i = 0; i < 10; i++)//עדיף שיהיה פה, ולא בפונקציה שמחשבת את הרמז
            {
                if (ClsGlobal.shapesUse[i] != 0)//מסמנים איזה צורות כבר נמצאות בלוח למעלה, כדי שלא נעשה רמז עליהם, לצורך זה משתמשים במערך הגלובלי
                    arrToHint[i] = 1;
            }

            Random random = new Random();
            int randomS = random.Next(0, 10);

            while (arrToHint[randomS] != 0)//בוחרים באופן רנדומלי צורה שנמצאת למטה, עליה נבצע את הבדיקה
            {
                randomS = random.Next(0, 10);
            }
            part = randomS + 1;
            ClsGlobal.previosShapeToHint[randomS] = new iq_project.Hint();//איפוס מערך העזר, שבו שמורות הצורות שהיו קודם ברמז
            ClsGlobal.previosShapeToHint[randomS].PreviosPart = 0;
            ClsGlobal.previosShapeToHint[randomS].IsFirstShape = true;

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 10; j++)
                {
                    if (GOfBoard[i, j].elipse.Fill != Brushes.White)
                    {
                        ClsGlobal.previosShapeToHint[randomS].BoardOnEnter[i, j] = true;
                        ClsGlobal.previosShapeToHint[randomS].BoardOnExit[i, j] = true;
                    }
                    else
                    {
                        ClsGlobal.previosShapeToHint[randomS].BoardOnEnter[i, j] = false;//מין העתק של הלוח איך שהוא נראה כשהפונקציה מתחילה לבדוק צורה כלשהיא
                        ClsGlobal.previosShapeToHint[randomS].BoardOnExit[i, j] = false;//בשביל הצורה הראשונה אנחנו מסמנים ככה גם בלוח של היציאה
                    }
                }
            GoodHint(1, arrToHint, part, true);//ועוד אחד כי מערך 
        }




        public void backToBlack()
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 10; j++)
                    if (GOfBoard[i, j].elipse.Stroke == Brushes.Gold)
                        GOfBoard[i, j].elipse.Stroke = Brushes.Black;
            


            //public ShearTzurot Hint2()
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        if (ClsGlobal.shapesUse[i] != 0)//מסמנים איזה צורות כבר נמצאות בלוח למעלה, כדי שלא נעשה רמז עליהם
            //            ClsGlobal.arrToHint[i] = 1;
            //    }
            //    ShearTzurot tsura = null;//טיפוס מסוג צורה שמוחזר בסיום הפונקציה, במקרה שלא נמצא רמז ערכו ישאר נאאל
            //    bool IsShape = false;//בא לבדוק האם סיימנו לחפש את כל הצורות שהיו למטה, או שעוד לא, אם סיימנו יוצת הודעה ואומרת שלא מצאנו כלום
            //    bool flagForThisPlace = true;//עוזר לדעת האם מצאנו מקום מתאים לרמז,ואפשר לצאת מהפונקציה או שעוד לא
            //    for (int i = 0; i < 10 && !IsShape; i++)
            //    {
            //        //בדיקה האם לא סיימנו לעבור על כל הצורות שיש, ולא מצאנו רמז. כי אם כן, אין מה להמשיך לבדוק
            //        if (ClsGlobal.arrToHint[i] == 0)
            //            IsShape = true;
            //    }
            //    if (!IsShape)
            //    {
            //        MessageBox.Show("cannot find anithing".ToString());
            //        for (int m = 0; m < 10; m++)
            //        {
            //            ClsGlobal.arrToHint[m] = 0;//ואיפוס מערך העזר של הצורות
            //        }
            //    }
            //    else
            //    {
            //        Random random = new Random();
            //        int randomS = random.Next(0, 10);

            //        while (ClsGlobal.arrToHint[randomS] != 0)//בוחרים באופן רנדומלי צורה שנמצאת למטה, עליה נבצע את הבדיקה
            //        {
            //            randomS = random.Next(0, 10);
            //        }
            //        ClsGlobal.arrToHint[randomS] = 1;//מסמנים במערך שאנו "עובדים" כעת על הצורה הזו (כדי לא לחפש אותה שוב במקרה שלא נמצא לה מקום ונצטרך לחפש צורה אחרת)
            //                                         //int rand_i = random.Next(0, 3);
            //                                         // int rand_j = random.Next(0, 8);

            //        //for (int i = rand_i; i < 5; i++)//זה מתחיל לבדוק את הלוח לא מההתחלה, אלא ממספר רנדומלי באמצע הלוח, כדי שהרמז ינתן תמיד במקום אחר
            //        //{//אם מהמקום הרנדומלי לא מצאנו מקום מתאים נבדור
            //        // for (int j = rand_j; j < 10; j++)

            //        int part = randomS + 1;
            //        FindMatrixSize(part);// מוצאים את סדר הגודל של הצורה (3*3 או 4*4 וזה נשמר במשתנה גלובלי
            //        long slicer1 = slicer;


            //        int dir = ClsGlobal.shapesDr[randomS];//מספר הצורה
            //        long DR = FindDr(part, dir);//מוצאים את הקוד של הצורה+הכיוון שלה
            //                                    //string ez_DR = DR.ToString();//זה בא כדי לדעת אם יש אפסים בתחילת ה-DR, כדי שנדע מאיפה להתחיל את המטריצה
            //                                    // int indexOfOne = ez_DR.IndexOf('1') - 1;

            //        for (int i = 0; i < 5; i++)//סריקה של הלוח העליון, על מנת למצוא מקום מתאים לצורה
            //        {
            //            for (int j = 0; j < 10; j++)
            //            {
            //                for (int k = i; k < (i + matrixSize); k++)
            //                {
            //                    for (int l = j; l < (j + matrixSize); l++)
            //                    {
            //                        flagForThisPlace = true;
            //                        long y = (DR / slicer) % 10;
            //                        if (y == 1)//משווים את הצורה שמלטה ואת המקום על פני הלוח למעלה
            //                        {
            //                            if (k >= 5 || l >= 10)//אם יצאנו מגבול המטריצה, יוצאים מהלולאה ישר, כי אין טעם להמשיך לבדוק
            //                            {
            //                                flagForThisPlace = false;
            //                                k = i + matrixSize;
            //                                l = j + matrixSize;
            //                            }
            //                            else
            //                            if (GOfBoard[k, l].elipse.Fill != Brushes.White || GOfBoard[k, l].IsTaken == true)
            //                            {
            //                                flagForThisPlace = false;//אם המקום לא מתאים יוצאים מהלולאה, וממשיכים לחפש מהמקום הבא
            //                                k = i + matrixSize;
            //                                l = j + matrixSize;
            //                            }
            //                        }
            //                        if (flagForThisPlace)
            //                            slicer /= 10;
            //                        else
            //                            slicer = slicer1;
            //                    }
            //                }

            //                slicer = slicer1;

            //                if (flagForThisPlace)//אם המקום הזה טוב, כלומר הוא מתאים לצורה שאותה בדקנו
            //                {
            //                    for (int k = i; k < (i + matrixSize); k++)
            //                    {
            //                        for (int l = j; l < (j + matrixSize); l++)
            //                        {
            //                            long y = (DR / slicer1) % 10;
            //                            if (y == 1 && k < 5 && l < 10)
            //                                GOfBoard[k, l].elipse.Stroke = Brushes.Gold;//אז צובעים את מסגרת הגומות בצבע זהב (לשלוש שניות)
            //                            slicer1 /= 10;
            //                        }
            //                    }
            //                    for (int m = 0; m < 10; m++)//ומשחררים את מערך העזר
            //                    {
            //                        ClsGlobal.arrToHint[m] = 0;
            //                    }
            //                    mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            //                    tsura = mw.c.Hint((part + "" + dir).ToString());//מחזירים את הצורה שעליה מרמזים, כדי לסמן אותה 
            //                    ii = i;//שומרים את המקום שבו הרמז, כדי שנוכל אח"כ להחזיר אותו לצבע במקורי
            //                    jj = j;//שומרים את המקום שבו הרמז, כדי שנוכל אח"כ להחזיר אותו לצבע במקורי

            //                    return tsura;

            //                }
            //            }
            //        }

            //        if (!flagForThisPlace)//אם לא מצאנו מקום מתאים, מפעילים את הפונקציה שוב, עם צורה אחרת
            //            return Hint();
            //    }
            //    return tsura;
            //}


            //for (int k = ii; k < (ii + matrixSize); k++)
            //{
            //    for (int l = jj; l < (jj + matrixSize); l++)
            //    {
            //        long y = (DR / slicer) % 10;
            //        if (y == 1 && k < 5 && l < 10)
            //            GOfBoard[k, l].elipse.Stroke = Brushes.Black;
            //        slicer /= 10;
            //    }

            //}
        }
    }
}
