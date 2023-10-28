using System.Reflection;

namespace OOAdvancedTopics
{
    #region Properties and Generics
    class TimePeriod1
    {
        private double seconds;
        public TimePeriod1()
        {
            this.seconds = 0;
        }
        //Define a property to read and write to the variable seconds.
        public double Seconds
        {
            get
            {
                return this.seconds;
            }
            set
            {
                this.seconds = value;
            }
        }

        //Another option to do the same without the seconds variable!
        //public double Seconds { get; set; }
        //you can define permissions
        //public double Seconds { get; private set; }
        //Define Hours property that expose another way of reading seconds as hours with verification
        public double Hours
        {
            get
            {
                return this.seconds / 3600;
            }
            set
            {
                if (value < 0 || value > 24)
                    throw new ArgumentOutOfRangeException("Hours must be between 0 and 24.");
                this.seconds = value * 3600;
            }
        }

        public double Days
        {
            get
            {
                return this.seconds / (24 * 3600);
            }
            set
            {
                this.seconds = value * 24 * 3600;
            }
        }

        public override string ToString()
        {
            return PropReflection.ObjectToString(this);
        }
    }

    class Player
    {

        public string Name { get; set; }
        public int BirthYear { get; set; }
    }
    class SoccerPlayer : Player
    {
        public int Goals { get; private set; }
        public SoccerPlayer() { Goals = 0; }
        public void AddGoal() { Goals++; }

        public override string ToString()
        {
            return PropReflection.ObjectToString(this);
        }
    }

    class PropReflection
    {
        public static void DemoProperties1()
        {
            TimePeriod1 t = new TimePeriod1();
            // The property assignment causes the 'set' accessor to be called.
            t.Hours = 24;

            // Retrieving the property causes the 'get' accessor to be called.
            Console.WriteLine("Time in hours: {0}", t.Hours);
        }

        public static void DemoProperties2()
        {
            SoccerPlayer sc1 = new SoccerPlayer();
            sc1.BirthYear = 1980;
            sc1.Name = "Kuku";
            sc1.AddGoal();

            Console.WriteLine($"{sc1.Name}, {sc1.BirthYear}, {sc1.Goals}");

            //OR

            SoccerPlayer sc2 = new SoccerPlayer()
            {
                BirthYear = 1980,
                Name = "Kuku"
            };
            sc2.AddGoal();

            Console.WriteLine($"{sc1.Name}, {sc1.BirthYear}, {sc1.Goals}");
        }

        public static void DemoType()
        {
            TimePeriod1 obj = new TimePeriod1();
            obj.Seconds = 7000;
            PrintProperties(obj);
        }

        public static void PrintProperties(Object obj)
        {
            //Get the type of the object!
            Type t = obj.GetType();
            Console.WriteLine("Type of object is {0}", t);
            // Get the public properties of the instance (not only related to Object).
            PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // Display information for all properties.
            for (int i = 0; i < propInfos.Length; i++)
            {
                PropertyInfo propInfo = propInfos[i];
                bool readable = propInfo.CanRead;
                bool writable = propInfo.CanWrite;

                Console.WriteLine("   Property name: {0}", propInfo.Name);
                Console.WriteLine("   Property type: {0}", propInfo.PropertyType);
                Console.WriteLine("   Read:{0}, Write {1}", readable, writable);
                if (readable)
                {
                    Console.WriteLine("   Property value: {0}", propInfo.GetValue(obj));
                }
                Console.WriteLine("*************************************************************************");
            }
        }

        public static string ObjectToString(Object obj)
        {
            string str = "";
            //Get the type of the object!
            Type t = obj.GetType();
            str += $"Object Type: {t}\n";
            // Get the public properties of the instance (not only related to Object).
            PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // Display information for all properties.
            foreach (PropertyInfo propInfo in propInfos)
            {
                bool readable = propInfo.CanRead;
                bool writable = propInfo.CanWrite;
                //check if readable
                if (readable)
                {
                    //check if type of property is not a class!
                    Object prop = propInfo.GetValue(obj);
                    if (!(prop.GetType().IsClass && !prop.GetType().Equals(typeof(string))))
                    {
                        str += $"Property name: {propInfo.Name} ";
                        str += $"value: {prop}\n";
                    }
                }
            }
            return str;
        }

