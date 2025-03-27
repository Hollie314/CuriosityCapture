using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    //for singleton behavior
    public static Game_Manager Instance { get; private set; }
    
    private void Awake()
    {
        //for singleton behavior
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //for singleton behavior_end
    }
    
    // for singleton Ensures it's created automatically if accessed before existing
    public static Game_Manager GetInstance()
    {
        if (Instance == null)
        {
            GameObject managerObject = new GameObject("Game_Manager");
            Instance = managerObject.AddComponent<Game_Manager>();
            DontDestroyOnLoad(managerObject);
        }
        return Instance;
    }
}
