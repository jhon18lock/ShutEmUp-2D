
using UnityEngine;

[CreateAssetMenu(fileName = "New Shooting config", menuName = "Shooting Config SO")]

public class ShootingConfig : ScriptableObject
{
    public float shootInitialWaitTime;

    public float shootCadence;

    public bool waitPerShootings;

    public int shootsAmount = 3;

    public float waitTimePerShootings;

    public bool autoShooting = false;
}