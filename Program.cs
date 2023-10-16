using System;
using System.IO;

namespace NoteTaker
{
    class Program
    {
        private static DateTime Date { get; set; }
        // Добавили свойство Date для хранения текущей даты
        public DateTime Deadline { get; set; }

        static void Main(string[] args)
        {
            // Создаем папку для хранения заметок (если она не существует)
            string directoryPath = "Заметки";
            Directory.CreateDirectory(directoryPath);

            while (true)
            {
                Console.Clear(); // Очищаем консоль перед выводом меню выбора действий

                Console.WriteLine("Выберите действие:");
                Console.WriteLine("       1. Создать новую заметку");
                Console.WriteLine("       2. Просмотреть существующие заметки");
                Console.WriteLine("       3. Выход");
                int pos = 1;
                ConsoleKeyInfo key;
                do
                {
                    Console.SetCursorPosition(0, pos);
                    Console.WriteLine("->");
                    key = Console.ReadKey();
                    Console.SetCursorPosition(0, pos);
                    Console.WriteLine("  ");
                    if (key.Key == ConsoleKey.UpArrow && pos != 1)
                        pos--;
                    else if (key.Key == ConsoleKey.DownArrow && pos != 3)
                        pos++;

                } while (key.Key != ConsoleKey.Enter);
                Console.Clear();

                if (pos == 1)
                {
                    Console.WriteLine(" ");
                }
                Console.Write("Пожалуйста подтвердите операцию: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateNote();
                        break;
                    case "2":
                        ViewNotes();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                        break;
                }

                Console.WriteLine();
            }
        }


        static void CreateNote()
        {
            Console.Write("Введите название заметки: ");
            string title = Console.ReadLine();
            Console.WriteLine("Введите date: ");

            Console.WriteLine("Введите текст заметки (для завершения введите пустую строку): ");
            string content = "";
            string line;
            string date;
            while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
            {
                content += line + Environment.NewLine;
            }

            // Сохраняем заметку в файл
            string filePath = $"Заметки/{title}.txt";
            File.WriteAllText(filePath, content);

            Console.WriteLine("Заметка успешно создана.");
            Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();
        }

        static void ViewNotes()
        {
            string[] noteFiles = Directory.GetFiles("Заметки");

            if (noteFiles.Length == 0)
            {
                Console.WriteLine("Заметок не найдено.");
                return;
            }

            Console.WriteLine("Переключение между заметками: стрелка влево - предыдущая заметка, стрелка вправо - следующая заметка");
            Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();

            int noteIndex = 0; // Переменная для хранения текущего индекса заметки
            bool exit = false; // Переменная для выхода из цикла просмотра заметок

            while (!exit)
            {
                Console.Clear(); // Очищаем консоль перед выводом текущей заметки

                string selectedNoteFile = noteFiles[noteIndex];
                string title = Path.GetFileNameWithoutExtension(selectedNoteFile);
                string content = File.ReadAllText(selectedNoteFile);

                Console.WriteLine($"Заметка {noteIndex + 1} из {noteFiles.Length}");
                Console.WriteLine("Название заметки: " + title);
                
                Console.WriteLine("Дата заметки:");
                Console.WriteLine("Содержимое заметки:");
                Console.WriteLine(content);
                


                Console.WriteLine();
                Console.WriteLine("Нажмите стрелку влево для просмотра предыдущей заметки, стрелку вправо - для следующей заметки");
                Console.WriteLine("Для выхода из просмотра заметок нажмите любую другую клавишу...");

                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (noteIndex > 0)
                            noteIndex--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (noteIndex < noteFiles.Length - 1)
                            noteIndex++;
                        break;
                    default:
                        exit = true;
                        break;
                }
            }
        }

    }
}