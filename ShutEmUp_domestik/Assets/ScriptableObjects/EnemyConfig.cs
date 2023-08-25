using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New enemy config", menuName = "Enemy Config SO")]

public class EnemyConfig : ScriptableObject
{
    #region
    public float moverSpeed;

    public Sprite sprite;

    public bool isShooter;

    //public float shootInitialWaitTime;

    //public float shootCadence;

    public int score;

    [Range(0, 1)]
    public float pickupChance;

    public bool shouldThrowPickup()
    {
        return DiceDado.IsChanceSuccess(pickupChance);
    }
    #endregion
}