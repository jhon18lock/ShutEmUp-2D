using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    Coroutine returnToPoolTimerCoroutine;
    [SerializeField] bool destroyByTime;
    [SerializeField] float destroyTime = 7f;
    public void DoDestroy()
    {
        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }

    private void OnEnable()
    {
        if (destroyByTime)
        {
            returnToPoolTimerCoroutine = StartCoroutine(ReturnToPoolAfterTime());

        }
    }


    IEnumerator ReturnToPoolAfterTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < destroyTime)
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
