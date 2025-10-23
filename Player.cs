enum State
{
    Winner,
    Loser,
    Playing,
    NotInGame,
    Transformed,

}
// доделать объект где есть появляется сыр и если мышь съедает этот сыр она становится котом. 
class Player
{
    public string name;
    public int location;
    public State state = State.NotInGame;
    public int distanceTraveled = 0;

    public Player(string name)
    {
        this.name = name;
        this.location = -1;
    }

    public bool IsInGame() => state != State.NotInGame;

    public string GetLocation() => IsInGame() ? (location + 1).ToString() : "??";

    public void Move(int steps, int fieldSize)
    {
        if (!IsInGame())
        {
            location = steps - 1;
            state = State.Playing;
        }
        else
        {
            location = (location + steps + fieldSize) % fieldSize;
            distanceTraveled += Math.Abs(steps);
        }
    }
}
