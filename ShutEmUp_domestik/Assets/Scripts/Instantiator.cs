using UnityEngine;

public class Instantiator : MonoBehaviour
{
    public GameObject prefab;

    public void DoInstantiate()
    {
        //instanciar como objeto separado del padre
        //Instantiate(prefab, transform.position, transform.rotation);

        /* instanciar como hijo
        Instantiate(prefab, transform, false);
        */

        ObjectPoolManager.SpawnObject(prefab, transform.position, transform.rotation);
    }
}
