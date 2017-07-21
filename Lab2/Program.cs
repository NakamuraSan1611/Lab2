using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2
{
    partial class Program
    {
        static bool checkIsCyrillic(string name)
        {
            Regex regexCyrillic = new Regex(@"\p{IsCyrillic}+");  // Cyrillic letters
            bool check = false;
            foreach (var item in name.ToCharArray())
                if (regexCyrillic.IsMatch(item.ToString()))
                    check = true;
            return check;
        }
        
        static void Main(string[] args)
        {
            Dictionary<int, string> files = new Dictionary<int, string>();
            Console.WriteLine("Введи путь");
            string path = Console.ReadLine();
            string[] temp = Directory.GetFiles(path);
            int size = 0;
            foreach (var item in temp)
                files.Add(size++, item);
            List<string> types = new List<string>();
            foreach (var item in files)
                if (!types.Contains(Path.GetExtension(item.Value)))
                    types.Add(Path.GetExtension(item.Value));
            Console.WriteLine("Нашел файлов {0} их расширения:", files.Count);

            foreach (var item in types)
                Console.Write(item);

            Console.WriteLine("\nВыбери расширения файлов без точки! (для продолжения введи команду go)");
            List<string> typesToWork = new List<string>();
            string choice = "";
            while (choice != "go")
            {
                choice = Console.ReadLine();
                typesToWork.Add(choice);
            }
            typesToWork.Remove("go");
            Dictionary<int, string> names = new Dictionary<int, string>();
            foreach (var item in files)
                names.Add(item.Key, item.Value.Substring(item.Value.LastIndexOf('\\') + 1, item.Value.Length - item.Value.LastIndexOf('\\') - 1));
            Dictionary<int, string> namesToWork = new Dictionary<int, string>();
            foreach (var item in names)
                foreach (var type in typesToWork)
                    if (item.Value.Contains(type))
                        namesToWork.Add(item.Key, item.Value.Substring(0, item.Value.LastIndexOf('.')));
            List<char> chs = new List<char>();
            foreach (var item in namesToWork)
                foreach (var ch in item.Value.ToCharArray())
                    if (!(char.IsLetterOrDigit(ch) || checkIsCyrillic(item.Value)))
                        if (!chs.Contains(ch))
                            chs.Add(ch);
            Console.WriteLine("нашел символы ");
            foreach (var item in chs)
                Console.Write("'{0}' ", item);
            Console.WriteLine();
            List<int> keys = new List<int>();
            foreach (var item in namesToWork)
                keys.Add(item.Key);
            foreach (var item in chs)
            {
                Console.WriteLine("На что заменить символ '{0}'", item);
                string newchar = Console.ReadLine();
                foreach (var key in keys)
                    if (!checkIsCyrillic(namesToWork[key]))
                        namesToWork[key] = namesToWork[key].Replace(item.ToString(), newchar).Replace(" ", "");
                    else
                        namesToWork[key] = namesToWork[key].Replace(item.ToString(), newchar);
            }
            foreach (var item in namesToWork)
            {
                string newfile = path + "\\" + item.Value + files[item.Key].Substring(files[item.Key].LastIndexOf('.'), files[item.Key].Length - files[item.Key].LastIndexOf('.'));
                File.Move(files[item.Key], newfile);
            }
            smoke();
            Console.ReadKey();
        }
    }
}
