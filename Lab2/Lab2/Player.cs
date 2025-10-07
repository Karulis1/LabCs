public enum State
{
    Winner,
    Loser,
    Playing,
    NotInGame
}
public class Player
{
    public string name;
    public int location;
    public State state;
    public int distanceTraveled;

    public Player(string name)
    {
        this.name = name;
        this.location = -1;
        this.state = State.NotInGame;
        this.distanceTraveled = 0;
    }

    public void Move(int steps, int fieldSize)
    {
        if (state == State.NotInGame)
        {
            location = steps;
            state = State.Playing;
        }
        else if (state == State.Playing)
        {
            int oldLocation = location;
            int newLocation = location + steps;

            while (newLocation < 1) newLocation += fieldSize;
            while (newLocation > fieldSize) newLocation -= fieldSize;

            location = newLocation;
            distanceTraveled += Math.Abs(steps);
        }
    }
}
