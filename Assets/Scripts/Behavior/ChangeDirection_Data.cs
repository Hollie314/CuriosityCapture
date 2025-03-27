using UnityEngine;


[CreateAssetMenu(fileName = "ChangeDirection_Data", menuName = "CaptureBehaviors/ChangeDirection_Data")]
public class ChangeDirection_Data : Behavior_Data
{
    public override void CaptureBehavior(Curiosity creature)
    {
        creature.OnchangeDirection();
    }
}
