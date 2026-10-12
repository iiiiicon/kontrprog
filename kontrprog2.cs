
using System;

class Program
{
    // Главный метод 
    // Вызывает все остальные методы в нужном порядке
    static void Main()
    {
        int n = ReadStudentCount();

        int[] scores = ReadScores(n);

        double average = CalculateAverage(scores);

        int bestStudent = FindBestStudent(scores);

        int aboveAverage = CountAboveAverage(scores, average);

        int[] categories = CountCategories(scores);

        PrintReport(scores, average, bestStudent,
                    aboveAverage, categories);
    }

    // Метод для ввода количества студентов
    // Проверяет, чтобы число было от 1 до 50
    static int ReadStudentCount()
    {
        while (true)
        {
            Console.Write("Введите число студентов (1..50): ");

            // Проверяю верность ввода
            if (int.TryParse(Console.ReadLine(), out int n)
                && n >= 1 && n <= 50)
            {
                return n;
            }

            // Если число неправильное, прошу ввести заново
            Console.WriteLine("Ошибка! Введите число от 1 до 50.");
        }
    }

    // Метод для ввода балла одного студента
    // Проверяет, чтобы балл был от 0 до 100
    static int ReadScore(int studentNumber)
    {
        while (true)
        {
            Console.Write($"Балл студента {studentNumber} (0..100): ");

            // Проверяю введённый балл
            if (int.TryParse(Console.ReadLine(), out int score)
                && score >= 0 && score <= 100)
            {
                return score;
            }

            // При ошибке повторяем ввод
            Console.WriteLine("Ошибка! Введите число от 0 до 100.");
        }
    }

    // Метод для ввода баллов всех студентов
    // Создаёт массив и заполняет его баллами
    static int[] ReadScores(int n)
    {
        int[] scores = new int[n];

        // Прохожу по всем студентам
        for (int i = 0; i < n; i++)
        {
            scores[i] = ReadScore(i + 1);
        }

        return scores;
    }

    // Метод для вычисления среднего балла
    // Складывает все баллы и делит сумму на число студентов
    static double CalculateAverage(int[] scores)
    {
        int sum = 0;

        // Нахожу сумму всех баллов
        for (int i = 0; i < scores.Length; i++)
        {
            sum += scores[i];
        }

        // Вычисляю среднее арифметическое
        return (double)sum / scores.Length;
    }

    // Метод для поиска лучшего студента
    // Если максимальный балл одинаковый
    // выбирается первый студент с таким баллом
    static int FindBestStudent(int[] scores)
    {
        int bestIndex = 0;

        // Сравниваю баллы студентов
        for (int i = 1; i < scores.Length; i++)
        {
            if (scores[i] > scores[bestIndex])
            {
                bestIndex = i;
            }
        }

        // Прибавляю 1, так как студенты нумеруются с 1
        return bestIndex + 1;
    }

    // Метод для подсчёта студентов
    // набравших строго больше среднего балла
    static int CountAboveAverage(int[] scores, double average)
    {
        int count = 0;

        // Проверяю балл каждого студента
        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] > average)
            {
                count++;
            }
        }

        return count;
    }

    // Метод для распределения баллов по категориям
    // 0-39   - неудовлетворительно
    // 40-59  - удовлетворительно
    // 60-79  - хорошо
    // 80-100 - отлично
    static int[] CountCategories(int[] scores)
    {
        int[] categories = new int[4];

        // Проверяю балл каждого студента
        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] <= 39)
            {
                categories[0]++;
            }
            else if (scores[i] <= 59)
            {
                categories[1]++;
            }
            else if (scores[i] <= 79)
            {
                categories[2]++;
            }
            else
            {
                categories[3]++;
            }
        }

        return categories;
    }

    // Метод для вывода гистограммы.
    // Каждая звёздочка обозначает одного студента.
    static void PrintHistogram(int[] categories)
    {
        string[] names =
        {
            "неуд.  (0-39)",
            "удовл. (40-59)",
            "хор.   (60-79)",
            "отл.   (80-100)"
        };

        // Вывожу название категории, звёздочки и количество
        for (int i = 0; i < categories.Length; i++)
        {
            Console.WriteLine(
                $"{names[i]}: {new string('*', categories[i])} {categories[i]}"
            );
        }
    }

    // Метод для вывода итогового отчёта.
    // Показывает средний балл, лучший результат,
    // количество студентов выше среднего и гистограмму.
    static void PrintReport(int[] scores, double average,
                            int bestStudent, int aboveAverage,
                            int[] categories)
    {
        Console.WriteLine();
        Console.WriteLine("===== ОТЧЁТ =====");

        // Вывод среднего балла с одним знаком после запятой
        Console.WriteLine($"Средний балл: {average:F1}");

        // Вывод лучшего результата и номера студента
        Console.WriteLine(
            $"Лучший результат: {scores[bestStudent - 1]} " +
            $"(студент {bestStudent})"
        );

        // Вывод количества студентов выше среднего
        Console.WriteLine(
            $"Выше среднего: {aboveAverage} из {scores.Length}"
        );

        // Вывод распределения оценок
        Console.WriteLine("Распределение оценок:");

        PrintHistogram(categories);
    }
}

