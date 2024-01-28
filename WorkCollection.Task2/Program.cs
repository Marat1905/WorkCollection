using System.Collections.Generic;

namespace WorkCollection.Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();

            foreach (string line in GetReadFiles(Path.Combine("Data","Text1.txt")))
            {
                var arrayTexts = CreateArray(line);
                AddChangeDictionary(arrayTexts, map);
            }

            Console.WriteLine("10 слов часто встречаемые");

            foreach (KeyValuePair<string, int> p in map.OrderByDescending(x => x.Value).Take(10))
                Console.WriteLine($"{p.Key} = {p.Value} раз");

        }

        /// <summary>Метод для чтения файла построчно</summary>
        /// <param name="path">Путь к файлу</param>
        private static IEnumerable<string> GetReadFiles(string path)
        {
            using (StreamReader read = new StreamReader(path))
                while (!read.EndOfStream)
                {
                    var result= read.ReadLine();
                    if(!string.IsNullOrWhiteSpace(result))
                        yield return result;
                }
            yield break;
        }

        /// <summary>Метод для разделения строки на массив</summary>
        /// <param name="text">Входящий текст</param>
        /// <returns>Возрат массива строк</returns>
        static string[] CreateArray(string text)
        {
            var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());

            return  noPunctuationText.Split(' ');
        }

        /// <summary>Добавление в словарь или приращение value</summary>
        /// <param name="texts">Массив строк</param>
        /// <param name="dictionary">Словарь</param>
        static void AddChangeDictionary(string[] texts, Dictionary<string,int> dictionary)
        {
            foreach (string text in texts)
            {
                if (string.IsNullOrWhiteSpace(text))
                    continue;

                if (dictionary.ContainsKey(text))
                    dictionary[text] += 1;
                else
                    dictionary.Add(text, 1);
            }
        }
    }
}