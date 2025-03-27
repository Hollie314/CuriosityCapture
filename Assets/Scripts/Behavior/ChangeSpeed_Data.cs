using UnityEngine;


[CreateAssetMenu(fileName = "ChangeSpeed_Data", menuName = "CaptureBehaviors/ChangeSpeed_Data")]
public class ChangeSpeed_Data : Behavior_Data
{
    public override void CaptureBehavior(Curiosity creature)
    {
        creature.OnSpeedUp();
    }
}
