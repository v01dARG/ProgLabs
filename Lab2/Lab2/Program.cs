using System;
using System.Collections.Generic;

namespace Lab2
{
    class Program
    {
        static void WriteMenu()
        {
            Console.WriteLine("Книжный каталог:");
            Console.WriteLine("1. Добавить книгу в каталог");
            Console.WriteLine("2. Поиск по названию");
            Console.WriteLine("3. Поиск по фрагменту названия");
            Console.WriteLine("4. Поиск по имени автора");
            Console.WriteLine("5. Поиск по ключевым словам");
            Console.WriteLine("6. Выход");
        }
        static void Main(string[] args)
        {
            BookCatalog Catalog = new BookCatalog();//Объект Книжный каталог
            List<FoundBook> FoundBooks = new List<FoundBook>();//найденые книги
            String S;//строка для ввода
            bool exit = false;
            WriteMenu();
            while (!exit)
            {
                S = Console.ReadLine();
                if (S.Length == 1)
                {
                    switch (S[0])
                    {
                        case '1':
                            Console.WriteLine("Введите название книги");
                            String STitle = Console.ReadLine();
                            Console.WriteLine("Введите автора книги");
                            String SAuthor = Console.ReadLine();
                            Console.WriteLine("Введите номер ISBN");
                            String ISBN = Console.ReadLine();
                            Console.WriteLine("Введите краткое описание");
                            S = Console.ReadLine();
                            if (Catalog.AddBook(STitle, SAuthor, S, ISBN))
                            {
                                Console.WriteLine("Книга добавлена " + STitle+" "+SAuthor);
                            }   
                            else
                            {
                                Console.WriteLine("Книга не добавлена!");
                            }
                            WriteMenu();
                            break;
                        case '2':
                            Console.WriteLine("Введите название книги");
                            S = Console.ReadLine();
                            if (Catalog.SelectBooksByTitle(S, ref FoundBooks))
                            {
                                for (int i = 0; i < FoundBooks.Count; i++)
                                {
                                    Console.WriteLine(Convert.ToString(i + 1) + ". " + FoundBooks[i].Title + " " + FoundBooks[i].Author);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Таких книг не найдено!");
                            }
                            WriteMenu();
                            break;
                        case '3':
                            Console.WriteLine("Введите фрагмент названия книги");
                            S = Console.ReadLine();
                            if (Catalog.SelectBooksByPartOfTitle(S, ref FoundBooks))
                            {
                                for (int i = 0; i < FoundBooks.Count; i++)
                                {
                                    Console.WriteLine(Convert.ToString(i + 1) + ". " + FoundBooks[i].Title + " " + FoundBooks[i].Author);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Таких книг не найдено!");
                            }
                            WriteMenu();
                            break;
                        case '4':
                            Console.WriteLine("Введите автора книги");
                            S = Console.ReadLine();
                            if (Catalog.SelectBooksByAuthor(S, ref FoundBooks))
                            {
                                for (int i = 0; i < FoundBooks.Count; i++)
                                {
                                    Console.WriteLine(Convert.ToString(i + 1) + ". " + FoundBooks[i].Title + " " + FoundBooks[i].Author);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Таких книг не найдено!");
                            }
                            WriteMenu();
                            break;
                        case '5':
                            Console.WriteLine("Введите ключевые слова через пробел");
                            S = Console.ReadLine();
                            if (Catalog.SelectBooksByKeyWords(S, ref FoundBooks))
                            {
                                for (int i = 0; i < FoundBooks.Count; i++)
                                {
                                    Console.WriteLine(Convert.ToString(i + 1) + ". " + FoundBooks[i].Title + " " + FoundBooks[i].Author + " " + FoundBooks[i].Description);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Таких книг не найдено!");
                            }
                            WriteMenu();
                            break;
                        case '6':
                            exit = true;
                            break;
                    }
                }
            }
        }
    }
}
