using System.IO;

// https://acmp.ru/index.asp?main=task&id_task=320

namespace practical_task2
{
    class Program
    {
        // Чтение данных из файла
        static string[] ReadData(string filePath)
        {
            using (StreamReader inputStream = new StreamReader(filePath)) return inputStream.ReadToEnd().Split(' ');
        }

        // Запись данных в файл
        static void WriteData(string filePath, ulong data)
        {
            using (StreamWriter sw = new StreamWriter(filePath)) sw.WriteLine(data);
        }

        // Подсччёт количества сочетаний из n по k
        static ulong Combinations(ulong n, ulong k)
        {
            // Количество сочетаний из n по 0 всегда равно 1
            if (k == 0) return 1;

            // Делаем так, чтобы k было меньше(или равно) чем n-k
            if (n - k < k) k = n - k;

            // Переменная для результата
            ulong result = 1;

            // n! / (k!*(n-k)!) = (n*(n-1)*(n-2)*...*(n-k+1)) / k!  -  количество сочетаний из n по k

            // Переменная для множителей в знаменателе
            ulong d = 2;

            // Цикл проходит по всем множителя в числителе
            for (ulong i = n - k + 1; i <= n; i++) 
            {
                // Умножаем результат на множитель из числителя
                result *= i;

                // Делим результат на множитель из знаменателя
                if (d <= k && result % d == 0) result /= d++;
            }
            
            // Делим результат на оставшиеся множители из знаменателя
            for (ulong i = d; i <= k; i++) result /= i;

            return result;
        }

        static void Main(string[] args)
        {
            // Считываем данные
            string[] input = ReadData("INPUT.TXT");
            ulong wight = ulong.Parse(input[0]), length = ulong.Parse(input[1]);

            // Переменная для результата
            ulong result = 0;

            // Максимальное кол-во "блоков" из плиток вдоль по коридору
            ulong maxAlong = length / wight;

            ulong n, k;

            // Цикл проходит от минимального до максимального кол-ва "блоков" из плиток расположенных вдоль по коридору
            for (ulong i = 0; i <= maxAlong; i++)
            {
                n = i + 1;
                k = length - wight * i;
                result += Combinations(n + k - 1, k);
            }

            // Запись результата в файл
            WriteData("OUTPUT.TXT", result);
        }
    }
}
