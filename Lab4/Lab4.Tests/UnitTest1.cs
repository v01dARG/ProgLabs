using Lab4.Controllers;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Lab3.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            BookCatalog Catalog = new BookCatalog();//������ ������� �������
            List<Book> FoundBooks = new List<Book>();//�������� �����

            Assert.True(Catalog.AddBook("������ ��������", "������ ���������", "����� � ������������, ��������� � ������� ��������, ���������� ��������� ������� � �������� �� ����������� �������. ������������� ������ �� ���� �������� �����, ���� ���������� �������� � ����� ����� �������.", "978-5-699-12014-7"));
            Assert.True(Catalog.AddBook("����� ������ � ����������� ������", "����� �������","����� �������� ������ �������� �����, ������, ������� �����, ��� �� ��������� � ��� ������������ ���� ��������. �������� � ����� ����������� � ���������� ��������, �� ������� ���� ������� ������, � ������� ������� ������������� ������� ����������� ����� ����������, �������� ��� ���������, ����� ��� ���� ����� ����� �������.", "978-2-621-14033-1"));
            Assert.True(Catalog.AddBook("����� ������ � ������ �������", "����� �������", "����� ������������ � ������ ������� ���� � ����� ����������� � ���������� ��������, �� ������� ����� � ��� ������ � ��� ����� � �������� ��������� � ���������� ������������ ��������� �� �������� �����, ����������� ����� ������������ ���������. ��������� ��������� �������� �������������� �������. ��� ������������ ��������� � ���������� � �� �� ��� �� ���������.", "978-2-621-14033-2"));
            Assert.True(Catalog.AddBook("����� ������ � ����� ��������", "����� �������", "� ������� ����� �����, �������� �� 3-� ����� ����� ����������� � ���������� ��������, ������ �� ������ �������� ������ ������� ������� ����� � ��������� �� ������ ������� ����������, ������� ������������� � ������ �� ����� �����-��-����� � � ��� ���� � ����� �����.", "978-2-621-14033-3"));
            Assert.True(Catalog.AddBook("����� ������ � ����� ����", "����� �������", "�� ������ �������� �����, ����� ������ ����� ���� ����������� � ������� � ������� ��� �����������, � ��� ��������� �� ������ ��������� � ����� �������� �����������, �� � ��������� ������� ����, ��� �� ������ ����� �� ������ ������� ��������. ", "978-2-621-14033-4"));
            Assert.True(Catalog.AddBook("�������� � �������������", "����� �����", "����� �������������� � 1813 ����. ������� ��������, ��� ������� � ������������ ������� �� ����, ��������� ������ ��� ������ �����. ����� � �������� �������� �������� ���������� � ������� ������ �����, �������� ������������������ ��. ������ ������ ������������, ����������, � ����� ����������� �������� � ������������� ���� ���������� ������� ���� �� �������� � ����, ��� ��� ��������, ��������� � ���������� ���� �����, �������� ������� �� �������.", "978-3-961-78353-1"));
            Assert.True(Catalog.AddBook("������� � ����������������", "����� �����", "������� ���� �����: �������������� � ���������� ������ � �����������, ��������� ��������. ����� ������ ����, ������ ������ ������ ����� �����, ��� ���������� � ���������� �� �� ��������.", "978-3-961-78353-2"));
            Assert.True(Catalog.AddBook("����", "����� �����", "����� ������� � �������������� ������ � �������� ������� �������, ������� � ���������� ������� ����� �������� � �������. ���� �������������� �������� � ������� �������������, ���� �������� ������������� ���� �����, ������������� ����� ������ �����. ������ ���������, ��� ��� ������� �� ������ �����, ��� ��������� � ���� ����� ��� ����� ������ � ��������, �� ����� ����������� �� ������� �� ���������.", "978-3-961-78353-3"));

            Assert.True(Catalog.SelectBooksByTitle("�������� � �������������", ref FoundBooks));
            Assert.Single(FoundBooks);
            Assert.Equal("�������� � �������������", FoundBooks[0].Title);

            Assert.True(Catalog.SelectBooksByPartOfTitle("������", ref FoundBooks));
            Assert.Equal(4, FoundBooks.Count);
            Assert.Equal("����� ������ � ����������� ������", FoundBooks[0].Title);
            Assert.Equal("����� ������ � ������ �������", FoundBooks[1].Title);
            Assert.Equal("����� ������ � ����� ��������", FoundBooks[2].Title);
            Assert.Equal("����� ������ � ����� ����", FoundBooks[3].Title);

            Assert.True(Catalog.SelectBooksByAuthor("����� �����", ref FoundBooks));
            Assert.Equal(3, FoundBooks.Count);
            Assert.Equal("�������� � �������������", FoundBooks[0].Title);
            Assert.Equal("������� � ����������������", FoundBooks[1].Title);
            Assert.Equal("����", FoundBooks[2].Title);

            Assert.True(Catalog.SelectBooksByKeyWords("����� ����� �������", ref FoundBooks));
            Assert.Equal(4, FoundBooks.Count);
            Assert.Equal("�������� � �������������", FoundBooks[0].Title);//����� ����� �������
            Assert.Equal("����", FoundBooks[1].Title);//����� �����
            Assert.Equal("������ ��������", FoundBooks[2].Title);//�����
            Assert.Equal("������� � ����������������", FoundBooks[3].Title);//�����

            Assert.True(Catalog.SaveXML());
            Assert.True(Catalog.SaveJSON());
            Assert.True(Catalog.SaveSQLLite());

            Catalog.ClearBookList();
            Assert.Equal(0, Catalog.CountBookList());
            Assert.True(Catalog.LoadXML());
            Assert.Equal(8, Catalog.CountBookList());
            Assert.True(Catalog.CheckBookList(0, "������ ��������", "������ ���������"));
            Assert.True(Catalog.CheckBookList(1, "����� ������ � ����������� ������", "����� �������"));
            Assert.True(Catalog.CheckBookList(2, "����� ������ � ������ �������", "����� �������"));
            Assert.True(Catalog.CheckBookList(3, "����� ������ � ����� ��������", "����� �������"));
            Assert.True(Catalog.CheckBookList(4, "����� ������ � ����� ����", "����� �������"));
            Assert.True(Catalog.CheckBookList(5, "�������� � �������������", "����� �����"));
            Assert.True(Catalog.CheckBookList(6, "������� � ����������������", "����� �����"));
            Assert.True(Catalog.CheckBookList(7, "����", "����� �����"));

            Catalog.ClearBookList();
            Assert.Equal(0, Catalog.CountBookList());
            Assert.True(Catalog.LoadJSON());
            Assert.Equal(8, Catalog.CountBookList());
            Assert.True(Catalog.CheckBookList(0, "������ ��������", "������ ���������"));
            Assert.True(Catalog.CheckBookList(1, "����� ������ � ����������� ������", "����� �������"));
            Assert.True(Catalog.CheckBookList(2, "����� ������ � ������ �������", "����� �������"));
            Assert.True(Catalog.CheckBookList(3, "����� ������ � ����� ��������", "����� �������"));
            Assert.True(Catalog.CheckBookList(4, "����� ������ � ����� ����", "����� �������"));
            Assert.True(Catalog.CheckBookList(5, "�������� � �������������", "����� �����"));
            Assert.True(Catalog.CheckBookList(6, "������� � ����������������", "����� �����"));
            Assert.True(Catalog.CheckBookList(7, "����", "����� �����"));

            Catalog.ClearBookList();
            Assert.Equal(0, Catalog.CountBookList());
            Assert.True(Catalog.LoadSQLLite());
            Assert.Equal(8, Catalog.CountBookList());
            Assert.True(Catalog.CheckBookList(0, "������ ��������", "������ ���������"));
            Assert.True(Catalog.CheckBookList(1, "����� ������ � ����������� ������", "����� �������"));
            Assert.True(Catalog.CheckBookList(2, "����� ������ � ������ �������", "����� �������"));
            Assert.True(Catalog.CheckBookList(3, "����� ������ � ����� ��������", "����� �������"));
            Assert.True(Catalog.CheckBookList(4, "����� ������ � ����� ����", "����� �������"));
            Assert.True(Catalog.CheckBookList(5, "�������� � �������������", "����� �����"));
            Assert.True(Catalog.CheckBookList(6, "������� � ����������������", "����� �����"));
            Assert.True(Catalog.CheckBookList(7, "����", "����� �����"));

            Assert.True(Catalog.UpdateBook(7, "���� � �����"));
            Assert.Equal("���� � �����;����� �����;������� ���� �����: �������������� � ���������� ������ � �����������, ��������� ��������. ����� ������ ����, ������ ������ ������ ����� �����, ��� ���������� � ���������� �� �� ��������.", Catalog.GetBook(7));
            Assert.True(Catalog.DeleteBook(1));
            Assert.Equal(7, Catalog.CountBookList());
            Assert.True(Catalog.DeleteBook(7));
            Assert.Equal(6, Catalog.CountBookList());
            Assert.True(Catalog.CheckBookList(0, "����� ������ � ����������� ������", "����� �������"));
            Assert.True(Catalog.CheckBookList(1, "����� ������ � ������ �������", "����� �������"));
            Assert.True(Catalog.CheckBookList(2, "����� ������ � ����� ��������", "����� �������"));
            Assert.True(Catalog.CheckBookList(3, "����� ������ � ����� ����", "����� �������"));
            Assert.True(Catalog.CheckBookList(4, "�������� � �������������", "����� �����"));
            Assert.True(Catalog.CheckBookList(5, "���� � �����", "����� �����"));
        }
    }
}