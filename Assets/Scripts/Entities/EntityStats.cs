using System;
using UnityEngine;



[CreateAssetMenu(fileName = "New Entity Stats", menuName = "Entity Data/Basic Entity Stats"), Serializable]
public class EntityStats : ScriptableObject
{
    [field: SerializeField]public int HitPoints {get; private set;}
    [field: SerializeField]public float MaxSpeed {get; private set;}
    [field: SerializeField]public float Acceleration {get; private set;}
    [field: SerializeField]public float JumpForce {get; private set;}
}
