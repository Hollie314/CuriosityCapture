using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //for singleton behavior
    public static GameManager Instance { get; private set; }

    public Curiosity_Data[] curiositie_datas;
    private int index_currentCuriosity = 0;
    
    [SerializeField] GameObject spawnlocation;

    private Dictionary<Curiosity,Curiosity_Data > curiosities;
    public event Action<int, float> OnCapturUpdate;
    public event Action<int> OnStopCapturingCuriosity;
    
    protected void Awake()
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
        
        curiositie_datas = GameController.GameDatabase.Curiosity_Data;
    }
    
    // for singleton Ensures it's created automatically if accessed before existing
    public static GameManager GetInstance()
    {
        if (Instance == null)
        {
            GameObject managerObject = new GameObject("Game_Manager");
            Instance = managerObject.AddComponent<GameManager>();
            DontDestroyOnLoad(managerObject);
        }
        return Instance;
    }

    public void Start()
    {
        curiosities = new Dictionary<Curiosity,Curiosity_Data >();
        
        foreach (var curiosity in curiositie_datas)
        {
            if (curiosity.Apparition_priority == index_currentCuriosity)
            {
                curiosities.Add(Instantiate(curiositie_datas[index_currentCuriosity].Avatar.GetComponent<Curiosity>()),curiosity);
            }
        }
    }

    public void Update()
    {
        foreach (var curiosity in curiosities)
        {
            if (curiosity.Key.isBeeingCaptured)
            {
                curiosity.Value.TriggerBehavior(curiosity.Key, Time.deltaTime);
            }
        }
    }

    public void CaptureBegin(GameObject creatureGO)
    {
        creatureGO.GetComponent<Curiosity>().Capture();
    }
    
    public void CaptureEnd(GameObject creatureGO)
    {
        creatureGO.GetComponent<Curiosity>().UnCapture();
    }

    public void CaptureUpdate(Curiosity curiosity)
    {
        OnCapturUpdate?.Invoke(curiosities[curiosity].Apparition_priority, curiosity.CapturePercent());
    }
}
