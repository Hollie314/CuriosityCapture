using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //for singleton behavior
    public static GameManager Instance { get; private set; }

    //curiosities
    private Curiosity_Data[] curiositie_datas;
    private List<Curiosity_Data> curiosities_leftToCatch;
    private int index_currentCuriosity = 0;
    [SerializeField]private Dictionary<Curiosity,Curiosity_Data > curiosities;
    
    //spawning
    public static event Action<int> OnCapture;
    [SerializeField] GameObject[] spawnLocations;
    
    //player
    private List<Curiosity_Data> capturedCuriosities;
    
    
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
        curiosities_leftToCatch = new List<Curiosity_Data>();
        curiositie_datas = GameController.GameDatabase.Curiosity_Data;
        curiosities_leftToCatch = curiositie_datas.ToList();
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
        capturedCuriosities = new List<Curiosity_Data>();
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
        foreach (var curiosity in curiosities_leftToCatch)
        {
            if (curiosity.Apparition_priority == index_currentCuriosity)
            {
                Curiosity script = Instantiate(curiosity.Avatar.GetComponent<Curiosity>(), spawnLocations[curiosity.Spawn_Location_Index].transform);
                curiosities.Add(script,curiosity);
                script.Initialize(curiosity.Speed, curiosity.MaxCapturePoint, curiosity.CaptureSpeed, curiosity.UncaptureSpeed, curiosity.splineObject);
            }
        }

        foreach (var curiosity in curiosities)
        {
            curiosities_leftToCatch.Remove(curiosity.Value);
        }
    }

    public void CaptureBegin(Curiosity curiosity)
    {
        // Call Capture if the component exists
        curiosity.Capture();
        
    }
    
    public void CaptureEnd(Curiosity curiosity)
    {
        curiosity.UnCapture();
        foreach (var behavior in  curiosities[curiosity].CaptureBehaviors)
        {
            behavior.ResetTime();
        }
    }

    private void NextSpawn()
    {
        if (curiosities.Count == 0)
        {
            if (curiosities_leftToCatch.Count == 0)
            {
                EndGame();
            }
            else
            {
                index_currentCuriosity++;
                InstantiateCuriosity();
                if (curiosities.Count == 0)
                {
                    NextSpawn();
                }
            }
        }
    }

    public void EndGame()
    {
        Debug.Log("end game");
        //to implement
    }
    public void Capture(Curiosity curiosity)
    {
        Debug.Log("it is captured !");
      
        int index = Array.IndexOf(curiositie_datas, curiosities[curiosity]);
        capturedCuriosities.Add(curiosities[curiosity]);
        OnCapture?.Invoke(index);
        
        curiosity.gameObject.SetActive(false);
        curiosities.Remove(curiosity);

        curiosity.DestroythisObject();
        NextSpawn();
    }
}
