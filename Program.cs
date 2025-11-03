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

        int size = 20; // Жестко заданный размер поля

        Game game = new Game(size);
        game.Run();

        Console.WriteLine($"Игра завершена. Результаты записаны в '{Game.OutFile}'.");
    }
}
