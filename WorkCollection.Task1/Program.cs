using System.Diagnostics;

namespace WorkCollection.Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<long> listTimeLinkedList = new List<long>();
            List<long> listTimeList = new List<long>();

            var files=ReadFiles(Path.Combine("Data", "Text1.txt"));

            Console.WriteLine($"Средняя время заполнения List:  {Estimate<string>(1000, files, ReadToList)}");

            Console.WriteLine($"Средняя время заполнения LinkedList:  {Estimate<string>(1000, files, ReadToLinkedList)}");

            Console.ReadLine();

        }

        static double Estimate<T>(int n,IEnumerable<T> files,Func<IEnumerable<T>,ICollection<T>> func)
        {
            var list = new List<long>(n);
            var timer = new Stopwatch();
            timer.Start();

            for (int i = 0; i < n; i++)
            {
                timer.Restart();

                func(files);

                timer.Stop();
                list.Add(timer.ElapsedMilliseconds);
            }
            return list.Average();
        }

        private static List<T> ReadToList<T>(IEnumerable<T> values)
        {
            var list = new List<T>();
            foreach (T value in values)
            {
                list.Add(value);
            }
            return list;
        }

        private static LinkedList<T> ReadToLinkedList<T>(IEnumerable<T> values)
        {
            var list = new LinkedList<T>();
            foreach (T value in values)
            {
                list.AddLast(value);
            }
            return list;
        }


        /// <summary>Читаем из файла и записываем в лист</summary>
        /// <param name="path">путь к файлу</param>
        /// <returns></returns>
        private static IEnumerable<string?> ReadFiles(string path)
        {
            var list = new List<string?>();

            foreach (string? line in GetReadFiles(path))
                list.Add(line);

            return list;
        }

        /// <summary>Метод для чтения файла построчно</summary>
        /// <param name="path">Путь к файлу</param>
        private static IEnumerable<string?> GetReadFiles(string path)
        {
            using (StreamReader read = new StreamReader(path))
                while (!read.EndOfStream)
                    yield return read.ReadLine();
            yield break;
        }
    }
}