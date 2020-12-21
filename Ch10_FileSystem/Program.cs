using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.IO.Compression;

namespace Ch10_FileSystem
{
    class Program
    {
        static void directoryTutorial()
        {
            string dir = @"C:/Code/Ch10.txt";
            Console.WriteLine($"Does {dir} exist? {Directory.Exists(dir)}"); //directory root가 존재하는가?
            Directory.CreateDirectory(dir); //directory 생성
            Console.WriteLine($"Does {dir} exist? {Directory.Exists(dir)}"); //directory root가 존재하는가?
            Directory.Delete(dir); // 삭제
            Console.WriteLine($"Does {dir} exist? {Directory.Exists(dir)}"); //directory root가 존재하는가?
        }

        static void fileSystemTutorial()
        {
            string textFile = @"C:/Code/Ch10.txt";
            string backupFile = @"C:/Code/Ch10.bak";

            Console.WriteLine($"Does {textFile} exist? {File.Exists(textFile)}"); //directory root가 존재하는가?

            StreamWriter textWriter = File.CreateText(textFile);
            textWriter.WriteLine("Hello, C#?"); //txt 파일에 한줄 string 입력
            textWriter.Dispose(); // same textReader.Close(); 리소스 해제
            Console.WriteLine($"Does {textFile} exist? {File.Exists(textFile)}"); //directory root가 존재하는가?

            File.Copy(textFile, backupFile, true); //copy the file (true value is copy & overwrite
            Console.WriteLine($"Does {backupFile} exist? {File.Exists(backupFile)}");

            File.Delete(textFile); //삭제
            Console.WriteLine($"Does {textFile} exist? {File.Exists(textFile)}");

            StreamReader textReader = File.OpenText(backupFile); //open text file
            Console.WriteLine(textReader.ReadToEnd()); //처음부터 끝까지 한번에 다 읽기
            textReader.Dispose(); // same textReader.Close();


        }

        static void filePathTutorial()
        {
            string textFile = @"C:/Code/Ch10.txt";

            Console.WriteLine($"File Name: {Path.GetFileName(textFile)}");
            Console.WriteLine($"File Name without Extension:{ Path.GetFileNameWithoutExtension(textFile)}");
            Console.WriteLine($"File Extension: {Path.GetExtension(textFile)}");
            Console.WriteLine($"Random File Name: {Path.GetRandomFileName()}");
            Console.WriteLine($"Temporary File Name: {Path.GetTempFileName()}");
        }

        static void showFileInfoTutorial()
        {
            string backup = @"C:\Code\Ch10.bak"; // 윈도우 
            var info = new FileInfo(backup);
            Console.WriteLine($"{backup} contains {info.Length} bytes.");
            Console.WriteLine($"{backup} was last accessed {info.LastAccessTime}.");
            Console.WriteLine($"{backup} has readonly set to {info.IsReadOnly}.");
        }

        static void xmlFileTutorial()
        {
            // string 배열을 정의한다.
            string[] callsigns = new string[] { "Husker", "Starbuck", "Apollo", "Boomer", "Bulldog", "Athena", "Helo", "Racetrack" };

            // 텍스트 쓰기 헬퍼를 사용하여 쓸 파일을 정의한다.
            string textFile = @"C:\Code\Ch10_Streams.txt"; // 윈도우 
            StreamWriter text = File.CreateText(textFile);

            // string 배열의 각 항목을 스트림에 쓴다.
            foreach (string item in callsigns)
            {
                text.WriteLine(item);
            }
            text.Dispose(); // 스트림을 닫는다. 

            // 파일의 내용을 콘솔에 출력한다.
            Console.WriteLine($"{textFile} contains {new FileInfo(textFile).Length} bytes.");
            Console.WriteLine(File.ReadAllText(textFile));

            // XML 쓰기 헬퍼를 사용하여 쓸 파일을 정의한다.
            string xmlFile = @"C:\Code\Ch10_Streams.xml";

            FileStream xmlFileStream = File.Create(xmlFile);
            XmlWriter xml = XmlWriter.Create(xmlFileStream, new XmlWriterSettings { Indent = true });

            // 파일에 XML 선언부를 쓴다.
            xml.WriteStartDocument();

            // root 엘리먼트를 쓴다.
            xml.WriteStartElement("callsigns");

            // string 배열의 각 항목을 스트림에 쓴다.
            foreach (string item in callsigns)
            {
                xml.WriteElementString("callsign", item);
            }

            // root 엘리먼트를 닫는다.
            xml.WriteEndElement();
            xml.Dispose();
            xmlFileStream.Dispose();

            // 파일의 내용을 콘솔에 출력한다.
            Console.WriteLine($"{xmlFile} contains {new FileInfo(xmlFile).Length} bytes.");
            Console.WriteLine(File.ReadAllText(xmlFile));


            // XML 출력을 압축한다.
            string gzipFilePath = @"C:\Code\Ch10.gzip"; // 윈도우 

            FileStream gzipFile = File.Create(gzipFilePath);
            GZipStream compressor = new GZipStream(gzipFile, CompressionMode.Compress);

            XmlWriter xmlGzip = XmlWriter.Create(compressor);
            xmlGzip.WriteStartDocument();
            xmlGzip.WriteStartElement("callsigns");
            foreach (string item in callsigns)
            {
                xmlGzip.WriteElementString("callsign", item);
            }
            xmlGzip.Dispose();
            compressor.Dispose(); // also closes the underlying stream 

            // 압축 파일의 내용을 콘솔에 출력한다.
            Console.WriteLine($"{gzipFilePath} contains {new FileInfo(gzipFilePath).Length} bytes.");
            Console.WriteLine(File.ReadAllText(gzipFilePath));

            // 압축 파일을 읽는다. 
            Console.WriteLine("Reading the compressed XML file:");
            gzipFile = File.Open(gzipFilePath, FileMode.Open);
            GZipStream decompressor = new GZipStream(gzipFile, CompressionMode.Decompress);
            XmlReader reader = XmlReader.Create(decompressor);
            while (reader.Read())
            {
                // callsign 엘리먼트인지 확인한다.
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "callsign"))
                {
                    reader.Read(); // 엘리먼트 안의 텍스트 노드로 이동한다. 
                    Console.WriteLine($"{reader.Value}"); // 값을 읽는다. 
                }
            }
            reader.Dispose();
            decompressor.Dispose();
        }

        static void Main(string[] args)
        {

        }
    }
}
