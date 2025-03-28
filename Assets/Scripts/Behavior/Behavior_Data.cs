using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Curiosity_Data", menuName = "Scriptable Objects/Curiosity_Data")]
 [Serializable]
    public abstract class Behavior : ScriptableObject
    {
        [field: SerializeField]
        public float TimeBeforeTriggerBehavior { get; private set; }
        public float CurrentTime { get; set; }
        public abstract void CaptureBehavior(Curiosity creature, float time);

        public void ResetTime()
        {
            CurrentTime = 0;
        }
    }

