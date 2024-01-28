using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace WorkCollection.Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();

            foreach (string line in GetReadFiles(Path.Combine("Data","Text1.txt")))
            {
                var noPunctuationText = new string(line.Where(c => !char.IsPunctuation(c)).ToArray());

                string[] words = noPunctuationText.Split(' ');
                
                foreach (string word in words)
                {
                    if(string.IsNullOrWhiteSpace(word)) 
                        continue;
                    
                    if (map.ContainsKey(word))
                        map[word] += 1;
                    else
                       map.Add(word, 1);
                }
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
    }
}