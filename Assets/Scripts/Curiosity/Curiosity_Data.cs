using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Curiosity_Data", menuName = "Scriptable Objects/Curiosity_Data")]
public class Curiosity_Data : ScriptableObject
{
    [field: SerializeField]
    public String curiosity_name { get; private set; }
    
    [field: SerializeField]
    public String curiosity_description { get; private set; }
    
    [field: SerializeField]
    public int apparition_priority { get; private set; }
    
    [field: SerializeField]
    public float speed { get; private set; }
    
    [field: SerializeField]
    public float timeBeforeTriggerBehavior { get; private set; }
    
    [field: SerializeField]
    public List<Behavior_Data> captureBehaviors { get; private set; }
    
    [field: SerializeField]
    public GameObject Avatar { get; private set; }
    
    public void TriggerBehavior(Curiosity creature)
    {
        if (captureBehaviors != null)
        {
            foreach (var captureBehavior in captureBehaviors)
            {
                captureBehavior.CaptureBehavior(creature);
            }
        }
        else
        {
            Debug.Log(creature.name + " has no capture behavior assigned.");
        }
    }
}
