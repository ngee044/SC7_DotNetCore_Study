using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using Ch10_FileSystem;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;


namespace Ch10_FileSystem
{
    public class ExEncoding
    {
        public static void xmlSerializeTutorial()
        {
            var people = new List<Person>
            {
              new Person(30000M) { FirstName = "Alice", LastName = "Smith",
                DateOfBirth = new DateTime(1974, 3, 14) },
              new Person(40000M) { FirstName = "Bob", LastName = "Jones",
                DateOfBirth = new DateTime(1969, 11, 23) },
              new Person(20000M) { FirstName = "Charlie", LastName = "Rose",
                DateOfBirth = new DateTime(1964, 5, 4),
                Children = new HashSet<Person>
                { new Person(0M) { FirstName = "Sally", LastName = "Rose",
                DateOfBirth = new DateTime(1990, 7, 12) } } }
            };

            // 쓰기용 파일을 생성한다. 
            //string xmlFilepath = @"/Users/markjprice/Code/Ch10_People.xml";
            string xmlFilepath = @"C:\Code\Ch10_People.xml"; // Windows 
            FileStream xmlStream = File.Create(xmlFilepath);

            // Person의 리스트를 XML로 형식화하는 객체를 생성한다.
            var xs = new XmlSerializer(typeof(List<Person>));

            // 스트림에 객체 그래프를 직렬화한다.
            xs.Serialize(xmlStream, people);

            // 파일 잠금을 해제하기 위해 스트림을 닫는다.
            xmlStream.Dispose();

            WriteLine($"Written {new FileInfo(xmlFilepath).Length} bytes of XML to { xmlFilepath}");
            WriteLine();

            // 직렬화된 객체 그래프를 출력한다.
            WriteLine(File.ReadAllText(xmlFilepath));

            
            //------------------역진렬화 시작~!
            FileStream xmlLoad = File.Open(xmlFilepath, FileMode.Open);
            // 직렬화 된 객체 그래플 person의 리스트로 역직렬화 한다.
            var loadedPeople = (List<Person>)xs.Deserialize(xmlLoad);
            foreach (var item in loadedPeople)
            {
                WriteLine($"{item.LastName} has {item.Children.Count} children.");
            }
            xmlLoad.Dispose();
        }

        public static void jsonSerializeTutorial()
        {
            var people = new List<Person>
            {
              new Person(30000M) { FirstName = "Alice", LastName = "Smith",
                DateOfBirth = new DateTime(1974, 3, 14) },
              new Person(40000M) { FirstName = "Bob", LastName = "Jones",
                DateOfBirth = new DateTime(1969, 11, 23) },
              new Person(20000M) { FirstName = "Charlie", LastName = "Rose",
                DateOfBirth = new DateTime(1964, 5, 4),
                Children = new HashSet<Person>
                { new Person(0M) { FirstName = "Sally", LastName = "Rose",
                DateOfBirth = new DateTime(1990, 7, 12) } } }
            };

            // 쓰기용 파일을 생성한다.
            //string jsonFilepath = @"/Users/markjprice/Code/Ch10_People.json";
            string jsonFilepath = @"C:\Code\Ch10_People.json"; // Windows 
            StreamWriter jsonStream = File.CreateText(jsonFilepath);

            // JSON으로 형식화하는 객체를 생성한다.
            var jss = new JsonSerializer();

            // 객체 그래프를 string에 직렬화한다.
            jss.Serialize(jsonStream, people);

            // 파일 잠금을 해제하기 위해 stream을 닫는다.
            jsonStream.Dispose();

            WriteLine();
            WriteLine($"Written {new FileInfo(jsonFilepath).Length} bytes of JSON to: { jsonFilepath}");

            // 직렬화 된 객체 그래프를 출력한다.
            WriteLine(File.ReadAllText(jsonFilepath));
        }

        static public void EncodingExample()
        {
            WriteLine("Encodings");
            WriteLine("[1] ASCII");
            WriteLine("[2] UTF-7");
            WriteLine("[3] UTF-8");
            WriteLine("[4] UTF-16 (Unicode)");
            WriteLine("[5] UTF-32");
            WriteLine("[any other key] Default");

            // 인코딩 방식을 선택한다. 
            Write("Press a number to choose an encoding: ");
            ConsoleKey number = ReadKey(false).Key;
            WriteLine();
            WriteLine();

            Encoding encoder;
            switch (number)
            {
                case ConsoleKey.D1:
                    encoder = Encoding.ASCII;
                    break;
                case ConsoleKey.D2:
                    encoder = Encoding.UTF7;
                    break;
                case ConsoleKey.D3:
                    encoder = Encoding.UTF8;
                    break;
                case ConsoleKey.D4:
                    encoder = Encoding.Unicode;
                    break;
                case ConsoleKey.D5:
                    encoder = Encoding.UTF32;
                    break;
                default:
                    encoder = Encoding.GetEncoding(0);
                    break;
            }

            // 인코딩 할 string을 정의한다.
            string message = "A pint of milk is ￡1.99";

            // string을 바이트 배열로 인코딩한다. 
            byte[] encoded = encoder.GetBytes(message);

            // 인코딩이 필요한 바이트 수를 확인한다.
            WriteLine($"{encoder.GetType().Name} uses {encoded.Length} bytes.");

            // 루프를 돌면서 각 바이트를 출력한다. 
            WriteLine($"Byte  Hex  Char");
            foreach (byte b in encoded)
            {
                WriteLine($"{b,4} {b.ToString("X"),4} {(char)b,5}");
            }

            // 인코딩 된 바이트 배열을 다시 디코딩하여 출력한다.
            string decoded = encoder.GetString(encoded);
            WriteLine(decoded);
        }
    }
}