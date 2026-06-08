using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd1_task1_2_Savitsin
{
    class PlayList
    {
        private static List<Song> list; //Лист структуры Song
        private int currentIndex; //Текущий индекс

        public PlayList() //Конструктор класса PlayList
        {
            list = new List<Song>();
            currentIndex = 0;
        }
        public Song CurrentSong() //Метод для получения по индексу песни из листа
        {
            if (list.Count > 0)
            {
                return list[currentIndex];
            }
            else
            {
                throw new IndexOutOfRangeException("Невозможно получить текущую аудиозапись для пустого плейлиста!");
            }
        }
        public static List<Song> GetList() //Получения листа структуры
        {
            return list;
        }
        public static void AddSong(string author, string title, string fileName) //Добавление песни в лист
        {
            Song song = new Song();

            song.Author = author;
            song.Title = title;
            song.FileName = fileName;

            list.Add(song);
        }
        public static void AddSong(Song song) //Перегруженный метод добавления песни в лист
        {
            list.Add(song);
        }
        public string GetInfo(Song song) //Получение информации о конкретной песни
        {
            return $"Автор: {song.Author}. Название: {song.Title}. Имя файла: {song.FileName}";
        }
        public string GetInfo() //Перегрузка метода GetInfo()
        {
            return $"Автор: {CurrentSong().Author}. Название: {CurrentSong().Title}. Имя файла: {CurrentSong().FileName}";
        }
        public static void ClearList() //Очистка листа
        {
            list.Clear();
        }
        public void DeleteAudio() //Удаление конкретной песни из листа
        {
            list.RemoveAt(currentIndex);
        }
        public void DeleteAudio(Song song) //Перегруженный метод удаления песни
        {
            list.Remove(song);
        }
        public void NextSong() //Переход к следующей песне
        {
            if (currentIndex >= 0 && currentIndex < list.Count - 1)
            {
                currentIndex++;
            }
        }
        public void PreviousSong() //Переход к предыдущей песне
        {
            if (currentIndex > 0 && currentIndex < list.Count)
            {
                currentIndex--;
            }
        }
        public void IndexSong(int index) //Переход к песне по индексу
        {
            currentIndex = index;
        }
        public string ContainsFileName(string filename) //Проверка на содержание имени файла
        {
            foreach (var item in list)
            {
                if (item.FileName == filename)
                {
                    return "Лист уже содержит такое название файла";
                }
            }
            return "";
        }
        public int GetIndex() //Получение текующего индекса
        {
            return currentIndex;
        }
    }
}
