using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tinter : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float timeToBack = 0.5f;
    public Color tintColor = Color.red;
    Color originalColor;

    Coroutine tinterCoroutine;

    private void Start()
    {
        originalColor = spriteRenderer.color;

        
    }

    public void DoTint()
    {

        tinterCoroutine = StartCoroutine(TintBackToOriginal());
    }


    IEnumerator TintBackToOriginal()
    {
        float elapsed = 0f;
        float duration = timeToBack;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                spriteRenderer.color = tintColor;
            }
            else
            {
                spriteRenderer.color = originalColor;
            }

            yield return null;
        }

        spriteRenderer.color = originalColor;
    }

    private void OnDisable()
    {
        if(tinterCoroutine != null)
        {
            StopCoroutine(tinterCoroutine);

        }

        spriteRenderer.color = originalColor;
    }
}
