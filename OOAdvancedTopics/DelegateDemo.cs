using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOAdvancedTopics
{
    class LambdaDemo
    {
        public static int Kuku(int x, int y) { return x + y; }
        public delegate string NumKuku(int x);
        public static void Demo1()
        {
            #region What are delegates and lambda operator =>
            /* מערך שיעור – Lambda and Delegate
1.	מעבר על התרגילים של List – פתרון והסבר, מענה על שאלות.
2.	הסבר על delegate:
a.	מחלקות הם טיפוסים של אובייקטים
b.	Delegates הם טיפוסים של פעולות! לדוגמא, אפשר להגדיר טיפוס delegate שמתאר פעולה שמקבלת מספר ומחזירה מחרוזת.
c.	עכשיו אפשר להגדיר משתנה מהטיפוס שהוגדר!
d.	ולתת לו ערך – שם פעולה מהטיפוס הזה!
e.	ואפשר להשתמש בהפניה כדי להפעיל את הפעולה.
3.	הסבר על lambda אופרטור:
a.	האופרטור <= מאפשר ליצור הפניה לפעולה ואת הפעולה עצמה. הראה דוגמא!
4.	בשביל מה צריך את זה? 
a.	בעיקר על מנת להעביר הפניה לפעולה כפרמטר לפעולה כלשהי... לדוגמא, נסתכל על פעולת חיפוש ב list של הקופים. פעולת החיפוש לפי שם תהיה דומה לפעולת חיפוש לפי מקום וכו,... אז אפשר להעביר לפעולת החיפוש פעולה שפועלת על קוף ומחזירה אמת או שקר אם החיפוש הצליח או לא!

             */
            NumKuku x = a => $"kuku{a}"; //return is not written if no command block was opened
            NumKuku y = a =>
            {
                a *= 2;
                return $"kuku{a}";
            };
            Console.WriteLine(x(3));
            Console.WriteLine(y(3));
            #endregion 
            #region Action Delegate
            Action writeOfer = () => Console.WriteLine("Ofer");

            writeOfer();

            Action writeMore = () =>
            {
                Console.WriteLine("I am writing more");
                Console.WriteLine("I am writing more");
                Console.WriteLine("I am writing more");
            };

            writeMore();
            #endregion
            #region Func<> delegate
            Func<int, int> square = x => x * x;
            Console.WriteLine(square(4));

            Func<int, int, int> myKuku = Kuku;
            #endregion

        }

    }
    
}
