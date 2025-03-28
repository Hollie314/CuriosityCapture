using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //for singleton behavior
    public static GameManager Instance { get; private set; }

    //curiosities
    private Curiosity_Data[] curiositie_datas;
    private int index_currentCuriosity = 0;
    private Dictionary<Curiosity,Curiosity_Data > curiosities;
    
    //spawning
    public event Action<Curiosity_Data> OnCapture;
    [SerializeField] GameObject[] spawnLocations;
    
    
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
        InstantiateCuriosity();
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

    private void InstantiateCuriosity()
    {
        foreach (var curiosity in curiositie_datas)
        {
            if (curiosity.Apparition_priority == index_currentCuriosity)
            {
                Curiosity script = Instantiate(curiosity.Avatar.GetComponent<Curiosity>(), spawnLocations[curiosity.Spawn_Location_Index].transform);
                curiosities.Add(script,curiosity);
                script.Initialize(curiosity.Speed, curiosity.MaxCapturePoint, curiosity.CaptureSpeed, curiosity.UncaptureSpeed, curiosity.splineObject);
            }
        }
    }

    public void CaptureBegin(GameObject creatureGO)
    {
        Curiosity curiosity = creatureGO.GetComponent<Curiosity>();
        curiosity.Capture();
    }
    
    public void CaptureEnd(GameObject creatureGO)
    {
        Curiosity curiosity = creatureGO.GetComponent<Curiosity>();
        curiosity.UnCapture();
        foreach (var behavior in  curiosities[curiosity].CaptureBehaviors)
        {
            behavior.ResetTime();
        }
    }

    public void Capture(Curiosity curiosity)
    {
        OnCapture?.Invoke(curiosities[curiosity]);;
    }
}
