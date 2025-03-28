using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //for singleton behavior
    public static GameManager Instance { get; private set; }

    //curiosities
    private List<Curiosity_Data> curiositie_datas;
    private int index_currentCuriosity = 0;
    [SerializeField]private Dictionary<Curiosity,Curiosity_Data > curiosities;
    
    //spawning
    public event Action<Curiosity_Data> OnCapture;
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
        curiositie_datas = GameController.GameDatabase.Curiosity_Data.ToList();
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

        foreach (var curiosity in curiosities)
        {
            curiositie_datas.Remove(curiosity.Value);
        }
    }

    public void CaptureBegin(GameObject creatureGO)
    {
        // Try to get the Curiosity component
        Curiosity curiosityComponent = creatureGO.GetComponent<Curiosity>();

        // Check if the component exists
        if (curiosityComponent != null)
        {
            // Call Capture if the component exists
            curiosityComponent.Capture();
        }
        else
        {
            // Log an error if the component is missing
            Debug.LogError("There is no Curiosity component attached to " + creatureGO.name);
        }
        
       
        
    }
    
    public void CaptureEnd(GameObject creatureGO)
    {
        if (creatureGO.GetComponent<Curiosity>()!=null)
        {
            creatureGO.GetComponent<Curiosity>().UnCapture();
            foreach (var behavior in  curiosities[creatureGO.GetComponent<Curiosity>()].CaptureBehaviors)
            {
                behavior.ResetTime();
            }
        }
        else
        {
            Debug.Log("there is an issue wtf");
        }

        
    }

    private void NextSpawn()
    {
        if (curiosities.Count == 0)
        {
            if (curiositie_datas.Count == 0)
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
    //    capturedCuriosities.Add(curiosities[curiosity]);
        OnCapture?.Invoke(curiosities[curiosity]);
        curiosities.Remove(curiosity);
        
        curiosity.gameObject.SetActive(false);
        NextSpawn();
    }
}
