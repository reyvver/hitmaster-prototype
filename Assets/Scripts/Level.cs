using UnityEngine.Events;

public static class Level
{
    public static UnityEvent Start;
    public static UnityEvent EndOfLevel;

    public static void Initialize()
    {
        if (Start == null)
            Start = new UnityEvent();
        
        if (EndOfLevel == null)
            EndOfLevel = new UnityEvent();
    }
}
