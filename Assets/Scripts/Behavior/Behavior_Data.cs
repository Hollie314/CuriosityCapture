using UnityEngine;

[CreateAssetMenu(fileName = "Behavior_Data", menuName = "Scriptable Objects/Behavior_Data")]
public abstract class Behavior_Data : ScriptableObject
{
    public abstract void CaptureBehavior(Curiosity creature);
}
