using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iq_project
{
     public class Hint
    {
        private int part;
        private int dir;
        //private Hint previosPart;
        private bool[,] boardOnEnter = new bool[5, 10];
        private bool[,] boardOnExit = new bool[5, 10];
        private bool isFirstShape=false;
        public int PreviosPart { get => part; set => part = value; }//צריך לשמור איזה צורה היתה לפני ברקורסיה
        public int LastDir { get => dir; set => dir = value; }//ואיזה כיוון בדקנו אחרון, לפני שהרקורסיה עברה לצורה הבאה
        //public Hint PreviosPart { get => previosPart; set => previosPart = value; }
        public bool[,] BoardOnEnter { get => boardOnEnter; set => boardOnEnter = value; }//מראה הלוח בכניסה
        public bool[,] BoardOnExit { get => boardOnExit; set => boardOnExit = value; }//מראה הלוח ביציאה
        public bool IsFirstShape { get => isFirstShape; set => isFirstShape = value; }//מסמן אם זו הצורה הראשונה, שעליה צריך להחזיר תשובה למשתמש, או שזה רק צורה לבדיקה פנימית של הרקורסיה
    }
}
