using UnityEngine.Events;


public static class Level
{
    public static UnityEvent Start;
    public static UnityEvent EndOfLevel;
    
    public static UnityEvent GangIsKilled;
    public static UnityEvent EnemyIsKilled;

    public static UnityEvent PlayerOnThePosition;
    public static UnityEvent PlayerOffThePosition;

    public static void Initialize()
    {
        if (Start == null)
            Start = new UnityEvent();
        
        if (EndOfLevel == null)
            EndOfLevel = new UnityEvent();
        
        if (GangIsKilled == null)
            GangIsKilled = new UnityEvent();
        
        if (EnemyIsKilled == null)
            EnemyIsKilled = new UnityEvent();
        
        if (PlayerOnThePosition == null)
            PlayerOnThePosition = new UnityEvent();
        
        if (PlayerOffThePosition == null)
            PlayerOffThePosition = new UnityEvent();
    }
}
