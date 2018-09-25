using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SWD_1
{
    // Базовый класс для описания идентификаторов
    public abstract class IDent
    {
        // Перечисление возможных способов использования идентификатора
        protected enum Classes { Classes, Consts, Vars, Methods };
        // Перечисление возможных типов идентификатора
        protected enum Types { int_type, float_type, string_type, bool_type, char_type, class_type };

        // Наименование идентификатора
        protected string IDName;
        // Способ использования идентификатора
        protected Classes IDClass;
        // Тип идентификатора
        protected Types IDType;


        // Свойство для получения хеш-функции
        public int GetHash
        {
            get { return IDName.GetHashCode(); }
        }

        // Конструктор
        public IDent(string Name, string Type)
        {
            IDName = Name;
            switch (Type)
            {
                case ("int"):
                    IDType = Types.int_type;
                    break;
                case ("float"):
                    IDType = Types.float_type;
                    break;
                case ("string"):
                    IDType = Types.string_type;
                    break;
                case ("bool"):
                    IDType = Types.bool_type;
                    break;
                case ("char"):
                    IDType = Types.char_type;
                    break;
                case ("class"):
                    IDType = Types.class_type;
                    break;
            }
        }
    }

    // Класс для описания переменной
    public class IDVar : IDent
    {
        public IDVar(string Name, string Type): base (Name, Type)
        {
            IDClass = Classes.Vars;
        }
    }

    // Класс для описания константы
    public class IDConst: IDent
    {
        // Значение константы
        object Value;

        public IDConst(string Name, string Type, object Val): base (Name, Type)
        {
            IDClass = Classes.Consts;
            Value = Val;
        }
    }

    // Класс для описания классов
    public class IDClass: IDent
    {
        public IDClass(string Name, string Type): base (Name, Type)
        {
            IDClass = Classes.Classes;
        }
    }

    // Класс для описания методов
    public class IDMethod: IDent
    {
        // Класс для описания параметров метода
        class Param
        {
            // Тип параметра
            Types type;
            // Перечисление возможных модификаторов параметров
            enum Mods { param_val, param_ref, param_out };
            // Модификатор параметра
            Mods mod;

            public Param(string Type, string Mod)
            {
                switch (Type)
                {
                    case ("int"):
                        type = Types.int_type;
                        break;
                    case ("float"):
                        type = Types.float_type;
                        break;
                    case ("string"):
                        type = Types.string_type;
                        break;
                    case ("bool"):
                        type = Types.bool_type;
                        break;
                    case ("char"):
                        type = Types.char_type;
                        break;
                    case ("class"):
                        type = Types.class_type;
                        break;
                }
                switch (Mod)
                {
                    case ("ref"):
                        mod = Mods.param_ref;
                        break;
                    case ("out"):
                        mod = Mods.param_out;
                        break;
                    case (""):
                        mod = Mods.param_val;
                        break;
                }
            }
        }

        // Список параметров метода
        class ParamList
        {
            Param Info;
            public ParamList Next { get; set; }

            public ParamList(Param Data)
            {
                Info = Data;
                Next = null;
            }
        }

        ParamList Arguments;

        public IDMethod(string Name, string Type, string Params): base (Name, Type)
        {
            IDClass = Classes.Methods;
            if (Params == "")
                Arguments = null;
            else
                Arguments = ParseParams(Params);
        }

        ParamList ParseParams(string Params)
        {            
            // Поиск во входной строке всех параметров
            MatchCollection Matches = Regex.Matches(Params, @"(?<mod>ref|out|w{0})\s*(?<type>\w+) (?<name>\w+)(\,|$)");
            // Если список параметров пустой
            if (Matches.Count == 0)
                return null;

            // Если список не пуст
            // Первый элемент в списке параметров
            ParamList Root;
            Root = new ParamList(new Param(Matches[0].Groups["type"].Value, Matches[0].Groups["mod"].Value));
            // Вспомогательная переменная для прохода по списку
            ParamList P = Root;
            for (int i = 1; i < Matches.Count; i++)
            {
                ParamList R = new ParamList(new Param(Matches[i].Groups["type"].Value, Matches[i].Groups["mod"].Value));
                P.Next = R;
                P = R;
            }

            return Root;
        }
    }
}
