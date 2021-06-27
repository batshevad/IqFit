namespace iq_project
{
    public static class ClsGlobal //מחלקת עזר סטטית
    {
        public static int numOfCard = 0;//עוזר לדעת את מספר השלב הפתוח כרגע
        public static int numDirection = 1;//מגריל כל פעם מספר כיוון עבור הצורות הניתנות להזזה
        public static int[] shapesUse = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };//מערך עזר כדי לדעת אח"כ באילו צורות השתמשנו ובאילו עוד לא
        public static int[] shapesDr = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };//מערך עזר כדי לדעת מה הכיוונים של הצורות שנמצאות למטה
        public static int[] orderShapes = { 0, 0, 0, 0, 0, 0, 0, 0 };//מערך שעוזר לסדר את הצורות על פני הלוח, כך שלא יעלו אחת על השניה
        public static int[] arrToHint = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };//בא לעזור לפונקצית הרמז, כדי שיהיה אפשר לעשות רמז על כל הצורות
        public static Hint[] previosShapeToHint = new Hint[10];
        //בא כדי לזכור איזה צורה היתה לפני הצורה שעושים עליה רמז עכשיו, כדי שאפשר לחזור אליה ברקורסיה
        public static bool flagForTheDrag;//עוזר לדעת האם צורה נגררה למקום חורי, ואז היא נשארת שם, או שהיא צריכה לחזור בחזרה
        public static int sumScore = 0;//סופר את הנקודות שצבר השחקן
        public static int numOfSteps = 0;//סופר כמה צעדים עשה השחקן בשלב הנוחכי
        public static string userNameString = "";//שומר את המחרוזת שכוללת את שם השחקן
        public static string userName = "";//שומר את שם השחקן בלבד
        public static NewLevel Level = new NewLevel();
        public static void Set_Level(NewLevel NL) => Level = NL;//שמירת הלחצן של השלב הנוכחי (כדי לצבוע אותו בעת הצורך)

        public static string DoneLevels { get; set; } = "";//שןמר את השלבים שהשחקן כבר עשה
        public static int NumLevel { get; set; } = 0;//עוזר לכתוב את מספרי השלבים על הלחצנים
    }
}
