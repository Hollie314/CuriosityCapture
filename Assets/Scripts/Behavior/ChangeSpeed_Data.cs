using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeSpeed_Data", menuName = "Behavior/ChangeSpeed_Data")]
[Serializable]
public class ChangeSpeed_Data : Behavior
{
    [field: SerializeField]
    public float Dash_speed { get; private set; }
    [field: SerializeField]
    public float TimeBeforeResetSpeed { get; private set; }
    
    public override void CaptureBehavior(Curiosity creature, float time)
    {
        CurrentTime += time;
        if (CurrentTime >= TimeBeforeTriggerBehavior)
        {
            creature.OnSpeedUp(Dash_speed, TimeBeforeResetSpeed);
            CurrentTime = -TimeBeforeResetSpeed;
        }
    }
}
