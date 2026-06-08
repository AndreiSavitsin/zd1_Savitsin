using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd1_task1_2_Savitsin
{
    class Product
    {
        public decimal Price { get; set; } //Цена

        public string Name { get; set; } //Название

        public Product(string Name, decimal Price) //Конструктор класса продукт
        {
            this.Name = Name;
            this.Price = Price;
        }

        public Product() { } //Конструктор класса продукт

        public string GetInfo() //Информация о товаре
        {
            return $"Наименование: {Name}. Цена: {Price}";
        }
    }
}
