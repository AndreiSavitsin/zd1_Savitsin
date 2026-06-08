using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd1_Savitsin
{
    public class Cat
    {
        private string name; //скрытое поле имя
        private double weight; //скрытое поле вес

        public static List<Cat> CatList = new List<Cat>(); //Лист для котов

        public string Name //свойство для имени
        {
            get //получение значения
            {
                return name;
            }
            set //Установка значения - используем проверку
            {
                bool OnlyLetters = true;

                //ключ. слово value - это то, что хотят свойству присвоить
                foreach (var ch in value)
                {
                    if (!char.IsLetter(ch))
                    {
                        OnlyLetters = false;
                    }
                }

                if (OnlyLetters)
                {
                    name = value;
                }
                else
                {
                    Console.WriteLine($"{value} - неправильное имя!");
                }
            }
        }
        public double Weight //свойство для веса
        {
            get //получаем значение
            {
                return weight;
            }
            set //устанавливаем значение - используем проверку
            {
                if (value <= 0)
                {
                    Console.WriteLine("Вес не может быть <= 0");
                }
                else if (value > 20)
                {
                    Console.WriteLine("Вес не может быть > 20");
                }
                else
                {
                    weight = value;
                }
            }
        }

        public Cat(string CatName, double CatWeight)
        {
            Name = CatName;
            Weight = CatWeight;
        }

        public void Meow() //Кот мяукает
        {
            Console.WriteLine($"{name}: МЯУ!");
        }

        public static void AddCat(Cat cat) //Добавление кота в лис
        {
            CatList.Add(cat);
        }
        public static void ShowCats() //Вывод всех котов из листа
        {
            foreach (var item in CatList)
            {
                Console.WriteLine($"Кот: {item.Name}. Вес: {item.Weight}");
            }
        }
    }
}
