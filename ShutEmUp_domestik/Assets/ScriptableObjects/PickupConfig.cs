using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//enums, se puede acceder desde cualquier clase
public enum PickupType
{
    None,
    Laser,
    Shield
}

[CreateAssetMenu(fileName ="New PickupConfig", menuName ="Pickups")]
public class PickupConfig : ScriptableObject
{
    public PickupType type;
    public int score;

    public float durationInSeconds;
}
