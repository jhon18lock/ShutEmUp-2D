using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<EnemyWavesConfig> wavesConfigs;
    //public List<Transform> spawnPoints;

    [SerializeField] Quaternion spawnRotation; //rotar en Z 180 grados

    [SerializeField] float initialWaitTime;
    [SerializeField] float cadenceBetweenWaves;

    private void Start()
    {
        StartCoroutine(EnemySpawnerCoroutine());

        //suscribirse a evento de GameController
        GameController.Instance.OnEnemyDied += OnEnemyDied;
    }

    private void OnDestroy()
    {
        //desuscribirse
        if(GameController.Instance != null)
        {
            GameController.Instance.OnEnemyDied -= OnEnemyDied;
        }
    }


    //metodo suscrito de GameController - Game ¿Controller crea este delegado
    // Nosotros nos suscribimos
    //esto ejecuta cuando la funcion de gamecontroller OnDie es llamada 
    // por EnemyController cuando se destruye un enemigo
    private void OnEnemyDied(GameObject go)
    {
        Debug.LogFormat("I'm Script Spawn, I have detected one of my little spawn have died T_T... name= {0}", go.name);
    }

    private IEnumerator EnemySpawnerCoroutine()
    {
        yield return new WaitForSeconds(initialWaitTime);
        foreach (var wave in wavesConfigs)
        {

            foreach (var enemy in wave.enemies)
            {
                Vector3 enemyPosition = Vector3.zero;

                if (enemy.useSpecificXPosition)
                {
                    enemyPosition = enemy.spawnReferencePosition;
                }
                else
                {
                    enemyPosition = new Vector3(Random.Range(-enemy.spawnReferencePosition.x, enemy.spawnReferencePosition.x), enemy.spawnReferencePosition.y, enemy.spawnReferencePosition.z);
                }

                //SpawnEnemy(enemy.enemyPrefab, enemy.config, enemyPosition, spawnRotation);
                SpawnEnemy(enemy.enemyPrefab, enemy.config, enemyPosition, spawnRotation);


                yield return new WaitForSeconds(wave.cadence);
            }
            yield return new WaitForSeconds(cadenceBetweenWaves);
        }


    }

    public void SpawnEnemy(GameObject enemyPrefab, EnemyConfig config, Vector3 enemyPos, Quaternion rot)
    {
        var enemyInstance = ObjectPoolManager.SpawnObject(enemyPrefab, enemyPos, rot);

        enemyInstance.GetComponent<EnemyController>().config = config;

        

        //informar eneigo creado
        GameController.Instance.EnemigoCreado();

    }

    //public void SpawnEnemy(EnemyController enemyPrefab, EnemyConfig config, Vector3 enemyPos, Quaternion rot)
    //{
    //    var enemyInstance = ObjectPoolManager.SpawnObject(enemyPrefab, enemyPos, rot);
        
    //    enemyInstance.config = config;



    //    //informar eneigo creado
    //    GameController.Instance.EnemigoCreado();
        
    //}

    
}
