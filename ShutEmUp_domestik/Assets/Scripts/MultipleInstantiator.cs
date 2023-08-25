using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleInstantiator : MonoBehaviour
{
    // instanciador multiple

    [SerializeField] List<Instantiator> instantiators;
    [SerializeField] float delayBetweenInstantiators;

    //metodo get para enemyController
    public int InstantiatorsCount
    {
        get { return instantiators.Count; }
    }

    public void InstantiateInSequence()
    {
        StartCoroutine(InstantiatorSequence());
    }

    IEnumerator InstantiatorSequence()
    {
        foreach (var instantiator in instantiators)
        {
            instantiator.DoInstantiate();
            yield return new WaitForSeconds(delayBetweenInstantiators);
        }
    }

    //instanciar segun index - uso para pickups
    public void InstantiateByIndex(int index)
    {
        if(index < 0 || index >= instantiators.Count)
        {
            Debug.LogErrorFormat("No se puede instanciar con index {0}", index);
            return;
        }

        var instantiator = instantiators[index];
        instantiator.DoInstantiate();
    }
}
