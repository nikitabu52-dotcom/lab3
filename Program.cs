using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;

namespace Лаба_Осн_Прог_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] R = { 1, 2, 3, 4, -11 };
            int sum = 0;
            for(int i = 0; i < R.Length; i++)
            {
                sum += R[i];
            }


            string fio = "ФИО: Бусаров Никита Сергеевич\nГруппа: 6105-090301";
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nГлавное меню");
                Console.WriteLine("1. Отгадай ответ");
                Console.WriteLine("2. Об авторе");
                Console.WriteLine("3. Выход");
                Console.WriteLine("4. Сортирвка массива");
                Console.Write("Выберите пункт меню: ");

                string pynkt = Console.ReadLine();

                switch (pynkt)
                {
                    case "1":
                        Ygadaika();
                        break;
                    case "2":
                        Console.WriteLine($"\n{fio}");
                        Console.WriteLine("Для выхода в меню нажмите любую клавишу...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "3":
                        exit = Exit();
                        break;
                    case "4":
                        SortArray();
                        break;
                    default:
                        Console.WriteLine("Ошибка: неверный пункт меню. Введите 1, 2 или 3");
                        break;

                }

            }

        }
        static double Proverka(string arg)
        {
            double number = 0;
            bool fl = false;

            while (!fl)
            {
                Console.WriteLine(arg);
                string otv = Console.ReadLine();
                if (double.TryParse(otv, out number))
                {
                    fl = true;
                }
                else
                {
                    Console.WriteLine("Ошибка. Введите корректное число: ");
                }
            }

            return number;
        }
        static void Ygadaika()
        {
            const double e = Math.E;
            const double p = Math.PI;
            double f;
            double a = Proverka("Введите значение а: ");

            double k = (a - p / 4) / p / 2;
            if (k == Math.Floor(k))
            {
                Console.WriteLine("Ошибка в вычислении тангенса при данном значении а");
                return;
            }
            double chisl = Math.Sin(a) + Math.Tan(2 * a);
            double znam = Math.Sqrt(Math.Log(Math.Pow(e, 2), 3));
            if (a == 0)
            {
                Console.WriteLine("Ошибка: деление на ноль");
                return;
            }
            int pop = 3;
            bool fl = false;
            f = Math.Round(chisl / znam, 2);
            Console.WriteLine($"Вычислите значение функции: (sin(a) + tg(2a))/ квадратный корень из log3(e**2), при а = {a}.");

            while (pop > 0 && !fl)
            {
                Console.WriteLine($"У вас осталось {pop} попыток.");
                double otv = Proverka("");
                if (Math.Abs(otv - f) < 0.01)
                {
                    Console.WriteLine("Поздравляю вы угадали првильный ответ");
                    fl = true;
                }
                else
                {
                    pop--;
                    if (pop > 0)
                    {
                        Console.WriteLine("Неверно. Попробуйте ещё разок");
                    }
                    else
                    {
                        Console.WriteLine($"Жаль, но попытки закончились. Правильный ответ: {f} ");
                    }

                }


            }
            Console.WriteLine("Для выхода в меню нажмите любую клавишу...");
            Console.ReadKey();
            Console.Clear();
        }
        static bool Exit()
        {
            char podtver = ('\0');
            bool fl = false;
            while (!fl)
            {
                Console.Write("Вы точно хотите выйти? (д/н): ");
                string otv = Console.ReadLine();

                if ((otv[0] == 'д' || otv[0] == 'н') && otv.Length == 1)
                {
                    podtver = otv[0];
                    fl = true;
                }
                else
                {
                    Console.WriteLine("Ошибка: Введите 'д' для выхода либо 'н' для отмены ");
                }
            }
            return podtver == 'д';

        }

        static void SortArray()
        {
            int n = Vvod();
            int[] a = new int[n];
            random(a, n);

            int[] rez1 = a.ToArray();
            int[] rez2 = a.ToArray();

            var sw = new Stopwatch();
            sw.Start();
            rez1 = SortPyz(rez1, n);
            sw.Stop();
            double time2 = sw.Elapsed.TotalMilliseconds;

            sw.Restart();
            rez2 = SortVib(rez2, n);
            sw.Stop();
            double time1 = sw.Elapsed.TotalMilliseconds;

            PrintArray("\nИзначальный массив: ", a);
            PrintArray("Сортировка пузырьком: ", rez2);
            Console.WriteLine($"время выполнения: {time1} мс");
            PrintArray("\nСортировка выбором: ", rez1);
            Console.WriteLine($"время выполнения: {time2} мс");

            if (time1 > time2)
                Console.WriteLine("\n===Быстрее сортировка выбором===");
            else if (time1 < time2)
                Console.WriteLine("\n===Быстрее сортировка пузырьком===");
            else
                Console.WriteLine("\n===Время одинаковое===");
        }

        static int[] random(int[] a, int n)
        {
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                a[i] = rnd.Next(-10, 10);
            }
            return a;
        }
        static int Vvod()
        {
            int n;
            while (true)
            {
                Console.Write("Введите размер массива: ");
                if (!int.TryParse(Console.ReadLine(), out n))
                    Console.WriteLine("Ошибка: введите число от 1 до 10");
                else if (n > 10)
                    Console.WriteLine("Ошибка: Массивы не могут быть выведены на экран, так как длина массива больше 10");
                else if (n < 1)
                    Console.WriteLine("Ошибка: введите положительное число");
                else
                    break;
            }
            return n;

        }
        static void PrintArray(string s, int[] a)
        {
            Console.Write(s);

            foreach (int i in a)
            {
                Console.Write("{0} ", i);
            }
            Console.Write("\n");
        }

        static int[] SortPyz(int[] a, int n)
        {

            bool c;
            do
            {
                c = true;

                for (int i = 0; i < n - 1; i++)
                {

                    if (a[i] > a[i + 1])
                    {
                        int b;
                        b = a[i];
                        a[i] = a[i + 1];
                        a[i + 1] = b;
                        c = false;
                    }
                }
            }
            while (!c);
            return a;
        }
        static int[] SortVib(int[] a, int n)
        {

            for (int i = 0; i < n - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (a[j] < a[min])
                    {
                        min = j;
                    }
                }
                if (min != i)
                {
                    int x = a[min];
                    a[min] = a[i];
                    a[i] = x;
                }
            }
            return a;
        }

    }
}
