using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeDirection_Data", menuName = "Behavior/ChangeDirection_Data")]
[Serializable]
public class ChangeDirection_Data : Behavior
{
    public override void CaptureBehavior(Curiosity creature, float time)
    {
        CurrentTime += time;
        if (CurrentTime >= TimeBeforeTriggerBehavior)
        {
            creature.OnchangeDirection();
            CurrentTime = 0;
        }
    }
}
