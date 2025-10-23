using System;
using System.IO;
using System.Collections.Generic;

enum GameState
{
    Start,
    End
}

class Game
{
    public static string InputFile;
    public static string OutFile;

    public int size;
    public Player cat;
    public Player mouse;
    public GameState state = GameState.Start;

    public int cheeseLocation = -1;

    private List<string> outputLines = new();

    public Game(int size)
    {
        this.size = size;
        cat = new Player("Cat");
        mouse = new Player("Mouse");

        Random rnd = new();
        cheeseLocation = rnd.Next(0, size);
    }

    public void Run()
    {
        string[] lines = File.ReadAllLines(InputFile);

        foreach (string line in lines[1..])
        {
            if (state == GameState.End) break;

            string[] parts = line.Split(' ');
            if (parts[0] == "M")
                DoMoveCommand('M', int.Parse(parts[1]));
            else if (parts[0] == "C")
                DoMoveCommand('C', int.Parse(parts[1]));
            else if (parts[0] == "P")
                DoPrintCommand();
        }

        WriteSummary();
        File.WriteAllLines(OutFile, outputLines);
    }

    private void DoMoveCommand(char who, int steps)
    {
        if (who == 'M') mouse.Move(steps, size);
        else if (who == 'C') cat.Move(steps, size);

        if (mouse.IsInGame() && mouse.location == cheeseLocation && mouse.state != State.Transformed)
        {
            mouse.state = State.Transformed;
            outputLines.Add($"Мышь съела сыр и стала котом!");
        }
        if (cat.IsInGame() && mouse.IsInGame() && cat.location == mouse.location)
        {
            cat.state = State.Winner;
            mouse.state = State.Loser;
            state = GameState.End;
        }
    }

    private void DoPrintCommand()
    {
        string catLoc = cat.GetLocation();
        string mouseLoc = mouse.GetLocation();
        string dist = (cat.IsInGame() && mouse.IsInGame()) ? Math.Abs(cat.location - mouse.location).ToString() : "??";
        string cheeseLoc = cheeseLocation >= 0 ? (cheeseLocation + 1).ToString() : "??";
        outputLines.Add($"{catLoc,-5} {mouseLoc,-7} {dist,-8} Cheese: {cheeseLoc}");
    }

    private void WriteSummary()
    {
        outputLines.Insert(0, "Cat and Mouse");
        outputLines.Insert(1, "");
        outputLines.Insert(2, "Cat   Mouse   Distance");
        outputLines.Insert(3, "----------------------");
        outputLines.Add("----------------------");
        outputLines.Add("");
        outputLines.Add("");
        outputLines.Add($"Cat distance traveled: {cat.distanceTraveled}");
        outputLines.Add($"Mouse distance traveled: {mouse.distanceTraveled}");
        outputLines.Add("");

        if (cat.location == mouse.location && cat.IsInGame() && mouse.IsInGame())
            outputLines.Add($"Мышь поймана в клетке: {cat.location + 1} - Mouse caught at: {cat.location + 1}");
        else
            outputLines.Add("Мышь ускользнула от кота - Mouse evaded Cat");
    }
}
