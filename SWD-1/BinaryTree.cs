using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD_1
{
    // Класс для описания элемента бинарного дерева
    public class BinaryTree
    {
        // Содержимое элемента, описывает какой-либо идентификатор
        IDent Descriptor;
        // Ссылки на левое и правое поддеревья
        BinaryTree Left, Right;

        // Конструктор класса
        public BinaryTree(IDent Desc)
        {
            Descriptor = Desc;
            Left = null;
            Right = null;
        }

    // Добавление элемента в дерево
    public BinaryTree Add(BinaryTree NewElement)
        {
            // По сравнению хеш-функций определяем, в какую часть поддерева добавить элемент
            if (NewElement.Descriptor.GetHash <= this.Descriptor.GetHash)
                // Если есть поддерево, продолжаем искать место
                if (this.Left != null)
                    this.Left.Add(NewElement);
                else
                    this.Left = NewElement;
            else
                if (this.Right != null)
                    this.Right.Add(NewElement);
                else
                    this.Right = NewElement;
           return this;
        }
    }
}