using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace Event_D.Z_26._06._25
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Задание 1: RGB для цвета радуги ===");
            RunTask1();

            Console.WriteLine("\n=== Задание 2: Класс Рюкзак ===");
            RunTask2();

            Console.WriteLine("\n=== Задание 3: Кол-во чисел, кратных 7 ===");
            RunTask3();

            Console.WriteLine("\n=== Задание 4: Кол-во положительных чисел ===");
            RunTask4();

            Console.WriteLine("\n=== Задание 5: Уникальные отрицательные числа ===");
            RunTask5();

            Console.WriteLine("\n=== Задание 6: Проверка слова в тексте ===");
            RunTask6();
        }

        static void RunTask1()
        {
            Func<string, string> getRgb = delegate (string color)
            {
                var rainbowColors = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "красный", "RGB(255, 0, 0)" },
                    { "оранжевый", "RGB(255, 165, 0)" },
                    { "жёлтый", "RGB(255, 255, 0)" },
                    { "зелёный", "RGB(0, 128, 0)" },
                    { "голубой", "RGB(0, 191, 255)" },
                    { "синий", "RGB(0, 0, 255)" },
                    { "фиолетовый", "RGB(128, 0, 128)" }
                };

                return rainbowColors.ContainsKey(color)
                    ? rainbowColors[color]
                    : "Неизвестный цвет";
            };

            var testColors = new[] { "красный", "зелёный", "фиолетовый", "чёрный" };
            foreach (var color in testColors)
            {
                Console.WriteLine($"Цвет: {color}, RGB: {getRgb(color)}");
            }
        }

        static void RunTask2()
        {
            var backpack = new Backpack();
            backpack.SetProperties("Синий", "Adidas", "Полиэстер", 0.9, 10.0);

            Console.WriteLine("Характеристики рюкзака:");
            Console.WriteLine($"Цвет: {backpack.Color}");
            Console.WriteLine($"Фирма: {backpack.Manufacturer}");
            Console.WriteLine($"Ткань: {backpack.Fabric}");
            Console.WriteLine($"Вес: {backpack.Weight} кг");
            Console.WriteLine($"Объём: {backpack.Capacity} л");
            Console.WriteLine();

            backpack.ItemAdded += delegate (object sender, BackpackItem item)
            {
                Console.WriteLine($"Добавлен предмет: {item.Name}, объём: {item.Volume} л.");
            };

            try
            {
                backpack.AddItem(new BackpackItem("Книга", 2));
                backpack.AddItem(new BackpackItem("Бутылка воды", 1.5));
                backpack.AddItem(new BackpackItem("Плед", 3));
                backpack.AddItem(new BackpackItem("Ноутбук", 4)); // Превысит объём
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void RunTask3()
        {
            int[] numbers = { 7, 14, 3, 21, 28, 10, 49, 50, 70, 2 };

            Func<int[], int> countMultiplesOf7 = arr => arr.Count(n => n % 7 == 0);

            int count = countMultiplesOf7(numbers);
            Console.WriteLine($"В массиве [{string.Join(", ", numbers)}] количество чисел, кратных 7: {count}");
        }

        static void RunTask4()
        {
            int[] numbers = { -10, 0, 5, 12, -3, 7, -1, 0, 8 };

            Func<int[], int> countPositives = arr => arr.Count(n => n > 0);

            int count = countPositives(numbers);
            Console.WriteLine($"В массиве [{string.Join(", ", numbers)}] количество положительных чисел: {count}");
        }

        static void RunTask5()
        {
            int[] numbers = { -10, -3, -10, 5, -3, -1, 0, -1, 8, -5 };

            Func<int[], IEnumerable<int>> uniqueNegatives = arr =>
                arr.Where(n => n < 0).Distinct();

            var negatives = uniqueNegatives(numbers);
            Console.WriteLine($"Уникальные отрицательные числа: {string.Join(", ", negatives)}");
        }

        static void RunTask6()
        {
            Func<string, string, bool> containsWord = (text, word) =>
                text.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0;

            string sampleText = "Это пример текста для тестирования лямбда-выражения.";
            string word1 = "теста";
            string word2 = "лямбда";

            Console.WriteLine($"Текст содержит слово '{word1}'? {containsWord(sampleText, word1)}");
            Console.WriteLine($"Текст содержит слово '{word2}'? {containsWord(sampleText, word2)}");
        }
    }

    class BackpackItem
    {
        public string Name { get; set; }
        public double Volume { get; set; }

        public BackpackItem(string name, double volume)
        {
            Name = name;
            Volume = volume;
        }
    }

    class Backpack
    {
        public string Color { get; set; }
        public string Manufacturer { get; set; }
        public string Fabric { get; set; }
        public double Weight { get; set; }
        public double Capacity { get; set; }

        public List<BackpackItem> Contents { get; private set; } = new List<BackpackItem>();

        public event EventHandler<BackpackItem> ItemAdded;

        public void SetProperties(string color, string manufacturer, string fabric, double weight, double capacity)
        {
            Color = color;
            Manufacturer = manufacturer;
            Fabric = fabric;
            Weight = weight;
            Capacity = capacity;
        }

        public void AddItem(BackpackItem item)
        {
            double usedVolume = Contents.Sum(obj => obj.Volume);

            if (usedVolume + item.Volume > Capacity)
                throw new InvalidOperationException("Объём рюкзака будет превышен!");

            Contents.Add(item);
            ItemAdded?.Invoke(this, item);
        }
    }
}



