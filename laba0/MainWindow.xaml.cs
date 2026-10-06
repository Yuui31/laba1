using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using laba1;
using Microsoft.Win32;

namespace laba1
{
    public partial class MainWindow : Window
    {
        // ===== ПОЛЯ КЛАССА =====
        private CEnemyTemplateList enemyList;    // список противников
        private List<EnemyIcon> enemyIcons;      // загруженные иконки
        private string selectedIconName = "";    // имя выбранной иконки

        public MainWindow()
        {
            InitializeComponent();

            // Инициализация списков
            enemyList = new CEnemyTemplateList();
            enemyIcons = new List<EnemyIcon>();
        }

        // ==================================================
        // ЗАГЛУШКИ ОБРАБОТЧИКОВ — заполним по одному на след. этапах
        // ==================================================

        // Кнопка "Загрузить иконки"
        private void LoadIconsButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаём диалог выбора папки
            OpenFolderDialog dialog = new OpenFolderDialog();

            // ShowDialog возвращает true, если пользователь выбрал папку и нажал OK
            if (dialog.ShowDialog() == true)
            {
                // Передаём путь к выбранной папке в метод загрузки
                LoadIconsFromFolder(dialog.FolderName);
            }
        }

        // Кнопка "Добавить"
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: этап 8
        }

        // Кнопка "Удалить"
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: этап 8
        }

        // Кнопка "Сохранить список"
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: этап 10
        }

        // Кнопка "Загрузить список"
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: этап 10
        }

        // Событие выбора иконки в правом списке
        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: этап 9
        }

        // Событие выбора противника в левом списке
        private void EnemiesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: этап 9
        }

        // ==================================================
        // ВСПОМОГАТЕЛЬНЫЙ МЕТОД — понадобится дальше
        // ==================================================

        // Загрузка иконок из указанной папки
        private void LoadIconsFromFolder(string path)
        {
            // 1. Очищаем старые данные — если пользователь загружает иконки повторно
            enemyIcons.Clear();
            IconsListBox.Items.Clear();

            // 2. Ищем все .png-файлы в указанной папке
            string[] files = Directory.GetFiles(path, "*.png");

            // 3. Для каждого найденного файла создаём EnemyIcon
            foreach (string file in files)
            {
                EnemyIcon icon = new EnemyIcon
                {
                    Name = System.IO.Path.GetFileName(file),   // "goblin_1.png"
                    ImagePath = file                 // полный путь
                };
                enemyIcons.Add(icon);
            }

            // 4. Отображаем иконки в ListBox
            foreach (EnemyIcon icon in enemyIcons)
            {
                Image image = new Image()
                {
                    Source = new BitmapImage(new Uri(icon.ImagePath, UriKind.Absolute)),
                    Height = 64
                };
                IconsListBox.Items.Add(image);
            }
        }
    }
}