        public static string ObjectToString(Object obj, int levels)
        {
            string str = "";
            //Get the type of the object!
            Type t = obj.GetType();
            str += $"Object Type: {t}\n";
            // Get the public properties of the instance (not only related to Object).
            PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // Display information for all properties.
            foreach (PropertyInfo propInfo in propInfos)
            {
                bool readable = propInfo.CanRead;
                bool writable = propInfo.CanWrite;
                //check if readable
                if (readable)
                {
                    //check if type of property is not a class!
                    Object prop = propInfo.GetValue(obj);
                    if (!(prop.GetType().IsClass && !prop.GetType().Equals(typeof(string))))
                    {
                        str += $"Property name: {propInfo.Name} ";
                        str += $"value: {prop}\n";
                    }
                    else //if this is a class lets show the toString of the class
                    {
                        if (levels > 0)
                        {
                            str += $"Property name: {propInfo.Name} (Class - Description below)\n";
                            str += $"{ObjectToString(prop, levels - 1)}\n";
                        }
                    }
                }
            }
            return str;
        }

        public static void DemoReflectionToString()
        {
            SoccerPlayer sc2 = new SoccerPlayer()
            {
                BirthYear = 1980,
                Name = "Kuku"
            };
            sc2.AddGoal();
            Console.WriteLine(ObjectToString(sc2));
        }
    }

    class Rectangle1
    {
        private int width, length;
        public Rectangle1(int w, int l)
        {
            this.width = w;
            this.length = l;
        }
        public int GetWidth() { return this.width; }
        public int GetLength() { return this.length; }
        public override string ToString()
        {
            return String.Format("Rectangle width={0}, length={1}", width, length);
        }
    }

