using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    Coroutine returnToPoolTimerCoroutine;
    [SerializeField] bool destroyByTime;
    [SerializeField] float destroyTime = 7f;


    private void OnEnable()
    {
        if (destroyByTime)
        {
            returnToPoolTimerCoroutine = StartCoroutine(ReturnToPoolAfterTime());

        }
    }

    public void DesactivarObjeto()
    {
        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }


    IEnumerator ReturnToPoolAfterTime()
    {
        float elapsedTime = 0f;

        while(elapsedTime < destroyTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
