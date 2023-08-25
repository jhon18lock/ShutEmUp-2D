using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WaveConfig", menuName ="WaveConfig SO")]
public class EnemyWavesConfig : ScriptableObject
{
    [Serializable]
    public class EachEnemyConfig
    {
        //public EnemyController enemyPrefab;
        public GameObject enemyPrefab;
        public Vector3 spawnReferencePosition;
        public bool useSpecificXPosition;

        public EnemyConfig config;
    }

    public List<EachEnemyConfig> enemies;

    public float cadence;
}
