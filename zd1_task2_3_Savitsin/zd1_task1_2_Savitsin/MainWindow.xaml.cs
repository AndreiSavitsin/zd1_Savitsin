using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace zd1_task1_2_Savitsin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtBudjet.Text = "Прибыль магазина: " + Shop.Budjet.ToString();
        }

        private void Task2(object sender, RoutedEventArgs e) //Кнопка Задание 2
        {
            PanelTask2.Visibility = Visibility.Visible;
            PanelTask3.Visibility = Visibility.Hidden;
        }

        private void Task3(object sender, RoutedEventArgs e) //Кнопка Задание 3
        {
            PanelTask2.Visibility = Visibility.Hidden;
            PanelTask3.Visibility = Visibility.Visible;
        }

        private void Exit(object sender, RoutedEventArgs e) //Кнопка Выход
        {
            this.Close();
        }

        private void btnAdd(object sender, RoutedEventArgs e) //Кнопка Купить товар
        {
            listResult.Items.Clear();
            foreach (char c in txtName.Text) //Проверка имени товара
            {
                if (!char.IsLetter(c))
                {
                    MessageBox.Show("Название продукта должно содержать только буквы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price)) //Проверка цены товара
            {
                MessageBox.Show("Цена за продукт должна быть числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (price <= 0)
                {
                    MessageBox.Show("Цена за продукт не может быть <= 0", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (!int.TryParse(txtCount.Text, out int count)) //Проверка количество товара
            {
                MessageBox.Show("Количество товара должно быть числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (count <= 0)
                {
                    MessageBox.Show("Количество товара не может быть <= 0", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            Product product = Shop.FindByName(txtName.Text);
            if (product == null)
            {
                product = new Product();
                product.Name = txtName.Text;
                product.Price = price;
            }

            Shop.BuyProduct(product, count);
            txtBudjet.Text = "Прибыль магазина: " + Shop.Budjet.ToString();
            ShowAll();
        }
        Shop shop = new Shop();
        private void btnClear(object sender, RoutedEventArgs e) //Кнопка очистить
        {
            listResult.Items.Clear();
        }

        private void btnShowAll(object sender, RoutedEventArgs e) //Кнопка показать все продукты
        {
            ShowAll();
        }
        private void ShowAll() //Отображение всех продуктов
        {
            listResult.Items.Clear();
            listResult.Items.Add("Список всех продуктов");

            foreach (KeyValuePair<Product, int> pair in Shop.GetProducts())
            {
                string productInfo = Shop.WriteProduct(pair.Key);
                listResult.Items.Add(productInfo);
            }
        }
        private void btnBuy(object sender, RoutedEventArgs e) //Кнопка продать товар
        {
            if (!int.TryParse(txtCount.Text, out int count)) //Проверка количество товара
            {
                MessageBox.Show("Количество товара должно быть числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (count <= 0)
                {
                    MessageBox.Show("Количество товара не может быть <= 0", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            Shop.Sell(shop, txtName.Text, count);
            txtBudjet.Text = "Прибыль магазина: " + Shop.Budjet.ToString();
            ShowAll();
        }

        private void btnSearch(object sender, RoutedEventArgs e) //Кнопка поиск товара
        {
            Product product = Shop.FindByName(txtName.Text);
            listResult.Items.Clear();
            listResult.Items.Add(Shop.WriteProduct(product));
        }

        private void btnAddAudio(object sender, RoutedEventArgs e) //Кнопка добавить аудиозапись
        {
            if (string.IsNullOrEmpty(txtAuthor.Text))
            {
                MessageBox.Show("Имя автора не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                MessageBox.Show("Название песни не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtFileName.Text))
            {
                MessageBox.Show("Путь к файлу с мелодией не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string error = playList.ContainsFileName(txtFileName.Text);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            PlayList.AddSong(txtAuthor.Text, txtTitle.Text, txtFileName.Text);
            ShowList();

            string fileName = txtFileName.Text;
            if (!txtFileName.Text.Contains(".txt"))
            {
                fileName = txtFileName.Text + ".txt";
            }

            using (StreamWriter sw = File.CreateText(fileName))
            {
                sw.WriteLine(txtAuthor.Text + " " + txtTitle.Text);
            }
        }
        PlayList playList = new PlayList();
        private void ShowList() //Отображение листа композиций в listBox
        {
            listPlayList.Items.Clear();
            foreach (var item in PlayList.GetList())
            {
                listPlayList.Items.Add(playList.GetInfo(item));
            }
        }

        private void btnNext(object sender, RoutedEventArgs e) //Кнопка перехода к следующей песне
        {
            playList.NextSong();
            ShowAudio(playList);
        }

        private void btnPrevious(object sender, RoutedEventArgs e) //Кнопка перехода к предыдущей песне
        {
            playList.PreviousSong();
            ShowAudio(playList);
        }

        private void btnIndex(object sender, RoutedEventArgs e) //Кнопка перехода по индексу
        {
            if (!int.TryParse(txtIndex.Text, out int index))
            {
                MessageBox.Show("Поле для ввода индекса не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (index >= 0 && index < PlayList.GetList().Count)
            {
                playList.IndexSong(index);
                ShowAudio(playList);
            }
        }
        private void ShowAudio(PlayList playList) //Выделить конкретную песню
        {
            listPlayList.SelectedIndex = playList.GetIndex();
        }
        private void btnStart(object sender, RoutedEventArgs e) //Кнопка перехода к началу
        {
            playList.IndexSong(0);
            ShowAudio(playList);
        }

        private void btnDelete(object sender, RoutedEventArgs e) //Кнопка удаления композиции
        {
            if (listPlayList.SelectedIndex != -1)
            {
                playList.IndexSong(listPlayList.SelectedIndex);
                playList.DeleteAudio();
                ShowList();
            }
            else
            {
                MessageBox.Show("Выберите композицию в листе", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClearPlayList(object sender, RoutedEventArgs e) //Кнопка очистить плейлист
        {
            PlayList.ClearList();
            ShowList();
        }

        private void btnShowPlayList(object sender, RoutedEventArgs e) //Кнопка показать плейлист
        {
            ShowList();
        }

        private void btnLoad(object sender, RoutedEventArgs e) //Кнопка загрузить из файла
        {
            if (File.Exists(txtFileName.Text))
            {
                string text = File.ReadAllText(txtFileName.Text);

                if (string.IsNullOrEmpty(text))
                {
                    MessageBox.Show("Текстовый файл пустой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                text = text.Trim();

                string[] parts = text.Split(' ');
                if (parts.Length == 2)
                {
                    string error = playList.ContainsFileName(txtFileName.Text);
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    Song song = new Song() { Author = parts[0], Title = parts[1], FileName = txtFileName.Text };
                    PlayList.AddSong(song);
                    ShowList();
                }
                else
                {
                    MessageBox.Show("Структура текстового файла неправильная", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Текстовый файл не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
