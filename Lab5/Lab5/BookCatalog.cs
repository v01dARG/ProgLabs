using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Serialization;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Text.Json.Serialization;
using System.Collections.ObjectModel;

public struct Book // Структура для хранения книг
{
    [JsonInclude]
    public String Title;// название
    [JsonInclude]
    public String Author;// автор
    [JsonInclude]
    public String Description;// описание
    [JsonInclude]
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
    public String Title { get; set; }// название
    public String Author { get; set; }// автор
    public String Description { get; set; }// описание
    public int KeyNum { get; set; }// количество найденных ключевых слов
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
    private static SQLiteConnection m_dbConn;
    private static SQLiteCommand m_sqlCmd;

    public void ClearBookList()// для тестов
    {
        BookList.Clear();
    }
    public int CountBookList()// для тестов
    {
        return BookList.Count;
    }
    public bool CheckBookList(int index, String Title, String Author)// для тестов
    {
        if (index<BookList.Count)
        {
            if (BookList[index].Title == Title && BookList[index].Author == Author)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    private static bool Create_SQLLite()
    {
        if (!File.Exists("catalog.sqlite"))
        {
            SQLiteConnection.CreateFile("catalog.sqlite");
        }

        try
        {
            m_dbConn = new SQLiteConnection("Data Source=catalog.sqlite;Version=3;");
            m_dbConn.Open();
            m_sqlCmd = m_dbConn.CreateCommand();
            m_sqlCmd.Connection = m_dbConn;

            m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS catalog (id integer primary key autoincrement, title varchar(200), author varchar(100), description varchar(400), isbn varchar(100) )";//создадим если нету
            m_sqlCmd.ExecuteNonQuery();
            m_sqlCmd.CommandText = "DELETE FROM catalog";//Очистим
            m_sqlCmd.ExecuteNonQuery();
            return true;
        }
        catch// (SQLiteException ex)
        {
            //Console.WriteLine("Error: " + ex.Message;
            return false;
        }
    }

    private static bool Connect_SQLLite()
    {
        if (!File.Exists("catalog.sqlite"))
        {
            //Console.WriteLine("No database catalog.sqlite";
            return false;
        }
        try
        {
            m_dbConn = new SQLiteConnection("Data Source=catalog.sqlite;Version=3;");
            m_dbConn.Open();
            m_sqlCmd = m_dbConn.CreateCommand();
            m_sqlCmd.Connection = m_dbConn;
            return true;
        }
        catch// (SQLiteException ex)
        {
            //Console.WriteLine("Error: " + ex.Message;
            return false;
        }
    }
    private static bool Read_SQLLite()
    {
        DataTable table = new DataTable();
        String sqlQuery;

        if (m_dbConn.State != ConnectionState.Open)
        {
            //Console.WriteLine("Open connection with SQLLite database";
            return false;
        }
        BookList.Clear();
        try
        {
            sqlQuery = "SELECT * FROM catalog";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    BookList.Add(new Book(Convert.ToString(row["title"]), Convert.ToString(row["author"]), Convert.ToString(row["description"]), Convert.ToString(row["isbn"])));
                }
            }
            else
            {
                //Console.WriteLine("Database is empty";
                return false;
            }
            return (BookList.Count>0);
        }
        catch// (SQLiteException ex)
        {
            //Console.WriteLine("Error: " + ex.Message;
            return false;
        }
    }

    private static bool Write_SQLlite()
    {
        if (m_dbConn.State != ConnectionState.Open)
        {
            //Console.WriteLine("Open connection with database";
            return false;
        }
        try
        {
            foreach (Book book in BookList)
            {
                m_sqlCmd.CommandText = "INSERT INTO catalog ('title','author','description','isbn') values ('" + book.Title + "','" + book.Author + "','" + book.Description + "','" + book.ISBN + "')";
                m_sqlCmd.ExecuteNonQuery();
            }
            return true;
        }
        catch// (SQLiteException ex)
        {
            //Console.WriteLine("Error: " + ex.Message;
            return false;
        }
    }
    public bool SaveSQLLite()
    {
        if (Create_SQLLite())
        {
            return Write_SQLlite();
        }
        else
        {
            return false;
        }
    }
    public bool LoadSQLLite()
    {
        if (Connect_SQLLite())
        {
            return Read_SQLLite();
        }
        else
        {
            return false;
        }
    }

    public bool SaveXML()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Book>));
            StreamWriter writer = new StreamWriter("catalog.xml");
            serializer.Serialize(writer, BookList);
            writer.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool LoadXML()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Book>));
            TextReader reader = new StreamReader("catalog.xml");
            BookList = (List<Book>)serializer.Deserialize(reader);
            reader.Close();
            return (BookList.Count > 0);
        }
        catch
        {
            return false;
        }
    }

    public bool SaveJSON()
    {
        try
        {
            string json = JsonSerializer.Serialize(BookList);//<List<Book>>
            File.WriteAllText("catalog.json", json);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool LoadJSON()
    {
        try
        {
            string data = File.ReadAllText("catalog.json");
            BookList = JsonSerializer.Deserialize<List<Book>>(data);
            return (BookList.Count > 0);
        }
        catch
        {
            return false;
        }
    }

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
    public void SelectAllBooks(ref List<FoundBook> Result)
    {
        Result.Clear();
        foreach (Book book in BookList)
        {
            Result.Add(new FoundBook(book.Title, book.Author, book.Description));
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
