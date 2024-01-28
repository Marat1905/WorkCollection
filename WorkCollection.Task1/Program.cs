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

        /// <summary>Метод для подсчета среднего выполнения работы</summary>
        /// <typeparam name="T">Обобщение</typeparam>
        /// <param name="count">Количество итераций</param>
        /// <param name="files">Источник</param>
        /// <param name="func">Делегат куда записываем</param>
        /// <returns>Среднее значение</returns>
        static double Estimate<T>(int count,IEnumerable<T> files,Func<IEnumerable<T>,ICollection<T>> func)
        {
            var list = new List<long>(count);
            var timer = new Stopwatch();
            timer.Start();

            for (int i = 0; i < count; i++)
            {
                timer.Restart();

                func(files);

                timer.Stop();
                list.Add(timer.ElapsedMilliseconds);
            }
            return list.Average();
        }

        /// <summary>Записываем в List</summary>
        /// <typeparam name="T">Обобщение</typeparam>
        /// <param name="files">Источник</param>
        /// <returns>Возвращаем List</returns>
        private static List<T> ReadToList<T>(IEnumerable<T> files)
        {
            var list = new List<T>();
            foreach (T value in files)
            {
                list.Add(value);
            }
            return list;
        }

        /// <summary>Записываем в LinkedList</summary>
        /// <typeparam name="T">Обобщение</typeparam>
        /// <param name="files">Источник</param>
        /// <returns>Возвращаем LinkedList</returns>
        private static LinkedList<T> ReadToLinkedList<T>(IEnumerable<T> files)
        {
            var list = new LinkedList<T>();
            foreach (T value in files)
            {
                list.AddLast(value);
            }
            return list;
        }

    }
}