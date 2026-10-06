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
            // 1. Проверяем, что имя введено
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Введите имя противника!");
                return;
            }

            // 2. Проверяем, что иконка выбрана
            if (string.IsNullOrEmpty(selectedIconName))
            {
                MessageBox.Show("Выберите иконку!");
                return;
            }

            // 3. Парсим числа. TryParse вернёт false, если не получилось
            if (!int.TryParse(BaseLifeTextBox.Text, out int baseLife))
            {
                MessageBox.Show("Некорректное базовое здоровье!");
                return;
            }

            if (!double.TryParse(LifeModifierTextBox.Text, out double lifeModifier))
            {
                MessageBox.Show("Некорректный модификатор здоровья!");
                return;
            }

            if (!int.TryParse(BaseGoldTextBox.Text, out int baseGold))
            {
                MessageBox.Show("Некорректное базовое золото!");
                return;
            }

            if (!double.TryParse(GoldModifierTextBox.Text, out double goldModifier))
            {
                MessageBox.Show("Некорректный модификатор золота!");
                return;
            }

            if (!double.TryParse(SpawnChanceTextBox.Text, out double spawnChance))
            {
                MessageBox.Show("Некорректный шанс появления!");
                return;
            }

            // 4. Добавляем противника в список
            enemyList.AddEnemy(
                NameTextBox.Text,
                selectedIconName,
                baseLife,
                lifeModifier,
                baseGold,
                goldModifier,
                spawnChance
            );

            // 5. Обновляем левый список
            RefreshEnemiesListBox();

            // 6. Очищаем форму
            ClearForm();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // 1. Проверяем, что выбран противник для редактирования
            if (EnemiesListBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите противника для редактирования!");
                return;
            }

            // 2. Запоминаем старое имя (по нему будем искать)
            string oldName = EnemiesListBox.SelectedItem.ToString();

            // 3. Проверяем, что новое имя не пустое
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Введите имя противника!");
                return;
            }

            // 4. Парсим числа (те же проверки, что и в AddButton_Click)
            if (!int.TryParse(BaseLifeTextBox.Text, out int baseLife))
            {
                MessageBox.Show("Некорректное базовое здоровье!");
                return;
            }
            if (!double.TryParse(LifeModifierTextBox.Text, out double lifeModifier))
            {
                MessageBox.Show("Некорректный модификатор здоровья!");
                return;
            }
            if (!int.TryParse(BaseGoldTextBox.Text, out int baseGold))
            {
                MessageBox.Show("Некорректное базовое золото!");
                return;
            }
            if (!double.TryParse(GoldModifierTextBox.Text, out double goldModifier))
            {
                MessageBox.Show("Некорректный модификатор золота!");
                return;
            }
            if (!double.TryParse(SpawnChanceTextBox.Text, out double spawnChance))
            {
                MessageBox.Show("Некорректный шанс появления!");
                return;
            }

            // 5. Обновляем противника в списке
            enemyList.UpdateEnemy(
                oldName,
                NameTextBox.Text,
                selectedIconName,
                baseLife,
                lifeModifier,
                baseGold,
                goldModifier,
                spawnChance
            );

            // 6. Обновляем интерфейс и очищаем форму
            RefreshEnemiesListBox();
            ClearForm();
            MessageBox.Show("Противник обновлён!");
        }

        // Кнопка "Удалить"
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // 1. Проверяем, что что-то выбрано в левом списке
            if (EnemiesListBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите противника для удаления!");
                return;
            }

            // 2. Получаем имя выбранного
            string selectedName = EnemiesListBox.SelectedItem.ToString();

            // 3. Удаляем
            enemyList.DeleteEnemyByName(selectedName);

            // 4. Обновляем список
            RefreshEnemiesListBox();
        }


        // Кнопка "Сохранить список"
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = "json";
            dialog.Filter = "JSON files (*.json)|*.json";

            if (dialog.ShowDialog() == true)
            {
                enemyList.SaveToJson(dialog.FileName);
                MessageBox.Show("Список сохранён!");
            }
        }

        // Кнопка "Загрузить список"
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = "json";
            dialog.Filter = "JSON files (*.json)|*.json";

            if (dialog.ShowDialog() == true)
            {
                enemyList.LoadFromJson(dialog.FileName);
                RefreshEnemiesListBox();
                MessageBox.Show("Список загружен!");
            }
        }

        // Событие выбора иконки в правом списке
        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // sender — это тот объект, на котором произошло событие (наш ListBox)
            ListBox iconHolder = sender as ListBox;

            // SelectedItem — выбранный элемент (у нас это Image)
            if (iconHolder != null && iconHolder.SelectedItem is Image selectedImage)
            {
                // Source хранит Uri картинки. Превращаем в строку и берём имя файла
                string fullPath = selectedImage.Source.ToString();
                selectedIconName = System.IO.Path.GetFileName(fullPath);

                // Показываем эту иконку в центре
                MainEnemyIcon.Source = selectedImage.Source;
            }
        }

        // Событие выбора противника в левом списке
        private void EnemiesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Если ничего не выбрано — выходим
            if (EnemiesListBox.SelectedItem == null)
                return;

            // Имя выбранного противника
            string selectedName = EnemiesListBox.SelectedItem.ToString();

            // Ищем его в списке
            CEnemyTemplate found = enemyList.GetEnemyByName(selectedName);
            if (found == null)
                return;

            // Заполняем форму его данными
            NameTextBox.Text = found.Name;
            BaseLifeTextBox.Text = found.BaseLife.ToString();
            LifeModifierTextBox.Text = found.LifeModifier.ToString();
            BaseGoldTextBox.Text = found.BaseGold.ToString();
            GoldModifierTextBox.Text = found.GoldModifier.ToString();
            SpawnChanceTextBox.Text = found.SpawnChance.ToString();

            // Запоминаем имя иконки и пытаемся её показать
            selectedIconName = found.IconName;
            // (для отображения иконки в центре нужно найти её в enemyIcons)
            foreach (EnemyIcon icon in enemyIcons)
            {
                if (icon.Name == found.IconName)
                {
                    MainEnemyIcon.Source = new BitmapImage(
                        new Uri(icon.ImagePath, UriKind.Absolute));
                    break;
                }
            }
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

        private void RefreshEnemiesListBox()
        {
            EnemiesListBox.Items.Clear();

            List<string> names = enemyList.GetListOfEnemyNames();
            foreach (string name in names)
            {
                EnemiesListBox.Items.Add(name);
            }
        }

        // Очистить форму ввода
        private void ClearForm()
        {
            NameTextBox.Clear();
            BaseLifeTextBox.Clear();
            LifeModifierTextBox.Clear();
            BaseGoldTextBox.Clear();
            GoldModifierTextBox.Clear();
            SpawnChanceTextBox.Clear();
            MainEnemyIcon.Source = null;
            selectedIconName = "";
        }
    }
}