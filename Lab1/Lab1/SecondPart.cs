using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class SecondPart
    {
        private int M;
        public int[,] array;

        public SecondPart(int size)
        {
            M = size;
            array = new int[size, size];
        }
        public SecondPart(int[,] array)
        {
            this.array = array;
            M = array.GetLength(0) + array.GetLength(1);

        }
        public void Initial()
        {
            Random random = new Random();
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    array[i, j] = random.Next(-100, 101); // Генерация случайного числа от -100 до 100
                }
            }
        }
        public void print()
        {
            Console.WriteLine("Двумерный массив со случайными значениями:");
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Console.Write(array[i, j] + "\t");
                }
                Console.WriteLine(); // Переход на новую строку после каждой строки массива
            }
        }

        public void sum()
        {
            int sum = 0;
            for (int j = 0; j < M; j++)
            {
                bool containsNegative = false;
                for (int i = 0; i < M; i++)
                {
                    if (array[i, j] < 0)
                    {
                        containsNegative = true;
                        break;
                    }
                }

                // Если столбец не содержит отрицательных элементов, добавляем сумму его элементов к общей сумме
                if (!containsNegative)
                {
                    for (int i = 0; i < M; i++)
                    {
                        sum += array[i, j];
                    }
                }
            }
            Console.WriteLine("Сумма элементов в столбцах без отрицательных элементов: " + sum);
        }

        public void minSum()
        {
            // Найти минимум среди сумм модулей элементов диагоналей, параллельных побочной диагонали

            // Диагонали над побочной диагональю
            int sum = 0;
            int minSum = int.MaxValue;
            int i = 0;
            int j = M - 2;
            int h = 1;

            for (; h < M; h++)
            {
                while (j != -M)
                {
                    sum = sum + Math.Abs(array[i, j]);
                    
                    i++;
                    j--;
                    if (i == M - 1 || j == -1)
                    {
                        if (sum < minSum) minSum = sum;
                        sum = 0;
                        i = 0;
                        j = M - 2 - h;
                        break;
                    }
                }
            }
            // Диагонали под побочной диагональю
            i = 1;
            j = M - 1;
            h = 1;

            for (; h < M; h++)
            {
                while (i != M)
                {
                    sum = sum + Math.Abs(array[i, j]);
                    i++;
                    j--;
                    if (i == M || j == M)
                    {
                        if (sum < minSum) minSum = sum;
                        sum = 0;
                        i = h + 1;
                        j = M - 1;
                        break;
                    }
                }
            }
            Console.WriteLine("Минимум среди сумм модулей элементов диагоналей, параллельных побочной диагонали: " + minSum);
        }
    }
}
