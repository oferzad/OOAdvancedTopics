using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOAdvancedTopics
{
    internal class HomeWork_Oct24
    {
    }

    //תרגיל ראשון
    public class TimePeriod
    {
        private int seconds;

        public TimePeriod(int seconds)
        {
            this.seconds = seconds;
        }
        public int Seconds   
        {
            get { return seconds % 60; }
            set { seconds = value; }  
        }

        public int Minutes   
        {
            get { return (seconds / 60) % 60; }   
        }

        public int Hours   
        {
            get { return (seconds / 3600) % 24; }   
        }

        public int Days   
        {
            get { return (seconds / 86400); }   
        }

        public override string ToString()
        {
            return $"{Days}:{Hours}:{Minutes}:{Seconds}";
        }

        //הפעלה של המחלקה
        public static void Play()
        {
            TimePeriod t = new TimePeriod(24*60*60*2+60*60*18+60*48+15);
            Console.WriteLine(t);
            //print: 2:18:48:15
        }
    }
    class GenericsHomeWork
    {
        //הפעולה הנדרשת
        public static GenericSearchArray<T> Merge<T>(T[] arr1, T[] arr2)
        {
            GenericSearchArray<T> obj = new GenericSearchArray<T>(arr1.Length + arr2.Length);
            for (int i = 0; i < arr1.Length; i++) 
            {
                obj.Add(arr1[i]);
            }
            for (int i = 0;i < arr2.Length; i++)
            {
                obj.Add(arr2[i]);
            }
            return obj;
        }
        //הפעלה של הפעולה
        public static void Play()
        {
            int[] arr1 = { 1, 2, 3, 4 };
            int[] arr2 = { 5, 6, 7, 8 };

            GenericSearchArray<int> obj = Merge<int>(arr1, arr2);

            string[] arr3 = { "1", "2", "3", "4" };
            string[] arr4 = { "2",  "3", "4" };

            GenericSearchArray<string> obj2 = Merge<string>(arr3, arr4);

        }
    }




}
