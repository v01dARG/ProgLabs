using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class FirstPart
    {
        private int N;
        public int[] array;

        public FirstPart(int size)
        {
            N = size;
            array = new int[N];
        }
        public FirstPart(int[] array)
        {
            this.array = array;
            N = array.Length;
        }
        public void Initial()
        {
            // Генератор случайных чисел
            Random random = new Random();
            //Заполнение случайными вещественными числами
            for (int i = 0; i < N; i++)
            {
                array[i] = random.Next(-100, 101);
            }
        }

        public void print()
        {
            // Вывод сгенерированного массива на экран
            Console.WriteLine("Массив:");
            foreach (int num in array)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }

        public void max()
        {
            // Поиск максимального элемента в массиве
            int maxElement = array[0];
            foreach (int num in array)
            {
                if (num > maxElement)
                {
                    maxElement = num;
                }
            }
            Console.WriteLine("Максимальный элемент в массиве: " + maxElement);
        }

        public void FSBP()
        {
            // Найти сумму элементов до последнего положительного элемента
            int sum = 0;
            bool foundPositive = false;

            for (int j = N - 1; j >= 0; j--)
            {
                int num = array[j];
                if (num > 0 && !foundPositive)
                {
                    foundPositive = true;
                    sum -= num;
                }
                if (foundPositive)
                {
                    sum += num;
                }
            }
            Console.WriteLine("Сумма элементов до последнего положительного элемента: " + sum);
        }

        public void hardOne(int a, int b)
        {
            // Сжать массив, удалив из него все элементы, модуль которых находится в интервале
            // [а, b]. Освободившиеся в конце массива элементы заполнить нулями.
            List<int> list = array.ToList();
            list.RemoveAll(x => Math.Abs(x) >= a && Math.Abs(x) <= b);
            list.AddRange(Enumerable.Repeat(0, array.Length - list.Count));
            array = list.ToArray();
        }
    }
    
}
