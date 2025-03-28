using UnityEngine;

public class GameDatabase
{
    
    public Curiosity_Data[] Curiosity_Data { get; private set; }
    
    public GameDatabase()
    {
        Curiosity_Data = Resources.LoadAll<Curiosity_Data>("Curiosities/Curiosity_Data");
    }
}