    class GenericMthods
    {
        static bool IntSearch(int[] arr, int item)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == item)
                    return true;
            }
            return false;
        }
        
        static bool GenericSearch<T>(T[] arr, T item)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Equals(item))
                    return true;
            }
            return false;
        }

        static void Generic<T, K>(T t, K k)
        {
            Console.WriteLine(t);
            Console.WriteLine(k);
        }

    }


    class Rectangle2
    {
        private int width, length;

        public Rectangle2(int w, int l)
        {
            this.width = w;
            this.length = l;
        }

        public int Width
        {
            get
            {

                return this.width;
            }
            set
            {
                this.width = value;
            }
        }

        public int Area
        {
            get
            {
                return this.width * this.length;
            }
        }

        public override string ToString()
        {
            return String.Format("Rectangle width={0}, length={1}", width, length);
        }
    }

    class Rectangle3
    {

        public Rectangle3(int w, int l)
        {
            this.Width = w;
            this.Length = l;
        }
        public int Width
        {
            get; private set;
        }

        public int Length { get; set; }

        public override string ToString()
        {
            return String.Format("Rectangle width={0}, length={1}", Width, Length);
        }
    }

    class RoomTempReader
    {
        private const int CURRENT_TEMP = 20;
        private int temp;
        public RoomTempReader() 
        {
            this.temp = CURRENT_TEMP; //This temperature is initated from the electronic device that return a temp in celsius!
        }
        #region Encapsulation
        public int GetTemp() 
        {
            return this.temp / 3;
        }
        #endregion
    }

    class AirconditionController
    {
        private int targetTemp; //in celsius
        public AirconditionController(int targetTemp)
        {
            this.targetTemp = targetTemp;
            RoomTempReader reader = new RoomTempReader();
            if (reader.GetTemp() < targetTemp)
            {
                StopCooling();
            }
            else
            {
                StartCooling();
            }
        }

        public void StartCooling() { }
        public void StopCooling() { }
    }


    class GenericSearchArray<T>
    {
        private int arrNumOfElements;
        private T[] arr;
        public GenericSearchArray(int arrSize) 
        {
            this.arr = new T[arrSize];
            this.arrNumOfElements = 0;
        }

        public void Add(T item) 
        {
            if (this.arrNumOfElements < this.arr.Length)
            {
                this.arr[this.arrNumOfElements++] = item;
            }
        }

        public bool Search(T item)
        {
            for (int i = 0; i < this.arrNumOfElements; i++)
            {
                if (this.arr[i].Equals(item))
                    return true;
            }
            return false;
        }
    }

    class IntSearchArray
    {
        private int arrNumOfElements;
        private int[] arr;
        public IntSearchArray(int arrSize)
        {
            this.arr = new int[arrSize];
            this.arrNumOfElements = 0;
        }

        public void Add(int item)
        {
            if (this.arrNumOfElements < this.arr.Length)
            {
                this.arr[this.arrNumOfElements++] = item;
            }
        }

        public bool Search(int item)
        {
            for (int i = 0; i < this.arrNumOfElements; i++)
            {
                if (this.arr[i].Equals(item))
                    return true;
            }
            return false;
        }

    }

    #endregion

    #region Collections
    class Rectangle
    {
        public Rectangle(int w, int l)
        {
            this.Width = w;
            this.Length = l;
        }

        public int Width { get; set; }
        public int Length { get; set; }
        public override string ToString()
        {
            return String.Format("Rectangle width={0}, length={1}", Width, Length);
        }
    }
    class CollectionsDemo
    {
        public static void ListDemo()
        {
            //Define a list of rectangles
            List<Rectangle> rectangles = new List<Rectangle>();
            Rectangle r1 = new Rectangle(1, 3);
            Rectangle r2 = new Rectangle(2, 3);
            Rectangle r3 = new Rectangle(3, 3);
            Rectangle r4 = new Rectangle(4, 3);
            Rectangle r5 = new Rectangle(5, 3);

            //Add rectangles to the list
            rectangles.Add(r1);
            rectangles.Add(r2);
            rectangles.Add(r3);
            rectangles.Add(r4);
            rectangles.Add(r5);

            //loop through the list using count and the [] operator

            Console.WriteLine("loop through the list using count and the [] operator");
            for (int i = 0; i < rectangles.Count; i++)
                Console.WriteLine(rectangles[i]);

            Console.WriteLine("Loop through the list using foreach");
            //Loop through the list using foreaach
            foreach (Rectangle r in rectangles)
                Console.WriteLine(r);

            //Delete from a list
            rectangles.Remove(r5);
            rectangles.RemoveAt(0);
            Console.WriteLine("after delete r5 and index 0");
            foreach (Rectangle r in rectangles)
                Console.WriteLine(r);


            //Update an object in a list
            rectangles[0] = r5;
            Console.WriteLine("after placing r5 in index 0");
            foreach (Rectangle r in rectangles)
                Console.WriteLine(r);

            //Copy to array
            Rectangle[] recArr = rectangles.ToArray();
            List<Rectangle> list = rectangles.ToList();

            Console.WriteLine("Copied list");
            foreach (Rectangle r in list)
                Console.WriteLine(r);

            Console.WriteLine("Array");
            foreach (Rectangle r in recArr)
                Console.WriteLine(r);

            //Monkeys Excersize 1,2,3,6,10
        }

        public static void DictionaryDemo()
        {
            //Define a dictinary of names and rectangles
            Dictionary<string, Rectangle> rectangles = new Dictionary<string, Rectangle>();
            Rectangle r1 = new Rectangle(1, 3);
            Rectangle r2 = new Rectangle(2, 3);
            Rectangle r3 = new Rectangle(3, 3);
            Rectangle r4 = new Rectangle(4, 3);
            Rectangle r5 = new Rectangle(5, 3);

            //Add rectangles to the list
            rectangles.Add("r1", r1);
            rectangles.Add("r2", r2);
            rectangles.Add("r3", r3);
            rectangles.Add("r4", r4);
            rectangles.Add("r5", r5);

            //loop through the dictionary using foreach and AsEnumerable()
            Console.WriteLine("loop through the list using count and the [] operator");
            foreach (KeyValuePair<string, Rectangle> r in rectangles.AsEnumerable())
            {
                Console.WriteLine("{0}:{1}", r.Key, r.Value);
            }

            //Direct find operation based on key!
            Rectangle myR = rectangles["r2"];
            Console.WriteLine("after searching r2:{0}", myR);

            //Delete from a list
            rectangles.Remove("r5");
            Console.WriteLine("after delete r5");
            foreach (KeyValuePair<string, Rectangle> r in rectangles.AsEnumerable())
            {
                Console.WriteLine("{0}:{1}", r.Key, r.Value);
            }

            //Update an object in a list
            rectangles["r1"] = r5;
            Console.WriteLine("after placing r5 in key r1");
            foreach (KeyValuePair<string, Rectangle> r in rectangles.AsEnumerable())
            {
                Console.WriteLine("{0}:{1}", r.Key, r.Value);
            }
        }

    }

    #endregion 


    internal class Program
    {
        

        static void Main(string[] args)
        {
        }
        
        static void Demo1()
        {
            TimePeriod.Play();

            Rectangle r1 = new Rectangle(1, 3);
            Rectangle r2 = new Rectangle(2, 3);
            Rectangle r3 = new Rectangle(3, 3);
            Rectangle r4 = new Rectangle(4, 3);
            Rectangle r5 = new Rectangle(5, 3);

            //Create a list of rectangles
            List<Rectangle> rectangles = new List<Rectangle>();

            //Add items to the list
            rectangles.Add(r1);
            rectangles.Add(r2);
            rectangles.Add(r3);
            rectangles.Add(r4);
            rectangles.Add(r5);

            //Scan a list using Count property and [] operator
            for (int i = 0; i < rectangles.Count; i++)
            {
                Console.WriteLine(rectangles[i]);
            }

            //Scan a list using foreach
            foreach (Rectangle r in rectangles)
            {
                Console.WriteLine(r);
            }

            //Remove items from list
            Console.WriteLine("After Removing r2");
            rectangles.Remove(r2);
            foreach (Rectangle r in rectangles)
            {
                Console.WriteLine(r);
            }

            rectangles.RemoveAt(2);
            Console.WriteLine("After Removing item number 2");
            foreach (Rectangle r in rectangles)
            {
                Console.WriteLine(r);
            }

            //Update item in list
            rectangles[2] = r2;
            Console.WriteLine("After updating item number 2 to be r2");
            foreach (Rectangle r in rectangles)
            {
                Console.WriteLine(r);
            }

            //Array to List and List To Array
            Rectangle[] rArr = rectangles.ToArray();
            Console.WriteLine("The list:");
            foreach (Rectangle r in rectangles)
            {
                Console.WriteLine(r);
            }
            Console.WriteLine("The Array:");
            foreach (Rectangle r in rArr)
            {
                Console.WriteLine(r);
            }

            r1.Length = 99;
            r1.Width = 99;

            Console.WriteLine("The list:");
            foreach (Rectangle r in rectangles)
            {
                Console.WriteLine(r);
            }
            Console.WriteLine("The Array:");
            foreach (Rectangle r in rArr)
            {
                Console.WriteLine(r);
            }

            //Monkeys Excersize 1,2,3,6,10

        }
        static List<Rectangle> GetAllBigRectangles(List<Rectangle> list, int minArea)
        {
            List<Rectangle> result = new List<Rectangle>();
            foreach (Rectangle r in list) 
            {
                if(r.Width*r.Length > minArea) 
                {
                    result.Add(r);
                }
            }
            return result;
        }

        static bool SearchRectangle(List<Rectangle> list, int width, int length)
        {
            foreach(Rectangle r in list) 
            {
                if (r.Width == width && r.Length == length)
                    return true;
            }
            return false;
        }

    }
}