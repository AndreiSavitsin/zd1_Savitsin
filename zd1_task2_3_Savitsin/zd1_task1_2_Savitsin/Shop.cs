using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace zd1_task1_2_Savitsin
{
    class Shop
    {
        public static Dictionary<Product, int> products; //Лист продуктов

        public Shop() //Конструктор класса магазин
        {
            products = new Dictionary<Product, int>();
        }

        public static Dictionary<Product, int> GetProducts() //Возвращает лист продуктов
        {
            return products;
        }

        public static void AddProduct(Product product, int count) //Добавление продукта в лист
        {
            Product existingProduct = FindByName(product.Name);

            if (existingProduct != null)
            {
                products[existingProduct] += count; // Если нашли по имени — плюсуем
            }
            else
            {
                products.Add(product, count); // Если нет — добавляем новый
            }
        }
        public static void CreateProduct(string name, decimal price, int count) //Создание продукта и добавление в лист продуктов
        {
            products.Add(new Product(name, price), count);
        }

        public static string WriteProduct(Product product) //Вывод продукта
        {
            // Проверяем, что объект продукта не равен null
            if (product == null)
            {
                return "Товар не задан ";
            }

            // Проверяем, есть ли этот продукт в списке
            if (products.ContainsKey(product))
            {
                return $"{product.GetInfo()}. Количество: {products[product]}";
            }
            else
            {
                return $"{product.GetInfo()}. Количество: 0 (нет на складе)";
            }
        }
        public static decimal Budjet { get; set; } //Прибыль магазина
        // Расчет при продаже (прибыль растет)
        public static void CalculateProfit(Product product, int count)
        {
            Budjet += product.Price * count * 2;
        }

        // Расчет с указанием типа операции (true - продажа, false - покупка)
        public static void CalculateProfit(Product product, int count, bool isSale)
        {
            if (isSale)
            {
                Budjet += product.Price * count;
            }
            else
            {
                Budjet -= product.Price * count;
            }
        }

        public static void BuyProduct(Product product, int count) //Расчёт прибыли магазина при покупке продукта
        {
            CalculateProfit(product, count, isSale: false);
            AddProduct(product, count);
        }
        public void Sell(Product product, int count) //Продажа товара
        {
            if (products.ContainsKey(product))
            {
                if (products[product] < count)
                {
                    MessageBox.Show("Нет в наличии!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    products[product] -= count;
                    CalculateProfit(product, count);

                    // Если стало 0 — полностью удаляем из словаря
                    if (products[product] <= 0)
                    {
                        products.Remove(product);
                    }
                }
            }
            else
            {
                MessageBox.Show("Товар не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void Sell(Shop shop,string ProductName, int count) //Перегрузка метода Sell
        {
            Product ToSell = FindByName(ProductName);
            if (ToSell != null)
            {
                shop.Sell(ToSell, count);
            }
            else
            {
                MessageBox.Show("Товар не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static Product FindByName(string name) //Поиск товара
        {
            foreach (var product in products.Keys)
            {
                if (product.Name == name)
                {
                    return product;
                }
            }
            return null;
        }
    }
}
