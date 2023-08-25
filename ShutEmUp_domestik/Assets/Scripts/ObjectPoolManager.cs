using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledEnemyInfo> EnemiesPools = new List<PooledEnemyInfo>();

    ///

    public static List<PooledObjectInfo> ObjectsPools = new List<PooledObjectInfo>();

    private GameObject _objectPoolEmptyHolder;

    static GameObject _particlesSystemsEmpty;
    static GameObject _gameObjectsEmpty;
    static GameObject _enemiesEmpty;


    // enum
    public enum PoolType
    {
        Particulas,
        GameObject,
        Bullets,
        None
    }

    // clase para tipos de pool
    public static PoolType PoolingType;

    private void Awake()
    {
        SetupEmpties();
    }

    // creando y organizando contenedores
    void SetupEmpties()
    {
        _objectPoolEmptyHolder = new GameObject("Pooled Objects");

        _particlesSystemsEmpty = new GameObject("Particles Effects");
        _particlesSystemsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _gameObjectsEmpty = new GameObject("Game Objects");
        _gameObjectsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _enemiesEmpty = new GameObject("Enemies");
        _enemiesEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);
    }


    // el tipo de objeto al llamar al ObjectPoolManager y pasarle como parametro el tipo de objeto es opcional
    // en donde se solicita el objeto se le puede pasar, ObjectPoolManager.PoolType.Particulas - por ejemplo
    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {
        PooledObjectInfo pool = ObjectsPools.Find(p => p.lookupString == objectToSpawn.name);
        // ES LO MISMO QUE ARRIBA 
        //PooledObjectInfo pool = null;
        //foreach (PooledObjectInfo p in ObjectsPools)
        //{
        //    if(p.lookupString == objectToSpawn.name)
        //    {
        //        pool = p;
        //        break;
        //    }
        //}


        // Si no existe
        if(pool == null)
        {
            pool = new PooledObjectInfo() { lookupString = objectToSpawn.name };
            ObjectsPools.Add(pool);
        }

        // Verificar objeto inactivo

        GameObject spawneableObj = pool.InactiveObjects.FirstOrDefault();
        // ES LO MISMO QUE ARRIBA
        //GameObject spawneableObj = null;
        //foreach (GameObject obj in pool.InactiveObjects)
        //{
        //    if(obj != null)
        //    {
        //        spawneableObj = obj;
        //        break;
        //    }
        //}

        if(spawneableObj == null)
        {
            // buscar padre del objeto vacio - Find the parent of the empty object
            GameObject parentObject = SetParentObject(poolType);

            // si no hay obj inactivos, crear
            spawneableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);


            if(parentObject != null)
            {
                spawneableObj.transform.SetParent(parentObject.transform);
            }
        }
        else
        {
            // si hay obj inactivos, reactivar
            spawneableObj.transform.position = spawnPosition;
            spawneableObj.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawneableObj);
            spawneableObj.SetActive(true);
        }


        return spawneableObj;
    }

    // metodo para enemies
    internal static EnemyController SpawnObject(EnemyController enemyController, Vector3 enemyPos, Quaternion rot)
    {
        PooledEnemyInfo pool = EnemiesPools.Find(p => p.lookupString == enemyController.name);
        // ES LO MISMO QUE ARRIBA 
        //PooledObjectInfo pool = null;
        //foreach (PooledObjectInfo p in ObjectsPools)
        //{
        //    if(p.lookupString == objectToSpawn.name)
        //    {
        //        pool = p;
        //        break;
        //    }
        //}



        // Si no existe
        if (pool == null)
        {
            pool = new PooledEnemyInfo() { lookupString = enemyController.name };
            EnemiesPools.Add(pool);
        }

        // Verificar objeto inactivo

        EnemyController spawneableObj = pool.InactiveObjects.FirstOrDefault();
        // ES LO MISMO QUE ARRIBA
        //GameObject spawneableObj = null;
        //foreach (GameObject obj in pool.InactiveObjects)
        //{
        //    if(obj != null)
        //    {
        //        spawneableObj = obj;
        //        break;
        //    }
        //}

        if (spawneableObj == null)
        {
            // buscar padre del objeto vacio - Find the parent of the empty object
            var parentObject = _enemiesEmpty.transform;

            // si no hay obj inactivos, crear
            spawneableObj = Instantiate(enemyController, enemyPos, rot);


            if (parentObject != null)
            {
                spawneableObj.transform.SetParent(parentObject.transform);
            }
        }
        else
        {
            // si hay obj inactivos, reactivar
            spawneableObj.transform.position = enemyPos;
            spawneableObj.transform.rotation = rot;
            pool.InactiveObjects.Remove(spawneableObj);
            //spawneableObj.SetActive(true);
            spawneableObj.gameObject.SetActive(true);
        }


        return spawneableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        //by taken off 7, we are removing the (clone) from the name of the passed in object
        string goName = obj.name.Substring(0, obj.name.Length - 7);
        PooledObjectInfo pool = ObjectsPools.Find(p => p.lookupString == goName);

        if(pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not pooled: " + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }

    public static void ReturnEnemyToPool(EnemyController obj)
    {
        //by taken off 7, we are removing the (clone) from the name of the passed in object
        string goName = obj.name.Substring(0, obj.name.Length - 7);
        PooledEnemyInfo pool = EnemiesPools.Find(p => p.lookupString == goName);

        if (pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not pooled: " + obj.name);
        }
        else
        {
            obj.gameObject.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }


    private static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.Particulas:
                return _particlesSystemsEmpty;
            case PoolType.GameObject:
                return _gameObjectsEmpty;
            case PoolType.None:
                return null;
            default:
                return null;
        }
    }

}


public class PooledObjectInfo
{
    public string lookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}

public class PooledEnemyInfo
{
    public string lookupString;
    public List<EnemyController> InactiveObjects = new List<EnemyController>();
}