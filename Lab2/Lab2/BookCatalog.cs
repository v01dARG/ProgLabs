using System;
using System.Collections.Generic;

internal struct Book // Структура для хранения книг
{
    public String Title;// название
    public String Author;// автор
    public String Description;// описание
    public String ISBN;// ISBN номер с дефисами 978-5-699-12014-7
    public Book(String Title, String Author, String Description, String ISBN)
    {
        this.Title = Title;
        this.Author = Author;
        this.Description = Description;
        this.ISBN = ISBN;
    }
}

public struct FoundBook : IComparable// Структура для хранения найденной книги
{
    public String Title;// название
    public String Author;// автор
    public String Description;// описание
    public int KeyNum;// количество найденных ключевых слов
    public FoundBook(String Title, String Author, String Description)
    {
        this.Title = Title;
        this.Author = Author;
        this.Description = Description;
        this.KeyNum = 0;
    }
    public FoundBook(String Title, String Author, String Description, int KeyNum)
    {
        this.Title = Title;
        this.Author = Author;
        this.Description = Description;
        this.KeyNum = KeyNum;
    }
    public int CompareTo(object obj)
    {
        FoundBook it = (FoundBook)obj;
        if (this.KeyNum == it.KeyNum) return 0;
        else if (this.KeyNum > it.KeyNum) return -1;
        else return 1;
    }
}

public class BookCatalog
{
    private static List<Book> BookList = new List<Book>();//Список книг
    // Добавить книгу
    public bool AddBook(String Title, String Author, String Description, String ISBN)
    {
        if (Title.Length == 0 || Author.Length == 0)
        {
            return false;
        }
        else
        {
            BookList.Add(new Book(Title, Author, Description, ISBN));
            return true;
        }
    }
    // Найти книги по названию
    public bool SelectBooksByTitle(String Title, ref List<FoundBook> Result)
    {
        Result.Clear();
        if (Title.Length == 0)
        {
            return false;
        }
        Title = Title.ToLower();
        foreach (Book book in BookList) { 
            if (book.Title.ToLower() == Title)
            {
                Result.Add(new FoundBook(book.Title, book.Author, book.Description));
            }
        }
        return (Result.Count > 0);
    }
    // Найти книги по части названия
    public bool SelectBooksByPartOfTitle(String Title, ref List<FoundBook> Result)
    {
        Result.Clear();
        if (Title.Length == 0)
        {
            return false;
        }
        Title = Title.ToLower();
        foreach (Book book in BookList)
        {
            if (book.Title.ToLower().IndexOf(Title)>=0)
            {
                Result.Add(new FoundBook(book.Title, book.Author, book.Description));
            }
        }
        return (Result.Count > 0);
    }
    // Найти книги по автору
    public bool SelectBooksByAuthor(String Author, ref List<FoundBook> Result)
    {
        Result.Clear();
        if (Author.Length == 0)
        {
            return false;
        }
        Author = Author.ToLower();
        foreach (Book book in BookList)
        {
            if (book.Author.ToLower() == Author)
            {
                Result.Add(new FoundBook(book.Title, book.Author, book.Description));
            }
        }
        return (Result.Count > 0);
    }
    // Найти книги по ключевым словам (через пробел)
    public bool SelectBooksByKeyWords(String StrKeyWords, ref List<FoundBook> Result)
    {
        Result.Clear();
        if (StrKeyWords.Length == 0)
        {
            return false;
        }
        StrKeyWords = StrKeyWords.ToLower();
        String[] KeyWords = StrKeyWords.Split(' ');
        if (KeyWords.Length == 0)
        {
            return false;
        }
        foreach (Book book in BookList)
        {
            int KeyNum = 0;// количество найденных ключевых слов
            String TitleLower = book.Title.ToLower();
            String DescLower = book.Description.ToLower();
            foreach (String key in KeyWords)
            {
                if (TitleLower.IndexOf(key + ' ') >= 0)
                {
                    KeyNum++;
                }
                else if (TitleLower.IndexOf(key + '.') >= 0)
                {
                    KeyNum++;
                }
                else if (TitleLower.IndexOf(key + ',') >= 0)
                {
                    KeyNum++;
                }
                else if (TitleLower.EndsWith(key))
                {
                    KeyNum++;
                }
                else if (DescLower.IndexOf(key + ' ') >= 0)
                {
                    KeyNum++;
                }
                else if (DescLower.IndexOf(key + '.') >= 0)
                {
                    KeyNum++;
                }
                else if (DescLower.IndexOf(key + ',') >= 0)
                {
                    KeyNum++;
                }
                else
                if (DescLower.EndsWith(key))
                {
                    KeyNum++;
                }
            }
            if (KeyNum > 0)// Добавим в результат
            {
                Result.Add(new FoundBook(book.Title, book.Author, book.Description, KeyNum));
            }
        }
        Result.Sort();
        return (Result.Count > 0);
    }
}
