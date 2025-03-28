using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Curiosity_Data", menuName = "Scriptable Objects/Curiosity_Data")]
public class Curiosity_Data : ScriptableObject
{
    [field: SerializeField]
    public String Curiosity_name { get; private set; }
    
    [field: SerializeField]
    public String Curiosity_description { get; private set; }
    
    [field: SerializeField]
    public int Apparition_priority { get; private set; }
    
    [field: SerializeField]
    public int Spawn_Location_Index { get; private set; }
    
    [field: SerializeField]
    public GameObject splineObject { get; private set;}
    
    [field: SerializeField]
    public float Speed { get; private set;}
    
    [field: SerializeField]
    public int MaxCapturePoint { get; private set;}
    
    [field: SerializeField]
    public float CaptureSpeed { get; private set;}
    
    [field: SerializeField]
    public float UncaptureSpeed { get; private set; }
    
    
    [field: SerializeReference] 
    public List<Behavior> CaptureBehaviors { get; private set; }
    
    [field: SerializeField]
    public GameObject Avatar { get; private set; }
    
    public void TriggerBehavior(Curiosity creature, float time)
    {
        if (CaptureBehaviors != null)
        {
            foreach (var captureBehavior in CaptureBehaviors)
            {
                captureBehavior.CaptureBehavior(creature, time);
            }
        }
        else
        {
            Debug.Log(creature.name + " has no capture behavior assigned.");
        }
    }
}
