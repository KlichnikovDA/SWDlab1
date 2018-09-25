using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace SWD_1
{
    public static class ReadID
    {
        // Метод для считывания идентификаторов из файла и записи их в бинарное дерево
        public static BinaryTree ParseFile(string Path)
        {
            StreamReader sr = new StreamReader(Path);
            
            // Корень бинарного дерева, считывание первой строки файла 
            BinaryTree Res = new BinaryTree(ParseString(sr.ReadLine()));
            // Считывание остальных строк файла
            while (!sr.EndOfStream)
            {     
                // Запись считанных строк в бинарное дерево
                Res.Add(new BinaryTree(ParseString(sr.ReadLine())));
            }

            return Res;
        }

        // Метод для парсинга идентификатора из строки
        static IDent ParseString(string InputString)
        {
            MatchCollection Matches;
            // Проверка на соответствие объявлению константы
            if (Regex.IsMatch(InputString, @"^const .+;$"))
            {
                Matches = Regex.Matches(InputString, @"^const (?<type>\w+) (?<name>\w+) = (?<value>.+);$");
                return new IDConst(Matches[0].Groups["name"].Value, Matches[0].Groups["type"].Value, Matches[0].Groups["value"].Value);
            }
            // Проверка на соответствие объявлению метода
            if (Regex.IsMatch(InputString, @"^\w+ \w+\(.*\);$"))
            {
                Matches = Regex.Matches(InputString, @"^(?<type>\w*) (?<name>\w*)\((?<params>.*)\);$");
                return new IDMethod(Matches[0].Groups["name"].Value, Matches[0].Groups["type"].Value, Matches[0].Groups["params"].Value);
            }
            // Проверка на соответствие объявлению класса
            if (Regex.IsMatch(InputString, @"^class .+;$"))
            {
                Matches = Regex.Matches(InputString, @"^class (?<name>\w+);$");
                return new IDClass(Matches[0].Groups["name"].Value, "class");
            }
            // Соответствие объявлению переменной
            Matches = Regex.Matches(InputString, @"^(?<type>\w+) (?<name>\w+);$");
            return new IDVar(Matches[0].Groups["name"].Value, Matches[0].Groups["type"].Value);
        }
    }
}
