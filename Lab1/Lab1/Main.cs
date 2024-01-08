using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main()
        {
            // Размер массива для задания 1
            int N = 10;
            // Диапазон для задания 1.2
            int a = 30, b = 40;

            //Размер Массива для задания 2
            int M = 5;

            //Первая часть
            Console.WriteLine("Часть 1");
            FirstPart firstPartInstance = new FirstPart(N);
            firstPartInstance.Initial();
            firstPartInstance.print();
            firstPartInstance.max();
            firstPartInstance.FSBP();
            Console.WriteLine("Сжать массив, удалив из него все элементы, модуль которых находится в интервале [" + a + "," + b + "]. Освободившиеся в конце массива элементы заполнить нулями.");
            firstPartInstance.hardOne(a, b);
            firstPartInstance.print();
            Console.WriteLine();

            //Вторая часть
            Console.WriteLine("Часть 2");
            SecondPart secondPartInstance = new SecondPart(M);
            secondPartInstance.Initial();
            secondPartInstance.print();
            secondPartInstance.sum();
            secondPartInstance.minSum();
            
            Console.WriteLine();
            Console.WriteLine("Нажмите Enter, чтобы закрыть приложение.");
            Console.ReadLine();
        }

    }
}