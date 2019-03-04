using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace FileSystemApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = string.Empty;

            Console.WriteLine("Выберите порядковый номер диска " + "в котором вы хотите хранить файл");

            DriveInfo[] drives = DriveInfo.GetDrives();
            for (int i = 0; i < drives.Length; i++)
            {
                if(drives[i].IsReady && drives[i].DriveType==DriveType.Fixed)
                Console.WriteLine($"{i}.{drives[i].Name}");
            }

            int position = int.Parse(Console.ReadLine());

            path += drives[position].Name;

            foreach (var directory in drives[position].RootDirectory.EnumerateDirectories())
            {
                Console.WriteLine(directory.Name);
            }


            Console.WriteLine("Напишите название новой директории, куда хотите" + " сохранить файл");

            path += Console.ReadLine();


            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Console.WriteLine("Напишите название файла");

            path += @"\" + Console.ReadLine();

            if (!File.Exists(path))
            {
                using (File.Create(path)) ;
            }


            string data = "Какой - то текст";

            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                byte[] bytesData = Encoding.UTF8.GetBytes(data);
                fileStream.Write(bytesData, 0, bytesData.Length);
            }


            using (var fileStream = File.OpenRead(path))
            {
                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, buffer.Length);

                string result = Encoding.UTF8.GetString(buffer);
            }

            using (var stream = new StreamReader(path)) //File.OpenText(path)  можно сделать запись из уже имеющегося File
            {
                string text = stream.ReadToEnd();
            }

            Console.ReadLine();
        }
    }
}
