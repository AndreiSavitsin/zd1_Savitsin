using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd1_Savitsin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество котов: ");
            if (!int.TryParse(Console.ReadLine(), out int CountCat))
            {
                Console.WriteLine("Количество котов должно быть целым числов");
            }
            else
            {
                if (CountCat <= 0)
                {
                    Console.WriteLine("Количество котов должно быть > 0");
                }
                else
                {
                    for (int i = 0; i < CountCat; i++)
                    {
                        Console.Write("Введите имя для кота: ");
                        string name = Console.ReadLine();
                        Console.Write("Введите вес кота: ");
                        if (double.TryParse(Console.ReadLine(), out double weight))
                        {
                            Cat cat = new Cat(name, weight);
                            cat.Meow();
                            Cat.AddCat(cat);
                        }
                        else
                        {
                            Console.WriteLine("Вес должен быть числом");
                        }
                    }
                }

                Cat.ShowCats();
            }
           

            Console.ReadKey();
        }
    }
}
