using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Game.InputFile = "ChaseData.txt";
        Game.OutFile = "PursuitLog.txt";

        if (!File.Exists(Game.InputFile))
        {
            Console.WriteLine($"Ошибка: файл '{Game.InputFile}' не найден.");
            return;
        }

        string[] lines = File.ReadAllLines(Game.InputFile);

        if (lines.Length == 0)
        {
            Console.WriteLine("Ошибка: входной файл пуст.");
            return;
        }

        if (!int.TryParse(lines[0], out int size) || size <= 0)
        {
            Console.WriteLine("Ошибка: первая строка должна быть положительным числом — размер игрового поля.");
            return;
        }

        Game game = new Game(size);
        game.Run();

        Console.WriteLine($"Игра завершена. Результаты записаны в '{Game.OutFile}'.");
    }
}
